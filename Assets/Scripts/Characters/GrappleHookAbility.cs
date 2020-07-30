using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GrappleHookAbility : MonoBehaviour
{
    [SerializeField] KeyCode Activate, Deactivate, Up, Down;
    
    // You’ll use these variables to keep track of the different components the RopeSystem script will interact with.
    public GameObject ropeHingeAnchor;
    public DistanceJoint2D ropeJoint;
    public Transform crosshair;
    public SpriteRenderer crosshairSprite;
    public BasicCharacterControler playerMovement;
    private bool ropeAttached;
    private Vector2 playerPosition;
    private Rigidbody2D ropeHingeAnchorRb;
    private SpriteRenderer ropeHingeAnchorSprite;

    public LineRenderer ropeRenderer;

    public LayerMask ropeLayerMask;

    public LayerMask stopLayerMask;

    private float ropeMaxCastDistance = 20f;
    private List<Vector2> ropePositions = new List<Vector2>();
    private bool distanceSet;

    public Vector2 ropeHook;
    public float swingForce = 400f;

    public float climbSpeed = 3f;
    private bool isColliding;

    private Dictionary<Vector2, int> wrapPointsLookup = new Dictionary<Vector2, int>();

    void Awake()
    {
        // The Awake method will run when the game starts and disables the ropeJoint (DistanceJoint2D component). It'll also set playerPosition to the current position of the Player.
        ropeJoint.enabled = false;
        playerPosition = transform.position;
        ropeHingeAnchorRb = ropeHingeAnchor.GetComponent<Rigidbody2D>();
        ropeHingeAnchorSprite = ropeHingeAnchor.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (playerMovement.isActive == true)
        {
            // This is the most important part of your main Update() loop. First, you capture the world position of the mouse cursor using the camera's ScreenToWorldPoint method. You then calculate the facing direction by subtracting the player's position from the mouse position in the world. You then use this to create aimAngle, which is a representation of the aiming angle of the mouse cursor. The value is kept positive in the if-statement.
            var worldMousePosition =
    Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            
            var facingDirection = worldMousePosition - transform.position;
            var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
            if (aimAngle < 0f)
            {
                aimAngle = Mathf.PI * 2 + aimAngle;
            }
           

            // The aimDirection is a rotation for later use. You're only interested in the Z value, as you're using a 2D camera, and this is the only relevant axis. You pass in the aimAngle * Mathf.Rad2Deg which converts the radian angle to an angle in degrees.
            var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
            // The player position is tracked using a convenient variable to save you from referring to transform.Position all the time.
            playerPosition = transform.position;

            if (!ropeAttached)
            {
                SetCrosshairPosition(aimAngle);
            }
            else
            {
                crosshairSprite.enabled = false;

                var hit = Physics2D.Raycast(playerPosition, (ropeHook - (Vector2)transform.position).normalized, ropeMaxCastDistance, ropeLayerMask);

                // If a valid raycast hit is found, ropeAttached is set to true, and a check is done on the list of rope vertex positions to make sure the point hit isn't in there already.
                if (hit.collider != null)
                {
                    float diff = (hit.point.x - ropeHook.x) + (hit.point.x - ropeHook.x);
                    if (Mathf.Abs(diff) >= 0.1)
                    {
                        ResetRope();
                    }
                }
            }

            HandleInput(aimDirection);
            UpdateRopePositions();
            HandleRopeLength();

        }
    }
    

    private void SetCrosshairPosition(float aimAngle)
    {
        if (!crosshairSprite.enabled)
        {
            crosshairSprite.enabled = true;
        }

        var x = transform.position.x + 1f * Mathf.Cos(aimAngle);
        var y = transform.position.y + 1f * Mathf.Sin(aimAngle);

        var crossHairPosition = new Vector3(x, y, 0);
        crosshair.transform.position = crossHairPosition;
    }
    // HandleInput is called from the Update() loop, and simply polls for input from the left and right mouse buttons.
    private void HandleInput(Vector2 aimDirection)
    {
        if (Input.GetKey(Activate))
        {
            // When a left mouse click is registered, the rope line renderer is enabled and a 2D raycast is fired out from the player position in the aiming direction. A maximum distance is specified so that the grappling hook can't be fired in infinite distance, and a custom mask is applied so that you can specify which physics layers the raycast is able to hit.
            if (ropeAttached) return;
            
            var No_No = Physics2D.Raycast(playerPosition, aimDirection, ropeMaxCastDistance, stopLayerMask );
            if (No_No.collider != null)
            {
                Debug.Log("NO NO NO *Waggs finger*");
                return;
            }
            ropeRenderer.enabled = true;

            var hit = Physics2D.Raycast(playerPosition, aimDirection, ropeMaxCastDistance, ropeLayerMask);

            // If a valid raycast hit is found, ropeAttached is set to true, and a check is done on the list of rope vertex positions to make sure the point hit isn't in there already.
            if (hit.collider != null)
            {
                ropeAttached = true;
                if (!ropePositions.Contains(hit.point))
                {
                    
                    // Provided the above check is true, then a small impulse force is added to the slug to hop him up off the ground, and the ropeJoint (DistanceJoint2D) is enabled, and set with a distance equal to the distance between the slug and the raycast hitpoint. The anchor sprite is also enabled.
                    // Jump slightly to distance the player a little from the ground after grappling to something.
                    //transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
                    ropePositions.Add(hit.point);
                    ropeJoint.enabled = true;
                    ropeHook = hit.point;
                    ropeJoint.distance = Vector2.Distance(playerPosition, hit.point);
                    
                    //ropeHingeAnchorSprite.enabled = true;
                }
            }
            // If the raycast doesn't hit anything, then the rope line renderer and rope joint are disabled, and the ropeAttached flag is set to false
            else
            {
                ropeRenderer.enabled = false;
                ropeAttached = false;
                ropeJoint.enabled = false;
            }
        }
        

        if (Input.GetKey(Deactivate))
        {
            ResetRope();
        }
    }

    private void HandleRopeLength()
    {
        // 1
        if (Input.GetKey(Up) && ropeAttached && !isColliding)
        {
            ropeJoint.distance -= Time.deltaTime * climbSpeed;
        }
        else if (Input.GetKey(Down) && ropeAttached)
        {
            ropeJoint.distance += Time.deltaTime * climbSpeed;
        }
    }

    void OnTriggerStay2D(Collider2D colliderStay)
    {
        isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D colliderOnExit)
    {
        isColliding = false;
    }

    // If the right mouse button is clicked, the ResetRope() method is called, which will disable and reset all rope/grappling hook related parameters to what they should be when the grappling hook is not being used.
    private void ResetRope()
    {
        if (!ropeAttached)
        {
            return;
        }
            

        ropeJoint.enabled = false;
        ropeAttached = false;
        //playerMovement.isSwinging = false;
        ropeRenderer.positionCount = 2;
        ropeRenderer.SetPosition(0, transform.position);
        ropeRenderer.SetPosition(1, transform.position);
        wrapPointsLookup.Clear();


        //Launch the player on breaking the rope


        

        var playerToHookDirection = (ropeHook - (Vector2)transform.position).normalized;
        Vector2 perpendicularDirection;
        if (playerMovement.facingRight != true)
        {
            perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);

            var rightPerpPos = (Vector2)transform.position + perpendicularDirection * 2f;
            Debug.DrawLine(transform.position, rightPerpPos, Color.red, 0f);
        }
        else
        {
            perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
            var leftPerpPos = (Vector2)transform.position - perpendicularDirection * -2f;
            Debug.DrawLine(transform.position, leftPerpPos, Color.red, 0f);

        }
        var force = perpendicularDirection * swingForce;
        GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Force);


        ropePositions.Clear();
        //ropeHingeAnchorSprite.enabled = false;
    }
    private void UpdateRopePositions()
    {
        // 1
        if (!ropeAttached)
        {
            return;
        }

        // 2
        ropeRenderer.positionCount = ropePositions.Count + 1;

        // 3
        for (var i = ropeRenderer.positionCount - 1; i >= 0; i--)
        {
            if (i != ropeRenderer.positionCount - 1) // if not the Last point of line renderer
            {
                ropeRenderer.SetPosition(i, ropePositions[i]);

                // 4
                if (i == ropePositions.Count - 1 || ropePositions.Count == 1)
                {
                    var ropePosition = ropePositions[ropePositions.Count - 1];
                    if (ropePositions.Count == 1)
                    {
                        ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                    else
                    {
                        ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                }
                // 5
                else if (i - 1 == ropePositions.IndexOf(ropePositions.Last()))
                {
                    var ropePosition = ropePositions.Last();
                    ropeHingeAnchorRb.transform.position = ropePosition;
                    if (!distanceSet)
                    {
                        ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                        distanceSet = true;
                    }
                }
            }
            else
            {
                // 6
                ropeRenderer.SetPosition(i, transform.position);
            }
           
            
        }
    }
    // 1
    private Vector2 GetClosestColliderPointFromRaycastHit(RaycastHit2D hit, PolygonCollider2D polyCollider)
    {
        // 2
        var distanceDictionary = polyCollider.points.ToDictionary<Vector2, float, Vector2>(
            position => Vector2.Distance(hit.point, polyCollider.transform.TransformPoint(position)),
            position => polyCollider.transform.TransformPoint(position));

        // 3
        var orderedDictionary = distanceDictionary.OrderBy(e => e.Key);
        return orderedDictionary.Any() ? orderedDictionary.First().Value : Vector2.zero;
    }

}
