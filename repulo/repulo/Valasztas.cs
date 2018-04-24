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

        public List<Metodusok.Ut> tarsasagNelkul = new List<Metodusok.Ut>();
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
                    int repulesiIdoOra = 0;
                    int repulesiIdoPerc = 0;
                    if (met.OraDarabolo(sor[5].ToString()) < met.OraDarabolo(sor[4].ToString()))
                    {
                        repulesiIdoOra = met.OraDarabolo(sor[5].ToString())+24 - met.OraDarabolo(sor[4].ToString());
                        repulesiIdoPerc = met.PercDarabolo(sor[5].ToString()) - met.PercDarabolo(sor[4].ToString());
                    }
                    else
                    {
                        repulesiIdoOra = met.OraDarabolo(sor[5].ToString()) - met.OraDarabolo(sor[4].ToString());
                        repulesiIdoPerc = met.PercDarabolo(sor[5].ToString()) - met.PercDarabolo(sor[4].ToString());
                    }
                    
                    Console.WriteLine("\tFelszállás: " + sor[1] + " Leszállás: " + sor[2] + " Távolság: " + sor[3] + " km Indul: " + sor[4].ToString()+" Érkezik "+sor[5].ToString()+" Időtartama: "+repulesiIdoOra+" óra "+ repulesiIdoPerc+" perc");
                    Console.WriteLine();
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

            bool van = false;
            while (!van)
            {
                Console.WriteLine("Kérlek add meg, hogy honnan szeretnél indulni(Ha nem szeretnél ez alapján szűrni akkor nyomj entert):");
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
                Console.WriteLine("Most az, hogy hova szeretnél utazni(Ha nem szeretnél ez alapján szűrni akkor nyomj entert):");
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
                if (met.atszallas.Count == 0)
                {
                    met.Kereses(ref ds, honnan, hova, null);
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
        }
    }
}
