using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Vector3 _bulletStartPosition;

    public void Shoot(Vector3 target, bool isPlayer)
    {
        GameObject bullet = Instantiate(_bullet, this.transform);
        bullet.transform.localPosition = _bulletStartPosition;
        bullet.transform.parent = null;
        Vector3 direction = target - bullet.transform.position;
        bullet.GetComponent<Rigidbody>().AddForce(direction.normalized * _bulletSpeed);
    }
}
