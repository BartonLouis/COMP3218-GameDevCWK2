using UnityEngine;

public class Character : MonoBehaviour
{

    public float targetDistance = 3f;
    public float viewRange = 100f;
    public float chaseSpeed = 50f;
    public float idleSpeed = 10f;
    public CharacterController2D controller;
    public Transform groundAheadCheck;
    public Transform castPoint;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;


    const float groundRadius = .2f;
    protected Transform target;
    private float direction = 1f;
    private bool chasing = false;
    private bool reached = false;


    // Start is called before the first frame update
    void Start()
    {
        target = Player.current.transform;
    }

    private void Update() {
        UpdateLogic();
    }

    protected void UpdateLogic() {
        if (CanSeeTarget()) {
            ChaseTarget();
            chasing = true;
        } else {
            chasing = false;
            StopChasingTarget();
        }
    }

    private bool CanSeeTarget() {
        reached = false;
        Vector2 endPos = castPoint.position + Vector3.right * viewRange * direction;
        RaycastHit2D ray = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));
        if (ray.collider != null) {
            if (ray.collider.gameObject.CompareTag("Player")) {
                return true;
            }
        }
        return false;
    }

    void onTargetReached() {
    
    }

    private void ChaseTarget() {
        // Turn to face same direction
        if (target.position.x < transform.position.x) {
            controller.FaceLeft();
            direction = -1f;
        } else {
            controller.FaceRight();
            direction = 1f;
        }
        float distance = Mathf.Abs(target.position.x - transform.position.x);
        if (distance < targetDistance) {
            onTargetReached();
            reached = true;
        }
        // Check that can move
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundAheadCheck.position, groundRadius, whatIsGround);
        if (colliders.Length > 0) {
            chasing = true;
        } else {
            StopChasingTarget();
        }
    }

    private void StopChasingTarget() {
        chasing = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundAheadCheck.position, groundRadius, whatIsGround);
        if (colliders.Length == 0) {
            controller.Flip();
            direction *= -1;
        }
    }

    private void FixedUpdate(){
        if (chasing && !reached) {
            controller.Move(direction * chaseSpeed * Time.fixedDeltaTime, false, false);
        } else if (!reached) {
            controller.Move(direction * idleSpeed * Time.fixedDeltaTime, false, false);
        } else {
            controller.Move(0, false, false);
        }
    }


}
