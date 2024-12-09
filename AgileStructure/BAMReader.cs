using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AgileStructure
{
    class BAMReader
    {
		private string BamFileName = "";
		private Decompressor? decom;
		private Dictionary<int, string>? headerText ;
		private Dictionary<int, string>? referenceText ;

		private string answer;
		private int redID;
		private int pos;
		private int l_ReadName;
		private int maq;
		private int flag;
		private int l_seq;
		private int next_refID;
		private int next_pos;
		private int tlen;
		private string read_Name;
		private string cigar;
		private string seq;
		private string qual;
		private string tags;
		//private string chr;
		//private string otherChr;

		public BAMReader()
		{ pos = int.MaxValue; }

		public BAMReader(string fileName)
		{
			BamFileName = fileName;
			decom = new Decompressor(fileName);
			
			byte[] cfour = new byte[4];
			int value = 0;
			decom.read(cfour, 4);

			string firstThree = getString(cfour);
			
			if (firstThree.Substring(0, 3).CompareTo("BAM") != 0)
			{
				throw new Exception("Not a BAM file.");
			}

			value = getInt32();
			byte[] c = new byte[value];
			decom.read(c, value);
			string line = "";
			int lineCount = 0;
			headerText = new Dictionary<int, string>();

			for (int index = 0; index < value; index++)
			{

				if (c[index] != '\n')
				{
					line += (char)c[index];
				}
				else
				{
					headerText.Add(lineCount, line);
					lineCount++;
					line = "";
				}
			}

			if (line.Length > 0)
			{
				headerText.Add(lineCount, line);
				lineCount++;
				line = "";
			}

			lineCount = 0;
			value = getInt32();

			int len = 0;
			int chrLen = 0;

			referenceText = new Dictionary<int, string>();
			for (int index = 0; index < value; index++)
			{				
				len = getInt32();
				c = new byte[len];
				decom.read(c, len);

				for (int counter = 0; counter < (int)len; counter++)
				{
					if (c[counter] != '\0')
					{ line += (char)c[counter]; }
				}

				chrLen = getInt32();

				line += "\t" + chrLen.ToString();
				referenceText.Add(lineCount, line);
				lineCount++;
				line = "";
			}
			
			pos = int.MaxValue;
		}

		public BAMReader(string FileName, long startFrom)
        {
			try
			{
				BamFileName = FileName;
				decom = new Decompressor(FileName);
				decom.SetFile(FileName, startFrom);
				pos = int.MaxValue;
			}
			catch(Exception ex)
			{ throw new Exception("File open"); }
        }

		public void Dispose()
        {
			decom.Dispose(); 
        }
		public List<string> getHeader()
		{			
			int hs = headerText.Count;
			List<string> headers = new List<string>();
			for (int index = 0; index < hs; index++)
			{
				headers.Add(getHeaderLine(index) + "\n");
			}
			
			return headers;
		}

		public string getHeaderLine(int index)
		{
			if (headerText.ContainsKey(index) == true)
			{
				return headerText[index];
			}
			else
			{
				throw new Exception("Index out of range.");
			}
		}

		public int getReferenceTextSize()
		{
			return referenceText.Count();
		}

		public string getReferenceTextLine(int index)
		{
			if (referenceText.ContainsKey(index) ==true)
			{
				return referenceText[index];
			}
			else
			{
				throw new Exception("Index out of range.");
			}
		}

		public string NextAlignedRead(bool includeQualString, string[] referencenames)
		{
			answer = "";
			try
			{
				int blockSize = getInt32();                         //0		0
				byte[] c = new byte[blockSize];
				if (blockSize == 0 || decom.read(c, blockSize) == -1)
				{
					c = null; ;
					return answer;
				}
				int startIndex = 0;
				redID = getInt32(c, startIndex);                    //4		4	
				startIndex += 4;
				pos = getInt32(c, startIndex);                      //4		8
				startIndex += 4;
				l_ReadName = getInt8(c, startIndex);                //1		9
				startIndex += 1;
				maq = getInt8(c, startIndex);                       //1		10
				startIndex += 1;
				int bin = getInt16(c, startIndex);                  //2		12	
				startIndex += 2;
				int cigarN = getInt16(c, startIndex);               //2		14
				startIndex += 2;
				flag = getInt16(c, startIndex);                     //2		16
				startIndex += 2;
				l_seq = getInt32(c, startIndex);                    //4		20
				startIndex += 4;
				next_refID = getInt32(c, startIndex);               //4		24
				startIndex += 4;
				next_pos = getInt32(c, startIndex);                 //4		28
				startIndex += 4;
				tlen = getInt32(c, startIndex);                     //4		32 
				startIndex += 4;
				read_Name = getString(c, startIndex, l_ReadName);
				startIndex += l_ReadName;
				cigar = getCigar(c, startIndex, cigarN);
				startIndex += (4 * cigarN);
				seq = getSequence(c, startIndex, l_seq);
				startIndex += ((l_seq + 1) / 2);
				if (includeQualString == true)
				{ qual = getQualityString(c, startIndex, l_seq); }
				startIndex += l_seq;
				tags = getTag(c, startIndex, blockSize);

				//List<string> bits1 = new List<string>();
				//if (redID > -1 && referencenames.Count() > 0)
				//{
				//	string test = referencenames[redID];
				//	bits1.AddRange(test.Split('\t'));
				//	chr = bits1[0];
				//}
				//else if (redID > -1)
				//{
				//	chr = redID.ToString();
				//	bits1.Add(chr);
				//}
				//else
				//{
				//	bits1.Add("*");
				//	chr = "*";
				//}

				//if (next_refID > -1 && referencenames.Count() > 0)
				//{
				//	List<string> bits2 = new List<string>();
				//	string test = referencenames[next_refID];
				//	bits2.AddRange(test.Split('\t'));
				//	otherChr = "";
				//	if (bits1[0].CompareTo(bits2[0]) == 0)
				//	{
				//		otherChr = "=";
				//	}
				//	else
				//	{
				//		otherChr = bits2[0];
				//	}
				//}
				//else if (next_refID > -1)
				//{
				//	if (redID == next_refID)
				//	{
				//		otherChr = "=";
				//	}
				//	else
				//	{
				//		otherChr = next_refID.ToString();
				//	}
				//}
				//else
				//{
				//	otherChr = "*";
				//}

				answer = read_Name + "\t" + flag.ToString() + "\t" + redID.ToString() + "\t"
					+pos.ToString() + "\t" + maq.ToString() + "\t" + cigar + "\t" + next_refID.ToString() + "\t"
					+ next_pos.ToString() + "\t" + tlen.ToString() + "\t" + seq + "\t" + qual + "\t" + tags + "\n";

				
			}
			catch (Exception ex)
			{ }
			return answer;
		}
		private string getString(byte[] c)
		{
			string answer = "";
			foreach (byte b in c)
			{ answer += (char)b; }
			return answer;
		}

		private string getString(byte[] c, int length)
		{
			string answer = "";
			for (int index =0;index< length; index++)
			{ answer += (char)c[index]; }
			return answer;
		}

		private string getString(int length)
		{
			string answer = "";
			byte[] c = new byte[length];
			decom.read(c, length);

			for (int index = 0; index < length; index++)
			{ answer += (char)c[index]; }
			return answer;
		}

	private string getString(byte[] c, int startIndex, int length)
		{
			string answer = "";

			for (int index = 0; index < length; index++)
			{
				if (c[index + startIndex] != 0)
				{
					answer += (char)c[index + startIndex];
				}
			}

			return answer;
		}

		private string getOneByteString(byte[] c, int startIndex)
		{
			char C = (char)c[startIndex];
			string answer = "";
			answer += C;
			return answer;
		}

		private string getQualityString(int length)
		{
			StringBuilder answer = new StringBuilder(length);
			byte[] c = new byte[length];
			decom.read(c, length);

			for (int index = 0; index < length; index++)
			{
				answer.Append((char)(c[index] + 33));
			}
			
			return answer.ToString();
		}

		private string getQualityString(byte[] c, int startIndex, int length)
		{
			StringBuilder answer = new StringBuilder(length);

			for (int index = 0; index < length; index++)
			{
				answer.Append((char)(c[index + startIndex] + 33));
			}

			return answer.ToString();
		}

		private string getCigar(int cycles)
		{
			string[] cigis = { "M", "I", "D", "N", "S", "H", "P", "=", "X" };
			StringBuilder answer = new StringBuilder();
			int length = 0;
			for (int index = 0; index < cycles; index++)
			{
				length = getInt32();
				int a1 = length >> 4;
				int a2 = length - (a1 << 4);
				answer.Append(a1.ToString() + cigis[a2]);
			}
			return answer.ToString();
		}

		private string getCigar(byte[] c, int startIndex, int cycles)
		{
			string[] cigis = { "M", "I", "D", "N", "S", "H", "P", "=", "X" };
			StringBuilder answer =new StringBuilder();
			int length = 0;
			for (int index = 0; index < cycles; index++)
			{
				//length = getunInt32();
				length = getInt32(c, startIndex);
				startIndex += 4;
				int a1 = length >> 4;
				int a2 = length - (a1 << 4);
				answer.Append(a1.ToString() + cigis[a2]);
			}

			if (answer.Length == 0)
			{
				answer.Clear();
				answer.Append("0");
			}

			return answer.ToString();
		}

		private string getSequence(int length)
		{
			string[] bp = { "", "A", "C", "M", "G", "R", "S", "V", "T", "W", "Y", "H", "K", "D", "B", "N" };
			StringBuilder answer = new StringBuilder(length);
			length = (length + 1) / 2;
			int a1 = 0;
			int a2 = 0;
			 int value = 0;
			for (int index = 0; index < length; index++)
			{
				value = getunInt8();
				a1 = value >> 4;
				a2 = value - (a1 << 4);
				answer.Append(bp[a1] + bp[a2]);
			}

			return answer.ToString();
		}

		private string getSequence(byte[] c, int startIndex, int length)
		{
			string[] bp = { "", "A", "C", "M", "G", "R", "S", "V", "T", "W", "Y", "H", "K", "D", "B", "N" };
			StringBuilder answer = new StringBuilder(length);
			length = (length + 1) / 2;
			int a1 = 0;
			int a2 = 0;
			 int value = 0;
			for (int index = 0; index < length; index++)
			{
				value = c[startIndex];
				startIndex += 1; ;
				a1 = value >> 4;
				a2 = value - (a1 << 4);
				answer.Append(bp[a1] + bp[a2]);
			}

			return answer.ToString();
		}

		private string getTag(byte[] c, int startIndex, int length)
		{
			string currentTag = "";

			string answer = "";
			while (startIndex < length)
			{
				currentTag = getString(c, startIndex, 2);
				startIndex += 2;
				string v_type = getString(c, startIndex, 1);
				startIndex += 1;
				if (v_type.CompareTo("c") == 0)
				{
					int i = getInt8(c, startIndex);
					startIndex += 1;
					answer += currentTag + ":i:" + i.ToString();
				}
				else if (v_type.CompareTo("C") == 0)
				{
					int i = getunInt8(c, startIndex);
					startIndex += 1;
					answer += currentTag + ":i:" + i.ToString();
				}
				else if (v_type.CompareTo("s") == 0)
				{
					int i = getInt16(c, startIndex);
					startIndex += 2;
					answer += currentTag + ":i:" + i.ToString();
				}
				else if (v_type.CompareTo("S") == 0)
				{
					int i = getunInt16(c, startIndex);
					startIndex += 2;
					answer += currentTag + ":i:" + i.ToString();
				}
				else if (v_type.CompareTo("i") == 0)
				{
					int i = getInt32(c, startIndex);
					startIndex += 4;
					answer += currentTag + ":i:" + i.ToString();
				}
				else if (v_type.CompareTo("I") == 0)
				{
					int i = (int)getunInt32(c, startIndex);
					startIndex += 4;
					answer += currentTag + ":i:" + i.ToString();
				}
				else if (v_type.CompareTo("A") == 0)
				{
					string s = getOneByteString(c, startIndex);
					startIndex += 1;
					answer += currentTag + ":A:" + s;
				}
				else if (v_type.CompareTo("f") == 0)
				{
					float f = getfloat(c, startIndex);
					startIndex += 4;
					answer += currentTag + ":f:" + f.ToString();
				}
				else if (v_type.CompareTo("Z") == 0)
				{
					string s = getString(c, startIndex, 1);
					startIndex += 1;
					answer += currentTag + ":Z:" + s;
					while (s.Length > 0)
					{
						s = getString(c, startIndex, 1);
						startIndex += 1;
						answer += s;
					}
				}
				else if (v_type.CompareTo("H") == 0)
				{
				}
				else if (v_type.CompareTo("B") == 0)
				{
					string v_type_local = getString(c, startIndex, 1);
					startIndex += 1;
					int count = getInt32(c, startIndex);//461
					startIndex += 4;
					answer += currentTag + ":f:" + v_type_local;
					switch (v_type_local[0])
					{
						case 'c':
						case 'C':
							for (int index = 0; index < count; index++)
							{
								int i = getunInt8(c, startIndex);
								startIndex += 1;
								answer += "," + i.ToString();
							}
							break;

						case 's':
						case 'S':
							for (int index = 0; index < count; index++)
							{
								int i = getInt16(c, startIndex);
								startIndex += 2;
								answer += "," + i.ToString();
							}
							break;

						case 'i':
						case 'I':
							for (int index = 0; index < count; index++)
							{
								int i = getInt32(c, startIndex);
								startIndex += 4;
								answer += "," + i.ToString();
							}
							break;
						case 'f':
							for (int index = 0; index < count; index++)
							{
								float f = getfloat(c, startIndex);
								startIndex += 4;
								answer += "," +f.ToString();
							}
							break;
					}


				}
				else
				{
					//quit in confusion but safely
					startIndex = length;

				}
				if (length > 0) { answer += "\t"; }
			}


			return answer;
		}

		private Int32 getInt8()
        {
			byte[] c = new byte[1];
			decom.read(c, 1);

			UInt32 answer = (UInt32)c[0];
			return (Int32)answer;
        }

		private int getInt8(byte[] c, int startIndex)
		{
			UInt32 answer = (UInt32)c[startIndex];
			return (Int32)answer;
		}

		private int getunInt8()
		{
			byte[] c = new byte[1];
			decom.read(c, 1);

			//int answer = c[0] + (c[1] << 8);
			int answer = c[0] ;

			return answer;
		}

		private int getunInt8(byte[] c, int startIndex)
		{
			int answer = (UInt16)c[startIndex];
			return answer;
		}

		private Int32 getInt16()
		{
			byte[] c = new byte[2];
			decom.read(c, 2);

			int answer = BitConverter.ToUInt16(c, 0);
			return answer;
		}

		private Int32 getInt16(byte[] c, int startIndex)
		{
			Int32 answer = BitConverter.ToInt16(c, startIndex);
			return answer;
		}

		private UInt16 getunInt16()
		{
			byte[] c = new byte[2];
			decom.read(c, 2);

			UInt16 answer = BitConverter.ToUInt16(c, 0);
			return answer;
		}

		private UInt16 getunInt16(byte[] c, int startIndex)
		{
			UInt16 answer = BitConverter.ToUInt16(c, startIndex); 
			return answer;
		}

		private Int32 getInt32()
		{
			byte[] c = new byte[4];
			decom.read(c, 4);

			int answer = BitConverter.ToInt32(c, 0);
			return answer;
		}

		private Int32 getInt32(byte[] c, int startIndex)
		{
			Int32 answer = BitConverter.ToInt32(c, startIndex);
			return answer;
		}

		private UInt32 getunInt32()
		{
			byte[] c = new byte[4];
			decom.read(c, 4);

			UInt32 answer = BitConverter.ToUInt32(c, 0);
			return answer;
		}

		private UInt32 getunInt32(byte[] c, int startIndex)
		{

			UInt32 answer = BitConverter.ToUInt32(c, startIndex);
			return answer;
		}

		private float getfloat(byte[] c, int startIndex)
		{
			float answer = BitConverter.ToSingle(c, startIndex);
			
			return answer;
		}
	}
}
