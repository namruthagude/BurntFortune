using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float PlayerSpeed = 5;
    public float MaxYValue;
    public float MinYValue;
    public float MaxXValue;
    public float MinXValue;
    float mouseX;
    float mouseY;
    Vector3 mousePos;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;

        gameManager.OnMoveFastPowerup += GameManager_OnMoveFastPowerup;

    }

    private void GameManager_OnMoveFastPowerup()
    {
        PlayerSpeed = 7.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.state != GameManager.GameState.Playing)
        {
            return;
        }

        mousePos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

       
        Vector2 direction = (mouseWorldPos - transform.position);

        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

      
        transform.rotation = Quaternion.Euler(0, 0, angle);

      
        if (Input.GetKey(KeyCode.W))
        {
            if (AudioManager.Singleton != null)
            {
                AudioManager.Singleton.PlayGameMusic();
            }

            transform.Translate(Vector2.right * PlayerSpeed * Time.deltaTime);



        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            if (AudioManager.Singleton != null)
            {
                AudioManager.Singleton.StopGameMusic() ;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Cookie")
        {
            //Collided with cookie increase point 
            if(AudioManager.Singleton != null)
            {
                AudioManager.Singleton.PlayOneShotSFX(AudioManager.Singleton.sound_cookieGrab);
            }
            gameManager.UpdateCookies();
            //Destroy cookie
            Destroy(collision.gameObject);
        }
    }
}
