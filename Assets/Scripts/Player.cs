using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    UnityEvent PlayerDied = new UnityEvent();

    [SerializeField] private float _speedConst;
    [SerializeField] private float _boost;
    [SerializeField] private int _healthConst;
    [SerializeField] private Transform _rotatePoint;

    private Soldier _soldier;
    private Camera _camera;
    private Gun _gun;
    private float _speed;
    private int _health;
    public bool _isActive = true;

    RaycastHit hit;
    Ray ray;

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
            PlayerDied.AddListener(ai[i].PlayerDied);
        }
    }

    private void FixedUpdate()
    {
        if (_isActive)
        {
            _soldier.SwitchStateOfRagdoll(false);
            ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                _gun.Shoot(hit.point, true);
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
        }
        else
        {
            _soldier.SwitchStateOfRagdoll(true);
        }
    }

    public void Damage(int damage)
    {
        _health -= damage;
        
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
    }

    private void RotateCharacter()
    {
        Physics.Raycast(ray, out hit);
        _rotatePoint.LookAt(hit.point);
    }
}
