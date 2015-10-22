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

    //BFT TODO: Break each function into smaller functions


    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Battle skirmish;
        int enemiesDefeated;
        public Window1()
        {
            InitializeComponent();
            skirmish = new Battle();
            YourHP.Text = "Your Health: " + ("" + skirmish.Player1.Health());
            EnemyHP.Text = "Enemy Health: " + ("" + skirmish.Player2.Health());
            YourGrenades.Text = "Grenades: " + skirmish.Player1.grenades;
            EnemyGrenades.Text = "Grenades: " + skirmish.Player2.grenades;

        }

        public Window1(string enemy)
        {
            InitializeComponent();
            skirmish = new Battle();
            enemiesDefeated = 0;
        }

        private void ShootButton_Click(object sender, RoutedEventArgs e)
        {
            //skirmish.DoAction(skirmish.Player1, skirmish.Player2, "Shoot");
            string EnemyHealth =  ("" + skirmish.DoAction(skirmish.Player1, skirmish.Player2, "Shoot"));
            int CurrentHealth = skirmish.Player1.Health();
            SinglePlayerBox.Text += "You shoot the enemy for " + ("" + skirmish.Player1.Shoot()) + " damage. \n" ;
            EnemyHP.Text = "Enemy Health: " + EnemyHealth;
            if(Convert.ToInt32(EnemyHealth) <= 0)
            {
                SinglePlayerBox.Text += "You defeated the enemy. Victory is yours. But the war rages on... \n";
                enemiesDefeated++;
                GrenadeButton.IsEnabled = false;
                HealButton.IsEnabled = false;
                ShootButton.IsEnabled = false;
                NewEnemyButton.IsEnabled = true;
                return;
            }
            EnemyTurn();
        }

        private void GrenadeButton_Click(object sender, RoutedEventArgs e)
        {
            //skirmish.DoAction(skirmish.Player1, skirmish.Player2, "Shoot");
            if(skirmish.Player1.grenades == 0)
            {
                SinglePlayerBox.Text += "Out of grenades! \n";
                return;
            }
            string EnemyHealth = ("" + skirmish.DoAction(skirmish.Player1, skirmish.Player2, "Throw"));
            SinglePlayerBox.Text += "Your grenade hit the enemy for " + ("" + skirmish.Player1.grenadeDam) + " damage. \n";
            EnemyHP.Text = "Enemy Health: " + EnemyHealth;
            YourGrenades.Text = "Grenades: " + "" + skirmish.Player1.grenades;
            if (Convert.ToInt32(EnemyHealth) <= 0)
            {
                SinglePlayerBox.Text += "You defeated the enemy. Victory is yours. But the war rages on... \n";
                enemiesDefeated ++;
                GrenadeButton.IsEnabled = false;
                HealButton.IsEnabled = false;
                ShootButton.IsEnabled = false;
                NewEnemyButton.IsEnabled = true;
                return;
            }
            EnemyTurn();
        }

        private void HealButton_Click(object sender, RoutedEventArgs e)
        {
            SinglePlayerBox.Text += "You healed for " + skirmish.Player1.Heal() + "\n";
            YourHP.Text = "Your Health: " + skirmish.Player1.Health();
            EnemyTurn();
        }

        private void NewEnemyButton_Click(object sender, RoutedEventArgs e)
        {
            skirmish.Player1.grenades += skirmish.Player2.grenades;
            skirmish.badGuy = new ComputerEnemy("Medic");
            YourGrenades.Text = "Grenades: " + "" + skirmish.Player1.grenades;
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
            YourHP.Text = "Your Health: " + ("" + skirmish.Player1.Health());
            EnemyHP.Text = "Enemy Health: " + ("" + skirmish.Player2.Health());
            GrenadeButton.IsEnabled = true;
            HealButton.IsEnabled = true;
            ShootButton.IsEnabled = true;
            NewEnemyButton.IsEnabled = false;
            
        }

        //BFT TODO: Make function name a verb
        private void EnemyTurn()
        {
            string YourHealth = ("" + skirmish.DoAction(skirmish.Player2, skirmish.Player1, skirmish.badGuy.Fight()));
            if (skirmish.Player2.checkDam(skirmish.badGuy.ReturnAction()) == 0)
            {
                SinglePlayerBox.Text += "You took no damage. \n";
            }
            else
            {
                SinglePlayerBox.Text += "You took " + skirmish.Player2.checkDam(skirmish.badGuy.ReturnAction()) + " damage. \n";
            }
          
            YourHP.Text = "Your Health: " + YourHealth;
            EnemyHP.Text = "Enemy Health: " + skirmish.Player2.Health();
            if (Convert.ToInt32(YourHealth) <= 0)
            {
                SinglePlayerBox.Text += "You have been defeated by the enemy. \n";
                GrenadeButton.IsEnabled = false;
                HealButton.IsEnabled = false;
                ShootButton.IsEnabled = false;
            }
        }

       
    }
}
