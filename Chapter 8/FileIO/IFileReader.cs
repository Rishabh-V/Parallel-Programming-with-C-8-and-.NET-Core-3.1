using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileIO
{
    public interface IFileReader
    {
        StreamReader GetFileReader(string filePath);
    }
}
