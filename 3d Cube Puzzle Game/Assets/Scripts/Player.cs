using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isRolling;
    public float rotateSpeed;

    Bounds _bound;
    Vector3 left, right, up, down;

    private void Start()
    {
        _bound = GetComponent<BoxCollider>().bounds;

        left = new Vector3(-_bound.size.x / 2, -_bound.size.y / 2, 0);
        right = new Vector3(_bound.size.x / 2, -_bound.size.y / 2, 0);
        up = new Vector3(0, -_bound.size.y / 2, _bound.size.z / 2);
        down = new Vector3(0, -_bound.size.y / 2, -_bound.size.z / 2);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) && !isRolling)
        {
            StartCoroutine(Roll(left));
        }
        else if (Input.GetKey(KeyCode.D) && !isRolling)
        {
            StartCoroutine(Roll(right));
        }
        else if (Input.GetKey(KeyCode.W) && !isRolling)
        {
            StartCoroutine(Roll(up));
        }
        else if (Input.GetKey(KeyCode.S) && !isRolling)
        {
            StartCoroutine(Roll(down));
        }
    }

    IEnumerator Roll(Vector3 positionToRotation)
    {
        isRolling = true;
        float angle = 0;

        Vector3 point = transform.position + positionToRotation;
        Vector3 axis = Vector3.Cross(Vector3.up, positionToRotation).normalized;

        while (angle < 90f)
        {
            float angleSpeed = Time.deltaTime + rotateSpeed;
            transform.RotateAround(point, axis, angleSpeed);
            angle += angleSpeed;

            yield return null;
        }

        transform.RotateAround(point, axis, 90 - angle);
        isRolling = false;
    }
}
