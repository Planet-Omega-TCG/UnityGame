using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardManager : MonoBehaviour {

    public static CardManager instance;
    public CardController cardControllerPrefab;

    public List<Card> cardDictionary = new List<Card>();
    public List<Traveller> travellerDictionary = new List<Traveller>();

    public Transform player1Hand, player2Hand, player1Traveller, player2Traveller;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        GenerateCards();
    }

    // Generates cards and adds them to each player's hand.
    private void GenerateCards() {
        foreach(Card card in cardDictionary)
        {
            CardController newCard = Instantiate(cardControllerPrefab, player1Hand);
            newCard.transform.localPosition = Vector3.zero;
            newCard.Initialize(card, 1);
        }
        foreach (Card card in cardDictionary)
        {
            CardController newCard = Instantiate(cardControllerPrefab, player2Hand);
            newCard.transform.localPosition = Vector3.zero;
            newCard.Initialize(card, 2);
        }
    }

    public void GenerateTravellers(List<Traveller> travellers) {
        
        CardController t1 = Instantiate(cardControllerPrefab, player1Traveller);
        t1.transform.localPosition = Vector3.zero;
        t1.Initialize(travellers[0], 1);

        CardController t2 = Instantiate(cardControllerPrefab, player2Traveller);
        t2.transform.localPosition = Vector3.zero;
        t2.Initialize(travellers[1], 2);

        PlayerManager.instance.LoadTravellersInfo(travellers);

    }

    public Traveller FindTravellerByName(string name) {

        foreach(Traveller tr in travellerDictionary)
        {
            if (tr.cardName == name) return tr;
        }
        throw new Exception("Cannot find traveller with name " + name);
    }

}
