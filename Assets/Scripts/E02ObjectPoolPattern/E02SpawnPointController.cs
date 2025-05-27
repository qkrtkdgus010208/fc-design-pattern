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
            // ī�޶��� ��ġ�� ���� Enemy ����

            Vector3 viewportPosition = Camera.main.WorldToViewportPoint(tr.position);

            if (viewportPosition.y > -0.2f && viewportPosition.y < 1.5f)
            {
                // Enemy�� �����ϰ� �Ǵ� ����
                if (!enemyObject)
                {
                    // Enemy ����
                    enemyObject = E02EnemyPool.Instance.GetEnemy(tr.position, tr.rotation);
                }
            }
            else
            {
                // Enemy�� �������� �ʰ� �Ǵ� ����
                if (enemyObject)
                {
                    // Enemy �Ҹ�
                    E02EnemyPool.Instance.ReturnEnemy(enemyObject);
                    enemyObject = null;
                }
            }

            yield return new WaitForSeconds(1);
        }
    }
}
