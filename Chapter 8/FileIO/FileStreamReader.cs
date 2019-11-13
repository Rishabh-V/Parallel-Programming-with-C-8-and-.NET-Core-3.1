using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileIO
{
    public class FileStreamReader : IFileReader
    {
        public StreamReader GetFileReader(string filePath)
        {
            return new StreamReader(filePath);
        }
    }
}
