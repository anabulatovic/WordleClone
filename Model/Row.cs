using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleClone.Model
{
    public class Row
    {
        public Row() 
        {
            Letters = new Letter[5]
            {
                new Letter(),
                new Letter(),
                new Letter(),
                new Letter(),
                new Letter()
            };
        }

        public Letter[] Letters { get; set; }

        public bool Validate(char[] correctAnswer)
        {
            int count = 0;

            for (int i = 0; i < Letters.Length; i++)
            {
                Letter letter = Letters[i];

                if (letter.Input == correctAnswer[i])
                {
                    letter.Color = Colors.Green;
                    count++;
                }

                else if (correctAnswer.Contains(letter.Input))
                {
                    letter.Color = Colors.Yellow;
                }

                else
                {
                    letter.Color = Colors.Gray;
                }
            }

            return count == 5;

        }

    }
}
