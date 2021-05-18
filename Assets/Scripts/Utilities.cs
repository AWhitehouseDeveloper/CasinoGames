using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Utilities
{
    public static Sprite FiveThousand;
    public static Sprite OneThousand;
    public static Sprite FiveHundred;
    public static Sprite OneHundred;
    public static Sprite Fifty;
    public static Sprite Twenty;
    public static Sprite Ten;
    public static Sprite Five;
    public static Sprite One;
    private static Player player;

    public static void Payout(int bet, int multiplier)
    {
        player.Payout((long)bet * multiplier);
        Debug.Log("Payout of " + (bet * multiplier));
    }

    public static int RollDie()
    {
        return Random.Range(1, 7); 
    }
}
