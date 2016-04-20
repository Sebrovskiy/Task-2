using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2.Classes;

namespace Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> MyList = Extentions.ReadFileByLine(@"MyText.txt");
            List<Concordance> List = Extentions.GetWordsFromLine(MyLines:MyList, LinesInList:0);
            if (List != null) List.Sort((x, y) => x.Word.CompareTo(y.Word));
            Extentions.WriteResultToFile(List);
            


            Console.ReadLine();
        }
    }
}
