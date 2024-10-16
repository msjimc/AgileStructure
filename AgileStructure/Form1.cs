using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AgileStructure;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Numerics;
using System.Diagnostics.Eventing.Reader;
using System.Threading;


namespace AgileStructure
{
    enum MutationType
    {
        NoSet,
        Deletion,
        Insertion,
        Inversion,
        Duplication,
        DuplicationRCStart,
        DuplicationRCEnd,
        Translocation
    }

    struct StringIntInt
    {
        public int interval;
        public int factor;
        public string letter;
        public StringIntInt(string Letter, int Interval, int Factor)
        {
            interval = Interval;
            factor = Factor;
            letter = Letter;
        }
    }

    struct StringInt
    {
        public int number;
        public string text;
        public StringInt(string Text, int Number)
        {
            number = Number;
            text = Text;
        }
    }

    public partial class Form1 : Form
    {
        bool simplified = true;

        string alignerString = "";
        ReferenceSequence[] referenceSequences;
        string[] referenceSequenceNames;
        IntervalPoints[] IPs = null;
        List<APoint> referenceDetails = null;
        string fileName;
        Bitmap bmp = null;
        Graphics g = null;
        Bitmap bmp_Region = null;

        int selectedRefLength = 1;
        int selectStart = 1;
        int selectEnd = 1;
        int selectedSecondaryRefLength = 1;
        int selectSecondaryStart = 1;
        int selectSecondaryEnd = 1;
        int binSize = 250;

        List<string> ListOfAllSecondaryAlignments;
        Dictionary<string, int> secondaryAlignments;
        Bitmap bmp_soft = null;
        Graphics g_soft = null;

        int[,] rows = new int[0, 0];
        int[,] secondaryRows = new int[0, 0];
        int readImageHeight = 8;
        int readStackHeight = 10;
        Dictionary<int, AlignedRead> DrawnARKeys;

        Dictionary<string, AlignedRead> AR = null;

        GeneData gd = null;
        bool drawGenes = true;
        RepeatData rd = null;
        string repeatFileName = "";
        //bool drawRepeats = true;

        Info id = null;

        List<string> history = new List<string>();
        List<StringInt> historySecondary;


        public Form1()
        {
            InitializeComponent();

            makeBlankImage();
            makeBlankSecondaryBase();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string file = FileString.OpenAs("Select the bam file", "*.bam|*.bam");
            if (System.IO.File.Exists(file) == false) { return; }

            Text = "Genomic rearrangements: " + file.Substring(file.LastIndexOf("\\") + 1);
            try
            {
                fileName = file;
                getBamHeader(file);
                processBAIFile(file + ".bai");
            }
            catch (Exception ex)
            {
                if (id != null) { id.WindowState = FormWindowState.Minimized; }
                MessageBox.Show(ex.Message, "Error");
            }
            history = new List<string>();
            historySecondary = new List<StringInt>();
            primaryAlignmentsToolStripMenuItem.DropDownItems.Clear();
            secondaryAlignmentsToolStripMenuItem.DropDownItems.Clear();

            makeBlankImage();
            makeBlankSecondaryBase();

        }

        private void getBamHeader(string bamFileName)
        {
            BAMReader br = null;
            try
            {
                StringBuilder sb = new StringBuilder(100);

                br = new BAMReader(bamFileName);
                List<string> headers = br.getHeader();
                br.Dispose();
                br = null;

                alignerString = "";
                int counter = 0;
                foreach (string s in headers)
                {
                    if (s.StartsWith("@SQ") == true)
                    { counter++; }
                    else if (s.StartsWith("@PG") == true)
                    { alignerString += s + "\n"; }
                }


                referenceSequences = new ReferenceSequence[counter];
                referenceSequenceNames = new string[counter];
                counter = 0;
                long size = 0;
                referenceDetails = new List<APoint>();
                cboRef.Items.Clear();
                cboRef.Items.Add("Select");
                foreach (string s in headers)
                {
                    if (s.StartsWith("@SQ") == true)
                    {
                        referenceSequences[counter] = new ReferenceSequence(s, counter);
                        referenceSequenceNames[counter] = referenceSequences[counter].name;
                        referenceSequences[counter].CalculateAccumulatedlength(size);
                        size = referenceSequences[counter].AccumlatedLengthEnd;
                        cboRef.Items.Add(referenceSequences[counter].name);
                        APoint rd = new APoint();
                        rd.referenceIndex = cboRef.Items.Count - 2;
                        rd.BPStart = 0;
                        rd.BPEnd = (ulong)referenceSequences[counter].Lenght;
                        rd.accumulatedStart = (ulong)referenceSequences[counter].AccumlatedLengthStart;
                        referenceDetails.Add(rd);
                        counter++;
                    }
                }
                cboRef.SelectedIndex = 0;

            }
            catch (Exception ex)
            { }
        }

        private int getReferenceIndexFromName(string name)
        {
            int index = 0;

            while (index < referenceSequences.Length)
            {
                if (referenceSequences[index].name.ToLower().Equals(name.ToLower()) == true)
                { return index; }
                index++;
            }

            return -1;
        }

        private bool IsRegionInReferenceSequence(int index, int start, int end)
        {
            if (start > -1 && end <= referenceSequences[index].Lenght)
            { return true; }
            else
            { return false; }
        }

        private void processBAIFile(string fileName)
        {
            int intervalcount = getIntervalCount(fileName);

            readBAIFile(fileName, intervalcount);
            IntervalPointsSorter IPsorter = new IntervalPointsSorter();
            Array.Sort(IPs, IPsorter);

        }

        private int getIntervalCount(string file)
        {
            int counter = 0;

            using (System.IO.BinaryReader br = new System.IO.BinaryReader(System.IO.File.Open(file, System.IO.FileMode.Open)))
            {
                byte[] magic = new byte[4];
                magic = br.ReadBytes(4);
                Int32 n_ref = BitConverter.ToInt32(br.ReadBytes(4), 0);

                for (int ref_index = 0; ref_index < n_ref; ref_index++)
                {
                    Int32 n_bin = BitConverter.ToInt32(br.ReadBytes(4), 0);

                    for (int bin_index = 0; bin_index < n_bin; bin_index++)
                    {
                        br.ReadBytes(4);
                        Int32 n_chunk = BitConverter.ToInt32(br.ReadBytes(4), 0);
                        br.ReadBytes(n_chunk * 16);
                    }

                    int n_intv = BitConverter.ToInt32(br.ReadBytes(4), 0);
                    br.ReadBytes(8 * n_intv);
                    counter += n_intv;
                }

                UInt64 unplaced = BitConverter.ToUInt64(br.ReadBytes(8), 0);
            }

            return counter;
        }

        private void readBAIFile(string file, int intervalcount)
        {

            IPs = new IntervalPoints[intervalcount];
            int counter = 0;
            using (System.IO.BinaryReader br = new System.IO.BinaryReader(System.IO.File.Open(file, System.IO.FileMode.Open)))
            {
                byte[] magic = new byte[4];
                magic = br.ReadBytes(4);
                UInt32 n_ref = BitConverter.ToUInt32(br.ReadBytes(4), 0);

                long bigtotal = 0;

                for (int ref_index = 0; ref_index < n_ref; ref_index++)
                {
                    UInt32 n_bin = BitConverter.ToUInt32(br.ReadBytes(4), 0);
                    UInt32 bin = 0;
                    for (UInt32 bin_index = 0; bin_index < n_bin; bin_index++)
                    {
                        bin = BitConverter.ToUInt32(br.ReadBytes(4), 0);
                        Int32 n_chunk = BitConverter.ToInt32(br.ReadBytes(4), 0);
                        br.ReadBytes((int)n_chunk * 16);
                    }
                    int n_intv = BitConverter.ToInt32(br.ReadBytes(4), 0);

                    UInt64 ioset = 0;
                    int space = 1 << 14;
                    int total = 0;
                    for (int index = 0; index < n_intv; index++)
                    {
                        ioset = BitConverter.ToUInt64(br.ReadBytes(8), 0);

                        IntervalPoints IP = new IntervalPoints(ref_index, total);
                        AddOffestToInterval(ioset, IP);

                        IPs[counter++] = IP;
                        total += space;

                    }
                    bigtotal += total;
                }
                UInt64 unplaced = BitConverter.ToUInt64(br.ReadBytes(8), 0);
            }
        }

        private void AddOffestToInterval(UInt64 value, IntervalPoints IP)
        {
            UInt64 startPoint = value >> 16;
            UInt64 bamStartPoint = value ^ (startPoint << 16);

            IP.setVirtualOffsets(bamStartPoint, startPoint);
        }

        private void cboRef_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRef.SelectedIndex > 0)
            {
                int index = cboRef.SelectedIndex;
                APoint rd = referenceDetails[index - 1];
                selectedRefLength = (int)rd.BPEnd;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReadFile();
        }

        private void ReadFile()
        {
            string title = Text;
            try
            {
                this.Enabled = false;
                bool justChimeric = onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem.Checked;
                bool justLargeIndels = onlyShowReadsWithALargeIndelToolStripMenuItem.Checked;

                if (selectEnd > 0 && selectStart > 0 && cboRef.SelectedIndex > 0)
                {
                    AddHistory();
                    rd = null;
                    secondaryAlignments = new Dictionary<string, int>();
                    DrawnARKeys = new Dictionary<int, AlignedRead>();
                    ListOfAllSecondaryAlignments = new List<string>();

                    makeBaseImage();
                    selectedIndex = new List<int>();

                    AR = new Dictionary<string, AlignedRead>(2000);
                    makeBaseImage();

                    int index = cboRef.SelectedIndex - 1;
                    int tempInt = selectStart - 16384;
                    if (tempInt < 0) tempInt = 0;
                    ulong regionStart = (ulong)tempInt;
                    tempInt = selectEnd + 16384;
                    if (tempInt < selectEnd) { tempInt = selectEnd; }
                    ulong regionEnd = (ulong)tempInt;

                    APoint aP = new APoint();
                    aP.BPStart = regionStart;
                    aP.referenceIndex = index;
                    IntervalPointsBinarySearcher IPbinary = new IntervalPointsBinarySearcher();

                    int indexRef = Array.BinarySearch(IPs, aP, IPbinary);

                    if (indexRef > IPs.Length || indexRef < 0) { return; }
                    IntervalPoints IP = IPs[indexRef];

                    int counter = 1;
                    int endRegion;
                    int lastReadPosition = 0;
                    int currentChromosome = index;
                    int lastRefIndex = index;
                    bool skipThisBlock = false;
                    int count = 0;

                    while (indexRef < IPs.Length && lastReadPosition <= (int)regionEnd && lastRefIndex <= IPs[indexRef].NameIndex)
                    {

                        if (indexRef > IPs.GetUpperBound(0)) { break; }
                        IP = IPs[indexRef];
                        if (indexRef + 1 < IPs.Length)
                        {
                            endRegion = IPs[indexRef + 1].getBPStart;
                            if (IPs[indexRef].get_StreamPoint == IPs[indexRef + 1].get_StreamPoint || lastReadPosition > IP.getBPStart)
                            {
                                skipThisBlock = true;
                            }
                        }
                        else { endRegion = selectEnd; }

                        if (skipThisBlock == false)
                        {
                            BAMReader br;
                            try
                            { br = new BAMReader(fileName, (long)IP.get_StreamPoint); }
                            catch (Exception ex)
                            { return; }
                            string r = br.NextAlignedRead(true, referenceSequenceNames);
                            AlignedRead art = new AlignedRead(r, AR.Count + 1);
                            if (art.getreferenceIndex == index)
                            { lastReadPosition = art.getPosition; }
                            else { lastReadPosition = 0; }

                            while (r.Length > 0 && (lastReadPosition < selectEnd && currentChromosome <= index))
                            {
                                string name = getKey(r);
                                if (AR.ContainsKey(name) == false)
                                {

                                    AlignedRead ar = new AlignedRead(r, AR.Count + 1);
                                    if (ar.IsGood == true)
                                    {
                                        currentChromosome = ar.getreferenceIndex;
                                        if (ar.getreferenceIndex == index)
                                        { lastReadPosition = ar.getPosition; }
                                        else { lastReadPosition = 0; }

                                        if (lastReadPosition <= selectEnd && lastReadPosition >= selectStart - 1 && currentChromosome == index)
                                        {
                                            if (ar.IsGood == true && ar.IsSupplementaryAlignment == false && ar.IsSecondaryAlignment == false)
                                            {
                                                if ((justChimeric == true && ar.getSecondaryAlignmentTag != "") || (justLargeIndels == true && ar.hasLargeIndel == true))
                                                {
                                                    AR.Add(name, ar);
                                                    ar = null;
                                                    count++;
                                                }
                                                else if (justChimeric == true && ar.getSecondaryAlignmentTag != "")
                                                {
                                                    AR.Add(name, ar);
                                                    ar = null;
                                                    count++;
                                                }
                                                else if (justLargeIndels == true && ar.hasLargeIndel == true)
                                                {
                                                    AR.Add(name, ar);
                                                    ar = null;
                                                    count++;
                                                }
                                                else if (justLargeIndels == false && justChimeric == false)
                                                {
                                                    count++;
                                                    AR.Add(name, ar);
                                                    ar = null;
                                                    count++;
                                                }
                                                else { count++; }

                                                if (count > 99)
                                                {
                                                    Text = "Loaded: " + AR.Count().ToString() + " reads. Current read start point: " + lastReadPosition.ToString("N0");
                                                    drawPrimaryAlignments(false);
                                                    Application.DoEvents();
                                                    count = 0;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (ar.getPosition == -1)
                                        { break; }
                                    }
                                }
                                r = br.NextAlignedRead(true, referenceSequenceNames);
                            }
                            br.Dispose();
                        }
                        if (lastReadPosition > selectEnd || index < currentChromosome || currentChromosome == -1)
                        { break; }
                        skipThisBlock = false;
                        indexRef++;
                        if (AR.Count > 0)
                        { Text = "Loaded: " + AR.Count().ToString() + " reads. Current read start point: " + lastReadPosition.ToString("N0"); }
                        Application.DoEvents();
                        counter++;
                    }
                    drawPrimaryAlignments(false);
                    setSecondaryAlignmentBins();
                    AddSecondaryAlignmentsBlocks();
                    makeBlankSecondaryBase();

                    DrawGenes(g, bmp.Height, cboRef.Text, selectStart, selectEnd);

                    p1.Image = bmp;
                }
            }
            finally
            {
                Text = title;
                this.Enabled = true;
            }
        }

        private int GetNextRead(StringBuilder sb, int startIndex, ref string read)
        {
            int index = 0;

            if (startIndex >= sb.Length)
            {
                read = "";
                return -1;
            }

            if (sb[startIndex] == '\n')
            { startIndex++; }

            index = startIndex;

            while (index < sb.Length)
            {
                if (sb[index] == '\n')
                {
                    read = sb.ToString(startIndex, index - startIndex);
                    return index;
                }
                index++;
            }
            return -1;
        }

        public int IndexOf(StringBuilder sb, string value, int startIndex, bool ignoreCase)
        {
            int len = value.Length;
            int max = (sb.Length - len) + 1;

            for (int i1 = startIndex; i1 < max; ++i1)
                if (sb[i1] == '\n')
                { return i1; }
            return -1;
        }

        private string getKey(string read)
        {
            int index = read.IndexOf("\t");//end of name
            string name = read.Substring(0, index);
            int indexA = read.IndexOf("\t", index + 1);//end of flag
            name += "|" + read.Substring(index + 1, indexA - (1 + index));

            index = read.IndexOf("\t", indexA + 1);//end of ref index            
            indexA = read.IndexOf("\t", index + 1);//end of position**
            name += "|" + read.Substring(index + 1, indexA - (1 + index));

            index = read.IndexOf("\t", indexA + 1);//end ofmappQ
            index = read.IndexOf("\t", index + 1);//end of CIGAR
            index = read.IndexOf("\t", index + 1);//end of pair ref index
            index = read.IndexOf("\t", index + 1);//end of pair ref position
            index = read.IndexOf("\t", index + 1);//insert size
            indexA = read.IndexOf("\t", index + 1);//end of end sequence**
            name += "|" + read.Substring(index + 1, indexA - index).Length.ToString();
            return name;
        }

        private void AddHistory()
        {
            if (history == null)
            { history = new List<string>(); }

            historySecondary = new List<StringInt>();
            secondaryAlignmentsToolStripMenuItem.DropDownItems.Clear();

            string line = cboRef.Text + " " + txtStart.Text + " bp " + txtEnd.Text + " bp";
            if (history.Contains(line))
            { history.Remove(line); }
            history.Add(line);
            if (history.Count > 10)
            { history.RemoveAt(0); }

            ToolStripMenuItem[] items = new ToolStripMenuItem[history.Count];
            for (int index = 0; index < history.Count; index++)
            {
                items[index] = new ToolStripMenuItem();
                items[index].Name = index.ToString();
                items[index].Text = history[index];
                items[index].Click += new EventHandler(HistoryLinesToolStripMenuItem);
            }
            primaryAlignmentsToolStripMenuItem.DropDownItems.Clear();
            primaryAlignmentsToolStripMenuItem.DropDownItems.AddRange(items);

        }

        private void HistoryLinesToolStripMenuItem(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            int index = Convert.ToInt32(clickedItem.Name);
            string[] items = history[index].Split(' ');
            int refindex = getReferenceIndexFromName(items[0]);
            txtStart.Text = items[1];
            txtEnd.Text = items[3];
            cboRef.SelectedIndex = refindex + 1;
            btnGetReads.PerformClick();
        }

        private bool ignoreHistory = false;
        private void AddSecondaryHistory()
        {
            if (ignoreHistory == true) { return; }
            if (historySecondary == null)
            { historySecondary = new List<StringInt>(); }


            string line = cboSecondaries.Text.Substring(0, cboSecondaries.Text.IndexOf(" ")) + " " + txtsStart.Text + " bp " + txtsEnd.Text + " bp";
            StringInt value = new StringInt(line, cboSecondaries.SelectedIndex);
            if (historySecondary.Contains(value) == true)
            { historySecondary.Remove(value); }
            historySecondary.Add(value);
            if (historySecondary.Count > 10)
            { historySecondary.RemoveAt(0); }

            ToolStripMenuItem[] items = new ToolStripMenuItem[historySecondary.Count];
            for (int index = 0; index < historySecondary.Count; index++)
            {
                items[index] = new ToolStripMenuItem();
                items[index].Name = index.ToString();
                items[index].Text = historySecondary[index].text;
                items[index].Click += new EventHandler(historySecondaryLinesToolStripMenuItem);
            }
            secondaryAlignmentsToolStripMenuItem.DropDownItems.Clear();
            secondaryAlignmentsToolStripMenuItem.DropDownItems.AddRange(items);
        }

        private void historySecondaryLinesToolStripMenuItem(object sender, EventArgs e)
        {
            try
            {
                ignoreHistory = true;
                ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
                int index = Convert.ToInt32(clickedItem.Name);
                string[] items = historySecondary[index].text.Split(' ');
                cboSecondaries.SelectedIndex = historySecondary[index].number;
                txtsStart.Text = items[1];
                txtsEnd.Text = items[3];
                ignoreHistory = false;
                btnSecondaryView.PerformClick();
            }
            finally
            { ignoreHistory = false; }

        }

        private void drawPrimaryAlignments(bool wholeSet)
        {
            if (wholeSet == true)
            {
                makeBaseImage();
                DrawGenes(g, bmp.Height, cboRef.Text, selectStart, selectEnd);
                //DrawRepeats(g, bmp.Height, cboRef.Text, selectStart, selectEnd);
                resetReadsDrawnStatus();
            }
            drawAlignments(AR);
        }

        private void makeBaseImage()
        {
            if (selectEnd == 0 || selectStart == 0 || selectEnd - selectStart < 10 || WindowState == FormWindowState.Minimized)
            {
                makeBlankImage();
                makeBlankSecondaryBase();
                return;
            }

            bmp = new Bitmap(p1.Width, p1.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            g.DrawLine(Pens.Black, 10, 2, bmp.Width - 10, 2);

            double xScale = (p1.Width - 20) / (double)(selectEnd - selectStart);
            Font f = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular);
            StringIntInt interval = getAnnotationInterval(p1, g, f, selectStart, selectEnd);
            int startHere = (selectStart / interval.interval) * interval.interval;
            for (int counter = startHere; counter < selectEnd; counter += interval.interval)
            {
                int point = (int)(xScale * (counter - selectStart)) + 10;
                g.DrawLine(Pens.Black, point, 2, point, 5);
                g.DrawString((counter / interval.factor).ToString("N0") + interval.letter, f, Brushes.Black, point, 5);
            }

            p1.Image = bmp;

            if ((drawGenes == true && gd != null) || (rd != null))
            { setRowMatrix(true); }
            else
            { setRowMatrix(false); }
        }

        private void makeBlankImage()
        {
            if (WindowState == FormWindowState.Minimized)
            { return; }
            bmp = new Bitmap(p1.Width, p1.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            g.DrawLine(Pens.Black, 10, 10, bmp.Width - 10, 10);
            p1.Image = bmp;
            if ((drawGenes == true && gd != null) || (rd != null))
            { setRowMatrix(true); }
            else
            { setRowMatrix(false); }
        }

        private void makeSecondaryBase()
        {
            if (selectSecondaryStart == 00 || selectedSecondaryRefLength < 10 || selectEnd - selectStart < 10 || selectSecondaryStart == 0 || WindowState == FormWindowState.Minimized)
            { return; }
            bmp_soft = new Bitmap(p2.Width, p2.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            g_soft = Graphics.FromImage(bmp_soft);
            g_soft.Clear(Color.White);
            g_soft.DrawLine(Pens.Black, 10, 2, bmp.Width - 10, 2);

            double xScale = (p2.Width - 20) / (double)(selectSecondaryEnd - selectSecondaryStart);
            Font f = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular);
            StringIntInt interval = getAnnotationInterval(p2, g, f, selectSecondaryStart, selectSecondaryEnd);
            int startHere = (selectSecondaryStart / interval.interval) * interval.interval;
            for (int counter = startHere; counter < selectSecondaryEnd; counter += interval.interval)
            {
                int point = (int)(xScale * (counter - selectSecondaryStart)) + 10;
                g_soft.DrawLine(Pens.Black, point, 2, point, 5);
                g_soft.DrawString((counter / interval.factor).ToString("N0") + interval.letter, f, Brushes.Black, point, 5);
            }


            p2.Image = bmp_soft;

            if ((drawGenes == true && gd != null) || (rd != null))
            { setSecondaryRowMatrix(true); }
            else
            { setSecondaryRowMatrix(false); }
        }

        private void makeBlankSecondaryBase()
        {
            if (WindowState == FormWindowState.Minimized)
            { return; }

            bmp_soft = new Bitmap(p2.Width, p2.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            g_soft = Graphics.FromImage(bmp_soft);
            g_soft.Clear(Color.White);
            g_soft.DrawLine(Pens.Black, 10, 10, bmp.Width - 10, 10);
            p2.Image = bmp_soft;
            if ((drawGenes == true && gd != null) || (rd != null))
            { setSecondaryRowMatrix(true); }
            else
            { setSecondaryRowMatrix(false); }
        }

        private void drawAlignments(Dictionary<string, AlignedRead> reads)
        {
            if (WindowState == FormWindowState.Minimized || AR == null)
            { return; }

            int starpoint = selectStart;
            int endPoint = selectEnd;
            double xScale = (p1.Width - 20) / (double)(endPoint - starpoint);
            int readCounter = 0;
            foreach (AlignedRead ar in AR.Values)
            {
                if (ar.IsDrawn == false)
                {
                    if (ar.getPosition < endPoint && ar.getEndPosition > starpoint)
                    {
                        int Y = getRowToUseAndSetRows(ar, xScale, starpoint);
                        if (Y > -1)
                        {
                            int row = (Y * readStackHeight) + 20;
                            ar.drawMe(g, row, readImageHeight, starpoint, xScale, selectedIndex, simplified);
                            if (DrawnARKeys.ContainsKey(ar.getIndex) == false)
                            { DrawnARKeys.Add(ar.getIndex, ar); }
                            readCounter++;
                            collectSecondaryAlignments(ar);
                        }
                    }
                }
            }

            g.FillRectangle(Brushes.White, 0, 0, 10, bmp.Height);
            g.FillRectangle(Brushes.White, bmp.Width - 10, 0, 10, bmp.Height);
            p1.Image = bmp;
        }

        private void DrawGenes(Graphics g, int bmpHeigth, string chromosomeName, int startPoint, int endPoint)
        {
            if (endPoint - startPoint > 100)
            {
                try
                {
                    if (drawGenes == true && gd != null)
                    {
                        chromosomeName = chromosomeName.ToLower();
                        double xScale = (p1.Width - 20) / (double)(endPoint - startPoint);

                        ChromosomalPoint left = new ChromosomalPoint(chromosomeName, startPoint - 50000);
                        ChromosomalPoint right = new ChromosomalPoint(chromosomeName, endPoint + 50000);
                        Point geneRange = gd.getIndexsRangeOffGenesInARegion(left, right);

                        g.FillRectangle(Brushes.White, 0, bmpHeigth - 22, bmp.Width, 22);

                        for (int index = geneRange.X; index <= geneRange.Y; index++)
                        {
                            int height = bmpHeigth - 14;
                            if ((index & 1) == 1) { height -= 8; }
                            if (gd.Genes[index].getChromosome.ToLower() == chromosomeName)
                            { gd.Genes[index].DrawGene(g, 10, height, 5, xScale, startPoint); }
                        }
                        g.FillRectangle(Brushes.White, 0, 0, 10, bmpHeigth);
                        g.FillRectangle(Brushes.White, bmp.Width - 10, 0, 10, bmpHeigth);
                    }
                }
                catch (Exception ex) { }
            }
        }

        private void DrawRepeats(Graphics g, int bmpHeigth, string chromosomeName, int startPoint, int endPoint)
        {
            try
            {
                if (rd != null)
                {
                    double xScale = (p1.Width - 20) / (double)(endPoint - startPoint);
                    int height = bmpHeigth - 6;

                    g.FillRectangle(Brushes.White, 0, bmpHeigth - 8, bmp.Width, 10);

                    for (int index = 0; index < rd.Repeats.Length; index++)
                    {
                        if (rd.Repeats[index].GetLocation.GetRegionEnd > startPoint || rd.Repeats[index].GetLocation.GetRegionStart < endPoint)
                        {
                            if (rd.Repeats[index].getChromosome.ToLower() == chromosomeName)
                            { rd.Repeats[index].DrawRepeat(g, 10, height, 5, xScale, startPoint); }
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void drawSecondaryAlignments(Dictionary<string, AlignedRead> reads)
        {
            if (WindowState == FormWindowState.Minimized || cboSecondaries.Items.Count == 0)
            { return; }

            int starpoint = (int)selectSecondaryStart;
            int endPoint = (int)selectSecondaryEnd;
            double xScale = (p2.Width - 20) / (double)(endPoint - starpoint);
            int readCounter = 0;
            string bin = cboSecondaries.Text;
            string chromosome = cboSecondaries.Text.Substring(0, bin.IndexOf(" "));

            foreach (AlignedRead ar in AR.Values)
            {
                string[] secondaryAlignmenttags = ar.getSecondaryAlignmentTag.Replace("Z:", "").Split(';');
                foreach (string s in secondaryAlignmenttags)
                {
                    if (s.Contains(chromosome + ",") == true)
                    {
                        string[] items = s.Split(',');
                        int sPlace = Convert.ToInt32(items[1]);
                        bool forwardSecondardy = items[2] == "+";
                        sPlace = getStartPointWithUnAlignedSequence(sPlace, items[3]);
                        int length = getUnAlignedLength(items[3]);
                        if (sPlace < endPoint && sPlace + length > starpoint)
                        {
                            int Y = getSecondaryRowToUseAndSetSecondaryRows(sPlace, sPlace + length, xScale, starpoint, ar.getIndex);
                            if (Y > -1)
                            {
                                int row = (Y * readStackHeight) + 20;
                                try
                                {
                                    ar.drawSoftClip(g_soft, row, readImageHeight, starpoint, xScale, items[3], sPlace, forwardSecondardy, selectedIndex);
                                }
                                catch { }
                            }
                        }
                    }
                }
                readCounter++;
            }

            g_soft.FillRectangle(Brushes.White, 0, 0, 10, bmp_soft.Height);
            g_soft.FillRectangle(Brushes.White, bmp_soft.Width - 10, 0, 10, bmp_soft.Height);
        }


        private int getFivePrimeSoftClipLength(string CIGAR)
        {
            int answer = 0;
            char[] tags = { 'H', 'S', 'I', 'D', 'N', 'P', 'M', '=', 'X' };
            int index = CIGAR.IndexOfAny(tags);
            if (index > -1)
            {
                if (CIGAR[index] == 'S')
                {
                    string length = CIGAR.Substring(0, index);
                    answer = Convert.ToInt32(length);
                }
            }
            return answer;
        }

        private int getThreePrimeSoftClipLength(string CIGAR)
        {
            int answer = 0;
            char[] tags = { 'H', 'S', 'I', 'D', 'N', 'P', 'M', '=', 'X' };
            int index = CIGAR.Substring(0, CIGAR.Length - 1).LastIndexOfAny(tags);
            if (index > -1)
            {
                if (CIGAR[CIGAR.Length - 1] == 'S')
                {
                    string length = CIGAR.Substring(index + 1);
                    answer = Convert.ToInt32(length.Substring(0, length.Length - 1));
                }
            }
            return answer;
        }

        private int getAlignedLength(string CIGAR)
        {
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
                        number = "";
                        break;
                    case 'I':
                        number = "";
                        break;
                    case 'D':
                    case 'N':
                    case 'P':
                        sum += Convert.ToInt32(number);
                        number = "";
                        break;
                    case 'M':
                    case '=':
                    case 'X':
                        sum += Convert.ToInt32(number);
                        number = "";
                        break;
                    default:
                        number += CIGAR[index];
                        break;
                }
            }

            return sum;
        }

        private int getUnAlignedLength(string CIGAR)
        {
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
                        sum += Convert.ToInt32(number);
                        number = "";
                        break;
                    case 'I':
                        number = "";
                        break;
                    case 'D':
                    case 'N':
                    case 'P':
                        sum += Convert.ToInt32(number);
                        number = "";
                        break;
                    case 'M':
                    case '=':
                    case 'X':
                        sum += Convert.ToInt32(number);
                        number = "";
                        break;
                    default:
                        number += CIGAR[index];
                        break;
                }
            }

            return sum;
        }

        private int getStartPointWithUnAlignedSequence(int alignedStart, string CIGAR)
        {
            int sum = alignedStart;
            string number = "";
            char[] tags = { 'H', 'S', 'I', 'D', 'N', 'P', 'M', '=', 'X' };
            int stopHere = CIGAR.IndexOfAny(tags) + 1;
            for (int index = 0; index < stopHere; index++)
            {
                switch (CIGAR[index])
                {
                    case 'H':
                        number = "";
                        break;
                    case 'S':
                        sum -= Convert.ToInt32(number);
                        number = "";
                        break;
                    case 'I':
                        number = "";
                        break;
                    case 'D':
                    case 'N':
                    case 'P':
                        sum += Convert.ToInt32(number);
                        number = "";
                        break;
                    case 'M':
                    case '=':
                    case 'X':
                        sum += Convert.ToInt32(number);
                        number = "";
                        break;
                    default:
                        number += CIGAR[index];
                        break;
                }
            }
            return sum;
        }

        private void setRowMatrix(bool DrawGenes)
        {
            int gap = 20;
            if (DrawGenes == true) { gap += 20; }
            int numberOfRows = (bmp.Height - gap) / readImageHeight;
            rows = new int[bmp.Width, numberOfRows];
            gbpP1.Text = "";
        }

        private void setSecondaryRowMatrix(bool DrawGenes)
        {
            int gap = 20;
            if (DrawGenes == true) { gap += 20; }
            int numberOfRows = (bmp_soft.Height - gap) / readImageHeight;
            secondaryRows = new int[bmp_soft.Width, numberOfRows];
            gbpP2.Text = "";
        }

        private int getRowToUseAndSetRows(AlignedRead ar, double xScale, int regionStart)
        {
            int startAlignment = (int)(10.0f + (float)(xScale * (ar.getPositionWithSoftClip - regionStart))) - 1;
            int endAlignment = (int)(10.0f + (float)(xScale * (ar.getEndPositionWithSoftClip - regionStart))) + 2;

            int test = 0;
            bool thisRow = false;
            while (test < rows.GetUpperBound(1))
            {
                int place = startAlignment;
                if (place < 0) { place = 0; }

                thisRow = true;
                while (place < endAlignment && place < rows.GetUpperBound(0))
                {
                    if (rows[place, test] != 0)
                    {
                        thisRow = false;
                        break;
                    }
                    place++;
                }
                if (thisRow == true)
                {
                    place = startAlignment;
                    if (place < 0) { place = 0; }
                    while (place < endAlignment && place < rows.GetUpperBound(0))
                    {
                        rows[place, test] = ar.getIndex;
                        place++;
                    }
                    return test;
                }
                else
                { test++; }
            }
            return -1;
        }

        private int getSecondaryRowToUseAndSetSecondaryRows(int sPlace, int sEndPlace, double xScale, int regionStart, int index)
        {
            int startAlignment = (int)(10.0f + (float)(xScale * (sPlace - regionStart))) - 1;
            int endAlignment = (int)(10.0f + (float)(xScale * (sEndPlace - regionStart))) + 2;

            int test = 0;
            bool thisRow = false;
            while (test < secondaryRows.GetUpperBound(1))
            {
                int place = startAlignment;
                if (place < 0) { place = 0; }

                thisRow = true;
                while (place < endAlignment && place < secondaryRows.GetUpperBound(0))
                {
                    if (secondaryRows[place, test] != 0)
                    {
                        thisRow = false;
                        break;
                    }
                    place++;
                }
                if (thisRow == true)
                {
                    place = startAlignment;
                    if (place < 0) { place = 0; }
                    while (place < endAlignment && place < secondaryRows.GetUpperBound(0))
                    {
                        secondaryRows[place, test] = index;
                        place++;
                    }
                    return test;
                }
                else
                { test++; }
            }
            return -1;
        }

        private void collectSecondaryAlignments(AlignedRead ar)
        {
            if (ar.getSecondaryAlignmentTag.Length > 0)
            {
                string hit = ar.getSecondaryAlignmentTag.Replace("Z:", "");
                ListOfAllSecondaryAlignments.Add(hit);
            }
        }

        private void setSecondaryAlignmentBins()
        {
            if (ListOfAllSecondaryAlignments.Count > 0)
            {
                Dictionary<string, List<int>> chrPlaces = new Dictionary<string, List<int>>();
                foreach (string hit in ListOfAllSecondaryAlignments)
                {
                    string[] hits = hit.Split(';');
                    foreach (string sa in hits)
                    {
                        if (string.IsNullOrEmpty(sa) == false)
                        {
                            string[] items = sa.Split(',');

                            bool addBoth = false;
                            int alignedLenght = getAlignedLength(items[3]);
                            if (alignedLenght > binSize)
                            { addBoth = true; }

                            if (chrPlaces.ContainsKey(items[0]) == false)
                            {
                                List<int> places = new List<int>();
                                places.Add(Convert.ToInt32(items[1]));
                                int readEnd = Convert.ToInt32(items[1]) + getAlignedLength(items[3]);
                                if (addBoth == true)
                                { places.Add(readEnd); }
                                chrPlaces.Add(items[0], places);
                            }
                            else
                            {
                                chrPlaces[items[0]].Add(Convert.ToInt32(items[1]));
                                int readEnd = Convert.ToInt32(items[1]) + getAlignedLength(items[3]);
                                if (addBoth == true)
                                { chrPlaces[items[0]].Add(readEnd); }
                            }
                        }
                    }
                }
                foreach (string k in chrPlaces.Keys)
                {
                    chrPlaces[k].Sort();
                    List<Point> bins = getBin(chrPlaces[k]);
                    foreach (Point p in bins)
                    {
                        if (secondaryAlignments.ContainsKey(k + " " + p.X.ToString("N0")) == false)
                        { secondaryAlignments.Add(k + " " + p.X.ToString("N0"), p.Y); }
                    }
                }
            }
        }

        private List<Point> getBin(List<int> places)
        {
            List<Point> bins = new List<Point>();
            if (places.Count < 2) { return bins; }
            int index = 0;
            int counter = 0;
            while (index < places.Count)
            {
                counter = 0;
                int currentStart = places[index];
                int indexStep = index;
                while (indexStep < places.Count)
                {
                    if (currentStart + binSize >= places[indexStep])
                    { counter++; }
                    else
                    {
                        if (counter > 1)
                        {
                            int theMiddle = (places[index] + places[indexStep - 1]) / 2;
                            bins.Add(new Point(theMiddle, counter));
                            counter = 0;
                            index = indexStep - 1;
                        }
                        indexStep = places.Count;
                    }
                    indexStep++;
                }

                if (counter > 1)
                {
                    int theMiddle = (places[index] + places[places.Count - 1]) / 2;
                    bins.Add(new Point(theMiddle, counter));
                    index += counter;
                    counter = 0;
                }
                index++;
            }
            if (counter > 1)
            {
                int theMiddle = (places[places.Count - counter] + places[places.Count - 1]) / 2;
                bins.Add(new Point(theMiddle, counter));
                counter = 0;
            }

            return bins;
        }

        private void AddSecondaryAlignmentsBlocks()
        {
            if (secondaryAlignments != null && secondaryAlignments.Count > 0)
            {
                List<string> bins = new List<string>();
                foreach (string k in secondaryAlignments.Keys)
                {
                    if (secondaryAlignments[k] > 1)
                    { bins.Add(k + " bp (" + secondaryAlignments[k].ToString() + ")"); }
                }
                bins.Sort();
                cboSecondaries.Items.Clear();
                cboSecondaries.Items.Add("Select a region");
                cboSecondaries.Items.AddRange(bins.ToArray());
                cboSecondaries.SelectedIndex = 0;
            }
            else
            {
                cboSecondaries.Items.Clear();
                cboSecondaries.Items.Add("Select a region");
                cboSecondaries.SelectedIndex = 0;
            }

        }

        List<int> selectedIndex = new List<int>();
        private void p1_MouseClick(object sender, MouseEventArgs e)
        {
            gbpP1.Text = "";
            if (cursorInP1.Y < p1.Height - 20 || (gd == null && rd == null))
            {
                int AR_Index = -1;
                int row = (cursorInP1.Y - 20) / readStackHeight;
                int place = cursorInP1.X;
                if (row > -1 && row <= rows.GetUpperBound(1))
                {
                    AR_Index = rows[place, row];
                    if (selectedIndex.Contains(AR_Index))
                    { selectedIndex.Remove(AR_Index); }
                    else
                    { selectedIndex.Add(AR_Index); }
                }

                drawPrimaryAlignments(true);
                DrawGenes(g, bmp.Height, cboRef.Text, selectStart, selectEnd);
                drawSecondaryAlignments();

                if (selectedIndex.Count > 0)
                { saveSelectedReadsToolStripMenuItem.Enabled = true; }
                else
                { saveSelectedReadsToolStripMenuItem.Enabled = false; }

            }
            else if ((drawGenes == true && gd != null) || (rd != null))
            {
                if (cursorInP1.Y < p1.Height - 8 && gd != null)
                {
                    gbpP1.Text = getGeneName(cursorInP1, selectStart, selectEnd, p1.Width, p1.Height, true);
                }
                else if (cursorInP1.Y > p1.Height - 8 && rd != null)
                {
                    gbpP1.Text = getRepeatName(cursorInP1, selectStart, selectEnd, p1.Width, p1.Height, true);
                }
            }
        }

        Point cursorInP1;
        private void p1_MouseMove(object sender, MouseEventArgs e)
        {
            if (p1RegionSelect == true)
            {
                bmp_Region = bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), bmp.PixelFormat);
                Graphics gMouse = Graphics.FromImage(bmp_Region);
                gMouse.DrawLine(Pens.Black, mouseDown1, 0, mouseDown1, bmp_Region.Height);
                gMouse.DrawLine(Pens.Black, e.X, 0, e.X, bmp_Region.Height);
                p1.Image = bmp_Region;
            }
            else
            {
                cursorInP1 = new Point(e.X, e.Y);
                p1p2 = 1;
            }

            if (showPosition == true)
            {
                double xScale = (p1.Width - 20) / (double)(selectEnd - selectStart);
                int position = (int)(e.X / xScale) + selectStart;
                toolTip1.Show(position.ToString("N0") + " bp", p1, e.X + 10, e.Y - 10);
            }
        }

        private void p1_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(p1);
        }

        bool p1RegionSelect = false;
        int mouseDown1 = 0;
        private void p1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                p1RegionSelect = true;
                mouseDown1 = e.X;
                bmp_Region = bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), bmp.PixelFormat);
                Graphics gMouse = Graphics.FromImage(bmp_Region);
                gMouse.DrawLine(Pens.Black, mouseDown1, 0, mouseDown1, bmp_Region.Height);
                p1.Image = bmp_Region;
            }
        }

        private void p1_MouseUp(object sender, MouseEventArgs e)
        {
            if (p1RegionSelect == true)
            {
                double xScale = (p1.Width - 20) / (double)(selectEnd - selectStart);
                int placeNow = (int)((e.X - 10) / xScale) + selectStart;
                int placeThen = (int)((mouseDown1 - 10) / xScale) + selectStart;
                if (Math.Abs(placeNow - placeThen) > 100)
                {
                    rd = null;

                    if (placeNow > placeThen)
                    {
                        txtStart.Text = placeThen.ToString("N0");
                        txtEnd.Text = placeNow.ToString("N0");
                    }
                    else
                    {
                        txtEnd.Text = placeThen.ToString("N0");
                        txtStart.Text = placeNow.ToString("N0");
                    }
                    btnGetReads.PerformClick();

                }
                else
                { p1.Image = bmp; }
            }
            p1RegionSelect = false;
        }

        private void cboSecondaries_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboSecondaries.SelectedIndex > 0)
            {
                string bin = cboSecondaries.Text;
                string chromosome = bin.Substring(0, bin.IndexOf(" "));
                string ssPlace = bin.Substring(bin.IndexOf(" ") + 1);
                ssPlace = ssPlace.Substring(0, ssPlace.IndexOf(" ")).Trim();
                int sPlace = Convert.ToInt32(ssPlace.Replace(",", ""));

                setSecondaryNuds(chromosome, sPlace);

                if (ignoreHistory == false)
                {
                    AddSecondaryHistory();
                    drawSecondaryAlignments();
                }
            }
            else
            {
                try
                {
                    ignoreHistory = true;
                    selectedSecondaryRefLength = 0;
                    selectedSecondaryRefLength = 0;
                    selectSecondaryStart = 0;
                    selectSecondaryEnd = 0;
                    txtsStart.Text = "0";
                    txtsEnd.Text = "0";
                    makeBlankSecondaryBase();
                }
                finally
                { ignoreHistory = false; }
            }
        }

        private void setSecondaryNuds(string refName, int sPlace)
        {
            try
            {
                ignoreHistory = true;
                foreach (ReferenceSequence rs in referenceSequences)
                {
                    if (rs.name.Equals(refName) == true)
                    {
                        long length = rs.Lenght;
                        selectedSecondaryRefLength = (int)length;
                        selectedSecondaryRefLength = (int)length;
                        if (sPlace - 50000 > -1)
                        { txtsStart.Text = (sPlace - 50000).ToString("N0"); }
                        else
                        { txtsStart.Text = "1"; }
                        if (sPlace + 50000 > length)
                        { txtsEnd.Text = length.ToString("N0"); }
                        else
                        { txtsEnd.Text = (sPlace + 50000).ToString("N0"); }
                    }
                }
            }
            finally
            { ignoreHistory = false; }
        }

        private void btnSecondaryView_Click(object sender, EventArgs e)
        {
            drawSecondaryAlignments();
        }

        private void drawSecondaryAlignments()
        {
            makeSecondaryBase();
            if (cboSecondaries.SelectedIndex == 0)
            { makeBlankSecondaryBase(); }
            else
            { drawSecondaryAlignments(AR); }

            if (((drawGenes == true && gd != null)) && cboSecondaries.Items.Count > 0 && cboSecondaries.SelectedIndex > 0)
            {
                string chromosomeName = cboSecondaries.Text.Substring(0, cboSecondaries.Text.IndexOf(" ")).Trim();
                if (drawGenes == true && gd != null)
                { DrawGenes(g_soft, bmp_soft.Height, chromosomeName, selectSecondaryStart, selectSecondaryEnd); }
            }

        }

        private void p2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                selectedIndex = new List<int>();
                saveSelectedReadsToolStripMenuItem.Enabled = false;
                drawPrimaryAlignments(true);
                DrawGenes(g, bmp.Height, cboRef.Text, selectStart, selectEnd);
                //DrawRepeats(g, bmp.Height, cboRef.Text, selectStart, selectEnd);
                drawSecondaryAlignments();
                return;
            }

            if (cursorInP2.Y < p2.Height - 20 || (gd == null && rd == null))
            {
                int AR_Index = -1;
                int row = (cursorInP2.Y - 20) / readStackHeight;
                int place = cursorInP2.X;

                if (row > -1 && row <= secondaryRows.GetUpperBound(1))
                {
                    AR_Index = secondaryRows[place, row];
                    if (selectedIndex.Contains(AR_Index))
                    { selectedIndex.Remove(AR_Index); }
                    else
                    { selectedIndex.Add(AR_Index); }
                }

                drawPrimaryAlignments(true);
                DrawGenes(g, bmp.Height, cboRef.Text, selectStart, selectEnd);
                //DrawRepeats(g, bmp.Height, cboRef.Text, selectStart, selectEnd);
                drawSecondaryAlignments();

                if (selectedIndex.Count > 0)
                { saveSelectedReadsToolStripMenuItem.Enabled = true; }
                else
                { saveSelectedReadsToolStripMenuItem.Enabled = false; }

            }
            else if ((drawGenes == true && gd != null) || (rd != null))
            {
                if (cursorInP2.Y < p2.Height - 8 && gd != null)
                {
                    gbpP2.Text = getGeneName(cursorInP2, selectSecondaryStart, selectSecondaryEnd, p2.Width, p2.Height, false);
                }
                else if (cursorInP2.Y > p2.Height - 8 && rd != null)
                {
                    gbpP2.Text = getRepeatName(cursorInP2, selectSecondaryStart, selectSecondaryEnd, p2.Width, p2.Height, false);
                }
            }
        }

        bool p2RegionSelect = false;
        int mouseDown2 = 0;
        private void p2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                p2RegionSelect = true;
                mouseDown2 = e.X;
                bmp_Region = bmp_soft.Clone(new Rectangle(0, 0, bmp_soft.Width, bmp_soft.Height), bmp_soft.PixelFormat);
                Graphics gMouse = Graphics.FromImage(bmp_Region);
                gMouse.DrawLine(Pens.Black, mouseDown2, 0, mouseDown2, bmp_Region.Height);
                p2.Image = bmp_Region;
            }
        }

        private void p2_MouseUp(object sender, MouseEventArgs e)
        {
            if (p2RegionSelect == true)
            {
                double xScale = (p1.Width - 20) / (double)(selectSecondaryEnd - selectSecondaryStart);
                int placeNow = (int)((e.X - 10) / xScale) + selectSecondaryStart;
                int placeThen = (int)((mouseDown2 - 10) / xScale) + selectSecondaryStart;
                if (Math.Abs(placeNow - placeThen) > 100)
                {
                    rd = null;

                    if (placeNow > placeThen)
                    {
                        txtsStart.Text = placeThen.ToString("N0");
                        txtsEnd.Text = placeNow.ToString("N0");
                    }
                    else
                    {
                        txtsEnd.Text = placeThen.ToString("N0");
                        txtsStart.Text = placeNow.ToString("N0");
                    }
                    btnSecondaryView.PerformClick();
                }
                else
                { p2.Image = bmp_soft; }
            }
            p2RegionSelect = false;
        }

        private string getGeneName(Point cursor, int StartPoint, int EndPoint, int width, int height, bool InP1)
        {
            double xScale = (width - 20) / (double)(EndPoint - StartPoint);
            int place = (int)((cursor.X - 10) / xScale);
            string chromosomeName;
            if (InP1 == true)
            { chromosomeName = cboRef.Text; }
            else
            { chromosomeName = cboSecondaries.Text.Substring(0, cboSecondaries.Text.IndexOf(" ")).Trim(); }
            ChromosomalPoint here = new ChromosomalPoint(chromosomeName, StartPoint + place);

            int index = Array.BinarySearch(gd.Genes, here, new GeneBinarySearcherPoint());
            if (index > -1)
            {
                Gene transcript = gd.Genes[index];
                return transcript.getName;
            }
            else
            {
                index = -index;
                for (int coor = index - 1; coor > index - 20; coor--)
                {
                    if (coor > -1 && coor < gd.Genes.Length)
                    {
                        Gene transcript = gd.Genes[coor];
                        if (transcript.GetLocation.GetRegionStart <= here.Base && transcript.GetLocation.GetRegionEnd >= here.Base)
                        {
                            return transcript.getName;
                        }
                    }
                }
            }
            return "";
        }

        private string getRepeatName(Point cursor, int StartPoint, int EndPoint, int width, int height, bool InP1)
        {
            double xScale = (width - 20) / (double)(EndPoint - StartPoint);
            int place = (int)((cursor.X - 10) / xScale);
            string chromosomeName;
            if (InP1 == true)
            { chromosomeName = cboRef.Text.Trim(); }
            else
            { chromosomeName = cboSecondaries.Text.Substring(0, cboSecondaries.Text.IndexOf(" ")).Trim(); }
            ChromosomalPoint here = new ChromosomalPoint(chromosomeName, StartPoint + place);

            int index = Array.BinarySearch(rd.Repeats, here, new RepeatBinarySearchPoint());
            if (index > -1)
            {
                Repeat repeat = rd.Repeats[index];
                return repeat.getLongName;
            }
            else
            {
                index = -index;
                for (int coor = index - 1; coor > index - 20; coor--)
                {
                    if (coor > -1 && coor < rd.Repeats.Length)
                    {
                        Repeat repeat = rd.Repeats[coor];
                        if (repeat.GetLocation.GetRegionStart <= here.Base && repeat.GetLocation.GetRegionEnd >= here.Base)
                        {
                            return repeat.getLongName;
                        }
                    }
                }
            }
            return "";
        }

        Point cursorInP2;
        private void p2_MouseMove(object sender, MouseEventArgs e)
        {
            if (p2RegionSelect == true)
            {
                bmp_Region = bmp_soft.Clone(new Rectangle(0, 0, bmp_soft.Width, bmp_soft.Height), bmp_soft.PixelFormat);
                Graphics gMouse = Graphics.FromImage(bmp_Region);
                gMouse.DrawLine(Pens.Black, mouseDown2, 0, mouseDown2, bmp_Region.Height);
                gMouse.DrawLine(Pens.Black, e.X, 0, e.X, bmp_Region.Height);
                p2.Image = bmp_Region;
            }
            else
            {
                cursorInP2 = new Point(e.X, e.Y);
                p1p2 = 2;
            }

            if (showPosition == true)
            {
                double xScale = (p2.Width - 20) / (double)(selectSecondaryEnd - selectSecondaryStart);
                int position = (int)(e.X / xScale) + selectSecondaryStart;
                toolTip1.Show(position.ToString("N0") + " bp", p2, e.X + 10, e.Y - 10);
            }
        }

        private void p2_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(p1);
        }

        private void nudsStart_ValueChanged(object sender, EventArgs e)
        {
            drawSecondaryAlignments();
        }

        private void nudsEnd_ValueChanged(object sender, EventArgs e)
        {
            drawSecondaryAlignments();
        }

        private void resetReadsDrawnStatus()
        {
            if (AR != null && AR.Count > 0)
            {
                foreach (AlignedRead ar in AR.Values)
                { ar.IsDrawn = false; }
            }
        }

        private bool resizing = false;
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (resizing == false)
            {
                resizing = true;
                timer1.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (resizing == true)
            { resizing = false; }
            else
            {
                timer1.Enabled = false;
                if (cboSecondaries.Items.Count == 0)
                {
                    makeBlankImage();
                    makeBlankSecondaryBase();
                    return;
                }

                int index = cboSecondaries.SelectedIndex;
                int startPoint = (int)selectSecondaryStart;
                int endPoint = (int)selectSecondaryEnd;
                drawPrimaryAlignments(true);
                DrawGenes(g, bmp.Height, cboRef.Text, selectStart, selectEnd);
                DrawRepeats(g, bmp.Height, cboRef.Text.Trim(), selectStart, selectEnd);
                drawSecondaryAlignments();
                string chromosomeName = cboSecondaries.Text.Substring(0, cboSecondaries.Text.IndexOf(" ")).Trim();
                if (rd != null)
                {
                    DrawRepeats(g_soft, bmp_soft.Height, chromosomeName, selectSecondaryStart, selectSecondaryEnd);
                    p2.Image = bmp_soft;
                }
                try
                {
                    cboSecondaries.SelectedIndex = index;
                    selectSecondaryStart = startPoint;
                    selectSecondaryEnd = endPoint;
                }
                catch { }
            }
        }

        bool ignoreValidation;
        private void txtNumber_Validating(object sender, CancelEventArgs e)
        {
            if (ignoreValidation == true) { return; }

            try
            {
                if (id != null) { id.WindowState = FormWindowState.Minimized; }
                TextBox txt = (TextBox)sender;
                string value = txt.Text.Trim().Replace(",", "");
                if (string.IsNullOrEmpty(value) == false)
                {
                    int aNumber = Convert.ToInt32(value);
                }
            }
            catch //(Exception ex)
            {
                e.Cancel = true;
                if (id != null) { id.WindowState = FormWindowState.Minimized; }
                MessageBox.Show("Must be a whole number", "Not a whole number", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { ignoreValidation = false; }
        }

        private void txtNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                string control = "";
                if ((TextBox)sender == txtStart || (TextBox)sender == txtEnd)
                { control = "primary"; }
                else if ((TextBox)sender == txtsStart || (TextBox)sender == txtsEnd)
                { control = "secondary"; }
                e.SuppressKeyPress = false;
                if (e.KeyCode == Keys.Left)
                { navigate(control, true); }
                else if (e.KeyCode == Keys.Right)
                { navigate(control, false); }
                else if (e.KeyCode == Keys.Up)
                { expand(control, true); }
                else if (e.KeyCode == Keys.Down)
                { expand(control, false); }


            }
            else
            {
                char c = (char)(e.KeyValue);
                if (char.IsDigit(c) == false)
                {
                    if (e.KeyValue == 188 || e.KeyValue == 46 || e.KeyValue == 8 || e.KeyValue == 127)
                    { e.SuppressKeyPress = false; }
                    else if (e.KeyValue == 37 || e.KeyValue == 39 || e.KeyValue == 9)
                    { e.SuppressKeyPress = false; }
                    else { e.SuppressKeyPress = true; }


                }
            }
        }

        private void txtStart_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string value = txtStart.Text.Trim().Replace(",", "");
                if (string.IsNullOrEmpty(value) == false)
                {
                    selectStart = Convert.ToInt32(value);
                    if (selectStart < 1)
                    {
                        selectStart = 1;
                        txtStart.Text = "1";
                    }
                    else if (selectStart > selectedRefLength)
                    {
                        selectStart = selectedRefLength;
                        txtStart.Text = selectedRefLength.ToString("N0");
                    }
                }
            }
            catch (Exception ex)
            { txtStart.Text = "1"; }
        }

        private void txtEnd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string value = txtEnd.Text.Trim().Replace(",", "");
                if (string.IsNullOrEmpty(value) == false)
                {
                    selectEnd = Convert.ToInt32(value);
                    if (selectEnd < 1)
                    {
                        selectEnd = 1;
                        txtEnd.Text = "1";
                    }
                    else if (selectEnd > selectedRefLength)
                    {
                        selectEnd = selectedRefLength;
                        txtEnd.Text = selectedRefLength.ToString("N0");
                    }
                }
            }
            catch (Exception ex)
            { txtEnd.Text = "1"; }
        }

        private void txtsEnd_TextChanged(object sender, EventArgs e)
        {
            rd = null;
            try
            {
                string value = txtsEnd.Text.Trim().Replace(",", "");
                if (string.IsNullOrEmpty(value) == false)
                {
                    selectSecondaryEnd = Convert.ToInt32(value);
                    if (selectSecondaryEnd < 1)
                    {
                        selectSecondaryEnd = 1;
                        txtsEnd.Text = "1";
                    }
                    else if (selectSecondaryEnd > selectedSecondaryRefLength && selectedSecondaryRefLength > 0)
                    {
                        selectSecondaryEnd = selectedSecondaryRefLength;
                        txtsEnd.Text = selectedSecondaryRefLength.ToString("N0");
                    }
                }
                if (ignoreHistory == false)
                {
                    drawSecondaryAlignments();
                    AddSecondaryHistory();
                }
            }
            catch (Exception ex)
            { txtsEnd.Text = "1"; }
        }

        private void txtsStart_TextChanged(object sender, EventArgs e)
        {
            rd = null;
            try
            {
                string value = txtsStart.Text.Trim().Replace(",", "");
                if (string.IsNullOrEmpty(value) == false)
                {
                    selectSecondaryStart = Convert.ToInt32(value);
                    if (selectSecondaryStart < 1)
                    {
                        selectSecondaryStart = 1;
                        txtsStart.Text = "1";
                    }
                    else if (selectSecondaryStart > selectedSecondaryRefLength && selectedSecondaryRefLength > 0)
                    {
                        selectSecondaryStart = selectedSecondaryRefLength;
                        txtsStart.Text = selectedSecondaryRefLength.ToString("N0");
                    }
                }
                if (ignoreHistory == false)
                {
                    drawSecondaryAlignments();
                    AddSecondaryHistory();
                }
            }
            catch (Exception ex)
            { txtsStart.Text = "1"; }
        }

        private void txtStart_Leave(object sender, EventArgs e)
        {
            txtStart.Text = selectStart.ToString("N0");
        }

        private void txtEnd_Leave(object sender, EventArgs e)
        {
            txtEnd.Text = selectEnd.ToString("N0");
        }

        private void txtsStart_Leave(object sender, EventArgs e)
        {
            txtsStart.Text = selectSecondaryStart.ToString("N0");
        }

        private void txtsEnd_Leave(object sender, EventArgs e)
        {
            txtsEnd.Text = selectSecondaryEnd.ToString("N0");
        }

        private void navigate(string control, bool left)
        {
            if (control == "primary")
            {
                int length = selectEnd - selectStart;
                if (selectStart == 1) { length++; }
                if (left == true)
                {
                    selectEnd = selectStart;
                    selectStart -= length;

                    if (selectStart < 1)
                    {
                        selectStart = 1;
                        selectEnd = length;
                    }

                    txtStart.Text = selectStart.ToString("N0");
                    txtEnd.Text = selectEnd.ToString("N0");
                }
                else
                {
                    selectStart = selectEnd;
                    selectEnd += length;

                    if (selectEnd > selectedRefLength)
                    {
                        selectEnd = selectedRefLength;
                        selectStart = selectEnd - length;
                    }

                    txtStart.Text = selectStart.ToString("N0");
                    txtEnd.Text = selectEnd.ToString("N0");
                }
            }
            else if (control == "secondary")
            {
                int length = selectSecondaryEnd - selectSecondaryStart;
                if (selectSecondaryStart == 1) { length++; }
                if (left == true)
                {
                    selectSecondaryEnd = selectSecondaryStart;
                    selectSecondaryStart -= length;

                    if (selectSecondaryStart < 1)
                    {
                        selectSecondaryStart = 1;
                        selectSecondaryEnd = length;
                    }

                    txtsStart.Text = selectSecondaryStart.ToString("N0");
                    txtsEnd.Text = selectSecondaryEnd.ToString("N0");
                }
                else
                {
                    selectSecondaryStart = selectSecondaryEnd;
                    selectSecondaryEnd += length;

                    if (selectSecondaryEnd > selectedRefLength)
                    {
                        selectSecondaryEnd = selectedRefLength;
                        selectSecondaryStart = selectSecondaryEnd - length;
                    }

                    txtsStart.Text = selectSecondaryStart.ToString("N0");
                    txtsEnd.Text = selectSecondaryEnd.ToString("N0");
                }
            }
        }

        private void expand(string control, bool bigger)
        {
            if (control == "primary")
            {
                int length = selectEnd - selectStart;
                if (selectStart == 1) { length++; }

                if (bigger == true)
                {

                    selectEnd += length / 2;
                    selectStart -= length / 2;

                    if (selectStart < 1)
                    { selectStart = 1; }

                    if (selectEnd > selectedRefLength)
                    { selectEnd = selectedRefLength; }

                    txtStart.Text = selectStart.ToString("N0");
                    txtEnd.Text = selectEnd.ToString("N0");
                }
                else if (length > 200)
                {
                    selectStart += length / 4;
                    selectEnd -= length / 4;

                    txtStart.Text = selectStart.ToString("N0");
                    txtEnd.Text = selectEnd.ToString("N0");
                }
            }
            else if (control == "secondary")
            {
                int length = selectSecondaryEnd + 1 - selectSecondaryStart;
                if (selectSecondaryStart == 1) { length++; }

                if (bigger == true)
                {
                    selectSecondaryEnd += length / 2;
                    selectSecondaryStart -= length / 2;

                    if (selectSecondaryStart < 1)
                    { selectSecondaryStart = 1; }

                    if (selectEnd > selectedSecondaryRefLength)
                    { selectEnd = selectedSecondaryRefLength; }

                    txtsStart.Text = selectSecondaryStart.ToString("N0");
                    txtsEnd.Text = selectSecondaryEnd.ToString("N0");
                }
                else if (length > 200)
                {
                    selectSecondaryStart += length / 4;
                    selectSecondaryEnd -= length / 4;

                    txtsStart.Text = selectSecondaryStart.ToString("N0");
                    txtsEnd.Text = selectSecondaryEnd.ToString("N0");
                }
            }
        }


        private void gTFAnnotationFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string AnnotationFile = FileString.OpenAs("Select a gene annotation file from the genome browser", "*.txt|*.txt");
            if (System.IO.File.Exists(AnnotationFile) == false) { return; }

            gd = new GeneData(AnnotationFile);
            if (gd != null && gd.Genes != null)
            { geneCoordinatesToolStripMenuItem.Enabled = true; }

            if (cboRef.SelectedIndex > 0)
            {
                drawPrimaryAlignments(true);
                DrawGenes(g, bmp.Height, cboRef.Text, selectStart, selectEnd);
                //DrawRepeats(g, bmp.Height, cboRef.Text, selectStart, selectEnd);
                drawSecondaryAlignments();

            }

        }

        private void repeatAnnotationFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string AnnotationFile = FileString.OpenAs("Select a rpeat annoation file from the genome browser", "*.txt|*.txt");
            if (System.IO.File.Exists(AnnotationFile) == false) { return; }

            repeatFileName = AnnotationFile;
            showRepeatsToolStripMenuItem.Enabled = true;

        }

        private void getRepeats()
        {
            if (System.IO.File.Exists(repeatFileName) == false) { return; }

            string chromosomeP1 = cboRef.Text;
            int startPointP1 = selectStart;
            int endPointP1 = selectEnd;
            string chromosomeP2 = "";
            int startPointP2 = -1;
            int endPointP2 = -1;
            if (cboSecondaries.SelectedIndex > 0)
            {
                chromosomeP2 = cboSecondaries.Text.Substring(0, cboSecondaries.Text.IndexOf(" ")).Trim();
                startPointP2 = selectSecondaryStart;
                endPointP2 = selectSecondaryEnd;
            }
            rd = new RepeatData(repeatFileName, chromosomeP1, startPointP1, endPointP1, chromosomeP2, startPointP2, endPointP2);
        }

        private void showRepeatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cboRef.SelectedIndex > 0)
            {
                string name = Text;
                try
                {
                    Text = "Reading file";
                    Application.DoEvents();
                    getRepeats();

                    DrawRepeats(g, bmp.Height, cboRef.Text, selectStart, selectEnd);
                    p1.Image = bmp;
                    string chromosomeName = cboSecondaries.Text.Substring(0, cboSecondaries.Text.IndexOf(" ")).Trim();
                    if (rd != null)
                    {
                        DrawRepeats(g_soft, bmp_soft.Height, chromosomeName, selectSecondaryStart, selectSecondaryEnd);
                        p2.Image = bmp_soft;
                    }
                }
                finally
                { Text = name; }
            }
        }

        private void viewReadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (id == null)
            {
                viewReadDataToolStripMenuItem.Checked = true;
                id = new Info(referenceSequenceNames, viewReadDataToolStripMenuItem);
                id.TopMost = true;
                id.Show();
                timer2.Enabled = true;
            }
            else if (id != null)
            {
                viewReadDataToolStripMenuItem.Checked = false;
                id.Close();
                id = null;
                timer2.Enabled = false;
            }
        }

        int dataARIndex = -1;
        int p1p2 = -1;
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (DrawnARKeys == null || AR == null) { return; }
            if (DrawnARKeys.Count == 0 || AR.Count == 0) { return; }

            Point thisCursor = new Point(-1, -1);
            int[,] thisRow = null;
            int height = p1.Height; ;
            if (p1p2 == 1)
            {
                thisCursor = cursorInP1;
                thisRow = rows;
            }
            else if (p1p2 == 2)
            {
                height = p2.Height;
                thisCursor = cursorInP2;
                thisRow = secondaryRows;
            }

            if (thisCursor.Y < height - 20)
            {
                int AR_Index = -1;
                int row = (thisCursor.Y - 20) / readStackHeight;
                int place = thisCursor.X;

                if (row > -1 && row <= thisRow.GetUpperBound(1) && place > -1 && place <= thisRow.GetUpperBound(0))
                {
                    AR_Index = thisRow[place, row];
                    if (AR_Index != dataARIndex)
                    {
                        if (AR_Index > -1 && DrawnARKeys.ContainsKey(AR_Index) == true)
                        {
                            AlignedRead ar = DrawnARKeys[AR_Index];
                            if (id != null)
                            {
                                id.displayData(ar.displayDataArray());
                            }
                        }
                        dataARIndex = AR_Index;
                    }
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && DrawnARKeys.ContainsKey(dataARIndex) == true)
            {
                string file = FileString.SaveAs("Select a text file to append the read data too", "text file (*.txt|*.txtx");
                if (file != "Cancel") { return; }

                System.IO.StreamWriter fw = null;

                try
                {
                    fw = new System.IO.StreamWriter(file, true);
                    fw.Write(DrawnARKeys[dataARIndex]);
                }
                catch (Exception ex)
                {
                    if (id != null) { id.WindowState = FormWindowState.Minimized; }
                    MessageBox.Show("Could save data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                { if (fw != null) { fw.Close(); } }
            }
            if (e.KeyCode == Keys.A && selectedIndex.Count > 0)
            {
                string file = FileString.SaveAs("Select a text file to append the read data too", "text file (*.txt|*.txtx");
                if (file != "Cancel") { return; }

                System.IO.StreamWriter fw = null;

                try
                {
                    fw = new System.IO.StreamWriter(file, true);
                    foreach (int index in selectedIndex)
                    { fw.Write(DrawnARKeys[index]); }
                }
                catch (Exception ex)
                {
                    if (id != null) { id.WindowState = FormWindowState.Minimized; }
                    MessageBox.Show("Could save data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                { if (fw != null) { fw.Close(); } }
            }
        }

        private void saveSelectedReadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedIndex.Count > 0)
            {
                string file = FileString.SaveAs("Select a text file to append the read data too", "text file (*.txt;*.fa)|*.txt;*.fa");
                if (file == "Cancel") { return; }

                System.IO.StreamWriter fw = null;

                try
                {

                    fw = new System.IO.StreamWriter(file);
                    if (file.LastIndexOf(".txt") == file.Length - 4)
                    {
                        foreach (AlignedRead ar in AR.Values)
                        {
                            if (ar.IsSelected == true)
                            {
                                string[] data = ar.displayDataArray();
                                fw.Write(data[0] + referenceSequenceNames[Convert.ToInt32(data[1])] + data[2]);
                            }
                        }
                    }
                    else
                    {
                        foreach (AlignedRead ar in AR.Values)
                        {
                            if (ar.IsSelected == true)
                            {
                                fw.Write(">" + ar.getName + "\n" + ar.getSequence + "\n");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (id != null) { id.WindowState = FormWindowState.Minimized; }
                    MessageBox.Show("Could save data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                { if (fw != null) { fw.Close(); } }
            }
        }

        private void clearSelectedReadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedIndex = new List<int>();
            drawPrimaryAlignments(true);
            DrawGenes(g, bmp.Height, cboRef.Text, selectStart, selectEnd);
            drawSecondaryAlignments();
        }

        private void deletionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string result = deletion();
            if (result[0] == 'e')
            {
                MessageBox.Show(result.Substring(1), "Error");
            }
            else
            {
                if (MessageBox.Show(result.Substring(1) + "\nSave with the selected read data?", "Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                { SaveToFile(result.Substring(1)); }
            }
        }

        private string deletion()
        {
            string result = "e";
            try
            {
                if (selectedIndex.Count > 0)
                {
                    BreakPointData[] bestPlaces = getBreakPoints(true, cboRef.Text);
                    int breakPoint1 = bestPlaces[0].getAveragePlace;
                    int breakPoint2 = bestPlaces[1].getAveragePlace;
                    if (bestPlaces[1] == null)
                    {
                        if (id != null) { id.WindowState = FormWindowState.Minimized; }
                        MessageBox.Show("Could not find both sides of the breakpoint", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return result;
                    }

                    string mutation = "";
                    MutationType answer = testMutationType(bestPlaces);
                    if (answer != MutationType.Deletion)
                    { mutation = setMutationPrefix(answer); }

                    if (breakPoint1 > breakPoint2)
                    { mutation += cboRef.Text + "." + breakPoint2.ToString("N0") + "_" + breakPoint1.ToString("N0") + "del"; }
                    else
                    { mutation += cboRef.Text + "." + breakPoint1.ToString("N0") + "_" + breakPoint2.ToString("N0") + "del"; }
                    if (id != null) { id.WindowState = FormWindowState.Minimized; }
                    //if (MessageBox.Show(mutation + "\nSave with the selected read data?", "Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    //{ SaveToFile(mutation); }
                    result = "o" + mutation;
                }
                else
                {
                    if (id != null) { id.WindowState = FormWindowState.Minimized; }
                    //MessageBox.Show("Please select the reads spanning the break point by clicking on them.", "No reads selected"); 
                    result = "elease select the reads spanning the break point by clicking on them.";
                }
            }
            catch (Exception ex)
            {
                if (id != null) { id.WindowState = FormWindowState.Minimized; }
                //MessageBox.Show("Could not identify the variant using the selected reads", "Error");
                result = "eCould not identify the variant using the selected reads";
            }
            return result;
        }

        private void inversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string result = inversion();
            if (result[0] == 'e')
            {
                MessageBox.Show(result.Substring(1), "Error");
            }
            else
            {
                if (MessageBox.Show(result.Substring(1) + "\nSave with the selected read data?", "Inversion", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                { SaveToFile(result.Substring(1)); }
            }
        }

        private string inversion()
        {
            string result = "e";
            try
            {
                if (selectedIndex.Count > 0)
                {
                    BreakPointData[] bestPlaces = getBreakPoints(true, cboRef.Text);
                    int breakPoint1 = bestPlaces[0].getAveragePlace;
                    int breakPoint2 = bestPlaces[1].getAveragePlace;

                    if (bestPlaces[1] == null)
                    {
                        if (id != null) { id.WindowState = FormWindowState.Minimized; }
                        MessageBox.Show("Could not find both sides of the breakpoint", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return result;
                    }

                    string mutation = "";
                    MutationType answer = testMutationType(bestPlaces);
                    if (answer != MutationType.Inversion)
                    { mutation = setMutationPrefix(answer); }

                    if (breakPoint1 > breakPoint2)
                    { mutation += cboRef.Text + "." + breakPoint2.ToString("N0") + "_" + breakPoint1.ToString("N0") + "inv"; }
                    else
                    { mutation += cboRef.Text + "." + breakPoint1.ToString("N0") + "_" + breakPoint2.ToString("N0") + "inv"; }

                    if (id != null) { id.WindowState = FormWindowState.Minimized; }
                    //if (MessageBox.Show(mutation + "\nSave with the selected read data?", "Inversion", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    //{ SaveToFile(mutation); }
                    result = "o" + mutation;
                }
                else
                {
                    if (id != null) { id.WindowState = FormWindowState.Minimized; }
                    //MessageBox.Show("Please select the reads spanning the break point by clicking on them.", "No reads selected");
                    result = "ePlease select the reads spanning the break point by clicking on them.";
                }
            }
            catch (Exception ex)
            {
                if (id != null) { id.WindowState = FormWindowState.Minimized; }
                MessageBox.Show("Could not identify the variant using the selected reads", "Error");
            }
            return result;
        }

        private void duplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string result = duplication();
            if (result[0] == 'e')
            {
                MessageBox.Show(result.Substring(1), "Error");
            }
            else
            {
                if (MessageBox.Show(result.Substring(1) + "\nSave with the selected read data?", "Duplication", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                { SaveToFile(result.Substring(1)); }
            }
        }

        private string duplication()
        {
            string result = "e";
            try
            {
                if (selectedIndex.Count > 0)
                {
                    BreakPointData[] bestPlaces = getBreakPoints(true, cboRef.Text);
                    int breakPoint1 = bestPlaces[0].getAveragePlace;
                    int breakPoint2 = bestPlaces[1].getAveragePlace;

                    if (bestPlaces[1] == null)
                    {
                        if (id != null) { id.WindowState = FormWindowState.Minimized; }
                        MessageBox.Show("Could not find both sides of the breakpoint", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return "e";
                    }

                    string mutation = "";
                    MutationType answer = testMutationType(bestPlaces);
                    if ((answer == MutationType.Duplication))
                    {
                        if (breakPoint1 > breakPoint2)
                        { mutation += cboRef.Text + "." + breakPoint2.ToString("N0") + "_" + breakPoint1.ToString("N0") + "dup"; }
                        else
                        { mutation += cboRef.Text + "." + breakPoint1.ToString("N0") + "_" + breakPoint2.ToString("N0") + "dup"; }
                    }
                    else if (answer == MutationType.DuplicationRCStart)
                    {
                        if (breakPoint1 > breakPoint2)
                        { mutation += cboRef.Text + "." + breakPoint2.ToString("N0") + "_" + breakPoint1.ToString("N0") + "dup\nThe duplicated sequence is inverted and at the 5' side of the duplication"; }
                        else
                        { mutation += cboRef.Text + "." + breakPoint1.ToString("N0") + "_" + breakPoint2.ToString("N0") + "dup\nThe duplicated sequence is inverted and at the 5' side of the duplication"; }
                    }
                    else if (answer == MutationType.DuplicationRCEnd)
                    {
                        if (breakPoint1 > breakPoint2)
                        { mutation += cboRef.Text + "." + breakPoint2.ToString("N0") + "_" + breakPoint1.ToString("N0") + "dup\nThe duplicated sequence is inverted and at the 3' side of the duplication"; }
                        else
                        { mutation += cboRef.Text + "." + breakPoint1.ToString("N0") + "_" + breakPoint2.ToString("N0") + "dup\nThe duplicated sequence is inverted and at the 3' side of the duplication"; }
                    }
                    else
                    {
                        mutation = setMutationPrefix(answer);
                        if (breakPoint1 > breakPoint2)
                        { mutation += cboRef.Text + "." + breakPoint2.ToString("N0") + "_" + breakPoint1.ToString("N0") + "dup"; }
                        else
                        { mutation += cboRef.Text + "." + breakPoint1.ToString("N0") + "_" + breakPoint2.ToString("N0") + "dup"; }
                    }
                    if (id != null) { id.WindowState = FormWindowState.Minimized; }
                    //if (MessageBox.Show(mutation + "\nSave with the selected read data?", "Duplication", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    //{ SaveToFile(mutation); }
                    result = "o" + mutation;
                }
                else
                {
                    if (id != null) { id.WindowState = FormWindowState.Minimized; }
                    //MessageBox.Show("Please select the reads spanning the break point by clicking on them.", "No reads selected"); 
                    result = "ePlease select the reads spanning the break point by clicking on them.";
                }
            }
            catch (Exception ex)
            {
                if (id != null) { id.WindowState = FormWindowState.Minimized; }
                //MessageBox.Show("Could not identify the variant using the selected reads", "Error"); 
                result = "Could not identify the variant using the selected reads";
            }
            return result;
        }

        private void insertionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string result = insertion();
            if (result[0] == 'e')
            {
                MessageBox.Show(result.Substring(1), "Error");
            }
            else
            {
                if (MessageBox.Show(result.Substring(1) + "\nSave with the selected read data?", "Insertion", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                { SaveToFile(result.Substring(1)); }
            }
        }

        private string insertion()
        {
            string result = "e";
            try
            {
                if (selectedIndex.Count > 0 && cboSecondaries.SelectedIndex > 0)
                {
                    string otherChromosome = cboSecondaries.Text.Substring(0, cboSecondaries.Text.IndexOf(" "));
                    BreakPointData[] bestPlaces = new BreakPointData[2];
                    int breakPoint1 = 0;
                    int breakPoint2 = 0;
                    int breakPoint3 = 0;
                    BreakPointData[] bestPlaces3rd;

                    if (otherChromosome != cboRef.Text)
                    {
                        bestPlaces = getBreakPointsOnSetChromosome(otherChromosome, false);
                        if (bestPlaces[1] != null)
                        {
                            breakPoint1 = bestPlaces[0].getAveragePlace;
                            breakPoint2 = bestPlaces[1].getAveragePlace;

                            bestPlaces3rd = getBreakPointsOnSetChromosome(cboRef.Text, false);
                            breakPoint3 = bestPlaces3rd[0].getAveragePlace;
                        }
                        else
                        {
                            bestPlaces = getBreakPointsOnSetChromosome(cboRef.Text, false);
                            breakPoint1 = bestPlaces[0].getAveragePlace;
                            breakPoint2 = bestPlaces[1].getAveragePlace;

                            bestPlaces3rd = getBreakPointsOnSetChromosome(otherChromosome, false);
                            breakPoint3 = bestPlaces3rd[0].getAveragePlace;
                        }
                    }
                    else
                    {
                        bestPlaces = getBreakPointsOnSetChromosome(otherChromosome, true);
                        breakPoint1 = bestPlaces[1].getAveragePlace;
                        breakPoint2 = bestPlaces[2].getAveragePlace;
                        breakPoint3 = bestPlaces[0].getAveragePlace;
                        bestPlaces3rd = new BreakPointData[1] { bestPlaces[0] };
                    }

                    if (bestPlaces[1] == null)
                    {
                        if (id != null) { id.WindowState = FormWindowState.Minimized; }
                        MessageBox.Show("Could not find all the breakpoints", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return result;
                    }

                    string mutation = "";
                    if (bestPlaces3rd[1] == null)
                    { mutation = bestPlaces3rd[0].getReferenceName + "." + breakPoint3.ToString("N0") + "_" + (breakPoint3 + 1).ToString("N0") + "ins " + bestPlaces[0].getReferenceName + "."; }
                    else
                    {
                        if (bestPlaces3rd[0].getAveragePlace < bestPlaces3rd[1].getAveragePlace)
                        { mutation = bestPlaces3rd[0].getReferenceName + "." + bestPlaces3rd[0].getAveragePlace.ToString("N0") + "_" + bestPlaces3rd[1].getAveragePlace.ToString("N0") + "ins " + bestPlaces[0].getReferenceName + "."; }
                        else
                        { mutation = bestPlaces3rd[0].getReferenceName + "." + bestPlaces3rd[1].getAveragePlace.ToString("N0") + "_" + bestPlaces3rd[0].getAveragePlace.ToString("N0") + "ins " + bestPlaces[0].getReferenceName + "."; }
                    }


                    if (breakPoint1 > breakPoint2)
                    { mutation += breakPoint2.ToString("N0") + "_" + breakPoint1.ToString("N0"); }
                    else
                    { mutation += breakPoint1.ToString("N0") + "_" + breakPoint2.ToString("N0"); }

                    if (id != null) { id.WindowState = FormWindowState.Minimized; }
                    //f (MessageBox.Show(mutation + "\nSave with the selected read data?", "Insertion", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    //{ SaveToFile(mutation); }
                    result = "o" + mutation;
                }
                else
                {
                    if (id != null) { id.WindowState = FormWindowState.Minimized; }
                    //MessageBox.Show("Please select the reads spanning the break point by clicking on them.", "No reads selected");
                    result = "ePlease select the reads spanning the break point by clicking on them.";
                }
            }
            catch (Exception ex)
            {
                if (id != null) { id.WindowState = FormWindowState.Minimized; }
                //MessageBox.Show("Could not identify the variant using the selected reads", "Error");
                result = "eCould not identify the variant using the selected reads";
            }
            return result;
        }

        private bool couldItBeAnInsert()
        {
            try
            {
                if (selectedIndex.Count > 0 && cboSecondaries.SelectedIndex > 0)
                {
                    string otherChromosome = cboSecondaries.Text.Substring(0, cboSecondaries.Text.IndexOf(" "));
                    BreakPointData[] bestPlaces = new BreakPointData[2];
                    int breakPoint1 = 0;
                    int breakPoint2 = 0;
                    int breakPoint3 = 0;
                    //BreakPointData[] bestPlaces3rd;

                    if (otherChromosome != cboRef.Text)
                    { return false; }
                    else
                    {
                        bestPlaces = getBreakPointsOnSetChromosome(otherChromosome, true);
                        if (bestPlaces[2] == null) { return false; }
                        breakPoint1 = bestPlaces[1].getAveragePlace / 100;
                        breakPoint2 = bestPlaces[2].getAveragePlace / 100;
                        breakPoint3 = bestPlaces[0].getAveragePlace / 100;
                    }

                    if (bestPlaces[2].Count < 2)
                    { return false; }
                    if (bestPlaces[1] == null || breakPoint1 == breakPoint2 || breakPoint1 == breakPoint3 || breakPoint2 == breakPoint3)
                    { return false; }
                    else
                    { return true; }
                }
                else
                { return false; }
            }
            catch (Exception ex)
            { return false; }
        }

        private BreakPointData[] selectForInversion(BreakPointData[] SameChr, BreakPointData[] DifferenceChr)
        {
            BreakPointData[] bestPlaces = new BreakPointData[3];
            bestPlaces[0] = SameChr[0];
            bestPlaces[1] = SameChr[1];


            if (SameChr[0].getReferenceName != DifferenceChr[0].getReferenceName)
            { bestPlaces[2] = DifferenceChr[0]; }
            else
            { bestPlaces[2] = DifferenceChr[1]; }

            return bestPlaces;
        }

        private string setMutationPrefix(MutationType mutationType)
        {
            string mutation = "";
            switch (mutationType)
            {
                case (MutationType.Deletion):
                    mutation = "The rearrangement appears to be a deletion.\n";
                    break;
                case (MutationType.Duplication):
                    mutation = "The rearrangement appears to be a duplication.\n";
                    break;
                case (MutationType.DuplicationRCStart):
                    mutation = "The rearrangement appears to be an inverted duplication.\n";
                    break;
                case (MutationType.DuplicationRCEnd):
                    mutation = "The rearrangement appears to be an inverted duplication.\n";
                    break;
                case (MutationType.Inversion):
                    mutation = "The rearrangement appears to be an inversion.\n";
                    break;
                case (MutationType.Translocation):
                    mutation = "The rearrangement appears to be a translocation.\n";
                    break;
                case MutationType.Insertion:
                    mutation = "The rearrangement appears to be an insertion.\n";
                    break;
                case (MutationType.NoSet):
                    mutation = "It was not possible to determine the type of rearrangement.\n";
                    break;
            }

            return mutation;

        }

        private MutationType testMutationType(BreakPointData[] bestPlaces)
        {
            Dictionary<string, int> orientations = new Dictionary<string, int>();

            if (bestPlaces[0].getReferenceName != bestPlaces[1].getReferenceName)
            { return MutationType.Translocation; }

            int pEnd = 0;
            int qEnd = 0;
            if (bestPlaces[0].getAveragePlace > bestPlaces[1].getAveragePlace)
            {
                pEnd = bestPlaces[1].getAveragePlace;
                qEnd = bestPlaces[0].getAveragePlace;
            }
            else
            {
                pEnd = bestPlaces[0].getAveragePlace;
                qEnd = bestPlaces[1].getAveragePlace;
            }

            foreach (int index in selectedIndex)
            {
                if (DrawnARKeys.ContainsKey(index) == true)
                {
                    AlignedRead ar = DrawnARKeys[index];
                    bool primaryStrand = ar.getForward;
                    if (string.IsNullOrEmpty(ar.getSecondaryAlignmentTag) == false)
                    {
                        string[] hits = ar.getSecondaryAlignmentTag.Substring(2).Split(';');
                        foreach (string hit in hits)
                        {
                            if (string.IsNullOrEmpty(hit) == false)
                            {
                                string[] items = hit.Split(',');
                                if (items[0].ToLower().Equals(bestPlaces[0].getReferenceName.ToLower()) == true)
                                {
                                    string key = "";
                                    string secondaryStrandtrand = "";
                                    int startPoint = Convert.ToInt32(items[1]);
                                    int endPoint = startPoint + getAlignedLength(items[3]);
                                    if (bestPlaces[0].inPlaces(startPoint) == true || bestPlaces[0].inPlaces(endPoint) == true || bestPlaces[1].inPlaces(startPoint) == true || bestPlaces[1].inPlaces(endPoint) == true)
                                    {
                                        secondaryStrandtrand = items[2];
                                        if (primaryStrand == true)
                                        {
                                            if (secondaryStrandtrand == "+")
                                            { key += "++"; }
                                            else { key += "+-"; }
                                        }
                                        else
                                        {
                                            if (secondaryStrandtrand == "+")
                                            { key += "-+"; }
                                            else { key += "--"; }
                                        }

                                        int middleOfSecondary = (startPoint + endPoint) / 2;
                                        if (middleOfSecondary < qEnd && middleOfSecondary > pEnd)
                                        { key += "inSide"; }
                                        else { key += "outSide"; }

                                        if (orientations.ContainsKey(key) == false)
                                        { orientations.Add(key, 1); }
                                        else { orientations[key]++; }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            MutationType answer = MutationType.NoSet;
            int[] strands = { 0, 0 };
            int[] inOut = { 0, 0 };
            foreach (string k in orientations.Keys)
            {
                if (k.Contains("++") == true || k.Contains("--") == true)
                { strands[0] += orientations[k]; }
                else
                { strands[1] += orientations[k]; }

                if (k.Contains("inSide") == true)
                { inOut[0] += orientations[k]; }
                else
                { inOut[1] += orientations[k]; }
            }

            int adjacentCount = Adjacent();
            if (Math.Abs(adjacentCount) > (float)selectedIndex.Count / 3)
            {
                if (adjacentCount > 0)
                { answer = MutationType.DuplicationRCStart; }
                else
                { answer = MutationType.DuplicationRCEnd; }
            }
            else if (strands[1] > strands[0])
            { answer = MutationType.Inversion; }
            else if (strands[1] < strands[0])
            {
                if (inOut[1] > inOut[0])
                { answer = MutationType.Deletion; }
                else if (inOut[1] < inOut[0])
                { answer = MutationType.Duplication; }
            }

            return answer;
        }

        private void translocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string result = translocation();
            if (result[0] == 'e')
            {
                MessageBox.Show(result.Substring(1), "Error");
            }
            else
            {
                if (MessageBox.Show(result.Substring(1) + "\nSave with the selected read data?", "Translocation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                { SaveToFile(result.Substring(1)); }
            }
        }

        private string translocation()
        {
            string result = "e";
            try
            {
                if (selectedIndex.Count > 0)
                {
                    BreakPointData[] bestPlaces = getBreakPoints(false, cboRef.Text);
                    int breakPoint1 = bestPlaces[0].getAveragePlace;
                    int breakPoint2 = bestPlaces[1].getAveragePlace;

                    if (bestPlaces[1] == null)
                    {
                        if (id != null) { id.WindowState = FormWindowState.Minimized; }
                        MessageBox.Show("Could not find both sides of the breakpoint", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return result;
                    }

                    string mutation = "";
                    MutationType answer = testMutationType(bestPlaces);
                    if (answer != MutationType.Deletion)
                    { mutation = setMutationPrefix(answer); }

                    int best1 = getChromosomeNumber(bestPlaces[0].getReferenceName);
                    int best2 = getChromosomeNumber(bestPlaces[1].getReferenceName);

                    if (best1 > best2)
                    {
                        mutation = "t(" + bestPlaces[1].getReferenceName + ";" + bestPlaces[0].getReferenceName + ") (g."
                            + breakPoint2.ToString("N0") + ";g." + breakPoint1.ToString("N0") + ")";
                    }
                    else
                    {
                        mutation = "t(" + bestPlaces[0].getReferenceName + ";" + bestPlaces[1].getReferenceName + ") (g."
                            + breakPoint1.ToString("N0") + ";g." + breakPoint2.ToString("N0") + ")";
                    }

                    if (id != null) { id.WindowState = FormWindowState.Minimized; }
                    //if (MessageBox.Show(mutation + "\nSave with the selected read data?", "Translocation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    //{ SaveToFile(mutation); }
                    result = "o" + mutation;
                }
            }
            catch (Exception ex)
            {
                if (id != null) { id.WindowState = FormWindowState.Minimized; }
                //MessageBox.Show("Could not identify the variant using the selected reads", "Error"); 
                result = "eCould not identify the variant using the selected reads";
            }
            return result;
        }

        private void SaveToFile(string mutation)
        {
            string file = FileString.SaveAs("Select the file to save the data in", "Text file (*.txt)|*.txt");
            if (file.Equals("Cancel") == true) { return; }

            System.IO.StreamWriter fw = null;
            try
            {
                fw = new System.IO.StreamWriter(file);
                fw.WriteLine(mutation);
                fw.WriteLine(" ");
                fw.WriteLine("Read data:");
                foreach (int index in selectedIndex)
                {
                    if (DrawnARKeys.ContainsKey(index) == true)
                    {
                        string[] data = DrawnARKeys[index].displayDataArray();
                        string dataW = data[0];
                        dataW += referenceSequenceNames[Convert.ToInt32(data[1])];
                        dataW += data[2];
                        fw.WriteLine(dataW);
                    }
                }
            }
            catch { }
            finally
            { if (fw != null) { fw.Close(); } }
        }

        private int getChromosomeNumber(string chrRefName)
        {
            int answer = -1;
            chrRefName = chrRefName.ToLower().Replace("chr", "");
            if (chrRefName.StartsWith("x"))
            { answer = 23; }
            else if (chrRefName.StartsWith("y"))
            { answer = 24; }
            else if (chrRefName.StartsWith("m"))
            { answer = 25; }
            else
            {
                int lastNumber = 0;
                for (int index = 0; index < chrRefName.Length; index++)
                {
                    if (char.IsDigit(chrRefName[index]) == true)
                    { lastNumber = index; }
                    else { break; }
                }
                string chrNumber = chrRefName.Substring(0, lastNumber + 1);
                try { answer = Convert.ToInt32(chrNumber); }
                catch (Exception ex) { answer = -1; }
            }

            return answer;
        }

        private int Adjacent()
        {
            int answerFivePrime = 0;
            int answerThreeprime = 0;
            foreach (int index in selectedIndex)
            {
                if (DrawnARKeys.ContainsKey(index) == true)
                {
                    AlignedRead ar = DrawnARKeys[index];
                    if (ar.hasFivePrimeSoftClip == true)
                    {
                        int primaryStartPoint = ar.getPosition;
                        string referenceSequenceTarget = referenceSequences[ar.getreferenceIndex].name;

                        string secondaryCIGAR = ar.getSecondaryAlignmentTag;
                        if (string.IsNullOrEmpty(secondaryCIGAR) == false)
                        {
                            string[] hits = secondaryCIGAR.Substring(2).Split(';');
                            foreach (string h in hits)
                            {
                                if (string.IsNullOrEmpty(h) == false)
                                {
                                    string[] items = h.Split(',');
                                    int startPoint = Convert.ToInt32(items[1]);
                                    if (referenceSequenceTarget.Equals(items[0]))
                                    {
                                        if (startPoint - 100 < primaryStartPoint && startPoint + 100 > primaryStartPoint)
                                        { answerFivePrime++; }
                                    }
                                }
                            }
                        }

                    }


                    if (ar.hasThreePrimeSoftClip == true)
                    {
                        int primaryEndPoint = ar.getEndPosition;
                        string referenceSequenceTarget = referenceSequences[ar.getreferenceIndex].name;

                        string secondaryCIGAR = ar.getSecondaryAlignmentTag;
                        if (string.IsNullOrEmpty(secondaryCIGAR) == false)
                        {
                            string[] hits = secondaryCIGAR.Substring(2).Split(';');
                            foreach (string h in hits)
                            {
                                if (string.IsNullOrEmpty(h) == false)
                                {
                                    string[] items = h.Split(',');
                                    int startPoint = Convert.ToInt32(items[1]);
                                    if (referenceSequenceTarget.Equals(items[0]))
                                    {
                                        int l = getAlignedLength(items[3]) + startPoint;
                                        if (l - 100 < primaryEndPoint && l + 100 > primaryEndPoint)
                                        { answerThreeprime++; }
                                    }
                                }
                            }
                        }
                    }

                }


            }

            int answer = 0;
            if (answerThreeprime > answerFivePrime)
            { answer = -answerThreeprime; }
            else { answer = answerFivePrime; }

            return answer;
        }

        private BreakPointData[] getBreakPoints(bool sameReferenceSequence, string referenceSequenceTarget)
        {
            Dictionary<string, List<int>> places = new Dictionary<string, List<int>>();
            foreach (int index in selectedIndex)
            {
                if (DrawnARKeys.ContainsKey(index) == true)
                {
                    AlignedRead ar = DrawnARKeys[index];
                    if (ar.hasFivePrimeSoftClip == true)
                    {
                        if (places.ContainsKey(referenceSequenceNames[ar.getreferenceIndex]) == false)
                        {
                            places.Add(referenceSequenceNames[ar.getreferenceIndex], new List<int>());
                            places[referenceSequenceNames[ar.getreferenceIndex]].Add(ar.getPosition);
                        }
                        else
                        { places[referenceSequenceNames[ar.getreferenceIndex]].Add(ar.getPosition); }
                    }

                    if (ar.hasThreePrimeSoftClip == true)
                    {
                        if (places.ContainsKey(referenceSequenceNames[ar.getreferenceIndex]) == false)
                        {
                            places.Add(referenceSequenceNames[ar.getreferenceIndex], new List<int>());
                            places[referenceSequenceNames[ar.getreferenceIndex]].Add(ar.getEndPosition);
                        }
                        else
                        { places[referenceSequenceNames[ar.getreferenceIndex]].Add(ar.getEndPosition); }
                    }

                    string secondaryCIGAR = ar.getSecondaryAlignmentTag;
                    if (string.IsNullOrEmpty(secondaryCIGAR) == false)
                    {
                        string[] hits = secondaryCIGAR.Substring(2).Split(';');
                        foreach (string h in hits)
                        {
                            if (string.IsNullOrEmpty(h) == false)
                            {
                                string[] items = h.Split(',');
                                int startPoint = Convert.ToInt32(items[1]);
                                if (sameReferenceSequence == referenceSequenceTarget.Equals(items[0]))
                                {
                                    if (getFivePrimeSoftClipLength(items[3]) > 50)
                                    {
                                        if (places.ContainsKey(items[0]) == false)
                                        {
                                            places.Add(items[0], new List<int>());
                                            places[items[0]].Add(startPoint);
                                        }
                                        else
                                        { places[items[0]].Add(startPoint); }
                                    }

                                    if (getThreePrimeSoftClipLength(items[3]) > 50)
                                    {
                                        if (places.ContainsKey(items[0]) == false)
                                        {
                                            places.Add(items[0], new List<int>());
                                            places[items[0]].Add(startPoint + getAlignedLength(items[3]));
                                        }
                                        else
                                        { places[items[0]].Add(startPoint + getAlignedLength(items[3])); }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            foreach (List<int> points in places.Values)
            { points.Sort(); }

            return getBreakPoints(places, sameReferenceSequence, false);
        }

        private BreakPointData getOtherSide(BreakPointData breakPoint, List<int[]> pairs)
        {
            BreakPointData answer = null;

            int first = breakPoint.getAveragePlace;
            int chromosome = getReferenceIndexFromName(breakPoint.getReferenceName);
            List<int> thesePlaces = new List<int>();
            string otherName = "";
            int otherPlace = -1;

            foreach (int[] pair in pairs)
            {
                if (pair[1] == chromosome && (pair[0] + 201 > first && pair[0] - 201 < first))
                {
                    thesePlaces.Add(pair[2]);
                    otherName = referenceSequenceNames[pair[3]];
                    otherPlace = pair[2];
                }
                else if (pair[3] == chromosome && (pair[2] + 201 > first && pair[2] - 201 < first))
                {
                    thesePlaces.Add(pair[0]);
                    otherName = referenceSequenceNames[pair[1]];
                    otherPlace = pair[0];
                }
            }

            if (otherName != "")
            {
                answer = new BreakPointData(otherPlace, thesePlaces.ToArray(), otherName);
            }
            return answer;
        }

        private BreakPointData[] getBreakPointsWithGapOnSameChromosome(BreakPointData[] breakPoints)
        {
            BreakPointData[] biggest = new BreakPointData[2];
            int first = -1;
            int second = -1;
            int gap = -1;
            for (int outer = 0; outer < breakPoints.Length - 1; outer++)
            {
                if (breakPoints[outer] != null)
                {
                    for (int inner = 1; inner < breakPoints.Length; inner++)
                    {
                        if (breakPoints[inner] != null)
                        {
                            if (breakPoints[inner].getReferenceName == breakPoints[outer].getReferenceName)
                            {
                                if (Math.Abs(breakPoints[inner].getAveragePlace - breakPoints[outer].getAveragePlace) > gap)
                                {
                                    first = inner;
                                    second = outer;
                                    gap = Math.Abs(breakPoints[inner].getAveragePlace - breakPoints[outer].getAveragePlace);
                                }
                            }
                        }
                    }
                }
            }

            if (first > -1)
            {
                biggest[0] = breakPoints[first];
                biggest[1] = breakPoints[second];
            }
            return biggest;
        }

        private BreakPointData[] getBreakPointsOnSetChromosome(string referenceSequenceTarget, bool three)
        {
            Dictionary<string, List<int>> places = new Dictionary<string, List<int>>();
            foreach (int index in selectedIndex)
            {
                if (DrawnARKeys.ContainsKey(index) == true)
                {
                    AlignedRead ar = DrawnARKeys[index];
                    if (referenceSequenceNames[ar.getreferenceIndex] == referenceSequenceTarget)
                    {
                        if (ar.hasFivePrimeSoftClip == true)
                        {
                            if (places.ContainsKey(referenceSequenceNames[ar.getreferenceIndex]) == false)
                            {
                                places.Add(referenceSequenceNames[ar.getreferenceIndex], new List<int>());
                                places[referenceSequenceNames[ar.getreferenceIndex]].Add(ar.getPosition);
                            }
                            else
                            { places[referenceSequenceNames[ar.getreferenceIndex]].Add(ar.getPosition); }
                        }
                        if (ar.hasThreePrimeSoftClip == true)
                        {
                            if (places.ContainsKey(referenceSequenceNames[ar.getreferenceIndex]) == false)
                            {
                                places.Add(referenceSequenceNames[ar.getreferenceIndex], new List<int>());
                                places[referenceSequenceNames[ar.getreferenceIndex]].Add(ar.getEndPosition);
                            }
                            else
                            { places[referenceSequenceNames[ar.getreferenceIndex]].Add(ar.getEndPosition); }
                        }
                    }

                    string secondaryCIGAR = ar.getSecondaryAlignmentTag;
                    if (string.IsNullOrEmpty(secondaryCIGAR) == false)
                    {
                        string[] hits = secondaryCIGAR.Substring(2).Split(';');
                        foreach (string h in hits)
                        {
                            if (string.IsNullOrEmpty(h) == false)
                            {
                                string[] items = h.Split(',');
                                int startPoint = Convert.ToInt32(items[1]);
                                if (referenceSequenceTarget.Equals(items[0]))
                                {
                                    if (getFivePrimeSoftClipLength(items[3]) > 50)
                                    {
                                        if (places.ContainsKey(items[0]) == false)
                                        {
                                            places.Add(items[0], new List<int>());
                                            places[items[0]].Add(startPoint);
                                        }
                                        else
                                        { places[items[0]].Add(startPoint); }
                                    }
                                    if (getThreePrimeSoftClipLength(items[3]) > 50)
                                    {
                                        if (places.ContainsKey(items[0]) == false)
                                        {
                                            places.Add(items[0], new List<int>());
                                            places[items[0]].Add(startPoint + getAlignedLength(items[3]));
                                        }
                                        else
                                        { places[items[0]].Add(startPoint + getAlignedLength(items[3])); }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            foreach (List<int> points in places.Values)
            { points.Sort(); }

            return getBreakPoints(places, true, three);
        }

        private BreakPointData[] getBreakPoints(Dictionary<string, List<int>> places, bool sameReferenceSequence, bool three)
        {
            if (three == false)
            { return getBreakPoints(places, sameReferenceSequence); }
            else
            { return getBreakThreePoints(places, sameReferenceSequence); }
        }

        private BreakPointData[] getBreakThreePoints(Dictionary<string, List<int>> places, bool sameReferenceSequence)
        {
            BreakPointData[] bestRegions = new BreakPointData[3];
            int best1 = 0;
            int best2 = 0;
            int best3 = 0;
            int best1Place = 0;
            int best2Place = 0;
            int best3Place = 0;
            foreach (string chr in places.Keys)
            {
                List<int> chrPlaces = places[chr];
                int index = 0;
                int counter = 0;
                for (int indexLoop = 0; indexLoop < chrPlaces.Count; indexLoop++)
                {
                    int regionStart = chrPlaces[indexLoop];
                    index = indexLoop;
                    counter = 0;
                    while (index < chrPlaces.Count && chrPlaces[index] <= regionStart + 200)
                    {
                        counter++;
                        index++;
                    }

                    if (counter > best1)
                    {
                        best1 = counter;
                        best1Place = regionStart;
                        List<int> near = new List<int>();
                        foreach (int p in chrPlaces)
                        {
                            if (p >= regionStart && p <= regionStart + 200)
                                near.Add(p);
                        }
                        bestRegions[0] = new BreakPointData(regionStart, near.ToArray<int>(), chr);
                    }
                }
            }

            foreach (string chr in places.Keys)
            {
                if ((chr == bestRegions[0].getReferenceName) == sameReferenceSequence)
                {
                    List<int> chrPlaces = places[chr];
                    int index = 0;
                    int counter = 0;
                    for (int indexLoop = 0; indexLoop < chrPlaces.Count; indexLoop++)
                    {
                        int regionStart = chrPlaces[indexLoop];
                        index = indexLoop;
                        counter = 0;
                        while (index < chrPlaces.Count && chrPlaces[index] <= regionStart + 200)
                        {
                            if (bestRegions[0].getReferenceName == chr)
                            {
                                if (regionStart < best1Place - 300 || regionStart > best1Place + 300)
                                { counter++; }
                            }
                            else
                            { counter++; }
                            index++;
                        }
                        System.Diagnostics.Debug.WriteLine("counter = " + counter.ToString() + " place " + regionStart.ToString());

                        if (regionStart > best2Place - 200 && regionStart < best2Place + 200)
                        {
                            if (counter > best1)
                            {
                                best2 = counter;
                                best2Place = regionStart;
                                List<int> near2 = new List<int>();
                                foreach (int p in chrPlaces)
                                {
                                    if (p >= regionStart - 200 && p <= regionStart + 200)
                                    { near2.Add(p); }
                                }
                                bestRegions[1] = new BreakPointData(regionStart, near2.ToArray<int>(), chr);
                                System.Diagnostics.Debug.WriteLine("updated best 2 at " + bestRegions[1].getAveragePlace.ToString("N0"));
                            }
                        }
                        else if (counter > best2 && (regionStart < best2Place - 300 || regionStart > best2Place + 300))
                        {
                            int tempBest3 = best2;
                            int tempBest3Place = best2Place;
                            BreakPointData tempBestRegion = bestRegions[1];

                            best2 = counter;
                            best2Place = regionStart;
                            List<int> near2 = new List<int>();
                            foreach (int p in chrPlaces)
                            {
                                if (p >= regionStart - 200 && p <= regionStart + 200)
                                { near2.Add(p); }
                            }
                            bestRegions[1] = new BreakPointData(regionStart, near2.ToArray<int>(), chr);
                            System.Diagnostics.Debug.WriteLine("New best 2 at " + bestRegions[1].getAveragePlace.ToString("N0"));

                            if (AreTwoBreakPointsTheSame(bestRegions[1], tempBestRegion, 200) == false)
                            {
                                best3 = tempBest3;
                                best3Place = tempBest3Place;
                                bestRegions[2] = tempBestRegion;
                                if (bestRegions[2] != null)
                                { System.Diagnostics.Debug.WriteLine("New best 3 1 at " + bestRegions[2].getAveragePlace.ToString("N0")); }
                            }
                        }
                        else if (regionStart > best3Place - 300 && regionStart < best3Place + 300)
                        {
                            best3 = counter;
                            best3Place = regionStart;
                            List<int> near3 = new List<int>();
                            foreach (int p in chrPlaces)
                            {
                                if (p >= regionStart - 200 && p <= regionStart + 200)
                                { near3.Add(p); }
                            }
                            bestRegions[2] = new BreakPointData(regionStart, near3.ToArray<int>(), chr);
                            System.Diagnostics.Debug.WriteLine("New best 3 2at " + bestRegions[2].getAveragePlace.ToString("N0"));
                        }
                        else if (counter > best3)
                        {
                            best3 = counter;
                            best3Place = regionStart;
                            List<int> near3 = new List<int>();
                            foreach (int p in chrPlaces)
                            {
                                if (p >= regionStart && p <= regionStart + 200)
                                    near3.Add(p);
                            }
                            bestRegions[2] = new BreakPointData(regionStart, near3.ToArray<int>(), chr);
                            System.Diagnostics.Debug.WriteLine("New best 3 3 at " + bestRegions[2].getAveragePlace.ToString("N0"));
                        }
                    }
                    System.Diagnostics.Debug.WriteLine("best1 " + bestRegions[0].getAveragePlace.ToString("N0") + " best 2 " + best2Place.ToString("N0") + " best 3 " + best3Place.ToString("N0"));
                }
            }
            return bestRegions;
        }

        private bool AreTwoBreakPointsTheSame(BreakPointData bp1, BreakPointData bp2, int span)
        {
            if (bp1 != null && bp2 != null)
            { return (Math.Abs(bp1.getAveragePlace - bp2.getAveragePlace) < span); }
            else
            { return false; }
        }

        private BreakPointData[] getBreakPoints(Dictionary<string, List<int>> places, bool sameReferenceSequence)
        {
            BreakPointData[] bestRegions = new BreakPointData[2];
            int best1 = 0;
            int best2 = 0;
            int best1Place = 0;
            int best2Place = 0;
            foreach (string chr in places.Keys)
            {
                List<int> chrPlaces = places[chr];
                int index = 0;
                int counter = 0;
                for (int indexLoop = 0; indexLoop < chrPlaces.Count; indexLoop++)
                {
                    int regionStart = chrPlaces[indexLoop];
                    index = indexLoop;
                    counter = 0;
                    while (index < chrPlaces.Count && chrPlaces[index] <= regionStart + 200)
                    {
                        counter++;
                        index++;
                    }

                    if (counter > best1)
                    {
                        best1 = counter;
                        best1Place = regionStart;
                        List<int> near = new List<int>();
                        foreach (int p in chrPlaces)
                        {
                            if (p >= regionStart && p <= regionStart + 200)
                            { near.Add(p); }
                        }
                        bestRegions[0] = new BreakPointData(regionStart, near.ToArray<int>(), chr);
                    }
                }
            }

            foreach (string chr in places.Keys)
            {
                if ((chr == bestRegions[0].getReferenceName) == sameReferenceSequence)
                {
                    List<int> chrPlaces = places[chr];
                    int index = 0;
                    int counter = 0;
                    for (int indexLoop = 0; indexLoop < chrPlaces.Count; indexLoop++)
                    {
                        int regionStart = chrPlaces[indexLoop];
                        index = indexLoop;
                        counter = 0;
                        while (index < chrPlaces.Count && chrPlaces[index] <= regionStart + 200)
                        {
                            if (bestRegions[0].getReferenceName == chr)
                            {
                                if (regionStart < best1Place - 300 || regionStart > best1Place + 300)
                                { counter++; }
                            }
                            else
                            { counter++; }
                            index++;
                        }

                        if (counter > best2 && (regionStart < best2Place - 300 || regionStart > best2Place + 300))
                        {
                            best2 = counter;
                            best2Place = regionStart;
                            List<int> near2 = new List<int>();
                            foreach (int p in chrPlaces)
                            {
                                if (p >= regionStart && p <= regionStart + 200)
                                    near2.Add(p);
                            }
                            bestRegions[1] = new BreakPointData(regionStart, near2.ToArray<int>(), chr);
                        }
                    }
                }
            }
            return bestRegions;
        }

        private void switchRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string region = cboSecondaries.Text;
            string[] items = region.Split(' ');
            for (int index = 0; index < referenceSequenceNames.Length; index++)
            {
                if (referenceSequenceNames[index].Equals(items[0]) == true)
                {
                    cboRef.SelectedIndex = index + 1;
                    break;
                }
            }
            txtStart.Text = txtsStart.Text;
            txtEnd.Text = txtsEnd.Text;
            btnGetReads.PerformClick();
        }

        private void variantTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cboSecondaries.SelectedIndex == 0)
            {
                if (id != null) { id.WindowState = FormWindowState.Minimized; }
                MessageBox.Show("You must select a region containing the secondary alignments.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (selectedIndex.Count > 0)
                {
                    bool sameSequence;
                    if (cboSecondaries.Text.ToLower().Contains((cboRef.Text + " ").ToLower()) == true)
                    { sameSequence = true; }
                    else { sameSequence = false; }

                    BreakPointData[] bestPlaces = getBreakPoints(sameSequence, cboRef.Text);
                    if (bestPlaces[1] == null)
                    {
                        if (id != null) { id.WindowState = FormWindowState.Minimized; }
                        MessageBox.Show("Could not find both sides of the breakpoint", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int breakPoint1 = bestPlaces[0].getAveragePlace;
                    int breakPoint2 = bestPlaces[1].getAveragePlace;

                    string mutation = "";
                    MutationType answer = testMutationType(bestPlaces);

                    if (answer == MutationType.Deletion || answer == MutationType.DuplicationRCStart || answer == MutationType.DuplicationRCEnd || answer == MutationType.Inversion)
                    {
                        if (couldItBeAnInsert() == true)
                        { answer = MutationType.Insertion; }
                    }
                    else if (answer == MutationType.Translocation)
                    {
                        string alt = cboSecondaries.Text;
                        BreakPointData[] set1 = getBreakPointsOnSetChromosome(cboRef.Text, false);
                        BreakPointData[] set2 = getBreakPointsOnSetChromosome(alt.Substring(0, alt.IndexOf(" ")), false);
                        if (set1[1] != null || set2[1] != null)
                        { answer = MutationType.Insertion; }
                    }

                    if (id != null) { id.WindowState = FormWindowState.Minimized; }
                    mutation = setMutationPrefix(answer);
                    MessageBox.Show(mutation, "Rearrangement type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (id != null) { id.WindowState = FormWindowState.Minimized; }
                    MessageBox.Show("Please select the reads spanning the break point by clicking on them.", "No reads selected");
                }
            }
            catch
            {
                if (id != null) { id.WindowState = FormWindowState.Minimized; }
                MessageBox.Show("Something went wrong does the alignments contian softclipped cigar strings!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void geneCoordinatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gd == null) { return; }
            if (referenceSequenceNames == null) { return; }
            FindGene fg = new FindGene();
            fg.setNames(gd);
            if (fg.ShowDialog() == DialogResult.OK)
            {
                if (fg.getGeneData != null)
                {
                    try
                    {
                        ignoreHistory = true;
                        string[] data = fg.getGeneData;
                        int index = getReferenceIndexFromName(data[0]);
                        cboRef.SelectedIndex = index + 1;
                        txtStart.Text = data[1];
                        txtEnd.Text = data[2];
                    }
                    finally
                    { ignoreHistory = false; }

                }
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {

            repopulateSecondaryList();

            FilterHits fh = new FilterHits(referenceSequenceNames, 2);
            if (fh.ShowDialog() == DialogResult.OK)
            {
                string chromosome = fh.getHitSequence.Trim() + " ";
                int count = fh.getHitCount;

                if (secondaryAlignments != null && secondaryAlignments.Count > 0)
                {

                    List<string> bins = new List<string>();
                    foreach (string k in secondaryAlignments.Keys)
                    {
                        if ((k.Contains(chromosome) == true || chromosome.StartsWith("Select")) && secondaryAlignments[k] >= count)
                        {
                            if (secondaryAlignments[k] > 1)
                            { bins.Add(k + " bp (" + secondaryAlignments[k].ToString() + ")"); }
                        }
                    }

                    bins.Sort();
                    cboSecondaries.Items.Clear();
                    cboSecondaries.Items.Add("Select a region");
                    cboSecondaries.Items.AddRange(bins.ToArray());
                    cboSecondaries.SelectedIndex = 0;
                }
                else
                {
                    cboSecondaries.Items.Clear();
                    cboSecondaries.Items.Add("Select a region");
                    cboSecondaries.SelectedIndex = 0;
                }
            }
        }

        private void repopulateSecondaryList()
        {
            if (secondaryAlignments != null && secondaryAlignments.Count > 0)
            {

                List<string> bins = new List<string>();
                foreach (string k in secondaryAlignments.Keys)
                {
                    if (secondaryAlignments[k] > 1)
                    { bins.Add(k + " bp (" + secondaryAlignments[k].ToString() + ")"); }
                }

                bins.Sort();
                cboSecondaries.Items.Clear();
                cboSecondaries.Items.Add("Select a region");
                cboSecondaries.Items.AddRange(bins.ToArray());
                cboSecondaries.SelectedIndex = 0;
            }
            else
            {
                cboSecondaries.Items.Clear();
                cboSecondaries.Items.Add("Select a region");
                cboSecondaries.SelectedIndex = 0;
            }
        }

        private StringIntInt getAnnotationInterval(PictureBox pB, Graphics g, Font f, int regionStart, int regionEnd)
        {
            int width = pB.Width - 20;
            int interval = regionEnd - regionStart;

            float pixelPerBase = ((float)width / interval);
            StringIntInt answer = new StringIntInt(" Mp", 500000000, 1000000);
            int index = 0;
            int[] multiple = { 100, 500, 1000, 5000, 10000, 50000, 100000, 500000, 1000000, 5000000, 10000000, 50000000, 100000000 };
            int[] factor = { 1, 1, 1000, 1000, 1000, 1000, 1000, 1000, 1000000, 1000000, 1000000, 1000000, 1000000 };
            string[] suffix = { " bp", " bp", " Kb", " Kb", " Kb", " Kb", " Kb", " Kb", " Mb", " Mb", " Mb", " Mb", " Mb" };
            while (index < multiple.Length)
            {
                string mark = (regionEnd / multiple[index]).ToString("N1") + suffix[index];
                float length = g.MeasureString(mark, f).Width;
                if (length < pixelPerBase * multiple[index])
                {
                    answer = new StringIntInt(suffix[index], multiple[index], factor[index]);
                    break;
                }
                index++;
            }

            return answer;
        }

        bool showPosition = false;
        private void showPositionOfCursorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPositionOfCursorToolStripMenuItem.Checked = !showPositionOfCursorToolStripMenuItem.Checked;
            showPosition = showPositionOfCursorToolStripMenuItem.Checked;
        }

        private void alignerStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (id != null) { id.WindowState = FormWindowState.Minimized; }
            if (string.IsNullOrEmpty(alignerString) == true)
            { MessageBox.Show("No data", "Aligner command string", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else
            { MessageBox.Show(alignerString, "Aligner command string", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void lookForIndelsWithinAReadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lookForIndelsWithinAReadToolStripMenuItem.Checked = !lookForIndelsWithinAReadToolStripMenuItem.Checked;
            simplified = !lookForIndelsWithinAReadToolStripMenuItem.Checked;
            btnGetReads.PerformClick();
        }

        private void insertationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedIndex.Count > 0)
            {
                List<int> startPoint = new List<int>();
                List<int> endPoint = new List<int>();
                List<string> inserts = new List<string>();

                foreach (int index in selectedIndex)
                {
                    if (DrawnARKeys.ContainsKey(index) == true)
                    {
                        AlignedRead ar = DrawnARKeys[index];
                        List<AlignedRead.fragment> blocks = ar.getBlocks;
                        int place = ar.getPositionWithSoftClip;
                        int start = 0;
                        foreach (AlignedRead.fragment frag in blocks)
                        {
                            if (frag.state == AlignedRead.fragmentType.Insert)
                            {
                                if (place >= selectStart && place <= selectEnd)
                                {
                                    if (frag.length > 10)
                                    {
                                        if (start == 0) { start = place - ar.getEndPosition; }
                                        startPoint.Add(place);
                                        endPoint.Add(place + frag.length);
                                        inserts.Add(cboRef.Text + ":" + place.ToString("N0") + "-" + (place + 1).ToString("N0") + "ins" + frag.length.ToString("N0") + "bp\t" + ar.getName);
                                    }
                                }
                            }
                            if (frag.state != AlignedRead.fragmentType.Insert)
                            { place += frag.length; }
                        }
                    }
                }
                if (startPoint.Count > 1 && endPoint.Count > 1)
                {
                    inserts.Sort(new insertSorter());
                    StringBuilder sb = new StringBuilder();

                    foreach (string s in inserts)
                    { sb.Append(s.Split('\t')[0] + "\n"); }

                    startPoint.Sort();
                    endPoint.Sort();

                    int middle = 0;
                    int mediumStart = 0;
                    int mediumEnd = 0;

                    if (startPoint.Count % 2 == 0)
                    {
                        middle = startPoint.Count / 2;
                        mediumStart = (startPoint[middle - 1] + startPoint[middle]) / 2;
                        mediumEnd = (endPoint[middle - 1] + endPoint[middle]) / 2;
                    }
                    else
                    {
                        middle = startPoint.Count / 2;
                        mediumStart = startPoint[middle];
                        mediumEnd = endPoint[middle];
                    }

                    sb.Append("\nMedian values\n" + cboRef.Text + ":" + mediumStart.ToString("N0") + "-" + (mediumStart + 1).ToString("N0") + "ins" + (mediumStart - mediumEnd).ToString("N0") + "bp\n\nDo you want to save the inserts annotation?");

                    if (id != null) { id.WindowState = FormWindowState.Minimized; }
                    if (MessageBox.Show(sb.ToString(), "Inserts", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        sb = new StringBuilder();
                        foreach (string s in inserts)
                        { sb.Append(s + "\n"); }
                        sb.Append("\nMedian values\n" + cboRef.Text + ":" + mediumStart.ToString("N0") + "-" + (mediumStart + 1).ToString("N0") + "ins" + (mediumStart - mediumEnd).ToString("N0"));
                        SaveToFile(sb.ToString());
                    }
                }
                else if (inserts.Count == 1)
                {
                    if (id != null) { id.WindowState = FormWindowState.Minimized; }
                    if (MessageBox.Show("Only one insert\n" + inserts[0] + "\n\nDo you want to save the inserts annotation ? ", "Insertions", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    { SaveToFile(inserts[0]); }
                }
            }
        }

        private void deletionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedIndex.Count > 0)
            {

                List<int> startPoint = new List<int>();
                List<int> endPoint = new List<int>();
                List<string> deletions = new List<string>();

                foreach (int index in selectedIndex)
                {
                    if (DrawnARKeys.ContainsKey(index) == true)
                    {
                        AlignedRead ar = DrawnARKeys[index];
                        List<AlignedRead.fragment> blocks = ar.getBlocks;
                        int place = ar.getPositionWithSoftClip;
                        foreach (AlignedRead.fragment frag in blocks)
                        {
                            if (frag.state == AlignedRead.fragmentType.Deletion)
                            {
                                if (place >= selectStart && place <= selectEnd)
                                {
                                    if (frag.length > 10)
                                    {
                                        startPoint.Add(place);
                                        endPoint.Add(place + frag.length);
                                        deletions.Add(cboRef.Text + ":" + place.ToString("N0") + "-" + (place + frag.length).ToString("N0") + "del\t" + ar.getName);
                                    }
                                }
                            }
                            if (frag.state != AlignedRead.fragmentType.Insert)
                            { place += frag.length; }
                        }
                    }
                }
                if (startPoint.Count > 0 && endPoint.Count > 0)
                {
                    deletions.Sort(new deletionSorter());
                    StringBuilder sb = new StringBuilder();

                    foreach (string s in deletions)
                    { sb.Append(s.Split('\t')[0] + "\n"); }

                    startPoint.Sort();
                    endPoint.Sort();

                    int middle = 0;
                    int mediumStart = 0;
                    int mediumEnd = 0;

                    if (startPoint.Count % 2 == 0)
                    {
                        middle = startPoint.Count / 2;
                        mediumStart = (startPoint[middle - 1] + startPoint[middle]) / 2;
                        mediumEnd = (endPoint[middle - 1] + endPoint[middle]) / 2;
                    }
                    else
                    {
                        middle = startPoint.Count / 2;
                        mediumStart = startPoint[middle];
                        mediumEnd = endPoint[middle];
                    }


                    sb.Append("\nMedian breakpoints\n" + cboRef.Text + ":" + mediumStart.ToString("N0") + "-" + mediumEnd.ToString("N0") + "del\n\nDo you want to save the data?");
                    if (id != null) { id.WindowState = FormWindowState.Minimized; }
                    if (MessageBox.Show(sb.ToString(), "Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        foreach (string s in deletions)
                        { sb.Append(s + "\n"); }
                        sb.Append("\nMedian breakpoints\n" + cboRef.Text + ":" + mediumStart.ToString("N0") + "-" + mediumEnd.ToString("N0") + "del");
                        SaveToFile(sb.ToString());

                    }
                }
                else if (deletions.Count == 1)
                {
                    if (id != null) { id.WindowState = FormWindowState.Minimized; }
                    if (MessageBox.Show("Only one insert\n" + deletions[0] + "\n\nDo you want to save the inserts annotation ? ", "Insertions", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    { SaveToFile(deletions[0]); }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void openBAMFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2.PerformClick();
        }

        private void onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem.Checked = !onlyShowReadsWithSecondaryAlignmentsToolStripMenuItem.Checked;
            btnGetReads.PerformClick();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void onlyShowReadsWithALargeIndelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onlyShowReadsWithALargeIndelToolStripMenuItem.Checked = !onlyShowReadsWithALargeIndelToolStripMenuItem.Checked;
            if (lookForIndelsWithinAReadToolStripMenuItem.Checked == false)
            { lookForIndelsWithinAReadToolStripMenuItem.PerformClick(); }
            else { btnGetReads.PerformClick(); }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (resizing == false)
            {
                resizing = true;
                timer1.Enabled = true;
            }
        }
    }
}
