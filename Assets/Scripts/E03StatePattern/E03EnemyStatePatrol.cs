using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E03EnemyStatePatrol : E03IEnemyState
{
    private E03EnemyController controller;
    private Quaternion targetRotation;
    private float rotationSpeed = 2.0f;

    public E03EnemyStatePatrol(E03EnemyController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        targetRotation = Quaternion.LookRotation(this.controller.transform.forward);
    }

    public void Excute()
    {
        if (this.controller)
        {
            float angle = Quaternion.Angle(this.controller.transform.rotation, targetRotation);
            this.controller.transform.rotation = Quaternion.Lerp(
                this.controller.transform.rotation,
                targetRotation, Time.deltaTime * rotationSpeed);

            if (angle < 1.0f)
            {
                targetRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            }

            // 주변에 Player가 있는지 확인
            Collider[] colliders = Physics.OverlapSphere(this.controller.transform.position, 
                7f, 1 << LayerMask.NameToLayer("Player"));

            if (colliders.Length > 0)
            {
                this.controller.FoundPlayer();
            }
        }
    }

    public void Exit()
    {

    }
}
