using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character2D : MonoBehaviour
{
    [SerializeField]
    AudioManager audioManager;
    [Header("Horizontal Movement")]
    public float moveSpeed = 10f;
    public Vector2 direction;
    private bool facingRight = true;

    [Header("Vertical Movement")]
    public float jumpSpeed = 15f;
    public float jumpDelay = 0.25f;
    private float jumpTimer;
    [SerializeField]
    bool alreadyJumped = false;
    bool canReleaseJump = false;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public LayerMask groundLayer;
    public int playerNumber = 1;
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
    private BoxCollider2D ownHitbox;

    [Header("Hitboxes")]
    public Vector2 upSmackAngle;
    public Vector2 smackAngle;
    public Vector2 downSmackAngle;
    public Vector2 CurrentForceAngle { get; private set; }
    private pushObject pusher;
    private bool grabbing;

    [Header("Cooldown")]
    public BoxCollider2D Hitbox;
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
    public float hitStunTime;
    private float hitStunTimeTimer;


    [Header("Interact")]
    private GameObject grabbed;
    public GameObject Grabbed
    {
        get => grabbed;
        set
        {
            if (grabbed != null)
            {
                grabbed.GetComponent<Collider2D>().enabled = true;
                grabbed.GetComponent<Rigidbody2D>().simulated = true;
                grabbed.transform.parent = null;
            }

            if (value != null)
            {
                value.transform.rotation = Quaternion.identity;
                value.transform.parent = transform;
                Vector3 offset = value.GetComponent<Collider2D>().offset;
                value.transform.position += offset;
                value.transform.position = transform.position;
                value.GetComponent<Collider2D>().enabled = false;
                value.GetComponent<Rigidbody2D>().simulated = false;
            }
            grabbed = value;
        }
    }

    private void Start()
    {
        transform.parent.GetComponent<PassthroughPlayer>().character2D = this;
        pusher = GetComponentInChildren<pushObject>();
        ownHitbox = GetComponent<BoxCollider2D>();
        grabbing = false;
        hitStunTimeTimer = 0;
        audioManager = GetComponent<AudioManager>();
        characterHolder = gameObject;
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (direction.y < -0.4f)
            FallThroughFloor();

        bool wasOnGround = onGround;
        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer)
            || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);

        if (!wasOnGround && onGround)
        {
            StartCoroutine(JumpSqueeze(1.25f, 0.8f, 0.05f));
            if (jumpReleased)
                alreadyJumped = false;
            else
                canReleaseJump = true;
        }

        if (!jumpReleased && !alreadyJumped)
        {
            jumpTimer = Time.time + jumpDelay;
            alreadyJumped = true;
        }
        animator.SetBool("jumping", !onGround);
        AttackUpdate();
    }
    void FixedUpdate()
    {
        if (hitStunTimeTimer > 0)
            hitStunTimeTimer -= Time.fixedDeltaTime;
        else
        {
            animator.SetBool("hurt", false);
            moveCharacter(direction.x);
        }

        if (jumpTimer > Time.time && onGround)
        {
            Jump();
        }

        modifyPhysics();
    }
    void moveCharacter(float horizontal)
    {
        rb.AddForce(Vector2.right * horizontal * moveSpeed);

        if (horizontal != 0 && transform.rotation != Quaternion.identity && onGround)
        {
            rb.angularVelocity = 0;
        }
        if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
            Flip();
        }
        float maxSpeed = grabbed == null ? this.maxSpeed : this.maxSpeed * 0.3f;
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
        animator.SetFloat("horizontal", Mathf.Abs(direction.x));
    }
    void FallThroughFloor()
    {
        StartCoroutine(FallThroughFloorContius());
    }


    private IEnumerator FallThroughFloorContius()
    {
        bool run = true;
        while (run)
        {
            run = false;
            Collider2D[] cols = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y) + Hitbox.offset, Hitbox.size, 0);
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].TryGetComponent(out Stayer stayer))
                {
                    Physics2D.IgnoreCollision(stayer.transform.root.Find("Hitbox").GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>(), true);
                    transform.position += new Vector3(0, -0.01f, 0);
                    run = true;
                    yield return null;
                    continue;
                }
            }
        }
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
            else if (rb.velocity.y > 0 && jumpReleased)
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

        audioManager.playLandSound();
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

    void AttackUpdate()
    {
        //activate Hitbox after windUp
        if (currentWindUp <= 0 && !Hitbox.enabled)
        {
            Hitbox.enabled = true;
            animator.SetBool("attacking", true);
        }
        else
        {
            currentWindUp -= Time.deltaTime;
            pushObject.State state;
            if (grabbing)
            {
                state = pushObject.State.Grabing;
            }
            else if (Grabbed != null)
            {
                state = pushObject.State.Throwing;

                audioManager.playThrowingSound();
            }
            else
            {
                state = pushObject.State.Pushing;
            }
            pusher.push(CurrentForceAngle, facingRight, state);

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
            pusher.ClearList();
            CurrentForceAngle = Vector2.zero;
            grabbing = false;
            animator.SetBool("attacking", false);
        }
    }
    public void Stun()
    {
        hitStunTimeTimer = hitStunTime;
        animator.SetBool("hurt", true);
        Grabbed = null;
    }
    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }
    public void OnAttackSide()
    {
        if (currentCoolDown <= 0)
        {
            currentCoolDown = coolDown;
            currentActiceFrames = activeFrames;
            currentWindUp = windUp;
            CurrentForceAngle = smackAngle;

            audioManager.playPushSound();
        };
    }
    public void OnAttackUp()
    {
        if (currentCoolDown <= 0)
        {
            currentCoolDown = coolDownUp;
            currentActiceFrames = activeFramesUp;
            currentWindUp = windUpUp;
            CurrentForceAngle = upSmackAngle;

            audioManager.playPushSound();
        }
    }
    public void OnAttackDown()
    {
        if (currentCoolDown <= 0)
        {
            currentCoolDown = coolDownDown;
            currentActiceFrames = activeFramesDown;
            currentWindUp = windUpDown;
            CurrentForceAngle = downSmackAngle;

            audioManager.playPushSound();
        }
    }
    public void OnGrab()
    {
        if (currentCoolDown <= 0 && direction == Vector2.zero)
        {
            currentCoolDown = coolDown;
            currentActiceFrames = activeFrames;
            currentWindUp = windUp;
            CurrentForceAngle = smackAngle;
            grabbing = true;

            audioManager.playGrabSound();
        }
    }

    bool jumpReleased = true;
    public void OnJump()
    {
        jumpReleased = !jumpReleased;
        if (jumpReleased && canReleaseJump)
        {

            audioManager.playJumpSound();
            canReleaseJump = false;
            alreadyJumped = false;
        }
    }
}
