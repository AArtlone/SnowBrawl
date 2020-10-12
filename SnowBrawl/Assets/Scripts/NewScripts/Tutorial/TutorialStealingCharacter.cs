using TMPro;
using UnityEngine;

public class TutorialStealingCharacter : TutorialPickingUpCharacter
{
    [SerializeField] private TextMeshProUGUI amount;

    protected override void UpdateSnowballs(int value)
    {
        base.UpdateSnowballs(value);

        amount.text = value.ToString();
    }
}
