using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
    public static string NextLoadScene{ get; private set; }
    public static string LastLoadScene { get; private set; }
    static bool isLoadSceceReady = false;
    AsyncOperation op=null;
    private void Start() {
        if(!isLoadSceceReady) {
            op=SceneManager.LoadSceneAsync("Scenes/LoadScene",LoadSceneMode.Single);
            op.allowSceneActivation = false;
        }
        isLoadSceceReady = true;
        Resources.UnloadUnusedAssets();
    }
    
    public string LoadSceneId="Scenes/Scene1";
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            LastLoadScene = SceneManager.GetActiveScene().name;
            NextLoadScene = LoadSceneId;
            op.allowSceneActivation = true;
            //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Scenes/LoadScene"));
        }
    }
}
