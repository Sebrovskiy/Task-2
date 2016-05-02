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
        SentenceType SentenceType { get; }
        List<ISentenceItem> MySentence { get; }
        int GetWordsCount();
    }
}
