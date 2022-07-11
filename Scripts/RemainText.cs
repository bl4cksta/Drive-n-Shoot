using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemainText : MonoBehaviour
{
    int remainCount;
    Text remainText;
    void Start()
    {
        remainCount = Main.enemiesToKill;
        remainText = GetComponent<Text>();
        remainText.text = "Remain: " + remainCount;
        GlobalEventManager.OnEnemyKilled.AddListener(EnemyKilled);
    }
    void EnemyKilled()
    {
        remainCount--;
        if (remainCount > 0)
            remainText.text = "Remain: " + remainCount;
        else
            gameObject.SetActive(false);        
    }
}
