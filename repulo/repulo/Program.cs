using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data;

namespace repulo
{
    class Program
    {
        static void Main(string[] args)
        {
            Metodusok met = new Metodusok();
            Valasztas valasztas = new Valasztas();

            int valasztasSzam = 0;
            bool kilep = false;

            string lekerdezes = "SELECT * FROM varos;";
            met.Lekerdezes(lekerdezes);
            lekerdezes = "SELECT * FROM jaratok;";
            met.Lekerdezes(lekerdezes);

            Console.WriteLine("Mit szeretne?");
            Console.WriteLine("1. Légitársaságok járatai");
            Console.WriteLine("2. Légi társaságok listája");
            Console.WriteLine("3. Útvonal tervezés");
            Console.WriteLine("4. Legkissebb városból legnagyobb városba");
            
            while (!kilep)
            {
                try
                {
                    valasztasSzam = Convert.ToInt16(Console.ReadLine());
                    kilep = true;
                }
                catch
                {
                    Console.WriteLine("Kérlek számot írj be!");
                }
            }
            switch (valasztasSzam)
            {
                case 1:
                    valasztas.LegitarsasagJaratai(ref met.ds);
                    Console.WriteLine("A program bezárásához nyomj meg egy gombot!");
                    Console.ReadKey();
                    break;
                case 2:
                    valasztas.LegitarsasagokListaja(ref met.ds);
                    Console.WriteLine("A program bezárásához nyomj meg egy gombot!");
                    Console.ReadKey();
                    break;
                case 3:
                    valasztas.Utvonaltervezes(ref met.ds);
                    Console.WriteLine("A program bezárásához nyomj meg egy gombot!");
                    Console.ReadKey();
                    break;
                case 4:
                    valasztas.Utvonal(ref met.ds);
                    Console.WriteLine("A program bezárásához nyomj meg egy gombot!");
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Nincs ilyen menüpont!");
                    break;
            }
        }
    }
}
