using WordleClone.ViewModel;

namespace WordleClone;

public partial class MainPage : ContentPage
{

	public MainPage(GameViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}



}

