using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    int i = 0;
    public List<Sprite> cardImages;
    Deck deck = new Deck();
    int bet = 0;
    int winPayout = 2;

    //first is dealer 2cd is player.
    //List<Player> players = new List<Player>(); 

    void Start()
    {
        int index = 0;
        foreach (Sprite sprite in cardImages)
        {
            Card c = new Card();
            c.sprite = sprite;
            #region HandleSuit
            if (sprite.name.Contains("Clubs"))
            {
                c.suit = eSuit.Clubs;
            }
            else if (sprite.name.Contains("Diamonds"))
            {
                c.suit = eSuit.Diamonds;
            }
            else if (sprite.name.Contains("Hearts"))
            {
                c.suit = eSuit.Hearts;
            }
            else
            {
                c.suit = eSuit.Spades;
            }
            #endregion
            #region HandleValue
            if (sprite.name.EndsWith("A")) c.value = 1;
            else if (sprite.name.EndsWith("2")) c.value = 2;
            else if (sprite.name.EndsWith("3")) c.value = 3;
            else if (sprite.name.EndsWith("4")) c.value = 4;
            else if (sprite.name.EndsWith("5")) c.value = 5;
            else if (sprite.name.EndsWith("6")) c.value = 6;
            else if (sprite.name.EndsWith("7")) c.value = 7;
            else if (sprite.name.EndsWith("8")) c.value = 8;
            else if (sprite.name.EndsWith("9")) c.value = 9;
            else if (sprite.name.EndsWith("0")) c.value = 10;
            else if (sprite.name.EndsWith("J")) c.value = 11;
            else if (sprite.name.EndsWith("Q")) c.value = 12;
            else if (sprite.name.EndsWith("K")) c.value = 13;
            #endregion
            deck.deck[index] = c;
            index++;
        }
        deck.Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        if(i < 52)
        {
            OnDraw();
            i++;
        }
    }

    public void OnDraw()
    {
        Debug.Log("Drawing");
        deck.Draw();
    }

    public void DrawCards(int numOfCards)
    {
        for (int i = 0; i < numOfCards; i++)
        {
            
        }
    }
}
