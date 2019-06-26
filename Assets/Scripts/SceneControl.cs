using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
{
    private bool summoning = false;
    private float waitingTime = 2f;
    private Transform droneSummonPoint;
    private Transform turretSummonPoint;
    [SerializeField] private GameObject dronePrefab;
    [SerializeField] private GameObject turretPrefab;

    private void Start()
    {
        droneSummonPoint = GameObject.Find(Constants.DroneSummoner).transform;
        turretSummonPoint = GameObject.Find(Constants.TurretSummoner).transform;
    }

    private void FixedUpdate()
    {
        SummonEnemy(dronePrefab, droneSummonPoint);
        SummonEnemy(turretPrefab, turretSummonPoint);
    }

    private void SummonEnemy(GameObject enemy, Transform enemyPosition)
    {
        if (enemyPosition.childCount == 0 && !summoning)
        {
            summoning = true;
            StartCoroutine(WaitToSummon(enemy, enemyPosition));
        }
    }

    IEnumerator WaitToSummon(GameObject enemy, Transform enemyPosition)
    {
        yield return new WaitForSecondsRealtime(waitingTime);
        Instantiate(enemy, enemyPosition);
        summoning = false;
    }
}
