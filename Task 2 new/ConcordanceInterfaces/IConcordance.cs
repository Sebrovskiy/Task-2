using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_new.Interfases
{
    public interface IConcordance
    {
        string Word { get; }
        ulong Count { get; set; }
        StringBuilder Pages { get; }
        int MaxNumberPage { get; set; }


    }
}
