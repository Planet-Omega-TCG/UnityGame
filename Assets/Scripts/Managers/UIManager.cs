using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    public TextMeshProUGUI player1HealthText, player2HealthText, instantsCounterText;
    public GameEndUIController gameEndUI;
    public GameStartUIController gameStartUI;

    private List<PlayerManager> players;

    private void Awake() {
        instance = this;

        GameStarted();
    }

    public void GameStarted() {
        PlayerManager.instance.CreatePlayers(); // Players are created
        gameStartUI.gameObject.SetActive(true); //This screen asks for travellers
        gameStartUI.AskForTravellers();
    }

    public void GameFinished(Player winner) {
        gameEndUI.gameObject.SetActive(true);
        gameEndUI.Initialize(winner);
    }

    public void UpdatePlayerValues(Player player1, Player player2) {
        UpdateHealthValues(player1.health, player2.health);
    }

    public void UpdateInstantsValues(int newInstants) {
        instantsCounterText.text = newInstants.ToString();
    }

    public void UpdateHealthValues(int p1Health, int p2Health) {
        player1HealthText.text = p1Health.ToString();
        player2HealthText.text = p2Health.ToString();
    }

}
