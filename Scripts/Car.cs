using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float bounds = 3.6f;
    float secondsToChangeDirection = 2f;
    int direction; // 0 = right, 1 = left
    protected bool isGameActive;

    void Start()
    {
        isGameActive = true;
        GlobalEventManager.OnGameOver.AddListener(GameOver);
        direction = 0;
        StartCoroutine(Moving());
    }
    protected IEnumerator Moving()
    {
        while(gameObject.activeInHierarchy && isGameActive)
        {
            yield return new WaitForSeconds(secondsToChangeDirection);
            direction = Random.Range(0, 2);
            secondsToChangeDirection = Random.Range(2, 4);
        }
    }
    void Update()
    {
        if (isGameActive)
        {
            var posX = transform.position.x;
            if (posX > (bounds + 0.7f) || posX < (-bounds - 0.7f))
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
            else if (posX <= bounds && direction == 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.right, speed * Time.deltaTime);
            }
            else if (posX >= -bounds && direction == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.left, speed * Time.deltaTime);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (direction == 1) direction = 0;
        else direction = 1;
    }
    void GameOver()
    {
        isGameActive = false;
    }
}