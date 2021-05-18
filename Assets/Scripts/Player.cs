using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Money = "Infinite";
    public long ChipMoney;

    public void Payout(long pay)
    {
        ChipMoney += pay;
    }

    public void OnWithdraw()
    {
        ChipMoney += 5000;
    }

}
