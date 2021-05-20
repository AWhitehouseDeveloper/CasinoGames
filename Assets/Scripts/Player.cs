using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public long ChipMoney;
    public long Money = 5000;

    public void Payout(long pay)
    {
        ChipMoney += pay;
    }

    public void OnWithdraw()
    {
        ChipMoney += 5000;
    }

}
