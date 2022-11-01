using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardManager : MonoBehaviour {

    public static CardManager instance;
    public CardController cardControllerPrefab;

    public List<Card> cardDictionary            = new List<Card>();
    public List<Traveller> travellerDictionary  = new List<Traveller>();

    public List<Card> player1Deck = new List<Card> ();
    public List<Card> player2Deck = new List<Card> ();
    public List<Card> player1Hand = new List<Card>();
    public List<Card> player2Hand = new List<Card>();

    public Transform player1HandArea, player2HandArea, player1TravellerArea, player2TravellerArea;

    public static int nbCardsPerDeck = 40;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        //GenerateCards();
        BuildDecks();
    }


    private void BuildDecks() {

        // Add desired nb of copies of each card to each deck. Random order is created when drawing.
        foreach (Card card in cardDictionary)
        {
            AddToDeck(1, 4, card);
            AddToDeck(2, 4, card);
        }
        
    }

    // Add 'card' to 'playerId''s deck 'repeat' amount of times.
    private void AddToDeck(int playerId, int repeat, Card card) {

        for (int i = 0; i < repeat; ++i)
        {
            if (playerId == 1) player1Deck.Add(card);
            else player2Deck.Add(card);
        }
    }

    public void GenerateTravellers(List<Traveller> travellers) {
        
        CardController t1 = Instantiate(cardControllerPrefab, player1TravellerArea.root);
        t1.transform.localPosition = Vector3.zero;
        t1.Initialize(travellers[0], 1, player1TravellerArea);

        CardController t2 = Instantiate(cardControllerPrefab, player2TravellerArea.root);
        t2.transform.localPosition = Vector3.zero;
        t2.Initialize(travellers[1], 2, player2TravellerArea);

        PlayerManager.instance.LoadTravellersInfo(travellers);

    }

    public void DrawCards(int nbOfCards) {

        for (int i = 0; i < nbOfCards; ++i)
        {

            // Player1

            int randomCard = UnityEngine.Random.Range(0, player1Deck.Count);
            CardController newCard1 = Instantiate(cardControllerPrefab, player1HandArea.root);
            newCard1.transform.localPosition = Vector3.zero;
            newCard1.Initialize(player1Deck[randomCard], 1, player1HandArea);
            //  Remove from deck and add to hand
            player1Deck.RemoveAt(randomCard);   
            player1Hand.Add(newCard1.card);


            // player 2

            randomCard = UnityEngine.Random.Range(0, player2Deck.Count);
            // choose and initialize random card from deck
            CardController newCard2 = Instantiate(cardControllerPrefab, player2HandArea.root);
            newCard2.transform.localPosition = Vector3.zero;
            newCard2.Initialize(player2Deck[randomCard], 2, player2HandArea);
            //  Remove from deck and add to hand
            player2Deck.RemoveAt(randomCard);
            player2Hand.Add(newCard2.card);
        }
    }

    public void RemoveCardFromHand(Card card) {
        Debug.Log("Removing card: " + card.cardName + " owned by " + card.ownerID);
        if (card.ownerID == 1)   player1Hand.Remove(card);
        else                     player2Hand.Remove(card);
    }

    public Traveller FindTravellerByName(string name) {

        foreach(Traveller tr in travellerDictionary)
        {
            if (tr.cardName == name) return tr;
        }
        throw new Exception("Cannot find traveller with name " + name);
    }

}
