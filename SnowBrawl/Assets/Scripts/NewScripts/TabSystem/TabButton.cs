using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] private TabGroup tabGroup;

    public Image BackgroundImage { get; private set; }

    private void Awake()
    {
        BackgroundImage = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.EnterTab(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.SelectTab(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.ExitTab();
    }

    public void Select(Color color)
    {
        UpdateVisual(color);
    }

    public void Select(Sprite sprite)
    {
        UpdateVisual(sprite);
    }

    public void Deselect()
    {

    }

    public void UpdateVisual(Color color)
    {
        Color newColor = new Color(color.r, color.g, color.b, 1f);
        BackgroundImage.color = newColor;
    }

    public void UpdateVisual(Sprite sprite)
    {
        BackgroundImage.sprite = sprite;
    }
}
