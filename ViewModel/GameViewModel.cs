using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WordleClone.Model;
using WordleClone.Services;

namespace WordleClone.ViewModel
{
    public partial class GameViewModel : ObservableObject
    {
        [ObservableProperty]
        private Row[] rows;
        private char[] correctAnswer;
        private int rowIndex;
        private int columnIndex;

        private readonly SolutionGeneratorService _service = new SolutionGeneratorService();

        public char[] firstKeyboardRow { get; }
        public char[] secondKeyboardRow { get; }
        public char[] thirdKeyboardRow { get; }

        public GameViewModel() 
        {
            rows = new Row[6]
            {
                new Row(),
                new Row(),
                new Row(), 
                new Row(),
                new Row(),
                new Row()
            };

            Task<string> randomWord = _service.GenerateSolution();

            correctAnswer = randomWord.Result.ToUpper().ToCharArray(); // new char[5]
            firstKeyboardRow = "QWERTYUIOP".ToCharArray();
            secondKeyboardRow = "ASDFGHJKL".ToCharArray();
            thirdKeyboardRow = "<ZXCVBNM>".ToCharArray();

        }

        [RelayCommand]
        public void Enter()
        {
            if (columnIndex != 5)
            {
                return;
            }

            bool valid = Rows[rowIndex].Validate(correctAnswer);

            if (valid)
            {
            }
            
            if (rowIndex == 5)
            {
                App.Current.MainPage.DisplayAlert("Game over!", "You have run out of turns!", "OK");
            }

            else
            {
                rowIndex++;
                columnIndex = 0;
            }
     
        }

        [RelayCommand]
        public void EnterLetter(char letter) 
        { 
            if (letter == '>')
            {
                Enter();
                return;
            }

            if (letter == '<')
            {
                if (columnIndex == 0)
                {
                    return;
                }

                columnIndex--;
                Rows[rowIndex].Letters[columnIndex].Input = ' ';

                return;
            }

            if (columnIndex == 5)
            {
                return;
            }

            Rows[rowIndex].Letters[columnIndex].Input= letter;
            columnIndex++;
        }
    }
}
