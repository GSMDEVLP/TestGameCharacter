using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    public float smoothSpeed = 0.125f;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - _player.position;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = _player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
