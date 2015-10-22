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
            //Player1Image.Source = (new Uri(@"C:\Users\KNieskes_be\Documents\Visual Studio 2013\Projects\Game\Game\Sprites\redidle.png"));
            SinglePlayerBox.Text = "You shoot the enemy for " + ("" + skirmish.Player1.shootDam) + " damage. \n" ;
            Player2HP.Text = "Enemy Health: " + EnemyHealth;
            if(Convert.ToInt32(EnemyHealth) <= 0)
            {
                Victory();
                return;
            }
            DoNextTurn();
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
            //Player1Image.Source = (new Uri(@"C:\Users\KNieskes_be\Documents\Visual Studio 2013\Projects\Game\Game\Sprites\redidle.png"));
            string EnemyHealth = ("" + skirmish.DoAction(skirmish.Player1, skirmish.Player2, "Throw"));
            SinglePlayerBox.Text = "Your grenade hit the enemy for " + ("" + skirmish.Player1.grenadeDam) + " damage. \n";
            Player2HP.Text = "Enemy Health: " + EnemyHealth;
            YourGrenades.Text = "Grenades: " + "" + skirmish.Player1.grenades;
            if (Convert.ToInt32(EnemyHealth) <= 0)
            {
                Victory();
                return;
            }
            DoNextTurn();
        }

        private void HealButton_Click(object sender, RoutedEventArgs e)
        {
            Player1HealAnimate();
            SinglePlayerBox.Text = "You healed for " + skirmish.Player1.Heal() + "\n";
            Player1HP.Text = "Your Health: " + skirmish.Player1.Health();
            DoNextTurn();
        }

        //private void NewEnemyButton_Click(object sender, RoutedEventArgs e)
        //{
        //    skirmish.Player1.grenades += skirmish.Player2.grenades;
        //    skirmish.badGuy = new ComputerEnemy("Medic");
        //    YourGrenades.Text = "Grenades: " + "" + skirmish.Player1.grenades;
        //    skirmish.Player1.shootDam += 5; 
        //    SinglePlayerBox.Text += "Your skill with your weapon has improved, you now do " + skirmish.Player1.shootDam + " damage per shot. \n";
        //    SinglePlayerBox.Text += "You healed for " + skirmish.Player1.Heal() + " before the next battle started. \n";
        //    if(enemiesDefeated == 1)
        //    {
        //        skirmish.badGuy = new ComputerEnemy("Medic");
        //        skirmish.Player2 = new Soldier("");
        //    }
        //    else
        //    {
        //        skirmish.badGuy = new ComputerEnemy(3);
        //        skirmish.Player2 = new Soldier("");
        //    }
        //    YourHP.Text = "Your Health: " + ("" + skirmish.Player1.Health());
        //    EnemyHP.Text = "Enemy Health: " + ("" + skirmish.Player2.Health());
        //    EnemyGrenades.Text = "Grenades: " + "" + skirmish.Player2.grenades;
        //    GrenadeButton.IsEnabled = true;
        //    HealButton.IsEnabled = true;
        //    ShootButton.IsEnabled = true;
        //    //NewEnemyButton.IsEnabled = false;
            
        //}

        //private void DoNextTurn()
        //{
        //    string YourHealth = ("" + skirmish.DoAction(skirmish.Player2, skirmish.Player1, skirmish.badGuy.Fight()));
        //    if (skirmish.Player2.checkDam(skirmish.badGuy.ReturnAction()) == 0)
        //    {
        //        SinglePlayerBox.Text += "You took no damage. \n";
        //    }
        //    else
        //    {
        //        SinglePlayerBox.Text += "You took " + skirmish.Player2.checkDam(skirmish.badGuy.ReturnAction()) + " damage. \n";
        //    }
        //    Player2AnimationSelect(skirmish.badGuy.ReturnAction());
        //    System.Threading.Thread.Sleep(900);
        //    Player2Image.Source = (new Uri(@"Sprites\greenidle.png", UriKind.Relative));
        //    //Player2Image.Source = (new Uri(@"C:\Users\KNieskes_be\Documents\Visual Studio 2013\Projects\Game\Game\Sprites\greenidle.png"));
        //    Player1Image.Source = (new Uri(@"Sprites\redidle.png", UriKind.Relative));
        //    //Player1Image.Source = (new Uri(@"C:\Users\KNieskes_be\Documents\Visual Studio 2013\Projects\Game\Game\Sprites\redidle.png"));
        //    YourHP.Text = "Your Health: " + YourHealth;
        //    EnemyHP.Text = "Enemy Health: " + skirmish.Player2.Health();
        //    if (Convert.ToInt32(YourHealth) <= 0)
        //    {
        //        SinglePlayerBox.Text += "You have been defeated by the enemy. \n";
        //        GrenadeButton.IsEnabled = false;
        //        HealButton.IsEnabled = false;
        //        ShootButton.IsEnabled = false;
        //    }
        //}

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

        //private void Player2AnimationSelect(string action)
        //{
        //    if(action == "Shoot")
        //    {
        //        Player2ShootAnimate();
        //    }
        //    else if(action == "Throw")
        //    {
        //        Player2GrenadeAnimate();
        //    }
        //    else if(action == "Heal")
        //    {
        //        Player2HealAnimate();
        //    }
        //}

        private void UpdateStats()
        {
            Player1HP.Text = "Your Health: " + ("" + skirmish.Player1.Health());
            Player2HP.Text = "Enemy Health: " + ("" + skirmish.Player2.Health());
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
            //NewEnemyButton.IsEnabled = true;
        }

    }
}
