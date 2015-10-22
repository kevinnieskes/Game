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
        int startingHealth; 

        public Soldier(string s)
        {
            name = s;
            shootDam = 10;
            grenadeDam = 25;
            health = 40;
            grenades = 2;
            startingHealth = health;
        }

        public int Shoot()
        {
            return shootDam;
        }

        public int Throw()
        {
            if (grenades == 0)
            {
                return 0;
            }
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
                health += Convert.ToInt32(startingHealth * .45);
                return Convert.ToInt32(startingHealth * .45);
            }
        }

        //BFT TODO: Make function name a verb
        public int Health()
        {      
            return health;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
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
