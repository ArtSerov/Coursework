using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveLoadManager
{
    class Student: IReadbleObject, IWritableObject
    {
        string name;
        string group;

        public Student(string name, string group)
        {
            this.name = name;
            this.group = group;
        }

        private Student(ILoadManager man)
        {
            name = man.ReadLine().Split(':')[1];
            group = man.ReadLine().Split(':')[1];
        }

        public void Write(ISaveManager man)
        {
            man.WriteLine($"name:{name}");
            man.WriteLine($"group:{group}");
        }

        public class Loader : IReadableObjectLoader
        {
            public Loader() {}
            public IReadbleObject Load(ILoadManager man)
            {
                return new Student(man);
            }
        }
    }
}

