using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    public PauseMenu pauseMenu;
    public ScoreScreen scoreScreen;
    public InGameUI inGameUI;
    public List<Enemy> enemies;
    public bool active = true;
    public static float Score
    {
        get
        {
            if (Instance)
            {
                return Instance.score;
            }
            return -1f;
        }
        set
        {
            if (Instance)
            {
                Instance.score = value;
                Instance.inGameUI.UpdateScore(value);
            }
        }
    }
    public float score;
    // Start is called before the first frame update

    void Awake()
    {
        Instance = this;
        enemies = new List<Enemy>();
    }
    void Start()
    {
        pauseMenu.gameObject.SetActive(false);
        scoreScreen.gameObject.SetActive(false);
        inGameUI.gameObject.SetActive(true);
        DontDestroyOnLoad(pauseMenu.gameObject);
        DontDestroyOnLoad(scoreScreen.gameObject);
        DontDestroyOnLoad(inGameUI.gameObject);
        Time.timeScale = 1;
        Score = 0;
        SceneChanger.LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (Controls.Pause)
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
    }

    public void GameOver(bool victory = false)
    {
        inGameUI.EndGame(victory);
        scoreScreen.EndGame(victory);
        pauseMenu.gameObject.SetActive(false);
    }
}