using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBody : MonoBehaviour
{
    bool isDestroy = false;
    public int dmg = 1;
    public float YDeadLine = -10;
    bool outofWorld = false;
    virtual protected void OnCollisionEnter2D(Collision2D collision) {
        if (isDestroy) return;
        else if (collision.gameObject.CompareTag("Player")) {
            var body = collision.gameObject.GetComponent<PlayerBody>();
            if (body.isInDrop()) {
                this.BeAttack();
                body.mov.JumpUp();
            } else {
                body.BeHurt(transform, dmg);
            }
        }
    }

    private void Update() {
        if (outofWorld&&transform.position.y < YDeadLine) {
            this.BeAttack();
            outofWorld = false;
        }
    }

    virtual protected void BeAttack() {
        GetComponent<Animator>().SetTrigger("Destroy");
    }
}
