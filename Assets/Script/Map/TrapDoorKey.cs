using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorKey : MonoBehaviour
{
    public TrapDoorScript trapDoor;
    Animator animator;
    private void Start() {
        trapDoor.registerKey();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            trapDoor.onKeyGet();
            if (animator != null)
                animator.SetTrigger("beGet");
            else
                Destroy(gameObject);
        }
    }
}
