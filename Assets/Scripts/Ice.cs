using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    [SerializeField]
    public Settings settings;
    public AudioSource flameAud;
    public AudioSource envAud;
    
    //public Animator anim;

    static int numberOfIcesTriggering = 0;
    bool melting = false;

    float baseIceLength;
    float baseIceWidth;
    float iceLength;

    private void Start()
    {
        numberOfIcesTriggering = 0;
        baseIceLength = transform.localScale.y;
        baseIceWidth = transform.localScale.x;
        iceLength = baseIceLength;
    }

    private void Update()
    {
        if (melting)
        {
            iceLength -= baseIceLength * Time.deltaTime / settings.meltTime;
            transform.localScale = new Vector3(baseIceWidth, iceLength, 1f);
            if (transform.localScale.y < 0.2f)
            {
                envAud.Play();
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Flame (Ice)"))
        {
            melting = true;
            numberOfIcesTriggering++;
            if (numberOfIcesTriggering == 1)
            {
                flameAud.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Flame (Ice)"))
        {
            melting = false;
            numberOfIcesTriggering--;
            if(numberOfIcesTriggering < 1)
                flameAud.Pause();
        }
    }
}
