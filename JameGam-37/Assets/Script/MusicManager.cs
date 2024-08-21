using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance {get; private set;}


    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();


        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "StartScene")
        {
            audioSource.volume = 0;
        }
        else
        {
            audioSource.volume = 1;
        }
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