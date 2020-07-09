using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class ResponseDesign
    {
        public string source { get; set; }
        public string mode { get; set; }
        public string type { get; set; }
        public DateTime createdAt { get; set; }
        public Validation validation { get; set; }
        public List<object> customization { get; set; }
        public bool conversion { get; set; }
        public bool review { get; set; }
        public Sides sides { get; set; }
        public string designId { get; set; }
    }
    public class Validation
    {
        public string status { get; set; }
        public object result { get; set; }

    }

    public class Order
    {
        public string status { get; set; }
        public List<object> notes { get; set; }

    }

    public class Artwork
    {
        public string status { get; set; }
        public List<object> notes { get; set; }

    }

    public class Approval
    {
        public Order order { get; set; }
        public Artwork artwork { get; set; }

    }

    public class Dimensions
    {
        public int width { get; set; }

    }

    public class Offset
    {
        public double top { get; set; }

    }

    public class Position
    {
        public string horizontal { get; set; }
        public Offset offset { get; set; }

    }

    public class Fronts
    {
        public Approval approval { get; set; }
        public string artwork { get; set; }
        public Dimensions dimensions { get; set; }
        public Position position { get; set; }
        public bool resize { get; set; }
        public string artworkId { get; set; }
        public string proof { get; set; }
        public double aspect { get; set; }

    }

    public class Sides
    {
        public Fronts front { get; set; }

    }

}
