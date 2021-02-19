using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] private bool _isPlayer = false;
    [SerializeField] private Transform _rotatePoint;

    private Rigidbody[] _playerRigidbody;
    private CharacterController _characterControll;
    private Animator _animator;
    private Transform _soldier;

    private Vector3 velocity;

    private void Start()
    {
        _playerRigidbody = GetComponentsInChildren<Rigidbody>();
        if (_isPlayer)
        {
            _characterControll = GetComponentInChildren<CharacterController>();
        }
        _animator = GetComponentInChildren<Animator>();
        _soldier = _animator.transform;
        SwitchStateOfRagdoll(false);
    }

    public void Move(float speed)
    {
        if (_rotatePoint.eulerAngles.y > 315 + 54 || _rotatePoint.eulerAngles.y < 45 + 54)
        {
            _soldier.rotation = Quaternion.Euler(0, 0, 0);
            #region Movement
            if (Input.GetKey(KeyCode.W))
            {
                SetBoolInAnimator("W", true);
                _characterControll.Move(Vector3.forward * speed);
            }
            else
            {
                SetBoolInAnimator("W", false);
            }

            if (Input.GetKey(KeyCode.A))
            {
                SetBoolInAnimator("A", true);
                _characterControll.Move(Vector3.left * speed);
            }
            else
            {
                SetBoolInAnimator("A", false);
            }

            if (Input.GetKey(KeyCode.S))
            {
                SetBoolInAnimator("S", true);
                _characterControll.Move(Vector3.back * speed);
            }
            else
            {
                SetBoolInAnimator("S", false);
            }

            if (Input.GetKey(KeyCode.D))
            {
                SetBoolInAnimator("D", true);
                _characterControll.Move(Vector3.right * speed);
            }
            else
            {
                SetBoolInAnimator("D", false);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                SetBoolInAnimator("Shift", true);
            }
            else
            {
                SetBoolInAnimator("Shift", false);
            }
            #endregion
        }
        else if (_rotatePoint.eulerAngles.y > 45 + 54 && _rotatePoint.eulerAngles.y < 135 + 54)
        {
            _soldier.rotation = Quaternion.Euler(0, 90, 0);
            #region Movement
            if (Input.GetKey(KeyCode.W))
            {
                SetBoolInAnimator("A", true);
                _characterControll.Move(Vector3.forward * speed);
            }
            else
            {
                SetBoolInAnimator("A", false);
            }

            if (Input.GetKey(KeyCode.A))
            {
                SetBoolInAnimator("S", true);
                _characterControll.Move(Vector3.left * speed);
            }
            else
            {
                SetBoolInAnimator("S", false);
            }

            if (Input.GetKey(KeyCode.S))
            {
                SetBoolInAnimator("D", true);
                _characterControll.Move(Vector3.back * speed);
            }
            else
            {
                SetBoolInAnimator("D", false);
            }

            if (Input.GetKey(KeyCode.D))
            {
                SetBoolInAnimator("W", true);
                _characterControll.Move(Vector3.right * speed);
            }
            else
            {
                SetBoolInAnimator("W", false);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                SetBoolInAnimator("Shift", true);
            }
            else
            {
                SetBoolInAnimator("Shift", false);
            }
            #endregion
        }
        else if (_rotatePoint.eulerAngles.y > 135 + 54 && _rotatePoint.eulerAngles.y < 225 + 54)
        {
            _soldier.rotation = Quaternion.Euler(0, 180, 0);
            #region Movement
            if (Input.GetKey(KeyCode.W))
            {
                SetBoolInAnimator("S", true);
                _characterControll.Move(Vector3.forward * speed);
            }
            else
            {
                SetBoolInAnimator("S", false);
            }

            if (Input.GetKey(KeyCode.A))
            {
                SetBoolInAnimator("D", true);
                _characterControll.Move(Vector3.left * speed);
            }
            else
            {
                SetBoolInAnimator("D", false);
            }

            if (Input.GetKey(KeyCode.S))
            {
                SetBoolInAnimator("W", true);
                _characterControll.Move(Vector3.back * speed);
            }
            else
            {
                SetBoolInAnimator("W", false);
            }

            if (Input.GetKey(KeyCode.D))
            {
                SetBoolInAnimator("A", true);
                _characterControll.Move(Vector3.right * speed);
            }
            else
            {
                SetBoolInAnimator("A", false);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                SetBoolInAnimator("Shift", true);
            }
            else
            {
                SetBoolInAnimator("Shift", false);
            }
            #endregion
        }
        else if (_rotatePoint.eulerAngles.y > 225 + 54 && _rotatePoint.eulerAngles.y < 315 + 54)
        {
            _soldier.rotation = Quaternion.Euler(0, -90, 0);
            #region Movement
            if (Input.GetKey(KeyCode.W))
            {
                SetBoolInAnimator("D", true);
                _characterControll.Move(Vector3.forward * speed);
            }
            else
            {
                SetBoolInAnimator("D", false);
            }

            if (Input.GetKey(KeyCode.A))
            {
                SetBoolInAnimator("W", true);
                _characterControll.Move(Vector3.left * speed);
            }
            else
            {
                SetBoolInAnimator("W", false);
            }

            if (Input.GetKey(KeyCode.S))
            {
                SetBoolInAnimator("A", true);
                _characterControll.Move(Vector3.back * speed);
            }
            else
            {
                SetBoolInAnimator("A", false);
            }

            if (Input.GetKey(KeyCode.D))
            {
                SetBoolInAnimator("S", true);
                _characterControll.Move(Vector3.right * speed);
            }
            else
            {
                SetBoolInAnimator("S", false);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                SetBoolInAnimator("Shift", true);
            }
            else
            {
                SetBoolInAnimator("Shift", false);
            }
            #endregion
        }
    }

    public void SwitchStateOfRagdoll(bool isRagdoll)
    {
        if (isRagdoll)
        {
            for (int i = 0; i < _playerRigidbody.Length; i++)
            {
                _playerRigidbody[i].isKinematic = false;
            }
            _animator.enabled = false;
        }
        else
        {
            for (int i = 0; i < _playerRigidbody.Length; i++)
            {
                _playerRigidbody[i].isKinematic = true;
            }
            _animator.enabled = true;
        }
    }

    public void SetBoolInAnimator(string name, bool value)
    {
        if (_animator.GetBool(name) != value)
        {
            _animator.SetBool(name, value);
        }
    }

    public void Gravity()
    {
        velocity += Physics.gravity * Time.fixedDeltaTime;
        _characterControll.Move(velocity * Time.fixedDeltaTime);
    }
}
