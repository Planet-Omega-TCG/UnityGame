using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum CardColor {
    RED,
    YELLOW,
    BLUE,
    GENERIC,
    TRAVELLER
}

public enum CardType {
    CHAINERTHOUGHT,
    THOUGHT,
    NEUROSIS,
    TRAVELLER
}

// Card should be the superclass of Traveller, Thought and Neurose.
[System.Serializable]
public class Card {

    public string       cardName;
    public int          instants;
    public string       description;
    public CardColor    color;
    public Sprite       image;
    public int          ownerID;

    public Card() {

    }

    public Card(string cardName, int instants, string description, CardColor color, Sprite image) {
        this.cardName    = cardName;
        this.instants    = instants;
        this.description = description;
        this.color       = color;
        this.image       = image;
    }

    public Card(Card card) {
        cardName        = card.cardName;
        instants        = card.instants;
        description     = card.description;
        color           = card.color;
        image           = card.image;
    }

    public override string ToString() { 
        return cardName + " (" + ownerID + ") i: " + instants;
    }

}
