using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTitle : MonoBehaviour
{
    [SerializeField][Tooltip("Copy and paste scene path here. No path = go to title screen")]
    private string scenePath;

    // Start is called before the first frame update
    void Start()
    {
        if(scenePath == "") 
        {
            Debug.Log("portal default to title screen");
            scenePath = "Assets/Scenes/LevelSelect.unity";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            // Debug.Log("you're going home");
            SceneManager.LoadScene(scenePath, LoadSceneMode.Single);

        }
    }
}
