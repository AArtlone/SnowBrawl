using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunctionality : MonoBehaviour {

	
	// Update is called once per frame
	
     void GameScene(int gameScene)
    {

        //load the instructions scene
        SceneManager.LoadScene("Instructions");

    }
}
