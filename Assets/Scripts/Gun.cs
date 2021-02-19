using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _aimDistance;
    [SerializeField] private Vector3 _bulletStartPosition;
    public float fixPosition;

    public void Shoot(Vector3 target, bool isPlayer)
    {
        if (isPlayer)
        {
            //Create bullet
            GameObject bullet = Instantiate(_bullet, this.transform);
            bullet.transform.localPosition = _bulletStartPosition;
            bullet.transform.parent = null;

            target += new Vector3(0, fixPosition, 0);

            //Shot
            Vector3 direction = target - bullet.transform.position;
            bullet.GetComponent<Rigidbody>().AddForce(direction.normalized * _bulletSpeed);
        }
        else
        {
            GameObject bullet = Instantiate(_bullet, this.transform);
            bullet.transform.localPosition = _bulletStartPosition;
            bullet.transform.parent = null;
            Vector3 direction = target - bullet.transform.position;
            bullet.GetComponent<Rigidbody>().AddForce(direction.normalized * _bulletSpeed);
        }
    }
}
