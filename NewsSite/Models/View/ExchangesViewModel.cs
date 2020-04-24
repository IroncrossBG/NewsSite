namespace NewsSite.Models.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ExchangesViewModel
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string PerUnit { get; set; }

        public string Course { get; set; }

        public string ReverseCourse { get; set; }
    }
}
