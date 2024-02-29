using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseInMainScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col.gameObject.tag == "PlayButton")
        {
            GameManager.instance.currentLevel += 1;
            SceneManager.LoadScene("Level1");
        }

        if(_col.gameObject.tag == "QuitButton")
        {
            Application.Quit();
        }
    }


}
