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
        bool siker = false;
        public string honnanEredeti = "";
        public string hovaEredeti = "";
        public struct ut
        {
           public string honnan;
           public string hova;
           public string km;
           public string ido;
           public string tarsasag;
        }
        public static string kapcsolat = "SERVER=localhost;DATABASE=repulo;UID=root;PASSWORD=mysql;";
        public static MySqlCommand parancs = new MySqlCommand();
        public static MySqlConnection cnn = new MySqlConnection(kapcsolat);
        public MySqlDataAdapter da = new MySqlDataAdapter();
        public DataSet ds = new DataSet();
        public List<ut> atszallas = new List<ut>();

        public void lekerdezes(string lekerdezes) {
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
        public bool kereses(string honnan, string hova)
        {
            
            bool megvan = false;

            for (int i = 0; i <ds.Tables[1].Rows.Count; i++)
            {
                if(honnan == ds.Tables[1].Rows[i][1].ToString())
                {
                    if(hova == ds.Tables[1].Rows[i][2].ToString())
                    {
                        hozzaad(i);
                        megvan = true;
                    }
                    else
                    {
                        megvan = kereses(ds.Tables[1].Rows[i][2].ToString(), hova);
                        if (megvan)
                        {
                            hozzaad(i);
                        }
                    }
                    
                }
            }
            return megvan;
        }
        public void hozzaad(int i)
        {
            ut utvonal = new ut();
            int ora = Convert.ToInt16(ds.Tables[1].Rows[i][4]) / 60;
            int perc = Convert.ToInt16(ds.Tables[1].Rows[i][4]) % 60;
            utvonal.honnan = ds.Tables[1].Rows[i][1].ToString();
            utvonal.hova = ds.Tables[1].Rows[i][2].ToString();
            utvonal.km = ds.Tables[1].Rows[i][3].ToString();
            utvonal.ido = ""+ora+"óra "+perc+" perc  "+ ds.Tables[1].Rows[i][4].ToString();
            utvonal.tarsasag = ds.Tables[1].Rows[i][5].ToString();
            atszallas.Insert(0,utvonal);
        }
        
    }
}
