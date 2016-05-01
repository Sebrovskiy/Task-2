using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Task_2_new.Interfases;

namespace Task_2_new.Classes
{
    public class Text
    {
        private List<ISentence> _mySentence = new List<ISentence>();
        public Text(string InputFilePath)
        {
            //Читаем файл, формируем листы слов, предложений.

            int _bufferSize = 5;
            char[] _buffer = new char[_bufferSize];
            string _currentSentence = "";
            string[] _words;
            int _bytesRead = 0;
            try
            {
                long _fileSize = new FileInfo(InputFilePath).Length;
                FileStream _fileStream = new FileStream(InputFilePath, FileMode.Open, FileAccess.Read);
                using (var sr = new StreamReader(_fileStream))
                {
                    while ((_bytesRead = sr.Read(_buffer, 0, _buffer.Length)) > 0)
                    {
                        foreach (var c in _buffer)
                        {
                            if ((c == '.') || (c == '!') || (c == '?'))
                            {
                                if (_currentSentence.Length > 0)
                                {
                                    _words = _currentSentence.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (_words.Count() > 0)
                                    {
                                        switch (c)
                                        {
                                            case '.':
                                                _mySentence.Add(new Sentence
                                                {
                                                    Words = _words.ToList(),
                                                    SentenceType = Types.SentenceType.affirmative
                                                });
                                                break;
                                            case '?':
                                                _mySentence.Add(new Sentence
                                                {
                                                    Words = _words.ToList(),
                                                    SentenceType = Types.SentenceType.interrogative
                                                });
                                                break;
                                            case '!':
                                                _mySentence.Add(new Sentence
                                                {
                                                    Words = _words.ToList(),
                                                    SentenceType = Types.SentenceType.exclamatory
                                                });
                                                break;
                                        }
                                    }
                                }
                                _currentSentence = "";
                            }
                            else
                            {
                                if (Char.IsLetter(c) || Char.IsNumber(c))
                                {
                                    _currentSentence += c;
                                }
                                else
                                {
                                    _currentSentence += " ";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public ICollection<ISentence> SortSentenceByNumberOfWords()
        {
            List<ISentence> _temp = new List<ISentence>();
           
            if (_mySentence != null)
            {
                var _sortSentence = from t in _mySentence
                                    orderby t.Words.Count()
                                    select t;
                foreach (var c in _sortSentence)
                {
                    _temp.Add(new Sentence {Words = c.Words, SentenceType = c.SentenceType });
                }
            }
            else return null;
            return _temp as ICollection<ISentence>;
        }

        public IEnumerable<string> SelectWordsInInterrogativeSentence(int WordLength)
        {
            List<string> _temp = new List<string>();

            if (_mySentence != null)
            {
                foreach (var c in _mySentence.Where(x=>x.SentenceType == Types.SentenceType.interrogative))
                {
                    foreach (var d in c.Words.Distinct())
                    {
                        if (d.Length == WordLength)
                            _temp.Add(d);
                    }
                }
            }
            else return null;
            return _temp.Distinct();
 
        }

        public ICollection<ISentence> DeleteAllWordsFromTextStartFromConsonantLetter(int WordLength)
        {
            if (_mySentence != null)
            {
                List<ISentence> _temp = new List<ISentence>();
                _temp = _mySentence;
                string _filter = new StringBuilder("\\b[^euioa]\\w{").AppendFormat("{0},{0}", WordLength - 1).Append("}\\b").ToString();
                foreach (var c in _temp)
                {
                    foreach (var x in c.Words.ToArray())
                    {
                        if (Regex.IsMatch(x, _filter, RegexOptions.IgnoreCase))
                        {
                            c.Words.Remove(x);
                        }
                    }
                }
                return _temp;
            }
            else return null;
           
        }

        public string ReplaseWordsOnSubstring(int WordLength, string Substring)
        {
            if (_mySentence != null)
            {
                StringBuilder s = new StringBuilder();
                foreach (var c in _mySentence.First().Words)
                {
                    if (c.Length == WordLength)
                    {
                        s.AppendFormat("{0} ", Substring);
                    }
                    else
                    {
                        s.AppendFormat("{0} ", c);
                    }
                }
                return s.ToString();
            }
            else return null;
        }
    }
}
        

