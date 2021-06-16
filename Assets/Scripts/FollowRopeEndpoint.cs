using UnityEngine;

//THIS SCRIPT MAKES THE FLAME FOLLOW THE ENDPOINT OF THE ROPE

public class FollowRopeEndpoint : MonoBehaviour
{
    [SerializeField]
    Settings settings;
    [SerializeField]
    Rope rope;
    Vector3 endpoint;

    // Update is called once per frame
    void Update()
    {
        
        endpoint = rope.ropeSegments[0].posNow;
        transform.position = endpoint;
    }
}
