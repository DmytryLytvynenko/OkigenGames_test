using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoovingWithConveyor : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Conveyor"))
        {
            transform.parent = collision.transform;
        }
    }
/*
    private void OnCollisionExit(Collision collision)
    {
        transform.parent = null;
    }*/
}
