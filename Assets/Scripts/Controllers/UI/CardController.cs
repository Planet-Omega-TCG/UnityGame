using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;

public class CardController : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IDragHandler
{
    public Card card;

    // Card Object UI displays
    public Image nameBackground, descriptionBackground, instantsBackground, cardBackground;
    public Image illustration;
    public TextMeshProUGUI nameText, descriptionText, instantsText;
    private Transform originalParent;

    Vector3 cachedScale;

    // Colors for background UI
    private Color yellow    = new Color(0.98f, 0.85f, 0.47f, 0.9f);
    private Color blue      = new Color(0.37f, 0.43f, 0.72f, 0.9f);
    private Color red       = new Color(0.81f, 0.23f, 0.23f, 0.9f);
    
    public bool resolvingCard = false;

    private void Start() {
        cachedScale = transform.localScale;
    }

    // Write the card "card"'s info into the card object displays.
    public void Initialize(Card card, int ownerID, Transform intendedParent) {

        this.card               = new Card(card) { ownerID = ownerID };

        nameText.text           = card.cardName;
        descriptionText.text    = card.description;
        illustration.sprite     = card.image;
        instantsText.text       = card.instants.ToString();

        ChangeBackgroundColor(card.color);  // Changes background of card depending on its color.

        originalParent = intendedParent; // Gets the original parent of the card (ex: player1Hand, player1Field)
        Tweener tween = transform.DOMove(intendedParent.transform.position, 1f, true);
        transform.DOScale(0.85f, 0.85f);
        tween.onComplete += () => { transform.SetParent(intendedParent); };

    }

    // Change colors of card depending on its color (red, yellow...)
    private void ChangeBackgroundColor(CardColor color) {

        switch (color)
        {
            case CardColor.YELLOW:
                nameBackground.color        = yellow;
                descriptionBackground.color = yellow;
                instantsBackground.color    = yellow - new Color(0f, 0f, 0f, 0.2f);
                nameText.color              = Color.black;
                descriptionText.color       = Color.black;
                instantsText.color          = Color.black;
                break;
            case CardColor.RED:
                nameBackground.color        = red;
                descriptionBackground.color = red;
                instantsBackground.color    = red - new Color(0f, 0f, 0f, 0.2f);
                break;
            case CardColor.BLUE:
                nameBackground.color        = blue;
                descriptionBackground.color = blue;
                instantsBackground.color    = blue - new Color(0f, 0f, 0f, 0.2f);
                break;
        }


    }

    internal void ApplyEffect() {
        resolvingCard = true;
    }

    // When the mouse goes over the card (should zoom in here!)
    public void OnPointerEnter(PointerEventData eventData) {

        //this.transform.SetAsLastSibling(); // bring to the front
        Debug.Log("enter: " + this.transform.GetSiblingIndex());

        //this.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        this.transform.DOScale(1.5f, 0.35f);

    }

    public void OnPointerExit(PointerEventData eventData) {


        Debug.Log("exit: " + this.transform.GetSiblingIndex());
        //this.transform.SetAsFirstSibling();
        //this.transform.localScale = cachedScale;
        this.transform.DOScale(1f, 0.35f);

    }

    // When the card is clicked.
    public void OnPointerDown(PointerEventData eventData) {

        // If the card is not in the sequence and if the player owns the card
        if (originalParent.name == $"Player{this.card.ownerID}Hand") {
            if (SequenceManager.instance.currentPlayerTurn == card.ownerID)
            {
                transform.SetParent(transform.root);
                // When we click we disable the raycast of the background because 
                // we don't want it to think it is being left on the background
                cardBackground.raycastTarget = false;
            }
            else transform.DOShakeScale(0.35f, 0.5f, 5);
        }
        else if (this.resolvingCard) // The card is in the sequence
            {
                transform.SetParent(transform.root);
                cardBackground.raycastTarget = false;
            }
        else transform.DOShakeScale(0.35f, 0.5f, 5);

    }

    // When the card is no longer being clicked.
    public void OnPointerUp(PointerEventData eventData) {

        // If the card is already in the sequence or not owned by player do nothing
        if (originalParent.name == $"Player{this.card.ownerID}Hand")
        {
            if (SequenceManager.instance.currentPlayerTurn == card.ownerID)
            {
                // If the card is already in the sequence do nothing
                cardBackground.raycastTarget = true;  // To be able to pick it up again
                AnalyzePointerUp(eventData);
            }
        }
        else
        {
            if (this.resolvingCard)
            {
                cardBackground.raycastTarget = true;
                AnalyzePointerUp(eventData);
            }
        }
    }

    private void AnalyzePointerUp(PointerEventData eventData) {

        GameObject playArea = eventData.pointerEnter;

        // check if player left card in some playable area
        if (playArea == null) ReturnToOriginalPlace();

        else
        {
            if (playArea.name == "Sequence" && SequenceManager.instance.instantsAvailable >= card.instants)
            {
                PlayCard(playArea.transform);
            }
            else if (playArea.name == $"Player{this.card.ownerID}Past" || playArea.name == $"Player{this.card.ownerID}Memory")
            {
                // Only if card has been resolved
                if (resolvingCard)
                {
                    MoveCardTo(playArea.transform);
                    resolvingCard = false;
                }
                else
                {
                    transform.DOShakeScale(0.25f, 0.25f, 3);
                    ReturnToOriginalPlace();
                }
            }
            else
            {
                transform.DOShakeScale(0.25f, 0.25f, 3);
                ReturnToOriginalPlace();
            }

        } 
    }

    // If card is left in a correct place its parent changes.
    private void PlayCard(Transform playArea) {
        MoveCardTo(playArea);
        SequenceManager.instance.AddCardToSequence(this);
        CardManager.instance.RemoveCardFromHand(this.card);
    }

    // Move this.card to a given transform area (e.g. player1Past, Sequence)
    public void MoveCardTo(Transform playArea) {
        transform.SetParent(playArea);          // The card's parent is where it has been left now.
        transform.localPosition = Vector3.zero; // Place card in the center of the area.
        originalParent = playArea;
    }

    // If card is left in an incorrect place it goes back to where it was.
    private void ReturnToOriginalPlace() {
        Tweener tween = transform.DOMove(originalParent.transform.position, 0.35f, true);
        tween.onComplete += () => { transform.SetParent(originalParent); };
    }

    // When mouse is no longer hovering on card


    // change the card position to the mouse position while dragging.
    public void OnDrag(PointerEventData eventData) {

        if (originalParent.name == $"Player{this.card.ownerID}Hand" && SequenceManager.instance.currentPlayerTurn == card.ownerID)
        {
            transform.position = eventData.position;
        }
        else if (originalParent.name == "Sequence" && resolvingCard)
        {
            transform.position = eventData.position;
        }
    }
    
}
