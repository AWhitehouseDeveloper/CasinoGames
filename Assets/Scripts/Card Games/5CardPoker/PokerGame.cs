using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Enums
public enum winCondition
{
    RoyalFlush,
    StraightFlush,
    FourOfAKind,
    FullHouse,
    Flush,
    Straight,
    ThreeOfAKind,
    TwoPair,
    Pair,
    None
}
#endregion
public class PokerGame : MonoBehaviour
{
    public List<Sprite> cardImages;             //used to create cards in deck
    Deck deck = new Deck();                     //contains all the cards of the deck with suit and value
    public int bet = 0;                         //amount the player bet on the game
    winCondition payout = winCondition.None;    //multiplier for the payout based on win condition.
    Card[] hand = new Card[5];                  //players hand


    void Start()
    {
        int index = 0;
        //makes each card
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
        DrawCards(5);
        CheckPayout();
        for(int i = 0; i < hand.Length; i++)
        {
            Debug.Log(hand[i].value + hand[i].suit.ToString());
        }
        Debug.Log(payout.ToString());
    }

    void Update()
    {
        
    }

    public void OnDraw()
    {
        Debug.Log("Drawing");
        DrawCards(1);
        CheckPayout();
    }

    public void DrawCards(int numOfCards)
    {
        for (int i = 0; i < numOfCards; i++)
        {
            hand[i] = deck.Draw();
        }
        CheckPayout();
    }

    public void CheckPayout()
    {
        bool fullHousePoss = false;
        int numOfPairs = 0;
        for (int i = 0; i < hand.Length - 1; i++)
        {
            winCondition possWinCondition = winCondition.None;
            int cSameNum = 1;
            int cSameSuit = 1;
            //int inOrder = 1;
            for (int j = 0; j < hand.Length; j++)
            {
                if(j != i)
                {
                    if (hand[i].value == hand[j].value)
                    {
                        cSameNum++;
                    }
                    if (hand[i].suit == hand[j].suit)
                    {
                        cSameSuit++;
                    }
                }
            }
            possWinCondition = handleWincondition(cSameNum,cSameSuit,ref fullHousePoss, ref numOfPairs);
            if (possWinCondition < payout) payout = possWinCondition;
        }
    }

    public winCondition handleWincondition(int Num, int Suit, ref bool FullHouse, ref int Pairs)
    {
        if (Num == 2)
        {
            FullHouse = true;
            Pairs++;
            return winCondition.Pair;
        }
        if(Num == 2 && Pairs > 2){ return winCondition.TwoPair; }
        if (Num == 3)
        {
            FullHouse = true;
            return winCondition.ThreeOfAKind;
        }
        if (Num == 4) { return winCondition.FourOfAKind; }
        if (Suit == 5) {return winCondition.Flush; }
        if ((FullHouse && Num == 2) || (FullHouse && Num == 3)) { return winCondition.FullHouse; }
        return winCondition.None;
    }
}
