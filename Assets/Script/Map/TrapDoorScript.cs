using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class TrapDoorScript : MonoBehaviour
{
    int keyCount = 0;
    public Vector2 openDoorHigh = new  Vector2(0,0);
    public float openDoorTime = 0.5f;
    public bool isDestroyAfterOpen = true;
    public void registerKey() { keyCount++; }

    public void onKeyGet() {
        if (keyCount == 0) {
            Debug.LogWarning("trap door");
            return;
        }
        if (keyCount == 1) {
            transform.DOMove(openDoorHigh + (Vector2)transform.position, openDoorTime).onComplete += () => {if(isDestroyAfterOpen) Destroy(gameObject); };
            
            return;
        }
        keyCount--;
    }
}
