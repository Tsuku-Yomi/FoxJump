using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerBody : MonoBehaviour
{
    public delegate void HitEventHandler(int dmg);

    public event HitEventHandler hitEvent;

    public PlayerMove mov;
    Rigidbody2D rigbody;
    public float charaFootOffset=-0.5f;
    public float YDeadLine = -10;
    public float GhostTime = 1f;
    float leftGhostTime = 0f;
    bool outofWorld = false;
    Tweener ghost;
    Material playerMaterial;
    void Start()
    {
        mov = GetComponent<PlayerMove>();
        rigbody = GetComponent<Rigidbody2D>();
        hitEvent += CallManager;
        playerMaterial = GetComponent<SpriteRenderer>().material;
        ghost = playerMaterial.DOFade(0, 0.1f).SetLoops(-1, LoopType.Restart).Pause();
    }

    private void Update() {
        if (leftGhostTime > 0) {
            leftGhostTime -= Time.deltaTime;
            if (leftGhostTime <= 0) { ghost.Restart();ghost.Pause();}
        }
    }

    public bool isInDrop(float y) {
        return charaFootOffset + transform.position.y > y;
    }

    public void BeHurt(Transform hitTrans,int dmg,float power=1) {
        mov.BeShock(new Vector2(hitTrans.position.x < transform.position.x ? power:-power,0));
        if (hitEvent != null && leftGhostTime <= 0f) hitEvent(dmg);
        if (leftGhostTime > 0f) return;
        leftGhostTime = GhostTime;
        ghost.Play();
    }

    public void BeHurtByTrap(bool up,int dmg,float power=1) {
        mov.BeShock(new Vector2(0, up ? power : -power));
        if (hitEvent != null && leftGhostTime<=0f) hitEvent(dmg);
        if (leftGhostTime > 0f) return;
        leftGhostTime = GhostTime;
        ghost.Play();
    }
    
    static void CallManager(int dmg) {
        ItemManager.Instance.AddItem("life", -dmg);
    }
}
