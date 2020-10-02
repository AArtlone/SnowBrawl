using System.Collections.Generic;
using UnityEngine;

public class MainMenuViewController : MonoBehaviour
{
    [SerializeField] private GameObject introView;

    [SerializeField] private List<GameObject> tutorialPages;

    [SerializeField] private GameObject previousPageButton;
    [SerializeField] private GameObject nextPageButton;

    private int tutorialPageIndex;

    private bool inIntroView = true;

    private void Update()
    {
        if (inIntroView && Input.anyKeyDown)
        {
            inIntroView = false;

            introView.SetActive(false);

            ShowFirstView();
        }
    }

    private void ShowFirstView()
    {
        nextPageButton.SetActive(true);

        introView.SetActive(false);

        tutorialPages[tutorialPageIndex].SetActive(true);
    }

    public void ShowNextTutorialPage()
    {
        tutorialPages[tutorialPageIndex].SetActive(false);

        tutorialPageIndex++;

        previousPageButton.SetActive(tutorialPageIndex != 0);
        nextPageButton.SetActive(tutorialPageIndex != tutorialPages.Count - 1);

        tutorialPages[tutorialPageIndex].SetActive(true);
    }

    public void ShowPreviousTutorialPage()
    {
        tutorialPages[tutorialPageIndex].SetActive(false);

        tutorialPageIndex--;

        previousPageButton.SetActive(tutorialPageIndex != 0);
        nextPageButton.SetActive(tutorialPageIndex != tutorialPages.Count - 1);

        tutorialPages[tutorialPageIndex].SetActive(true);
    }
}
