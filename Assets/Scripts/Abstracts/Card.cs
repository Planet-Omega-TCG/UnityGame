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
    NEUROSIS
}

[System.Serializable]
public class Card {

    public int          id;
    public string       cardName;
    public int          instants;
    public string       description;
    public CardType     type;
    public CardColor    color;
    public Sprite       image;
    public int          ownerID;

    public Card(int id, string cardName, int instants, string description, CardColor color, CardType type, Sprite image) {
        this.id          = id;
        this.cardName    = cardName;
        this.instants    = instants;
        this.description = description;
        this.color       = color;
        this.type        = type;
        this.image       = image;
    }

}
