using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagingScenes : MonoBehaviour {

    public void NextScene()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            SceneManager.LoadScene("InstructionsPt1");
        } else if (SceneManager.GetActiveScene().name == "InstructionsPt1")
        {
            SceneManager.LoadScene("InstructionsPt2");
        } else if (SceneManager.GetActiveScene().name == "InstructionsPt2")
        {
            SceneManager.LoadScene("Round1");
        }   
    }
}
