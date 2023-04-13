using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class TpDoor : MonoBehaviour
{
    public Transform targetTransform;
    public CinemachineConfiner2D confiner2D;
    public PolygonCollider2D targetCollider;
    public Material highlightMaterial;
    GameObject player;
    Material normalMaterial;
    SpriteRenderer render;
    bool isPlayerIn = false;
    private void Start() {
        render = GetComponent<SpriteRenderer>();
    }

    public void tpPlayer() {
        if (targetCollider != null&&confiner2D!=null) {
            confiner2D.m_BoundingShape2D = targetCollider;
        }
        player.transform.position=targetTransform.position;
        render.material = normalMaterial;
        isPlayerIn = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            player = collision.gameObject;
            normalMaterial = render.material;
            render.material = highlightMaterial;
            isPlayerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            render.material = normalMaterial;
            isPlayerIn = false;
        }
    }

    private void Update() {
        if (isPlayerIn && Input.GetKeyDown(KeyCode.E)) {
            tpPlayer();
        }
    }
}
