using System;
using System.Collections.Generic;

namespace deckOfCards
{
    public class Deck
    {
        public List<Card> cards = new List<Card>();

        string[] faces = {"Ace", "2", "3", "4","5", "6", "7", "8", "9", "10", "Jack", "Queen", "King"};

        string[] suits = {"Hearts", "Clubs", "Diamonds", "Spades"};
        public Deck(){
            reset();  
        }

        public Card deal(){
            Card dealtCard = cards[0];
            cards.RemoveAt(0);
            return dealtCard;
        }

        public void reset(){
            cards.Clear();
            foreach (string suit in suits){
                for (int i=0; i<faces.Length; i++){
                    Card newCard = new Card(faces[i], suit, i+1);
                    cards.Add(newCard);
                }
            } 

        }

        public List<Card> shuffle(){
            Random rand = new Random();
            for (int i=0; i<cards.Count; i++){
                int j = rand.Next(i, cards.Count);
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
            return cards;
        }
    }
}