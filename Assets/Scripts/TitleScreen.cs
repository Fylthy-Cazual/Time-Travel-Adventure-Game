using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button5;
    public Button button7;


    // Start is called before the first frame update
    void Start()
    {
        Button btn1 = button1.GetComponent<Button>();
        Button btn2 = button2.GetComponent<Button>();
        Button btn5 = button5.GetComponent<Button>();
        Button btn7 = button7.GetComponent<Button>();

		btn1.onClick.AddListener(LoadLevel1);
		btn2.onClick.AddListener(LoadLevel2);
		btn5.onClick.AddListener(LoadLevel5);
		btn7.onClick.AddListener(LoadLevel7);
    }

    void LoadLevel1(){
		Debug.Log ("loading level 1");
        SceneManager.LoadScene("Assets/Scenes/1st level.unity", LoadSceneMode.Single);

	}
    void LoadLevel2(){
		Debug.Log ("loading level 1");
        SceneManager.LoadScene("Assets/Scenes/2nd level.unity", LoadSceneMode.Single);

	}
    void LoadLevel5(){
		Debug.Log ("loading level 1");
        SceneManager.LoadScene("Assets/Scenes/5th level.unity", LoadSceneMode.Single);

	}
    void LoadLevel7(){
		Debug.Log ("loading level 1");
        SceneManager.LoadScene("Assets/Scenes/7th level.unity", LoadSceneMode.Single);

	}

    // // Update is called once per frame
    // void Update()
    // {
        
    // }




}
