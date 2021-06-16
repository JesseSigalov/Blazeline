using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoothFollow : MonoBehaviour
{

    [SerializeField]
    private Settings settings;

    public Transform lowerLeftBound, upperRightBound;
    public Vector3 offset;

    public Transform target;

    float minX, maxX, minY, maxY, hExt, vExt;
    bool constrain;

    private void Start() {
        vExt = Camera.main.orthographicSize * 2;
        hExt = vExt * Camera.main.aspect;

        if (lowerLeftBound && upperRightBound)
        {
            constrain = true;
            minX = lowerLeftBound.position.x + hExt / 2;
            minY = lowerLeftBound.position.y + vExt / 2;
            maxX = upperRightBound.position.x - hExt / 2;
            maxY = upperRightBound.position.y - vExt / 2;
        } else { 
            Debug.LogError("No camera bounds set ! Camera position will not be constrained.");
            constrain = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desired = Vector3.Lerp(transform.position, target.position + offset, settings.smoothingAmount);

        if (constrain) {
            desired.x = Mathf.Clamp(desired.x, minX, maxX);
            desired.y = Mathf.Clamp(desired.y, minY, maxY);
        }

        transform.position = desired;
    }
}
