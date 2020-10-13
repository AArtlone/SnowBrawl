using System.Collections;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    public bool HasAnyPowerUp { get; private set; }

    private PowerUpType powerUpType;

    public void PowerUpWasPickedUp(PowerUpData powerUpData)
    {
        SoundManager.PlaySound(Sound.PowerUp);

        StartCoroutine(Player1PowerUpCo(powerUpData));
    }

    private IEnumerator Player1PowerUpCo(PowerUpData powerUpData)
    {
        HasAnyPowerUp = true;
        powerUpType = powerUpData.powerUpType;

        yield return new WaitForSeconds(powerUpData.powerUpDuration);

        HasAnyPowerUp = false;
    }

    public bool HasPowerUp(PowerUpType type)
    {
        if (!HasAnyPowerUp)
            return false;

        return powerUpType == type;
    }
}
