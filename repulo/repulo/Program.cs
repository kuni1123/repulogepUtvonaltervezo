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

            string lekerdezes = "SELECT * FROM varos;";
            met.lekerdezes(lekerdezes);
            lekerdezes = "SELECT * FROM jaratok;";
            met.lekerdezes(lekerdezes);
            //for (int i = 0; i < met.ds.Tables[0].Rows.Count; i++)
            //{
            //    Console.Write(met.ds.Tables[0].Rows[i][0]);
            //    Console.WriteLine(met.ds.Tables[0].Rows[i][1]);
            //}
            //for (int i = 0; i < met.ds.Tables[1].Rows.Count; i++)
            //{
            //    Console.Write(met.ds.Tables[1].Rows[i][0] + ";");
            //    Console.Write(met.ds.Tables[1].Rows[i][1] + ";");
            //    Console.Write(met.ds.Tables[1].Rows[i][2] + ";");
            //    Console.Write(met.ds.Tables[1].Rows[i][3] + ";");
            //    Console.Write(met.ds.Tables[1].Rows[i][4] + ";");
            //    Console.WriteLine(met.ds.Tables[1].Rows[i][5]);
            //}
            //Console.WriteLine("Járatok és városok beolvasása Kész!");
            Console.WriteLine("Kérlek add meg, hogy honnan szeretnél indulni:");
            bool honnanVan = false;
            string honnan = "";
            string hova = "";
            while (!honnanVan)
            {
                honnan = Console.ReadLine();
                for (int i = 0; i < met.ds.Tables[1].Rows.Count; i++)
                {
                    if (met.ds.Tables[1].Rows[i][1].ToString() == honnan)
                    {
                        honnanVan = true;
                    }
                }
                if (!honnanVan)
                {
                    Console.WriteLine("Innen nem indulnak járatok: "+honnan);
                }
            }

            Console.WriteLine("És most add meg, hogy hova szeretnél utazni:");
            bool hovaVan = false;
            while (!hovaVan)
            {
                hova = Console.ReadLine();
                for (int i = 0; i < met.ds.Tables[1].Rows.Count; i++)
                {
                    if (met.ds.Tables[1].Rows[i][2].ToString() == hova)
                    {
                        hovaVan = true;
                    }
                }
                if (!hovaVan)
                {
                    Console.WriteLine("Nem indul "+honnan+"->" + hova+" járat!");
                }
            }
            met.honnanEredeti = honnan;
            met.hovaEredeti = hova;
            met.kereses(honnan, hova);
            Console.WriteLine("A legrövidebb útvonal:");
            for (int i = 0; i < met.atszallas.Count; i++)
            {
                Metodusok.ut utvonal = new Metodusok.ut();
                utvonal = met.atszallas[i];
                Console.WriteLine("Felszállás: "+utvonal.honnan+" Leszállás: "+utvonal.hova+" Távolság: "+utvonal.km+" km Időtartam "+utvonal.ido+" Társaság: "+utvonal.tarsasag);
            }
            Console.ReadKey();
        }
    }
}
