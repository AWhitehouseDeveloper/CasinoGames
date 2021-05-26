using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Betting : MonoBehaviour
{
    public List<Image> chipSprites;
    public List<Image> arrowSprites;

    [Range(1, 9)] private static int selectedChip = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (selectedChip != 0) selectedChip--;
            else selectedChip = 8;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (selectedChip != 8) selectedChip++;
            else selectedChip = 0;
        }
        PositionArrow();
    }

    public void ChangeButtonImage(Button b)
    {
        b.GetComponent<Image>().sprite = chipSprites[selectedChip].sprite;
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

    public Image GetSelectedChipSprite()
    {
        return chipSprites[selectedChip];   
    }

    private void PositionArrow()
    {
        for (int i = 0; i < arrowSprites.Count; i++)
        {
            if (i == selectedChip) arrowSprites[i].gameObject.SetActive(true);
            else arrowSprites[i].gameObject.SetActive(false);
        }
    }
}
