using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{

    
    [SerializeField]
    private TMP_Text text_cookiesCollected;
    [SerializeField]
    private TMP_Text text_highScore;

    private GameManager gameManager;
    private RuntimeDBManager runtimeDBManager;
    // Start is called before the first frame update
    void Start()
    {

        gameManager = GameManager.Instance;
        runtimeDBManager = RuntimeDBManager.Instance;

        UpdateGameOverUI();
        
    }

    private void OnEnable()
    {
        if (gameManager != null)
        {
            UpdateGameOverUI();
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateGameOverUI()
    {
        text_cookiesCollected.text = gameManager.GetCookies().ToString();
        text_highScore.text = runtimeDBManager.HighestScore.ToString();
    }
    public void OnRestart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
