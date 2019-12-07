using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Update()
    {
        if (Controls.Pause)
        {
            Game.Instance.Pause();
        }
    }
    public void OnEnable()
    {
        Time.timeScale = 0;
        Game.Instance.active = false;
    }
    public void OnDisable()
    {
        Time.timeScale = 1;
        Game.Instance.active = true;
    }
    public void Resume()
    {
        Game.Instance.Pause();
    }
    public void Restart()
    {
        SceneChanger.LoadScene(Scenes.Game);
    }
    public void Menu()
    {
        SceneChanger.LoadScene(Scenes.MainMenu);
    }
}