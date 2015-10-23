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

namespace Game
{
    /// <summary>
    /// Interaction logic for LocalMultiplayer.xaml
    /// </summary>
    /// 
     
    public partial class LocalMultiplayer : Window
    {
        int CurrentPlayer;
        Battle skirmish;
        int enemiesDefeated;

        public LocalMultiplayer()
        {
            InitializeComponent();
            skirmish = new Battle();
            UpdateStats();
        }

        private void ShootButton_Click(object sender, RoutedEventArgs e)
        {
            string EnemyHealth =  ("" + skirmish.DoAction(skirmish.Player1, skirmish.Player2, "Shoot"));
            int CurrentHealth = skirmish.Player1.Health();
            Player1ShootAnimate();
            System.Threading.Thread.Sleep(900);
            Player1Image.Source = (new Uri(@"Sprites\redidle.png", UriKind.Relative));
            SinglePlayerBox.Text += "Player1" + " shot the enemy for " + ("" + skirmish.Player1.shootDam) + " damage. \n" ;
            Player2HP.Text = "Enemy Health: " + EnemyHealth;
            if(Convert.ToInt32(EnemyHealth) <= 0)
            {
                Victory();
                return;
            }
            DoNextTurn(1);
        }

        private void GrenadeButton_Click(object sender, RoutedEventArgs e)
        {
            if(skirmish.Player1.grenades == 0)
            {
                SinglePlayerBox.Text += "Out of grenades! \n";
                return;
            }
            Player1GrenadeAnimate();
            System.Threading.Thread.Sleep(900);
            Player2Image.Source = (new Uri(@"Sprites\Greenidle.png", UriKind.Relative));
            string EnemyHealth = ("" + skirmish.DoAction(skirmish.Player1, skirmish.Player2, "Throw"));
            SinglePlayerBox.Text += "Player1" + "'s grenade hit the enemy for " + ("" + skirmish.Player1.grenadeDam) + " damage. \n";
            Player2HP.Text = "Player2 Health: " + EnemyHealth;
            YourGrenades.Text = "Grenades: " + "" + skirmish.Player1.grenades;
            if (Convert.ToInt32(EnemyHealth) <= 0)
            {
                Victory();
                return;
            }
            DoNextTurn(1);
        }

        private void HealButton_Click(object sender, RoutedEventArgs e)
        {
            Player1HealAnimate();
            SinglePlayerBox.Text += "Player1" + " healed for " + skirmish.Player1.Heal() + "\n";
            Player1HP.Text = "Player1 Health: " + skirmish.Player1.Health();
            DoNextTurn(1);
        }

        //Player 2 Button Controls

        private void ShootButtonRight_Click(object sender, RoutedEventArgs e)
        {
            string Player2HPHealth = ("" + skirmish.DoAction(skirmish.Player2, skirmish.Player1, "Shoot"));
            int CurrentHealth = skirmish.Player1.Health();
            Player2ShootAnimate();
            System.Threading.Thread.Sleep(900);
            Player2Image.Source = (new Uri(@"Sprites\greenidle.png", UriKind.Relative));
            SinglePlayerBox.Text += "Player2 shot the enemy for " + ("" + skirmish.Player1.shootDam) + " damage. \n";
            Player2HP.Text = "Player2 Health: " + Player2HPHealth;
            if (Convert.ToInt32(Player2HPHealth) <= 0)
            {
                Victory();
                return;
            }
            DoNextTurn(2);
        }

        private void GrenadeButtonRight_Click(object sender, RoutedEventArgs e)
        {
            string Player2HPHealth = ("" + skirmish.DoAction(skirmish.Player2, skirmish.Player1, "Throw"));
            int CurrentHealth = skirmish.Player1.Health();
            Player2GrenadeAnimate();
            System.Threading.Thread.Sleep(900);
            Player2Image.Source = (new Uri(@"Sprites\greenidle.png", UriKind.Relative));
            SinglePlayerBox.Text += "Player2 shot the enemy for " + ("" + skirmish.Player1.shootDam) + " damage. \n";
            Player2HP.Text = "Player2 Health: " + Player2HPHealth;
            if (Convert.ToInt32(Player2HPHealth) <= 0)
            {
                Victory();
                return;
            }
            DoNextTurn(2);
        }

        private void HealButtonRight_Click(object sender, RoutedEventArgs e)
        {
            Player2HealAnimate();
            SinglePlayerBox.Text += "Player2 healed for " + skirmish.Player2.Heal() + "\n";
            Player2HP.Text = "Player2 Health: " + skirmish.Player2.Health();
            DoNextTurn(2);
        }

        //Starts New Match
        private void RematchButton_Click(object sender, RoutedEventArgs e)
        {
            skirmish.Player1 = new Soldier("");
            skirmish.Player2 = new Soldier("");
            UpdateStats();
            RematchButton.IsEnabled = false;
            DoNextTurn(1);
        }

        private void DoNextTurn(int player)
        {
            Player2Image.Source = (new Uri(@"Sprites\greenidle.png", UriKind.Relative));
            Player1Image.Source = (new Uri(@"Sprites\redidle.png", UriKind.Relative));
            if (player == 1)
            {
                GrenadeButton.IsEnabled = false;
                HealButton.IsEnabled = false;
                ShootButton.IsEnabled = false;
                GrenadeButtonRight.IsEnabled = true;
                HealButtonRight.IsEnabled = true;
                ShootButtonRight.IsEnabled = true;
            }
            else if (player == 2)
            {
                GrenadeButtonRight.IsEnabled = false;
                HealButtonRight.IsEnabled = false;
                ShootButtonRight.IsEnabled = false;
                GrenadeButton.IsEnabled = true;
                HealButton.IsEnabled = true;
                ShootButton.IsEnabled = true;
            }
        }

        // Animations
        private void Player1ShootAnimate()
        {
            Player1Image.Source = (new Uri(@"Sprites\RedShoot.Gif", UriKind.Relative));      
        }
        private void Player1GrenadeAnimate()
        {
            Player2Image.Source = (new Uri(@"Sprites\GreenExplosion.Gif", UriKind.Relative));
        }   
        private void Player1HealAnimate()
        {
            Player1Image.Source = (new Uri(@"Sprites\redidle.png", UriKind.Relative));
        }
        private void Player2ShootAnimate()
        {
            Player2Image.Source = (new Uri(@"Sprites\GreenShoot.Gif", UriKind.Relative));          
        }

        private void Player2GrenadeAnimate()
        {

            Player1Image.Source = (new Uri(@"Sprites\RedExplosion.Gif", UriKind.Relative));         
        }

        private void Player2HealAnimate()
        {
            Player2Image.Source = (new Uri(@"Sprites\greenidle.png", UriKind.Relative));
        }

        // Updates Text
        private void UpdateStats()
        {
            Player1HP.Text = "Player1 Health: " + ("" + skirmish.Player1.Health());
            Player2HP.Text = "Player2 Health: " + ("" + skirmish.Player2.Health());
            YourGrenades.Text = "Grenades: " + skirmish.Player1.grenades;
            EnemyGrenades.Text = "Grenades: " + skirmish.Player2.grenades;
        }

        private void Victory()
        {
            SinglePlayerBox.Text += "You defeated the enemy. Victory is yours. But the war rages on... \n";
            enemiesDefeated++;
            GrenadeButton.IsEnabled = false;
            HealButton.IsEnabled = false;
            ShootButton.IsEnabled = false;
            RematchButton.IsEnabled = true;
        }
        private void ImageDefault()
        {
            Player1Image.Source = (new Uri(@"Sprites\redidle.gif", UriKind.Relative));
            Player2Image.Source = (new Uri(@"Sprites\greenidle.png", UriKind.Relative));
        }

       

    }
}
