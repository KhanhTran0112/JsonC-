using Chilkat;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp2.CrawlData
{

    public class CrawlCategoriesAndProduct
    {
        static string sql = @"Data Source=DESKTOP-8GEUI6U;Initial Catalog=JSON-CSharp;Integrated Security=True";
        public string jsonCategories { get; set; }

        public CrawlCategoriesAndProduct()
        {

        }

        public CrawlCategoriesAndProduct(string jsonCategories)
        {
            this.jsonCategories = jsonCategories;
        }


        private static void AddCategory(List<Category> categories)
        {
            SqlConnection sqlConnection = new SqlConnection(sql);
            sqlConnection.Open();
            string cmdText = "INSERT INTO CATEGORY_TABLE(NAME, URL, IDCATEGORY) VALUES ('" + categories[0].name + "', '" + categories[0].url + "', '" + categories[0].id + "')";
            SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        private static void AddProduct(List<Product> products)
        {
            SqlConnection sqlConnection = new SqlConnection(sql);
            sqlConnection.Open();
            string cmdText = "INSERT INTO PRODUCT_TABLE(NAMEPRODUCT, URLPRODUCT, IDPRODUCT, NAMECATEGORY) VALUES ('" + products[0].nameProduct + "', '" + products[0].urlProduct + "', '" + products[0].idProduct + "', '" + products[0].nameCategory + "')";
            SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void Categories()
        {
            jsonCategories = new WebClient().DownloadString("https://api.scalablepress.com/v2/categories");
            System.Text.RegularExpressions.MatchCollection mcCategory = System.Text.RegularExpressions.Regex.Matches(jsonCategories, @"""name"": ""(.+?)"".+?""url"": ""https://api\.scalablepress\.com/(.+?)"".+?""categoryId"": ""(.+?)""", RegexOptions.Singleline);
            if (mcCategory.Count > 0)
            {
                foreach (System.Text.RegularExpressions.Match itemCategory in mcCategory)
                {
                    string nameCategory = itemCategory.Groups[1].Value.Trim();
                    string urlCategory = itemCategory.Groups[2].Value.Trim();
                    urlCategory = "https://api.scalablepress.com/" + urlCategory;
                    string idCategory = itemCategory.Groups[3].Value.Trim();
                    List<Category> categories = new List<Category>();
                    categories.Add(new Category(nameCategory, urlCategory, idCategory));
                    AddCategory(categories);
                }
                Console.WriteLine("ddax xong");
            }


            Console.WriteLine("");
        }
        public void Product()
        {
            List<Category> categories = new List<Category>();

            SqlConnection sqlConnection = new SqlConnection(sql);
            sqlConnection.Open();
            string cmdText = "SELECT * FROM CATEGORY_TABLE";
            SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                categories.Add(new Category(reader[1].ToString(), reader[2].ToString(), reader[3].ToString()));
            }
            sqlConnection.Close();
            int i = 0;
            foreach(var item in categories)
            {
                string jsonProduct = new WebClient().DownloadString(item.url);
                System.Text.RegularExpressions.MatchCollection mcProductDemo = System.Text.RegularExpressions.Regex.Matches(jsonProduct, @"""products"": (.+?)""categoryId""", RegexOptions.Singleline);
                if (mcProductDemo.Count > 0)
                {
                    foreach(System.Text.RegularExpressions.Match itemProductDemo in mcProductDemo)
                    {
                        string s = itemProductDemo.Groups[1].Value.Trim();
                        System.Text.RegularExpressions.MatchCollection mcProduct = System.Text.RegularExpressions.Regex.Matches(s, @"""name"": ""(.+?)"".+?""id"": ""(.+?)"".+?", RegexOptions.Singleline);
                        foreach(System.Text.RegularExpressions.Match itemProduct in mcProduct)
                        {
                            string name = itemProduct.Groups[1].Value.Trim().Replace("'","''");
                            string id = itemProduct.Groups[2].Value.Trim();
                            string url = "https://api.scalablepress.com/v3/products/" + itemProduct.Groups[2].Value.Trim();
                            string nameCategory = item.name;
                            Console.WriteLine(name + "\t" + id + "\t");
                            i++;
                            List<Product> products = new List<Product>();
                            products.Add(new Product(name, id, url, nameCategory));
                            AddProduct(products);
                        }
                    }
                }
            }
            Console.WriteLine(i);
            Console.WriteLine("Đã xong!!!");
        }
    }

}
