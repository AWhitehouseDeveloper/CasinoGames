using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck
{
    public Card[] deck = new Card[52];
    private int index = 0;

    public void Draw()
    {
        if(index >= deck.Length)
        {
            Debug.Log("Out of Cards");
            index--;
        }
        Debug.Log(deck[index].value + deck[index].suit.ToString());
        index++;
        //return deck[index - 1];
    }

    public void Shuffle()
    {
        index = 0;
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
