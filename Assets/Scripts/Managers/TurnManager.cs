using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    public static TurnManager instance; // frowned upon...

    public int currentPlayerTurn;

    private void Awake() {
        instance = this;
        StartTurnGameplay(1);
    }

    private void Start() {
        StartTurnGameplay(1);
    }

    public void StartTurnGameplay(int playerID) {
        currentPlayerTurn = playerID;
        PlayerManager.instance.AssignTurn(currentPlayerTurn);
        StartTurn();
    }

    public void StartTurn() {
        GamePlayUIController.instance.UpdateCurrentPlayerTurn(currentPlayerTurn);
    }



    public void EndTurn() {
        currentPlayerTurn = currentPlayerTurn == 1 ? 2 : 1;
        StartTurn();
    }


}
