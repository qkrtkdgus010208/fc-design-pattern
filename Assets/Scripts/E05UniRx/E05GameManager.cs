using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class E05GameManager : MonoBehaviour, E04IObserver<E05PlayerData>
{
    [SerializeField] Image hpBar;
    [SerializeField] Image hpBarBackground;

    private int currentLevel = 0;

    private static E05GameManager instance;
    public static E05GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<E05GameManager>();
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

    private void Start()
    {
        E05PlayerController playerController = FindObjectOfType<E05PlayerController>();
        playerController.Subscribe(this);
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

    #region Observer Pattern
    public void OnNext(E05PlayerData value)
    {
        hpBar.transform.localScale = new Vector3(value.hp / 100f, 1, 1);
    }

    public void OnError(Exception error)
    {
        Debug.Log("Error : " + error.Message);
    }

    public void OnCompleted()
    {
        hpBarBackground.enabled = false;
        hpBar.enabled = false;
    }
    #endregion
}
