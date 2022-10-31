using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Traveller : Card
{

    public int DrawExtra;
    public int memorySlots;
    public int instantPower;
    
    public Traveller(string cardName, int instants, string description, CardColor color, Sprite image, int drawExtra, int memorySlots, int instantPower) : base(cardName, instants, description, color, image) {
        this.cardName = cardName;
        this.instants = instants;
        this.description = description;
        this.color = color;
        this.image = image;
        this.DrawExtra = drawExtra;
        this.memorySlots = memorySlots;
        this.instantPower = instantPower;
    }

}
