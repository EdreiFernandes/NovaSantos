using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    public float fireRate;
    public float nextFire = 0;
    [SerializeField] private GameObject bullet;
    
    public void Shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }
}
