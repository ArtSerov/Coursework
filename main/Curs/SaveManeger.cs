﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Curs;
namespace Curs
{
    interface ISaveManager
    {
        void WriteLine(string line);
        void WriteObject(IWritableObject obj);
    }

    public interface IWritableObject
    {
        void Write(SaveManager man);
    }
    public class SaveManager:ISaveManager
    {
        FileInfo file;

        public  SaveManager(string filename)
        {
            file = new FileInfo(filename);
            file.CreateText().Close();
        }

        public void WriteLine(string line)
        {
            StreamWriter output = file.AppendText();
            output.WriteLine(line);
            output.Close();
        }
        public void WriteObject(IWritableObject obj)
        {
            obj.Write(this);
        }
    }
}
