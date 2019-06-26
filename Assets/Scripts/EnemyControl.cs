using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private GameObject player;
    private Animator enemyAnimator;
    private bool facingRight = false;
    [SerializeField] private int enemyVitality;
    [SerializeField] private float turningDuration;
    [SerializeField] private SharedVariableFloat explosionDuration;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Constants.PlayerTag);
        enemyAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 perpedicularToPlayer = Vector2.Perpendicular(player.transform.position);
        Debug.Log(perpedicularToPlayer);
        if (perpedicularToPlayer.y > 0 && !facingRight)
        {            
            StartCoroutine(Turning());
        }
        else if (perpedicularToPlayer.y < 0 && facingRight)
        {            
            StartCoroutine(Turning());
        }
    }

    public void TakeDamage(int damage)
    {
        enemyVitality -= damage;
        if (enemyVitality <= 0) Dead();
    }

    private void Dead()
    {
        enemyAnimator.SetTrigger("Destroyed");
        Destroy(gameObject, explosionDuration.Value);
    }

    IEnumerator Turning()
    {
        facingRight = !facingRight;
        enemyAnimator.SetTrigger("Turning");
        yield return new WaitForSecondsRealtime(turningDuration);
        transform.Rotate(0f, 180f, 0f);
    }
}
