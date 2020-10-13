using UnityEngine;

public class MainMenuViewController : MonoBehaviour
{
    private bool isInLoadingSequence;


    private void Start()
    {
        SoundManager.PlaySound(Sound.Background);
    }

    private void Update()
    {
        if (isInLoadingSequence || !SBInputManager.Instance.AnyKeyDown())
            return;

        isInLoadingSequence = true;

        SBSceneManager.Instance.LoadControlsCustomization();
    }
}
