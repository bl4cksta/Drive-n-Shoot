using UnityEngine;
using UnityEngine.UI;

public class TemperatureBar : MonoBehaviour
{
    //[SerializeField] Slider _slider;
    //[SerializeField] Vector3 offset;
    //Transform parent;
    Slider _slider;
    void Start()
    {
        _slider = GetComponent<Slider>();
    }
    public void SetBar(float curTemp, float maxTemp)
    {
        _slider.gameObject.SetActive(curTemp > maxTemp);
        
        _slider.maxValue = maxTemp;
        _slider.value = curTemp;
    }
}