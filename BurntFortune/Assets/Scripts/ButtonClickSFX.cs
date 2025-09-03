using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSFX : MonoBehaviour
{
    private Button button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClick);
    }

   private void ButtonClick()
    {
        if(AudioManager.Singleton != null)
        {
            AudioManager.Singleton.PlayOneShotSFX(AudioManager.Singleton.sound_buttonClick);
        }
    }
}
