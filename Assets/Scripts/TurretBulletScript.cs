using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletScript : MonoBehaviour
{
    public GameObject _shootingEnemyTarget;
    public float _bulletSpeed = 350.0f;
    public float _delayToShoot = 1.0f;
    private float _timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer < _delayToShoot)
            _timer++;
        // jeżeli przeciwnik zginął
        if (_shootingEnemyTarget == null)
            Destroy(gameObject);
        else
        {
            if(_timer >= _delayToShoot)
            {
                if (Vector3.Distance(transform.position, _shootingEnemyTarget.transform.position) > 2.0f)
                {
                    transform.position = Vector3.MoveTowards(transform.position,
                                                             _shootingEnemyTarget.transform.position,
                                                             _bulletSpeed * Time.deltaTime);
                }
                else
                {
                    // hit
                    Destroy(gameObject);
                }
            }
        }
    }

}
