using UnityEngine;

// THIS CLASS ENHANCES JUMPING BY MODIFYING GRAVITY AT RUNTIME 

public class BetterJumping : MonoBehaviour
{
    public Settings settings;

    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {


        if (rb.velocity.y < 0)
        {
            rb.gravityScale = settings.fallMultiplier;
        } else if (rb.velocity.y > 0 && !Input.GetButtonDown("Jump"))
        {
            rb.gravityScale = settings.lowJumpMultiplier;
        } else 
        {
            rb.gravityScale = settings.defaultGravityScale;
        }
    }
}
