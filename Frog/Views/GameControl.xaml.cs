using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Frog.ViewModels;

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
            game.Start();
        }

        private void GameOver()
        {

            string message = string.Empty;
            for (int i = 0; i < game.Players.Count; i++)
            {
                message += $"{game.Players[i].Name} Score: {game.Players[i].Score} \n";
            }

            MessageBox.Show(message);

            game.Clear();
            
        }

    }
}
