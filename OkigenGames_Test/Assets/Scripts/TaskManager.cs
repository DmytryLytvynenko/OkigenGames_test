using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private int _minFruitCount;
    [SerializeField] private int _maxFruitCount;
    [SerializeField] private TextMeshProUGUI _taskText;
    [SerializeField] private TextMeshProUGUI _taskProgress;
    private float _currentFruitCount = 0;
    private float _FruitsForTask;
    private Fruit _neededFruitType;

    public float CurrentFruitCount 
    {
        get
        {
            return _currentFruitCount;
        }
    }
    private enum Fruit
    {
        Banana,
        Apple,
        Orange
    }
    private void Start()
    {
        GenerateTask();
    }
    public bool CountFruit()
    {
        _currentFruitCount++;
        _taskProgress.text = $"Progress: {_currentFruitCount} / {_FruitsForTask}";

        return (_currentFruitCount / _FruitsForTask) == 1.0;
    }
    public string GetNeededFruit()
    {
        return _neededFruitType.ToString();
    }
    private void GenerateTask()
    {
        _neededFruitType = (Fruit)Random.Range(0, 3);
        _FruitsForTask = Random.Range(_minFruitCount, _maxFruitCount + 1);
        _taskProgress.text = $"Progress: {_currentFruitCount} / {_FruitsForTask}";
        if (_FruitsForTask != 1)
        {
            _taskText.text = $"Collect {_FruitsForTask} {_neededFruitType}s";
        }
        else
        {
            _taskText.text = $"Collect {_FruitsForTask} {_neededFruitType}s";
        }
    }
}
