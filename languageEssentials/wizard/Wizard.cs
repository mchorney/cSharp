using System;

namespace wizard {


    public class Wizard : Human
    {
        public Wizard(string name) : base(name)
        {
            health = 50;
            intelligence = 25;
        }

        public void Heal()
        {
            health += 10 * health;
        }

        public void Fireball(Human opponent)
        {
            Random rand = new Random();
            opponent.health -= rand.Next(20, 50);
        }
    }

}