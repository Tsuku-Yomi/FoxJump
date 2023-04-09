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
    }

    void getLifeChange(int life) {
        text.text = life.ToString();
    }
}
