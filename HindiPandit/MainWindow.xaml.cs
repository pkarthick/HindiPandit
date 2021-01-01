using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

using System.IO;
using System.Windows.Controls.Primitives;
using System.Net;
using System.Text.RegularExpressions;
using System.IO.Packaging;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using Microsoft.Win32;

namespace HindiPandit
{

    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private Dictionary<string, List<HindiLetter>> hindiMap = new Dictionary<string, List<HindiLetter>>();

        private ObservableCollection<HindiLetter> consonantsCollection = new ObservableCollection<HindiLetter>();

        public MainWindow()
        {
            InitializeComponent();
            PopulateDictionary();

            //this.CurrentSentence = new Sentence();
            this.sentences.Add(new Sentence());

            this.SentencesControl.DataContext = this.sentences;
            this.TransliteratedWords.DataContext = this.transliteratedWords;

            
            
        }

        void RichTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            this.currentTextBox = sender as RichTextBox;
            this.currentTextBox.Focus();
        }

        void RichTextBox_Initialized(object sender, EventArgs e)
        {
            this.currentTextBox = sender as RichTextBox;   
        }


        void RichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            this.currentTextBox = sender as RichTextBox;

            //if (this.currentTextBox != null)
            //{
            //    if (this.currentTextBox.Selection.IsEmpty)
            //    {
            //        TextPointer tp = this.currentTextBox.Selection.Start;


            //        if (tp.Parent is Run)
            //        {
            //            int forwardOffset = this.currentTextBox.Selection.Start.GetTextRunLength(LogicalDirection.Forward);
            //            int backwardOffset = this.currentTextBox.Selection.Start.GetTextRunLength(LogicalDirection.Backward);

            //            string text = ((Run)tp.Parent).Text;

            //            int startIndex = text.LastIndexOf(' ', forwardOffset);
            //            int endIndex = text.IndexOf(' ', backwardOffset - 1);

            //            TextPointer selectionStart = this.currentTextBox.Selection.Start.GetPositionAtOffset(forwardOffset, LogicalDirection.Forward);
            //            TextPointer selectionEnd = this.currentTextBox.Selection.End.GetPositionAtOffset(backwardOffset, LogicalDirection.Forward);
                        
            //            this.currentTextBox.Selection.Select(selectionStart, selectionEnd);

            //        }


            //    }
            //}
        }

        private void InsertTextAtCurrentPosition(string text)
        {
            if (this.currentTextBox != null)
            {
                if (this.currentTextBox.Selection.IsEmpty)
                {
                    this.currentTextBox.AppendText(text);

                    //new TextRange(this.currentTextBox.Selection.Start, this.currentTextBox.Selection.End).Text = text;
                    //TextPointer startPointer = this.currentTextBox.Selection.Start.GetPositionAtOffset(text.Length);

                    //if (startPointer != null)
                    //    startPointer = startPointer.GetInsertionPosition(LogicalDirection.Forward);
                    
                    //if (startPointer == null)
                    //    startPointer = this.currentTextBox.Selection.End.GetInsertionPosition(LogicalDirection.Forward);
                    

                    //if( startPointer != null)
                    //    this.currentTextBox.Selection.Select(startPointer, startPointer);
                    //else
                    //    this.currentTextBox.Selection.Select(this.currentTextBox.Document.ContentEnd.GetNextInsertionPosition(LogicalDirection.Backward), this.currentTextBox.Document.ContentEnd.GetNextInsertionPosition(LogicalDirection.Backward));

                    //int endPositionOffset = this.currentTextBox.Selection.End.GetOffsetToPosition(this.currentTextBox.Document.ContentEnd.GetInsertionPosition(LogicalDirection.Forward));

                    //if (endPositionOffset == 0)
                    //{
                    //    this.currentTextBox.Document.ContentEnd.GetInsertionPosition(LogicalDirection.Forward).InsertTextInRun(text);
                    //    this.currentTextBox.Selection.Select(this.currentTextBox.Document.ContentEnd, this.currentTextBox.Document.ContentEnd);
                    //}
                    //else
                    //{
                    //    this.currentTextBox.Selection.Start.InsertTextInRun(text);
                    //    this.currentTextBox.Selection.Select(this.currentTextBox.Selection.Start.GetNextInsertionPosition(LogicalDirection.Forward), this.currentTextBox.Selection.Start.GetNextInsertionPosition(LogicalDirection.Forward));
                    //}
                }
                else
                {
                    new TextRange(this.currentTextBox.Selection.Start, this.currentTextBox.Selection.End).Text = text;
                    //TextPointer startPointer = this.currentTextBox.Selection.Start.GetPositionAtOffset(text.Length);

                    //if (startPointer != null)
                    //    startPointer = startPointer.GetInsertionPosition(LogicalDirection.Forward);
                    //else
                    //    startPointer = this.currentTextBox.Selection.End.GetInsertionPosition(LogicalDirection.Forward);

                    //this.currentTextBox.Selection.Select(startPointer, startPointer);


                }

                //this.currentTextBox.Focus();

            }
        }


        void RichTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.currentTextBox = sender as RichTextBox;

            if (this.currentTextBox != null)
            {
                this.currentTextBox.Background = Brushes.LightYellow;
            }

        }


        void RichTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.currentTextBox = sender as RichTextBox;

            if (this.currentTextBox != null)
            {
                this.currentTextBox.Background = Brushes.LightYellow;
            }

        }
        

        private void PopulateDictionary()
        {

            hindiMap.Add("क",
                new List<HindiLetter>
                {
                    new HindiLetter{ Letter="क", ReadableEnglish="ka" },
                    new HindiLetter{ Letter="का", ReadableEnglish="kaa" },
                    new HindiLetter{ Letter="कि", ReadableEnglish="ki" },
                    new HindiLetter{ Letter="की", ReadableEnglish="kee" },
                    new HindiLetter{ Letter="कु", ReadableEnglish="ku" },
                    new HindiLetter{ Letter="कू", ReadableEnglish="koo" },
                    new HindiLetter{ Letter="कृ", ReadableEnglish="kru" },
                    new HindiLetter{ Letter="के", ReadableEnglish="ke" },
                    new HindiLetter{ Letter="कै", ReadableEnglish="kai" },
                    new HindiLetter{ Letter="को", ReadableEnglish="ko" },
                    new HindiLetter{ Letter="कौ", ReadableEnglish="kau" },
                    new HindiLetter{ Letter="कं", ReadableEnglish="kam" },
                    new HindiLetter{ Letter="कः", ReadableEnglish="kah" },
                }
            );


            hindiMap.Add("ख",
               new List<HindiLetter>
                {
                    new HindiLetter{ Letter="ख", ReadableEnglish="kha" },
                    new HindiLetter{ Letter="खा", ReadableEnglish="khaa" },
                    new HindiLetter{ Letter="खि", ReadableEnglish="khi" },
                    new HindiLetter{ Letter="खी", ReadableEnglish="khee" },
                    new HindiLetter{ Letter="खु", ReadableEnglish="khu" },
                    new HindiLetter{ Letter="खू", ReadableEnglish="khoo" },
                    new HindiLetter{ Letter="खृ", ReadableEnglish="khru" },
                    new HindiLetter{ Letter="खे", ReadableEnglish="khe" },
                    new HindiLetter{ Letter="खै", ReadableEnglish="khai" },
                    new HindiLetter{ Letter="खो", ReadableEnglish="kho" },
                    new HindiLetter{ Letter="खौ", ReadableEnglish="khau" },
                    new HindiLetter{ Letter="खं", ReadableEnglish="kham" },
                    new HindiLetter{ Letter="खः", ReadableEnglish="khah" },
                }
           );
            
            hindiMap.Add("ग",
            new List<HindiLetter>
{
new HindiLetter{ Letter="ग", ReadableEnglish="ga" },
new HindiLetter{ Letter="गा", ReadableEnglish="gaa" },
new HindiLetter{ Letter="गि", ReadableEnglish="gi" },
new HindiLetter{ Letter="गी", ReadableEnglish="gee" },
new HindiLetter{ Letter="गु", ReadableEnglish="gu" },
new HindiLetter{ Letter="गू", ReadableEnglish="goo" },
new HindiLetter{ Letter="गृ", ReadableEnglish="gru" },
new HindiLetter{ Letter="गे", ReadableEnglish="ge" },
new HindiLetter{ Letter="गै", ReadableEnglish="gai" },
new HindiLetter{ Letter="गो", ReadableEnglish="go" },
new HindiLetter{ Letter="गौ", ReadableEnglish="gau" },
new HindiLetter{ Letter="गं", ReadableEnglish="gam" },
new HindiLetter{ Letter="गः", ReadableEnglish="gah" },
}
            );
            
            hindiMap.Add("घ",
            new List<HindiLetter>
{
new HindiLetter{ Letter="घ", ReadableEnglish="gha" },
new HindiLetter{ Letter="घा", ReadableEnglish="ghaa" },
new HindiLetter{ Letter="घि", ReadableEnglish="ghi" },
new HindiLetter{ Letter="घी", ReadableEnglish="ghee" },
new HindiLetter{ Letter="घु", ReadableEnglish="ghu" },
new HindiLetter{ Letter="घू", ReadableEnglish="ghoo" },
new HindiLetter{ Letter="घृ", ReadableEnglish="ghru" },
new HindiLetter{ Letter="घे", ReadableEnglish="ghe" },
new HindiLetter{ Letter="घै", ReadableEnglish="ghai" },
new HindiLetter{ Letter="घो", ReadableEnglish="gho" },
new HindiLetter{ Letter="घौ", ReadableEnglish="ghau" },
new HindiLetter{ Letter="घं", ReadableEnglish="gham" },
new HindiLetter{ Letter="घः", ReadableEnglish="ghah" },
}
            );


            hindiMap.Add("ङ",
            new List<HindiLetter>
{
new HindiLetter{ Letter="ङ", ReadableEnglish="nga" },
new HindiLetter{ Letter="ङा", ReadableEnglish="ngaa" },
new HindiLetter{ Letter="ङि", ReadableEnglish="ngi" },
new HindiLetter{ Letter="ङी", ReadableEnglish="ngee" },
new HindiLetter{ Letter="ङु", ReadableEnglish="ngu" },
new HindiLetter{ Letter="ङू", ReadableEnglish="ngoo" },
new HindiLetter{ Letter="ङ्रु", ReadableEnglish="ngru" },
new HindiLetter{ Letter="ङे", ReadableEnglish="nge" },
new HindiLetter{ Letter="ङै", ReadableEnglish="ngai" },
new HindiLetter{ Letter="ङो", ReadableEnglish="ngo" },
new HindiLetter{ Letter="ङौ", ReadableEnglish="ngau" },
new HindiLetter{ Letter="ङं", ReadableEnglish="ngam" },
new HindiLetter{ Letter="ङः", ReadableEnglish="ngah" },
}
            );



            
            hindiMap.Add("च",
            new List<HindiLetter>
{
new HindiLetter{ Letter="च", ReadableEnglish="ca" },
new HindiLetter{ Letter="चा", ReadableEnglish="caa" },
new HindiLetter{ Letter="चि", ReadableEnglish="ci" },
new HindiLetter{ Letter="ची", ReadableEnglish="cee" },
new HindiLetter{ Letter="चु", ReadableEnglish="cu" },
new HindiLetter{ Letter="चू", ReadableEnglish="coo" },
new HindiLetter{ Letter="चृ", ReadableEnglish="cru" },
new HindiLetter{ Letter="चे", ReadableEnglish="ce" },
new HindiLetter{ Letter="चै", ReadableEnglish="cai" },
new HindiLetter{ Letter="चो", ReadableEnglish="co" },
new HindiLetter{ Letter="चौ", ReadableEnglish="cau" },
new HindiLetter{ Letter="चं", ReadableEnglish="cam" },
new HindiLetter{ Letter="चः", ReadableEnglish="cah" },
}
            );
            
            hindiMap.Add("छ",
            new List<HindiLetter>
{
new HindiLetter{ Letter="छ", ReadableEnglish="cha" },
new HindiLetter{ Letter="छा", ReadableEnglish="chaa" },
new HindiLetter{ Letter="छि", ReadableEnglish="chi" },
new HindiLetter{ Letter="छी", ReadableEnglish="chee" },
new HindiLetter{ Letter="छु", ReadableEnglish="chu" },
new HindiLetter{ Letter="छू", ReadableEnglish="choo" },
new HindiLetter{ Letter="छृ", ReadableEnglish="chru" },
new HindiLetter{ Letter="छे", ReadableEnglish="che" },
new HindiLetter{ Letter="छै", ReadableEnglish="chai" },
new HindiLetter{ Letter="छो", ReadableEnglish="cho" },
new HindiLetter{ Letter="छौ", ReadableEnglish="chau" },
new HindiLetter{ Letter="छं", ReadableEnglish="cham" },
new HindiLetter{ Letter="छः", ReadableEnglish="chah" },
}
            );
            
            hindiMap.Add("ज",
            new List<HindiLetter>
{
new HindiLetter{ Letter="ज", ReadableEnglish="ja" },
new HindiLetter{ Letter="जा", ReadableEnglish="jaa" },
new HindiLetter{ Letter="जि", ReadableEnglish="ji" },
new HindiLetter{ Letter="जी", ReadableEnglish="jee" },
new HindiLetter{ Letter="जु", ReadableEnglish="ju" },
new HindiLetter{ Letter="जू", ReadableEnglish="joo" },
new HindiLetter{ Letter="जृ", ReadableEnglish="jru" },
new HindiLetter{ Letter="जे", ReadableEnglish="je" },
new HindiLetter{ Letter="जै", ReadableEnglish="jai" },
new HindiLetter{ Letter="जो", ReadableEnglish="jo" },
new HindiLetter{ Letter="जौ", ReadableEnglish="jau" },
new HindiLetter{ Letter="जं", ReadableEnglish="jam" },
new HindiLetter{ Letter="जः", ReadableEnglish="jah" },
}
            );
            
            hindiMap.Add("झ",
            new List<HindiLetter>
{
new HindiLetter{ Letter="झ", ReadableEnglish="jha" },
new HindiLetter{ Letter="झा", ReadableEnglish="jhaa" },
new HindiLetter{ Letter="झि", ReadableEnglish="jhi" },
new HindiLetter{ Letter="झी", ReadableEnglish="jhee" },
new HindiLetter{ Letter="झु", ReadableEnglish="jhu" },
new HindiLetter{ Letter="झू", ReadableEnglish="jhoo" },
new HindiLetter{ Letter="झृ", ReadableEnglish="jhru" },
new HindiLetter{ Letter="झे", ReadableEnglish="jhe" },
new HindiLetter{ Letter="झै", ReadableEnglish="jhai" },
new HindiLetter{ Letter="झो", ReadableEnglish="jho" },
new HindiLetter{ Letter="झौ", ReadableEnglish="jhau" },
new HindiLetter{ Letter="झं", ReadableEnglish="jham" },
new HindiLetter{ Letter="झः", ReadableEnglish="jhah" },
}
            );

            hindiMap.Add("ञ",
                       new List<HindiLetter>
{
new HindiLetter{ Letter="ञ", ReadableEnglish="ña" },
new HindiLetter{ Letter="ञा", ReadableEnglish="ñaa" },
new HindiLetter{ Letter="ञि", ReadableEnglish="ñi" },
new HindiLetter{ Letter="ञी", ReadableEnglish="ñee" },
new HindiLetter{ Letter="ञु", ReadableEnglish="ñu" },
new HindiLetter{ Letter="ञू", ReadableEnglish="ñoo" },
new HindiLetter{ Letter="ञृ", ReadableEnglish="ñru" },
new HindiLetter{ Letter="ञॆ", ReadableEnglish="ñe" },
new HindiLetter{ Letter="ञै", ReadableEnglish="ñai" },
new HindiLetter{ Letter="ञो", ReadableEnglish="ño" },
new HindiLetter{ Letter="ञौ", ReadableEnglish="ñau" },
new HindiLetter{ Letter="ञं", ReadableEnglish="ñam" },
new HindiLetter{ Letter="ञः", ReadableEnglish="ñah" },
}
                       );
            

            hindiMap.Add("ट",
            new List<HindiLetter>
{
new HindiLetter{ Letter="ट", ReadableEnglish="Ta" },
new HindiLetter{ Letter="टा", ReadableEnglish="Taa" },
new HindiLetter{ Letter="टि", ReadableEnglish="Ti" },
new HindiLetter{ Letter="टी", ReadableEnglish="Tee" },
new HindiLetter{ Letter="टु", ReadableEnglish="Tu" },
new HindiLetter{ Letter="टू", ReadableEnglish="Too" },
new HindiLetter{ Letter="टृ", ReadableEnglish="Tru" },
new HindiLetter{ Letter="टे", ReadableEnglish="Te" },
new HindiLetter{ Letter="टै", ReadableEnglish="Tai" },
new HindiLetter{ Letter="टो", ReadableEnglish="To" },
new HindiLetter{ Letter="टौ", ReadableEnglish="Tau" },
new HindiLetter{ Letter="टं", ReadableEnglish="Tam" },
new HindiLetter{ Letter="टः", ReadableEnglish="Tah" },
}
            );
            
            hindiMap.Add("ठ",
            new List<HindiLetter>
{
new HindiLetter{ Letter="ठ", ReadableEnglish="Tha" },
new HindiLetter{ Letter="ठा", ReadableEnglish="Thaa" },
new HindiLetter{ Letter="ठि", ReadableEnglish="Thi" },
new HindiLetter{ Letter="ठी", ReadableEnglish="Thee" },
new HindiLetter{ Letter="ठु", ReadableEnglish="Thu" },
new HindiLetter{ Letter="ठू", ReadableEnglish="Thoo" },
new HindiLetter{ Letter="ठृ", ReadableEnglish="Thru" },
new HindiLetter{ Letter="ठे", ReadableEnglish="The" },
new HindiLetter{ Letter="ठै", ReadableEnglish="Thai" },
new HindiLetter{ Letter="ठो", ReadableEnglish="Tho" },
new HindiLetter{ Letter="ठौ", ReadableEnglish="Thau" },
new HindiLetter{ Letter="ठं", ReadableEnglish="Tham" },
new HindiLetter{ Letter="ठः", ReadableEnglish="Thah" },
}
            );
            
            hindiMap.Add("ड",
            new List<HindiLetter>
{
new HindiLetter{ Letter="ड", ReadableEnglish="Da" },
new HindiLetter{ Letter="डा", ReadableEnglish="Daa" },
new HindiLetter{ Letter="डि", ReadableEnglish="Di" },
new HindiLetter{ Letter="डी", ReadableEnglish="Dee" },
new HindiLetter{ Letter="डु", ReadableEnglish="Du" },
new HindiLetter{ Letter="डू", ReadableEnglish="Doo" },
new HindiLetter{ Letter="डृ", ReadableEnglish="Dru" },
new HindiLetter{ Letter="डे", ReadableEnglish="De" },
new HindiLetter{ Letter="डै", ReadableEnglish="Dai" },
new HindiLetter{ Letter="डो", ReadableEnglish="Do" },
new HindiLetter{ Letter="डौ", ReadableEnglish="Dau" },
new HindiLetter{ Letter="डं", ReadableEnglish="Dam" },
new HindiLetter{ Letter="डः", ReadableEnglish="Dah" },
}
            );
            
            hindiMap.Add("ढ",
            new List<HindiLetter>
{
new HindiLetter{ Letter="ढ", ReadableEnglish="Dha" },
new HindiLetter{ Letter="ढा", ReadableEnglish="Dhaa" },
new HindiLetter{ Letter="ढि", ReadableEnglish="Dhi" },
new HindiLetter{ Letter="ढी", ReadableEnglish="Dhee" },
new HindiLetter{ Letter="ढु", ReadableEnglish="Dhu" },
new HindiLetter{ Letter="ढू", ReadableEnglish="Dhoo" },
new HindiLetter{ Letter="ढृ", ReadableEnglish="Dhru" },
new HindiLetter{ Letter="ढे", ReadableEnglish="Dhe" },
new HindiLetter{ Letter="ढै", ReadableEnglish="Dhai" },
new HindiLetter{ Letter="ढो", ReadableEnglish="Dho" },
new HindiLetter{ Letter="ढौ", ReadableEnglish="Dhau" },
new HindiLetter{ Letter="ढं", ReadableEnglish="Dham" },
new HindiLetter{ Letter="ढः", ReadableEnglish="Dhah" },
}
            );
            
            hindiMap.Add("ण",
            new List<HindiLetter>
{
new HindiLetter{ Letter="ण", ReadableEnglish="Ṇa" },
new HindiLetter{ Letter="णा", ReadableEnglish="Ṇaa" },
new HindiLetter{ Letter="णि", ReadableEnglish="Ṇi" },
new HindiLetter{ Letter="णी", ReadableEnglish="Ṇee" },
new HindiLetter{ Letter="णु", ReadableEnglish="Ṇu" },
new HindiLetter{ Letter="णू", ReadableEnglish="Ṇoo" },
new HindiLetter{ Letter="णृ", ReadableEnglish="Ṇru" },
new HindiLetter{ Letter="णे", ReadableEnglish="Ṇe" },
new HindiLetter{ Letter="णै", ReadableEnglish="Ṇai" },
new HindiLetter{ Letter="णो", ReadableEnglish="Ṇo" },
new HindiLetter{ Letter="णौ", ReadableEnglish="Ṇau" },
new HindiLetter{ Letter="णं", ReadableEnglish="Ṇam" },
new HindiLetter{ Letter="णः", ReadableEnglish="Ṇah" },
}
            );
            
            hindiMap.Add("त",
            new List<HindiLetter>
{
new HindiLetter{ Letter="त", ReadableEnglish="ta" },
new HindiLetter{ Letter="ता", ReadableEnglish="taa" },
new HindiLetter{ Letter="ति", ReadableEnglish="ti" },
new HindiLetter{ Letter="ती", ReadableEnglish="tee" },
new HindiLetter{ Letter="तु", ReadableEnglish="tu" },
new HindiLetter{ Letter="तू", ReadableEnglish="too" },
new HindiLetter{ Letter="तृ", ReadableEnglish="tru" },
new HindiLetter{ Letter="ते", ReadableEnglish="te" },
new HindiLetter{ Letter="तै", ReadableEnglish="tai" },
new HindiLetter{ Letter="तो", ReadableEnglish="to" },
new HindiLetter{ Letter="तौ", ReadableEnglish="tau" },
new HindiLetter{ Letter="तं", ReadableEnglish="tam" },
new HindiLetter{ Letter="तः", ReadableEnglish="tah" },
}
            );
            
            hindiMap.Add("थ",
            new List<HindiLetter>
{
new HindiLetter{ Letter="थ", ReadableEnglish="tha" },
new HindiLetter{ Letter="था", ReadableEnglish="thaa" },
new HindiLetter{ Letter="थि", ReadableEnglish="thi" },
new HindiLetter{ Letter="थी", ReadableEnglish="thee" },
new HindiLetter{ Letter="थु", ReadableEnglish="thu" },
new HindiLetter{ Letter="थू", ReadableEnglish="thoo" },
new HindiLetter{ Letter="थृ", ReadableEnglish="thru" },
new HindiLetter{ Letter="थे", ReadableEnglish="the" },
new HindiLetter{ Letter="थै", ReadableEnglish="thai" },
new HindiLetter{ Letter="थो", ReadableEnglish="tho" },
new HindiLetter{ Letter="थौ", ReadableEnglish="thau" },
new HindiLetter{ Letter="थं", ReadableEnglish="tham" },
new HindiLetter{ Letter="थः", ReadableEnglish="thah" },
}
            );
            
            hindiMap.Add("द",
            new List<HindiLetter>
{
new HindiLetter{ Letter="द", ReadableEnglish="da" },
new HindiLetter{ Letter="दा", ReadableEnglish="daa" },
new HindiLetter{ Letter="दि", ReadableEnglish="di" },
new HindiLetter{ Letter="दी", ReadableEnglish="dee" },
new HindiLetter{ Letter="दु", ReadableEnglish="du" },
new HindiLetter{ Letter="दू", ReadableEnglish="doo" },
new HindiLetter{ Letter="दृ", ReadableEnglish="dru" },
new HindiLetter{ Letter="दे", ReadableEnglish="de" },
new HindiLetter{ Letter="दै", ReadableEnglish="dai" },
new HindiLetter{ Letter="दो", ReadableEnglish="do" },
new HindiLetter{ Letter="दौ", ReadableEnglish="dau" },
new HindiLetter{ Letter="दं", ReadableEnglish="dam" },
new HindiLetter{ Letter="दः", ReadableEnglish="dah" },
}
            );
            
            hindiMap.Add("ध",
            new List<HindiLetter>
{
new HindiLetter{ Letter="ध", ReadableEnglish="dha" },
new HindiLetter{ Letter="धा", ReadableEnglish="dhaa" },
new HindiLetter{ Letter="धि", ReadableEnglish="dhi" },
new HindiLetter{ Letter="धी", ReadableEnglish="dhee" },
new HindiLetter{ Letter="धु", ReadableEnglish="dhu" },
new HindiLetter{ Letter="धू", ReadableEnglish="dhoo" },
new HindiLetter{ Letter="धृ", ReadableEnglish="dhru" },
new HindiLetter{ Letter="धे", ReadableEnglish="dhe" },
new HindiLetter{ Letter="धै", ReadableEnglish="dhai" },
new HindiLetter{ Letter="धो", ReadableEnglish="dho" },
new HindiLetter{ Letter="धौ", ReadableEnglish="dhau" },
new HindiLetter{ Letter="धं", ReadableEnglish="dham" },
new HindiLetter{ Letter="धः", ReadableEnglish="dhah" },
}
            );
            
            hindiMap.Add("न",
            new List<HindiLetter>
{
new HindiLetter{ Letter="न", ReadableEnglish="na" },
new HindiLetter{ Letter="ना", ReadableEnglish="naa" },
new HindiLetter{ Letter="नि", ReadableEnglish="ni" },
new HindiLetter{ Letter="नी", ReadableEnglish="nee" },
new HindiLetter{ Letter="नु", ReadableEnglish="nu" },
new HindiLetter{ Letter="नू", ReadableEnglish="noo" },
new HindiLetter{ Letter="नृ", ReadableEnglish="nru" },
new HindiLetter{ Letter="ने", ReadableEnglish="ne" },
new HindiLetter{ Letter="नै", ReadableEnglish="nai" },
new HindiLetter{ Letter="नो", ReadableEnglish="no" },
new HindiLetter{ Letter="नौ", ReadableEnglish="nau" },
new HindiLetter{ Letter="नं", ReadableEnglish="nam" },
new HindiLetter{ Letter="नः", ReadableEnglish="nah" },
}
            );
            
            hindiMap.Add("प",
            new List<HindiLetter>
{
new HindiLetter{ Letter="प", ReadableEnglish="pa" },
new HindiLetter{ Letter="पा", ReadableEnglish="paa" },
new HindiLetter{ Letter="पि", ReadableEnglish="pi" },
new HindiLetter{ Letter="पी", ReadableEnglish="pee" },
new HindiLetter{ Letter="पु", ReadableEnglish="pu" },
new HindiLetter{ Letter="पू", ReadableEnglish="poo" },
new HindiLetter{ Letter="पृ", ReadableEnglish="pru" },
new HindiLetter{ Letter="पे", ReadableEnglish="pe" },
new HindiLetter{ Letter="पै", ReadableEnglish="pai" },
new HindiLetter{ Letter="पो", ReadableEnglish="po" },
new HindiLetter{ Letter="पौ", ReadableEnglish="pau" },
new HindiLetter{ Letter="पं", ReadableEnglish="pam" },
new HindiLetter{ Letter="पः", ReadableEnglish="pah" },
}
            );
            
            hindiMap.Add("फ",
            new List<HindiLetter>
{
new HindiLetter{ Letter="फ", ReadableEnglish="pha" },
new HindiLetter{ Letter="फा", ReadableEnglish="phaa" },
new HindiLetter{ Letter="फि", ReadableEnglish="phi" },
new HindiLetter{ Letter="फी", ReadableEnglish="phee" },
new HindiLetter{ Letter="फु", ReadableEnglish="phu" },
new HindiLetter{ Letter="फू", ReadableEnglish="phoo" },
new HindiLetter{ Letter="फृ", ReadableEnglish="phru" },
new HindiLetter{ Letter="फे", ReadableEnglish="phe" },
new HindiLetter{ Letter="फै", ReadableEnglish="phai" },
new HindiLetter{ Letter="फो", ReadableEnglish="pho" },
new HindiLetter{ Letter="फौ", ReadableEnglish="phau" },
new HindiLetter{ Letter="फं", ReadableEnglish="pham" },
new HindiLetter{ Letter="फः​", ReadableEnglish="phah" },
}
            );
            
            hindiMap.Add("ब",
            new List<HindiLetter>
{
new HindiLetter{ Letter="ब", ReadableEnglish="ba" },
new HindiLetter{ Letter="बा", ReadableEnglish="baa" },
new HindiLetter{ Letter="बि", ReadableEnglish="bi" },
new HindiLetter{ Letter="बी", ReadableEnglish="bee" },
new HindiLetter{ Letter="बु", ReadableEnglish="bu" },
new HindiLetter{ Letter="बू", ReadableEnglish="boo" },
new HindiLetter{ Letter="बृ", ReadableEnglish="bru" },
new HindiLetter{ Letter="बे", ReadableEnglish="be" },
new HindiLetter{ Letter="बै", ReadableEnglish="bai" },
new HindiLetter{ Letter="बो", ReadableEnglish="bo" },
new HindiLetter{ Letter="बौ", ReadableEnglish="bau" },
new HindiLetter{ Letter="बं", ReadableEnglish="bam" },
new HindiLetter{ Letter="बः​", ReadableEnglish="bah" },
}
            );
            
            hindiMap.Add("भ",
            new List<HindiLetter>
{
new HindiLetter{ Letter="भ", ReadableEnglish="bha" },
new HindiLetter{ Letter="भा", ReadableEnglish="bhaa" },
new HindiLetter{ Letter="भि", ReadableEnglish="bhi" },
new HindiLetter{ Letter="भी", ReadableEnglish="bhee" },
new HindiLetter{ Letter="भु", ReadableEnglish="bhu" },
new HindiLetter{ Letter="भू", ReadableEnglish="bhoo" },
new HindiLetter{ Letter="भृ", ReadableEnglish="bhru" },
new HindiLetter{ Letter="भे", ReadableEnglish="bhe" },
new HindiLetter{ Letter="भै", ReadableEnglish="bhai" },
new HindiLetter{ Letter="भो", ReadableEnglish="bho" },
new HindiLetter{ Letter="भौ", ReadableEnglish="bhau" },
new HindiLetter{ Letter="भं", ReadableEnglish="bham" },
new HindiLetter{ Letter="भः", ReadableEnglish="bhah" },
}
            );
            
            hindiMap.Add("म",
            new List<HindiLetter>
{
new HindiLetter{ Letter="म", ReadableEnglish="ma" },
new HindiLetter{ Letter="मा", ReadableEnglish="maa" },
new HindiLetter{ Letter="मि", ReadableEnglish="mi" },
new HindiLetter{ Letter="मी", ReadableEnglish="mee" },
new HindiLetter{ Letter="मु", ReadableEnglish="mu" },
new HindiLetter{ Letter="मू", ReadableEnglish="moo" },
new HindiLetter{ Letter="मृ", ReadableEnglish="mru" },
new HindiLetter{ Letter="मे", ReadableEnglish="me" },
new HindiLetter{ Letter="मै", ReadableEnglish="mai" },
new HindiLetter{ Letter="मो", ReadableEnglish="mo" },
new HindiLetter{ Letter="मौ", ReadableEnglish="mau" },
new HindiLetter{ Letter="मं", ReadableEnglish="mam" },
new HindiLetter{ Letter="मः", ReadableEnglish="mah" },
}
            );
            
            hindiMap.Add("य",
            new List<HindiLetter>
{
new HindiLetter{ Letter="य", ReadableEnglish="ya" },
new HindiLetter{ Letter="या", ReadableEnglish="yaa" },
new HindiLetter{ Letter="यि", ReadableEnglish="yi" },
new HindiLetter{ Letter="यी", ReadableEnglish="yee" },
new HindiLetter{ Letter="यु", ReadableEnglish="yu" },
new HindiLetter{ Letter="यू", ReadableEnglish="yoo" },
new HindiLetter{ Letter="यृ", ReadableEnglish="yru" },
new HindiLetter{ Letter="ये", ReadableEnglish="ye" },
new HindiLetter{ Letter="यै", ReadableEnglish="yai" },
new HindiLetter{ Letter="यो", ReadableEnglish="yo" },
new HindiLetter{ Letter="यौ", ReadableEnglish="yau" },
new HindiLetter{ Letter="यं", ReadableEnglish="yam" },
new HindiLetter{ Letter="यः", ReadableEnglish="yah" },
}
            );
            
            hindiMap.Add("र",
            new List<HindiLetter>
{
new HindiLetter{ Letter="र", ReadableEnglish="ra" },
new HindiLetter{ Letter="रा", ReadableEnglish="raa" },
new HindiLetter{ Letter="रि", ReadableEnglish="ri" },
new HindiLetter{ Letter="री", ReadableEnglish="ree" },
new HindiLetter{ Letter="रु", ReadableEnglish="ru" },
new HindiLetter{ Letter="रू", ReadableEnglish="roo" },
new HindiLetter{ Letter="रृ", ReadableEnglish="rru" },
new HindiLetter{ Letter="रे", ReadableEnglish="re" },
new HindiLetter{ Letter="रै", ReadableEnglish="rai" },
new HindiLetter{ Letter="रो", ReadableEnglish="ro" },
new HindiLetter{ Letter="रौ", ReadableEnglish="rau" },
new HindiLetter{ Letter="रं", ReadableEnglish="ram" },
new HindiLetter{ Letter="रः", ReadableEnglish="rah" },
}
            );
            
            hindiMap.Add("ल",
            new List<HindiLetter>
{
new HindiLetter{ Letter="ल", ReadableEnglish="la" },
new HindiLetter{ Letter="ला", ReadableEnglish="laa" },
new HindiLetter{ Letter="लि", ReadableEnglish="li" },
new HindiLetter{ Letter="ली", ReadableEnglish="lee" },
new HindiLetter{ Letter="लु", ReadableEnglish="lu" },
new HindiLetter{ Letter="लू", ReadableEnglish="loo" },
new HindiLetter{ Letter="लृ", ReadableEnglish="lru" },
new HindiLetter{ Letter="ले", ReadableEnglish="le" },
new HindiLetter{ Letter="लै", ReadableEnglish="lai" },
new HindiLetter{ Letter="लो", ReadableEnglish="lo" },
new HindiLetter{ Letter="लौ", ReadableEnglish="lau" },
new HindiLetter{ Letter="लं", ReadableEnglish="lam" },
new HindiLetter{ Letter="लः", ReadableEnglish="lah" },
}
            );


            hindiMap.Add("ळ",
            new List<HindiLetter>
{
new HindiLetter{ Letter="ळ", ReadableEnglish="La" },
new HindiLetter{ Letter="ळा", ReadableEnglish="Laa" },
new HindiLetter{ Letter="ळि", ReadableEnglish="Li" },
new HindiLetter{ Letter="ऴी", ReadableEnglish="Lee" },
new HindiLetter{ Letter="ळु", ReadableEnglish="Lu" },
new HindiLetter{ Letter="ळू", ReadableEnglish="Loo" },
new HindiLetter{ Letter="ळृ", ReadableEnglish="Lru" },
new HindiLetter{ Letter="ळे", ReadableEnglish="Le" },
new HindiLetter{ Letter="ळै", ReadableEnglish="Lai" },
new HindiLetter{ Letter="ळो", ReadableEnglish="Lo" },
new HindiLetter{ Letter="ळौ", ReadableEnglish="Lau" },
new HindiLetter{ Letter="ळं", ReadableEnglish="Lam" },
new HindiLetter{ Letter="ळः", ReadableEnglish="Lah" },
}
            );


             


            hindiMap.Add("ह",
           new List<HindiLetter>
{
new HindiLetter{ Letter="ह", ReadableEnglish="ha" },
new HindiLetter{ Letter="हा", ReadableEnglish="haa" },
new HindiLetter{ Letter="हि", ReadableEnglish="hi" },
new HindiLetter{ Letter="ही", ReadableEnglish="hee" },
new HindiLetter{ Letter="हु", ReadableEnglish="hu" },
new HindiLetter{ Letter="हू", ReadableEnglish="hoo" },
new HindiLetter{ Letter="हृ", ReadableEnglish="hru" },
new HindiLetter{ Letter="हे", ReadableEnglish="he" },
new HindiLetter{ Letter="है", ReadableEnglish="hai" },
new HindiLetter{ Letter="हो", ReadableEnglish="ho" },
new HindiLetter{ Letter="हौ", ReadableEnglish="hau" },
new HindiLetter{ Letter="हं", ReadableEnglish="ham" },
new HindiLetter{ Letter="हः​", ReadableEnglish="hah" },
}
           );

            hindiMap.Add("व",
            new List<HindiLetter>
{
new HindiLetter{ Letter="व", ReadableEnglish="va" },
new HindiLetter{ Letter="वा", ReadableEnglish="vaa" },
new HindiLetter{ Letter="वि", ReadableEnglish="vi" },
new HindiLetter{ Letter="वी", ReadableEnglish="vee" },
new HindiLetter{ Letter="वु", ReadableEnglish="vu" },
new HindiLetter{ Letter="वू", ReadableEnglish="voo" },
new HindiLetter{ Letter="वृ", ReadableEnglish="vru" },
new HindiLetter{ Letter="वे", ReadableEnglish="ve" },
new HindiLetter{ Letter="वै", ReadableEnglish="vai" },
new HindiLetter{ Letter="वो", ReadableEnglish="vo" },
new HindiLetter{ Letter="वौ", ReadableEnglish="vau" },
new HindiLetter{ Letter="वं", ReadableEnglish="vam" },
new HindiLetter{ Letter="वः", ReadableEnglish="vah" },
}
            );
            hindiMap.Add("श",
            
                new List<HindiLetter>
{
new HindiLetter{ Letter="श", ReadableEnglish="śa" },
new HindiLetter{ Letter="शा", ReadableEnglish="śaa" },
new HindiLetter{ Letter="शि", ReadableEnglish="śi" },
new HindiLetter{ Letter="शी", ReadableEnglish="śee" },
new HindiLetter{ Letter="शु", ReadableEnglish="śu" },
new HindiLetter{ Letter="शू", ReadableEnglish="śoo" },
new HindiLetter{ Letter="शृ", ReadableEnglish="śru" },
new HindiLetter{ Letter="शे", ReadableEnglish="śe" },
new HindiLetter{ Letter="शै", ReadableEnglish="śai" },
new HindiLetter{ Letter="शो", ReadableEnglish="śo" },
new HindiLetter{ Letter="शौ", ReadableEnglish="śau" },
new HindiLetter{ Letter="शं", ReadableEnglish="śam" },
new HindiLetter{ Letter="शः", ReadableEnglish="śah" },
}
            );
            
            hindiMap.Add("ष",
            new List<HindiLetter>
{
new HindiLetter{ Letter="ष", ReadableEnglish="Ṣa" },
new HindiLetter{ Letter="षा", ReadableEnglish="Ṣaa" },
new HindiLetter{ Letter="षि", ReadableEnglish="Ṣi" },
new HindiLetter{ Letter="षी", ReadableEnglish="Ṣee" },
new HindiLetter{ Letter="षु", ReadableEnglish="Ṣu" },
new HindiLetter{ Letter="षू", ReadableEnglish="Ṣoo" },
new HindiLetter{ Letter="षृ", ReadableEnglish="Ṣru" },
new HindiLetter{ Letter="षे", ReadableEnglish="Ṣe" },
new HindiLetter{ Letter="षै", ReadableEnglish="Ṣai" },
new HindiLetter{ Letter="षो", ReadableEnglish="Ṣo" },
new HindiLetter{ Letter="षौ", ReadableEnglish="Ṣau" },
new HindiLetter{ Letter="षं", ReadableEnglish="Ṣam" },
new HindiLetter{ Letter="षः", ReadableEnglish="Ṣah" },
}
            );
            
            hindiMap.Add("स",
            new List<HindiLetter>
{
new HindiLetter{ Letter="स", ReadableEnglish="sa" },
new HindiLetter{ Letter="सा", ReadableEnglish="saa" },
new HindiLetter{ Letter="सि", ReadableEnglish="si" },
new HindiLetter{ Letter="सी", ReadableEnglish="see" },
new HindiLetter{ Letter="सु", ReadableEnglish="su" },
new HindiLetter{ Letter="सू", ReadableEnglish="soo" },
new HindiLetter{ Letter="सृ", ReadableEnglish="sru" },
new HindiLetter{ Letter="से", ReadableEnglish="se" },
new HindiLetter{ Letter="सै", ReadableEnglish="sai" },
new HindiLetter{ Letter="सो", ReadableEnglish="so" },
new HindiLetter{ Letter="सौ", ReadableEnglish="sau" },
new HindiLetter{ Letter="सं", ReadableEnglish="sam" },
new HindiLetter{ Letter="सः", ReadableEnglish="sah" },
}
            );




            hindiMap.Add("क़",
            new List<HindiLetter>
{
new HindiLetter{ Letter="क़", ReadableEnglish="qa" },
new HindiLetter{ Letter="क़ा", ReadableEnglish="qaa" },
new HindiLetter{ Letter="क़ि", ReadableEnglish="qi" },
new HindiLetter{ Letter="क़ी", ReadableEnglish="qee" },
new HindiLetter{ Letter="क़ु", ReadableEnglish="qu" },
new HindiLetter{ Letter="क़ू", ReadableEnglish="qoo" },
new HindiLetter{ Letter="क़ृ", ReadableEnglish="qru" },
new HindiLetter{ Letter="क़े", ReadableEnglish="qe" },
new HindiLetter{ Letter="क़ै", ReadableEnglish="qai" },
new HindiLetter{ Letter="क़ो", ReadableEnglish="qo" },
new HindiLetter{ Letter="क़ौ", ReadableEnglish="qau" },
new HindiLetter{ Letter="क़ं", ReadableEnglish="qam" },
new HindiLetter{ Letter="क़ः", ReadableEnglish="qah" },
}
            );




            hindiMap.Add("ख़",
            new List<HindiLetter>
{
new HindiLetter{ Letter="ख़", ReadableEnglish="Qha" },
new HindiLetter{ Letter="ख़ा", ReadableEnglish="Qhaa" },
new HindiLetter{ Letter="ख़ि", ReadableEnglish="Qhi" },
new HindiLetter{ Letter="ख़ी", ReadableEnglish="Qhee" },
new HindiLetter{ Letter="ख़ु", ReadableEnglish="Qhu" },
new HindiLetter{ Letter="ख़ू", ReadableEnglish="Qhoo" },
new HindiLetter{ Letter="ख़ृ", ReadableEnglish="Qhru" },
new HindiLetter{ Letter="ख़े", ReadableEnglish="Qhe" },
new HindiLetter{ Letter="ख़ै", ReadableEnglish="Qhai" },
new HindiLetter{ Letter="ख़ो", ReadableEnglish="Qho" },
new HindiLetter{ Letter="ख़ौ", ReadableEnglish="Qhau" },
new HindiLetter{ Letter="ख़ं", ReadableEnglish="Qham" },
new HindiLetter{ Letter="ख़ः", ReadableEnglish="Qhah" },
}
            );




            hindiMap.Add("ग़",
            new List<HindiLetter>
{
new HindiLetter{ Letter="ग़", ReadableEnglish="Ga" },
new HindiLetter{ Letter="ग़ा", ReadableEnglish="Gaa" },
new HindiLetter{ Letter="ग़ि", ReadableEnglish="Gi" },
new HindiLetter{ Letter="ग़ी", ReadableEnglish="Gee" },
new HindiLetter{ Letter="ग़ु", ReadableEnglish="Gu" },
new HindiLetter{ Letter="ग़ू", ReadableEnglish="Goo" },
new HindiLetter{ Letter="ग़ृ", ReadableEnglish="Gru" },
new HindiLetter{ Letter="ग़े", ReadableEnglish="Ge" },
new HindiLetter{ Letter="ग़ै", ReadableEnglish="Gai" },
new HindiLetter{ Letter="ग़ो", ReadableEnglish="Go" },
new HindiLetter{ Letter="ग़ौ", ReadableEnglish="Gau" },
new HindiLetter{ Letter="ग़ं", ReadableEnglish="Gam" },
new HindiLetter{ Letter="ग़ः", ReadableEnglish="Gah" },
}
            );



            hindiMap.Add("ज़",
            new List<HindiLetter>
{
new HindiLetter{ Letter="ज़", ReadableEnglish="za" },
new HindiLetter{ Letter="ज़ा", ReadableEnglish="zaa" },
new HindiLetter{ Letter="ज़ि", ReadableEnglish="zi" },
new HindiLetter{ Letter="ज़ी", ReadableEnglish="zee" },
new HindiLetter{ Letter="ज़ु", ReadableEnglish="zu" },
new HindiLetter{ Letter="ज़ू", ReadableEnglish="zoo" },
new HindiLetter{ Letter="ज़ृ", ReadableEnglish="zru" },
new HindiLetter{ Letter="ज़े", ReadableEnglish="ze" },
new HindiLetter{ Letter="ज़ै", ReadableEnglish="zai" },
new HindiLetter{ Letter="ज़ो", ReadableEnglish="zo" },
new HindiLetter{ Letter="ज़ौ", ReadableEnglish="zau" },
new HindiLetter{ Letter="ज़ं", ReadableEnglish="zam" },
new HindiLetter{ Letter="ज़ः", ReadableEnglish="zah" },
}
            );




            hindiMap.Add("ड़",
            new List<HindiLetter>
{
new HindiLetter{ Letter="ड़", ReadableEnglish="Da" },
new HindiLetter{ Letter="ड़ा", ReadableEnglish="Daa" },
new HindiLetter{ Letter="ड़ि", ReadableEnglish="Di" },
new HindiLetter{ Letter="ड़ी", ReadableEnglish="Dee" },
new HindiLetter{ Letter="ड़ु", ReadableEnglish="Du" },
new HindiLetter{ Letter="ड़ू", ReadableEnglish="Doo" },
new HindiLetter{ Letter="ड़ृ", ReadableEnglish="Dru" },
new HindiLetter{ Letter="ड़े", ReadableEnglish="De" },
new HindiLetter{ Letter="ड़ै", ReadableEnglish="Dai" },
new HindiLetter{ Letter="ड़ो", ReadableEnglish="Do" },
new HindiLetter{ Letter="ड़ौ", ReadableEnglish="Dau" },
new HindiLetter{ Letter="ड़ं", ReadableEnglish="Dam" },
new HindiLetter{ Letter="ड़ः", ReadableEnglish="Dah" },
}
            );



            hindiMap.Add("ढ़",
            new List<HindiLetter>
{
new HindiLetter{ Letter="ढ़", ReadableEnglish="Dha" },
new HindiLetter{ Letter="ढ़ा", ReadableEnglish="Dhaa" },
new HindiLetter{ Letter="ढ़ि", ReadableEnglish="Dhi" },
new HindiLetter{ Letter="ढ़ी", ReadableEnglish="Dhee" },
new HindiLetter{ Letter="ढ़ु", ReadableEnglish="Dhu" },
new HindiLetter{ Letter="ढ़ू", ReadableEnglish="Dhoo" },
new HindiLetter{ Letter="ढ़ृ", ReadableEnglish="Dhru" },
new HindiLetter{ Letter="ढ़े", ReadableEnglish="Dhe" },
new HindiLetter{ Letter="ढ़ै", ReadableEnglish="Dhai" },
new HindiLetter{ Letter="ढ़ो", ReadableEnglish="Dho" },
new HindiLetter{ Letter="ढ़ौ", ReadableEnglish="Dhau" },
new HindiLetter{ Letter="ढ़ं", ReadableEnglish="Dham" },
new HindiLetter{ Letter="ढ़ः", ReadableEnglish="Dhah" },
}
            );

            
            hindiMap.Add("फ़",
            new List<HindiLetter>
{
new HindiLetter{ Letter="फ़", ReadableEnglish="fa" },
new HindiLetter{ Letter="फ़ा", ReadableEnglish="faa" },
new HindiLetter{ Letter="फ़ि", ReadableEnglish="fi" },
new HindiLetter{ Letter="फ़ी", ReadableEnglish="fee" },
new HindiLetter{ Letter="फ़ु", ReadableEnglish="fu" },
new HindiLetter{ Letter="फ़ू", ReadableEnglish="foo" },
new HindiLetter{ Letter="फ़ृ", ReadableEnglish="fru" },
new HindiLetter{ Letter="फ़े", ReadableEnglish="fe" },
new HindiLetter{ Letter="फ़ै", ReadableEnglish="fai" },
new HindiLetter{ Letter="फ़ो", ReadableEnglish="fo" },
new HindiLetter{ Letter="फ़ौ", ReadableEnglish="fau" },
new HindiLetter{ Letter="फ़ं", ReadableEnglish="fam" },
new HindiLetter{ Letter="फ़ः", ReadableEnglish="fah" },
}
            );

            
            foreach (List<HindiLetter> hindiLetters in this.hindiMap.Values)
            {
                foreach (HindiLetter hindiLetter in hindiLetters)
                {
                    try
                    {
                        Sentence.HindiLetterReadableEnglishMap.Add(hindiLetter.Letter, hindiLetter.ReadableEnglish);
                    }
                    catch
                    {
                       
                    }
                }
            }


            Sentence.HindiLetterReadableEnglishMap.Add("अ", "a"); //a
            Sentence.HindiLetterReadableEnglishMap.Add("आ", "aa"); //aa
            Sentence.HindiLetterReadableEnglishMap.Add("इ", "i"); //i
            Sentence.HindiLetterReadableEnglishMap.Add("ई", "ee"); //ee
            Sentence.HindiLetterReadableEnglishMap.Add("उ", "u"); //u
            Sentence.HindiLetterReadableEnglishMap.Add("ऊ", "oo"); //oo
            Sentence.HindiLetterReadableEnglishMap.Add("ए", "ae"); //ae
            Sentence.HindiLetterReadableEnglishMap.Add("ऐ", "ai"); //ai
            Sentence.HindiLetterReadableEnglishMap.Add("ओ", "o"); //o
            Sentence.HindiLetterReadableEnglishMap.Add("औ", "au"); //au
            Sentence.HindiLetterReadableEnglishMap.Add("अं", "am"); //am
            Sentence.HindiLetterReadableEnglishMap.Add("आः", "aha"); //aha


            Sentence.HindiLetterReadableEnglishMap.Add("्", ""); //virama
            Sentence.HindiLetterReadableEnglishMap.Add("ा", "aa"); //a
            Sentence.HindiLetterReadableEnglishMap.Add("ि", "i"); //i
            Sentence.HindiLetterReadableEnglishMap.Add("ी", "ee"); //ee
            Sentence.HindiLetterReadableEnglishMap.Add("ु", "u"); //u
            Sentence.HindiLetterReadableEnglishMap.Add("ू", "oo"); //oo
            Sentence.HindiLetterReadableEnglishMap.Add("ऋ", "ri"); //ri
            Sentence.HindiLetterReadableEnglishMap.Add("ॠ", "rī"); //rī
            Sentence.HindiLetterReadableEnglishMap.Add("ऌ", "li"); //li
            Sentence.HindiLetterReadableEnglishMap.Add("ॡ", "lī"); //lī


            Sentence.HindiLetterReadableEnglishMap.Add("ृ", "ru"); //ru
            Sentence.HindiLetterReadableEnglishMap.Add("ॄ", ""); 
            Sentence.HindiLetterReadableEnglishMap.Add("ॣ", "");
            //Sentence.HindiLetterReadableEnglishMap.Add("ॡ", "");

            Sentence.HindiLetterReadableEnglishMap.Add("े", "ae"); //ae
            Sentence.HindiLetterReadableEnglishMap.Add("ै", "ai"); //ai
            Sentence.HindiLetterReadableEnglishMap.Add("ो", "o"); //o
            Sentence.HindiLetterReadableEnglishMap.Add("ौ", "au"); //au


            Sentence.HindiLetterReadableEnglishMap.Add("॑", ""); //udatta
            Sentence.HindiLetterReadableEnglishMap.Add("॒", ""); //anudatta
            Sentence.HindiLetterReadableEnglishMap.Add("॓", ""); //accent grave
            Sentence.HindiLetterReadableEnglishMap.Add("॔", ""); //accent aigu 
            Sentence.HindiLetterReadableEnglishMap.Add("ँ", "N"); //candra bindu 
            Sentence.HindiLetterReadableEnglishMap.Add("ं", "n"); //anusvara am
            Sentence.HindiLetterReadableEnglishMap.Add("ः", "ha"); //visarga aha
            Sentence.HindiLetterReadableEnglishMap.Add("़", ""); //nukta dot below


            Sentence.HindiLetterReadableEnglishMap.Add("।", ". "); //danda
            Sentence.HindiLetterReadableEnglishMap.Add("॥", ""); //double danda
            Sentence.HindiLetterReadableEnglishMap.Add("ऽ", ""); //avagraha
            Sentence.HindiLetterReadableEnglishMap.Add("॰", ""); //
            Sentence.HindiLetterReadableEnglishMap.Add("ॐ", "om"); //om


            

        }



        private ToggleButton lastConsonant = null;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowConsonants(sender as ToggleButton);
        }


        private void ShowConsonants(ToggleButton hindiLetterButton)
        {
            StackPanel sp = hindiLetterButton.Content as StackPanel;

            if (hindiLetterButton != null)
            {
                if (lastConsonant != null)
                    lastConsonant.IsChecked = null;

                lastConsonant = hindiLetterButton;

                consonantsCollection.Clear();

                foreach (HindiLetter letter in hindiMap[((TextBlock)sp.Children[0]).Text])
                {
                    consonantsCollection.Add(letter);
                }

                this.Consonants.DataContext = consonantsCollection;
            }

            if (!myPopup.IsOpen)
                myPopup.IsOpen = true;

            
        }

        

        private void Button_HindiConsonantLetter_Click(object sender, RoutedEventArgs e)
        {
            Button hindiLetterButton = sender as Button;
            HindiLetter hindiLetter = hindiLetterButton.DataContext as HindiLetter;

            //int selectionStart = this.currentTextBox.SelectionStart;
            //int selectionLength = this.currentTextBox.SelectionLength;

            InsertTextAtCurrentPosition(hindiLetter.Letter);

            //this.CurrentSentence.HindiSentence = this.CurrentSentence.HindiSentence + hindiLetter.Letter;

            this.myPopup.IsOpen = false;


        }

        private void Button_HindiLetter_Click(object sender, RoutedEventArgs e)
        {
            Button hindiLetterButton = sender as Button;
            StackPanel sp = hindiLetterButton.Content as StackPanel;

            InsertTextAtCurrentPosition(((TextBlock)sp.Children[0]).Text);
            //this.CurrentSentence.HindiSentence = this.CurrentSentence.HindiSentence + ((TextBlock)sp.Children[0]).Text;

        }
        
        private void Button_Space_Click(object sender, RoutedEventArgs e)
        {
            InsertTextAtCurrentPosition(" ");
            //this.CurrentSentence.HindiSentence = this.CurrentSentence.HindiSentence + " ";
        }
        

        private void Button_Enter_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentSentence.HindiSentence = new TextRange(this.currentTextBox.Document.ContentStart, this.currentTextBox.Document.ContentEnd).Text;
            sentences.Add(new Sentence());
        }

        private void HindiTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click_Translate(object sender, RoutedEventArgs e)
        {
            //string hindiWord;

            //string uri = "http://translate.google.com/#hi|en|%E0%A4%B6%E0%A4%BF%E0%A4%95%E0%A5%8D%E0%A4%B7%E0%A4%95";

            //string a = "दूध";

            //a = "शिक्षक";

            //char[] aa = a.ToCharArray();

            //TranslateClient client = new TranslateClient("Hindi Pandit App");
            //string translated = client.Translate(a, "hi", "en");


            string file = @"C:\Users\Karthik\Desktop\hindi.txt";

            string[] readableLetters = { "kha", "khaa", "khi", "khee", "khu", "khoo", "khru", "khe", "khai", "kho", "khau", "kham", "khah" };

            string [] lines = File.ReadAllLines(file);

            StringBuilder sb = new StringBuilder();

            foreach (string line in lines)
            {
                string [] hindiLetters = line.Split(' ');

                if (hindiLetters.Length < readableLetters.Length)
                    break;

                hindiLetters = hindiLetters.Where(s => string.IsNullOrEmpty(s) == false).ToArray();

                sb.AppendFormat( "hindiMap.Add(\"{0}\",", hindiLetters[0] );
                sb.AppendLine();
                sb.AppendLine("new List<HindiLetter>");
                sb.AppendLine("{");


                for (int i=0; i < readableLetters.Length; i++)
                {
                    sb.Append( "new HindiLetter{ Letter=\"" );
                    sb.Append( hindiLetters[i] );
                    sb.Append( "\", ReadableEnglish=\"" );
                    sb.Append( readableLetters[i] );
                    sb.AppendLine( "\" }," );

                }

                sb.AppendLine("}");
                sb.AppendLine(");");

            }

            string entireContent = sb.ToString();



        }

        private ObservableCollection<Sentence> sentences = new ObservableCollection<Sentence>();

        //private Sentence currentSentence = null;

        private Sentence CurrentSentence
        {
            get
            {
                if (this.currentTextBox == null)
                    return null;

                return this.currentTextBox.DataContext as Sentence;
            }
        }
        
 
        private void Button_Click_AddTransliteratedWord(object sender, RoutedEventArgs e)
        {
            InsertTextAtCurrentPosition((((Button)sender).Content).ToString() + " ");
            this.currentTextBox.Selection.Select(this.currentTextBox.Document.ContentEnd, this.currentTextBox.Document.ContentEnd);

            //if( string.IsNullOrEmpty(this.CurrentSentence.HindiSentence) )
            //    this.CurrentSentence.HindiSentence = (((Button)sender).Content).ToString();
            //else
            //    this.CurrentSentence.HindiSentence = this.CurrentSentence.HindiSentence + " " + (((Button)sender).Content).ToString();

        }

        private void Button_AddWord_Click(object sender, RoutedEventArgs e)
        {
            PopulateTransliteratedWords();

        }

        private void TextToTransliterate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PopulateTransliteratedWords();
            }
        }

        private void PopulateTransliteratedWords()
        {
            try
            {
                WebRequest request = WebRequest.Create(string.Format("http://www.google.com/transliterate/indic?tlqt=1&langpair=en|hi&text={0}&&tl_app=1", this.TextToTransliterate.Text));
                WebResponse response = request.GetResponse();

                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream, true);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();

                /*
                 
    [
    {
    "ew" : "namaste",
    "hws" : [
    "\u0928\u092E\u0938\u094D\u0924\u0947","\u0928\u092E\u0938\u0924\u0947","\u0928\u092E\u093E\u0938\u094D\u0924\u0947","\u0928\u093E\u092E\u093E\u0938\u094D\u0924\u0947","\u0928\u093E\u092E\u0938\u094D\u0924\u0947",
    ]
    },
    ]

                 */

                int hwsIndex = responseFromServer.IndexOf("hws") + 3;

                int startSquareBracketIndex = responseFromServer.IndexOf("[", hwsIndex);
                int endSquareBracketIndex = responseFromServer.IndexOf("]", hwsIndex);

                string commaSeparatedHindiWordsLine = responseFromServer.Substring(startSquareBracketIndex + 1, endSquareBracketIndex - (startSquareBracketIndex + 1)).Replace("\n", "");

                transliteratedWords.Clear();

                foreach (string rawHindiWord in commaSeparatedHindiWordsLine.Split(','))
                {
                    string hindiCharsLine = rawHindiWord.Replace("\"", "");

                    string[] hindiChars = hindiCharsLine.Split(new string[] { "\\u" }, StringSplitOptions.RemoveEmptyEntries);

                    string transliteratedWord = string.Empty;

                    foreach (string hindiCharsWord in hindiChars)
                    {
                        string hindiChar = Char.ConvertFromUtf32(Int32.Parse(hindiCharsWord, System.Globalization.NumberStyles.AllowHexSpecifier));
                        transliteratedWord += hindiChar;
                    }

                    if (!string.IsNullOrEmpty(transliteratedWord))
                        transliteratedWords.Add(transliteratedWord);

                }
            }
            catch
            {
                MessageBox.Show("Could not connect to Google. Please check your internet connection");
            }

        }


        private ObservableCollection<string> transliteratedWords = new ObservableCollection<string>();


        
        private void Button_Click_SpecialCharacter(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button != null)
            {
                if (button.Content is StackPanel)
                {
                    StackPanel sp = button.Content as StackPanel;
                    InsertTextAtCurrentPosition(((TextBlock)sp.Children[0]).Text);
//                    this.CurrentSentence.HindiSentence = this.CurrentSentence.HindiSentence + ((TextBlock)sp.Children[0]).Text;
                }
                else
                {
                    InsertTextAtCurrentPosition(button.Content.ToString());
//                    this.CurrentSentence.HindiSentence = this.CurrentSentence.HindiSentence + button.Content.ToString();
                }
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (myPopup.IsOpen)
                myPopup.IsOpen = false;
        }

      

        private RichTextBox currentTextBox = null;

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {

            FlowDocument document = new FlowDocument();
            
            foreach (Sentence sentence in this.sentences)
            {
                document.Blocks.Add(new Paragraph(new Run(sentence.HindiSentence) { FontSize=24, Foreground = Brushes.Green }));
                document.Blocks.Add(new Paragraph(new Run(sentence.EnglishReadableSentence) { FontSize = 20, Foreground = Brushes.Blue }));
                document.Blocks.Add(new Paragraph(new Run(sentence.EnglishMeaning) { FontSize = 18, Foreground = Brushes.Red }));
                document.Blocks.Add(new Paragraph() { LineHeight=5 });
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = "rtf";
            dialog.Filter = "Rich Text Format (*.rtf) |*.rtf";
            bool? ok = dialog.ShowDialog();

            if (ok.HasValue && ok.Value == true)
            {
                FileInfo fileInfo1 = new FileInfo(dialog.FileName);

                using (FileStream file = fileInfo1.Create())
                {
                    TextRange textRange = new TextRange(document.ContentStart, document.ContentEnd);
                    textRange.Save(file, DataFormats.Rtf);
                }

                FlowDocumentReader docViewer = new FlowDocumentReader();
                docViewer.Document = document;
                docViewer.ViewingMode = FlowDocumentReaderViewingMode.Scroll;


                Window window = new Window();
                window.Content = docViewer;

                window.ShowDialog();


            }
            
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

       
        private void Button_Click_Word(object sender, RoutedEventArgs e)
        {
            InsertTextAtCurrentPosition(((TextBlock)((Grid)((Button)sender).Content).Children[0]).Text.ToString() + " ");
            this.currentTextBox.Selection.Select(this.currentTextBox.Document.ContentEnd, this.currentTextBox.Document.ContentEnd);
        }

        private void TextToTransliterate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.TextToTransliterate.SelectAll();
        }

       

    
    }



}
