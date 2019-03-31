
using System.Windows;
using System.Windows.Controls;
using Frog.ViewModels;
using System.Threading.Tasks;

namespace Frog.Views
{
    /// <summary>
    /// Interaction logic for GameControl.xaml
    /// </summary>
    public partial class GameControl : UserControl
    {

        private Game game;

        public GameControl()
        {
            InitializeComponent();
            game = new Game();
            DataContext = game;
            game.GameOver += GameOver;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus();
        }

        private void StartNewGame_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame.IsEnabled = false;
            game.Clear();
            game.Start();
        }

        private void GameOver()
        {

            string message = string.Empty;
            for (int i = 0; i < game.Players.Count; i++)
            {
                message += $"{game.Players[i].Name} Score: {game.Players[i].Score} \n";
            }
            Task task = new Task(() => MessageBox.Show(message));
            task.Start();


            StartNewGame.IsEnabled = true;

        }


    }
}
