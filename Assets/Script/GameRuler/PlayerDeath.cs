using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    void Start() {
        ItemManager.Instance.RegisterCountChangeEvent("life", OnPlayerHit);
    }

    void OnPlayerHit(int life) {
        if (life == 0) {
            SceneManager.LoadScene("DeathScene",LoadSceneMode.Single);
            Destroy(ItemManager.Instance.gameObject);
        }
    }
}
