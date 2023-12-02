using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    #region inspector var
    [Tooltip("Speed of the boomerang, like how fast the thing flys.")]
    public float speed = 20;
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


    void FixedUpdate()
    {
        if (maxDistanceCheck() || isReturning)
        {
            returnToPlayer();
        } else{
            rb.velocity = transform.right * -1 * speed;
        }
    }
    
    private void returnToPlayer()
    ///<summary>
    /// go back to player
    ///<summary>

    {
        Vector2 vel = (transform.position - owner.transform.position).normalized * -1 * Mathf.Abs(speed);
        rb.MovePosition(rb.position + vel * Time.fixedDeltaTime);
    }

    private bool maxDistanceCheck()
    ///<summary>
    /// Return true if boomer >= max distance travel.
    ///<summary>

    {

        float currentDistance = Vector2.Distance(transform.position, startPos);
        Debug.Log(currentDistance);
        if (currentDistance > maxDistance) 
        {
            isReturning = true;
            return true;
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isReturning)
        { // destroy self if is returning and touched player
            if (other.gameObject == owner)
            {
                owner.GetComponent<BoomerangWeapon>().boomerReturned();
                Destroy(gameObject);
            }
        }

         if (other.tag == "Enemy") {
            Enemy target = other.gameObject.GetComponent<Enemy>();

            if (isReturning) 
            { //bonuse damage on returning
                target.TakeDmg((int) (baseDamage * returnDmgMult)); 
            } else {
              //deal damage
                target.TakeDmg((int) baseDamage); 
            }
        }
    }

    
}
