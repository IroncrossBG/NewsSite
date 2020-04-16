using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Models.View
{
    public class ExchangesViewModel
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public int PerUnit { get; set; }

        public double Course { get; set; }

        public double ReverseCourse { get; set; }
    }
}
