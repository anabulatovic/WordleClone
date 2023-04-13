using WordleClone.Services;
using WordleClone.ViewModel;

namespace WordleClone;

public partial class MainPage : ContentPage
{
	private GameViewModel _gameViewModel;
	public MainPage(GameViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_gameViewModel = viewModel;
	}

	//protected override void OnAppearing()
	//{
 //       _gameViewModel.GetSolution();
 //   }
}

