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
        public event EventHandler<IReadbleObject> ObjectDidLoad;
        public event EventHandler<FileInfo> DidStartLoad;
        public event EventHandler<FileInfo> DidEndLoad;

        public LoadManager(string filename)
         {
               file = new FileInfo(filename);
               input = null;
         }

        public IReadbleObject Read(IReadableObjectLoader loader)
        {
            var result = loader.Load(this);
            if (ObjectDidLoad != null)
                ObjectDidLoad.Invoke(this, result);
            return result;
            }

        public void BeginRead()
            {
                if (input != null)
                    throw new IOException("Load Error");
            if (DidStartLoad != null)
                DidStartLoad.Invoke(this, file);
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
            if (DidEndLoad != null)
                DidEndLoad.Invoke(this, file);

            input.Close();
            }
        }
    
}
