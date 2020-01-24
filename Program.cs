using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveLoadManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Student s = new Student("Testov T.T.", "TEST-00");
            //
            SaveManager saver = new SaveManager("test.txt");
            saver.WriteObject(s);
            //
            LoadManager loader = new LoadManager("test.txt");
            List<Student> sList = new List<Student>();
            loader.BeginRead();
            while (loader.IsLoading)
                sList.Add(loader.Read(new Student.Loader()) as Student);
            loader.EndRead();
            //
            foreach(Student st in sList)
                Console.WriteLine(st);
            Console.ReadKey();
        }
    }
}
