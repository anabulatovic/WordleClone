using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WordleClone.Model;

namespace WordleClone.ViewModel
{
    public partial class GameViewModel : ObservableObject
    {
        [ObservableProperty]
        private Row[] rows;
        private int rowIndex;
        private int columnIndex;

        [RelayCommand]
        public void Enter()
        {
            if (columnIndex != 5)
            {
                return;
            }
            bool valid = true;
            if (valid)
            {
                if (rowIndex == 5)
                {
                    // end of game
                }
                else
                {
                    rowIndex++;
                    columnIndex = 0;
                }
                rowIndex= 0;
                columnIndex= 0;
            }
        }
        [RelayCommand]
        public void EnterLetter(char letter) 
        { 
            if (columnIndex == 6)
            {

            }
        }


    }
}
