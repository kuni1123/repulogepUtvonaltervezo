using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repulo
{
    class Metodusok
    {
        public string honnanEredeti = "";
        public string hovaEredeti = "";
        public static int HONNAN_INDEX = 1;
        public static int HOVA_INDEX = 2;
        public static int TARSASAG_INDEX = 6;
        public int repulesiIdo = 0;

        public struct Ut
        {
           public string honnan;
           public string hova;
           public string km;
           public string indul;
           public string erkezik;
           public string tarsasag;
        }

        public struct egyUt
        {
            public string ut;
            public int km;
            public int ido;
            public string elozo;
        }

        #region változók
        public static string kapcsolat = "SERVER=localhost;DATABASE=repulo;UID=root;PASSWORD=mysql;";
        public static MySqlConnection cnn = new MySqlConnection(kapcsolat);
        public List<string> legitarsasagLista = new List<string>();
        public static MySqlCommand parancs = new MySqlCommand();
        public MySqlDataAdapter da = new MySqlDataAdapter();
        public List<Ut> atszallas2 = new List<Ut>();
        public List<Ut> atszallas = new List<Ut>();
        public List<egyUt> utak = new List<egyUt>();
        public List<int> kmek = new List<int>();
        public DataSet ds = new DataSet();
        string tarsasag="";
        egyUt EgyUt = new egyUt();
        #endregion

        public void Lekerdezes(string lekerdezes) {
            try
            {
                DataTable dt = new DataTable();
                cnn.Open();
                parancs = new MySqlCommand(lekerdezes, cnn);
                da.SelectCommand = parancs;
                da.Fill(dt);
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public bool kisVarosNagyVaros(ref DataSet ds2, string honnan, string hova,  string erkezik = null, string tarsasag = null)
        {
            bool megvan = false;
            for (int i = 0; i < ds2.Tables[1].Rows.Count; i++)
            {
                if (honnan == ds2.Tables[1].Rows[i][HONNAN_INDEX].ToString() && ds2.Tables[1].Rows[i][TARSASAG_INDEX].ToString() == tarsasag)
                {
                    if (hova == ds2.Tables[1].Rows[i][HOVA_INDEX].ToString() && OraDarabolo(erkezik) < OraDarabolo(ds2.Tables[1].Rows[i][4].ToString()))
                    {
                        Hozzaad(i, ref ds2);
                        erkezik = ds2.Tables[1].Rows[i][5].ToString();
                        megvan = true;
                    }
                    else
                    {
                        megvan = kisVarosNagyVaros(ref ds2, ds2.Tables[1].Rows[i][HOVA_INDEX].ToString(), hova, ds2.Tables[1].Rows[i][5].ToString(), tarsasag);
                        if (megvan)
                        {
                            Hozzaad(i, ref ds2);
                        }
                    }

                }
            }
            return megvan;
        }

        public void Hozzaad(int i, ref DataSet ds2)
        {
            Ut utvonal;
            utvonal.honnan = ds2.Tables[1].Rows[i][1].ToString();
            utvonal.hova = ds2.Tables[1].Rows[i][2].ToString();
            utvonal.km = ds2.Tables[1].Rows[i][3].ToString();
            utvonal.indul = ds2.Tables[1].Rows[i][4].ToString();
            utvonal.erkezik = ds2.Tables[1].Rows[i][5].ToString();
            utvonal.tarsasag = ds2.Tables[1].Rows[i][6].ToString();
            if (!atszallas.Contains(utvonal))
            {
                atszallas.Insert(0,utvonal);
            }
        }

        public void LegitarsasagokListaja(ref DataSet ds, bool kiiras)
        {
            foreach(DataRow item in ds.Tables[1].Rows)
            {
                if (!legitarsasagLista.Contains(item[6]) && kiiras)
                {
                    legitarsasagLista.Add(item[6].ToString());
                    Console.WriteLine(item[6].ToString());
                }
                else
                {
                    legitarsasagLista.Add(item[6].ToString());
                }
            }
        }

        public string OraValto(string kezdoIdo, string vegIdo)
        {
            string vissza = "";
            string[] ora1String = kezdoIdo.Split(':');
            int[] ora1 = new int[2];
            ora1[0] = Convert.ToInt16(ora1String[0])*60;
            ora1[1] = Convert.ToInt16(ora1String[1]);

            string[] ora2String = vegIdo.Split(':');
            int[] ora2 = new int[2];
            ora2[0] = Convert.ToInt16(ora2String[0])*60;
            ora2[1] = Convert.ToInt16(ora2String[1]);

            int oraKezdo, oraVeg;
            oraKezdo = ora1[0] + ora1[1];
            oraVeg = ora2[0] + ora2[1];

            int kesz = oraVeg - oraKezdo;
            vissza = "" + kesz / 60 + " óra " + kesz % 60 + " perc ";
            return vissza;
        }

        public int OraDarabolo(string vegIdo)
        {
            string[] darab = new string[2];
            if (vegIdo != null)
            {
                darab = vegIdo.Split(':');
            }
            return Convert.ToInt16(darab[0]);
        }

        public int PercDarabolo(string vegIdo)
        {
            string[] darab = new string[2];
            if (vegIdo != null)
            {
                darab = vegIdo.Split(':');
            }
            return Convert.ToInt16(darab[1]);
        }

        public void hianyosKiiratas(DataRow dr)
        {
            if (tarsasag != dr[TARSASAG_INDEX].ToString())
            {
                Console.WriteLine();
                Console.WriteLine("Társaság: " + dr[TARSASAG_INDEX]);
                Console.WriteLine();
            }
            Console.WriteLine("\tFelszállás: " + dr[HONNAN_INDEX] + " Leszállás: " + dr[HOVA_INDEX] + " Távolság: " + dr[3] + " km Indulás: " + dr[4] + " Érkezés: " + dr[5] + " Társaság: " + dr[TARSASAG_INDEX]);
        }

        public bool Kereses(ref DataSet ds2, string honnan = null, string hova = null, string erkezik = null)
        {
            bool megvan = false;
            if(honnan!=null && hova != null)
            {
                for (int i = 0; i < ds2.Tables[1].Rows.Count; i++)
                {
                    if (honnan == ds2.Tables[1].Rows[i][HONNAN_INDEX].ToString())
                    {
                        if (hova == ds2.Tables[1].Rows[i][HOVA_INDEX].ToString() && OraDarabolo(erkezik) < OraDarabolo(ds2.Tables[1].Rows[i][4].ToString()))
                        {
                            Hozzaad(i, ref ds2);
                            erkezik = ds2.Tables[1].Rows[i][5].ToString();
                            megvan = true;
                        }
                        else
                        {
                            megvan = Kereses(ref ds2, ds2.Tables[1].Rows[i][HOVA_INDEX].ToString(), hova, ds2.Tables[1].Rows[i][5].ToString());
                            if (megvan)
                            {
                                Hozzaad(i, ref ds2);
                            }
                        }

                    }
                }
            }
            if (honnan == null && hova != null)
            {
                foreach (DataRow dr in ds2.Tables[1].Rows)
                {
                    if (dr[HOVA_INDEX].ToString() == hova)
                    {
                        hianyosKiiratas(dr);
                    }
                }
            }
            else
            {
                foreach (DataRow dr in ds2.Tables[1].Rows)
                {
                    if (dr[HONNAN_INDEX].ToString() == honnan)
                    {
                        hianyosKiiratas(dr);
                    }
                }
            }
            return megvan;
        }

        public void KeszKiiratas()
        {
            List<string> volt = new List<string>();
            List<Ut> aktualis = new List<Ut>();

            int repulesiIdo = 0;
            int repulesiIdoPerc = 0;
            string elozo = "";

            for (int i = 0; i < legitarsasagLista.Count; i++)
            {

                for (int y = 0; y < atszallas.Count; y++)
                {
                    if (legitarsasagLista[i] == atszallas[y].tarsasag)
                    {
                        if (!volt.Contains(atszallas[y].tarsasag))
                        {
                            Console.WriteLine();
                            Console.WriteLine(atszallas[y].tarsasag);
                            Console.WriteLine();
                            volt.Add(atszallas[y].tarsasag);
                        }
                        Ut utvonal = new Ut();
                        utvonal = atszallas[y];

                        if (!aktualis.Contains(utvonal))
                        {
                            aktualis.Add(utvonal);
                            Console.WriteLine("\t" + utvonal.honnan + "->" + utvonal.hova + " Indulás: " + utvonal.indul + " Érkezés: " + utvonal.erkezik + " km " + utvonal.km + " Társaság: " + utvonal.tarsasag);
                            if (repulesiIdo == 0)
                            {
                                repulesiIdo += OraDarabolo(utvonal.erkezik) - OraDarabolo(utvonal.indul);
                                repulesiIdoPerc += PercDarabolo(utvonal.erkezik) - PercDarabolo(utvonal.indul);
                                elozo = utvonal.erkezik;
                            }
                            else
                            {
                                repulesiIdo += OraDarabolo(utvonal.indul) - OraDarabolo(elozo);
                                repulesiIdo += OraDarabolo(utvonal.erkezik) - OraDarabolo(utvonal.indul);

                                repulesiIdoPerc += PercDarabolo(utvonal.indul) - PercDarabolo(elozo);
                                repulesiIdoPerc += PercDarabolo(utvonal.erkezik) - PercDarabolo(utvonal.indul);

                                elozo = utvonal.erkezik;
                            }
                        }
                    }
                }
                if (repulesiIdo != 0)
                {
                    if (repulesiIdo < 0)
                    {
                        repulesiIdo = repulesiIdo * -1;
                    }
                    if (repulesiIdoPerc > 60)
                    {
                        repulesiIdo += repulesiIdoPerc / 60;
                        repulesiIdoPerc = repulesiIdoPerc % 60;
                    }
                    Console.WriteLine();
                    Console.WriteLine("\tA repülés időtartama " + repulesiIdo + " óra " + repulesiIdoPerc + " perc");
                    repulesiIdo = 0;
                    repulesiIdoPerc = 0;
                }

            }
        }
    }
}
