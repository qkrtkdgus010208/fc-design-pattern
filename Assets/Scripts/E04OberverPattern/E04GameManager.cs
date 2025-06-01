using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class E04GameManager : MonoBehaviour, E04IObserver<E04PlayerData>
{
    [SerializeField] Image hpBar;
    [SerializeField] Image hpBarBackground;

    private int currentLevel = 0;

    private static E04GameManager instance;
    public static E04GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<E04GameManager>();
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
        E04PlayerController playerController = FindAnyObjectByType<E04PlayerController>();
        playerController.Subscribe(this);
    }

    public void UpdatePlayerPosition(Vector3 playerPosition)
    {
        //Debug.Log($"Player Position: {playerPosition}");
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
    public void OnNext(E04PlayerData value)
    {
        hpBar.transform.localScale = new Vector3(value.hp / 100f, 1, 1);
    }

    public void OnError(Exception error)
    {
        Debug.Log("Error : " + error.Message);
    }

    public void OnCompleted()
    {
        if (hpBarBackground != null)
            hpBarBackground.enabled = false;

        if (hpBar != null)
            hpBar.enabled = false;
    }
    #endregion
}
