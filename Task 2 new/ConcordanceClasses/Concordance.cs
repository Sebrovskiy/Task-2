using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2_new.Interfases;

namespace Task_2_new.Classes
{
    class Concordance : IConcordance
    {
        public string Word { get; set; }
        public ulong Count { get; set; }
        public StringBuilder Pages { get; set; }
        public int MaxNumberPage { get; set; }
    }
}
