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

    private void SetupButtons() {
        endTurnButton.onClick.AddListener(() => { TurnManager.instance.EndTurn(); });
    }

    public void UpdateCurrentPlayerTurn(int id) {

        currentPlayerTurnText.gameObject.SetActive(true);
        currentPlayerTurnText.text = $"Player {id}'s turn!";

        // TODO: Make it blink

        StartCoroutine(BlinkCurrentPlayerTurn());

    }

    private IEnumerator BlinkCurrentPlayerTurn() {
        yield return new WaitForSeconds(0.5f);
        currentPlayerTurnText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        currentPlayerTurnText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        currentPlayerTurnText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        currentPlayerTurnText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        currentPlayerTurnText.gameObject.SetActive(false);
    }
}
