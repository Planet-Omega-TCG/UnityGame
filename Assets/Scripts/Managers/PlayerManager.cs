using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;               // typically frowned upon in game industry...
    public List<Player> players = new List<Player>();

    private void Awake() {
        instance = this;
    }

    private void Start() {
        UIManager.instance.UpdatePlayerValues(players[0], players[1]);
    }

    public void CreatePlayers() {
        players.Add(new Player(20, 1));
        players.Add(new Player(20, 2));
    }

    public void LoadTravellersInfo(List<Traveller> travellers) {
        for (int i = 0; i < travellers.Count; ++i)
        {
            players[i].traveller = travellers[i];
        }
    }

    // Updates boolean myTurn for each Player in the list of players
    // Will be called by TurnManager
    internal void AssignTurn(int currentPlayerTurn) {
        foreach (Player player in players) player.myTurn = player.id == currentPlayerTurn;
    }

    // Updates player health and checks whether player is dead
    public void DamagePlayer(int id, int damage) {
        Player p = FindPlayerByID(id);
        FindPlayerByID(id).health -= damage; // use negative damage to heal
        UIManager.instance.UpdatePlayerValues(players[0], players[1]);  // update healt on screen

        if (p.health <= 0) PlayerLost(id);
    }

    private void PlayerLost(int id) {
        UIManager.instance.GameFinished(id==1 ? FindPlayerByID(2) : FindPlayerByID(1));
    }

    public Player FindPlayerByID(int id) {
        foreach(Player p in players) if (p.id == id) return p;
        throw new Exception("Cannot find player with id " + id);
    }

}
