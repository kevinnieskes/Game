using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Soldier
    {
        public string name;
        public int shootDam;
        public int grenadeDam;
        public int health;
        public int grenades;
        public int accuracy;
        public int aimCount;
        int startingHealth; 

        public Soldier(string name)
        {
            this.name = name;
            shootDam = 30;
            grenadeDam = 45;
            health = 100;
            grenades = 2;
            accuracy = 85;
            aimCount = 0;
            startingHealth = health;
        }

        public int Shoot()
        {
            Random rand = new Random();
            int aim = rand.Next(1, 100);
            if (aim > accuracy)
            {
                return -0;
            }
            aimCount--;
            return shootDam;
        }

        public int Throw()
        {
            if (grenades == 0)
            {
                return 0;
            }
            Random rand = new Random();
            int aim = rand.Next(1, 100);
            if (aim > accuracy)
            {
                grenades -= 1;
                return 0;
            }
            aimCount--;
            grenades -= 1;
            return grenadeDam;
        }

        public int Heal()
        {
            if(startingHealth <= health)
            {
                return 0;
            }
            else
            {
                startingHealth += Convert.ToInt32(startingHealth * .09);
                health += Convert.ToInt32(startingHealth * .45);
                return Convert.ToInt32(startingHealth * .45);
            }
        }

        public int Aim()
        {
            if(aimCount < 0)
            {
                aimCount = 0;
            }
            accuracy = 100;
            aimCount += 3;
            return accuracy;
        }

        //BFT TODO: Make function name a verb
        public int Health()
        {      
            return health;
        }

        public int TakeDamage(int damage)
        {
            health -= damage;
            return damage;
        }

        public int checkDam(string attack)
        {
            if (attack == "Shoot")
            {
                return shootDam;
            }
            else if (attack == "Throw")
            {
                return grenadeDam;
            }
            else if (attack == "Heal")
            {
                return 0;
            }
            return 0;

        }
    }
}
