using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleShooter : MonoBehaviour
{
    public GameObject Eagle;
    public float EagleLifeTime = 4f;
    public float ShotColdTime = 3f;
    public float nowColdTime=3f;
    public Vector2 eagleSpeed;
    public bool eagleFaceLeft = true;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }

    private void Update() {
        if (nowColdTime <= 0f) {
            nowColdTime = ShotColdTime;
            GameObject eagle=Instantiate(Eagle, transform.position, transform.rotation,null);
            StartCoroutine(makeEagleDead(eagle)); 
        }
        nowColdTime -= Time.deltaTime;
    }
    
    IEnumerator makeEagleDead(GameObject obj) {
        obj.GetComponent<Rigidbody2D>().velocity = eagleSpeed;
        obj.transform.localScale = new Vector3(eagleFaceLeft ? -1 : 1, 1, 1);

        yield return new WaitForSeconds(EagleLifeTime);

        if (!(obj == null))
        obj.GetComponent<Animator>()?.SetTrigger("Destroy");
        
    }

}

