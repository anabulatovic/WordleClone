using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
