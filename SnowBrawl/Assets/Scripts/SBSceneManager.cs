using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SBSceneManager : MonoBehaviour 
{
    public static SBSceneManager Instance;

    [SerializeField] private string loadingSceneName;
    [SerializeField] private string controlsSceneName;
    [SerializeField] private string tutorialSceneName;
    [SerializeField] private string[] roundsSceneNames;

    public int LoadingOperationProgress { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

            DontDestroyOnLoad(this);
        }
    }

    public void LoadTutorial()
    {
        StartCoroutine(LoadSceneCo(tutorialSceneName));
    }
    
    public void LoadControlsCustomization()
    {
        StartCoroutine(LoadSceneCo(controlsSceneName));
    }

    private IEnumerator LoadSceneCo(string sceneToLoad)
    {
        yield return null;

        string activeScene = SceneManager.GetActiveScene().name;

        AsyncOperation loadingSceneAsync = SceneManager.LoadSceneAsync(loadingSceneName, LoadSceneMode.Additive);

        while(!loadingSceneAsync.isDone)
        {
            print("LoadingScene is not loaded yet");
            yield return null;
        }

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            LoadingOperationProgress = (int)(asyncOperation.progress * 100f);
            
            if (asyncOperation.progress >= .9f)
                asyncOperation.allowSceneActivation = true;

            yield return null;
        }
    }

    public void LoadFirstRound()
    {
        string sceneToLoad = roundsSceneNames[0];

        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadNextRound()
    {
        SceneManager.LoadScene("Round1");
    }
}
