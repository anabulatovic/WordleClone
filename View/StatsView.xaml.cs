using WordleClone.ViewModel;

namespace WordleClone.View;

public partial class StatsView : ContentPage
{

    public StatsView(StatsViewModel statsViewModel)
	{
		InitializeComponent();
		BindingContext = statsViewModel;
	}
}