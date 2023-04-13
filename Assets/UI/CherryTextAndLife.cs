using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CherryTextAndLife : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start() {
        text = GetComponent<Text>();
        ItemManager.Instance.RegisterCountChangeEvent("cherry", getCherryChange);
        ItemManager.Instance.AddItem("cherry", 0);
    }

    void getCherryChange(int cherry) {
        text.text = cherry.ToString();
    }

    private void OnDestroy() {
        ItemManager.Instance.UnregEvent("cherry", getCherryChange);
    }
}
