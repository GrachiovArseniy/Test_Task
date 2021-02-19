using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.gameObject.GetComponentInParent<Player>().Damage(_damage);
            Destroy(this.gameObject);
        }
        else if (col.CompareTag("AI"))
        {
            col.gameObject.GetComponentInParent<AI>().Damage(_damage);
            Destroy(this.gameObject);
        }
        else if (col.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
        else if (col.CompareTag("DestroyZone"))
        {
            Destroy(this.gameObject);
        }
    }
}
