using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieSpawner : MonoBehaviour
{
    public static CookieSpawner Instance;
    public GameObject cookiePrefab;
    public int baseCookies = 5;
    public float baseTime = 20f;
    public int maxCookies = 20;
    public float minTime = 5f;
    public int cookieIncreasePerWave = 3;
    public float timeDecreasePerWave = 1f;

    private int currentWaveCookies;
    private float currentWaveTime;
    private GameManager gameManager;
    private BoxCollider2D _collider;

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
        _collider = GetComponent<BoxCollider2D>();
        gameManager = GameManager.Instance;
        int wave = gameManager.GetWave();
        SpawnCookies(wave);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCookies(int wave)
    {
        currentWaveCookies = Mathf.Min(baseCookies + (wave - 1) * cookieIncreasePerWave, maxCookies);
        currentWaveTime = Mathf.Max(baseTime - (wave - 1) * timeDecreasePerWave, minTime);
        gameManager.SetSpawnedCookies(currentWaveCookies);

        for (int i = 0; i < currentWaveCookies; i++)
        {
            GameObject go_cookie = Instantiate(cookiePrefab, new Vector3(Random.Range(_collider.bounds.min.x, _collider.bounds.max.x), Random.Range(_collider.bounds.min.y, _collider.bounds.max.y), 0), Quaternion.identity);
            Cookie cookie = go_cookie.GetComponent<Cookie>();
            cookie.SetTime(currentWaveTime);
        }
    }
}
