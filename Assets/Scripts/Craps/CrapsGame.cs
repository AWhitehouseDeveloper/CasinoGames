using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrapsGame : MonoBehaviour
{
    #region Properties
    public Betting betting;
    public GameObject PointIndicator;
    public Sprite IndicatorOn;
    public Sprite IndicatorOff;
    private bool FirstRoll = true;
    private bool EndofRound = false;
    private int point = 0;
    private bool PassWin = false;
    private int PassBet = 0;
    private int DontPassBet = 0;
    private int FieldAmount = 0;
    private Dictionary<int, int> HardwayBets = new Dictionary<int, int>();
    private Dictionary<int, int> PointBets = new Dictionary<int, int>();
    private Dictionary<int, int> OneRollBets = new Dictionary<int, int>();
    #endregion

    #region MainGameFunction(OnRoll, FlipIndicator, and EndRound)
    public void OnRoll()
    {
        int die1 = Utilities.RollDie();
        int die2 = Utilities.RollDie();
        int roll = die1 + die2;
        if (FirstRoll)
        {
            if (roll == 2 || roll == 3 || roll == 12)
            {
                PassWin = false;
                EndofRound = true;
                Debug.Log("Craps");
            }
            else if (roll == 7 || roll == 11)
            {
                PassWin = true;
                EndofRound = true;
                Debug.Log("Natural");
            }
            else
            {
                FirstRoll = false;
                point = roll;
                FlipIndicator(point);
            }
        }
        else
        {
            if (roll == point)
            {
                PassWin = true;
                EndofRound = true;
            }
            else if (roll == 7)
            {
                PassWin = false;
                EndofRound = true;
            }
        }
        Hardways(die1, die2);
        if(FieldAmount > 0) FieldBets(roll);
        Points(roll);
        OneRolls(roll);
        if(roll == 7)
        {
            HardwayBets.Clear();
            OneRollBets.Clear();
            PointBets.Clear();
        }
        SetButtonSprites();
        Debug.Log(roll);
        if (EndofRound)
        {
            EndRound();
        }
    }

    private void FlipIndicator(int num)
    {
        PointIndicator.transform.position = Vector3.zero;
        if (num == -1)
        {
            PointIndicator.transform.position = new Vector2(255, 640);
            PointIndicator.GetComponent<Image>().sprite = IndicatorOff;
        }
        else
        {
            PointIndicator.GetComponent<Image>().sprite = IndicatorOn;
            if (num == 4)
                PointIndicator.transform.position = new Vector2(305, 525);
            else if (num == 5)
                PointIndicator.transform.position = new Vector2(395, 525);
            else if (num == 6)
                PointIndicator.transform.position = new Vector2(495, 525);
            else if (num == 8)
                PointIndicator.transform.position = new Vector2(590, 525);
            else if (num == 9)
                PointIndicator.transform.position = new Vector2(685, 525);
            else if (num == 10)
                PointIndicator.transform.position = new Vector2(780, 525);
        }
    }

    private void EndRound()
    {
        PassBetOut();
        FirstRoll = true;
        EndofRound = false;
        FlipIndicator(-1);
        point = 0;
    }
    #endregion

    #region Pass/DontPassBet
    private void PassBetOut()
    {
        if(PassWin)
        {
            Utilities.Payout(PassBet, 2);
        }
        else
        {
            Utilities.Payout(DontPassBet, 2);
        }
        PassBet = 0;
        DontPassBet = 0;
    }
    public void PlacePassBet()
    {
        PassBet += betting.GetSelectedChip();
        SetButtonSprites();
    }
    public void PlaceDontPassBet()
    {
        DontPassBet += betting.GetSelectedChip();
        SetButtonSprites();
    }
    #endregion

    #region PointBets
    private void Points(int roll)
    {
        float multiplier = 1;
        if (PointBets.ContainsKey(roll))
        {
            if (roll == 4 || roll == 10)
            {
                multiplier = 2;
            }
            else if(roll == 5 || roll == 9)
            {
                multiplier = (3 / 2);
            }
            else if(roll == 6 || roll == 8)
            {
                multiplier = (6 / 5);
            }
            int amount = PointBets[roll];
            Utilities.Payout(amount, multiplier);
            PointBets.Remove(roll);
        }
    }

    public void OnClickPointBet(int placement)
    {
        PlacePointBet(placement, betting.GetSelectedChip());
    }

    public void PlacePointBet(int placement, int amount)
    {
        if (PointBets.ContainsKey(placement)) PointBets[placement] += amount;
        else PointBets.Add(placement, amount);
        Debug.Log("Point Bet of " + amount + " Placed on " + placement);
        SetButtonSprites();
    }
    #endregion

    #region FieldBets
    private void FieldBets(int roll)
    {
        int multiplier = 1;
        if (roll == 2) multiplier = 2;
        else if (roll == 12) multiplier = 3;
        Utilities.Payout(FieldAmount, multiplier);
        if (roll == 6 || roll == 7 || roll == 8) FieldAmount = 0;
    }

    public void PlaceFieldBet()
    {
        FieldAmount += betting.GetSelectedChip();
        SetButtonSprites();
    }
    #endregion

    #region Hardways
    private void Hardways(int roll1, int roll2)
    {
        if (HardwayBets.ContainsKey(-1))
        {
            int multiplier = 7;
            int amount = HardwayBets[-1];
            Utilities.Payout(amount, multiplier);
        }
        int roll = roll1 + roll2;
        if (HardwayBets.ContainsKey(roll))
        {
            int multiplier = 1;
            if (roll == 4 || roll == 10 && roll1 == roll2)
            {
                multiplier = 7;
            }
            else if (roll == 6 || roll == 8 && roll1 == roll2)
            {
                multiplier = 9;
            }
            int amount = HardwayBets[roll];
            Utilities.Payout(amount, multiplier);
            HardwayBets.Remove(roll);
        }
        if (roll == 7) HardwayBets.Clear();
    }

    public void OnClickHardwayBet(int placement)
    {
        PlaceHardwayBet(placement, betting.GetSelectedChip());
    }

    public void PlaceHardwayBet(int placement, int amount)
    {
        if (HardwayBets.ContainsKey(placement)) HardwayBets[placement] += amount;
        else HardwayBets.Add(placement, amount);
        Debug.Log("Hardway Bet of " + amount + " Placed on " + placement);
        SetButtonSprites();
    }
    #endregion

    #region OneRolls
    private void OneRolls(int roll)
    {
        Dictionary<int, int> temp = new Dictionary<int, int>();
        int multiplier = 1;
        if (roll == 7) multiplier = 4;
        else if (roll == 2 || roll == 12) multiplier = 30;
        else if (roll == 3 || roll == 11) multiplier = 15;

        for(int i = 0; i < OneRollBets.Count; i++)
        {
            if (OneRollBets.ContainsKey(roll))
            {
                int amount = OneRollBets[roll];
                Utilities.Payout(amount, multiplier);
                temp.Add(roll, amount);
            }
        }
        OneRollBets.Clear();
        for(int i = 0; i < temp.Count; i++)
        {
            OneRollBets = temp;
        }
    }

    public void OnClickOneRollBet(int placement)
    {
        PlaceOneRollBet(placement, betting.GetSelectedChip());
    }

    public void PlaceOneRollBet(int placement, int amount)
    {
        if (OneRollBets.ContainsKey(placement)) OneRollBets[placement] += amount;
        else OneRollBets.Add(placement, amount);
        Debug.Log("OneRoll Bet of " + amount + " Placed on " + placement);

        SetButtonSprites();
    }
    #endregion

    public void SetButtonSprites()
    {
        foreach (int H in HardwayBets.Keys)
        {
            switch (H)
            {
                case 4:
                    betting.ChangeButtonImage(betting.GetButton("Double2"), betting.GetChipNumFromValue(HardwayBets[H]));
                    break;
                case 6:
                    betting.ChangeButtonImage(betting.GetButton("Double3"), betting.GetChipNumFromValue(HardwayBets[H]));
                    break;
                case 8:
                    betting.ChangeButtonImage(betting.GetButton("Double4"), betting.GetChipNumFromValue(HardwayBets[H]));
                    break;
                case 10:
                    betting.ChangeButtonImage(betting.GetButton("Double5"), betting.GetChipNumFromValue(HardwayBets[H]));
                    break;
                default:
                    break;
            }
        }
        foreach (int O in OneRollBets.Keys)
        {
            switch (O)
            {
                case -1:
                    betting.ChangeButtonImage(betting.GetButton("Any"), betting.GetChipNumFromValue(OneRollBets[O]));
                    break;
                case 2:
                    betting.ChangeButtonImage(betting.GetButton("Double1"), betting.GetChipNumFromValue(OneRollBets[O]));
                    break;
                case 3:
                    betting.ChangeButtonImage(betting.GetButton("1&2"), betting.GetChipNumFromValue(OneRollBets[O]));
                    break;
                case 7:
                    betting.ChangeButtonImage(betting.GetButton("7"), betting.GetChipNumFromValue(OneRollBets[O]));
                    break;
                case 11:
                    betting.ChangeButtonImage(betting.GetButton("5&6"), betting.GetChipNumFromValue(OneRollBets[O]));
                    break;
                case 12:
                    betting.ChangeButtonImage(betting.GetButton("Double6"), betting.GetChipNumFromValue(OneRollBets[O]));
                    break;
                default:
                    break;
            }
        }
        foreach (int P in PointBets.Keys)
        {
            switch (P)
            {
                case 4:
                    betting.ChangeButtonImage(betting.GetButton("4"), betting.GetChipNumFromValue(PointBets[P]));
                    break;
                case 5:
                    betting.ChangeButtonImage(betting.GetButton("5"), betting.GetChipNumFromValue(PointBets[P]));
                    break;
                case 6:
                    betting.ChangeButtonImage(betting.GetButton("6"), betting.GetChipNumFromValue(PointBets[P]));
                    break;
                case 8:
                    betting.ChangeButtonImage(betting.GetButton("8"), betting.GetChipNumFromValue(PointBets[P]));
                    break;
                case 9:
                    betting.ChangeButtonImage(betting.GetButton("9"), betting.GetChipNumFromValue(PointBets[P]));
                    break;
                case 10:
                    betting.ChangeButtonImage(betting.GetButton("10"), betting.GetChipNumFromValue(PointBets[P]));
                    break;
                default:
                    break;
            }
        }
        betting.ChangeButtonImage(betting.GetButton("Field"), betting.GetChipNumFromValue(FieldAmount));
        betting.ChangeButtonImage(betting.GetButton("PassLine"), betting.GetChipNumFromValue(PassBet));
        betting.ChangeButtonImage(betting.GetButton("Don'tPass"), betting.GetChipNumFromValue(DontPassBet));
    }

}
