using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;

namespace Task1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextService _text = new TextService();
        ResultStorage _rs = new ResultStorage();

        //record all id's and their content to check if we have such id in dictionary to ignore such user's requests
        private Dictionary<int, string> dic_texts = new Dictionary<int, string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnCount_Click(object sender, RoutedEventArgs e)
        {
            //clear list
            listViewResult.Items.Clear();

            //check here the input string (add regex check 0-9 and ',' , ';' - only)
            string[] cut = textBoxQuery.Text.Split(new char[] { ',', ';' });

            foreach (var item in cut)
            {
                try
                {
                    int id = int.Parse(item); //parse string to int

                    if (id < 1 || id > 20) //check if we got right id range
                        throw new ArgumentException("id is out of range");

                    if (dic_texts.ContainsKey(id)) //check if we got it in dic (id = key)
                        throw new ArgumentException("we got that key");

                    var result = await _text.GetResults(item); //get-request

                    //add to dic
                    dic_texts.Add(id, result);

                    string newResult = RemovePunc(result); //remove punctuation marks from the string and rewrite our string
                    
                    int wordCount = WordCount(newResult); //count words
                    
                    int vowelCount = VowelCount(newResult); //count vowels
                    
                    var obj = new ResultBuilder(result, wordCount, vowelCount); //create new object in class

                    _rs.AddResult(obj); //add to result storage
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }

            CollectResults(); //add data to grid 

            //punctuation remover
            static string RemovePunc(string newResult)
            {
                //special situation for strings with chars '-' and '—', we have to change them to ' '.
                newResult = newResult.Replace('—', ' ');
                newResult = newResult.Replace('-', ' ');

                //replace unusual chars with common
                Regex regexVowel = new Regex("[éàóíúæÁäüöӧøіòêёуеыаоэяиюЁУЕЫАОЭЯИЮeyuioaEYUIOA]"); //to make program more "friendly" we can use more regular expressions. As an example we can find which languages we are going to work with, then make "regex" different for each language (do not forget capital letters!)

                newResult = regexVowel.Replace(newResult, "A"); //here i put "A"s everywhere to count them later

                //remove punctuation, replace with emptiness
                string empty = "";
                for (int i = 0; i < newResult.Length; i++)
                    if (char.IsPunctuation(newResult[i]))
                        newResult = newResult.Replace(newResult[i].ToString(), empty);
                
                //remove double spaces
                Regex regex = new Regex(@"\s+");
                newResult = regex.Replace(newResult, " ");

                return newResult;
            }

            //words counter
            static int WordCount(string newResult)
            {
                int counter = 0;
                for (int i = 0; i < newResult.Length; i++)
                    if (newResult[i] == ' ')
                        counter++;

                return counter + 1;
            }

            //vowels counter
            static int VowelCount(string newResult)
            {
                int counter = 0;
                for (int i = 0; i < newResult.Length; i++)
                    if (newResult[i] == 'A')
                        counter++;

                return counter;
            }

            //show results
            void CollectResults()
            {
                for (int i = 0; i < _rs.AllResults.Count; i++)
                    listViewResult.Items.Add(_rs.AllResults[i]);
            }
        }
    }
}
