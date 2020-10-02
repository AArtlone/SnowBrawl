using System;
using System.Collections;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    private bool hasPowerUp;
    private PowerUpType powerUpType;

    public void PowerUpWasPickedUp(PowerUpData powerUpData)
    {
        StartCoroutine(Player1PowerUpCo(powerUpData));
    }

    private IEnumerator Player1PowerUpCo(PowerUpData powerUpData)
    {
        hasPowerUp = true;
        powerUpType = powerUpData.powerUpType;

        yield return new WaitForSeconds(powerUpData.powerUpDuration);

        hasPowerUp = false;
    }

    public bool HasPowerUp(PowerUpType type)
    {
        if (!hasPowerUp)
            return false;

        return powerUpType == type;
    }
}
