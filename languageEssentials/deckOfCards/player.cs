using System.Collections.Generic;

namespace deckOfCards
{
    public class Player
    {
        public string name;
        public List<Card> hand = new List<Card>();

        public Player(string cName){
            name = cName;
        }

        public Card draw(Deck deck){
            Card card = deck.deal();
            hand.Add(card);
            return card;
        }

        public Card discard(int idx){
            if (hand.Count <= idx){
                return null;
            }
            Card discarded = hand[idx];
            hand.RemoveAt(idx);
            return discarded;
        }
    }
}