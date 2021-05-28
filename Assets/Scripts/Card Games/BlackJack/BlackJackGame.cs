using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackJackGame : MonoBehaviour
{
    #region Enums
    public enum winCondition
    {
        FiveCardCharlie,
        BlackJack,
        Win,
        Bust,
        Loss,
        None
    }
    #endregion
    #region Props/feilds
    //int i = 0;
    public List<Sprite> cardImages;
    public Sprite backOfCard;
    Deck deck = new Deck();
    int bet = 0;
    winCondition payout = winCondition.None;

    //first is dealer 2cd is player.
    Card[] playerHand = new Card[6];
    public Image[] playerButtons = new Image[5];
    int playerCards = 2;
    Card[] dealerHand = new Card[6];
    public Image[] dealerButtons = new Image[5];
    int dealerCards = 2;
    #endregion
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
        for (int i = 0; i < playerButtons.Length; i++)
        {
            playerButtons[i].gameObject.SetActive(false);
            dealerButtons[i].gameObject.SetActive(false);
        }
        deck.Shuffle();
        for (int i = 0; i < 2; i++)
        {
            playerButtons[i].gameObject.SetActive(true);
            playerHand[i] = deck.Draw();
        }
        for (int i = 0; i < 2; i++)
        {
            dealerButtons[i].gameObject.SetActive(true);
            dealerHand[i] = deck.Draw();
        }
    }
    void Update()
    {
        for (int i = 0; i < playerButtons.Length; i++)
        {
            if(playerButtons[i].IsActive())playerButtons[i].sprite = playerHand[i].sprite;
            if(i>0 && dealerButtons[i].IsActive()) dealerButtons[i].sprite = dealerHand[i].sprite;
        }

        if(payout == winCondition.Bust)
        {
            handlePayout();
        }
    }

    #region CardMethods
    public void OnHit()
    {
        if(playerCards < 6)
        {
            playerHand[playerCards] = deck.Draw();
            playerButtons[playerCards].gameObject.SetActive(true);
            playerCards++;

            if (GetHandTotal(playerHand) > 21)
            {
                payout = winCondition.Bust;
            }
            Debug.Log(payout.ToString());
        }
    }
    public void OnStand()
    {
        Debug.Log("Standing");
        dealerButtons[0].sprite = dealerHand[0].sprite;
        while(GetHandTotal(dealerHand) < 17)
        {
            dealerHand[dealerCards] = deck.Draw();
            dealerButtons[dealerCards].gameObject.SetActive(true);
            dealerCards++;
            Debug.Log("Dealer hit");
        }
        Debug.Log("Dealer Stayed");
        CheckWin();
        Debug.Log(payout.ToString());
    }
    #endregion
    #region Helper
    public void CheckWin()
    {
        int pTotal = GetHandTotal(playerHand);
        int dTotal = GetHandTotal(dealerHand);
        if (pTotal > 21) { payout = winCondition.Bust; }
        else if (pTotal <= 21 && playerHand.Length == 5) { payout = winCondition.FiveCardCharlie; }
        else if (pTotal == 21) { payout = winCondition.BlackJack; }
        else if (dTotal < pTotal || pTotal < dTotal && dTotal > 21) { payout = winCondition.Win; }
        else if (dTotal > pTotal && dTotal <= 21) { payout = winCondition.Loss; }
        else if (dTotal == pTotal) payout = winCondition.None;
    }
    public int handlePayout()
    {
        switch (payout)
        {
            case winCondition.Loss:
                bet = 0;
                break;
            case winCondition.Bust:
                bet = 0;
                break;
            case winCondition.Win:
                bet *= 2;
                break;
            case winCondition.BlackJack:
                bet *= 3;
                break;
            case winCondition.FiveCardCharlie:
                bet *= 4;
                break;
        }
        return bet;
    }
    public int GetHandTotal(Card[] hand)
    {
        int total = 0;
        int handSize = 0;
        int AceCounter = 0;
        if (hand == dealerHand) handSize = dealerCards;
        if (hand == playerHand) handSize = playerCards;
        for(int i = 0; i < handSize; i++)
        {
            if (hand[i].value > 1 && hand[i].value < 10) total += hand[i].value;
            else if (hand[i].value == 1) { total += 11; AceCounter++; }
            else total += 10;
        }
        while(total > 21 && AceCounter > 0)
        {
            total -= 10;
        }
        return total;
    }
    #endregion
}
