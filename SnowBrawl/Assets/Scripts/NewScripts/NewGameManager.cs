using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    public static NewGameManager Instance;

    [SerializeField] private MovementSettings mvSettings;

    public MovementSettings MVSettings { get { return mvSettings; } }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
