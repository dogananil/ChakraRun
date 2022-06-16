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
    [SerializeField] private TextMeshProUGUI m_TapToPlay;
    [SerializeField] private AnimationCurve m_TapToPlayAnimationCurve;
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
    public void TapToPlay()
    {
        StartCoroutine(TapToPlayAnimation());
    }
    private IEnumerator TapToPlayAnimation()
    {
        float timeLapse = 0, totalTime = 4.0f;
        m_TapToPlay.gameObject.SetActive(true);
        while (!GameManager.isGameStart)
        {
            m_TapToPlay.transform.localScale = Vector3.one * m_TapToPlayAnimationCurve.Evaluate(timeLapse / totalTime);
            timeLapse += Time.deltaTime;
            if(timeLapse>=totalTime)
            {
                timeLapse = 0;
            }
            yield return null;
        }
        m_TapToPlay.gameObject.SetActive(false);
        m_TapToPlay.transform.localScale = Vector3.one;
    }
}
