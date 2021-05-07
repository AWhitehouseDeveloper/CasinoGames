using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Money = "Infinite";
    public long ChipMoney;

    public void OnWithdraw()
    {
        ChipMoney += 10000;
    }

}
