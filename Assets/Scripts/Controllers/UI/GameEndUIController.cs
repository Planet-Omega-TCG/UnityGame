using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameEndUIController : MonoBehaviour
{

    public TextMeshProUGUI winnerName;

    public void Initialize(Player winner) {
        winnerName.text = "Player: " + winner.id + " has won!";
    }
}
