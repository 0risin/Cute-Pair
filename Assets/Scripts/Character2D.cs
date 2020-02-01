using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2D : MonoBehaviour
{

    [Header("Horizontal Movement")]
    public float moveSpeed = 10f;
    public Vector2 direction;
    private bool facingRight = true;

    [Header("Vertical Movement")]
    public float jumpSpeed = 15f;
    public float jumpDelay = 0.25f;
    private float jumpTimer;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public LayerMask groundLayer;
    public GameObject characterHolder;

    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;

    [Header("Collision")]
    public bool onGround = false;
    public float groundLength = 0.6f;
    public Vector3 colliderOffset;

    [Header("Hitboxes")]
    public Vector2 upSmackAngle;
    public Vector2 smackAngle;
    public Vector2 downSmackAngle;
    Vector2 currentForceAngle=Vector2.zero;
    private pushObject pushObject;

    [Header("Cooldown")]
    public Collider2D Hitbox;
    public float windUpUp;
    public float windUp;
    public float windUpDown;
    public float activeFramesUp;
    public float activeFrames;
    public float activeFramesDown;
    public float coolDownUp;
    public float coolDown;
    public float coolDownDown;
    public float currentCoolDown;
    public float currentActiceFrames;
    public float currentWindUp;

    private void Start()
    {
        pushObject = GetComponentInChildren<pushObject>();
    }

    // Update is called once per frame
    void Update()
    {
        bool wasOnGround = onGround;
        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);

        if (!wasOnGround && onGround)
        {
            StartCoroutine(JumpSqueeze(1.25f, 0.8f, 0.05f));
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDelay;
        }
        animator.SetBool("onGround", onGround);
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Attack();
    }
    void FixedUpdate()
    {
        moveCharacter(direction.x);
        if (jumpTimer > Time.time && onGround)
        {
            Jump();
        }

        modifyPhysics();
    }
    void moveCharacter(float horizontal)
    {
        rb.AddForce(Vector2.right * horizontal * moveSpeed);

        if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
            Flip();
        }
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
        animator.SetFloat("horizontal", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("vertical", rb.velocity.y);
    }
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpTimer = 0;
        StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));
    }
    void modifyPhysics()
    {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

        if (onGround)
        {
            if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = 0f;
            }
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag * 0.15f;
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }
    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds)
    {
        Vector3 originalSize = Vector3.one;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            characterHolder.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }
        t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            characterHolder.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
    }

    void Attack()
    {
        if (Input.GetButtonDown("Attack") && currentCoolDown <= 0)
        {
            //set combat values
            if (Input.GetAxis("Horizontal") != 0 && currentCoolDown <= 0)
            {
                currentCoolDown = coolDown;
                currentActiceFrames = activeFrames;
                currentWindUp = windUp;

                currentForceAngle = smackAngle;
            }
            if (Input.GetAxis("Vertical") > 0 && currentCoolDown <= 0)
            {
                currentCoolDown = coolDownUp;
                currentActiceFrames = activeFramesUp;
                currentWindUp = windUpUp;
                currentForceAngle = upSmackAngle;
            }
            if (Input.GetAxis("Vertical") < 0 && currentCoolDown <= 0)
            {
                currentCoolDown = coolDownDown;
                currentActiceFrames = activeFramesDown;
                currentWindUp = windUpDown;
                currentForceAngle = downSmackAngle;
            }
        }

        //activate Hitbox after windUp
        if (currentWindUp <= 0 && !Hitbox.enabled)
        {
            Hitbox.enabled = true;
        }
        else
        {
            currentWindUp -= Time.deltaTime;
            pushObject.push(currentForceAngle, facingRight);
        }
        //count down cooldown before letting next attack
        if (currentCoolDown >= 0)
            currentCoolDown -= Time.deltaTime;
        //disable Hitbox after count down active frames 
        if (currentActiceFrames >= 0)
            currentActiceFrames -= Time.deltaTime;
        else
        {
            Hitbox.enabled = false;
            pushObject.ClearList();
            currentForceAngle = Vector2.zero;
        }
    }
    public Vector2 getCurrentForceAngle()
    {
        return currentForceAngle;
    }
}
