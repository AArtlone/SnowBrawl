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

        AsyncOperation loadingSceneAsync = SceneManager.LoadSceneAsync(loadingSceneName, LoadSceneMode.Additive);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        asyncOperation.allowSceneActivation = false;

        LoadingOperationProgress = (int)(asyncOperation.progress * 100f);

        if (asyncOperation.progress >= .9f)
            asyncOperation.allowSceneActivation = true;
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
