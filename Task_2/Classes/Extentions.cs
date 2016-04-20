using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2.Classes;
using Task_2.Interfaces;

namespace Task_2
{
    public static class Extentions
    {
        public static List<string> ReadFileByLine(string path = @"MyText1.txt")
        {
            try
            {
                List<string> _myList = new List<string>();
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        _myList.Add(line);
                    }
                }
                return _myList;
            }          
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public static List<Concordance> GetWordsFromLine(List<string> MyLines, int LinesInList)
        {
            if (LinesInList > 0)
            {
                try
                {
                    List<Concordance> _myWords = new List<Concordance>();
                    string[] _words; int _lineNumber = 0; int _pageNumber = 1;
                    foreach (var c in MyLines)
                    {
                        _words = c.Split(new[] { ' ', ',', ':', '?', '!', '.', '-' }, StringSplitOptions.RemoveEmptyEntries);
                        _lineNumber += 1; bool _wordAlreadyAdded;
                        if (_lineNumber > LinesInList)
                        {
                            _pageNumber += 1; _lineNumber = 1;
                        }
                        foreach (var x in _words)
                        {
                            _wordAlreadyAdded = false;
                            foreach (var y in _myWords)
                            {
                                if (x.ToLower() == y.Word)
                                {
                                    _wordAlreadyAdded = true;
                                    y.Count += 1;
                                    if (!y.Pages.Contains(_pageNumber))
                                    {
                                        y.Pages.Add(_pageNumber);
                                    }
                                }
                            }
                            if (!_wordAlreadyAdded)
                            {
                                _myWords.Add(new Concordance { Word = x.ToLower(), Count = 1, Pages = new List<int>(new int[] { _pageNumber }) });
                            }
                        }
                    }
                    return _myWords;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
            else 
            {
                Console.WriteLine("GetWordsFromLine: incorrect input data.");
                return null;
            }
        }

        public static void WriteResultToFile(List<Concordance> MyList, string path = @"MyNewText.txt")
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    string _currentLetter = ""; int Lenth = 0; string s = "";
                    foreach (var x in MyList)
                    {
                        if (x.Word[0].ToString().ToUpper() != _currentLetter)
                        {
                            _currentLetter = x.Word[0].ToString().ToUpper();
                            sw.WriteLine(_currentLetter);
                        }
                        sw.Write(x.Word);
                        Lenth = 25 - x.Word.Length;
                        for (int i = 1; i < Lenth; i++)
                        {
                            s += ".";
                        }
                        sw.WriteLine(" {0} {1} : {2}", s, x.Count, ListToString<int>(x.Pages.ToList()));
                        s = "";
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static string ListToString<T>(List<T> MyList)
        {
            string s = "";
            foreach (var x in MyList)
            {
                s += x.ToString() + " ";
            }
            return s;
        }
    }
}
