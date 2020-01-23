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

            List<string> fam = InputOutput.ReadWaterFamilyData("family.csv");
            List<FamilyOfAnimals> allfam = new List<FamilyOfAnimals>();
            List<Complex> newcom = InputOutput.ReadComplexData("ComplexData.csv");
            List<TypeOfAnimal> animals = InputOutput.ReadAnimalsData("Animals.csv",fam, ref allfam);


            Resettlement.AddInComplex(newcom, animals);                 //Авто добавление ТОФ в коммплексы с учетом тепла и воды 1
            //InputOutput.СonsoleAddAni(ref animals,fam, ref allfam);   //Пользовательское добавление  вида и семейства животного 3
            //InputOutput.СonsoleAddComplex(ref newcom);                //Создание комплекса с консоли 4
           // Resettlement.AddTOFInComplex(newcom,animals);             //Добавление в комплекс вида животного который может там проживать ТРАКЕЧ 5
           // Resettlement.AddTOFInComplex(newcom, animals);             //Добавление в комплекс вида животного который может там проживать ТРАКЕЧ 5
            Resettlement.AddAnimalsInComplex(newcom,animals);           //Поселение введенного животного с выбором комплекса ТРАКЕЧ 6
           // Resettlement.AddAnimalsInComplex(newcom, animals);
           // Resettlement.AddAnimalsInComplex(newcom, animals);
           // Resettlement.DelAnimalsInComplex(newcom);                   //Высиление животного из комплекса
           // InputOutput.PopulationFamily(allfam);                       //Вывод численности популяции семейства
            InputOutput.ResultsFile(newcom);                            //Запись в файл информации про комплексы

            
            Console.ReadKey();

        }
    }
}