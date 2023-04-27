using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryToLife : MonoBehaviour
{
    public int addtime = 5;
    public int getScore = 100;
    // Start is called before the first frame update
    void Start()
    {
        ItemManager.Instance.RegisterCountChangeEvent("cherry", AddLife);
    }

    void AddLife(int cherry) {
        if (cherry % addtime == 0) {
            ItemManager.Instance.AddItem("life", 1);
        }
        ItemManager.Instance.AddItem("score", getScore);
    }

    private void OnDestroy() {
        ItemManager.Instance.UnregEvent("cherry", AddLife);
    }
}
