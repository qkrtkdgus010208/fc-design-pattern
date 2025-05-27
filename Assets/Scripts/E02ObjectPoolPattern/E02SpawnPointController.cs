using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E02SpawnPointController : MonoBehaviour
{
    private Transform tr;
    private GameObject enemyObject;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        StartCoroutine(CheckAvailableSpawnPoint());
    }

    IEnumerator CheckAvailableSpawnPoint()
    {
        while (true)
        {
            // 카메라의 위치에 따라 Enemy 생성

            Vector3 viewportPosition = Camera.main.WorldToViewportPoint(tr.position);

            if (viewportPosition.y > -0.2f && viewportPosition.y < 1.5f)
            {
                // Enemy가 등장하게 되는 구간
                if (!enemyObject)
                {
                    // Enemy 생성
                    enemyObject = E02EnemyPool.Instance.GetEnemy(tr.position, tr.rotation);
                }
            }
            else
            {
                // Enemy가 등장하지 않게 되는 구간
                if (enemyObject)
                {
                    // Enemy 소멸
                    E02EnemyPool.Instance.ReturnEnemy(enemyObject);
                    enemyObject = null;
                }
            }

            yield return new WaitForSeconds(1);
        }
    }
}
