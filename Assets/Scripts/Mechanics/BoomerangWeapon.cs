using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoomerangWeapon : MonoBehaviour
{

    [SerializeField] [Tooltip("boomer prefab goes here")]
    private GameObject boomerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnWeaponAttack(InputValue movementValue)
    {
        float val = movementValue.Get<float>();
        if (val == 1) //key down
        {
            Debug.Log("boom");
            GameObject boomer = Instantiate(boomerPrefab, transform);
            boomer.transform.position = new Vector3(transform.position.x,
                                                    transform.position.y,
                                                    transform.position.z); 
            boomer.GetComponent<Boomerang>().owner = gameObject;

            if (transform.localScale.x < 0)
            {
                boomer.GetComponent<Boomerang>().speed *= -1;
            }
    
        }

    }
}
