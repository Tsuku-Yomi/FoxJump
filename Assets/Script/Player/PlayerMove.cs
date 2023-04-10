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
    public PhysicsMaterial2D jumpMaterial2D;
    public PhysicsMaterial2D groundMaterial2D;
    //public Collider2D plant;
    float XAxisMovement = 0;
    float beShockTime = 0;
    uint InGroundTime = 0;
    public RaycastHit2D[] cast = new RaycastHit2D[3];
    void Start() {
        if (rigidbody2d == null) rigidbody2d = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
        if (collider2d == null) collider2d = GetComponent<Collider2D>();
    }

    void Update() {
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
    }

    void CheckGround() {
        if (collider2d.Raycast(Vector2.down, cast, 1f, (1 << 10)|(1 << 11)) > 0) {
            if (InGroundTime == 0) collider2d.sharedMaterial = groundMaterial2D;
            InGroundTime++;
        } else {
            if (InGroundTime != 0) collider2d.sharedMaterial = jumpMaterial2D;
            InGroundTime = 0;
        }
    }

    void MoveByJump() {
        if (Input.GetButtonDown("Jump") && GroundTimeSet <= InGroundTime) {
            JumpUp();
        }
    }

    public void JumpUp() {
        InGroundTime = 0;
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
    }

    void MoveInXAxis() {
        XAxisMovement = Input.GetAxis("Horizontal");
        if (XAxisMovement != 0) {
            rigidbody2d.velocity = new Vector2(XAxisMovement * speedX, rigidbody2d.velocity.y);
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

    bool isDownPush = false;
    void FallofPlant() {
        if (Input.GetKey(KeyCode.S)) {
            if (!isDownPush) {
                Physics2D.IgnoreLayerCollision(11,8,true);
                //Physics2D.IgnoreCollision(collider2d, plant, true);
                isDownPush = true;
            }
        } else if (isDownPush) {
            Physics2D.IgnoreLayerCollision(11, 8, false);
            //Physics2D.IgnoreCollision(collider2d, plant, false);
            isDownPush = false;
        }
    }

    public void BeShock(Vector2 axis) {
        animator.SetBool("onShock", true);
        beShockTime = ShockTime;
        rigidbody2d.velocity = new Vector2(ShockPower * axis.x, ShockPower * axis.y);
    }

}
