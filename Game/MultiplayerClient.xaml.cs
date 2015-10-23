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
using System.Windows.Shapes;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Game
{
    /// <summary>
    /// Interaction logic for MultiplayerClient.xaml
    /// </summary>
    public partial class MultiplayerClient : Window
    {
        Battle skirmish;
        int enemiesDefeated;
        NetworkStream stream;
        TcpListener server;
        TcpClient client;
        string YourAction;
       
        public MultiplayerClient()
        {
            InitializeComponent();
            skirmish = new Battle();
            UpdateStats();
            ImageDefault();
            YourAction = "";
        }

        private void ShootButton_Click(object sender, RoutedEventArgs e)
        {
            YourAction = "Shoot";
            TurnButton.IsEnabled = true;
        }

        private void GrenadeButton_Click(object sender, RoutedEventArgs e)
        {
            YourAction = "Grenade";
            TurnButton.IsEnabled = true;
        }

        private void HealButton_Click(object sender, RoutedEventArgs e)
        {
            YourAction = "Heal";
            TurnButton.IsEnabled = true;
        }

        private void AimButton_Click(object sender, RoutedEventArgs e)
        {
            YourAction = "Aim";
            TurnButton.IsEnabled = true;
        }

        private void TurnButton_Click(object sender, RoutedEventArgs e)
        {
            GrenadeButton.IsEnabled = false;
            HealButton.IsEnabled = false;
            ShootButton.IsEnabled = false;
            string action = Recieve();
            int damage = skirmish.DoAction(skirmish.Player1, skirmish.Player2, action);
            if (damage == 0)
            {
                SinglePlayerBox.Text += "You took no damage. \n";
            }
            else
            {
                SinglePlayerBox.Text += "You took " + ("" + damage) + " damage. \n";
            }
            Player2AnimationSelect(action);
            System.Threading.Thread.Sleep(900);
            ImageDefault();
            UpdateStats();
            if (skirmish.Player1.Health() <= 0)
            {
                Victory();
            }
            Send(YourAction);
            damage = skirmish.DoAction(skirmish.Player2, skirmish.Player1, YourAction);
            if (damage == 0)
            {
                SinglePlayerBox.Text += "You did no damage. \n";
            }
            else
            {
                SinglePlayerBox.Text += "You did " + ("" + damage) + " damage. \n";
            }
            Player1AnimationSelect(action);
            System.Threading.Thread.Sleep(900);
            ImageDefault();
            UpdateStats();
            if (skirmish.Player1.Health() <= 0)
            {
                SinglePlayerBox.Text += "You have been defeated by the enemy. \n";
                GrenadeButton.IsEnabled = false;
                HealButton.IsEnabled = false;
                ShootButton.IsEnabled = false;
            }
            GrenadeButton.IsEnabled = true;
            HealButton.IsEnabled = true;
            ShootButton.IsEnabled = true;
            TurnButton.IsEnabled = false;


        }

        private void Player1ShootAnimate()
        {
            Player1Image.Source = (new Uri(@"Sprites\RedShot.Gif", UriKind.Relative));
        }
        private void Player1GrenadeAnimate()
        {

            Player2Image.Source = (new Uri(@"Sprites\GreenExplosion.Gif", UriKind.Relative));
        }

        private void Player1HealAnimate()
        {
            ImageDefault();
        }

        private void Player2ShootAnimate()
        {

            Player2Image.Source = (new Uri(@"Sprites\GreenShoot.Gif", UriKind.Relative));
        }

        private void Player2GrenadeAnimate()
        {

            Player1Image.Source = (new Uri(@"Sprites\RedExplosion2.Gif", UriKind.Relative));
        }

        private void Player2HealAnimate()
        {

            ImageDefault();
        }

        private void Player1AnimationSelect(string action)
        {
            if (action == "Shoot")
            {
                Player1ShootAnimate();
            }
            else if (action == "Throw")
            {
                Player1GrenadeAnimate();
            }
            else if (action == "Heal")
            {
                Player1HealAnimate();
            }
            else if (action == "Aim")
            {
                Player1HealAnimate();
            }
        }

        private void Player2AnimationSelect(string action)
        {
            if (action == "Shoot")
            {
                Player2ShootAnimate();
            }
            else if (action == "Throw")
            {
                Player2GrenadeAnimate();
            }
            else if (action == "Heal")
            {
                Player2HealAnimate();
            }
            else if (action == "Aim")
            {
                Player2HealAnimate();
            }
        }

        private void UpdateStats()
        {
            YourHP.Text = "Your Health: " + ("" + skirmish.Player1.Health());
            EnemyHP.Text = "Enemy Health: " + ("" + skirmish.Player2.Health());
            YourGrenades.Text = "Grenades: " + skirmish.Player1.grenades;
            EnemyGrenades.Text = "Grenades: " + skirmish.Player2.grenades;
        }

        private void Victory()
        {
            SinglePlayerBox.Text += "You defeated the enemy. Victory is yours. But the war rages on... \n";
            GrenadeButton.IsEnabled = false;
            HealButton.IsEnabled = false;
            ShootButton.IsEnabled = false;
        }

        private void ImageDefault()
        {
            Player1Image.Source = (new Uri(@"Sprites\redidle.gif", UriKind.Relative));
            Player2Image.Source = (new Uri(@"Sprites\greenidle.gif", UriKind.Relative));
        }
        public void Send(string action)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(action);
            stream = client.GetStream();
            stream.Write(data, 0, data.Length);
        }

        public string Recieve()
        {
            Byte[] data = new Byte[256];
            stream = client.GetStream();
            String responseData = String.Empty;
            Int32 bytes = stream.Read(data, 0, data.Length);
            string action = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            return action;
        }

        public void Join()
        {
            try
            {
                Int32 port = 13000;
                client = new TcpClient("10.2.20.13", port);
                ConnectionBox.Text = "Connected.";
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectionBox.Text = "Attempting Connection to Host.";
            Join();
        }

        
    }
}
