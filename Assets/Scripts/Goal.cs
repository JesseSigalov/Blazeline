using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public Animator anim;
    public PlayerMovement playerMovement;
    public PlayerDeath playerDeath;
    public AudioSource aud;
    public AudioClip portalWhoosh;

    private void Awake()
    {
        playerDeath.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            StartCoroutine("Portal");
        }
    }

    IEnumerator Portal()
    {
        anim.SetBool("Portal", true);
        aud.PlayOneShot(portalWhoosh);
        playerMovement.disabled = true;
        playerDeath.enabled = false;
        yield return new WaitForSecondsRealtime(1.6f);

        GameObject.FindObjectOfType<SceneChanger>().LoadIndex(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
