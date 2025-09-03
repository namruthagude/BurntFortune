using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveEndUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text_cookiesCollected;
    private GameManager gameManager;

    [Header("Tutorial Instructions")]
    [SerializeField]
    private GameObject go_FirstWaveCompleted;

    private void Start()
    {
        gameManager = GameManager.Instance;
        text_cookiesCollected.text = gameManager.GetCookies().ToString();

        CheckingFirstWave();

    }

    private void CheckingFirstWave()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsStrings.FIRSTWAVECOMPLETED))
        {
            StartCoroutine(FirstWaveCompletedCoroutine());
        }
        
    }

    private IEnumerator FirstWaveCompletedCoroutine()
    {
        go_FirstWaveCompleted.SetActive(true);
        PlayerPrefs.SetInt(PlayerPrefsStrings.FIRSTWAVECOMPLETED, 1);
        yield return new WaitForSeconds(3);
        go_FirstWaveCompleted.SetActive(false);
    }

    public void OnEnable()
    {
        if (gameManager != null)
        {
            text_cookiesCollected.text = gameManager.GetCookies().ToString();
        }
        
    }
    public void OnRiskIt()
    {
       gameManager.OnRisking();
    }

    public void OnBankIt()
    {
        gameManager.OnBanking();
    }
}
