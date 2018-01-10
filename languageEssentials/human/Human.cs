namespace human
{
    public class Human
    {
        public string name;
        public int strength;
        public int intelligence;
        public int dexterity;
        public int health;

        public Human(string newName)
        {
            name = newName;
            strength = 3;
            intelligence = 3;
            dexterity = 3;
            health = 100;
        }

        public Human(string newName, int newStrength, int newIntelligence, int newDexterity, int newHealth)
        {
            name = newName;
            strength = newStrength;
            intelligence = newIntelligence;
            dexterity = newDexterity;
            health = newHealth;
        }

        public void Attack(object attacked)
        {
            if (attacked is Human)
            {
                Human victim = attacked as Human;
                int damage = 5 * strength;
                victim.health -= damage;
            }

        }
    }
}