using CommunityToolkit.Mvvm.ComponentModel;

namespace WordleClone.ViewModel
{
    public partial class StatsViewModel : ObservableObject
    {
        [ObservableProperty]
        private int gamesWon;
        [ObservableProperty]
        private int gamesPlayed;
        [ObservableProperty]
        private float averageAttemptOfSuccess;
        [ObservableProperty]
        private string gameWinPercentage;

        public StatsViewModel()
        {
            gamesWon = Preferences.Default.Get("wonGames", 0);
            gamesPlayed = Preferences.Default.Get("playedGames", 0);
            averageAttemptOfSuccess = Preferences.Default.Get("averageAttemptOfSuccess", 0f);
            gameWinPercentage = (gamesPlayed == 0 ? 0 : (float)gamesWon / gamesPlayed * 100).ToString() + "%";
        }

        public void IncrementGamesWon()
        {
            Preferences.Default.Set("wonGames", ++gamesWon);
        }

        public void IncrementGamesPlayed()
        {
            Preferences.Default.Set("playedGames", ++gamesPlayed);
            gameWinPercentage = ((float)gamesWon / gamesPlayed * 100).ToString() + "%";
        }

        public void UpdateAverageAttempt(int attempt)
        {
            averageAttemptOfSuccess = (averageAttemptOfSuccess * (gamesWon - 1) + attempt) / gamesWon;
            Preferences.Default.Set("averageAttemptOfSuccess", averageAttemptOfSuccess);
        }
    }
}
