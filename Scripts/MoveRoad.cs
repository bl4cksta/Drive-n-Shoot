using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRoad : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f, zBound = -190f;
    [SerializeField] GameObject roadToOrient;
    bool isGameActive;
    private void Start()
    {
        isGameActive = true;
        GlobalEventManager.OnGameOver.AddListener(GameOver);
    }
    void Update()
    {
        if (isGameActive)
            Move();
        else
            if(moveSpeed >= 0)
            {
                moveSpeed -= 0.05f;
                Move();
            }
    }
    void Move()
    {
        transform.position += Vector3.back * moveSpeed * Time.deltaTime;
                if (transform.position.z <= zBound)
                    transform.position = new Vector3(roadToOrient.transform.position.x - 0.04f, transform.position.y, roadToOrient.transform.position.z + 107.5359f);
    }
    void GameOver()
    {
        isGameActive = false;
    }
}
