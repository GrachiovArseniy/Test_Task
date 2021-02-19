using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Soldier))]
public class AI : MonoBehaviour
{
    [SerializeField] private float _speed;
    [Tooltip("Time between shots in seconds")]
    [SerializeField] private float _cooldownConst;
    [SerializeField] private float _shootingDistance;
    [SerializeField] private int _healthConst;

    private NavMeshAgent _agent;
    private Soldier _soldier;
    private Transform _player;
    private Gun _gun;
    private float _cooldown;
    private int _health;
    bool _isActive = true;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _soldier = GetComponent<Soldier>();
        _player = FindObjectOfType<Player>().transform;
        _gun = GetComponentInChildren<Gun>();
        _cooldown = _cooldownConst;
        _health = _healthConst;

        _agent.speed = _speed;
        _soldier.SwitchStateOfRagdoll(false);
    }

    private void FixedUpdate()
    {
        if (_isActive)
        {
            if ((_agent.transform.position - _player.position).magnitude > _shootingDistance)
            {
                if (_agent.isStopped)
                {
                    _agent.isStopped = false;
                }

                _soldier.SetBoolInAnimator("W", true);
                _soldier.SetBoolInAnimator("Shift", true);

                _agent.SetDestination(_player.position);
            }
            else
            {
                if (!_agent.isStopped)
                {
                    _agent.isStopped = true;
                }

                _soldier.SetBoolInAnimator("W", false);
                _soldier.SetBoolInAnimator("Shift", false);

                if (_cooldown <= 0)
                {
                    _gun.Shoot(new Vector3(_player.position.x, _player.position.y + 1, _player.position.z), false);
                }

                _cooldown = _cooldown <= 0 ? _cooldownConst : _cooldown - Time.fixedDeltaTime;
            }

            Rotate();
        }
    }

    private void Rotate()
    {
        _agent.transform.LookAt(_player);
    }

    public void Damage(int damage)
    {
        _health -= damage;

        //if died
        if (_health <= 0)
        {
            _isActive = false;
            _agent.isStopped = true;
            _soldier.SwitchStateOfRagdoll(true);
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

    public void PlayerDied()
    {
        _cooldown = 6.5f;
    }
}
