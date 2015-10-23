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

namespace Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void JoinButton_Click(object sender, RoutedEventArgs e)
        {
            MultiplayerClient Multiplayer = new MultiplayerClient();
            Multiplayer.Show();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Window2 Multiplayer = new Window2();
            Multiplayer.Show();
        }

        private void SinglePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            Window3 SinglePlayer = new Window3();
            SinglePlayer.Show();
        }

        private void LocalButton_Click(object sender, RoutedEventArgs e)
        {
            LocalMultiplayer Local = new LocalMultiplayer();
            Local.Show();
        }



       
    }
}
