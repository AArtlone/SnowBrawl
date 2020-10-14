using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SBSceneManager : MonoBehaviour 
{
    public static SBSceneManager Instance;

    [SerializeField] private string mainMenuSceneName;
    [SerializeField] private string loadingSceneName;
    [SerializeField] private string controlsSceneName;
    [SerializeField] private string tutorialSceneName;
    [SerializeField] private string[] roundsSceneNames;

    private int roundIndex;

    private bool isInLoading;

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

    public void LoadMainMenu()
    {
        if (isInLoading)
            return;

        StartCoroutine(LoadSceneCo(mainMenuSceneName));
    }

    public void LoadTutorial()
    {
        if (isInLoading)
            return;

        StartCoroutine(LoadSceneCo(tutorialSceneName));
    }
    
    public void LoadControlsCustomization()
    {
        if (isInLoading)
            return;

        StartCoroutine(LoadSceneCo(controlsSceneName));
    }

    public void LoadFirstRound()
    {
        if (isInLoading)
            return;

        roundIndex = 0;

        StartCoroutine(LoadSceneCo(roundsSceneNames[roundIndex]));
    }

    public void LoadNextRound()
    {
        if (isInLoading)
            return;

        roundIndex++;

        if (roundIndex >= roundsSceneNames.Length)
            return;

        StartCoroutine(LoadSceneCo(roundsSceneNames[roundIndex]));
    }

    private IEnumerator LoadSceneCo(string sceneToLoad)
    {
        isInLoading = true;

        yield return null;

        AsyncOperation loadingSceneAsync = SceneManager.LoadSceneAsync(loadingSceneName, LoadSceneMode.Additive);

        while(!loadingSceneAsync.isDone)
            yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            LoadingOperationProgress = (int)(asyncOperation.progress * 100f);
            
            if (asyncOperation.progress >= .9f)
            {
                asyncOperation.allowSceneActivation = true;
                isInLoading = false;
            }

            yield return null;
        }
    }

    public bool IsLastRound()
    {
        return roundIndex == roundsSceneNames.Length - 1;
    }
}
