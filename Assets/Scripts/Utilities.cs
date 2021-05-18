using System.Collections;
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

    [Range(1, 9)]private static int selectedChip = 1;
    private static Player player;
    public static Image arrow;

    public static void OnRight()
    {
        if(selectedChip != 9) selectedChip++;
        PositionArrow();
    }

    public static void OnLeft()
    {
        if(selectedChip != 1) selectedChip--;
        PositionArrow();
    }

    public static void PositionArrow()
    {
        switch(selectedChip)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
        }
    }

    public static void Payout(int bet, int multiplier)
    {
        //player.Payout((long)bet * multiplier);
        Debug.Log("Payout of " + (bet * multiplier));
    }

    public static int RollDie()
    {
        return Random.Range(1, 7); 
    }
}
