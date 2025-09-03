using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDUI : MonoBehaviour
{

    [SerializeField]
    private Sprite image_pause;
    [SerializeField] 
    private Sprite image_resume;
    [SerializeField]
    private TMP_Text text_Waves;
    [SerializeField]
    private TMP_Text text_Cookies;
    [Header("Powerups")]
    [SerializeField]
    private Button button_MoveFast;
    [SerializeField]
    private Button button_FreezeTime;
    [SerializeField]
    private TMP_Text text_MoveFastNumber;
    [SerializeField]
    private TMP_Text text_FreezeTimeNumber;
    [SerializeField]
    private TMP_Text text_warning;
    [SerializeField]
    private GameObject go_textWarning;

    [Header("Tutorial Instructions")]
    [SerializeField]
    private GameObject go_FirstWave;
    [SerializeField]
    private GameObject go_FirstMoveFast;
    [SerializeField]
    private GameObject go_FirstFreezeTime;


    
    private bool _isPowerUpEnabled = false;
    private GameManager gameManager;

    private string twoPowerUpActivation = "You can't enable two powerups at once";
    private string noPowerUp = "Wait untill next wave to use powerup";
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        UpdatePowerups();
        CheckingFirstWave();
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckingFirstWave()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsStrings.FIRSTWAVE))
        {
            StartCoroutine(FistWaveCoroutine());
        }
        
    }

    private IEnumerator FistWaveCoroutine()
    {
        go_FirstWave.SetActive(true);
        PlayerPrefs.SetInt(PlayerPrefsStrings.FIRSTWAVE, 1);
        yield return new WaitForSeconds(3);
        go_FirstWave.SetActive(false);
    }


    public void UpdateWave(int value)
    {
        text_Waves.text = value.ToString();
        UpdatePowerups();
    }

    public void UpdateCookies(int value)
    {
        text_Cookies.text = value.ToString();
    }

    public void OnPause(Image image)
    {
        if(image.sprite == image_pause)
        {
            image.sprite = image_resume;
            gameManager.SetGameState(GameManager.GameState.Pause);
        }
        else
        {
            image.sprite = image_pause;
            Time.timeScale = 1;
            gameManager.SetGameState(GameManager.GameState.Playing);
        }
    }

    public void OnMoveFastClick()
    {
        if (gameManager.IsPowerUpEnabled)
        {
            //Show warning text
            go_textWarning.SetActive(true);
            text_warning.text = twoPowerUpActivation;
            StartCoroutine(HideWarningTextCoroutine());
            return;
        }

        gameManager.IsPowerUpEnabled = true;
        CheckingFirstMoveFast();
        int num = int.Parse(text_MoveFastNumber.text);
        if(num > 0)
        {
            text_MoveFastNumber.text = (num -1).ToString();
        }
        else
        {
            go_textWarning.SetActive(true);
            text_warning.text = noPowerUp;
            StartCoroutine(HideWarningTextCoroutine());
        }
        gameManager.AssignPowerup(GameManager.PowerupState.MoveFast);
    }

    private void CheckingFirstMoveFast()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsStrings.FIRSTMOVEFAST))
        {
            StartCoroutine(FirstMoveFastCoroutine());
        }
        
    }

    private IEnumerator FirstMoveFastCoroutine()
    {
        go_FirstMoveFast.SetActive(true);
        PlayerPrefs.SetInt(PlayerPrefsStrings.FIRSTMOVEFAST, 1);
        yield return new WaitForSeconds(3);
        go_FirstMoveFast.SetActive(false);
    }

    public void OnFreezeTimeClick()
    {
        if (gameManager.IsPowerUpEnabled)
        {
            //Show warning text
            go_textWarning.SetActive(true);
            text_warning.text = twoPowerUpActivation;
            StartCoroutine(HideWarningTextCoroutine());
            return;
        }
        gameManager.IsPowerUpEnabled = true;
        CheckingFirstFreezeTime();
        int num = int.Parse(text_FreezeTimeNumber.text);
        if (num > 0)
        {
            text_FreezeTimeNumber.text = (num - 1).ToString();
        }
        else
        {
            go_textWarning.SetActive(true);
            text_warning.text = noPowerUp;
            StartCoroutine(HideWarningTextCoroutine());
        }
        gameManager.AssignPowerup(GameManager.PowerupState.FreezeTime);
    }

    private void CheckingFirstFreezeTime()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsStrings.FIRSTFREEZETIME))
        {
            StartCoroutine(FirstFreezeTimeCoroutine());
        }
       
    }

    private IEnumerator FirstFreezeTimeCoroutine()
    {
        go_FirstFreezeTime.SetActive(true);
        PlayerPrefs.SetInt(PlayerPrefsStrings.FIRSTFREEZETIME, 1);
        yield return new WaitForSeconds(5);
        go_FirstFreezeTime.SetActive(false);
    }

    public void UpdatePowerups()
    {
        if(gameManager == null)
        {
            gameManager = GameManager.Instance;
        }
        if(gameManager.GetWave() > 10)
        {
            button_MoveFast.interactable = true;
            text_MoveFastNumber.text = "1".ToString();
        }

        if(gameManager.GetWave() > 15)
        {
            button_FreezeTime.interactable = true;
            text_FreezeTimeNumber.text="1".ToString();
        }
    }

    private IEnumerator HideWarningTextCoroutine()
    {
        yield return new WaitForSeconds(3);
        go_textWarning.SetActive(false);
    }
}
