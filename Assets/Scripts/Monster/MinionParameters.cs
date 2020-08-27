using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinionParameters : MonoBehaviour
{
    public int _currentWaypointIndex = 0;

    private float _CurrentHP;
    private float _MaxHP = 100;

    public GameObject _healthBarUI;
    public Slider _slider;

    public Transform _CameraTransform;

    public GameObject _CoinPrefab;

    private void Start()
    {
        _CurrentHP = _MaxHP;
        _slider.value = CalculateHealth();
        //_healthBarUI.transform.LookAt(_CameraTransform);
    }


    void Update()
    {
        _slider.value = CalculateHealth();

        if (ShouldBeDestroyed())
        {
            // play anim
            // ...
            // decide if drop coin
            if (Random.Range(0, 5) == 1.0f)
            {
                Instantiate(_CoinPrefab, transform.position, Quaternion.identity);
            }


            // add score
            RoundsScript._killedMinions++;

            // destroy
            Destroy(this.gameObject);

            
        }



        
    }

    float CalculateHealth()
    {
        return _CurrentHP / _MaxHP;
    }

    void GetHit(float value)
    {
        _CurrentHP -= value;
    }

    bool ShouldBeDestroyed()
    {
        if (_CurrentHP <= 0)
            return true;
        else
            return false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            GetHit(2.0f);
        }
    }

}
