using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float wallSlideSpeed = 0.5f; // Kecepatan turun saat menempel di dinding tanpa input
    [SerializeField] private float jumpCooldown = 0.5f; // Waktu cooldown untuk jump

    SoundManager soundManager;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    private float lastJumpTime; // Waktu terakhir melakukan jump

    private void Awake()
    {
        // Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Set animator parameters
        anim.SetBool("walk", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        // Set jump animation when not grounded
        anim.SetBool("jump", !isGrounded());

        // Wall jump logic
        if (wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                if (horizontalInput == 0)
                {
                    // Turun perlahan jika tidak ada input horizontal
                    body.velocity = new Vector2(0, -wallSlideSpeed);
                }
                else
                {
                    // Tetap menempel pada dinding
                    body.gravityScale = 0;
                    body.velocity = Vector2.zero;
                }
            }
            else
                body.gravityScale = 1;

            if (Input.GetKey(KeyCode.Space) && Time.time >= lastJumpTime + jumpCooldown)
            {
                Jump();
                lastJumpTime = Time.time;
            }

            // Handle running sound
            if (isGrounded() && horizontalInput != 0)
            {
                soundManager.PlaySFX(soundManager.run);
            }
            else if (!isGrounded() && horizontalInput != 0)
            {
                soundManager.StopSFX(soundManager.run);
            }
            else
            {
                soundManager.StopSFX(soundManager.run);
            }
        }
        else
            wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
            SoundManager.PlaySFX("jump");// Play jump sound once
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 1, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 1, 1.5f);

            wallJumpCooldown = 0;
            SoundManager.PlaySFX("jump");// Play jump sound once
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.05f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
