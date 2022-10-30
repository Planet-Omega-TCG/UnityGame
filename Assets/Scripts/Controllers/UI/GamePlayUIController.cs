using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class GamePlayUIController : MonoBehaviour {
    public static GamePlayUIController instance;
    public TextMeshProUGUI currentPlayerTurnText, newSequenceText, resolvingSequenceText;
    public Button endTurnButton, endSequenceButton, okButton;

    private void Awake() {
        instance = this;
        SetupButtons();
    }

    // Sets up buttons in UI
    private void SetupButtons() {
        endTurnButton.onClick.AddListener(() => { SequenceManager.instance.EndTurn(); });
        endSequenceButton.onClick.AddListener(() => SequenceManager.instance.ResolveSequence());
        okButton.onClick.AddListener(() => SequenceManager.instance.ResolveNextCard());
    }

    private void DisplayTravellers(List<CardController> travellers) {

    }

    // Turns on the "Player X's turn" text showing up.
    public void ShowCurrentPlayerTurnText(int id) {

        currentPlayerTurnText.gameObject.SetActive(true);
        currentPlayerTurnText.text = $"Player {id}'s turn!";
        currentPlayerTurnText.transform.SetAsLastSibling(); // bring text on top of cards

        StartCoroutine(BlinkGameObject(currentPlayerTurnText.gameObject, 3, 0.5f, true));

    }

    public void ShowResolvingSequenceText() {
        resolvingSequenceText.gameObject.SetActive(true);
        StartCoroutine(BlinkGameObject(resolvingSequenceText.gameObject, 1, 1.5f, true));
    }

    public void ShowNewSequenceText() {
        newSequenceText.gameObject.SetActive(true);
        StartCoroutine(BlinkGameObject(newSequenceText.gameObject, 1, 1.5f, true));
    }

    private IEnumerator BlinkGameObject(GameObject obj, int times, float time, bool act) {
        bool active = act;
        for(int i = 0; i < times; ++i)
        {
            yield return new WaitForSeconds(time);
            obj.SetActive(!act);
            act = !act;
        }
    }
}
