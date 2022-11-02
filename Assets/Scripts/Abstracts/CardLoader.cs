using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLoader : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

        TextAsset dataset = Resources.Load<TextAsset>("CardList");
        string[] lines = dataset.text.Split('\n');

        for (int i = 1; i < lines.Length; ++i)
        {

            string[] data = lines[i].Split(',');

            Card card = CreateNewCard(data);
            // Add card to cardDcitionary in CardManager.

        }


    }

    private Card CreateNewCard(string[] data) {

        Card card;

        string colour = data[0].Trim();
        string ctype = data[1].Trim();
        string effect = data[2];
        string flavour = data[3];
        string id = data[4];
        string instants = data[5].Trim();
        string memory = data[6];
        string title = data[9];

        CardColor colorP = CardColor.GENERIC;
        switch (colour)
        {
            case "red":
                colorP = CardColor.RED;
                break;
            case "yellow":
                colorP = CardColor.YELLOW;
                break;
            case "blue":
                colorP = CardColor.BLUE;
                break;
        }

        ThoughtType thoughtTypeP = ThoughtType.NONE;

        switch (ctype)
        {
            // thought
            case "thought":
                thoughtTypeP = ThoughtType.THOUGHT;
                break;
            case "chainer":
                thoughtTypeP = ThoughtType.CHAINER;
                break;
            case "neurotic":
                thoughtTypeP = ThoughtType.NEUROTIC;
                break;
            case "trauma":
                thoughtTypeP = ThoughtType.TRAUMA;
                break;
        }

        int instantsP = 0;
        try
        {
            instantsP = int.Parse(instants);
        }
        catch (Exception e)
        {
            Debug.Log("error parsing " + instants);
        }


        Sprite sprite = Resources.Load<Sprite>("CardSprites/Golos");

        if (thoughtTypeP != ThoughtType.NONE) card = new Thought(title, instantsP, effect, thoughtTypeP, colorP, sprite);
        else card = new Neurose(title, instantsP, effect, colorP, sprite);



        return card;
    }
}



     

/*
public Card(string cardName, int instants, string description, CardColor color, Sprite image) {
    this.cardName = cardName;
    this.instants = instants;
    this.description = description;
    this.color = color;
    this.image = image;
}*/