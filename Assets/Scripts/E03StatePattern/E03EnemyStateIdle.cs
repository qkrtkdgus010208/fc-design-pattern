using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E03EnemyStateIdle : E03IEnemyState
{
    private E03EnemyController controller;
    private Quaternion targetRotation;
    private float rotationSpeed = 2.0f;

    public E03EnemyStateIdle(E03EnemyController controller)
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

            if (angle > 1.0f)
            {
                this.controller.transform.rotation = Quaternion.Lerp(this.controller.transform.rotation,
                    targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }

    public void Exit()
    {

    }
}
