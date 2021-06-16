using UnityEngine;

// THIS CLASS MOVES THE ENDPOINT OF THE ROPE TOWARDS THE PLAYER, EFFECTIVELY SHRINKING THE ROPE

public class TemporaryEndPoint : MonoBehaviour
{
    [SerializeField]
    private Settings settings;
    [SerializeField]
    private Transform player;
    public bool move = false;

    // Update is called once per frame
    void Update()
    {
        if (move) {
            transform.position = Vector2.MoveTowards(transform.position, player.position, settings.shrinkSpeed * Time.deltaTime);
        }
    }
}
