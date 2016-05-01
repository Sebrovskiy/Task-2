using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2_new.Interfases;
using Task_2_new.Types;

namespace Task_2_new.Classes
{
    public class Sentence:ISentence
    {
        public SentenceType SentenceType
        {
            get;
            set;
        }
        public List<string> Words
        {
            get;
            set;
        }
    }
}
