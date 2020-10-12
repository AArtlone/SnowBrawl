using UnityEngine;

public class MainMenuViewController : MonoBehaviour
{
    private bool isInLoadingSequence;

    private void Update()
    {
        if (isInLoadingSequence || !SBInputManager.Instance.AnyKeyDown())
            return;

        isInLoadingSequence = true;

        SBSceneManager.Instance.LoadControlsCustomization();
    }
}
