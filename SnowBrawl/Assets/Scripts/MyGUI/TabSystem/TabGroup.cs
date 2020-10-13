using System.Collections.Generic;
using UnityEngine;

public abstract class TabGroup : MonoBehaviour
{
    [SerializeField] private List<GameObject> pages;

    [SerializeField] private EffectBase selectEffect;
    
    protected List<TabButton> tabButtons;

    protected TabButton selectedTab;

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

    public abstract void EnterTab(TabButton tabButton);

    public virtual void SelectTab(TabButton tabButton)
    {
        if (selectedTab != null)
            selectedTab.Deselect();

        selectedTab = tabButton;

        ResetTabButtonsVisuals();

        int index = tabButton.transform.GetSiblingIndex();

        for (int i = 0; i < pages.Count; i++)
            pages[i].SetActive(index == i);
    }

    public void ExitTab()
    {
        ResetTabButtonsVisuals();
    }

    protected abstract void ResetTabButtonsVisuals();

    private void ResetPages()
    {
        pages.ForEach(p => p.SetActive(false));
    }
}
