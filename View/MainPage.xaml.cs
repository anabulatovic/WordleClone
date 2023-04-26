using WordleClone.ViewModel;
using WordleClone.View;

namespace WordleClone;

public partial class MainPage : ContentPage
{
	private GameViewModel _gameViewModel;
    private StatsViewModel _statsViewModel;

    public MainPage(GameViewModel viewModel, StatsViewModel statsViewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_gameViewModel = viewModel;
        _statsViewModel = statsViewModel;
	}

    private async void btnHelp_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HelpView());
    }

    private async void btnStats_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StatsView(_statsViewModel));
    }

}

