using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Cours;

namespace Curs
{
 
        public interface ILoadManager
        {
            string ReadLine();
            IReadbleObject Read(IReadableObjectLoader loader);
        }

        public interface IReadbleObject
        { }

        public interface IReadableObjectLoader
        {
            IReadbleObject Load(ILoadManager man);
        }
        
        public class LoadManager : ILoadManager
        {
            FileInfo file;
            StreamReader input;
            public LoadManager(string filename)
            {
                file = new FileInfo(filename);
                input = null;
            }

            public IReadbleObject Read(IReadableObjectLoader loader)
            {
                return loader.Load(this);
            }

            public void BeginRead()
            {
                if (input != null)
                    throw new IOException("Load Error");

                input = file.OpenText();
            }
            public bool IsLoading
            {
                get { return input != null && !input.EndOfStream; }
            }
            public string ReadLine()
            {
                if (input == null)
                    throw new IOException("Load Error");

                string line = input.ReadLine();
                return line;
            }

            public void EndRead()
            {
                if (input == null)
                    throw new IOException("Load Error");

                input.Close();
            }
        }
    
}
