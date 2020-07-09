using Chilkat;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using System.Data.SqlClient;
using System.Data;
using Nancy.Extensions;
using ConsoleApp2.CrawlData;

namespace ConsoleApp2
{
    class Program
    {
        public static SqlConnection sqlConnection;

        private static void Connect()
        {
            string sql = @"Data Source=DESKTOP-8GEUI6U;Initial Catalog=JSON-CSharp;Integrated Security=True";
            sqlConnection = new SqlConnection(sql);
        }

        private static void AddOrderToDatabase(string orderToken, string total)
        {
            Connect();
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO ResponseCreateOrder(tokenOrder, total) VALUES ('" + orderToken + "', '" + total + "')";
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        private static void AddDesignToDatabase(string idDesign, string nameDesign, string linkImage, string note, string dateCreat)
        {
            Connect();
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO ResponseCreateOrder(idDesign, nameDesign, linkImage, note, dateCreat) VALUES('" + idDesign + "', '" + nameDesign + "', '" + linkImage + "', '" + note + "', '" + dateCreat + "')";
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        private static List<ImageDesign> ReadCreateDesignToDatabase()
        {
            Connect();
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "Select * from ResponseCreateDesign";
            SqlDataReader reader = sqlCommand.ExecuteReader();
            List<ImageDesign> imageDesigns = new List<ImageDesign>();
            while (reader.Read())
            {
                var time = Convert.ToDateTime(reader[4]).ToString("dd/MM/yyyy");
                imageDesigns.Add(new ImageDesign(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), time));
                //Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString());
            }
            sqlConnection.Close();
            return imageDesigns;
        }

        private static string ResponseCreateDesign()
        {
            Chilkat.Global glob = new Chilkat.Global();
            Chilkat.Http http = new Chilkat.Http();
            bool success = glob.UnlockBundle("Anything for 30-day trial");

            string idDesign;

            var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($":live_3fcM3B7nwXUQFQvGuI6uAw"));
            http.SetRequestHeader("Authorization", $"Basic {base64authorization}");

            HttpRequest requestCreateDesign = new HttpRequest();
            requestCreateDesign.AddHeader("Authorization", $"Basic {base64authorization}");
            requestCreateDesign.AddParam("type", "dtg");
            requestCreateDesign.AddParam("sides[front][artwork]", @"https://www.dropbox.com/s/ujzun8lrldha60d/1.png?dl=1");
            //requestCreateDesign.AddParam("sides[front][colors][0]", "ash");
            requestCreateDesign.AddParam("sides[front][dimensions][width]", "5");
            requestCreateDesign.AddParam("sides[front][position][horizontal]", "C");
            requestCreateDesign.AddParam("sides[front][position][offset][top]", "2.5");
            HttpResponse responseCreateDesign = http.PostUrlEncoded("https://api.scalablepress.com/v2/design", requestCreateDesign);
               
            string json = responseCreateDesign.BodyStr.ToString();
            ResponseDesign responseDesign = JsonConvert.DeserializeObject<ResponseDesign>(json);
            //AddDesignToDatabase(responseDesign.designId.ToString().Trim())
            //idDesign = responseDesign.designId.ToString().Trim();
            idDesign = responseDesign.designId;
            return idDesign;
        }

        private static void ResponeOrder()
        {
            Chilkat.Global glob = new Chilkat.Global();
            Chilkat.Http http = new Chilkat.Http();
            bool success = glob.UnlockBundle("Anything for 30-day trial");

            string idDesign = ResponseCreateDesign();

            var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($":live_3fcM3B7nwXUQFQvGuI6uAw"));
            http.SetRequestHeader("Authorization", $"Basic {base64authorization}");

            HttpRequest requestCreateOrder = new HttpRequest();
            requestCreateOrder.AddHeader("Authorization", $"Basic {base64authorization}");
            requestCreateOrder.AddParam("type", "dtg");
            requestCreateOrder.AddParam("products[0][id]", "gildan-sweatshirt-crew");
            requestCreateOrder.AddParam("products[0][color]", "Antique Sapphire");
            requestCreateOrder.AddParam("products[0][quantity]", "12");
            requestCreateOrder.AddParam("products[0][size]", "lrg");
            requestCreateOrder.AddParam("address[name]", "Collin Nyberg");
            requestCreateOrder.AddParam("address[address1]", "1405 stabler ln");
            requestCreateOrder.AddParam("address[address2]", "");
            requestCreateOrder.AddParam("address[city]", "Yuba city");
            requestCreateOrder.AddParam("address[state]", "CA");
            requestCreateOrder.AddParam("address[zip]", "95993");
            requestCreateOrder.AddParam("address[country]", "US");
            requestCreateOrder.AddParam("features[shipping]", "DHL-SM");
            requestCreateOrder.AddParam("features[foldPoly]", "true");
            requestCreateOrder.AddParam("designId", idDesign);
            HttpResponse responseCreateOrder = http.PostUrlEncoded("https://api.scalablepress.com/v2/quote", requestCreateOrder);
            if (responseCreateOrder != null)
            {
                string json = responseCreateOrder.BodyStr.ToString();
                ResponseOrder responseOrder = JsonConvert.DeserializeObject<ResponseOrder>(json);

                AddOrderToDatabase(responseOrder.orderToken.Trim(), responseOrder.total.ToString().Trim());

                Console.WriteLine("Đã thêm vào csdl");
            }
        }


        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;



            Console.ReadKey();
        }

    }
}
