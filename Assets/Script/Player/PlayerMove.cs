using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    Rigidbody2D rigidbody2d;
    Animator animator;
    Collider2D collider2d;
    public float speedX = 1.0f;
    public float jumpForce = 1.0f;
    public float GroundTimeSet = 10;
    public float ShockTime = 0.5f;
    public float ShockPower = 4f;
    public float wallCheckLenght = 1f;
    public float wallJumpSpeed = 1f;
    public float wallFallSpeed = 2f;
    public PhysicsMaterial2D jumpMaterial2D;
    public PhysicsMaterial2D groundMaterial2D;
    //public Collider2D plant;
    float XAxisMovement = 0;
    float beShockTime = 0;
    uint InGroundTime = 0;
    enum AirType {
        IN_AIR,
        IN_LEFT_WALL,
        IN_RIGHT_WALL,
        BETWEEN_WALL//在两堵墙之间
    }
    AirType inAirType=AirType.IN_AIR;
    float leftWallColdDown, rightWallColdDown;
    public float WallJumpColdDown=1f;
    public RaycastHit2D[] cast = new RaycastHit2D[3];
    void Start() {
        if (rigidbody2d == null) rigidbody2d = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
        if (collider2d == null) collider2d = GetComponent<Collider2D>();
    }

    void Update() {
        if (leftWallColdDown > 0f) {
            leftWallColdDown -= Time.deltaTime;
        }
        if (rightWallColdDown > 0f) {
            rightWallColdDown -= Time.deltaTime;
        }
        if (beShockTime > 0) {
            beShockTime -= Time.deltaTime;
            return;
        }
        MoveInXAxis();
        MoveByJump();
        FallofPlant();
        AnimParaSet();

    }

    void FixedUpdate() {

        CheckGround();
        CheckIsInWall();
    }

    void CheckGround() {
        if (collider2d.Raycast(Vector2.down, cast, 1f, (1 << 10)|(1 << 11)|(1 << 12)) > 0) {
            if (InGroundTime == 0) collider2d.sharedMaterial = groundMaterial2D;
            InGroundTime++;
        } else {
            if (InGroundTime != 0) collider2d.sharedMaterial = jumpMaterial2D;
            InGroundTime = 0;
        }
    }

    void CheckIsInWall() {
        inAirType = AirType.IN_AIR;
        if (InGroundTime <= GroundTimeSet) {
            bool inLeftWall = collider2d.Raycast(Vector2.left, cast, wallCheckLenght, (1 << 10) | (1 << 12)) > 0;
            bool inRightWall = collider2d.Raycast(Vector2.right, cast, wallCheckLenght, (1 << 10) | (1 << 12)) > 0;
            if (inLeftWall && inRightWall) inAirType = AirType.BETWEEN_WALL;
            else if (inLeftWall) inAirType = AirType.IN_LEFT_WALL;
            else if (inRightWall) inAirType = AirType.IN_RIGHT_WALL;
            if (inAirType != AirType.IN_AIR) {
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, Mathf.Clamp(rigidbody2d.velocity.y, -wallFallSpeed, float.MaxValue));
            }
        }
        
    }

    void MoveByJump() {
        if (Input.GetButtonDown("Jump")) {
            if(GroundTimeSet <= InGroundTime)JumpUp();
            else if (
                inAirType == AirType.IN_LEFT_WALL||
                inAirType==AirType.IN_RIGHT_WALL||
                inAirType==AirType.BETWEEN_WALL) {
                JumpInWall();
            }
        }
    }

    public void JumpUp() {
        InGroundTime = 0;
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
    }

    public void JumpInWall() {
        Debug.Log("Wall Jump");
        Vector2 vec;
        vec.y = jumpForce;
        if (inAirType == AirType.IN_LEFT_WALL && leftWallColdDown <= 0f) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            vec.x = wallJumpSpeed;
            leftWallColdDown = WallJumpColdDown;
        } else if (inAirType == AirType.IN_RIGHT_WALL && rightWallColdDown <= 0f) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
            vec.x = -wallJumpSpeed;
            rightWallColdDown = WallJumpColdDown;
        } else if (inAirType == AirType.BETWEEN_WALL) {
            vec.x = 0;
        } else {
            return;
        }
        rigidbody2d.velocity = vec;
    }

    void MoveInXAxis() {
        XAxisMovement = Input.GetAxis("Horizontal");
        if (XAxisMovement != 0) {
            rigidbody2d.velocity = new Vector2(Mathf.Lerp(rigidbody2d.velocity.x,XAxisMovement*speedX,0.1f), rigidbody2d.velocity.y);
            if (XAxisMovement > 0) {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            } else {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
            }
        }

    }

    void AnimParaSet() {
        animator.SetFloat("runPara", Mathf.Abs(XAxisMovement));
        animator.SetFloat("fallPara", rigidbody2d.velocity.y);
        animator.SetBool("onGround", InGroundTime != 0);
        animator.SetBool("onShock", false);
    }
    public float DownTime = 1f;
    float isDownPush = 0f;
    void FallofPlant() {
        if (Input.GetKey(KeyCode.S)) {
            if (isDownPush<=0) {
                Physics2D.IgnoreLayerCollision(11,8,true);
                //Physics2D.IgnoreCollision(collider2d, plant, true);
            }
            isDownPush = DownTime;
        } else if (isDownPush>0) {
            isDownPush -= Time.deltaTime;
            if(isDownPush<=0)
            Physics2D.IgnoreLayerCollision(11, 8, false);
            //Physics2D.IgnoreCollision(collider2d, plant, false);
        }
    }

    public void BeShock(Vector2 axis) {
        animator.SetBool("onShock", true);
        beShockTime = ShockTime;
        rigidbody2d.velocity = new Vector2(ShockPower * axis.x, ShockPower * axis.y);
    }

}
