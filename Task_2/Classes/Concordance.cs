using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2.Interfaces;

namespace Task_2.Classes
{
    public class Concordance:IConcordance
    {
        public string Word { get; set; }
        public int Count { get; set; }
        public ICollection<int> Pages { get; set; }
    }
}
