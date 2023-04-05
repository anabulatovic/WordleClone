using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleClone.Model
{
    public partial class Letter : ObservableObject
    {
        [ObservableProperty]
        private char input;

        [ObservableProperty]
        private Color color;
        //public 
    }
}
