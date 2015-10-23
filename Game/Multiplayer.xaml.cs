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
    /// Interaction logic for Window3.xaml
    /// </summary>
    /// //BFT TODO: Condense the code for all game modes (you should have one engine that accepts input from keyboard or network, not multiple sets of the same logic)

    public partial class Window2 : Window
    {
        Battle skirmish;
        int enemiesDefeated;
        NetworkStream stream;
        TcpListener server;
        TcpClient client;
        string YourAction;
        int playerTurn;
        bool local;
        bool single;
        bool host;
        public Window2()
        {

            InitializeComponent();
            skirmish = new Battle();
            UpdateStats();
            ImageDefault();
            YourAction = "";
            local = false;
            single = false;
            host = false;
            playerTurn = 0;
        }

        public Window2(string loc)
        {

            InitializeComponent();
            skirmish = new Battle();
            UpdateStats();
            ImageDefault();
            YourAction = "";
            playerTurn = 1; 
            local = true;
            single = false;
            host = false;
            playerTurn = 1;
        }

        public Window2(int sing)
        {

            InitializeComponent();
            skirmish = new Battle();
            UpdateStats();
            ImageDefault();
            YourAction = "";
            local = false;
            single = true;
            host = false;
            enemiesDefeated = 0;
            playerTurn = 0;
        }

        public Window2(char isHost)
        {

            InitializeComponent();
            skirmish = new Battle();
            UpdateStats();
            ImageDefault();
            YourAction = "";
            local = false;
            single = false;
            host = true;
            playerTurn = 0;
        }

        private void ShootButton_Click(object sender, RoutedEventArgs e)
        {
            YourAction = "Shoot";
            if(single == true || local == true)
            {
                SingleShoot();
                return;
            }
            
            TurnButton.IsEnabled = true;
        }

        private void GrenadeButton_Click(object sender, RoutedEventArgs e)
        {
            YourAction = "Throw";
            if (single == true || local == true)
            {
                SingleGrenade();
                return;
            }
            
            TurnButton.IsEnabled = true;
        }

        private void HealButton_Click(object sender, RoutedEventArgs e)
        {
            YourAction = "Heal";
            if (single == true || local == true)
            {
                SingleHeal();
                return;
            }
            YourAction = "Heal";
            TurnButton.IsEnabled = true;
        }

        private void AimButton_Click(object sender, RoutedEventArgs e)
        {
            YourAction = "Aim";
            if (single == true)
            {
                SingleAim();
                return;
            }
            TurnButton.IsEnabled = true;
        }

        private void TurnButton_Click(object sender, RoutedEventArgs e)
        {
            if (single == false && local == false)
            {
                if(host == true)
                {
                    MultiPlayerHostTurn();
                }
                else if(host == false)
                {
                    ClientTurn();
                }
            }
        }
        

        private void Player1ShootAnimate()
        {
            Player1Image.Source = (new Uri(@"Sprites\redShot.Gif", UriKind.Relative));
        }
        private void Player1GrenadeAnimate()
        {

            Player2Image.Source = (new Uri(@"Sprites\greenexplo.Gif", UriKind.Relative));
        }

        private void Player1HealAnimate()
        {
            ImageDefault();
        }

        private void Player2ShootAnimate()
        {

            Player2Image.Source = (new Uri(@"Sprites\greenshot.Gif", UriKind.Relative));
        }

        private void Player2GrenadeAnimate()
        {

            Player1Image.Source = (new Uri(@"Sprites\redExplo.Gif", UriKind.Relative));
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
            if(local == true)
            {
                UpdateStatsAlternate();
                return;
            }
            YourHP.Text = "Your Health: " + ("" + skirmish.Player1.Health());
            EnemyHP.Text = "Enemy Health: " + ("" + skirmish.Player2.Health());
            YourGrenades.Text = "Grenades: " + skirmish.Player1.grenades;
            EnemyGrenades.Text = "Grenades: " + skirmish.Player2.grenades;
        }

        private void UpdateStatsAlternate()
        {
            YourHP.Text = "Player1 Health: " + ("" + skirmish.Player1.Health());
            EnemyHP.Text = "Player2 Health: " + ("" + skirmish.Player2.Health());
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
        

        

        private void SingleShoot()
        {
            if (local == true && playerTurn == 2)
            {
                LocalTurn();
                return;
            }
            int damage = skirmish.DoAction(skirmish.Player1, skirmish.Player2, "Shoot");
            if (damage == 0)
            {
                SinglePlayerBox.Text += "Attack Missed! \n";
                DoEnemyTurn();
                return;
            }
            int CurrentHealth = skirmish.Player1.Health();
            Player1ShootAnimate();
            System.Threading.Thread.Sleep(900);
            ImageDefault();
            SinglePlayerBox.Text += "You shoot the enemy for " + ("" + skirmish.Player1.shootDam) + " damage. \n";
            UpdateStats();
            if (Convert.ToInt32(skirmish.Player2.Health()) <= 0)
            {
                Victory();
                return;
            }
            playerTurn = 2;
            if (local == false)
            {
                DoEnemyTurn();
            }
            
        }

        private void SingleGrenade()
        {
            if (local == true && playerTurn == 2)
            {
                LocalTurn();
                return;
            }
            if (skirmish.Player1.grenades == 0)
            {
                SinglePlayerBox.Text += "Out of grenades! \n";
                return;
            }
            int damage = skirmish.DoAction(skirmish.Player1, skirmish.Player2, "Throw");
            if (damage == 0)
            {
                SinglePlayerBox.Text += "Attack Missed! \n";
                playerTurn = 2;
                if (local == false)
                {
                    DoEnemyTurn();
                }
                return;
            }
            Player1GrenadeAnimate();
            System.Threading.Thread.Sleep(900);
            ImageDefault();
            SinglePlayerBox.Text += "Your grenade hit the enemy for " + ("" + damage) + " damage. \n";
            UpdateStats();
            if (Convert.ToInt32(skirmish.Player2.Health()) <= 0)
            {
                Victory();
                return;
            }
            playerTurn = 2;
            if (local == false)
            {
                DoEnemyTurn();
            }
        }

        private void SingleHeal()
        {
            if (local == true && playerTurn == 2)
            {
                LocalTurn();
                return;
            }
            Player1HealAnimate();
            SinglePlayerBox.Text = "You healed for " + skirmish.Player1.Heal() + "\n";
            UpdateStats();
            playerTurn = 2;
            if (local == false)
            {
                DoEnemyTurn();
            }
        }

        

        private void SingleAim()
        {
            if (local == true && playerTurn == 2)
            {
                LocalTurn();
                return;
            }
            skirmish.Player1.Aim();
            SinglePlayerBox.Text = "You took aim at the enemy. \n";
            playerTurn = 2;
            if (local == false)
            {
                DoEnemyTurn();
            }
        }

        private void NewEnemyButton_Click(object sender, RoutedEventArgs e)
        {
            skirmish.Player1.grenades += skirmish.Player2.grenades;
            skirmish.Player1.shootDam += 5;
            SinglePlayerBox.Text += "Your skill with your weapon has improved, you now do " + skirmish.Player1.shootDam + " damage per shot. \n";
            SinglePlayerBox.Text += "You healed for " + skirmish.Player1.Heal() + " before the next battle started. \n";
            if (enemiesDefeated == 1)
            {
                skirmish.badGuy = new ComputerEnemy("Medic");
                skirmish.Player2 = new Soldier("");
            }
            else
            {
                skirmish.badGuy = new ComputerEnemy(3);
                skirmish.Player2 = new Soldier("");
            }
            UpdateStats();
            GrenadeButton.IsEnabled = true;
            HealButton.IsEnabled = true;
            ShootButton.IsEnabled = true;
            //NewEnemyButton.IsEnabled = false;

        }

        private void MultiPlayerHostTurn()
        {
            GrenadeButton.IsEnabled = false;
            HealButton.IsEnabled = false;
            ShootButton.IsEnabled = false;
            Send(YourAction);
            int damage = skirmish.DoAction(skirmish.Player1, skirmish.Player2, YourAction);
            if (damage == 0)
            {
                SinglePlayerBox.Text += "You did no damage. \n";
            }
            else
            {
                SinglePlayerBox.Text += "You did " + ("" + damage) + " damage. \n";
            }
            Player1AnimationSelect(YourAction);
            System.Threading.Thread.Sleep(900);
            ImageDefault();
            UpdateStats();
            if (skirmish.Player2.Health() <= 0)
            {
                Victory();
            }
            string action = "";
            while (action == "")
            {
                action = Recieve();
            }
            damage = skirmish.DoAction(skirmish.Player2, skirmish.Player1, action);
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

        private void ClientTurn()
        {
            GrenadeButton.IsEnabled = false;
            HealButton.IsEnabled = false;
            ShootButton.IsEnabled = false;
            string action = "";
            while (action == "")
            {
                action = Recieve();
            }
            int damage = skirmish.DoAction(skirmish.Player2, skirmish.Player1, action);
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
                SinglePlayerBox.Text += "You have been defeated by the enemy. \n";
                GrenadeButton.IsEnabled = false;
                HealButton.IsEnabled = false;
                ShootButton.IsEnabled = false;
            }
            Send(YourAction);
            damage = skirmish.DoAction(skirmish.Player1, skirmish.Player2, YourAction);
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
            if (skirmish.Player2.Health() <= 0)
            {
                Victory();
            }
            GrenadeButton.IsEnabled = true;
            HealButton.IsEnabled = true;
            ShootButton.IsEnabled = true;
            TurnButton.IsEnabled = false;
        }

        private void LocalTurn()
        {
            playerTurn = 1;
            int damage = skirmish.DoAction(skirmish.Player2, skirmish.Player1, YourAction);
            if (damage == 0)
            {
                SinglePlayerBox.Text += "Player2 did no damage. \n";
            }
            else
            {
                SinglePlayerBox.Text += "Player2 did " + ("" + damage) + " damage. \n";
            }
            Player2AnimationSelect(YourAction);
            System.Threading.Thread.Sleep(900);
            ImageDefault();
            UpdateStats();
            if (skirmish.Player1.Health() <= 0)
            {
                Victory();
            }
        }

        private void DoEnemyTurn()
        {
            int damage = skirmish.DoAction(skirmish.Player2, skirmish.Player1, skirmish.badGuy.Fight());
            if (damage == 0)
            {
                SinglePlayerBox.Text += "You took no damage. \n";
            }
            else
            {
                SinglePlayerBox.Text += "You took " + ("" + damage) + " damage. \n";
            }
            Player2AnimationSelect(skirmish.badGuy.ReturnAction());
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

        public void Host()
        {
            server = null;
            try
            {
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Any;
                server = new TcpListener(localAddr, port);
                server.Start();
                client = server.AcceptTcpClient();
                ConnectionBox.Text += "Connected. \n";
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }
        }

        public void Join()
        {
            try
            {
                Int32 port = 13000;
                client = new TcpClient("10.2.20.13", port);
                ConnectionBox.Text += "Connected.";
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }
        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if(host == false)
            {
                ClientConnect();
                return;
            }
            ConnectionBox.Text += "Waiting for player connection. \n";
            Host();
        }
        private void ClientConnect()
        {
            ConnectionBox.Text += "Attempting Connection to Host. \n";
            if(local == true || single == true)
            {
                ConnectionBox.Text += "Cannot connect to server in single player or local multiplayer. Please launch appropriate game mode from main window.";
                return;
            }
            Join();
        }

        
    }
}


