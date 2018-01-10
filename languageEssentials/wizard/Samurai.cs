namespace wizard {

    public class Samurai : Human
    {
        public Samurai(string name) : base(name)
        {
            health = 200;
        }

        public void death_blow(Human victim) {
            if (victim.health < 50) {
                victim.health = 0;
            }
        }

        public void meditate() {
            health = 200;
        }


        
    }

}