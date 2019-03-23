﻿using System;
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

        void GameOver(int Score)
        {
            MessageBox.Show($"Congratulations, game finished Your score:{Score} ", "Good job");
        }


        void InitializeGame()
        {
            if(game!=null)
            {
                game.PlayerLostEvent -= GameOver;
            }
            game = null;
            game = new Game();
            DataContext = game;
            game.PlayerLostEvent += GameOver;
        }
    }
}
