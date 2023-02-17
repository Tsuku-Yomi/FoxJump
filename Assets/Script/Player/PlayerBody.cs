using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public delegate void HitEventHandler(int dmg);

    public event HitEventHandler hitEvent;

    public PlayerMove mov;
    Rigidbody2D rigbody;
    public float dropPara=-0.1f;
    public float YDeadLine = -10;
    bool outofWorld = false;

    void Start()
    {
        mov = GetComponent<PlayerMove>();
        rigbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (outofWorld&&transform.position.y < YDeadLine) {
            if (hitEvent != null) hitEvent(1000);
            //TODO µôÂäËÀÍö
        }
    }

    public bool isInDrop() {
        return rigbody.velocity.y < dropPara;
    }

    public void BeHurt(Transform hitTrans,int dmg) {
        mov.BeShock(hitTrans.position.x < transform.position.x);
        if (hitEvent != null) hitEvent(dmg);
    }
}
