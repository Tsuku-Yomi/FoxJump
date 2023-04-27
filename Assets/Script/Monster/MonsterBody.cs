using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBody : MonoBehaviour
{
    bool isDestroy = false;
    public int dmg = 1;
    public float YDeadLine = -10;
    public float ShockPower = 2;
    public float invTime = 1f;
    public int getScore=100;
    Collider2D collider2d;
    private void Start() {
        collider2d = GetComponent<Collider2D>();
    }

    virtual protected void OnCollisionEnter2D(Collision2D collision) {
        if (isDestroy) return;
        else if (collision.gameObject.CompareTag("Player")) {
            var body = collision.gameObject.GetComponent<PlayerBody>();
            if (body.isInDrop(transform.position.y)) {
                this.BeAttack();
                ItemManager.Instance.AddItem("score", getScore);
                body.mov.JumpUp();
            } else {
                body.BeHurt(transform, dmg,ShockPower);
                collider2d.isTrigger = true;
                StartCoroutine(RestartRigidBody());
            }
        }
    }

    virtual protected void BeAttack() {
        GetComponent<Animator>().SetTrigger("Destroy");
        collider2d.isTrigger = true;
    }

    IEnumerator RestartRigidBody() {
        yield return new WaitForSeconds(invTime);
        collider2d.isTrigger = false;
    }
}
