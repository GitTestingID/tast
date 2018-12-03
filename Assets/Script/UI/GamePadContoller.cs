using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadContoller : MonoBehaviour {

    void GoNextScene()
    {
        LoadSceneManager.Instance.LoadSceneAsync(LoadSceneManager.SCENE_STATE.Game);
    }
	// Use this for initialization
	void Start () {
        Invoke("GoNextScene", 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
