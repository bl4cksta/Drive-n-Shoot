using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCutscene : MonoBehaviour
{
    [SerializeField] GameObject helicopter, playerCar;
    [SerializeField] float speed = 7f, rotSpeed = 10f;
    Camera _cam;
    Vector3[] bigHeliPos = new Vector3[4];
    Quaternion[] bigHeliRotation = new Quaternion[4];
    Vector3[] cameraPos = new Vector3[4];
    Quaternion[] camRotation = new Quaternion[4];
    int phase;
    
    void Start()
    {
        _cam = Camera.main;
        phase = 0; 

        bigHeliPos[0] = new Vector3(0, -0.92f, -1.9f); // присоединяем
        bigHeliRotation[0] = Quaternion.Euler(0, 0, 0);
        cameraPos[0] = new Vector3(5.3f, 2.7f, 11.31f);
        camRotation[0] = Quaternion.Euler(5, -170, 0);


        bigHeliPos[1] = new Vector3(-2.49f, 4.59f, 6.96f); // улетаем 1
        bigHeliRotation[1] = Quaternion.Euler(4, -12.5f, 0);
        cameraPos[1] = new Vector3(5.3f, 6.7f, 27.27f);
        camRotation[1] = Quaternion.Euler(4, -150, 0);

        bigHeliPos[2] = new Vector3(-4.7f, 8.57f, 12.69f); // улетаем 2
        bigHeliRotation[2] = Quaternion.Euler(6, -28.3f, 0);
        cameraPos[1] = new Vector3(5.3f, 6.7f, 27.27f);
        camRotation[2] = Quaternion.Euler(3, -145, 0);

    }

    void FixedUpdate()
    {
        if (helicopter.transform.position != bigHeliPos[phase])
        {
            helicopter.transform.position = Vector3.MoveTowards(helicopter.transform.position, bigHeliPos[phase], speed * Time.fixedDeltaTime);
            helicopter.transform.rotation = Quaternion.RotateTowards(helicopter.transform.rotation, bigHeliRotation[phase], rotSpeed * Time.fixedDeltaTime);
            _cam.transform.position = Vector3.MoveTowards(_cam.transform.position, cameraPos[phase], speed * Time.fixedDeltaTime);
            _cam.transform.rotation = Quaternion.RotateTowards(_cam.transform.rotation, camRotation[phase], rotSpeed * Time.fixedDeltaTime);
        }
        else if (phase == 0)
        {
            phase++;
            playerCar.transform.SetParent(helicopter.transform);
            //_cam.transform.SetParent(helicopter.transform);
        }
        else if(phase == 1)
        {
            phase++;
            GlobalEventManager.SendGameOver();
        }
    }
}
