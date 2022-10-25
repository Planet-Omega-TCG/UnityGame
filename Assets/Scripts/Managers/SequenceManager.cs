using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{

    public static SequenceManager instance; // frowned upon...


    public List<CardController> sequenceCards = new List<CardController>();
    public List<CardController> cards = new List<CardController>();

    public int currentPlayerTurn;

    private void Awake() {
        instance = this;
        
    }

    private void Start() {
        StartTurnGameplay(1);
    }

    // Beginning of each turn
    public void StartTurnGameplay(int playerID) {
        currentPlayerTurn = playerID;
        StartTurn();
        UIManager.instance.UpdateInstantsValues(ResourceManager.instance.instantsAvailable); // Display original instants
    }

    public void StartTurn() {
        GamePlayUIController.instance.UpdateCurrentPlayerTurn(currentPlayerTurn);
        PlayerManager.instance.AssignTurn(currentPlayerTurn); // Assign turns at the beginning of each turn
    }

    public void EndTurn() {
        currentPlayerTurn = currentPlayerTurn == 1 ? 2 : 1;
        StartTurn();
    }

    public void AddCardToSequence(CardController cc) {
        sequenceCards.Add(cc);
        ResourceManager.instance.DiscountInstants(cc.card.instants);

        if (ResourceManager.instance.instantsAvailable == 0)
        {
            Debug.Log("No instants: resolving the seq");
            ResolveSequence();
        }


    }

    public void ResolveSequence() {

        Debug.Log("Resolving the sequence");

        sequenceCards.Reverse();

        if (sequenceCards.Count > 0) sequenceCards[0].ApplyEffect();  // start resolving the first card.
    }

    public void ResolveNextCard() {
        if (sequenceCards.Count > 0)
        {
            Debug.Log("Removing card: " + sequenceCards[0].card.ToString());
            sequenceCards.Remove(sequenceCards[0]);
            if (sequenceCards.Count > 0)
            {
                sequenceCards[0].ApplyEffect();
            }
        }
    }
}
