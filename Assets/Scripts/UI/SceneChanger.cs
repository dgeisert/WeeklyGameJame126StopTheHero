using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    Preloader,
    MainMenu,
    Game,
    Level1,
    Level2,
    Level3
}

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance;
    public static void LoadScene(Scenes scene)
    {
        Instance.loadScene((int) scene, true);
    }

    public static void LoadNextLevel()
    {
        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
        {
            PlayerPrefs.SetInt("UnlockedLevel", Mathf.Max(PlayerPrefs.GetInt("UnlockedLevel"), SceneManager.GetActiveScene().buildIndex - 1));
            Instance.loadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Game.Instance.GameOver(true);
        }
    }

    public static void LoadLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level") + 2);
    }

    public UnityEngine.UI.Image blackout;
    public void Start()
    {
        Instance = this;
        LoadScene(Scenes.MainMenu);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        StartCoroutine(DoLoadIn());
    }
    bool isLoading = false;

    public void loadScene(int scene, bool destroy = false)
    {
        if (!isLoading)
        {
            isLoading = true;
            StartCoroutine(DoLoadScene(scene, destroy));
        }
    }
    IEnumerator DoLoadIn()
    {
        while (blackout.color.a > 0)
        {
            Color c = blackout.color;
            c.a -= 0.02f;
            blackout.color = c;
            yield return null;
        }
        blackout.gameObject.SetActive(false);
        isLoading = false;
    }
    IEnumerator DoLoadScene(int scene, bool destroy)
    {
        blackout.gameObject.SetActive(true);
        float startTime = Time.time;
        while (blackout.color.a < 1)
        {
            Color c = blackout.color;
            c.a += 0.02f;
            blackout.color = c;
            yield return null;
        }
        if (destroy)
        {
            if (Game.Instance != null)
            {
                Destroy(Game.Instance.pauseMenu.gameObject);
                Destroy(Game.Instance.scoreScreen.gameObject);
                Destroy(Game.Instance.inGameUI.gameObject);
                Destroy(Game.Instance.gameObject);
            }
            if (Char.Instance != null)
            {
                Destroy(Char.Instance.gameObject);
            }
        }
        SceneManager.LoadScene(scene);
    }

}