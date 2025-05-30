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
                        createFunc: () => Instantiate(enemyPrefab),                                              // Object Pool�� ���ο� ������Ʈ�� ���� �Ǿ�� �� �� ȣ��Ǵ� �޼���
                        actionOnGet: enemy => enemy.gameObject.SetActive(true),          // Object Pool�� �ִ� ������Ʈ�� ����ϰ��� �� �� ó���ؾ� �� ������ ���� �޼���
                        actionOnRelease: enemy => enemy.gameObject.SetActive(false),     // Object Pool�� �ݳ��� ������Ʈ�� ���� ó�� ������ ���� �޼���
                        actionOnDestroy: enemy => Destroy(enemy),                        // Object Pool���� ���� ���� ������Ʈ�� �Ҹ� �Ǿ�� �� �� ó���� ���� �޼���
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

    // Enemy Pool���� ���� �� �� �ִ� Enemy ��û�ϴ� �Լ�
    public GameObject GetEnemy(Vector3 position, Quaternion rotation)
    {
        GameObject enemy = enemyPool.Get();

        enemy.transform.position = position;
        enemy.transform.rotation = rotation;

        return enemy;
    }

    // ���̻� ������� �ʴ� Enemy�� Pool�� �ݳ��ϴ� �Լ�
    public void ReturnEnemy(GameObject enemy)
    {
        enemyPool.Release(enemy);
    }

}
