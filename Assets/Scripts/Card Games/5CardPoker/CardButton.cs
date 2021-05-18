using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardButton : MonoBehaviour
{
    public PokerGame game;
    public bool isSelected = false;
    
    public void OnSelect()
    {
        if(game.cardSwaps < 3)
        {
            for (int i = 0; i < game.buttons.Length; i++)
            {
                if(!isSelected)
                {
                    isSelected = true;
                    gameObject.GetComponent<Image>().color = Color.gray;
                }
                else
                {
                    isSelected = false;
                    gameObject.GetComponent<Image>().color = Color.white;
                }
            }
        }
    }
}
