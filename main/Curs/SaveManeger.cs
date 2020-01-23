using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Curs
{
    interface IWritableObject
    {
        void Write(SaveManeger man);
    }
    public class SaveManeger
    {
        FileInfo file;
        public void SaveManager(string filename)
        {
            file = new FileInfo(filename);
            file.CreateText();
        }

        public void WriteLine(string line)
        {
            StreamWriter output = file.AppendText();
            output.WriteLine(line);
            output.Close();
        }
        void WriteObject(IWritableObject obj)
        {
            obj.Write(this);
        }
    }
}
