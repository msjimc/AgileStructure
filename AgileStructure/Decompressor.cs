using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Compression;

namespace AgileStructure
{
	class Decompressor
	{
		
		bool endofFile = false;		
		System.IO.FileStream? currentFile;
		GZipStream? decompressor;

		int positionInChunk;

		List<byte>? dataArray;


		long placeinfile;
		int bytesread = 0;

		public Decompressor()
		{  }

		public Decompressor(string FileName)
		{						
			positionInChunk = 0;
			currentFile = System.IO.File.Open(FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);			
			decompressor = new GZipStream(currentFile, CompressionMode.Decompress, true);
			endofFile = false;
		}

		public void Dispose()
        {
			if (decompressor != null) { decompressor.Close(); }
			if (currentFile != null) { currentFile.Close(); }
			currentFile = null;
        }
		public int read(byte[] c, int length)
		{
			if (dataArray == null || bytesread == 0)
			{
				GetNextChunk();
				if (dataArray == null || dataArray.Count() == 0)
				{ return -1; }
			}

			if (positionInChunk + length > bytesread)
			{
				int placeInC = 0;
				int index = positionInChunk;
				while (placeInC < length)
				{
					if (index == bytesread)
					{
						GetNextChunk();
						if (bytesread == 0)
						{ return -1; }
						index = 0;
					}
					c[placeInC] = dataArray[index];
					index++;
					placeInC++;
				}
				positionInChunk = index;
			}
			else
			{
				for (int index = 0; index < length; index++)
				{
					c[index] = dataArray[index + positionInChunk];
				}
				positionInChunk += length;
			}
			return 0;
		}

		public byte[] read(int length)
		{
			byte[] c = new byte[length];

			if (positionInChunk + length > bytesread && dataArray != null)
			{
				int placeInC = 0;
				int index = positionInChunk;
				while (placeInC < length)
				{
					if (index == dataArray.Count())
					{
						GetNextChunk();
						index = 0;
					}
					c[placeInC] = dataArray[index];
					index++;
					placeInC++;
				}
				positionInChunk = index;
			}
			else if (dataArray != null)
			{
				for (int index = 0; index < length; index++)
				{
					c[index] = dataArray[index + positionInChunk];
				}
				positionInChunk += length;
			}
			return c;
		}

		private void GetNextChunk()
		{
			if (decompressor == null) { return; }
			if (endofFile == true)
			{
				if (dataArray != null) { dataArray.Clear(); }
				return;
			}

			byte[] data = new byte[32000];
			
			bytesread = 0;
            int thisTime = 0;
            while ((thisTime = decompressor.Read(data, bytesread, data.Length - bytesread)) > 0)
            {
                bytesread += thisTime;
            }

			dataArray = new List<byte>();

			dataArray.AddRange(data);
		}

		public void SetFile(string FileName)
		{
			if (currentFile !=null)
			{
				currentFile.Close();
				currentFile = null;
			}		

			if (currentFile == null)
			{
				currentFile = System.IO.File.Open(FileName, System.IO.FileMode.Open);
				decompressor = new GZipStream(currentFile, CompressionMode.Decompress);
			}
			placeinfile = 0;
		}

		public void SetFile(string FileName, long startFrom)
		{		
			positionInChunk = 0;
			if (currentFile == null)
			{
				currentFile = System.IO.File.Open(FileName, System.IO.FileMode.Open);
				decompressor = new GZipStream(currentFile, CompressionMode.Decompress);
			}
				placeinfile = startFrom;
			currentFile.Position = startFrom;
		}

		public bool ReturnValue
		{
			get { return endofFile; }

		}
	}
}

