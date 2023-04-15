namespace WordleClone.Model
{
    public class Row
    {
        enum State
        {
            Gray = 0, 
            Yellow = 1, 
            Green = 2
        }

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

        private void ColorFields(State[] letterStates)
        {
            int length = letterStates.Length;

            for (int i = 0; i < length; i++)
            {
                if (letterStates[i] == State.Gray)
                {
                    Letters[i].Color = Colors.Gray;
                }

                else if (letterStates[i] == State.Yellow)
                {
                    Letters[i].Color = Color.FromUint(0xffc9b458);
                }

                else if (letterStates[i] == State.Green)
                {
                    Letters[i].Color = Color.FromUint(0xff6aaa64);
                }
                
            }
        }

        public bool Validate(char[] correctAnswer)
        {
            int count = 0;
            State[] letterStates = new State[5] { State.Gray, State.Gray, State.Gray, State.Gray, State.Gray };
            State[] answerStates = new State[5] { State.Gray, State.Gray, State.Gray, State.Gray, State.Gray };
            int length = Letters.Length;

            for (int i = 0; i < length; i++)
            {
                if (Letters[i].Input == correctAnswer[i])
                {
                    count++;
                    letterStates[i] = State.Green;
                    answerStates[i] = State.Green;
                }
            }

            if (count == length)
            {
                ColorFields(letterStates);
                return true;
            }

            for (int i = 0; i < length; i++)
            {
                if (letterStates[i] != State.Green)
                {
                    for (int j = 0; j < length; j++)
                    {
                        if (Letters[i].Input == correctAnswer[j] && answerStates[j] == State.Gray)
                        {
                            letterStates[i] = State.Yellow;
                            answerStates[j] = State.Yellow;
                            break;
                        }
                    }
                }
            }

            ColorFields(letterStates);
            return false;
            
        }
    }
}
