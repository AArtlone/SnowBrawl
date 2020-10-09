using TMPro;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadingText;

    private void Update()
    {
        string text = "Loading - " + SBSceneManager.Instance.LoadingOperationProgress.ToString();

        loadingText.text = text;
    }
}
