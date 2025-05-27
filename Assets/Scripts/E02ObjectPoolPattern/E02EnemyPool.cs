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

    // Enemy Pool에서 재사용 할 수 있는 Enemy 요청하는 함수
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

    // 더이상 사용하지 않는 Enemy를 Pool에 반납하는 함수
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
