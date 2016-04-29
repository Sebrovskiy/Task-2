using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2_new.Classes;
using Task_2_new.Interfases;
using System.Configuration;

namespace Task_2_new
{
    class Program
    {
        static void Main(string[] args)
        {
            ICollection<IConcordance> MyConcordance = FileHandler.GetWordsFromFileToConcordance
                (InputFilePath: ConfigurationManager.AppSettings["InputFilePath"], LinesOnPage: 10);
            if (MyConcordance != null) MyConcordance = FileHandler.SortConcordance(MyConcordance as List<IConcordance>);
            if (FileHandler.WriteResultToFile(_concordanceList: MyConcordance, _outputFilePaht: ConfigurationManager.AppSettings["OutputFilePath"]) == 1)
            {
                Console.WriteLine("Makink concordance complete suscessfully!");
            }
            else
            {
                Console.WriteLine("Something went wrong...");
            }
            Console.ReadLine();
        }
    }
}
