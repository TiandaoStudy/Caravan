﻿using Finsa.Caravan.Common.Logging.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            CleanUpLogRepository.Invoke(null);
        }
    }
}