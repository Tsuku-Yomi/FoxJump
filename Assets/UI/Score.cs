using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text text;
    void Start() {
        text = GetComponent<Text>();
        ItemManager.Instance.RegisterCountChangeEvent("score",ScoreEvent);
    }

    void ScoreEvent(int score) {
        text.text = score.ToString();
    }

    private void OnDestroy() {
        ItemManager.Instance.UnregEvent("score", ScoreEvent);
    }
}
