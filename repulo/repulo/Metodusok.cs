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
        public void kereses(string honnan, string hova)
        {
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                if(honnan == ds.Tables[1].Rows[i][1].ToString() && hova == ds.Tables[1].Rows[i][2].ToString())
                {
                    ut utvonal = new ut();
                    utvonal.honnan = ds.Tables[1].Rows[i][1].ToString();
                    utvonal.hova = ds.Tables[1].Rows[i][2].ToString();
                    utvonal.km = ds.Tables[1].Rows[i][3].ToString();
                    utvonal.ido = ds.Tables[1].Rows[i][4].ToString();
                    utvonal.tarsasag = ds.Tables[1].Rows[i][5].ToString();
                    atszallas.Add(utvonal);
                    //Console.WriteLine(utvonal.honnan+";"+utvonal.hova+";"+utvonal.km+";"+utvonal.ido+";"+utvonal.tarsasag);
                }
            }
        }
    }
}
