using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject _BulletPrefab;
    public Transform _BulletInitPosition;
    public Transform _BulletDirection;

    private List<GameObject> _BulletPrefabList;

    public AudioSource _bulletSound;

    // Start is called before the first frame update
    void Start()
    {
        _BulletPrefabList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0.0f)
        {
            FireBullet();
        }

        MoveBullet();
    }

    void FireBullet()
    {
        // podniesc pocisk troche wyzej TODO TODO TODO
        var tempBullet = Instantiate(_BulletPrefab, transform.position + 35.0f * transform.forward, transform.rotation);
        _BulletPrefabList.Add(tempBullet);
        _bulletSound.Play();
    }

    void MoveBullet()
    {
        foreach(var bullet in _BulletPrefabList)
        {
            if(bullet == null)
            {
                //_BulletPrefabList.Remove(bullet);
            }
            else
            {
                bullet.transform.position += Time.deltaTime *
                    bullet.GetComponent<BParametersScript>().getSpeed()
                    * bullet.transform.forward;
            }
        }
    }

}
