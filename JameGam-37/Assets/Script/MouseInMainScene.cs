using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Feedbacks;

public class MouseInMainScene : MonoBehaviour
{
    private bool canLaunchNextScene;
    private bool canQuit;

    private bool loadingSomething;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canLaunchNextScene)
        {
            print(GameManager.instance.currentLevel);
            SceneManager.LoadScene("Level" + GameManager.instance.currentLevel);
        }

        if (canQuit)
        {
            Application.Quit();
        }
    }

    List<Transform> GetChildren(Transform parent)
    {
        List<Transform> children = new List<Transform>();
        foreach(Transform child in parent)
        {
            children.Add(child);
        }
        return children;
    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col.gameObject.tag == "PlayButton" && loadingSomething == false)
        {
            List<Transform> children = GetChildren(_col.gameObject.transform);
            StartCoroutine(cooldownAnimation(4.5f, 1));
            MMFeedbacks feedbacks = children[0].GetComponent<MMFeedbacks>();
            feedbacks.PlayFeedbacks();

            children = GetChildren(gameObject.transform);
            feedbacks = children[0].GetComponent<MMFeedbacks>();
            feedbacks.PlayFeedbacks();
        }

        if(_col.gameObject.tag == "QuitButton" && loadingSomething == false)
        {
            List<Transform> children = GetChildren(_col.gameObject.transform);
            StartCoroutine(cooldownAnimation(4.5f, 2));
            MMFeedbacks feedbacks = children[0].GetComponent<MMFeedbacks>();
            feedbacks.PlayFeedbacks();


            children = GetChildren(gameObject.transform);
            feedbacks = children[0].GetComponent<MMFeedbacks>();
            feedbacks.PlayFeedbacks();
        }



        if(_col.gameObject.tag == "Rock")
        {
            List<Transform> children = GetChildren(gameObject.transform);
            MMFeedbacks feedbacks = children[0].GetComponent<MMFeedbacks>();
            feedbacks.PlayFeedbacks();
        }
    }

    IEnumerator cooldownAnimation(float cooldown, int id)
    {
        loadingSomething = true;
        yield return new WaitForSeconds(cooldown);

        if(id == 1)
        {
            canLaunchNextScene = true;
        }
        if(id == 2)
        {
            canQuit = false;
        }
    }


}
