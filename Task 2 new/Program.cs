using System;
using System.Collections.Generic;

using Task_2_new.Interfases;
using System.Configuration;
using Task_2_new.Classes;

namespace Task_2_new
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                #region Task 1
                Text t = new Text(InputFilePath: ConfigurationManager.AppSettings["InputFilePath"]);

                ICollection<ISentence> sort = t.SortSentenceByNumberOfWords();

                IEnumerable<string> select = t.SelectWordsInInterrogativeSentence(WordLength: 3);

                ICollection<ISentence> delete = t.DeleteAllWordsFromTextStartFromConsonantLetter(3);

                string NewString = t.ReplaseWordsOnSubstring(WordLength: 3, Substring: "substr");
                #endregion

                #region Task 2
                Console.WriteLine("Try to make file concordance");
                ICollection<IConcordance> MyConcordance = MakeConcordance.GetWordsFromFileToConcordance
                    (InputFilePath: ConfigurationManager.AppSettings["InputFilePath"], LinesOnPage: 10);
                MyConcordance = MakeConcordance.SortConcordance(MyConcordance as List<IConcordance>);
                MakeConcordance.WriteResultToFile(_concordanceList: MyConcordance, _outputFilePaht: ConfigurationManager.AppSettings["OutputFilePath"]);
                Console.WriteLine("Making concordance complete suscessfully!");
                #endregion
 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }       
            

            Console.ReadLine();
        }
    }
}
