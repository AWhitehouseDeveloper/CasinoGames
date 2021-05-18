using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrapsGame : MonoBehaviour
{
    /// <summary>
    /// Each round has two phases: "come-out" and "point". Dice are passed to the left. 
    /// To start a round, the shooter makes one or more "come-out" rolls. 
    /// A come-out roll of 2, 3 or 12 is called "craps" or "crapping out", and anyone betting the Pass line loses. 
    /// On the other hand, anyone betting the Don't Pass line on come out wins with a roll of 2 or 3 and ties (pushes) if a 12 is rolled. 
    /// Shooters may keep rolling after crapping out; the dice are only required to be passed if a shooter sevens out (rolls a seven after a point has been established). 
    /// A come-out roll of 7 or 11 is a "natural"; the Pass line wins and Don't Pass loses. 
    /// The other possible numbers are the point numbers: 4, 5, 6, 8, 9, and 10. 
    /// If the shooter rolls one of these numbers on the come-out roll, this establishes the "point" – to "pass" or "win", 
    /// the point number must be rolled again before a seven.

    /// The dealer flips a button to the "On" side and moves it to the point number signifying the second phase of the round.
    /// If the shooter "hits" the point value again before rolling a seven, the Pass line wins and a new round starts. 
    /// If the shooter rolls any seven before repeating the point number (a "seven-out"), 
    /// the Pass line loses, the Don't Pass line wins, and the dice pass clockwise to the next new shooter for the next round.

    /// Once a point has been established any multi-roll bet(including Pass and/or Don't Pass line bets and odds) 
    /// are unaffected by the 2, 3, 11 or 12; 
    /// the only numbers which affect the round are the established point, any specific bet on a number, or any 7. 
    /// Any single roll bet is always affected (win or lose) by the outcome of any roll.
    /// 
    /// </summary>
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
    private Dictionary<int, int> OneRollBets = new Dictionary<int, int>();

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
        if (die1 == die2) Hardways(roll);
        if(FieldAmount > 0) FieldBets(roll);
        OneRolls(roll);
        Debug.Log(roll);
        if (EndofRound)
        {
            EndRound();
        }
    }

    public void FlipIndicator(int num)
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

    public void EndRound()
    {
        PassBetOut();
        FirstRoll = true;
        EndofRound = false;
        FlipIndicator(-1);
        point = 0;
    }
    #endregion

    #region Pass/DontPassBet
    public void PassBetOut()
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
    public void PlacePassBet(int amount)
    {
        PassBet += amount;
    }
    public void PlaceDontPassBet(int amount)
    {
        DontPassBet += amount;
    }
    #endregion

    #region FieldBets
    public void FieldBets(int roll)
    {
        int multiplier = 1;
        if (roll == 2) multiplier = 2;
        else if (roll == 12) multiplier = 3;
        Utilities.Payout(FieldAmount, multiplier);
        if (roll == 6 || roll == 7 || roll == 8) FieldAmount = 0;
    }

    public void PlaceFieldBet(int amount)
    {
        FieldAmount += amount;
    }
    #endregion

    #region Hardways
    public void Hardways(int roll)
    {
        int multiplier = 1;
        if(HardwayBets.ContainsKey(-1))
        {
            multiplier = 7;
            int amount = HardwayBets[-1];
            Utilities.Payout(amount, multiplier);
        }
        multiplier = 1;
        if (HardwayBets.ContainsKey(roll))
        {
            if (roll == 4 || roll == 10) multiplier = 7;
            else if (roll == 6 || roll == 8) multiplier = 9;
            int amount = HardwayBets[roll];
            Utilities.Payout(amount, multiplier);
            HardwayBets.Clear();
            PlaceHardwayBet(roll, amount);
        }
        else HardwayBets.Clear();
    }

    public void OnClickHardwayBet(int placement)
    {
        PlaceHardwayBet(placement, 100);
        Debug.Log("Hardway Bet Placed on " + placement);
    }

    public void PlaceHardwayBet(int placement, int amount)
    {
        HardwayBets.Add(placement, amount);
    }
    #endregion

    #region OneRolls
    public void OneRolls(int roll)
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
        PlaceOneRollBet(placement, 1000);
        Debug.Log("OneRoll Bet Placed on " + placement);
    }

    public void PlaceOneRollBet(int placement, int amount)
    {
        OneRollBets.Add(placement, amount);
    }
    #endregion
}
