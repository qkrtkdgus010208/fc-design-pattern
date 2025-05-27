using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E02EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int maxEnemy = 3;

    private Queue<GameObject> enemyPool;

    private static E02EnemyPool instance;
    public static E02EnemyPool Instance
    {
        get
        {
            if (instance == null)
                instance = FindAnyObjectByType<E02EnemyPool>();
            return instance;
        }
    }

    private void Awake()
    {
        if (!instance)
        {
            enemyPool = new Queue<GameObject>();
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Enemy Pool���� ���� �� �� �ִ� Enemy ��û�ϴ� �Լ�
    public GameObject GetEnemy(Vector3 position, Quaternion rotation)
    {
        GameObject enemy;

        if (enemyPool.Count > 0)
        {
            enemy = enemyPool.Dequeue();
        }
        else
        {
            enemy = Instantiate(enemyPrefab);
        }

        enemy.transform.position = position;
        enemy.transform.rotation = rotation;
        enemy.SetActive(true);

        return enemy;
    }

    // ���̻� ������� �ʴ� Enemy�� Pool�� �ݳ��ϴ� �Լ�
    public void ReturnEnemy(GameObject enemy)
    {
        if (enemyPool.Count < maxEnemy)
        {
            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
        }
        else
        {
            Destroy(enemy);
        }
    }

}
