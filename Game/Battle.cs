using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Battle
    {
        public Soldier Player1;
        public Soldier Player2;
        //string nextMove;
        public ComputerEnemy badGuy;

        public Battle()
        {
            Player1 = new Soldier("You");
            Player2 = new Soldier("Computer");
            badGuy = new ComputerEnemy();
        }
        public Battle(string enemy)
        {
            Player1 = new Soldier("You");
            Player2 = new Soldier(enemy);
        }

        public int DoAction(Soldier Attacker, Soldier Defender, string action)
        {
            if(action == "Shoot")
            {
                Defender.TakeDamage(Attacker.Shoot());
            }
            else if(action == "Throw")
            {
                Defender.TakeDamage(Attacker.Throw());
            }
            else if (action == "Heal")
            {
                Attacker.Heal();
            }
            return Defender.Health();
        }
    }
}
