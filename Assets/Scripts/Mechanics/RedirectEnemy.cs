using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedirectEnemy : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Enemy e = col.gameObject.GetComponent<Enemy>();
        if (e != null)
        {
            e.Redirect();
        }
    }
}
