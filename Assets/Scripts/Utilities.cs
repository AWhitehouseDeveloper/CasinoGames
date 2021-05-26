using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Utilities
{
    private static Player player = new Player() { ChipMoney = 10000, Money = 5000 };

    public static void Payout(int bet, int multiplier)
    {
        //player.Payout((long)bet * multiplier);
        Debug.Log("Payout of " + (bet * multiplier));
    }

    public static void Payout(int bet, float multiplier)
    {
        //player.Payout((long)bet * multiplier);
        Debug.Log("Payout of " + (bet * multiplier));
    }

    public static int RollDie()
    {
        return Random.Range(1, 7); 
    }
}
