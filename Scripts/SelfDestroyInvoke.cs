using UnityEngine;

public class SelfDestroyInvoke : MonoBehaviour
{
    [SerializeField] float delay = 1.5f;
    void Start()
    {
        Invoke("SelfDestroy", delay);
    }
    void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
