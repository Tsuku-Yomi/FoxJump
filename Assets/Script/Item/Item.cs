using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string ItemName="null";
    public int ItemNum=1;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            ItemManager.Instance.AddItem(ItemName, ItemNum);
            Debug.Log("player get " + ItemName);
            GetComponent<Animator>()?.SetBool("IsDestroy", true);
        }
    }
}
