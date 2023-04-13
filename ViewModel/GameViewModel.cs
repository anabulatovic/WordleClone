using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Drawing;
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

        private SolutionService _solutionService;

        public char[] firstKeyboardRow { get; }
        public char[] secondKeyboardRow { get; }
        public char[] thirdKeyboardRow { get; }

        public GameViewModel(SolutionService solutionService)
        {
            _solutionService = solutionService;

            firstKeyboardRow = "QWERTYUIOP".ToCharArray();
            secondKeyboardRow = "ASDFGHJKL".ToCharArray();
            thirdKeyboardRow = "<ZXCVBNM>".ToCharArray();

            InitializeGame();
        }

        public async void GetSolution()
        {
            string word = await _solutionService.GetSolution();
            correctAnswer = word.ToUpper().ToCharArray();
        }

        private void InitializeGame()
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

            GetSolution();
        }

        [RelayCommand]
        public void Enter()
        {
            if (columnIndex != 5)
            {
                return;
            }

            bool isValid = Rows[rowIndex].Validate(correctAnswer);

            string word = new string(correctAnswer);
            

            if (isValid)
            {
                WordEntry entry = _solutionService.GetDefinition(word).Result;
                Application.Current.MainPage.DisplayAlert("Congratulations", word + entry.PartOfSpeech + entry.Definition, "Play again", "Cancel");
                
                //if (playAgain)
                //{
                //    NewGame();
                //}
                
                
            }
            
            if (rowIndex == 5)
            {
                /*bool playAgain = */
                WordEntry entry = _solutionService.GetDefinition(word).Result;
                Application.Current.MainPage.DisplayAlert("Game over!", "You have run out of turns!\n" + word + " (" + entry.PartOfSpeech + ")\n"+ entry.Definition, "Play again");//.Result;

                //for (int i = 0; i < rowIndex + 1; i++)
                //{
                //    for (int j = 0; j < columnIndex; j++)
                //    {
                //        Rows[i].Letters[j].Input = ' ';
                //        Rows[i].Letters[j].Color = Microsoft.Maui.Graphics.Color.FromUint(0xFF212121);
                //    }

                //}
                //NewGame();

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

            Rows[rowIndex].Letters[columnIndex].Input = letter;
            columnIndex++;
        }

        //private void NewGame()
        //{
        //    InitializeGame();

        //    for (int i = 0; i < rowIndex + 1; i++)
        //    {
        //        for (int j = 0; j < columnIndex; j++)
        //        {
        //            Rows[i].Letters[j].Input = ' ';
        //            Rows[i].Letters[j].BackgroundColor = Colors.Gray;
        //        }

        //    }

        //}
    }
}
