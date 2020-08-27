using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using System;

public class TurretParametersScript : MonoBehaviour
{

    // Start is called before the first frame update
    private float _time;


    public GameObject _TowerBulletPrefab;
    public GameObject _TowerStartShootingPoint;

    private float _fireDelay = 1.5f;
    public string _TurretType;
    public List<Material> _materialsList;

    public float _turretLevel = 1;

    public TextMeshProUGUI _turretTypeLevelText;

    void Start()
    {
        _time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_time <= _fireDelay)
            _time += Time.deltaTime;
        else
        {
            _time = 0.0f;
            if(gameObject.transform.GetChild(1).gameObject.activeSelf== true)
            {
                var tempEnemies = Physics.OverlapSphere(gameObject.transform.position, 65.0f, LayerMask.GetMask("minion"));
                foreach (var x in tempEnemies)
                {
                    // new one
                    var tempBullet = Instantiate(_TowerBulletPrefab, _TowerStartShootingPoint.transform.position, Quaternion.identity);

                    if (_TurretType == "water")
                    {
                        tempBullet.GetComponent<Renderer>().material = _materialsList[0];
                        tempBullet.GetComponent<TurretBulletScript>()._bulletSpeed = 200.0f * _turretLevel;
                        tempBullet.GetComponent<TurretBulletScript>()._shootingEnemyTarget = x.gameObject;
                    }

                    if (_TurretType == "fire")
                    {
                        tempBullet.GetComponent<Renderer>().material = _materialsList[1];
                        tempBullet.GetComponent<TurretBulletScript>()._bulletSpeed = 200.0f * _turretLevel;
                        tempBullet.GetComponent<TurretBulletScript>()._shootingEnemyTarget = x.gameObject;
                    }
                }
            }
        }
            
        // update levelu wiezyczki
        _turretTypeLevelText.text = "Typ: " + _TurretType + "\nPoziom: " + _turretLevel;

    }


    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

    }

}
