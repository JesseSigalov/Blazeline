using System.Collections;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class TNT : MonoBehaviour
{
    [SerializeField]
    private Settings settings;

    public AudioSource aud;
    public AudioClip boomSFX;
    public LayerMask destructible;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Flame (TNT)"))
        {
            StartCoroutine(Explosion());
            
        }
    }

    private void Update() {
        if (anim) {explosionLight.intensity = Mathf.SmoothDamp(0, maxBrightness, ref yVel, 0.15f);}
    }

    public Light2D explosionLight;
    public float maxBrightness;
    float yVel;
    bool anim;

    IEnumerator Explosion ()
    {

        yield return new WaitForSecondsRealtime(settings.explosionTime);
        aud.PlayOneShot(boomSFX);

        anim = true;

        Collider2D[] walls = Physics2D.OverlapCircleAll(transform.position, settings.explosionRadius, destructible);
        foreach (Collider2D wall in walls)
        {
            Destroy(wall.gameObject);
        }
         anim = false;

        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, settings.explosionRadius);
    }
}
