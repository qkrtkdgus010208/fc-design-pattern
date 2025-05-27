using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E03EnemyStateAttack : E03IEnemyState
{
    private E03EnemyController controller;
    private Quaternion targetRotation;
    private float rotationSpeed = 10.0f;
    private float attackCoolTime = 1.0f;
    private float attackTimer = 0f;

    public E03EnemyStateAttack(E03EnemyController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {

    }

    public void Excute()
    {
        if (this.controller)
        {
            Collider[] colliders = Physics.OverlapSphere(this.controller.transform.position,
                7f, 1 << LayerMask.NameToLayer("Player"));

            if (colliders.Length > 0)
            {
                attackTimer += Time.deltaTime;

                targetRotation = Quaternion.LookRotation(colliders[0].transform.position - this.controller.transform.position);

                float angle = Quaternion.Angle(this.controller.transform.rotation, targetRotation);
                this.controller.transform.rotation = Quaternion.Lerp(this.controller.transform.rotation,
                    targetRotation, Time.deltaTime * rotationSpeed);

                if (angle < 1.0f)
                {
                    if (attackTimer > attackCoolTime)
                    {
                        this.controller.Attack();
                        attackTimer = 0f;
                    }
                }
            }
            else
            {
                this.controller.NotFoundPlayer();
            }
        }
    }

    public void Exit()
    {

    }
}
