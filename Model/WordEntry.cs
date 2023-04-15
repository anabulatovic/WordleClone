namespace WordleClone.Model
{
    public class WordEntry
    {
        public string PartOfSpeech { get; set; }
        public string Definition { get; set; }

        public WordEntry(string partOfSpeech, string definition)
        {
            PartOfSpeech = partOfSpeech;
            Definition = definition;
        }

    }

}
