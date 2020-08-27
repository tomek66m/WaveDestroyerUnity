using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CrystalManagerScript : MonoBehaviour
{
    private float _HPLevel = 1000.0f;
    private float _MaxHPLevel = 1000.0f;
    public Slider _slider;

    public TextMeshProUGUI _crystalHPText;
    public GameObject _EndGameText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = _HPLevel/_MaxHPLevel;
        _crystalHPText.text = "Krzyształ: " + _HPLevel;

        if(_HPLevel <= 0.0f)
        {
            _HPLevel = 0.0f;
            _EndGameText.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
//
        }

        Debug.Log(_HPLevel);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "CasterBullet")
        {
            _HPLevel -= 50.0f;
        }
    }

}
