using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverZone : MonoBehaviour
{
    [SerializeField] Slider gameoverSlider;
    [SerializeField] int maxGameoverPoints;
    int gameOverPoints = 0;
    bool isGameActive;
    private void Start()
    {
        isGameActive = true;
        gameoverSlider.maxValue = maxGameoverPoints;
        SetBar(gameOverPoints);
    }
    private void FixedUpdate()
    {
        if (isGameActive)
        {
            if (gameOverPoints >= maxGameoverPoints)
            {
                isGameActive = false;
                GlobalEventManager.SendGameOver();
            }
            if (gameOverPoints > 2)
                gameOverPoints -= 2;
            SetBar(gameOverPoints);
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            gameOverPoints += 2;
        }
    }

    public void SetBar(float curPoints)
    {
        gameoverSlider.gameObject.SetActive(curPoints > maxGameoverPoints / 10);

        //gameoverSlider.maxValue = maxPoints;
        gameoverSlider.value = curPoints;
    }
}
