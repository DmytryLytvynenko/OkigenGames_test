using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictorySalute : MonoBehaviour
{
    [SerializeField] private Color[] _saluteColors;
    [SerializeField] private GameObject _saluteEffect;
    void Start()
    {
        StartCoroutine(SaluteCoroutine());
    }

    private IEnumerator SaluteCoroutine()
    {
        int randomSalutePos;
        int randomSaluteColor;

        while (true)
        {
            yield return new WaitForSeconds(0.85f);

            randomSalutePos = Random.Range(0, transform.childCount);
            randomSaluteColor = Random.Range(0, _saluteColors.Length);
            GameObject salute = Instantiate(_saluteEffect, transform.GetChild(randomSalutePos).position, Quaternion.identity);
            salute.GetComponent<ParticleSystem>().startColor = _saluteColors[randomSaluteColor];
        }
    }

    private void OnDisable()
    {
        StopCoroutine(SaluteCoroutine());
    }
}
