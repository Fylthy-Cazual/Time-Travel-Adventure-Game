using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationTrigger : MonoBehaviour
{
    private GameObject player;
    [SerializeField] float TriggerPoint;
    [SerializeField] List<GameObject> objects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject obj in objects) {
                obj.SetActive(false);
            }
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.transform.position.x - TriggerPoint) < 1) {
            foreach(GameObject obj in objects) {
                obj.SetActive(true);
            }
        }
    }
}
