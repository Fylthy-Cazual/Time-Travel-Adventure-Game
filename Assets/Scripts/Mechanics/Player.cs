using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region Player Status
    [SerializeField] protected int maxHp;
    [SerializeField] Item heldItem;
    [SerializeField] List<Item> inventory;
    public int hp;
    [SerializeField] protected int maxTimeMeter;
    public int timeMeter;
    #endregion

    #region Player Movement
    [SerializeField] protected float runSpeed;
    [SerializeField] protected float maxJumpTime;
    [SerializeField] protected float initialJumpVelocity;
    [SerializeField] protected float fallSpeedMultiplier;
    [SerializeField] protected float dashDuration;
    [SerializeField] protected float dashSpeed;
    [SerializeField] protected float dashCooldown;
    [SerializeField] protected float slowdownDuration;
    float currentJumpVelocity;
    float movement;
    float dashY;
    bool canDash;
    bool isDashing;
    bool inKnockback;
    bool jumpIsHeld;
    bool canJump;
    protected float jumpStep; // Calculated on Start
    #endregion

    #region Unity
    protected Rigidbody2D rb;
    protected Animator animator;
    protected SpriteRenderer sr;
    protected TimeReverse tr;
    protected SlowdownField sf;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        inventory = new List<Item>();
        hp = maxHp;
        timeMeter = maxTimeMeter;

        currentJumpVelocity = initialJumpVelocity;
        jumpStep = initialJumpVelocity / maxJumpTime;
        
        movement = 0f;
        dashY = 0f;
        canDash = true;
        isDashing = false;
        jumpIsHeld = false;
        canJump = false;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<TimeReverse>();
        sf = GetComponentInChildren<SlowdownField>();
        sf.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        UpdateVelocity();
    }

    void OnMove(InputValue movementValue)
    {
        movement = movementValue.Get<float>();
        if (movement > 0) {
            // sr.flipX = true;
            transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y,transform.localScale.z);
        }
        if (movement < 0) {
            // sr.flipX = false;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y,transform.localScale.z);
        }
        animator.SetFloat("Movement", Mathf.Abs(movement));
    }

    void OnDown(InputValue movementValue)
    {
        float val = movementValue.Get<float>();
        if (val == 1)
        {
            dashY -= 1f;
        }
        else
        {
            dashY += 1f;
        }
    }

    void OnJump(InputValue movementValue)
    {
        float val = movementValue.Get<float>();
        // Jump was pressed down
        if (val == 1) 
        {
            if (canJump) 
            {
                jumpIsHeld = true;
                canJump = false;
            }
            dashY += 1f;
        }
        // Jump was released
        else 
        {
            jumpIsHeld = false;
            dashY -= 1f;
        }
    }

    void OnDash(InputValue movementValue)
    {
        float val = movementValue.Get<float>();
        // Dash was pressed down
        if (val == 1)
        {
            if (canDash)
            {
                StartDash();
                Invoke("EndDash", dashDuration);
                Invoke("RestoreDash", dashCooldown);
            }
        }
    }

    void OnReverse()
    {
        if (timeMeter > 0)
        {
            timeMeter -= 1;
            tr.ReverseTrue();
        }
    }

    void OnSlowdown()
    {
        if (timeMeter > 0)
        {
            timeMeter -= 1;
            sf.gameObject.SetActive(true);
            Invoke("SlowdownOver", slowdownDuration);
        }
    }

    void OnReset()
    {
        SceneManager.LoadScene("DemoLevel");
    }

    void SlowdownOver()
    {
        sf.gameObject.SetActive(false);
    }

    void UpdateVelocity()
    {
        if (isDashing || inKnockback) return;

        Vector2 currVelocity = rb.velocity;

        float newHorizontalVelocity = movement * runSpeed;

        float newVerticalVelocity = rb.velocity.y;
        if (jumpIsHeld && currentJumpVelocity > 0f)
        {
            newVerticalVelocity = currentJumpVelocity;
            currentJumpVelocity -= jumpStep;
            if (currentJumpVelocity < 0f) 
            {
                currentJumpVelocity = 0f;
                jumpIsHeld = false;
            }
        }
        else if (currentJumpVelocity < initialJumpVelocity)
        {
            newVerticalVelocity = -currentJumpVelocity;
            currentJumpVelocity += fallSpeedMultiplier * jumpStep;
            if (currentJumpVelocity > initialJumpVelocity)
            {
                currentJumpVelocity = initialJumpVelocity;
            }
        }
        rb.velocity = new Vector2(newHorizontalVelocity, newVerticalVelocity);
    }

    void StartDash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(movement, dashY).normalized * dashSpeed;
    }

    void EndDash()
    {
        isDashing = false;
        rb.velocity = Vector2.zero;
    }
    
    void RestoreDash()
    {
        canDash = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        bool wasFalling = rb.velocity.y < 0f;
        bool collidedVertically = true;

        Vector2 firstPoint = col.GetContact(0).point;
        for (int i = 1; i < col.contactCount; i++)
        {
            Vector2 currPoint = col.GetContact(i).point;
            if (currPoint.y != firstPoint.y)
            {
                collidedVertically = false;
                break;
            }
        }

        if (wasFalling && collidedVertically)
        {
            canJump = true;
            canDash = true;
        }
    }

    public void AddItem(Item item)
    {
        inventory.Add(item);
    }
    
    public void TakeDmg(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Die();
        }
    }

    public void KnockBack(Vector3 direction, float force)
    {
        inKnockback = true;
        rb.velocity = new Vector2(direction.x, direction.y) * force;
        Invoke("EndKnockBack", 1f);
    }

    void EndKnockBack()
    {
        rb.velocity = Vector2.zero;
        inKnockback = false;
    }

    void Die()
    {
        SceneManager.LoadScene("GameOver");
    }
}
