using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{

    public List<Card> hand = new List<Card>();
    public List<Card> deck = new List<Card> ();
    
    private int nbCards = 40;

    void Start()
    {
        BuildDeck();
        
    }

    private void BuildDeck() {
        // Add (nbCards) random cards to the deck
        for (int i = 0; i < nbCards; ++i)
        {
            int randomCard = Random.Range(0, CardDatabase.cardDBSize);
            deck.Add(CardDatabase.cardList[randomCard]);
        }
    }

}
