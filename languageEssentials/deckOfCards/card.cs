namespace deckOfCards
{
    public class Card
    {
        public string stringVal;
        public string suit;
        public int val;

        public Card(string cStringVal, string cSuit, int cVal){
            stringVal = cStringVal;
            suit = cSuit;
            val = cVal;
        }

    }
}