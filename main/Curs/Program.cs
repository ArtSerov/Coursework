using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cours;

namespace Curs
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //List<string> fam = InputOutput.ReadWaterFamilyData("C:\\Users\\ТЕМА\\Desktop\\Course-work\\Course-work\\Coursework\\main\\family.csv");
            //List<FamilyOfAnimals> allfam = new List<FamilyOfAnimals>();
            List<Complex> newcom = InputOutput.ReadComplexData("C:\\Users\\ТЕМА\\Desktop\\Course-work\\Course-work\\Coursework\\main\\ComplexData.csv");
            //List<TypeOfAnimal> animals = InputOutput.ReadAnimalsData("C:\\Users\\ТЕМА\\Desktop\\Course-work\\Course-work\\Coursework\\main\\Animals.csv", fam, ref allfam);
            
            #region animals
            List<TypeOfAnimal> animals = new List<TypeOfAnimal>
            {
                new TypeOfAnimal("Волосатая лягушка", 2, "Африка", true, "Пискуньи"),
                new TypeOfAnimal("Красноглазая квакша", 3, "Австралия", true, "Квакши"),
                new TypeOfAnimal("Кряква", 5, "Евразия", true, "Утиные")
            };
            #endregion

            Resettlement.AddInComplex(newcom, animals);
            DatedLogger datlog = new DatedLogger(Console.Out);


            SaveManager SMComplex = new SaveManager("complex.csv");
            foreach (var x in newcom)
            {
                SMComplex.WriteObject(x);
            }
            Console.WriteLine("\n");
            SaveManager SMAnimals = new SaveManager("animal.csv");
            foreach (var x in animals)
            {
                SMAnimals.WriteObject(x);
            }

            LoadManager loader1 = new LoadManager("animal.csv");
            LoadLogger loadLog1 = new LoadLogger(loader1,datlog);

            List<TypeOfAnimal> loadAni = new List<TypeOfAnimal>();
            loader1.BeginRead();
            while (loader1.IsLoading)
                loadAni.Add(loader1.Read(new TypeOfAnimal.Loader()) as TypeOfAnimal);
            loader1.EndRead();
            Console.WriteLine("\n");
            Console.ReadKey();
            LoadManager loader = new LoadManager("complex.csv");
            LoadLogger loadLog2 = new LoadLogger(loader, datlog);
            List<Complex> load = new List<Complex>();
            loader.BeginRead();
            while (loader.IsLoading)
                load.Add(loader.Read(new Complex.Loader()) as Complex);
            loader.EndRead();
            





            #region
            // 

            // Resettlement.AddInComplex(newcom, animals);                 //Авто добавление ТОФ в коммплексы с учетом тепла и воды 1
            // InputOutput.СonsoleAddAni(ref animals,fam, ref allfam);   //Пользовательское добавление  вида и семейства животного 3
            // InputOutput.СonsoleAddComplex(ref newcom);                //Создание комплекса с консоли 4
            // Resettlement.AddTOFInComplex(newcom,animals);             //Добавление в комплекс вида животного который может там проживать ТРАКЕЧ 5
            // Resettlement.AddTOFInComplex(newcom, animals);             //Добавление в комплекс вида животного который может там проживать ТРАКЕЧ 5
            // Resettlement.AddAnimalsInComplex(newcom,animals);           //Поселение введенного животного с выбором комплекса ТРАКЕЧ 6
            // Resettlement.AddAnimalsInComplex(newcom, animals);
            // Resettlement.AddAnimalsInComplex(newcom, animals);
            // Resettlement.DelAnimalsInComplex(newcom);                   //Высиление животного из комплекса
            // InputOutput.PopulationFamily(allfam);                       //Вывод численности популяции семейства
            // InputOutput.ResultsFile(newcom);                            //Запись в файл информации про комплексы

            // 
            #endregion
            Console.ReadKey();

        }
    }
}