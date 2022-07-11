using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject shotPrefab, shotPoint;
    //[SerializeField] float shotForce = 1000f;
    [SerializeField] float cameraDistance = 10f;
    [SerializeField] Image pricel;
    [SerializeField] Slider _slider;
    private readonly int maxTemp = 60; // максимальная температура до перегрева

    private Vector3 lookPos = new Vector3(0,0,-10);
    private bool shooting; // проверка в корутине на выстрел
    private int temperature; // перегрев оружия

    private bool ableToShoot; // можем стрелять из-за перегрева
    private bool isGameActive;
    private List<GameObject> shots = new List<GameObject>();
    private float razbrosRange = 1.1f; // разброс при стрельбе
    void Start()
    {
        isGameActive = true;
        GlobalEventManager.OnGameOver.AddListener(GameOver);
        ableToShoot = true;
        StartCoroutine(Shoot());
    }
    void MakeShot(GameObject shot)
    {
        shot.transform.position = shotPoint.transform.position;
        shot.transform.LookAt(lookPos + new Vector3(Random.Range(-razbrosRange, razbrosRange), Random.Range(-razbrosRange, razbrosRange), Random.Range(-razbrosRange, razbrosRange)));

        shot.SetActive(true);

        shot.GetComponent<ShotController>().ForceStart();
    }
    IEnumerator Shoot()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(0.1f);
            if (temperature >= maxTemp)
            {
                ableToShoot = false;
                shotPoint.SetActive(true);
                temperature -= 2;
            }
            else if (shooting == true && ableToShoot)
            {
                bool shooted = false;
                GameObject shot;
                if (shots.Count > 0)
                {
                    foreach (var shotOld in shots)
                    {
                        if (!shotOld.activeInHierarchy)
                        {
                            shooted = true;
                            shot = shotOld;
                            MakeShot(shot);
                            break;
                        }
                    }
                }
                if (!shooted)
                {
                    shot = Instantiate(shotPrefab, Vector3.zero, shotPrefab.transform.rotation);
                    MakeShot(shot);
                    shots.Add(shot);
                }
                temperature++; // добавляем по 1 градусу
            }
            else if (temperature > 1)
                temperature -= 2; // убираем по 2 градуса, может даже 3
            else if (temperature <= 1 && !ableToShoot)
            {
                ableToShoot = true;
                shotPoint.SetActive(false);
            }
            SetBar(temperature, maxTemp);
        }
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            lookPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y + 500, cameraDistance));
            pricel.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y + 500);
            shooting = true;
        }
        else
            shooting = false;
        if (transform.rotation != Quaternion.LookRotation(lookPos))
        {
            transform.parent.rotation = Quaternion.RotateTowards(transform.parent.rotation, Quaternion.LookRotation(lookPos), 150 * Time.deltaTime);
        }
    }

    public void SetBar(float curTemp, float maxTemp)
    {
        _slider.gameObject.SetActive(curTemp > 15);
        _slider.maxValue = maxTemp - 18;
        _slider.value = curTemp - 15;
    }
    void GameOver()
    {
        isGameActive = false;
    }
}
