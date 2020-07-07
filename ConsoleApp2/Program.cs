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

namespace ConsoleApp2
{
    class Program
    {

        private static readonly HttpClient client = new HttpClient();

        public static Dictionary<string, string> dicOrder = new Dictionary<string, string>();

        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Chilkat.Global glob = new Chilkat.Global();
            Chilkat.Http http = new Chilkat.Http();
            bool success = glob.UnlockBundle("Anything for 30-day trial");


            Console.Write("Gõ run để chạy: ");
            string s = Console.ReadLine();
            if (s == "run")
            {
                var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($":live_3fcM3B7nwXUQFQvGuI6uAw"));
                http.SetRequestHeader("Authorization", $"Basic {base64authorization}");


                using (var httpClient = new HttpClient())
                {
                    var json = await httpClient.GetStringAsync(@"https://api.scalablepress.com/v2/categories");
                    
                }


                HttpRequest requestCreateOrder = new HttpRequest();
                requestCreateOrder.AddHeader("Authorization", $"Basic {base64authorization}");
                requestCreateOrder.AddParam("type", "screenprint");
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
                requestCreateOrder.AddParam("designId", "5f041cabe1be4536aac822fb");
                HttpResponse responseCreateOrder = http.PostUrlEncoded("https://api.scalablepress.com/v2/quote", requestCreateOrder);
                if (responseCreateOrder != null)
                {
                    string json = responseCreateOrder.BodyStr.ToString();
                    ResponseOrder responseOrder = JsonConvert.DeserializeObject<ResponseOrder>(json);
                        dicOrder.Add(responseOrder.orderToken.Trim(), responseOrder.subtotal.ToString().Trim());
                        Console.WriteLine(dicOrder.Count);
                    foreach (KeyValuePair<string, string> item in dicOrder)
                    {
                        if (item.Key == responseOrder.subtotal.ToString().Trim())
                        {
                            Console.WriteLine("Đã có hóa đơn");
                        }
                        else
                        {

                            Console.WriteLine(item.Key + "\t" + item.Value);
                        }
                    }
                }



                //HttpRequest requestCreateDesign = new HttpRequest();
                //requestCreateDesign.AddHeader("Authorization", $"Basic {base64authorization}");
                //requestCreateDesign.AddParam("type", "dtg");
                //requestCreateDesign.AddParam("sides[front][artwork]", @"https://www.dropbox.com/s/ujzun8lrldha60d/1.png?dl=1");
                //requestCreateDesign.AddParam("sides[front][colors][0]", "ash");
                //requestCreateDesign.AddParam("sides[front][dimensions][width]", "5");
                //requestCreateDesign.AddParam("sides[front][position][horizontal]", "C");
                //requestCreateDesign.AddParam("sides[front][position][offset][top]", "2.5");
                //HttpResponse responseCreateDesign = http.PostUrlEncoded("https://api.scalablepress.com/v2/design", requestCreateDesign);
                //if (responseCreateDesign == null)
                //{
                //    Console.WriteLine("Lỗi");
                //}

            }
            else
            {
                Console.WriteLine("Lỗi");
            }
            Console.ReadKey();
        }

        private static async void GetJson(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var json = await httpClient.GetStringAsync(url);
            }
        }
    }
}
