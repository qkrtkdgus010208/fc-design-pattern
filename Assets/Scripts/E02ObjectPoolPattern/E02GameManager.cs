using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class E02GameManager : MonoBehaviour
{
    private int currentLevel = 0;

    private static E02GameManager instance;
    public static E02GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<E02GameManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdatePlayerPosition(Vector3 playerPosition)
    {
        Debug.Log($"Player Position: {playerPosition}");
    }

    public void LoadNextLevel()
    {
        if (currentLevel > 0)
        {
            currentLevel = 0;
        }
        else
        {
            currentLevel = 1;
        }

        SceneManager.LoadScene(currentLevel);
    }
}
