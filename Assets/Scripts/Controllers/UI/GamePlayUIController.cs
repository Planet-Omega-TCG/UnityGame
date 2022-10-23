using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class GamePlayUIController : MonoBehaviour
{
    public static GamePlayUIController instance;
    public TextMeshProUGUI currentPlayerTurnText;
    public Button endTurnButton;

    private void Awake() {
        instance = this;
        SetupButtons();
    }

    // Sets up buttons in UI
    private void SetupButtons() {
        endTurnButton.onClick.AddListener(() => { TurnManager.instance.EndTurn(); });
    }

    // Turns on the "Player X's turn" text showing up.
    public void UpdateCurrentPlayerTurn(int id) {

        currentPlayerTurnText.gameObject.SetActive(true);
        currentPlayerTurnText.text = $"Player {id}'s turn!";

        StartCoroutine(BlinkCurrentPlayerTurn());

    }

    // Makes Text saying player's turn blink
    private IEnumerator BlinkCurrentPlayerTurn() {
        yield return new WaitForSeconds(0.5f);
        currentPlayerTurnText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        currentPlayerTurnText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        currentPlayerTurnText.gameObject.SetActive(false);
    }
}
