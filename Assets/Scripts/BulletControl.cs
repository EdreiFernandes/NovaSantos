using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    private Animator bulletAnimator;
    private Rigidbody2D bulletRigidBody;    
    [SerializeField] private int bulletDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float hitDuration;
    [SerializeField] private float bulletDuration;

    private void Start()
    {
        bulletRigidBody = GetComponent<Rigidbody2D>();
        bulletAnimator = GetComponent<Animator>();
        Destroy(gameObject, bulletDuration);
        bulletRigidBody.velocity = transform.right * bulletSpeed;
    } 

    public void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.tag.Equals(Constants.EnemyTag))
        {
            //Enemy Damage
            EnemyControl scriptTurret = collision.gameObject.GetComponent<EnemyControl>();
            scriptTurret.TakeDamage(bulletDamage);

            //Bullet hit Animation
            bulletRigidBody.velocity = Vector2.zero;
            bulletAnimator.SetTrigger("Hit");
            Destroy(gameObject, hitDuration);
        }
    }
}
