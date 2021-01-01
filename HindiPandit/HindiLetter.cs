using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using System.Windows;
using System.Collections.ObjectModel;

namespace HindiPandit
{
        
    public class HindiWordMeaning : IEquatable<HindiWordMeaning>
    {
        public string HindiWord { get; set; }
        public string EnglishMeaning { get; set; }
        public string EnglishReadableWord { get; set; }


        #region IEquatable<HindiWordMeaning> Members

        public bool Equals(HindiWordMeaning other)
        {
            return this.HindiWord == other.HindiWord && this.EnglishMeaning == other.EnglishMeaning;
        }

        #endregion
    }

    class HindiLetter
    {
        public string Letter { get; set; }
        public string ReadableEnglish { get; set; }
    }

    public class Sentence : INotifyPropertyChanged
    {
        static Sentence()
        {
            WordsMeaning = new ObservableCollection<HindiWordMeaning>();
        }

        public static ObservableCollection<HindiWordMeaning> WordsMeaning
        {
            get;
            private set;
        }


        public static Dictionary<string, string> HindiWordReadableEnglishMap = new Dictionary<string, string>();

        public static Dictionary<string, string> HindiLetterReadableEnglishMap = new Dictionary<string, string>();

        private string hindiSentence = string.Empty;

        public string HindiSentence
        {
            get
            {
                return this.hindiSentence;
            }
            set
            {

                if (this.hindiSentence != value)
                {
                    this.hindiSentence = value;

                    string[] hindiWords = value.Trim().Split(new string[] { " ", " । " }, StringSplitOptions.RemoveEmptyEntries);
                    
                    foreach (string hindiWord in hindiWords)
                    {
                        try
                        {
                            string englishMeaning = ""; // translateclient.Translate(hindiWord.Trim(), "hi", "en");

                            if (!HindiWordReadableEnglishMap.ContainsKey(hindiWord))
                                HindiWordReadableEnglishMap[hindiWord] = GetEnglishReadableWord(hindiWord);

                            HindiWordMeaning hwm = new HindiWordMeaning { HindiWord = hindiWord, EnglishReadableWord = HindiWordReadableEnglishMap[hindiWord], EnglishMeaning = englishMeaning };

                            if (!WordsMeaning.Contains(hwm))
                            {
                                WordsMeaning.Add(hwm);
                            }
                        }
                        catch
                        {

                        }

                    }

                    StringBuilder sb = new StringBuilder();

                    if (!string.IsNullOrEmpty(this.hindiSentence))
                    {

                        foreach (char c in this.hindiSentence.ToCharArray())
                        {
                            if (c == ' ' || c == '\n')
                            {
                                if (c == ' ')
                                {
                                    if (sb.Length >= 2 && sb[sb.Length - 1] == 'a' && sb[sb.Length - 2] != 'a')
                                        sb.Remove(sb.Length - 1, 1);

                                    sb.Append(c);
                                }
                            }
                            else
                            {
                                if (c == '।' || c == '्' || c == 'ा' || c == 'ि' || c == 'ी' || c == 'ु' || c == 'ू' || c == 'ृ' || c == 'े' || c == 'ै' || c == 'ो' || c == 'ौ')
                                {
                                    sb.Remove(sb.Length - 1, 1);

                                    if( c != '्' )
                                    sb.Append(HindiLetterReadableEnglishMap[c.ToString()]);
                                }
                                else
                                {
                                    if( HindiLetterReadableEnglishMap.ContainsKey(c.ToString() ))
                                    sb.Append(HindiLetterReadableEnglishMap[c.ToString()]);
                                    else
                                    {
                                        //MessageBox.Show( string.Format("No readable english text is available for the character '{0}'", c) );
                                    }
                                }
                                
                            }
                        }

                        if (sb.Length >= 2 && sb[sb.Length - 1] == 'a' && sb[sb.Length - 2] != 'a')
                            sb.Remove(sb.Length - 1, 1);

                        this.EnglishReadableSentence = sb.ToString().TrimEnd();

                        string [] hindiSentences = this.hindiSentence.Split( new string[] {"।"}, StringSplitOptions.RemoveEmptyEntries);

                        this.EnglishMeaning = string.Empty;

                        //foreach (string hindiSentence in hindiSentences)
                        //{

                        //    try
                        //    {
                        //        this.EnglishMeaning += translateclient.Translate(hindiSentence.Trim(), "hi", "en").TrimEnd() + ". ";
                        //    }
                        //    catch
                        //    {
                        //    }
                        //}

                        //this.EnglishMeaning = this.EnglishMeaning.TrimEnd();
                    }
                    else
                    {
                        this.EnglishReadableSentence = string.Empty;
                        this.EnglishMeaning = string.Empty;
                    }

                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("HindiSentence"));
                }
            }
        }


        public static string GetEnglishReadableWord(string hindiSentence)
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(hindiSentence))
            {
                foreach (char c in hindiSentence.ToCharArray())
                {
                    if (c == ' ' || c == '\n')
                    {
                        if (c == ' ')
                        {
                            if (sb.Length >= 2 && sb[sb.Length - 1] == 'a' && sb[sb.Length - 2] != 'a')
                                sb.Remove(sb.Length - 1, 1);

                            sb.Append(c);
                        }
                    }
                    else
                    {
                        if (c == '।' || c == '्' || c == 'ा' || c == 'ि' || c == 'ी' || c == 'ु' || c == 'ू' || c == 'ृ' || c == 'े' || c == 'ै' || c == 'ो' || c == 'ौ')
                        {
                            sb.Remove(sb.Length - 1, 1);

                            if (c != '्')
                                sb.Append(HindiLetterReadableEnglishMap[c.ToString()]);
                        }
                        else
                        {
                            if (HindiLetterReadableEnglishMap.ContainsKey(c.ToString()))
                                sb.Append(HindiLetterReadableEnglishMap[c.ToString()]);
                            else
                            {
                                MessageBox.Show(string.Format("No readable english text is available for the character '{0}'", c));
                            }
                        }

                    }
                }

                if (sb.Length >= 2 && sb[sb.Length - 1] == 'a' && sb[sb.Length - 2] != 'a')
                    sb.Remove(sb.Length - 1, 1);

                
            }

            return sb.ToString().TrimEnd();

        }

        private string englishReadable;

        public string EnglishReadableSentence
        {
            get
            {
                return this.englishReadable;
            }
            set
            {
                this.englishReadable = value;


                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs("EnglishReadableSentence"));
            }
        }

        private string englishMeaning;

        public string EnglishMeaning
        {
            get
            {
                return this.englishMeaning;
            }
            set
            {
                this.englishMeaning = value;


                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs("EnglishMeaning"));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
