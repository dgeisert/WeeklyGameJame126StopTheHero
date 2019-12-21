using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{

    void OnTriggerEnter(Collider collider)
    {
        switch (collider.tag)
        {
            case "Player":
                Time.timeScale = 0;
                Game.Instance.active = false;
                SceneChanger.LoadNextLevel();
                break;
            default:
                break;
        }
    }
}