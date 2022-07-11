using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    [SerializeField] GameObject bullet, exlposion;
    [SerializeField] float shotForce = 1.5f;
    bool shooting;
    //Vector3 razbros;
    public void ForceStart()
    {
        //razbros = new Vector3(Random.Range(-0.2f, 0.2f), 0, 1);
        shooting = true;
        Invoke("SelfDeactivate", 1.5f);
    }
    private void FixedUpdate()
    {
        
        if(shooting)
            transform.position += transform.forward * shotForce;
    }
    void SelfDeactivate()
    {
        shooting = false;
        exlposion.SetActive(false);
        bullet.SetActive(true);
        transform.position = Vector3.zero;
        gameObject.SetActive(false);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            shooting = false;
            CancelInvoke();
            exlposion.SetActive(true);
            bullet.SetActive(false);
            Invoke("SelfDeactivate", 0.5f);
        }
    }
}
