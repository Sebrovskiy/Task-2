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
            int _bufferSize = 1024;
            char[] _buffer = new char[_bufferSize];
            string _currentWord = "";
            List<ISentenceItem> _currentListSentence = new List<ISentenceItem>();
            int _bytesRead = 0;
            try
            {
                FileStream _fileStream = new FileStream(InputFilePath, FileMode.Open, FileAccess.Read);
                using (var sr = new StreamReader(_fileStream))
                {
                    while ((_bytesRead = sr.Read(_buffer, 0, _buffer.Length)) > 0)
                    {
                        foreach (var c in _buffer)  // Check buffer element by type
                        {
                            if (Char.IsPunctuation(c))
                            {
                                if (_currentWord != "") //If buffer element is punctuation and before it was a word,
                                {                       // add this word and punctuation element to list 
                                    _currentListSentence.Add(new Word { MyWord = _currentWord });
                                    _currentWord = "";
                                    _currentListSentence.Add(new Punctuation { Item = c });
                                } 
                                
                            }
                            if (Char.IsLetterOrDigit(c))
                            {
                                _currentWord += c;
                            }
                            if (Char.IsSeparator(c))
                            {
                                if (_currentWord != "")// If buffer element is separator and before it was a word, add this word to list
                                {
                                    _currentListSentence.Add(new Word { MyWord = _currentWord });
                                    _currentWord = "";
                                } 
                            }

                            if ((c == '.') || (c == '!') || (c == '?')) // Check for end of sentence
                            {
                                if (_currentListSentence.Count > 1) // If current sentence conteins something else besides ".", "!", "?",
                                {                                   // add sentense to list
                                    switch (c)
                                    {
                                        case '.':
                                            _mySentence.Add(new Sentence
                                            {
                                                MySentence = _currentListSentence,
                                                SentenceType = Types.SentenceType.affirmative
                                            });
                                            _currentListSentence = new List<ISentenceItem>();
                                            break;
                                        case '?':
                                            _mySentence.Add(new Sentence
                                            {
                                                MySentence = _currentListSentence,
                                                SentenceType = Types.SentenceType.interrogative
                                            });
                                            _currentListSentence = new List<ISentenceItem>();
                                            break;
                                        case '!':
                                            _mySentence.Add(new Sentence
                                            {
                                                MySentence = _currentListSentence,
                                                SentenceType = Types.SentenceType.exclamatory
                                            });
                                            _currentListSentence = new List<ISentenceItem>();
                                            break;
                                    }
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ICollection<ISentence> SortSentenceByNumberOfWords()
        {
            List<ISentence> _temp = new List<ISentence>();

            if (_mySentence != null)
            {
                var _sortSentence = from t in _mySentence
                                    orderby t.GetWordsCount()
                                    select t;
                foreach (var c in _sortSentence)
                {
                    _temp.Add(new Sentence { MySentence = c.MySentence, SentenceType = c.SentenceType });
                }
            }
            else return null;
            return _temp as ICollection<ISentence>;
        }
        public ICollection<string> SelectWordsInInterrogativeSentence(int WordLength)
        {
            List<string> _temp = new List<string>();

            if (_mySentence != null)
            {
                foreach (var c in _mySentence.Where(x => x.SentenceType == Types.SentenceType.interrogative)) //Select interrogative sentences
                {
                    foreach (var d in c.MySentence)
                    {
                        if (d is IWord)
                        {
                            if (((d as IWord).MyWord.Length == WordLength) & !_temp.Contains((d as IWord).MyWord))
                                _temp.Add((d as IWord).MyWord);
                        }          
                    }
                }
            }
            else return null;
            return _temp;

        }
        public ICollection<ISentence> DeleteAllWordsFromTextStartFromConsonantLetter(int WordLength)
        {
            List<ISentence> _temp = new List<ISentence>();
            _temp = _mySentence;
            if (_mySentence != null)
            {   // Build RegularExpressions
                string _filter = new StringBuilder("\\b[^euioa]\\w{").AppendFormat("{0},{0}", WordLength - 1).Append("}\\b").ToString();
                foreach (var c in _temp)
                {
                    foreach (var d in c.MySentence.ToArray())
                    {
                        if (d is IWord)
                        {
                            if (Regex.IsMatch((d as IWord).MyWord, _filter, RegexOptions.IgnoreCase))
                            {
                                c.MySentence.Remove(d);
                            }
                        }
                    }
                }
            }
            return _temp;
        }
        public string ReplaseWordsOnSubstring(int WordLength, string Substring)
        {
            if (_mySentence != null)
            {
                ISentence _temtSentence = _mySentence.First(); //Take sentence, first for example
                StringBuilder _sb = new StringBuilder(); 
                foreach (var c in _temtSentence.MySentence)
                {
                    if (c is IWord)
                    if ((c as IWord).MyWord.Length == WordLength) 
                    {
                        _sb.AppendFormat("{0} ", Substring); 
                    }
                    else
                    {
                        _sb.AppendFormat("{0} ", (c as IWord).MyWord);
                    }
                }
                return _sb.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
        

