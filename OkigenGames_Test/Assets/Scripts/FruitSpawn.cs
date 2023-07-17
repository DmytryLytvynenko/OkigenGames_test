using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawn : MonoBehaviour
{
    [SerializeField] private float _spawnRate;
    [SerializeField] private GameObject[] _fruits;
    private float _timer = 0;

    private void Update()
    {
        if (_timer < 0)
        {
            SpawnFruit();
            _timer = _spawnRate;
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }

    private void SpawnFruit()
    {
        int randomFruit = Random.Range(0, _fruits.Length);

        Instantiate(_fruits[randomFruit], transform.position, Quaternion.identity);
    }
}
