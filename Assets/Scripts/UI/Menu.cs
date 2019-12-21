using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject settings;
    public GameObject controls;
    public GameObject menu;
    public GameObject levelSelect;
    public List<Button> levelSelectButtons = new List<Button>();
    public UnityEngine.Audio.AudioMixer mixer;
    public Image NoMusic;
    public Image NoSound;
    void Start()
    {
        Time.timeScale = 1;
        OpenMenu(menu);
        for (int i = 0; i < levelSelectButtons.Count; i++)
        {
            if (i + 4 > UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings ||
                i > PlayerPrefs.GetInt("UnlockedLevel"))
            {
                levelSelectButtons[i].interactable = false;
            }
        }
    }
    public void PlayGame(int level)
    {
        PlayerPrefs.SetInt("Level", level);
        SceneChanger.LoadScene(Scenes.Game);
    }
    public void OpenMenu(GameObject panel)
    {

        menu.SetActive(false);
        controls.SetActive(false);
        settings.SetActive(false);
        levelSelect.SetActive(false);
        panel.SetActive(true);
    }
    public void MuteMusic()
    {
        float volume;
        mixer.GetFloat("MusicVolume", out volume);
        if (volume < -70)
        {
            NoMusic.gameObject.SetActive(false);
            mixer.SetFloat("MusicVolume", 0);
        }
        else
        {
            NoMusic.gameObject.SetActive(true);
            mixer.SetFloat("MusicVolume", -80);
        }
    }
    public void MuteSound()
    {
        float volume;
        mixer.GetFloat("MasterVolume", out volume);
        if (volume < -70)
        {
            NoSound.gameObject.SetActive(false);
            mixer.SetFloat("MasterVolume", 0);
        }
        else
        {
            NoSound.gameObject.SetActive(true);
            mixer.SetFloat("MasterVolume", -80);
        }
    }
}