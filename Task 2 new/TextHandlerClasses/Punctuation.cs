using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2_new.Interfases;

namespace Task_2_new.Classes
{
    public class Punctuation:ISentenceItem, IPunctuation
    {
        public char Item {get; set;}
    }
}
