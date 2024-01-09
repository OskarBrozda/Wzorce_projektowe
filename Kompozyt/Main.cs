using System;
using System.Collections.Generic;
using System.Text;

namespace Kompozyt
{
    public interface Kompozyt
    {
        void Renderuj();

        void DodajElement(Kompozyt element);

        void UsunElement(Kompozyt element);
    }

    public class Lisc : Kompozyt
    {
        public string nazwa { get; set; }

        public void Renderuj()
        {
            Console.WriteLine(nazwa + " renderowanie...");
        }

        public Lisc()
        {

        }

        public void DodajElement(Kompozyt element)
        {
            throw new NotSupportedException("...");
        }

        public void UsunElement(Kompozyt element)
        {
            throw new NotSupportedException("...");
        }
    }

    public class Wezel : Kompozyt
    {
        private List<Kompozyt> Lista = new List<Kompozyt>();

        public string nazwa { get; set; }

        public void Renderuj()
        {
            Console.WriteLine(nazwa + " rozpoczęcie renderowania"); 
            foreach (var item in Lista)
            {
                item.Renderuj();
            }
            Console.WriteLine(nazwa + " zakończenie renderowania");
        }

        public void DodajElement(Kompozyt element)
        {
            if (!Lista.Contains(element))
            {
                Lista.Add(element);
            }
        }

        public void UsunElement(Kompozyt element)
        {
            if (Lista.Contains(element))
            {
                Lista.Remove(element);
            }
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Wezel korzen = new Wezel();
            korzen.nazwa = "Korzeń";
            Lisc lisc = new Lisc() { nazwa = "Liść 1.1" };
            korzen.DodajElement(lisc);

            Wezel wezel2 = new Wezel();
            wezel2.nazwa = "Węzeł 2";
            for (int i = 1; i <= 3; i++)
            {
                wezel2.DodajElement(new Lisc() { nazwa = $"Liść 2.{i}" });
            }
            korzen.DodajElement(wezel2);

            Wezel wezel3 = new Wezel();
            wezel3.nazwa = "Węzeł 3";
            for (int i = 1; i <= 2; i++)
            {
                wezel3.DodajElement(new Lisc() { nazwa = $"Liść 3.{i}" });
            }

            Wezel wezel33 = new Wezel();
            wezel33.nazwa = "Węzeł 3.3";
            wezel33.DodajElement(new Lisc() { nazwa = "Liść 3.3.1" });

            wezel3.DodajElement(wezel33);
            korzen.DodajElement(wezel3);
            korzen.Renderuj();
        }
    }
}