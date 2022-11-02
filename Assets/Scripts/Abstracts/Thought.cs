using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThoughtType {
    CHAINER,
    THOUGHT,
    NEUROTIC,
    TRAUMA,
    NONE
}

public class Thought : Card {

    //public int instants;
    public ThoughtType thoughtType;

    public Thought(string cardName, int instants, string description, ThoughtType thoughtType, CardColor color, Sprite image) : base(cardName, instants, description, color, image) {
        this.cardName = cardName;
        this.instants = instants;
        this.description = description;
        this.color = color;
        this.image = image;
        this.thoughtType = thoughtType;
    }

}
