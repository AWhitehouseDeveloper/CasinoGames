using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PokerGame : MonoBehaviour
{
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
    #region properties/fields
    public int cardSwaps = 0;
    public List<Sprite> cardImages;             //used to create cards in deck
    Deck deck = new Deck();                     //contains all the cards of the deck with suit and value
    public int bet = 0;                         //amount the player bet on the game
    winCondition payout = winCondition.None;    //multiplier for the payout based on win condition.
    Card[] hand = new Card[5];                  //players hand
    public Button[] buttons = new Button[5];    //Buttons on ui that will give us that cards index
    public int[] cardIndex = new int[5] { -1, -1, -1, -1, -1 };  //Storing card index to swap card.
    #endregion
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
    }
    void Update()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.sprite = hand[i].sprite;
            if(buttons[i].GetComponent<CardButton>().isSelected)
            {
                cardIndex[i] = i;
            }
            else
            {
                cardIndex[i] = -1;
            }
        }
    }
    #region CardMethods
    public void OnDraw()
    {
        SwapCards();
    }
    public void DrawCards(int numOfCards)
    {
        for (int i = 0; i < numOfCards; i++)
        {
            hand[i] = deck.Draw();
        }
        CheckPayout();
    }
    public void SwapCards()
    {
        if(cardSwaps<3)
        {
            for (int i = 0; i < cardIndex.Length; i++)
            {
                if (cardIndex[i] == -1) continue;
                hand[cardIndex[i]] = deck.Draw();
                cardIndex[i] = -1;
            }
            foreach (Button button in buttons)
            {
                button.GetComponent<CardButton>().isSelected = false;
                button.GetComponent<Image>().color = Color.white;
            }
            cardSwaps++;
        }
        if(cardSwaps >= 2)
        {
            CheckPayout();
            Debug.Log(payout.ToString());
            switch (payout)
            {
                case winCondition.RoyalFlush:
                    bet *= 100001;
                    break;
                case winCondition.StraightFlush:
                    bet *= 10001;
                    break;
                case winCondition.FourOfAKind:
                    bet *= 1001;
                    break;
                case winCondition.FullHouse:
                    bet *= 101;
                    break;
                case winCondition.Flush:
                    bet *= 51;
                    break;
                case winCondition.Straight:
                    bet *= 26;
                    break;
                case winCondition.ThreeOfAKind:
                    bet *= 11;
                    break;
                case winCondition.TwoPair:
                    bet *= 6;
                    break;
                case winCondition.Pair:
                    bet *= 2;
                    break;
                case winCondition.None:
                    bet = 0;
                    break;
            }
        }
        
    }
    #endregion
    #region Helper
    public void CheckPayout()
    {
        payout = winCondition.None;
        bool fullHousePoss = false;
        int numOfPairs = 0;
        for (int i = 0; i < hand.Length - 1; i++)
        {
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
            winCondition poss = handleWincondition(cSameNum,cSameSuit,ref fullHousePoss, ref numOfPairs);
            if (poss < payout) payout = poss;
        }
    }
    public winCondition handleWincondition(int Num, int Suit, ref bool FullHouse, ref int Pairs)
    {
        winCondition condition = winCondition.None;
        if (Num == 2)
        {
            Pairs++;
            condition = winCondition.Pair;
        }
        if(Num == 2 && Pairs > 2){ return winCondition.TwoPair; }
        if (Num == 3)
        {
            FullHouse = true;
            condition = winCondition.ThreeOfAKind;
        }
        if (Suit == 5) {condition = winCondition.Flush; }
        if ((FullHouse && Num == 2)) { condition = winCondition.FullHouse; }
        if (Num == 4) { condition = winCondition.FourOfAKind; }
        return condition;
    }
    #endregion
}
