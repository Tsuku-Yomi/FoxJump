using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public delegate void HitEventHandler(int dmg);

    public event HitEventHandler hitEvent;

    public PlayerMove mov;
    Rigidbody2D rigbody;
    public float charaFootOffset=-0.5f;
    public float YDeadLine = -10;
    bool outofWorld = false;

    void Start()
    {
        mov = GetComponent<PlayerMove>();
        rigbody = GetComponent<Rigidbody2D>();
        hitEvent += CallManager;
        CallManager(-3);
    }

    private void Update() {
        if (outofWorld&&transform.position.y < YDeadLine) {
            if (hitEvent != null) hitEvent(1000);
            //TODO µôÂäËÀÍö
        }
    }

    public bool isInDrop(float y) {
        return charaFootOffset + transform.position.y > y;
    }

    public void BeHurt(Transform hitTrans,int dmg) {
        mov.BeShock(hitTrans.position.x < transform.position.x);
        if (hitEvent != null) hitEvent(dmg);
    }
    
    static void CallManager(int dmg) {
        ItemManager.Instance.AddItem("life", -dmg);
    }
}
