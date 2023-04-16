using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string ItemName="null";
    public int ItemNum=1;
    //防止多次获取
    bool isGet = false;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")&&!isGet) {
            isGet = true;
            ItemManager.Instance.AddItem(ItemName, ItemNum);
            Debug.Log("player get " + ItemName);
            GetComponent<Animator>()?.SetBool("IsDestroy", true);
        }
    }
}
