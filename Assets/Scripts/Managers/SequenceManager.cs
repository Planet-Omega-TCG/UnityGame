using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{

    public static SequenceManager instance; 

    public List<CardController> sequenceCards = new List<CardController>(); // cards in the sequence

    public int currentPlayerTurn;   // player who's turn it is
    public int startSequencePlayer; // player who started this sequence

    public int instantsAvailable;


    private void Awake() {
        instance = this;     
    }

    private void Start() {
        startSequencePlayer = 1;
        StartSequenceGameplay(startSequencePlayer);
    }

    // Beginning of each sequence. playerId will start.
    public void StartSequenceGameplay(int playerID) {

        currentPlayerTurn = playerID;       // this player starts now
        instantsAvailable = ThrowDice(4);   // Four dice are thrown at the beginning of each sequence
        CardManager.instance.DrawCards(instantsAvailable/4); 
        StartTurn(playerID);

        // UI
        GamePlayUIController.instance.ShowNewSequenceText();                                    // Display new sequence sign
        UIManager.instance.UpdateInstantsValues(instantsAvailable);                             // Display new instants available

    }

    public void StartTurn(int playerID) {
        GamePlayUIController.instance.ShowCurrentPlayerTurnText(playerID);
        PlayerManager.instance.AssignTurn(playerID); // Assign turns at the beginning of each turn
    }

    public void EndTurn() {
        currentPlayerTurn = currentPlayerTurn == 1 ? 2 : 1;
        StartTurn(currentPlayerTurn);
    }

    public void AddCardToSequence(CardController cc) {
        sequenceCards.Add(cc);
        DiscountInstants(cc.card.instants);

        if (instantsAvailable == 0)
        {
            Debug.Log("No instants: resolving the seq");
            ResolveSequence();
        }
    }

    public void ResolveSequence() {

        if (sequenceCards.Count > 0)
        {
            ApplySequenceDamage(currentPlayerTurn, instantsAvailable);
            sequenceCards.Reverse();
            GamePlayUIController.instance.ShowResolvingSequenceText(); // Show Resolving Sequence Sign
            sequenceCards[0].ApplyEffect();  // start resolving the first card.
        }
        else EndSequence();
    }

    public void ResolveNextCard() {
        if (sequenceCards.Count > 0)
        {
            sequenceCards.Remove(sequenceCards[0]);
            if (sequenceCards.Count > 0) sequenceCards[0].ApplyEffect();
            else EndSequence();
        }
        else EndSequence();
        
    }

    private void EndSequence() {
        // check any passive effects
        StartSequenceGameplay(startSequencePlayer == 1 ? 2 : 1); // each time a new sequence starts 
    }

    private void ApplySequenceDamage(int player, int damage) {
        PlayerManager.instance.DamagePlayer(player, damage);
    }

    // Returns the outcome of throwing n dice
    private int ThrowDice(int n) {
        int nb = 0;
        for (int i = 0; i < n; ++i) nb += UnityEngine.Random.Range(1, 6);
        return nb;
    }

    // Discounts used instants and calls to update the counter
    public void DiscountInstants(int instants) {
        instantsAvailable -= instants;
        UIManager.instance.UpdateInstantsValues(instantsAvailable);
    }

}
