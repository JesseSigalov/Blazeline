using UnityEngine;

public class ShrinkRope : MonoBehaviour
{
    [SerializeField]
    private Settings settings;

    Rope rope;

    // Start is called before the first frame update
    void Start()
    {
        rope = this.GetComponent<Rope>();

        rope.currentMaxRadius = settings.maxRadius;
    }

    // Update is called once per frame
    void Update()
    {
        rope.currentMaxRadius -= Time.deltaTime * settings.shrinkSpeed;
    }
}
