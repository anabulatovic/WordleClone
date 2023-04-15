using CommunityToolkit.Mvvm.ComponentModel;

namespace WordleClone.Model
{
    public partial class Letter : ObservableObject
    {

        public Letter() 
        {
            Color = Colors.Black;
        }


        [ObservableProperty]
        private char input;

        [ObservableProperty]
        private Color color;
    }
}
