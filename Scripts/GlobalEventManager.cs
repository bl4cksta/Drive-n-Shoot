using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager
{
    public static UnityEvent OnEnemyKilled = new UnityEvent(); // убиваем врага
    public static UnityEvent OnSpawnBoss = new UnityEvent(); // убили достаточно врагов, спавним босса
    public static UnityEvent OnGameOver = new UnityEvent(); // гейм овер
    public static UnityEvent LevelComplete = new UnityEvent(); // прошли уровень
    public static void SendEnemyKilled()
    {
        OnEnemyKilled.Invoke();
    }
    public static void SendSpawnBoss()
    {
        OnSpawnBoss.Invoke();
    }
    public static void SendGameOver()
    {
        OnGameOver.Invoke();
    }
    public static void SendLevelComplete()
    {
        LevelComplete.Invoke();
    }
}
