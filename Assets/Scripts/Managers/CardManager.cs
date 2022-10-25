using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    public List<Card> cardDictionary = new List<Card>();

    public CardController cardControllerPrefab;

    public Transform player1Hand, player2Hand;


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

}
