using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    #region inspector var
    [Tooltip("Speed of the boomerang, like how fast the thing flys.")]
    public float speed = 10;
    [SerializeField] [Tooltip("how far it flies befor it comes back.")]
    private float maxDistance = 50;
    [SerializeField] [Tooltip("How much damage the boomerang does when it hits enemies.")]
    private float baseDamage = 1f;
    [SerializeField] [Tooltip("Damage multiplier for when the boomerang is on the way back to the player.")]
    private float returnDmgMult = 2.0f;
    [Tooltip("The player")]
    public GameObject owner = null;
    #endregion



    private float damage;
    private Rigidbody2D rb;
    private Vector2 startPos;
    private bool isReturning;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // todo: factor in direction the player is facing
        startPos = transform.position;
        isReturning = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (maxDistanceCheck() || isReturning)
        {
            returnToPlayer();
        } else{
            rb.velocity = transform.right * speed * 2;
            // Debug.Log(transform.right);
        }
    }
    
    private void returnToPlayer()
    ///<summary>
    /// go back to player
    ///<summary>

    {
        Debug.Log("coming bcak");
        rb.velocity = (owner.transform.position - transform.position).normalized * speed * 2;
    }

    private bool maxDistanceCheck()
    ///<summary>
    /// Return true if boomer >= max distance travel.
    ///<summary>

    {

        float currentDistance = Vector2.Distance(transform.position, startPos);

        if (currentDistance < 2 && isReturning) 
        {
            Destroy(gameObject);
        }

        if (currentDistance > maxDistance) 
        {
            isReturning = true;
            return true;
        }
        return false;
    }

    
}
