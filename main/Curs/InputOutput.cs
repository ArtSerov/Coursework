using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework;
namespace Curs
{
    class InputOutput
    {
        public static List<Complex> ReadComplexData(string journal)
        {
            List<Complex> сomplexes = new List<Complex>();
            StreamReader sr = new StreamReader(journal);
            int s = 0;
            while (!sr.EndOfStream)
            {
                string[] values = sr.ReadLine().Split(';');
                сomplexes.Add(new Complex(values[0], int.Parse(values[1])));
                сomplexes[s].Water(values[2]);
                сomplexes[s].Heat(values[3]);
                s++;
            }
            sr.Close();
            
            return сomplexes;
        }


        public static List<string> ReadWaterFamilyData(string journal)
        {
            List<string> fam = new List<string>();
            StreamReader sr = new StreamReader(journal);
            while (!sr.EndOfStream)
            {
                string[] values = sr.ReadLine().Split(';');
                
                fam.Add(values[0]);
            }
            sr.Close();
            return fam;
        }

        public static List<TypeOfAnimal> ReadAnimalsData(string journal, List<string>  waterfam,ref List<FamilyOfAnimals> allfam)
        {
            List<TypeOfAnimal> animals = new List<TypeOfAnimal>();
            StreamReader sr = new StreamReader(journal);
            int s = 0;
            while (!sr.EndOfStream)
            {
                int flag=-1;
                int c = 0;
                string[] values = sr.ReadLine().Split(';');
                foreach(var x in allfam)
                {
                    if (values[0] == x.getfamily_name) flag = c;
                    c++;
                }
                if (flag == -1)
                {
                    FamilyOfAnimals family = new FamilyOfAnimals(values[0]);
                    allfam.Add(family);
                    animals.Add(new TypeOfAnimal(family, values[1], float.Parse(values[2]), values[3]));
                    animals[s].NeedInWater(waterfam);
                }
                else
                {
                    animals.Add(new TypeOfAnimal(allfam[flag], values[1], float.Parse(values[2]), values[3]));
                    animals[s].NeedInWater(waterfam);
                }
                s++;
            }
            sr.Close();
            return animals;
        }


        public static void СonsoleAddAni(ref List<TypeOfAnimal> animals, List<string> waterfam, ref List<FamilyOfAnimals> allfam)
        {
            Console.WriteLine("****************************\n");
            Console.WriteLine("Добавление семейства и вида животного в БД зоопарка");
            string a, b, c;
            float d;
            Console.WriteLine("Введите название семейства\n");
            a = Console.ReadLine();
            Console.WriteLine("Введите название вида \n");
            b = Console.ReadLine();
            Console.WriteLine("Введите название континента обитания\n");
            c = Console.ReadLine();
            Console.WriteLine("Введите кол-во корма\n");
            d = float.Parse(Console.ReadLine());
            int flag = -1;
            int s = 0;
            foreach (var x in allfam)
            {
                if (a == x.getfamily_name) flag = s;
                s++;
            }
            if (flag == -1)
            {
                FamilyOfAnimals family = new FamilyOfAnimals(a);
                allfam.Add(family);
                TypeOfAnimal ani = new TypeOfAnimal(family, b, d, c);
                ani.NeedInWater(waterfam);
                animals.Add(ani);
            }
            else
            {
                TypeOfAnimal ani = new TypeOfAnimal(allfam[flag], b, d, c);
                ani.NeedInWater(waterfam);
                animals.Add(ani);
            }
        }


        public static void СonsoleAddComplex(ref List<Complex> сomplexes)
        {
            Console.WriteLine("****************************\n");
            Console.WriteLine("Создание нового комплекса");
            int x = сomplexes.Count;
            string a, b, c;
            int ix;
            Console.WriteLine("Введите название комплекса\n");
            a = Console.ReadLine();
            Console.WriteLine("Введите номер помещения\n");
            for (; ; )
            {
                ix = Int32.Parse(Console.ReadLine());
                if (ix > 0) break;
                else Console.WriteLine("Ошибка ввода");
            }
            for (;;){
                Console.WriteLine("Есть ли в комплексе водоем (есть/нет) \n");
                b = Console.ReadLine();
                b.ToLower();
                if (b == "есть" || b=="нет") break;
            }
            for (;;){
                Console.WriteLine("Есть ли в комплексе отопление (есть/нет) \n");
                c = Console.ReadLine();
                c.ToLower();
                if ( c == "есть" || c == "нет")break ;
            } 
            сomplexes.Add(new Complex(a, ix));
            сomplexes[x].Water(b+" водоем");
            сomplexes[x].Heat(c+" отопление");
        }

        public static void PopulationFamily(List<FamilyOfAnimals> allfam)
        {
            bool flag = false;
            Console.WriteLine("******************************************");
            Console.WriteLine("Семейства жиаотных проживающих в зоопарке");
            foreach (var x in allfam)
                Console.WriteLine(x.getfamily_name);
            for (; ; )
            {
                Console.WriteLine("Видите название семейства популяцию которого Вы хотите узнать");
                string fam = Console.ReadLine();

                foreach (var x in allfam)
                {
                    if (x.getfamily_name == fam)
                    {
                        flag = true;
                        Console.WriteLine($"В зоопарке {x.getpop} животных этого семейства");
                    }
                }
                if (flag == false) Console.WriteLine("Семейство введено не правильно или его нет в зоопарке");
                else break;
            }
        }
        //ВЫВОД
        public static void ResultsFile(List<Complex> complices)
          {
            for (int i = 0; i < complices.Count; i++)
            {
                
                StreamWriter sw = new StreamWriter(complices[i] + ".csv", false, Encoding.UTF8);
                sw.WriteLine("Животных в комплексе: " + complices[i].animals_in_room + ";");
                sw.WriteLine("Корм для комплеска: " + complices[i].daily_feed + ";");
                foreach (var x in complices[i].presents)
                {
                    if (x.Value == 0) continue;
                    sw.WriteLine(x.Key + " в количестве "+ x.Value);
                }
                sw.Close();
            }
          }
          

    }
}
