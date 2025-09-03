using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject go_gameOverUI;
    [SerializeField]
    private GameObject go_HUDUI;
    [SerializeField]
    private GameObject go_WaveEndUI;

    private HUDUI hudUI;
    // Start is called before the first frame update
    void Start()
    {
        ShowHUDUI();
        hudUI = go_HUDUI.GetComponent<HUDUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TurnOffAllPanels()
    {
        go_gameOverUI.SetActive(false);
        go_HUDUI.SetActive(false);
        go_WaveEndUI.SetActive(false);
    }

    public void ShowGameOverUI()
    {
        TurnOffAllPanels();
        go_gameOverUI.SetActive(true);
    }

    public void ShowHUDUI()
    {
        TurnOffAllPanels();
        go_HUDUI.SetActive(true);
    }

    public void ShowWaveEndUI()
    {
        TurnOffAllPanels();
        go_WaveEndUI.SetActive(true) ;
        if (AudioManager.Singleton != null)
        {
            AudioManager.Singleton.PlayOneShotSFX(AudioManager.Singleton.sound_wavelCompleted);
        }
    }

    public void UpdateWave(int wave)
    {
        hudUI.UpdateWave(wave);
    }

    public void UpdateCookies(int cookies)
    {
        hudUI.UpdateCookies(cookies);
    }
}
