using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{

    public static List<Card> cardList = new List<Card> ();
    public static int cardDBSize = 3;


    private void Awake() {

        cardList.Add(new Card(0, "Moment of Rage", 1, 
                            "Target Neurosis in your Cortex Argues. \n (Returns to the Cortex after Arguing)\n", 
                            CardColor.RED, CardType.THOUGHT, Resources.Load<Sprite>("Moment_of_Rage")));
        cardList.Add(new Card(1, "Studied Move", 3, 
                            "Chainer.\nSculpt 2.\nIf Chained, you choose who starts the Chained Sequence.",      
                            CardColor.YELLOW, CardType.THOUGHT, Resources.Load<Sprite>("Studied_Move")));
        cardList.Add(new Card(2, "Gorudo's Lake", 3, 
                            "Sculpt 2.\n\n [Fluid 1-5.]",                                                        
                            CardColor.BLUE, CardType.THOUGHT, Resources.Load<Sprite>("Gorudo's_Lake")));
    }
}