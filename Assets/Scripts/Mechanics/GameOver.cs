using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    void Update()
    {
         if (Input.anyKey)
        {
            SceneManager.LoadScene("LevelSelect", LoadSceneMode.Single);
        }
    }
}
