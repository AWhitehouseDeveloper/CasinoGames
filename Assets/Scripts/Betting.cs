using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Betting : MonoBehaviour
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
    public List<Image> arrowSprites;

    [Range(1, 9)] private static int selectedChip = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (selectedChip != 1) selectedChip--;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (selectedChip != 9) selectedChip++;
        }
        PositionArrow();
    }
    public int GetSelectedChip()
    {
        switch (selectedChip)
        {
            case 1:
                return 1;
            case 2:
                return 5;
            case 3:
                return 10;
            case 4:
                return 20;
            case 5:
                return 50;
            case 6:
                return 100;
            case 7:
                return 500;
            case 8:
                return 1000;
            case 9:
                return 5000;
        }
        return 0;
    }

    private void PositionArrow()
    {
        for (int i = 0; i < arrowSprites.Count; i++)
        {
            if (i == selectedChip) arrowSprites[i].GetComponent<GameObject>().SetActive(true);
            else arrowSprites[i].GetComponent<GameObject>().SetActive(false);
        }
    }
}
