using System;

namespace deckOfCards
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck myDeck = new Deck();

            myDeck.shuffle();
        
            Player myPlayer = new Player("me");

            myPlayer.draw(myDeck);
            myPlayer.draw(myDeck);
            myPlayer.draw(myDeck);
            myPlayer.draw(myDeck);

            foreach (Card card in myPlayer.hand){
                Console.WriteLine("{0} of {1}", card.stringVal, card.suit);
            }

            myPlayer.discard(2);

            Console.WriteLine("******************");
            foreach (Card card in myPlayer.hand){
                Console.WriteLine("{0} of {1}", card.stringVal, card.suit);
            }
        }
    }
}
