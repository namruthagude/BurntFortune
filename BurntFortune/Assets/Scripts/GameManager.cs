using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event Action OnMoveFastPowerup;

    [SerializeField]
    private GameUI gameUI;

    private int wave = 1;
    private int cookies = 0;
    private int spawnedCookies;
    public enum GameState
    {
        None,
        Playing,
        Pause,
        Won,
        Lost
    }
    public enum PowerupState
    {
        None,
        MoveFast,
        FreezeTime
    }

    public PowerupState powerUp;
    public GameState state;
    public bool IsPowerUpEnabled = false;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (RuntimeDBManager.Instance != null)
        {
            wave = RuntimeDBManager.Instance.Wave;
            gameUI.UpdateWave(wave);
            gameUI.UpdateCookies(cookies);
        }
       SetGameState(GameState.Playing);
       // cookies = RuntimeDBManager.Instance.Cookies;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        if (state == GameState.Lost)
        {
            // make temp cookies as 0
            cookies = 0;
            wave = RuntimeDBManager.Instance.Wave;

            // Save Game Data
            RuntimeDBManager.Instance.SaveCookies(cookies);
            RuntimeDBManager.Instance.SaveWave(wave);
        }

      
        //Showing Game zOver UI
        gameUI.ShowGameOverUI();
    }

    public void UpdateWave()
    {
        wave += 1;
        gameUI.UpdateWave(wave);
    }

    public void UpdateCookies()
    {
        cookies += 1;
        gameUI.UpdateCookies(cookies);
        spawnedCookies -= 1;
        if(spawnedCookies <= 0)
        {
            UpdateWave();
            //Show Wave UI
            if (AudioManager.Singleton != null)
            {
                AudioManager.Singleton.PlayOneShotSFX(AudioManager.Singleton.sound_panelpop);
            }
            gameUI.ShowWaveEndUI();
        }
    }


    public void SetGameState(GameState gameState)
    {
        state = gameState;
    }
    public int GetWave()
    {
        return wave;
    }

    public int GetCookies()
    {
        return cookies;
    }

    public void SetSpawnedCookies(int value)
    {
        spawnedCookies = value;
    }

    public void OnRisking()
    {
        cookies *= 2;
        gameUI.UpdateCookies(cookies);
        gameUI.ShowHUDUI();
        CookieSpawner.Instance.SpawnCookies(wave);
        //SceneManager.LoadScene("SampleScene");
        
    }

    public void OnBanking()
    {

        RuntimeDBManager.Instance.SaveCookies(cookies);
        RuntimeDBManager.Instance.SaveWave(wave);
        SetGameState(GameState.Won);
        if (AudioManager.Singleton != null)
        {
            AudioManager.Singleton.PlayOneShotSFX(AudioManager.Singleton.music_gameWon);
        }
        GameOver();
    }

    public void AssignPowerup(PowerupState state)
    {
        powerUp = state;
        if (powerUp == PowerupState.MoveFast)
        {
            OnMoveFastPowerup?.Invoke();
            StartCoroutine(ResetPowerUp());
        }
        else
        {
            StartCoroutine(ResetPowerUp());
        }
    }
    
    private IEnumerator ResetPowerUp()
    {
        yield return new WaitForSeconds(5);
        powerUp = PowerupState.None;
        IsPowerUpEnabled = true;
    }
}
