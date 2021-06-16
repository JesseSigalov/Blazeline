using UnityEngine;

// THIS CLASS HANDLES INPUT AND PLAYER MOTION

public class PlayerMovement : MonoBehaviour
{
    public Settings settings;
    public Animator anim;
    [SerializeField]
    Transform groundCheck;

    public bool disabled;

    Rigidbody2D rb;
    SpriteRenderer sr;

    bool jump;
    bool grounded;
    float xInput;


    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, settings.groundDetectionRadius, settings.groundLayer);

        GetInput();

        if (!disabled)
            rb.velocity = new Vector2(xInput * settings.horizontalSpeed * Time.deltaTime, rb.velocity.y);
        else
            rb.velocity = new Vector2(0f, rb.velocity.y);

        anim.SetFloat("Velocity", Mathf.Abs(xInput));

    }

    private void FixedUpdate() {
        if (grounded && jump)
        {
            jump = false;
            rb.AddForce(Vector2.up * settings.jumpForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
            anim.SetBool("Jump", false);
        }
    }

    private void GetInput() {

        xInput = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            anim.SetBool("Jump", true);
        }

        if (xInput > 0)
            sr.flipX = false;
        if (xInput < 0)
            sr.flipX = true;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, settings.groundDetectionRadius);
    }
}
