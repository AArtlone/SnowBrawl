using System.Collections;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager Instance;

    [SerializeField] private RectTransform droppedSnowballIconPrefab;
    [SerializeField] private RectTransform pickedUpSnowballIconPrefab;

    private int playerBaseCanvasIconsCounter;
    private int playerCanvasIconsCounter;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void PowerUpPickedUp(Player player, RectTransform prefab)
    {
        PositionEffect effect = InstantiateIcon(player.PlayerCanvas, playerCanvasIconsCounter, prefab, 0f, 10f);

        if (effect == null)
            return;

        StartCoroutine(PlayerCanvasIconCo(effect));
    }

    public void PickedUpSnowball(Player player)
    {
        PositionEffect effect = InstantiateIcon(player.PlayerCanvas, playerCanvasIconsCounter, pickedUpSnowballIconPrefab, -4f, 10f);

        if (effect == null)
            return;

        StartCoroutine(PlayerCanvasIconCo(effect));
    }

    public void DroppedSnowballPopUp(PlayerBase playerBase)
    {
        PositionEffect effect = InstantiateIcon(playerBase.Canvas, playerBaseCanvasIconsCounter, droppedSnowballIconPrefab, -20f, 30f);

        if (effect == null)
            return;

        StartCoroutine(PlayerBaseCanvasIconCo(effect));
    }

    private PositionEffect InstantiateIcon(Transform canvas, int counter, RectTransform prefab, float ySpawnPosOffset, float effectYTargetOffset)
    {
        RectTransform icon;
        if (counter == 0)
            icon = Instantiate(prefab, canvas.transform);
        else
        {
            icon = Instantiate(prefab, canvas.transform);

            icon.localPosition = new Vector2(0, counter * ySpawnPosOffset);
        }

        PositionEffect effect = icon.GetComponent<PositionEffect>();

        if (effect == null)
            return null;

        float yPos = icon.localPosition.y;

        effect.SetStartAndTargetValues(new Vector2(0, yPos), new Vector2(0, yPos + effectYTargetOffset));

        effect.PlayEffect();

        return effect;
    }

    private IEnumerator PlayerCanvasIconCo(EffectBase effect)
    {
        playerCanvasIconsCounter++;

        yield return new WaitForSeconds(effect.tween.targetTime);

        if (effect == null)
            yield break;

        if (effect.gameObject != null)
            Destroy(effect.gameObject);

        playerCanvasIconsCounter--;
    }

    private IEnumerator PlayerBaseCanvasIconCo(EffectBase effect)
    {
        playerBaseCanvasIconsCounter++;

        yield return new WaitForSeconds(effect.tween.targetTime);

        if (effect == null)
            yield break;

        if (effect.gameObject != null)
            Destroy(effect.gameObject);

        playerBaseCanvasIconsCounter--;
    }
}
