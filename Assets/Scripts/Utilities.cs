using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    private static Player player;

    public static void Payout(int bet, int multiplier)
    {
        player.Payout((long)bet * multiplier);
    }

    public static int RollDie()
    {
        return Random.Range(1, 6); 
    }
}
