using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameStartUIController : MonoBehaviour
{

    public static GameStartUIController instance;

    public List<Traveller> chosenTravellers = new List<Traveller>();


    public TextMeshProUGUI playerIdText;
    public List<Button> cardButtons;


    private void Awake() {
        instance = this;
        SetupCardButtons();
    }

    private void SetupCardButtons() {
        foreach (Button button in cardButtons)
        {
            button.onClick.AddListener(() => { this.AssignAndAskForNextTraveller(button.name); });
        }
    }

    public void AskForTravellers() {
        playerIdText.text = "Player " + PlayerManager.instance.players[0].id.ToString() + ": ";
    }

    public void AssignAndAskForNextTraveller(String chosenTravellerName) {

        // Add traveller to the list
        Traveller traveller = CardManager.instance.FindTravellerByName(chosenTravellerName);
        chosenTravellers.Add(traveller);

        // Ask for next traveller
        playerIdText.text = "Player " + PlayerManager.instance.players[1].id.ToString() + ": ";

        if (chosenTravellers.Count == 2) {
            CardManager.instance.GenerateTravellers(chosenTravellers);
            this.gameObject.SetActive(false);
            SequenceManager.instance.StartSequenceGameplay(1);
        }

    }
}
