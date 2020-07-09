using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.CrawlData
{
    public class Category
    {
        public string name { get; set; }
        public string url { get; set; }
        public string id { get; set; }
        public Category()
        {

        }
        public Category(string name, string url, string id)
        {
            this.url = url;
            this.name = name;
            this.id = id;
        }
    }
}
