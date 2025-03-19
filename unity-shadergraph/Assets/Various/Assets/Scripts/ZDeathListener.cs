using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZDeathListener : MonoBehaviour
{
    public UnityEvent<int> onZombieDeath;
    public void OnEnable()
    {
        ZombieCounter.onZombieDeath.AddListener(OnZombieDeath);
    }

    private void OnZombieDeath(int deadZombies)
    {
        onZombieDeath.Invoke(deadZombies);
    }
}
