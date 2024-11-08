using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public LevelManager LevelManager { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        LevelManager = GetComponentInChildren<LevelManager>();

        if (LevelManager == null)
        {
            Debug.LogWarning("LevelManager component not found in GameManager's children.");
        }

        DontDestroyOnLoad(gameObject);

        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            DontDestroyOnLoad(mainCamera.gameObject);
        }
        else
        {
            Debug.LogWarning("Main Camera not found in the scene.");
        }
    }
}
