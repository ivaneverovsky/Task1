namespace Task1
{
    class ResultBuilder
    {
        //model of data in grid
        public string Text { get; set; }
        public int WordCount { get; set; }
        public int VowelCount { get; set; }

        //constructor
        public ResultBuilder(string text, int wordCount, int vowelCount)
        {
            Text = text;
            WordCount = wordCount;
            VowelCount = vowelCount;
        }
    }
}
