using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    public static InGameUI Instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI speachText;
    public GameObject speachArea;
    public bool speach2Said;
    string[] speach1 = new string[]
    {
        "The hero is trying to take the boss out, and I'm going to stop him.",
        "I'll have to make it to the top floor.  I hope none of the boss's minions get in the way."
    };
    string[] speach2 = new string[]
    {
        "It seems the others haven't gotten the memo, I'll need to take out anyone in my way."
    };

    public void UpdateScore(float val)
    {
        scoreText.text = "Score: " + val.ToString("#,#");
    }
    void Start()
    {
        Instance = this;
        speachArea.SetActive(false);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        switch (scene.buildIndex)
        {
            case 3:
                StartCoroutine(TypeText(speach1));
                break;
            default:
                break;
        }
    }
    public void Speach2()
    {
        if (!speach2Said)
        {
            StartCoroutine(TypeText(speach2));
            speach2Said = true;
        }
    }
    public IEnumerator TypeText(string[] text, int str = 0)
    {
        speachText.text = "";
        speachArea.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        char[] chars = text[str].ToCharArray();
        for (int i = 0; i < text[str].Length; i++)
        {
            speachText.text += chars[i];
            yield return null;
        }
        yield return new WaitForSeconds(3);
        speachText.text = "";
        speachArea.SetActive(false);
        if (text.Length > str + 1)
        {
            StartCoroutine(TypeText(text, str + 1));
        }
    }
    public void EndGame(bool victory)
    {
        gameObject.SetActive(false);
    }
}