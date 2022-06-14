using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public GameObject fx_WinConfetti;

    public GameObject StartPanel;
    public GameObject gameScreenPanel;
    public GameObject winScreenPanel;
    public GameObject loseScreenPanel;

    public Image imgGold;

    public List<GameObject> chakraImages;
    public Image chakraImage;
    public Image chakraFillBar;
    public TextMeshProUGUI tapText;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI winGoldText;
    public TextMeshProUGUI loseGoldText;

    public List<TextMeshProUGUI> texts;
    public TextMeshProUGUI txt_Plus1;
    public TextMeshProUGUI txt_Minus1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

}
