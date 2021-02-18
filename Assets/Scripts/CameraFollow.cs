using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _maxPosition;
    [SerializeField] private Vector3 _cameraPosition;
    private Transform _player;
    private Transform _camera;

    private void Start()
    {
        _player = FindObjectOfType<Player>().transform;
        _camera = GetComponentInChildren<Camera>().transform;
    }

    private void FixedUpdate()
    {
        transform.position = _player.position + _cameraPosition;

        Vector2 delta = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0);
        _camera.position = new Vector3(delta.x * _maxPosition, transform.position.y, delta.y * _maxPosition)
                                + new Vector3(transform.position.x, 0, transform.position.z);
    }
}
