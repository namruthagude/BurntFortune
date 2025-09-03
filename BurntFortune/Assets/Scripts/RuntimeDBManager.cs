using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeDBManager : MonoBehaviour
{
    public static RuntimeDBManager Instance;
    public int Wave;
    public int Cookies;
    public int HighestScore;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        RetrivingData();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RetrivingData()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsStrings.WAVE))
        {
            Wave = PlayerPrefs.GetInt(PlayerPrefsStrings.WAVE);
        }
        else
        {
            Wave = 1;
            PlayerPrefs.SetInt(PlayerPrefsStrings.WAVE, Wave);  
        }

        if (PlayerPrefs.HasKey(PlayerPrefsStrings.COOKIES))
        {
            Cookies = PlayerPrefs.GetInt(PlayerPrefsStrings.COOKIES);
        }
        else
        {
            Cookies = 0;
            PlayerPrefs.SetInt(PlayerPrefsStrings.COOKIES, Cookies);
        }
        if (PlayerPrefs.HasKey(PlayerPrefsStrings.HIGHESTSCORE))
        {
            HighestScore = PlayerPrefs.GetInt(PlayerPrefsStrings.HIGHESTSCORE);
        }
        else
        {
            HighestScore = 0;
            PlayerPrefs.SetInt(PlayerPrefsStrings.HIGHESTSCORE, HighestScore);
        }
    }

    public void SaveCookies(int value)
    {
        Cookies += value;
        PlayerPrefs.SetInt(PlayerPrefsStrings.COOKIES, Cookies);
        if(value > HighestScore)
        {
            UpdateHighestScore(value);
        }
    }

    public void SaveWave(int value)
    {
        Wave = value;
        PlayerPrefs.SetInt(PlayerPrefsStrings.WAVE, Wave);
    }

    private void UpdateHighestScore(int value)
    {
        HighestScore = value;
        PlayerPrefs.SetInt(PlayerPrefsStrings.HIGHESTSCORE, HighestScore);
    }
}
