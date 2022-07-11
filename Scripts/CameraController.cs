using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 cameraPos = new Vector3(-0.00125f, 0.0145f, 0);
    private Quaternion cameraRotEulers;
    [SerializeField] float cameraSpeed = 0.05f;
    bool cameraPosOk;
    void Start()
    {
        GlobalEventManager.OnGameOver.AddListener(GameOver);
        cameraRotEulers = Quaternion.Euler(5, -90, 0);
        StartCoroutine(MoveCamera());
    }
    IEnumerator MoveCamera()
    {
        while(!cameraPosOk)
        {   
            yield return new WaitForFixedUpdate();
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, cameraPos, cameraSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, cameraRotEulers, 0.2f);
            if (transform.localPosition == cameraPos)
            {
                transform.localRotation = cameraRotEulers;
                cameraPosOk = true;
            }
        }
    }
    void GameOver()
    {
        cameraPosOk = false;
        cameraPos = new Vector3(0.019f, 0.0663f, -0.0773f);
        cameraRotEulers = Quaternion.Euler(25, -55, 0);
        StartCoroutine(MoveCamera());
    }
}
