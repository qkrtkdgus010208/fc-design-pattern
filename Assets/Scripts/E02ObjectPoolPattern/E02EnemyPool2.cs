using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class E02EnemyPool2 : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int minEnemy = 3;
    [SerializeField] private int maxEnemy = 10;

    private IObjectPool<GameObject> enemyPool;

    private static E02EnemyPool2 instance;
    public static E02EnemyPool2 Instance
    {
        get
        {
            if (instance == null)
                instance = FindAnyObjectByType<E02EnemyPool2>();
            return instance;
        }
    }

    private void Awake()
    {
        if (!instance)
        {
            if (enemyPool == null)
            {
                enemyPool = new ObjectPool<GameObject>(
                        createFunc: () => Instantiate(enemyPrefab),                                              // Object Pool에 새로운 오브젝트가 생성 되어야 할 때 호출되는 메서드
                        actionOnGet: enemy => enemy.gameObject.SetActive(true),          // Object Pool에 있는 오브젝트를 사용하고자 할 때 처리해야 할 내용을 담을 메서드
                        actionOnRelease: enemy => enemy.gameObject.SetActive(false),     // Object Pool에 반납할 오브젝트에 대한 처리 내용을 담을 메서드
                        actionOnDestroy: enemy => Destroy(enemy),                        // Object Pool에서 관리 중인 오브젝트가 소멸 되어야 할 때 처리를 담을 메서드
                        true,
                        minEnemy,
                        maxEnemy);
            }

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
        GameObject enemy = enemyPool.Get();

        enemy.transform.position = position;
        enemy.transform.rotation = rotation;

        return enemy;
    }

    // 더이상 사용하지 않는 Enemy를 Pool에 반납하는 함수
    public void ReturnEnemy(GameObject enemy)
    {
        enemyPool.Release(enemy);
    }

}
