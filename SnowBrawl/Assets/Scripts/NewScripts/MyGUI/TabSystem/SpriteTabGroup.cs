using UnityEngine;

public class SpriteTabGroup : TabGroup
{
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private Sprite activeSprite;

    public override void EnterTab(TabButton tabButton)
    {
        if (selectedTab == null || tabButton == selectedTab)
            return;

        tabButton.UpdateVisual(hoverSprite);
    }

    public override void SelectTab(TabButton tabButton)
    {
        base.SelectTab(tabButton);

        selectedTab.Select(activeSprite);
    }

    protected override void ResetTabButtonsVisuals()
    {
        foreach (TabButton button in tabButtons)
        {
            // We dont reset the selected tab button
            if (selectedTab != null && selectedTab == button)
                continue;

            button.UpdateVisual(idleSprite);
        }
    }
}
