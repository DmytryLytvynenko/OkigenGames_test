using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMove : MonoBehaviour
{
    [SerializeField] private float _velocity;// скорость подъема
    [SerializeField] private float _offsetY;
    [SerializeField] private float _timeToLIve;
/*    [SerializeField] private Camera _cam;*/

    private Vector3 end;// докуда подниматься

    void Start()
    {
        end = new Vector3(transform.position.x, transform.position.y + _offsetY, transform.position.z);
        transform.LookAt(Camera.main.transform);
/*        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);
*/        Invoke(nameof(Die), _timeToLIve);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, end, _velocity * Time.deltaTime);
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
