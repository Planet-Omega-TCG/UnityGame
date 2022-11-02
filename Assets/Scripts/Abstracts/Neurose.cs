using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neurose : Card {

    public int attackInstants;
    public int attackPower;
    public int blockPower;
    public int blockInstants;

    public Neurose(string cardName, int instants, string description, CardColor color, Sprite image) : base(cardName, instants, description, color, image) {
        this.cardName = cardName;
        this.instants = instants;
        this.description = description;
        this.color = color;
        this.image = image;
    }

}
