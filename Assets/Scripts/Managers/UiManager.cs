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

    public Image powerUpImage;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI winGoldText;
    public TextMeshProUGUI loseGoldText;
    public TextMeshProUGUI unitBuyGoldText;
    public TextMeshProUGUI fullText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

}
