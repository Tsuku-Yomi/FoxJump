using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeTextScript : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        ItemManager.Instance.RegisterCountChangeEvent("life", getLifeChange);
        ItemManager.Instance.AddItem("life", 0);
    }

    void getLifeChange(int life) {
        text.text = life.ToString();
    }

    private void OnDestroy() {
        ItemManager.Instance.UnregEvent("life", getLifeChange);
    }
}
