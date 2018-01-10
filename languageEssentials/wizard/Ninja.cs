namespace wizard {

    public class Ninja : Human
    {
        public Ninja(string name) : base(name)
        {
            dexterity = 175;
        }

        public void Steal(Human victim)
        {
            health += 10;
        }

        public void get_away()
        {
            health -= 15;
        }

        
    }

}