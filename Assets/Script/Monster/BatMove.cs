using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class BatMove : MonoBehaviour
{
    public Transform leftUp;
    public Transform rightDown;
    public float awakeTime = 0.2f;
    public float deadTime = 10f;
    public float flySpeed = 7f;
    float xl, xr, yu, yd;

    bool isAwake = false;
    Transform playerTransform;
    Rigidbody2D rigid;
    Animator animator;

    private void Start() {
        playerTransform = GameObject.FindWithTag("Player")?.transform;
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        if (playerTransform == null) Debug.LogError("No Player");
        xl = leftUp.position.x;
        xr = rightDown.position.x;
        yu = leftUp.position.y;
        yd = rightDown.position.y;
        if (xl > xr) {
            float tmp = xl;
            xl = xr;
            xr = tmp;
        }
        if (yd > yu) {
            float tmp = yu;
            yu = yd;
            yd = tmp;
        }
    }

    private void Update() {
        if (!isAwake && awakeTest()) {
            isAwake = true;
            animator.SetTrigger("Awake");
            transform.DOMoveY(playerTransform.position.y, awakeTime).onComplete+=()=> {
                rigid.velocity = new Vector2(playerTransform.position.x < transform.position.x ? -flySpeed : flySpeed, 0);
                if(playerTransform.position.x < transform.position.x) {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                StartCoroutine(deadSelf());
            };
        }
    }

    bool awakeTest() {
        return playerTransform.position.x >= xl && playerTransform.position.x <= xr && playerTransform.position.y >= yd && playerTransform.position.y <= yu;
    }

    IEnumerator deadSelf() {
        yield return new WaitForSeconds(deadTime);
        animator.SetTrigger("Destroy");
    }
}
