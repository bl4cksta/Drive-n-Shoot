using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager
{
    public static UnityEvent OnEnemyKilled = new UnityEvent(); // ������� �����
    public static UnityEvent OnSpawnBoss = new UnityEvent(); // ����� ���������� ������, ������� �����
    public static UnityEvent OnGameOver = new UnityEvent(); // ���� ����
    public static UnityEvent LevelComplete = new UnityEvent(); // ������ �������
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
