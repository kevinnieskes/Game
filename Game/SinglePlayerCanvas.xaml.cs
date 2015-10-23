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
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        Battle skirmish;
        int enemiesDefeated;
        public Window3()
        {
            
            InitializeComponent();
            skirmish = new Battle();
            UpdateStats();
            ImageDefault();
        }

        private void ShootButton_Click(object sender, RoutedEventArgs e)
        {
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
            SinglePlayerBox.Text += "You shoot the enemy for " + ("" + skirmish.Player1.shootDam) + " damage. \n" ;
            UpdateStats();
            if (Convert.ToInt32(skirmish.Player2.Health()) <= 0)
            {
                Victory();
                return;
            }
            DoEnemyTurn();
        }

        private void GrenadeButton_Click(object sender, RoutedEventArgs e)
        {
            if(skirmish.Player1.grenades == 0)
            {
                SinglePlayerBox.Text += "Out of grenades! \n";
                return;
            }
            int damage = skirmish.DoAction(skirmish.Player1, skirmish.Player2, "Throw");
            if(damage == 0)
            {
                SinglePlayerBox.Text += "Attack Missed! \n";
                DoEnemyTurn();
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
            DoEnemyTurn();
        }

        private void HealButton_Click(object sender, RoutedEventArgs e)
        {
            Player1HealAnimate();
            SinglePlayerBox.Text = "You healed for " + skirmish.Player1.Heal() + "\n";
            UpdateStats();
            DoEnemyTurn();
        }

        private void AimButton_Click(object sender, RoutedEventArgs e)
        {
            skirmish.Player1.Aim();
            SinglePlayerBox.Text = "You took aim at the enemy. \n";
            DoEnemyTurn();
        }

        private void NewEnemyButton_Click(object sender, RoutedEventArgs e)
        {
            skirmish.Player1.grenades += skirmish.Player2.grenades;
            skirmish.Player1.shootDam += 5; 
            SinglePlayerBox.Text += "Your skill with your weapon has improved, you now do " + skirmish.Player1.shootDam + " damage per shot. \n";
            SinglePlayerBox.Text += "You healed for " + skirmish.Player1.Heal() + " before the next battle started. \n";
            if(enemiesDefeated == 1)
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
            NewEnemyButton.IsEnabled = false;
            
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

        private void Player2AnimationSelect(string action)
        {
            if(action == "Shoot")
            {
                Player2ShootAnimate();
            }
            else if(action == "Throw")
            {
                Player2GrenadeAnimate();
            }
            else if(action == "Heal")
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
            enemiesDefeated++;
            GrenadeButton.IsEnabled = false;
            HealButton.IsEnabled = false;
            ShootButton.IsEnabled = false;
            NewEnemyButton.IsEnabled = true;
        }
       
        private void ImageDefault()
        {
            Player1Image.Source = (new Uri(@"Sprites\redidle.gif", UriKind.Relative));
            Player2Image.Source = (new Uri(@"Sprites\greenidle.png", UriKind.Relative));
        }

    }
}
