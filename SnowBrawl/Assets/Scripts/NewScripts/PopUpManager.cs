using System.Collections;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager Instance;

    [SerializeField] private RectTransform iconPrefab;

    private int counter;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void DroppedSnowballPopUp(PlayerBase playerBase)
    {
        RectTransform icon;
        if (counter == 0)
            icon = Instantiate(iconPrefab, playerBase.Canvas.transform);
        else
        {
            icon = Instantiate(iconPrefab, playerBase.Canvas.transform);

            icon.localPosition = new Vector2(0, counter * -20f);
        }

        PositionEffect effect = icon.GetComponent<PositionEffect>();

        if (effect == null)
            return;

        float yPos = icon.localPosition.y;

        effect.SetStartAndTargetValues(new Vector2(0, yPos), new Vector2(0, yPos + 30f));

        effect.PlayEffect();

        StartCoroutine(DroppedSnowbalPopUpCo(effect));
    }

    private IEnumerator DroppedSnowbalPopUpCo(EffectBase effect)
    {
        counter++;

        yield return new WaitForSeconds(effect.tween.targetTime);

        Destroy(effect.gameObject);

        counter--;
    }
}
