using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.CrawlData
{
    public class Product
    { 
        public string nameProduct { get; set; }
        public string idProduct { get; set; }
        public string urlProduct { get; set; }
        public string nameCategory { get; set; }

        public Product()
        {

        }
        public Product(string nameProduct, string idProduct, string urlProduct, string nameCategory)
        {
            this.nameCategory = nameCategory;
            this.idProduct = idProduct;
            this.urlProduct = urlProduct;
            this.nameProduct = nameProduct;
        }
    }
}
