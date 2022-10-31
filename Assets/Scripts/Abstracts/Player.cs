using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{
    public int health;
    public int id;
    public Card traveller;

    public int memorySlots;
    public int manaBase;
    public int extraDraw;

    public bool myTurn;

    public Player(int health, int id) {
        this.health = health;
        this.id     = id;
    }

}
