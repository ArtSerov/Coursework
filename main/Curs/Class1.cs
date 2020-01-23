using Curs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cours

{

    public class FamilyOfAnimals: IWritableObject
    {
        private string family_name;
        private int Population;

        public FamilyOfAnimals() { }
        public FamilyOfAnimals(string fam_name)
        {
            family_name = fam_name;
        }
        public int getpop { get { return Population; } }
        public string getfamily_name { get { return family_name; } }
        public void SetFN(string name) { family_name = name; }
        public void SetPop(int pop) { Population += pop; }

        public void Write(SaveManeger man)
        {
            man.WriteLine($"family name: {family_name}");
            man.WriteLine($"population: {Population}");
        }
    }

    public class TypeOfAnimal: IWritableObject
    {
        private readonly string Name;
        private readonly float daily_feed_intake;
        private readonly string continent_habitat;
        private bool Reservoir;
        private bool Heat;
        FamilyOfAnimals family;

        public TypeOfAnimal(FamilyOfAnimals family, string Name, float daily_feed_intake, string continent_habitat)
        {
            this.family = family;
            this.Name = Name;
            this.daily_feed_intake = daily_feed_intake;
            this.continent_habitat = continent_habitat;
            if (continent_habitat == "Южная Америка" || continent_habitat == "Африка" || continent_habitat == "Австралия") Heat = true;
            else Heat = false;
        }

        public void NeedInWater(List<string> waterfam)
        {
            for (int i = 0; i < waterfam.Count; i++)
            {
                if (family.getfamily_name == waterfam[i]) Reservoir = true;
            }
        }
        public void SetPop(int pop) { family.SetPop(pop); }
        public int GetPop { get { return family.getpop; } }
        public string GetName { get { return Name; } }
        public float GetDFI { get { return daily_feed_intake; } }
        public string GetContinent { get { return continent_habitat; } }
        public bool GetReservoir { get { return Reservoir; } }
        public bool GetHeat { get { return Heat; } }
        public void SetReservoir(bool x) { Reservoir = x; }
        public void SetHeat(bool x) { Heat = x; }

        public override string ToString()
        {
            return Name + " " + " из семейства " + family.getfamily_name;
        }

        public void Write(SaveManeger man)
        {
            man.WriteLine($"Name: {Name}");
            man.WriteLine($"Continent habitat: {continent_habitat}");
        }
    }
    public class Complex: IWritableObject
    {

        private readonly string name_complex;
        private readonly int room_number;
        private bool availability_water;
        private bool presence_heat;
        public Dictionary<TypeOfAnimal, int> presents;
        public int animals_in_room;
        public float daily_feed;

        public Complex(string name_complex, int room_number)
        {
            this.name_complex = name_complex;
            this.room_number = room_number;
            presents = new Dictionary<TypeOfAnimal, int>();
            Console.WriteLine("Criete new complex");
        }
        public Complex(string name_complex, int room_number, string water, string heat)
        {
            this.name_complex = name_complex;
            this.room_number = room_number;
            if (water == "есть водоем")
                availability_water = true;
            else availability_water = false;
            if (heat == "есть отопление")
                presence_heat = true;
            else presence_heat = false;

        }

        public void adding(TypeOfAnimal animals)
        {
            presents.Add(animals, 0);
        }
        public void adding(List<TypeOfAnimal> animals)
        {
            bool flag = false;
            bool x = false;
            Console.WriteLine("\nВиды животных условия проживания которых совпадают с условиями комплекса");
            for (int i = 0; i < animals.Count; i++)
            {
                if (animals[i].GetHeat == presence_heat && animals[i].GetReservoir == availability_water)
                {
                    Console.WriteLine(animals[i].GetName);
                    flag = true;
                }

            }
            if (flag == false)
            {
                Console.WriteLine("Нет животных которых можно было бы добавить в этот комплекс");
                return;

            }
            Console.WriteLine("Введите вид животных который можно будет поселить в комплекс");
            string type = Console.ReadLine();
            foreach (var key in animals)
            {
                if (type.Equals(key.GetName)) x = true;
            }
            if (x == false)
            {
                Console.WriteLine("Ошибка ввода");
                return;
            }
            for (int i = 0; i < animals.Count; i++)
            {
                try
                {
                    if (type == animals[i].GetName)
                    {
                        presents.Add(animals[i], 0);
                        Console.WriteLine($"Теперь {animals[i]} можно будет поселить в комплекс {name_complex}");
                        flag = true;
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Животное этого вида уже может проживать в этом комплексе");
                    return;

                }
            }
        }
        public void AddAni(string typeofani, int kol_vo, ref bool flag)
        {
            for (int i = 0; i < presents.Count; i++)
            {
                var key = presents.ElementAt(i).Key;
                if (key.GetName == typeofani)
                {
                    presents[key] += kol_vo;
                    daily_feed += key.GetDFI * kol_vo;
                    animals_in_room += kol_vo;
                    key.SetPop(kol_vo);
                    Console.WriteLine($"Животное из семейства {key.GetName} добавленно в {name_complex} комплекс в количестве {kol_vo}");
                    flag = true;
                }
            }
        }
        public void DelAni(string typeofani, int kol_vo)
        {
            for (int i = 0; i < presents.Count; i++)
            {
                var key = presents.ElementAt(i).Key;
                if (key.GetName == typeofani)
                {
                    presents[key] -= kol_vo;
                    daily_feed -= key.GetDFI;
                    animals_in_room -= kol_vo;
                    key.SetPop(-kol_vo);
                    Console.WriteLine($"{kol_vo} животное из семейства {key.GetName} выселено из комплекса {name_complex}");

                }
            }
        }
        public void Water(string water)
        {
            if (water == "есть водоем")
                availability_water = true;
            else availability_water = false;
        }
        public void Heat(string heat)
        {
            if (heat == "есть отопление")
                presence_heat = true;
            else presence_heat = false;
        }
        public bool GetWater { get { return availability_water; } }
        public bool GetHeat { get { return presence_heat; } }
        public override string ToString()
        {
            return "комплекс " + name_complex + " " + room_number;
        }
        public void WriteData()
        {

            Console.WriteLine($"\n В комплексе {name_complex + " " + room_number} находиться");
            for (int i = 0; i < presents.Count; i++)
            {
                if (presents.ElementAt(i).Value > 0)
                    Console.WriteLine($"{presents.ElementAt(i).Key.ToString()} в количестве { presents.ElementAt(i).Value} ");
            }
        }

        public void Write(SaveManeger man)
        {
            man.WriteLine($"Name complex: { name_complex}");
            man.WriteLine($"room number: {room_number}");
        }
    }

    public class Resettlement
    {

        public static void AddInComplex(List<Complex> complices, List<TypeOfAnimal> animals)
        {
            for (int i = 0; i < animals.Count; i++)
            {
                for (int j = 0; j < complices.Count; j++)
                {
                    if (complices[j].GetWater == true && animals[i].GetReservoir == true && complices[j].GetHeat == false && animals[i].GetHeat == false)
                    {
                        Console.WriteLine($"{animals[i].ToString()}\t могут быть помешенны помещены в  {complices[j].ToString()}.");
                        complices[j].adding(animals[i]);

                    }
                    if (complices[j].GetWater == true && animals[i].GetReservoir == true && complices[j].GetHeat == true && animals[i].GetHeat == true)
                    {
                        Console.WriteLine($"{animals[i].ToString()}\t  могут быть помешенны помещены в  {complices[j].ToString()}.");
                        complices[j].adding(animals[i]);
                    }
                    if (complices[j].GetWater == false && animals[i].GetReservoir == false && complices[j].GetHeat == true && animals[i].GetHeat == true)
                    {
                        Console.WriteLine($"{animals[i].ToString()}\t  могут быть помешенны помещены в  {complices[j].ToString()}.");
                        complices[j].adding(animals[i]);
                    }
                    if (complices[j].GetWater == false && animals[i].GetReservoir == false && complices[j].GetHeat == false && animals[i].GetHeat == false)
                    {
                        Console.WriteLine($"{animals[i].ToString()}\t  могут быть помешенны помещены в  {complices[j].ToString()}.");
                        complices[j].adding(animals[i]);
                    }
                }

            }

        }

        public static void AddAnimalsInComplex(List<Complex> complices, List<TypeOfAnimal> animals)
        {
            Console.WriteLine("******************************");
            Console.WriteLine("\nПоселите животного в комплекс");
            Console.WriteLine("Виды животных которые могут проживать в зоопарке");
            for (int i = 0; i < animals.Count; i++) Console.WriteLine(animals[i].GetName);
            Console.WriteLine("Введите вид животного которого хотите поселить");
            string animal = Console.ReadLine();
            Console.WriteLine($"Комплексы в которые можно поселить {animal}");
            bool flag1 = false;
            int[] mas = new int[complices.Count];
            int num;
            for (int i = 0; i < complices.Count; i++)
            {
                for (int j = 0; j < complices[i].presents.Count; j++)
                {
                    var key = complices[i].presents.ElementAt(j).Key;
                    if (key.GetName == animal)
                    {
                        Console.WriteLine($"{"№" + i + " " + complices[i].ToString()}");
                        mas[i] = i;
                        flag1 = true;
                    }
                }
            }
            if (flag1 == false)
            {
                Console.WriteLine("Нет комплекса в который можно поселить это животное");
                return;
            }
            for (; ; )
            {
                Console.WriteLine($"Введите номер комплекса в который вы хотите поселить {animal}");
                num = Int32.Parse(Console.ReadLine());
                if (mas.Contains(num)) break;
                else Console.WriteLine("Ошибка ввода");
            }
            bool flag = false;
            int kol_vo;

            for (; ; )
            {
                Console.WriteLine("Введите количество животных которых хотите поселить");
                kol_vo = Int32.Parse(Console.ReadLine());
                if (kol_vo > 0) break;
                else Console.WriteLine("Ошибка ввода");
            }
            complices[num].AddAni(animal, kol_vo, ref flag);


        }

        public static void DelAnimalsInComplex(List<Complex> complices)
        {
            Console.WriteLine("******************************");
            string animal;
            int kol_vo;
            bool flag = false;
            bool flag2 = false;
            Console.WriteLine($"Комплексы");
            int s = 1;
            foreach (var x in complices)
            {
                Console.WriteLine($"{"№" + s + " " + x.ToString()}");
                s++;
            }
            Console.WriteLine("Введите номер комплекса из которого вы хотите удалить животное");
            int nc = Int32.Parse(Console.ReadLine());
            nc--;
            if (complices[nc].animals_in_room == 0)
            {
                Console.WriteLine("В комплексе нет животных");
                return;
            }
            complices[nc].WriteData();
            for (; ; )
            {
                Console.WriteLine("Введите название животного которого хотите удалить из комплекса");
                animal = Console.ReadLine();
                foreach (var x in complices[nc].presents)
                {
                    if (x.Key.GetName == animal) flag2 = true;
                }
                if (flag2 == true) break;
                else Console.WriteLine("Ошибка ввода.");
            }
            for (; ; )
            {
                Console.WriteLine("Введите количество животных которых вы хотите удалить");
                kol_vo = Int32.Parse(Console.ReadLine());
                foreach (var x in complices[nc].presents)
                {
                    if (x.Key.GetName == animal && x.Value >= kol_vo) flag = true;
                }
                if (flag == true) break;
                else Console.WriteLine("Ошибка ввода. Количество животных которо вы хотите удалить больше че кол-во животных проживающих в комплексе ");
            }
            complices[nc].DelAni(animal, kol_vo);

        }

        public static void AddTOFInComplex(List<Complex> newcom, List<TypeOfAnimal> animals)
        {
            int nc;
            Console.WriteLine("******************************");
            Console.WriteLine("\nДобавление в комплекс вида животного который может там проживать");
            Console.WriteLine("Комплексы:");
            for (int i = 0; i < newcom.Count; i++)
            {
                Console.WriteLine($"№{i + 1} {newcom[i].ToString()}");
            }
            for (; ; )
            {
                Console.WriteLine("Введите номер комплекса");
                nc = Int32.Parse(Console.ReadLine());
                if (nc - 1 >= 0 && nc - 1 < newcom.Count) break;
                else Console.WriteLine("Ошибка ввода");
            }
            newcom[--nc].adding(animals);

        }


    }
}
