using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class ResponseOrder
    {
            public double total { get; set; }
            public double subtotal { get; set; }
            public int fees { get; set; }
            public double tax { get; set; }
            public double shipping { get; set; }
            public List<Breakdown> breakdown { get; set; }
            public List<Sla> sla { get; set; }
            public List<Dimension> dimensions { get; set; }
            public List<string> facilities { get; set; }
            public string orderToken { get; set; }
            public List<object> warnings { get; set; }
            public string mode { get; set; }


    }
    public class Breakdown
    {
        public int fees { get; set; }
        public double shipping { get; set; }
        public double printing { get; set; }
        public double blanks { get; set; }
        public double tax { get; set; }

    }

    public class Sla
    {
        public int days { get; set; }

    }

    public class Front
    {
        public int width { get; set; }

    }

    public class Dimension
    {
        public Front front { get; set; }

    }

}
