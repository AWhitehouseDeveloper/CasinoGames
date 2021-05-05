using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    private static Deck instance;
    public static Deck Instance { get { return instance; } }
    public Card[] deck = new Card[52];
    public List<Sprite> cardImages;
    private int index = 0;

    private void Awake()
    {
        instance = new Deck();
    }

    void Start()
    {
        // create each card and add it to the deck
        int index = 0;
        foreach(Sprite sprite in cardImages)
        {
            Card c = new Card();
            c.sprite = sprite;
            #region HandleSuit
            if (sprite.name.Contains("Clubs"))
            {
                c.suit = eSuit.Clubs;
            }
            else if(sprite.name.Contains("Diamonds"))
            {
                c.suit = eSuit.Diamonds;
            }
            else if(sprite.name.Contains("Hearts"))
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
            deck[index] = c;
            index++;
        }
        Shuffle();
    }

    public Card Draw()
    {
        index++;
        return deck[index - 1];
    }

    public void Shuffle()
    {
        for(int i = 0; i < 300; i++)
        {
            Swap(ref deck[Random.Range(0, 51)], ref deck[Random.Range(0, 51)]);
        }
    }

    public void Swap(ref Card a, ref Card b)
    {
        Card temp = a;
        a = b;
        b = temp;
    }
}
