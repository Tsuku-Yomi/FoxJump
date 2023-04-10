using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonGameObject : MonoBehaviour
{
    static HashSet<string> instanceSet;
    void Start()
    {
        if (!instanceSet.Contains(gameObject.name)) {
            instanceSet.Add(gameObject.name);
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
