using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    float fireRate = 0.5f;
    private float nextFire = 0.0f;
    public UnityEvent OnFire;
    public Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        if(firePoint == null)
        {
            firePoint = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttemptFire()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Fire();
        }
    }

    public void Fire()
    {
        OnFire.Invoke();
        RaycastHit hit;
        if(Physics.Raycast(firePoint.position, firePoint.forward, out hit, 100))
        {
            Debug.Log(hit.transform.name);
            IDamageable damageable = hit.transform.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.TakeDamage(10);
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(firePoint.position, firePoint.forward * 100);
    }
}
