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
        private Difficulty difficulty = Difficulty.EASY;
        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        void GameOver()
        {
            InitializeGame();
        }

        void NextLevel()
        {
            if(difficulty == Difficulty.HARD)
            {
                MessageBox.Show("Congratulations, game finished", "Good job");
            }
            else
            {
                difficulty++;
            }
            InitializeGame();
        }

        void InitializeGame()
        {
            if(game!=null)
            {
                game.PlayerLostEvent -= GameOver;
                game.PlayerWonEvent -= NextLevel;
            }
            game = null;
            game = new Game(difficulty);
            DataContext = game;
            game.PlayerLostEvent += GameOver;
            game.PlayerWonEvent += NextLevel;
        }
    }
}
