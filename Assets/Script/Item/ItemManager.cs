using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public delegate void ItemCountChangeHandler(int num);
    static ItemManager instance;
    static public ItemManager Instance { get { return instance; } private set { instance = value; } }

    Dictionary<string, ItemCountChangeHandler> CountChangeEventMap=new Dictionary<string, ItemCountChangeHandler>();
    Dictionary<string, int> ItemCountMap = new Dictionary<string, int>();

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(this);
        }
        ItemCountMap.Add("life", 3);
    }

    public void RegisterCountChangeEvent(string itemName,ItemCountChangeHandler handler) {
        if (CountChangeEventMap.ContainsKey(itemName)) {
            CountChangeEventMap[itemName] += handler;
        } else {
            CountChangeEventMap.Add(itemName, handler);
        }
    }

    public void UnregEvent(string itemName, ItemCountChangeHandler handler) {
        if (CountChangeEventMap.ContainsKey(itemName)) {
            CountChangeEventMap[itemName] -= handler;
        }
    }

    public void AddItem(string itemName,int cnt) {
        if (ItemCountMap.ContainsKey(itemName)) {
            ItemCountMap[itemName] += cnt;
        } else {
            ItemCountMap.Add(itemName, cnt);
        }
        if (CountChangeEventMap.ContainsKey(itemName)) {
            CountChangeEventMap[itemName](ItemCountMap[itemName]);
        }
    }


}
