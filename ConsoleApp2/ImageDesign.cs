using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class ImageDesign
    {
        string id { get; set; }
        string name { get; set; }
        string link { get; set; }
        string note { get; set; }
        string dateCreat { get; set; }

        public ImageDesign()
        {

        }


        public ImageDesign(string id, string name, string link, string note, string dateCreat)
        {
            this.id = id;
            this.name = name;
            this.link = link;
            this.name = name;
            this.dateCreat = dateCreat;
        }

    }
}
