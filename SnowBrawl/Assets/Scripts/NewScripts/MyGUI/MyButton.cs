using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public Action onClick;

    [SerializeField] private UpdateMethod updateMethod;
    [Space(5f)]
    [SerializeField] private Image backgroundImage;
    [Space(5f)]
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private Sprite activeSprite;
    [Space(5f)]
    [SerializeField] private Color idleColor;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color activeColor;

    private void Awake()
    {
        if (backgroundImage == null)
        {
            Debug.LogWarning("BackgroundImage is not set in the editor.");
            enabled = false;
        }

        this.AddButtonSounds();

        if (updateMethod == UpdateMethod.Color)
            UpdateVisual(idleColor);
        else
            UpdateVisual(idleSprite);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (updateMethod == UpdateMethod.Color)
            UpdateVisual(hoverColor);
        else
            UpdateVisual(hoverSprite);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (updateMethod == UpdateMethod.Color)
            UpdateVisual(activeColor);
        else
            UpdateVisual(activeSprite);

        if (onClick != null)
            onClick.Invoke();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (updateMethod == UpdateMethod.Color)
            UpdateVisual(idleColor);
        else
            UpdateVisual(idleSprite);
    }

    private void UpdateVisual(Sprite sprite)
    {
        backgroundImage.sprite = sprite;
    }

    private void UpdateVisual(Color colorToUpdate)
    {
        Color color = new Color(colorToUpdate.r, colorToUpdate.g, colorToUpdate.b, 1f);

        backgroundImage.color = color;
    }
}

public enum UpdateMethod
{
    Sprite,
    Color
}
