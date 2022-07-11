using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Obstacle : MonoBehaviour
{
    private float speed = 50f;
    private Rigidbody _rb;
    private bool exploded;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        ForceStart();
    }

    void ForceStart()
    {
        speed += Random.Range(-10f, 10f);
    }

    void FixedUpdate()
    {
        if (transform.position.z >= -190)
            transform.position += (Vector3.back * Time.fixedDeltaTime * speed);
        else
        {
            gameObject.SetActive(false);
            transform.position = Vector3.zero;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!exploded)
            {
                Vector3 offsetRay = new Vector3(0, 7, 0);
                float attackRadius = 7f;
                int layerSkip = 1 << 6;
                RaycastHit[] hit = Physics.SphereCastAll(transform.position + offsetRay, attackRadius, transform.position, 6f, layerSkip);
                if (hit.Length > 0)
                {
                    for (int i = 0; i < hit.Length; i++)
                    {
                        hit[i].transform.gameObject.GetComponent<Enemy>().Explode();
                    }
                }
                ExplodeCar();
            }
        }
    }
    void ExplodeCar()
    {
        exploded = true;
        _rb.isKinematic = false;
        _rb.useGravity = true;
        _rb.AddForceAtPosition(transform.position + new Vector3(Random.Range(-400f, 400f), Random.Range(1500, 2300), 0), transform.position + new Vector3(0, -2, 0), ForceMode.Impulse);
        Invoke("ResetAfterExplosion", 3f);
    }
    void ResetAfterExplosion()
    {
        exploded = false;
        _rb.isKinematic = true;
        _rb.useGravity = false;
        gameObject.SetActive(false);
        //transform.position = transform.parent.position;
        //transform.rotation = transform.parent.position;
    }
}
