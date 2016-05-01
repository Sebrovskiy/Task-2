using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_new.Interfases
{
    public interface IPunctuation : ISentenceItem
    {
        char Item { get; }
    }
}
