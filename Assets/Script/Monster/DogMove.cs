using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMove : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public float speed=5f;
    public bool StartAsLeft = true;
    Rigidbody2D rigbody;
    float lft;
    float rgh;

    private void Start() {
        rigbody = GetComponent<Rigidbody2D>();
        lft = left.transform.position.x;
        rgh = right.transform.position.x;
        if (!StartAsLeft) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            rigbody.velocity = new Vector2(-speed, rigbody.velocity.y);
        } else {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            rigbody.velocity = new Vector2(speed, rigbody.velocity.y);
        }
    }

    private void Update() {
        if (transform.position.x <= lft) {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            rigbody.velocity = new Vector2(speed, rigbody.velocity.y);
        } else if(transform.position.x >= rgh) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            rigbody.velocity = new Vector2(-speed, rigbody.velocity.y);
        }
    }
}
