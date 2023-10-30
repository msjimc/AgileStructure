using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AgileStructure
{
    class AlignedRead
    {
        public enum fragmentType
        {
            Insert,
            Match,
            Deletion,
            Unaligned
        }
        public struct fragment
        {
            public fragment(int Length, fragmentType State)
            {
                state = State;
                length = Length;
            }
            public fragmentType state { get; }
            public int length { get; }
        }

        private int index;
        private string name;
        private int flag;
        private int MAPQ;
        private string sequence;
        private string quality;
        private string CIGAR;
        private int referenceIndex = -1;
        private int positions = -1;
        private bool forward;
        private int alignedLength;
        private List<fragment> blocks;
        private List<fragment> blocksSimplified;
        private Dictionary<string, string> tags;
        private bool isGood;
        private bool isSecondaryAlignment;
        private bool isSupplementaryAlignment;
        private bool drawn = false;
        private bool largeIndel = false;

        private List<string> variants = null;

        public AlignedRead(string line, int Index)
        {
            try
            {
                index = Index;
                string[] items = line.Split('\t');

                name = items[0];
                sequence = items[9];
                quality = items[10];
                CIGAR = items[5];
                MAPQ = Convert.ToInt32(items[4]);
                positions = Convert.ToInt32(items[3]);
                referenceIndex = Convert.ToInt32(items[2]);
                flag = Convert.ToInt32(items[1]);
                isSecondaryAlignment = (256 & flag) == 256;
                isSupplementaryAlignment = (2048 & flag) == 2048;
                forward = !((16 & flag) == 16);
                alignedLength = getAlignedLengthAndMakeBlock(CIGAR);
                makeBlocksSimplified();
                tags = new Dictionary<string, string>();
                if (items.Length > 11)
                {
                    for (int index = 11; index < items.Length; index++)
                    {
                        if (items[index].Length > 3)
                        { tags.Add(items[index].Substring(0, 2), items[index].Substring(3)); }
                    }
                }
                isGood = true;
            }
            catch (Exception ex)
            {
                isGood = false;
            }

        }

        public string[] displayDataArray()
        {
            string strand = "";
            if (forward == true)
            { strand = "Forward"; }
            else
            { strand = "Reverse"; }
            string[] data = { "1", "2", "3" };
            data[0] =  "Name: " + name + "\nFlag: " + flag.ToString() + "\nStrand: " + strand + "\nReference: ";
            data[1] =  referenceIndex.ToString();
            data[2] = "\nPosition: " + positions.ToString("N0") + " - " + getEndPosition.ToString("N0") + "\nSecondary alignments" + getSecondaryAlignmentTag
                + "\nMapping Quality: " + MAPQ.ToString() + "\nCIGAR:\n" + CIGAR + "\nSequence:\n" + sequence + "\nQuality\n" + quality + "\n";                 
            foreach (string t in tags.Keys)
            { data[2] += t + ":" + tags[t] +  "\n"; }
            data[2] += "\n";
            return data;
        }

        public void drawMe(Graphics g, float top, int featureHeight, int start, double XScale, List<int> selectedIndex, bool simplified)
        {            
            List<fragment> lBlocks;
            if (simplified == true)
            { lBlocks = blocksSimplified; }
            else
            { lBlocks = blocks; }

            RectangleF r = new RectangleF(1.0f, top, 1.0f, (float)featureHeight);
            float alignStart = -1;
            float alignEnd = -1;

            SolidBrush aligned;
            SolidBrush unaligned;
            if (forward == true)
            {
                aligned = new SolidBrush(Color.Green);
                unaligned = new SolidBrush(Color.PaleGreen);
            }
            else
            {
                aligned = new SolidBrush(Color.Red);
                unaligned = new SolidBrush(Color.Pink);
            }

            int length = getEndPositionWithSoftClip - getPositionWithSoftClip;
            double imageLength = length * XScale;
            if (imageLength > 1)
            {
                for (int order = 0; order < 3; order++)
                {
                    int drawFrom = (positions - start);
                    if (lBlocks[0].state == fragmentType.Unaligned)
                    { drawFrom -= lBlocks[0].length; }
                    alignStart = drawFrom;
                    foreach (fragment f in lBlocks)
                    {
                        float x = 10.0f + (float)(drawFrom * XScale);
                        float w = (float)(f.length * XScale);
                        r.X = x;
                        r.Width = w;

                        switch (f.state)
                        {
                            case fragmentType.Deletion:
                                if (order == 0)
                                {
                                    if (r.Left > 2)
                                    { g.DrawLine(Pens.Black, r.Right, r.Y + (r.Height/2), r.Left, r.Top + (r.Height/2)); }
                                }
                                break;
                            case fragmentType.Unaligned:
                                if (order == 1)
                                { g.FillRectangle(unaligned, r); }
                                break;
                            case fragmentType.Match:
                                if (order == 1)
                                { g.FillRectangle(aligned, r); }
                                break;
                            case fragmentType.Insert:
                                r.Width=1;
                                if (order == 2)
                                {
                                    g.FillRectangle(aligned, r);
                                    if (f.length > 10)
                                    { g.DrawLine(Pens.Black, (int)r.X,(int)( r.Top - 2), (int)r.X, (int)(r.Bottom - 2)); }
                                }
                                break;
                        }
                        if (f.state != fragmentType.Insert)
                        { drawFrom += f.length; }
                    }
                }
                alignEnd = r.Right;
            }
            else
            {
                alignStart = getPositionWithSoftClip - start;
                r.X = 10.0f + (float)(alignStart * XScale);
                if (imageLength < 1) { imageLength = 1; }
                r.Width = (float)imageLength;
                alignEnd = r.Right;
                g.FillRectangle(aligned, r);
            }

            if (selectedIndex.Contains(index))
            {
                r.X =  10.0f + (float)(alignStart * XScale);
                r.Width = alignEnd - r.X;
                Pen box = new Pen(Color.Blue, 2);
                g.DrawRectangle(box, r.X, r.Y + 1, r.Width, r.Height - 1);
            }
            
            drawn = true;
        }

        public void drawSoftClip(Graphics g2, float top, int featureHeight, int start, double XScale, string sCIGAR, int sPosition, bool isForward, List<int> selectedIndex)
        {
            List<fragment> sBlocks = new List<fragment>();
            int sLength = getSecondaryAlignedLengthAndMakeBlock(sCIGAR, ref sBlocks);
            float alignStart = -1;
            float alignEnd = -1;

            SolidBrush aligned;
            SolidBrush unaligned;
            if (isForward == true)
            {
                aligned = new SolidBrush(Color.Green);
                unaligned = new SolidBrush(Color.PaleGreen);
            }
            else
            {
                aligned = new SolidBrush(Color.Red);
                unaligned = new SolidBrush(Color.Pink);
            }

            RectangleF r = new RectangleF(1.0f, top, 1.0f, (float)featureHeight);
            for (int order = 0; order < 2; order++)
            {
                int drawFrom = (sPosition - start);

                alignStart = drawFrom;
                foreach (fragment f in sBlocks)
                {
                    float x = 10.0f + (float)(drawFrom * XScale);
                    float w = (float)(f.length * XScale);
                    r.X = x;
                    r.Width = w;

                    switch (f.state)
                    {
                        case fragmentType.Deletion:
                            if (order == 1)
                            { g2.FillRectangle(aligned, r); }
                            break;
                        case fragmentType.Unaligned:
                            if (order == 0)
                            { g2.FillRectangle(unaligned, r); }
                            break;
                        case fragmentType.Match:
                            if (order == 0)
                            { g2.FillRectangle(aligned, r); }
                            break;
                        case fragmentType.Insert:
                            r.Width=1;
                            if (order == 1)
                            { g2.FillRectangle(aligned, r); }
                            break;
                    }
                    if (f.state != fragmentType.Insert)
                    { drawFrom += f.length; }
                }
            }
            alignEnd = r.Right;

            if (selectedIndex.Contains(index))
            {
                r.X =  10.0f + (float)(alignStart * XScale);
                r.Width = alignEnd - r.X;
                Pen box = new Pen(Color.Blue, 2);
                g2.DrawRectangle(box, r.X, r.Y + 1, r.Width, r.Height - 1);
            }
           
        }

        private int getAlignedLengthAndMakeBlock(string CIGAR)
        {           
           blocks = new List<fragment>();
            int sum = 0;
            string number = "";
            for (int index = 0; index < CIGAR.Length; index++)
            {
                switch (CIGAR[index])
                {
                    case 'H':
                        number = "";
                        break;
                    case 'S':
                        blocks.Add(new fragment(Convert.ToInt32(number), fragmentType.Unaligned));
                        number = "";
                        break;
                    case 'I':
                        int lengthI = Convert.ToInt32(number);
                        blocks.Add(new fragment(lengthI, fragmentType.Insert));
                        if (lengthI > 25)
                        { largeIndel = true; }
                        number = "";
                        break;
                    case 'D':
                    case 'N':
                    case 'P':
                        int lengthD = Convert.ToInt32(number);
                        blocks.Add(new fragment(lengthD, fragmentType.Deletion));
                        sum += Convert.ToInt32(number);
                        if (lengthD > 25)
                        { largeIndel = true; }
                        number="";
                        break;
                    case 'M':
                    case '=':
                    case 'X':
                        blocks.Add(new fragment(Convert.ToInt32(number), fragmentType.Match));
                        sum += Convert.ToInt32(number);
                        number="";
                        break;
                    default:
                        number += CIGAR[index];
                        break;
                }
            }
            
            return sum;
        }

        private void makeBlocksSimplified()
        {           
            blocksSimplified = new List<fragment>();
            if (blocks[0].state == fragmentType.Unaligned)
            { blocksSimplified.Add(blocks[0]); }
            blocksSimplified.Add(new fragment(getEndPosition - getPosition, fragmentType.Match));
            if (blocks[blocks.Count - 1].state == fragmentType.Unaligned)
            { blocksSimplified.Add(blocks[blocks.Count - 1]); }
        }

        private int getSecondaryAlignedLengthAndMakeBlock(string CIGAR, ref List<fragment> sBlocks)
        {
            int sum = 0;
            string number = "";
            for (int index = 0; index < CIGAR.Length; index++)
            {
                switch (CIGAR[index])
                {
                    case 'H':
                        number="";
                        break;
                    case 'S':
                        sBlocks.Add(new fragment(Convert.ToInt32(number), fragmentType.Unaligned));
                        sum += Convert.ToInt32(number);
                        number="";
                        break;
                    case 'I':
                        sBlocks.Add(new fragment(Convert.ToInt32(number), fragmentType.Insert));
                        number="";
                        break;
                    case 'D':
                    case 'N':
                    case 'P':
                        sBlocks.Add(new fragment(Convert.ToInt32(number), fragmentType.Deletion));
                        sum += Convert.ToInt32(number);
                        number="";
                        break;
                    case 'M':
                    case '=':
                    case 'X':
                        sBlocks.Add(new fragment(Convert.ToInt32(number), fragmentType.Match));
                        sum += Convert.ToInt32(number);
                        number="";
                        break;
                    default:
                        number += CIGAR[index];
                        break;
                }
            }

            return sum;
        }

        public int getPosition
        { get { return positions; } }

        public int getEndPosition
        { get { return positions + alignedLength; } }

        public int getPositionWithSoftClip
        {
            get
            {
                if (blocks[0].state == fragmentType.Unaligned)
                { return positions - blocks[0].length; }
                else
                { return positions; }
            }
        }

        public int getEndPositionWithSoftClip
        {
            get
            {
                if (blocks[blocks.Count - 1].state == fragmentType.Unaligned)
                { return positions + alignedLength + blocks[blocks.Count - 1].length; }
                else
                { return positions + alignedLength; ; }
            }
        }

        public string getKey
        { get { return name + positions.ToString() + forward.ToString(); } }

        public string getSecondaryAlignmentTag
        {
            get
            {
                if (tags.ContainsKey("SA") == true)
                { return tags["SA"]; }
                else { return ""; }
            }
        }

        public int getIndex
        { get { return index; } }

        public int getreferenceIndex
        { get { return referenceIndex; } }

        public bool getForward
        { get { return forward; } }

        public bool IsDrawn
        {
            get { return drawn; }
            set { drawn = value; }
        }

        public bool IsSupplementaryAlignment
        { get { return isSupplementaryAlignment; } }

        public bool IsSecondaryAlignment
        { get { return isSecondaryAlignment; } }

        public bool IsGood
        { get { return isGood; } }

        public List<fragment> getBlocks
        { get { return blocks; } }

        public string getName
        { get { return name; } }

        public string getSequence
        { get { return sequence; } }

        public bool hasLargeIndel
        { get { return largeIndel; } }

        public bool hasFivePrimeSoftClip
        { get { return (blocks[0].state == fragmentType.Unaligned && blocks[0].length > 50); } }

        public bool hasThreePrimeSoftClip
        { get { return (blocks[blocks.Count-1].state == fragmentType.Unaligned && blocks[blocks.Count-1].length > 50); } }

        public List<string> GetVariants
        { get { return variants; } }
            
    }
}
