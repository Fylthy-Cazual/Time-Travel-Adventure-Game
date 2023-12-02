using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlat : MonoBehaviour
{
    protected Collider2D cl;
    protected SpriteRenderer sr;
    bool startDisappear;
    // Start is called before the first frame update
    void Start()
    {
        cl = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        startDisappear = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Player player = col.gameObject.GetComponent<Player>();
        if (player != null && !startDisappear)
        {
            Invoke("Disappear", 4f);
            startDisappear = true;
            Invoke("Reappear", 5f);
            
        }
    }
    void Disappear()
    {
        cl.enabled = false;
        sr.enabled = false;
    }
    void Reappear()
    {
        cl.enabled = true;
        sr.enabled = true;
        startDisappear = false;
    }

}
