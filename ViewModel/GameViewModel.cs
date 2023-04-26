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

        private SolutionService _solutionService;
        private StatsViewModel _statsViewModel;

        public char[] firstKeyboardRow { get; }
        public char[] secondKeyboardRow { get; }
        public char[] thirdKeyboardRow { get; }

        public GameViewModel(SolutionService solutionService, StatsViewModel statsViewModel)
        {
            _solutionService = solutionService;
            _statsViewModel = statsViewModel;

            firstKeyboardRow = "QWERTYUIOP".ToCharArray();
            secondKeyboardRow = "ASDFGHJKL".ToCharArray();
            thirdKeyboardRow = "⌫ZXCVBNM↵".ToCharArray();

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
        public async void Enter()
        {

            if (columnIndex != 5)
            {
                return;
            }

            char[] input = new char[5];

            for (int i = 0; i < 5; i++)
            {
                input[i] = Rows[rowIndex].Letters[i].Input;
            }
            
            string inputWord = new string(input);
            bool exists = _solutionService.Exists(inputWord).Result;

            if (!exists)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Input must be a valid word", "OK");
                return;
            }

            else
            {
                bool isValid = Rows[rowIndex].Validate(correctAnswer);

                string word = new string(correctAnswer);

                if (isValid)
                {
                    try
                    {
                        WordEntry entry = _solutionService.GetDefinition(word).Result;
                        _statsViewModel.IncrementGamesWon();
                        _statsViewModel.IncrementGamesPlayed();
                        _statsViewModel.UpdateAverageAttempt(rowIndex + 1);
                        await Application.Current.MainPage.DisplayAlert("Congratulations", word + " (" + entry.PartOfSpeech + ")\n" + entry.Definition, "Play again", "Cancel");
                    }

                    catch 
                    {
                        await Application.Current.MainPage.DisplayAlert("Congratulations", word + "\nWe currently do not have a definition for this word.", "OK");
                    }
                    

                    NewGame();

                }

                else if (rowIndex == 5)
                {
                    try
                    {
                        WordEntry entry = _solutionService.GetDefinition(word).Result;
                        await Application.Current.MainPage.DisplayAlert("Game over!", "You have run out of turns!\n" + word + " (" + entry.PartOfSpeech + ")\n" + entry.Definition, "Play again");
                    }

                    catch
                    {
                        await Application.Current.MainPage.DisplayAlert("Game over!", "You have run out of turns!\n" + word + "\nWe currently do not have a definition for this word.", "OK");
                    }
                    _statsViewModel.IncrementGamesPlayed();

                    NewGame();
                }

                else
                {
                    rowIndex++;
                    columnIndex = 0;
                }
            }
        }

        [RelayCommand]
        public void EnterLetter(char letter) 
        { 
            if (letter == '↵')
            {
                Enter();
                return;
            }

            if (letter == '⌫')
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

        private void NewGame()
        {
            //InitializeGame();

            GetSolution();

            for (int i = 0; i < rowIndex + 1; i++)
            {
                for (int j = 0; j < columnIndex; j++)
                {
                    Rows[i].Letters[j].Input = ' ';
                    Rows[i].Letters[j].Color = Colors.Black;
                }
            }

            rowIndex = 0;
            columnIndex = 0;

        }
    }
}
