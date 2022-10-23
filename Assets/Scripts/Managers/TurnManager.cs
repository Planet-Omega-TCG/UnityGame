using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    public static TurnManager instance; // frowned upon...

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
    }

    public void StartTurn() {
        GamePlayUIController.instance.UpdateCurrentPlayerTurn(currentPlayerTurn);
        PlayerManager.instance.AssignTurn(currentPlayerTurn); // Assign turns at the beginning of each turn
    }



    public void EndTurn() {
        currentPlayerTurn = currentPlayerTurn == 1 ? 2 : 1;
        StartTurn();
    }


}
