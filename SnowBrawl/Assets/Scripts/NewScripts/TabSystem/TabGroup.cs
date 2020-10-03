using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private bool useColor;

    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private Sprite activeSprite;

    [SerializeField] private Color idleColor;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color activeColor;

    private List<TabButton> tabButtons;

    [SerializeField] private List<GameObject> pages;

    private TabButton selectedTab;

    private void Start()
    {
        ResetTabButtonsVisuals();

        ResetPages();

        int index = tabButtons[0].transform.GetSiblingIndex();

        SelectTab(tabButtons[index]);
    }

    public void Subscribe(TabButton tabButton)
    {
        if (tabButtons == null)
            tabButtons = new List<TabButton>();

        tabButtons.Add(tabButton);
    }

    public void EnterTab(TabButton tabButton)
    {
        if (selectedTab == null || tabButton == selectedTab)
            return;

        if (useColor)
            tabButton.UpdateVisual(hoverColor);
        else
            tabButton.UpdateVisual(hoverSprite);
    }

    public void SelectTab(TabButton tabButton)
    {
        if (selectedTab != null)
            selectedTab.Deselect();

        selectedTab = tabButton;

        ResetTabButtonsVisuals();

        if (useColor)
            selectedTab.Select(activeColor);
        else
            selectedTab.Select(activeSprite);

        int index = tabButton.transform.GetSiblingIndex();

        for (int i = 0; i < pages.Count; i++)
            pages[i].SetActive(index == i);
    }

    public void ExitTab()
    {
        ResetTabButtonsVisuals();
    }

    private void ResetTabButtonsVisuals()
    {
        foreach (TabButton button in tabButtons)
        {
            // We dont reset the selected tab button
            if (selectedTab != null && selectedTab == button)
                continue;

            if (useColor)
            {
                Color color = new Color(idleColor.r, idleColor.g, idleColor.b, 1f);

                button.UpdateVisual(color);

                continue;
            }

            button.UpdateVisual(idleSprite);
        }
    }

    private void ResetPages()
    {
        pages.ForEach(p => p.SetActive(false));
    }
}
