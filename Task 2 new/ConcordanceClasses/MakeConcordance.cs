using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2_new.Interfases;

namespace Task_2_new.Classes
{
    public static class MakeConcordance
    {
        public static ICollection<IConcordance> GetWordsFromFileToConcordance(string InputFilePath, int LinesOnPage)
        {
            int _bufferSize = 1024;
            char[] _buffer = new char[_bufferSize];
            string[] _words;
            string _currentLine = "";
            int _lineNumber = 0;
            int _currentPage = 1;
            bool _wordAlreadyAdded;
            int _bytesRead = 0;
            List<string> _linesList = new List<string>();
            List<IConcordance> _concordanceList = new List<IConcordance>();
            try
            {
                FileStream _fileStream = new FileStream(InputFilePath, FileMode.Open, FileAccess.Read);
                using (var sr = new StreamReader(_fileStream))
                {                             
                    while ((_bytesRead = sr.Read(_buffer, 0, _buffer.Length)) > 0)
                    {   
                        #region TakeLinesFromBuffer
                        
                        foreach (var c in _buffer)
                        {
                            if (c == '\n')
                            {
                                _currentLine += c;
                                _linesList.Add(_currentLine);
                                _currentLine = "";
                            }
                            else
                            {
                                _currentLine += c;
                            }                           
                        }


                        if (!_buffer.ToString().Contains("\n") && _linesList.Count() == 0) //If line is bigger then buffer
                        {
                            _linesList.Add(_currentLine);
                            if (_lineNumber > 0)
                            {
                                _lineNumber--;
                            }
                        }

                        if (_bytesRead < _bufferSize)
                        {
                            _linesList.Add(_currentLine);
                        }
                        #endregion

                        #region PutWordsFromLineToConcordance

                        if (_linesList.Count() > 0)
                        {                           
                            foreach (var c in _linesList.ToArray())
                            {
                                _words = c.Split(new[] { ' ', ',', ':', '?', '!', '.', '-', '\n', '\r', '\0' },
                                                        StringSplitOptions.RemoveEmptyEntries);
                                _lineNumber += 1;
                                 if (_lineNumber > LinesOnPage) // Check the begining of new page
                                 {
                                    _currentPage += 1; _lineNumber = 1;
                                 }
                                 foreach (var x in _words)
                                 {
                                    _wordAlreadyAdded = false;
                                    foreach (var y in _concordanceList)
                                    {
                                        if (x.ToLower() == y.Word)  //Is current world already added?
                                        {
                                            _wordAlreadyAdded = true;
                                            y.Count += 1;
                                            if (y.MaxNumberPage != _currentPage)
                                            {
                                                y.Pages.AppendFormat(",{0}", _currentPage);
                                                y.MaxNumberPage = _currentPage;
                                            }
                                         }
                                     }
                                     if (!_wordAlreadyAdded)
                                     {
                                        _concordanceList.Add(new Concordance
                                        {
                                            Word = x.ToLower(),
                                            Count = 1,
                                            Pages = new StringBuilder(_currentPage.ToString()),
                                            MaxNumberPage = _currentPage
                                        });
                                     }
                                  }
                                    _linesList.Remove(c);
                               } 
                        }
                        _buffer = new char[_bufferSize];
                        #endregion
                    }
                }
                return _concordanceList;         
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }           
        }
        public static void WriteResultToFile(ICollection<IConcordance> _concordanceList, string _outputFilePaht)
        {
            if (_concordanceList != null)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(_outputFilePaht, false, System.Text.Encoding.Default))
                    {
                        string _currentLetter = "";
                        foreach (var x in _concordanceList)
                        {
                            if (x.Word[0].ToString().ToUpper() != _currentLetter)
                            {
                                _currentLetter = x.Word[0].ToString().ToUpper();
                                sw.WriteLine(_currentLetter);
                            }
                            sw.WriteLine(" {0} {1} : {2} ", x.Word, x.Count, x.Pages);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }       
            }
        }
        public static ICollection<IConcordance> SortConcordance(List<IConcordance> _concordanceList)
        {
            if (_concordanceList != null)
            {
                _concordanceList.Sort((x, y) => x.Word.CompareTo(y.Word));
            }
            else return null;

            return _concordanceList as ICollection<IConcordance>;
        }

     
    }
}
