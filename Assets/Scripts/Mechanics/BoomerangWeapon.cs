using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoomerangWeapon : MonoBehaviour
{

    [SerializeField] [Tooltip("boomer prefab goes here")]
    private GameObject boomerPrefab;
    [SerializeField] [Tooltip("Where to put boomer in. Don't set this to gameObjects that changes its scale (e.g. player)")]
    private GameObject boomerParent;
    private bool isthrownn;

    // Start is called before the first frame update
    void Start()
    {
        isthrownn = false;
    }

    void OnWeaponAttack(InputValue movementValue)
    {
        float val = movementValue.Get<float>();
        if (val == 1 && !isthrownn) //key down
        {
            GameObject boomer = Instantiate(boomerPrefab, transform);
            boomer.transform.parent = boomerParent.transform;
            boomer.transform.position = new Vector3(transform.position.x,
                                                    transform.position.y,
                                                    transform.position.z); 
            boomer.GetComponent<Boomerang>().owner = gameObject;
            isthrownn = true;

            if (transform.localScale.x < 0)
            {
                boomer.GetComponent<Boomerang>().speed *= -1;
            }
    
        }

    }

    public void boomerReturned() 
    {
    /// <summary>
    /// Called by Boomerang when it returns to the player.
    /// Sets isThrown to false;
    /// <summary>

        isthrownn = false;
    }
}
