using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Soldier))]
public class Player : MonoBehaviour
{
    UnityEvent PlayerDied = new UnityEvent();

    [SerializeField] private float _speedConst;
    [SerializeField] private float _boost;
    [SerializeField] private int _healthConst;
    public Transform _rotatePoint;
    [SerializeField] private Material _lineMaterial;
    [SerializeField] private float _lineRadius;
    [SerializeField] private bool _drawLine;

    private Soldier _soldier;
    private Camera _camera;
    private Gun _gun;
    private float _speed;
    private int _health;
    private bool _isActive = true;

    private RaycastHit _hit;
    private Ray _ray;

    private void Start()
    {
        _soldier = GetComponent<Soldier>();
        _camera = Camera.main;
        _gun = GetComponentInChildren<Gun>();
        _speed = _speedConst;
        _health = _healthConst;
        _soldier.SwitchStateOfRagdoll(false);

        AI[] ai = FindObjectsOfType<AI>();

        for (int i = 0; i < ai.Length; i++)
        {
            Debug.Log(ai[i]);
            PlayerDied.AddListener(ai[i].PlayerDied);
        }
    }

    private void FixedUpdate()
    {
        if (_isActive)
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                _gun.Shoot(_hit.point, true);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _speed = _speedConst * _boost;
            }
            else
            {
                _speed = _speedConst;
            }

            _soldier.Move(_speed);
            RotateCharacter();
            _rotatePoint.eulerAngles += Vector3.up * 54;
        }
        _soldier.Gravity();
    }

    public void Damage(int damage)
    {
        _health -= damage;
        
        //if died
        if (_health <= 0)
        {
            _isActive = false;
            _soldier.SwitchStateOfRagdoll(true);
            PlayerDied.Invoke();
            StartCoroutine(Rebirth());
        }
    }

    private IEnumerator Rebirth()
    {
        yield return new WaitForSeconds(5);
        _health = _healthConst;
        _isActive = true;
        _soldier.SwitchStateOfRagdoll(false);
    }

    private void RotateCharacter()
    {
        Physics.Raycast(_ray, out _hit);
        _rotatePoint.LookAt(_hit.point);
    }
}
