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
using Frog.Utilities;


namespace Frog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game game;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        void GameOver()
        {

            string message = string.Empty;
            for (int i = 0; i < game.Players.Count; i++)
            {
                message += $"{game.Players[i].Name} Score: {game.Players[i].Score} \n";
            }


            MessageBox.Show(message);
            InitializeGame();
        }

        void InitializeGame()
        {
            if (game != null)
            {
                game.Players.Clear();
                game.ItemsOnScreen.Clear();
                game.GameOver -= GameOver;
                game = null;
                this.DataContext = null;

            }

            game = new Game();
            DataContext = game;
            game.GameOver += GameOver;
        }

    }
}
