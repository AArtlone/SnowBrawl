using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunctionality : MonoBehaviour {

	
	// Update is called once per frame
	
     void GameScene(int gameScene)
    {

        //load the instructions scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Instructions");

    }
}
