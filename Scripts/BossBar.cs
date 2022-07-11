using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    Slider _progressBar;
    bool isGameActive;
    int timer = 100;
    void Start()
    {
        _progressBar = GetComponent<Slider>();
        GlobalEventManager.OnSpawnBoss.AddListener(ForceStart);
        GlobalEventManager.LevelComplete.AddListener(LevelComplete);
        gameObject.SetActive(false);
    }

    void ForceStart()
    {
        _progressBar.maxValue = timer;
        _progressBar.value = timer;
        isGameActive = true;
        gameObject.SetActive(true);
        StartCoroutine(TimerCount());
    }
    IEnumerator TimerCount()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(0.2f);
            timer--;
            _progressBar.value = timer;
            if (timer <= 0)
                GlobalEventManager.SendGameOver();
        }
    }
    void LevelComplete()
    {
        isGameActive = false;
        gameObject.SetActive(false);
    }
}
