using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    //Slider _progressBar;
    float health;
    NavMeshAgent _agent;
    private void Start()
    {
        GlobalEventManager.OnSpawnBoss.AddListener(ForceStart);
        gameObject.SetActive(false);
    }
    void ForceStart()
    {
        gameObject.SetActive(true);
        health = maxHealth;
        //_progressBar = GameObject.FindGameObjectWithTag("BossBar").GetComponent<Slider>();
        //_progressBar.gameObject.SetActive(true);
        //_progressBar.gameObject.transform.position = new Vector2(0, 560);
        //_progressBar.maxValue = maxHealth;
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(new Vector3(0, 0, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        health -= 20;
        //_audioSource.PlayOneShot(_audioSource.clip);
        //_progressBar.value += 20;
        if (health <= 0)
        {
            //Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            GlobalEventManager.SendLevelComplete();
            gameObject.SetActive(false);
        }
    }
}
