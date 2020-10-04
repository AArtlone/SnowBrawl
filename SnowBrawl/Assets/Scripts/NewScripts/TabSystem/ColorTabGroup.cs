using UnityEngine;

public class ColorTabGroup : TabGroup
{
    [SerializeField] private Color idleColor;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color activeColor;

    public override void EnterTab(TabButton tabButton)
    {
        if (selectedTab == null || tabButton == selectedTab)
            return;

        tabButton.UpdateVisual(hoverColor);
    }

    public override void SelectTab(TabButton tabButton)
    {
        base.SelectTab(tabButton);

        selectedTab.Select(activeColor);
    }

    protected override void ResetTabButtonsVisuals()
    {
        foreach (TabButton button in tabButtons)
        {
            // We dont reset the selected tab button
            if (selectedTab != null && selectedTab == button)
                continue;

            Color color = new Color(idleColor.r, idleColor.g, idleColor.b, 1f);

            button.UpdateVisual(color);
        }
    }
}
