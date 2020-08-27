using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PParametersScript : MonoBehaviour
{
    private float _currentHP;
    private float _maxHp = 200.0f;

    public Slider _HPSlider;
    public TextMeshProUGUI _HPText;

    public float _currentCoinsValue;
    private float _startCoinsValue = 500.0f;
    private float _maxCoinsValue = 5000.0f;
    private float _coinValue = 20.0f;

    public Slider _CoinValueSlider;
    public TextMeshProUGUI _CoinValueText;

    public GameObject _EndGameText;

    // Start is called before the first frame update
    void Start()
    {
        _currentHP = _maxHp;
        _currentCoinsValue = _startCoinsValue;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHPSlider();
        UpdateCoinValueSlider();
        CheckIfShouldEndTheGame();

    }

    void CheckIfShouldEndTheGame()
    {
        if(_currentHP <=0)
        {
            _currentHP = 0;
            _EndGameText.SetActive(true);
            Time.timeScale = 0.0f;

        }
    }

    void GetHit(float value)
    {
        if(_currentHP >= 0)
            _currentHP -= value;
    }

    public void GetCoin()
    {
        _currentCoinsValue += _coinValue;
        _currentCoinsValue = Mathf.Clamp(_currentCoinsValue, 0, _maxCoinsValue);
    }

    void UpdateHPSlider()
    {
        _HPSlider.value = _currentHP / _maxHp;
        _HPText.text = "HP: " + _currentHP;
    }

    void UpdateCoinValueSlider()
    {
        _CoinValueSlider.value = _currentCoinsValue / _maxCoinsValue;
        _CoinValueText.text = "Waluta: " + _currentCoinsValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CasterBullet")
        {
            GetHit(5.0f);
        }
    }
}
