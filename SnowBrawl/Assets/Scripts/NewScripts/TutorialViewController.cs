using System.Collections.Generic;
using UnityEngine;

public class TutorialViewController : MonoBehaviour
{
    [SerializeField] private List<GameObject> tutorialPages;

    [Space(10f)]
    [SerializeField] private GameObject confirmationPopUp;

    [SerializeField] private GameObject previousPageButton;
    [SerializeField] private GameObject nextPageButton;
    [SerializeField] private GameObject startButton;

    private int tutorialPageIndex;

    private void Awake()
    {
        ShowFirstView();
    }

    private void ShowFirstView()
    {
        nextPageButton.SetActive(true);

        tutorialPages[tutorialPageIndex].SetActive(true);
    }

    public void ShowNextTutorialPage()
    {
        UpdateTutorialPage(true);
    }

    public void ShowPreviousTutorialPage()
    {
        UpdateTutorialPage(false);
    }

    private void UpdateTutorialPage(bool next)
    {
        tutorialPages[tutorialPageIndex].SetActive(false);

        if (next)
            tutorialPageIndex++;
        else
            tutorialPageIndex--;

        tutorialPages[tutorialPageIndex].SetActive(true);

        // Updating buttons 
        previousPageButton.SetActive(tutorialPageIndex != 0);

        nextPageButton.SetActive(tutorialPageIndex != tutorialPages.Count - 1);

        startButton.SetActive(tutorialPageIndex == tutorialPages.Count - 1);
    }

    public void ShowConfirmationPopUp()
    {
        confirmationPopUp.SetActive(true);
    }

    public void LoadGame()
    {
        if (SBSceneManager.Instance == null)
            return;

        SBSceneManager.Instance.LoadFirstRound();
    }
}
