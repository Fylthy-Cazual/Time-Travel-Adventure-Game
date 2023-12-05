using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class weaponManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning("The `weaponManager` is NOT done: open script for details!");
        // details below:
        // different weapons may be refactored into a weapon class at a later date
        // until then this script will do nothing.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnWeaponAttack(InputValue movementValue) 
    {
        float val = movementValue.Get<float>();
        if (val == 1) 
        {
            // call function from weapon
        }
    }

}
