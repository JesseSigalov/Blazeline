using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    [SerializeField]
    private Settings settings;

    public Transform throwingPoint;
    public GameObject thrownPrefab;

    Rigidbody2D rb;

    private void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Wait for input
        //calculate direction
        //add force

        if (Input.GetButtonDown("Fire1"))
        {
            if (thrownPrefab) 
            {
                GameObject obj = Instantiate(thrownPrefab, throwingPoint.position, Quaternion.identity);
                rb = obj.GetComponent<Rigidbody2D>();
                rb.gravityScale = settings.defaultGravityScale;
            } else { Debug.Log("NO THROWN PREFAB SET"); }

            Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - throwingPoint.position;
            rb.AddForce(direction * settings.throwForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }

    }
}
