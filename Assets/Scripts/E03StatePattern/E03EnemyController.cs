using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E03EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    private E03IEnemyState stateIdle;
    private E03IEnemyState statePatrol;
    private E03IEnemyState stateAttack;
    private E03IEnemyState currentState;

    private void Start()
    {
        stateIdle = new E03EnemyStateIdle(this);
        statePatrol = new E03EnemyStatePatrol(this);
        stateAttack = new E03EnemyStateAttack(this);

        changeState(stateIdle);
    }
    private void Update()
    {
        currentState.Excute();
    }

    private void changeState(E03IEnemyState newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    private void OnBecameVisible()
    {
        if (currentState == stateIdle)
        {
            changeState(statePatrol);
        }
    }

    private void OnBecameInvisible()
    {
        if (currentState != stateIdle)
        {
            changeState(stateIdle);
        }
    }

    public void FoundPlayer()
    {
        changeState(stateAttack);
    }

    public void NotFoundPlayer()
    {
        changeState(statePatrol);
    }

    public void Attack()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
}
