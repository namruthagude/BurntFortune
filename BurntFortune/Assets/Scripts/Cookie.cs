using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cookie : MonoBehaviour
{
    [SerializeField]
    float totalTime = 120;
    [SerializeField]
    private Image sliderImage;
    private float time;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        time = totalTime;
        gameManager = GameManager.Instance;

    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.state != GameManager.GameState.Playing)
        {
            return;
        }

        if(gameManager.powerUp == GameManager.PowerupState.FreezeTime)
        {
            return ;
        }
        time -= Time.deltaTime;
        float value = time / totalTime;
        if(value < 0.3)
        {
            sliderImage.color = Color.red;
        }
        sliderImage.fillAmount = value;
        if(time < 0)
        {
            if(gameManager.state == GameManager.GameState.Lost || gameManager.state == GameManager.GameState.Won)
            {
                return;
            }
            Debug.Log("Game Over ");
            gameManager.SetGameState(GameManager.GameState.Lost) ;
            if (AudioManager.Singleton != null)
            {
                AudioManager.Singleton.PlayOneShotSFX(AudioManager.Singleton.music_gameLost);
            }
            gameManager.GameOver();

            Destroy(this.gameObject);
        }
    }

    public void SetTime(float time)
    {
        totalTime = time;
    }
}
