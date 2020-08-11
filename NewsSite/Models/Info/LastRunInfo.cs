using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Models.Info
{
    public class LastRunInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime LastRun { get; set; }
    }
}
