using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    //[SerializeField] AudioClip hit;//, explode;
    [SerializeField] GameObject explosionPrefab;

    Rigidbody _rb;
    NavMeshAgent _agent;
    AudioSource _audioSource;
    float health;
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody>();
        gameObject.SetActive(false);
        //ForceStart();
    }
    public void ForceStart()
    {
        health = maxHealth;
        _agent.enabled = true;
        _agent.SetDestination(new Vector3(Random.Range(3.5f, -4.3f), 0, -14));
        _agent.speed += Random.Range(-3f, 3f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shot"))
        {
            health -= 20;
            _audioSource.PlayOneShot(_audioSource.clip);
            if (health <= 0)
                Death();
        }
        else if(other.CompareTag("Obstacle"))
        {
            health -= 40;
            _audioSource.PlayOneShot(_audioSource.clip);
            if (health <= 0)
                Death();
        }
    }
    void Death()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GlobalEventManager.SendEnemyKilled();
        gameObject.SetActive(false);
    }
    public void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        _agent.enabled = false;
        _rb.isKinematic = false;
        _rb.useGravity = true;
        _rb.AddForceAtPosition(transform.position + new Vector3(Random.Range(-800f, 800f), Random.Range(1600, 2300), 0), transform.position + new Vector3(0, -2, 0), ForceMode.Impulse);
        Invoke("ResetAfterExplode", 3f);
        StartCoroutine(MovingBack());
    }
    void ResetAfterExplode()
    {
        _rb.useGravity = false;
        _rb.isKinematic = true;
        transform.position = transform.parent.position;
        _agent.enabled = true;
        gameObject.SetActive(false);
    }
    IEnumerator MovingBack()
    {
        while(gameObject.activeInHierarchy)
        {
            transform.position += Vector3.back * Time.fixedDeltaTime * 50;
            yield return new WaitForFixedUpdate();
        }
    }
}
    
