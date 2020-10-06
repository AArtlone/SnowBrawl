using TMPro;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadingText;

    private void Update()
    {
        print(SBSceneManager.Instance.LoadingOperationProgress);

        string text = "Loading - " + SBSceneManager.Instance.LoadingOperationProgress.ToString();

        loadingText.text = text;
    }
}
