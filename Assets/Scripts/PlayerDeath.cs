using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public Animator anim;
    public PlayerMovement playerMovement;
    public AudioSource aud;
    public AudioClip sizzle;

    private void Awake()
    {
        playerMovement.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Flame (Player)"))
        {
            StartCoroutine("Burn");
        }
        else if(collision.gameObject.CompareTag("Death Zone"))
        {
            GameObject.FindObjectOfType<SceneChanger>().LoadIndex(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator Burn()
    {
        if (anim.GetBool("Portal") == false && anim.GetBool("Burn") == false)
        {
            anim.SetBool("Burn", true);
            playerMovement.disabled = true;
            aud.PlayOneShot(sizzle);
            
            yield return new WaitForSecondsRealtime(2f);
            anim.SetBool("Burn", false);

            GameObject.FindObjectOfType<SceneChanger>().LoadIndex(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
