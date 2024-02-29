using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    

    public static GameManager instance {get; private set;}

    public int currentLevel;
    public bool[] levelCount;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        levelCount = new bool[15];
    }


    public void InitGame()
    {
        Debug.Log("Demarrage du jeu...");
    }

    // Si je vois un double de moi j'explose sinon je vie
    private void Awake() 
    {
        if(instance != null && instance != this)
        { 
            Destroy(gameObject); 
        }
        else
        {
            instance = this;
        }
    }
}
