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
            HashSet<int> correctIndices = new HashSet<int>();

            for (int i = 0; i < Letters.Length; i++)
            {
                Letter letter = Letters[i];

                if (letter.Input == correctAnswer[i])
                {
                    letter.Color = Color.FromUint(0xff6aaa64);
                    count++;
                    correctIndices.Add(i);
                }

                else if (correctAnswer.Contains(letter.Input))
                {
                    int index = -1;

                    for (int j = i + 1; j < correctAnswer.Length; j++)
                    {
                        if (correctAnswer[j] == letter.Input && !correctIndices.Contains(j))
                        {
                            index = j;
                            break;
                        }
                    }

                    if (index != -1)
                    {
                        letter.Color = Color.FromUint(0xffc9b458);
                        correctIndices.Add(index);
                    }

                    else
                    {
                        letter.Color = Colors.Gray;
                    }
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
