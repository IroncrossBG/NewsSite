﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Services
{
    public interface IScrapperService
    {
        public void RunSegaScrapper(DateTime from, DateTime to);
    }
}