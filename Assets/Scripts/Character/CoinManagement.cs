using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManagement : MonoBehaviour
{
    private List<GameObject> _Coins;
    private float _CoinRotateSpeed = 100.0f;
    public GameObject _PlayerGameObject;
    void Start()
    {
        _Coins = new List<GameObject>();

        foreach(Transform child in transform)
        {
            _Coins.Add(child.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        RotateCoins();
    }

    void RotateCoins()
    {
        foreach(var coin in _Coins)
        {
            coin.transform.Rotate(new Vector3(0.0f, _CoinRotateSpeed * Time.deltaTime, 0.0f));
        }
    }
}
