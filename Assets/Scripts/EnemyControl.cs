using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private GameObject player;
    private float lastTurn = 0f;
    private Animator enemyAnimator;      
    private bool facingRight = false;
    [SerializeField] private float timeToTurn;
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
        TurningTimer();
    }

    public void TakeDamage(int damage)
    {
        enemyVitality -= damage;
        if (enemyVitality <= 0) Dead();        
    }

    private void Dead()
    {
        enemyAnimator.SetTrigger(Constants.DestroyedTrigger);
        Destroy(gameObject, explosionDuration.Value);
    }

    private void TurningTimer()
    {
        float currentTime = Time.time;
        if (lastTurn + timeToTurn < currentTime)
        {
            StartCoroutine(Turning());
            lastTurn = currentTime;
        }
    }

    IEnumerator Turning()
    {
        facingRight = !facingRight;
        enemyAnimator.SetTrigger(Constants.TurningTrigger);
        yield return new WaitForSecondsRealtime(turningDuration);
        transform.Rotate(0f, 180f, 0f); //flip
    }
}
