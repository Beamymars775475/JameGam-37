using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Feedbacks;

public class MouseInteractionInLevels : MonoBehaviour
{
    private bool canLaunchNextScene;
    private bool loadingSomething;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (canLaunchNextScene)
        {
            GameManager.instance.currentLevel +=1;
            SceneManager.LoadScene("Level" + GameManager.instance.currentLevel);
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
        if(_col.gameObject.tag == "WinFlag" && loadingSomething == false)
        {
            List<Transform> children = GetChildren(_col.gameObject.transform);
            StartCoroutine(cooldownAnimation(4.5f));
            MMFeedbacks feedbacks = children[0].GetComponent<MMFeedbacks>();
            feedbacks.PlayFeedbacks();
        }
        if(_col.gameObject.tag == "Rock" || _col.gameObject.tag == "WinFlag")
        {
            List<Transform> children = GetChildren(gameObject.transform);
            MMFeedbacks feedbacks = children[0].GetComponent<MMFeedbacks>();
            feedbacks.PlayFeedbacks();
        }
    }

    IEnumerator cooldownAnimation(float cooldown)
    {
        loadingSomething = true;
        yield return new WaitForSeconds(cooldown);
        canLaunchNextScene = true;
    }

}
