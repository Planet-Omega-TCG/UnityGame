using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thought : Card {

    //public int instants;
    public bool chainer;

    public Thought(string cardName, int instants, string description, CardColor color, Sprite image, bool chainer) : base(cardName, instants, description, color, image) {
        this.cardName = cardName;
        this.instants = instants;
        this.description = description;
        this.color = color;
        this.image = image;
        this.chainer = chainer; 
    }

}
