using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZombieCounter : MonoBehaviour
{
    public static int deadZombies = 0;
    public static UnityEvent<int> onZombieDeath =new UnityEvent<int>();

    public void Start()
    {
        deadZombies = 0;
    }
    public static void AddDeadZombie()
    {
        deadZombies++;
        onZombieDeath.Invoke(deadZombies);
    }


}
