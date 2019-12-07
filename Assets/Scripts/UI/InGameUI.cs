using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI manaText;

    public void UpdateScore(float val)
    {
        scoreText.text = "Score: " + val.ToString("#,#");
    }
    public void UpdateMana(float val, float max)
    {
        manaText.text = "Mana: " + val.ToString("#,#") + "/" + max.ToString();
    }
    public void EndGame(bool victory)
    {
        gameObject.SetActive(false);
    }
}