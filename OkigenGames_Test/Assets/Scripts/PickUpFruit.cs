using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickUpFruit : MonoBehaviour
{
    private TaskManager _taskManager;
    private Animator _playerAnim;
    private Animator _cameraAnim;
    private Camera _cam;
    private Transform _pickedFruit;
    [SerializeField] private Transform[] _fruitPosInBasket;
    [SerializeField] private Transform _basket;
    [SerializeField] private Transform _pickUpEffectPlace;
    [SerializeField] private Transform _pickUpTextPlace;
    [SerializeField] private GameObject _pickUpEffect;
    [SerializeField] private GameObject _pickUpText;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _task;
    [SerializeField] private LayerMask _layersToHit;
    [SerializeField] private Transform _playersHand;
    [SerializeField] private float _leftPickUpBoundary;
    [SerializeField] private float _rightPickUpBoundary;
    [SerializeField] private float _fruitScaleInBasket;

    private enum State
    {
        Idle,
        Dance,
        PickUpFruit
    }
    private void Start()
    {
        _playerAnim = GetComponent<Animator>();
        _taskManager = GetComponent<TaskManager>();
        _cam = Camera.main;
        _cameraAnim = Camera.main.GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {          
            HandleFruit(Input.mousePosition);
        }
    }
/*    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        HandleFruit(eventData);
    }
    private void HandleFruit(PointerEventData eventData)
    {
        Ray ray = _cam.ScreenPointToRay(eventData.position);
        if (Physics.Raycast(ray, out RaycastHit hitData, 100f, _layersToHit))
        {
            _anim.SetInteger("State", 2);
            _pickedFruit = hitData.transform;
            StartCoroutine(PickingFruitCoroutine(hitData.rigidbody));
        }
    }*/
    private void HandleFruit(Vector3 mousePos)
    {
        Ray ray = _cam.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hitData, 100f, _layersToHit))
        {
            if (_pickedFruit)
                return;

            _pickedFruit = hitData.transform;

            if (!_pickedFruit.CompareTag(_taskManager.GetNeededFruit()))
                return;

            if (_pickedFruit.position.x < _leftPickUpBoundary || _pickedFruit.position.x > _rightPickUpBoundary)
                return;

            StartCoroutine(PickingFruitCoroutine(hitData));
        }
    }
    private IEnumerator PickingFruitCoroutine(RaycastHit FruitData)
    {
        _playerAnim.SetInteger("State", (int)State.PickUpFruit);

        yield return new WaitForSeconds(0.5f);

        FruitData.rigidbody.useGravity = false;
        FruitData.rigidbody.isKinematic = true;
        _pickedFruit.parent = _playersHand;
        _pickedFruit.localPosition = new Vector3(-0.0845f, 0.1061f, 0.0448f);

        yield return new WaitForSeconds(0.75f);

        _pickedFruit.parent = _fruitPosInBasket[(int)_taskManager.CurrentFruitCount];
        _pickedFruit.localPosition = new Vector3(0, 0, 0);
        _pickedFruit.localScale = new Vector3(_fruitScaleInBasket, _fruitScaleInBasket, _fruitScaleInBasket);
        _pickedFruit.localRotation = Quaternion.identity;
        _pickedFruit = null;
        FruitData.collider.enabled = false;
        _playerAnim.SetInteger("State", (int)State.Idle);

        Instantiate(_pickUpEffect, _pickUpEffectPlace.position, Quaternion.identity);
        Instantiate(_pickUpText, _pickUpTextPlace.position, Quaternion.identity);

        if (_taskManager.CountFruit())
        {
            _winScreen.SetActive(true);
            _task.SetActive(false);
            _playerAnim.SetInteger("State", (int)State.Dance);
            _cameraAnim.SetBool("Win", true);

            yield return new WaitForSeconds(1f);

            transform.position = new Vector3(transform.position.x, transform.position.y, 2.1f);
            _basket.parent = null;
            _basket.position = new Vector3(3.2f, 0.2f, 1.3f);
            _basket.rotation = Quaternion.Euler(-90, 90, 0);
            StopCoroutine(nameof(PickingFruitCoroutine));
        }
    }
}
