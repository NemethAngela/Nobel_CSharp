using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Nobel_CSharp
{
    internal class Program
    {
        static List<NobelDij> nobelDijLista = new List<NobelDij>();

        static void Main(string[] args)
        {
            //Console.WriteLine("Add meg a neved!");
            //string nev = Console.ReadLine();
            //Console.WriteLine($"Szia {nev}!");

            // Fájl beolvasása
            ReadFile("nobel.csv");

            // 3. feladat: Artur B. McDonald milyen típusú díjat kapott! Feltételezheti, hogy életében csak egyszer kapott Nobel­ díjat.
            Console.WriteLine("3. feladat");
            NobelDij result = FindNobelDijByName("Arthur B.", "McDonald");
            Console.WriteLine(result.ToString());

            // 4. feladat: ki kapott 2017-ben irodalmi Nobel-díjat!
            Console.WriteLine("4. feladat");
            List<NobelDij> resultList = FindNobelDijByYearAndType(2017, "irodalmi");
            NobelDijListaKiir(resultList);

            // 5. feladat: mely szervezetek kaptak béke Nobel-díjai 1990-től napjainkig!
            Console.WriteLine("5. feladat");
            List<NobelDij> resultList2 = FindCorpNobelDijFromYear(1990);
            NobelDijListaKiir(resultList2);

            // 6. feladat: A Curie család több tagja is kapott díjat. melyik évben a család melyik tagja milyen díjat kapott!
            Console.WriteLine("6. feladat");
            List<NobelDij> resultList3 = FindNobelDijByVezeteknevPart("Curie");
            NobelDijListaKiir(resultList3);

            // 7. feladat: melyik típusú díjból hány darabot osztottak ki a Nobel-díj történelme folyamán!
            Console.WriteLine("7. feladat");
            ListSumOfNobelDijakByType();

            // 8. feladat: Hozzon létre orvosi.txt néven egy UTF-8 kódolású szöveges állományt, amely tartalmazza az
            // összes kiosztott orvosi Nobel-díj adatait! A fájlban szerepeljen kettősponttal elválasztva a
            // kiosztás éve és a díjazott teljes neve! A sorok évszám szerint növekvő rendben legyenek az
            // állományban!
            Console.WriteLine("8. feladat");
            WriteFile("orvosi.txt");
            Console.WriteLine("A fájlírás elkészült");

            Console.ReadKey();
        }

        private static void ReadFile(string fileName)
        {
            using (TextReader reader = new StreamReader(fileName))
            {
                string line = reader.ReadLine();

                while (line != null)
                {
                    line = reader.ReadLine();
                    if (line == null)
                        continue;

                    string[] parsed = line.Split(';');
                    NobelDij nd = new NobelDij();
                    nd.Ev = int.Parse(parsed[0]);
                    nd.Tipus = parsed[1];
                    nd.Keresztnev = parsed[2];

                    if (parsed.Length == 3)
                    {
                        nd.Vezeteknev = "";
                    }
                    else if (parsed.Length == 4)
                    {
                        nd.Vezeteknev = parsed[3];
                    }

                    nobelDijLista.Add(nd);
                }
            }
        }

        private static void WriteFile(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                List<NobelDij> orvosiNobelLista = nobelDijLista
                    .Where(nobel => nobel.Tipus == "orvosi")
                    .OrderBy(nobel => nobel.Ev)
                    .ToList();

                foreach (var nobel in orvosiNobelLista)
                {
                    string line = $"{nobel.Ev}:{nobel.Vezeteknev} {nobel.Keresztnev}";
                    writer.WriteLine(line);
                }

                writer.Close();
            }
        }

        private static void NobelDijListaKiir(List<NobelDij> lista)
        {
            foreach (var nobel in lista)
            {
                Console.WriteLine(nobel.ToString());
            }
        }

        private static NobelDij FindNobelDijByName(string keresztnev, string vezeteknev)
        {
            return nobelDijLista
                .Where(nobel => nobel.Keresztnev == keresztnev && nobel.Vezeteknev == vezeteknev)
                .FirstOrDefault();
        }

        private static List<NobelDij> FindNobelDijByYearAndType(int ev, string tipus)
        {
            return nobelDijLista
                .Where(nobel => nobel.Ev == ev && nobel.Tipus == tipus)
                .ToList();
        }

        private static List<NobelDij> FindCorpNobelDijFromYear(int ev)
        {
            return nobelDijLista
                .Where(nobel => nobel.Ev >= ev && nobel.Vezeteknev == "")
                .ToList();
        }

        private static List<NobelDij> FindNobelDijByVezeteknevPart(string nevreszlet)
        {
            return nobelDijLista
                .Where(nobel => nobel.Vezeteknev.Contains(nevreszlet))
                .ToList();
        }

        private static void ListSumOfNobelDijakByType()
        {
            var resuslt = nobelDijLista
                .GroupBy(nobel => nobel.Tipus)
                .ToDictionary(group => group.Key, group => group.Count())
                .ToList();

            foreach (var item in resuslt)
            {
                Console.WriteLine($"Típus: {item.Key}, Darab: {item.Value}");
            }
        }
    }
}
