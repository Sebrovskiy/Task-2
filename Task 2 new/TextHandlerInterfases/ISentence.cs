using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2_new.Types;

namespace Task_2_new.Interfases
{
    public interface ISentence
    {
        SentenceType SentenceType { get; set; }
        List<ISentenceItem> MySentence { get; set; }
        int GetWordsCount();
    }
}
