using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Betting : MonoBehaviour
{
    public List<Image> chipSprites;
    public List<Image> arrowSprites;
    public GameObject ButtonsContainer;

    private Button[] buttons;
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

    public void ChangeButtonImage(Button b, int chipNum)
    {
        if (b.name == "Field")
        {
            b.GetComponentInChildren<Image>().sprite = chipSprites[chipNum].sprite;
            b.GetComponentInChildren<Image>().type = Image.Type.Simple;
        }
        else
        {
            b.GetComponent<Image>().sprite = chipSprites[chipNum].sprite;
            b.GetComponent<Image>().type = Image.Type.Simple;
        }
    }

    public int GetSelectedChip()
    {
        switch (selectedChip)
        {
            case 0:
                return 1;
            case 1:
                return 5;
            case 2:
                return 10;
            case 3:
                return 20;
            case 4:
                return 50;
            case 5:
                return 100;
            case 6:
                return 500;
            case 7:
                return 1000;
            case 8:
                return 5000;
        }
        return 0;
    }

    public Image GetSelectedChipSprite()
    {
        return chipSprites[selectedChip];
    }

    public int GetChipNumFromValue(int i)
    {
        if (i >= 5000) return 8;
        else if (i >= 1000) return 7;
        else if (i >= 500) return 6;
        else if (i >= 100) return 5;
        else if (i >= 50) return 4;
        else if (i >= 20) return 3;
        else if (i >= 10) return 2;
        else if (i >= 5) return 1;
        return 0;
    }

    private void PositionArrow()
    {
        for (int i = 0; i < arrowSprites.Count; i++)
        {
            if (i == selectedChip) arrowSprites[i].gameObject.SetActive(true);
            else arrowSprites[i].gameObject.SetActive(false);
        }
    }

    public Button GetButton(string buttonName)
    {
        buttons = ButtonsContainer.GetComponentsInChildren<Button>();
        foreach(Button B in buttons)
        {
            if(B.name == buttonName)
            {
                return B.GetComponent<Button>();
            }
        }

        return null;
    }

}
