using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repulo
{
    class Valasztas
    {


        Metodusok met = new Metodusok();
        public void LegitarsasagokListaja(ref DataSet ds, bool kiir = true)
        {
            met.LegitarsasagokListaja(ref ds,kiir);
        }

        public void Utvonal(ref DataSet ds)
        {
            #region Változók

            int kisVaros = Convert.ToInt32(ds.Tables[0].Rows[0][1]);
            string kisVarosNeve = ds.Tables[0].Rows[0][0].ToString();
            int nagyVaros = Convert.ToInt32(ds.Tables[0].Rows[0][1]);
            string nagyVarosNeve = ds.Tables[0].Rows[0][0].ToString();


            #endregion

            LegitarsasagokListaja(ref ds, false);

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                if (Convert.ToInt32(item[1]) < kisVaros)
                {
                    kisVaros = Convert.ToInt32(item[1]);
                    kisVarosNeve = item[0].ToString();
                }
                if(Convert.ToInt32(item[1]) > nagyVaros)
                {
                    nagyVaros = Convert.ToInt32(item[1]);
                    nagyVarosNeve = item[0].ToString();
                }
            }

            met.honnanEredeti = kisVarosNeve;
            met.hovaEredeti = nagyVarosNeve;

            Console.WriteLine(kisVarosNeve+"->"+nagyVarosNeve);

            for (int i = 0; i < met.legitarsasagLista.Count; i++)
            {
                met.kisVarosNagyVaros(ref ds,kisVarosNeve,nagyVarosNeve,null,met.legitarsasagLista[i]);
            }
            met.KeszKiiratas();

        }

        public void LegitarsasagJaratai(ref DataSet ds)
        {
            Console.WriteLine("Melyik légitársaság járatait szeretné látni?");
            Console.WriteLine("Lista:");
            met.LegitarsasagokListaja(ref ds,true);
            Console.WriteLine();
            string legitarsasag = Console.ReadLine();
            
            Console.WriteLine();
            Console.WriteLine(legitarsasag);
            Console.WriteLine();
            foreach (DataRow sor in ds.Tables[1].Rows)
            {
                if (sor[6].ToString()==legitarsasag)
                {
                    Console.WriteLine("Felszállás: " + sor[1] + " Leszállás: " + sor[2] + " Távolság: " + sor[3] + " km Időtartam " + met.OraValto(sor[4].ToString(),sor[5].ToString()));
                }
            }
            
        }

        public void Utvonaltervezes(ref DataSet ds)
        {
            LegitarsasagokListaja(ref ds,false);
            string honnan = "";
            string hova = "";
            List<string> volt = new List<string>();
            List<Metodusok.Ut> aktualis = new List<Metodusok.Ut>();

            int repulesiIdo = 0;
            string elozo = "";

            bool van = false;
            while (!van)
            {
                Console.WriteLine("Kérlek add meg, hogy honnan szeretnél indulni:");
                honnan = Console.ReadLine();
                foreach(DataRow dr in ds.Tables[0].Rows)
                if (dr[0].ToString().Contains(honnan) || honnan == null)
                {
                    van = true;
                }
                if(!van)
                    Console.WriteLine("Sajnos innen nem indulnak járatok!");
            }
            met.honnanEredeti = honnan;

            van = false;
            while (!van)
            {
                Console.WriteLine("Most az, hogy hova szeretnél utazni:");
                hova = Console.ReadLine();
                foreach (DataRow dr in ds.Tables[0].Rows)
                    if (dr[0].ToString().Contains(hova) || hova == null)
                    {
                        van = true;
                    }
                if (!van)
                {
                 Console.WriteLine("Sajnos ide nem indulnak járatok!");
                }
            }
            met.hovaEredeti = hova;

            if (honnan != "" && hova != "")
            {
                for (int i = 0; i < met.legitarsasagLista.Count; i++)
                {
                    met.kisVarosNagyVaros(ref ds, honnan, hova, null,met.legitarsasagLista[i]);
                }
            }
            else
            {
                if (honnan == "" && hova != "")
                {
                    met.Kereses(ref ds, null, hova);
                }
                else
                {
                    if (hova == "" && honnan!="")
                    {
                        met.Kereses(ref ds, honnan, null);
                    }
                    else
                        Console.WriteLine("Vagy az induló állomást vagy a cél állomást megkell adni!");
                }
            }
            if (met.atszallas.Count > 0)
            {
                met.KeszKiiratas();
            }
            else
            {
                Console.WriteLine(met.atszallas.Count);
                Console.ReadKey();
                met.Kereses(ref ds, honnan, hova);
                Console.WriteLine(met.atszallas.Count);
            }
        }
    }
}
