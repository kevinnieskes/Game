using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Game
{
    //Make this a sub-class of Soldier
    class ComputerEnemy
    {
        private string action;
        int id;
 
        //mindless shooter
        public ComputerEnemy()
        {
            action = "Shoot";
            id = 0;
        }

        //random fighter
        public ComputerEnemy(int num)
        {
            id = 1;
        }

        //healer
        public ComputerEnemy(string healer)
        {
            id = 2;
        }

        public string Fight()
        {
            if (id == 1)
            {
                Random rand = new Random();
                int move = rand.Next(1, 100);
                if (move >= 65)
                {
                    action = "Shoot";
                }
                else if( move <= 64 && move >= 33)
                {
                    action = "Heal";
                }
                else if (move <= 32)
                {
                    action = "Throw";
                }
                
            }
            else if(id == 2)
            {
                Random rand = new Random();
                int move = rand.Next(1, 100);
                if (move >= 55)
                {
                    action = "Shoot";
                }
                else if (move <= 54 && move >= 10)
                {
                    action = "Heal";
                }
                else if (move <= 9)
                {
                    action = "Throw";
                }
            }
            return action;
        }

        public string ReturnAction()
        {
            return action;
        }


    }
}
