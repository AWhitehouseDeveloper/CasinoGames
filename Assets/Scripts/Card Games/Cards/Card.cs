using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Enums
public enum eSuit
{
    Diamonds,
    Spades,
    Hearts,
    Clubs
}
#endregion

public class Card
{
    public eSuit suit;
    /// <summary>
    /// 2-10 are number cards.
    /// 11 is jack
    /// 12 is queen
    /// 13 is king
    /// 1  is ace
    /// </summary>
    public int value;
    public Sprite sprite;
}
