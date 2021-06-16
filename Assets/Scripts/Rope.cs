using System.Collections.Generic;
using UnityEngine;

// THIS CLASS IS RESPONSIBLE FOR THE ROPE'S PHYSICS 

[RequireComponent(typeof(LineRenderer))]
public class Rope : MonoBehaviour
{
    [SerializeField]
    private Settings settings;

    private LineRenderer lineRenderer;
    public List<RopeSegment> ropeSegments = new List<RopeSegment>();

    [SerializeField]
    private Transform player;

    //public bool endpointIsMouse = false;

    //public Transform endpointOverride;
    private Vector3 endpoint;


    // Use this for initialization
    void Start()
    {
        this.lineRenderer = this.GetComponent<LineRenderer>();
        Vector3 ropeStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        for (int i = 0; i < settings.segments; i++)
        {
            this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= settings.segmentLength;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.DrawRope();

        /*if (Input.GetKeyDown(KeyCode.X) && endpointIsMouse)
        {
            endpointIsMouse = false;
            endpointOverride.position = endpoint;
            endpointOverride.GetComponent<TemporaryEndPoint>().move = true;

        } else if (Input.GetKeyDown(KeyCode.X) && !endpointIsMouse)
        {
            endpointOverride.GetComponent<TemporaryEndPoint>().move = false;
            endpointIsMouse = true;
        }*/

    }

    private void FixedUpdate()
    {
        this.Simulate();
    }

    [HideInInspector]
    public float currentMaxRadius;

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, currentMaxRadius);
    }

    private void Simulate()
    {
        // SIMULATION
        Vector2 forceGravity = new Vector2(0f, -1.5f);

        for (int i = 1; i < settings.segments; i++)
        {
            RopeSegment firstSegment = this.ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
            this.ropeSegments[i] = firstSegment;
        }

        //CONSTRAINTS
        for (int i = 0; i < 50; i++)
        {
            this.ApplyConstraint();
        }
    }



    private void ApplyConstraint()
    {
        /*if (endpointIsMouse)
        {
            //Constraint to Mouse
            RopeSegment firstSegment = this.ropeSegments[0];
            firstSegment.posNow = CalculateRopeEndpoint();
            this.ropeSegments[0] = firstSegment;


        } else 
        {
            //Constraint to Fixed Endpoint
            RopeSegment firstSegment = this.ropeSegments[0];
            firstSegment.posNow = endpointOverride.position;
            this.ropeSegments[0] = firstSegment;
        }*/ 

        //Constraint to Mouse
        RopeSegment firstSegment = this.ropeSegments[0];
        firstSegment.posNow = CalculateRopeEndpoint();
        this.ropeSegments[0] = firstSegment;

        //Constraint to Player
        RopeSegment lastSegment = this.ropeSegments[settings.segments - 1];
        lastSegment.posNow = player.position;
        this.ropeSegments[settings.segments - 1] = lastSegment;

        for (int i = 0; i < settings.segments - 1; i++)
        {
            RopeSegment firstSeg = this.ropeSegments[i];
            RopeSegment secondSeg = this.ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - settings.segmentLength);
            Vector2 changeDir = Vector2.zero;

            if (dist > settings.segmentLength)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            } else if (dist < settings.segmentLength)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                this.ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this.ropeSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                this.ropeSegments[i + 1] = secondSeg;
            }
        }
    }

    private Vector3 CalculateRopeEndpoint()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        endpoint = mousePos;

        Vector3 directionToPlayer = mousePos - player.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer >= currentMaxRadius)
        {
            // endpoint is maxRadius units apart from player, along the line directionToPlayer
            endpoint = (directionToPlayer.normalized * currentMaxRadius) + player.position;
        }

        return endpoint;
    }

    private void DrawRope()
    {
        float lineWidth = settings.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[settings.segments];
        for (int i = 0; i < settings.segments; i++)
        {
            ropePositions[i] = this.ropeSegments[i].posNow;
        }

        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }

    public struct RopeSegment
    {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }
}