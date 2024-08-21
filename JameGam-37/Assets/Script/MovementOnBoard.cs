using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class MovementOnBoard : MonoBehaviour
{
    private bool isDragging = false;
    private bool isPossibleDragging = false;
    private Rigidbody2D rb;



    public Transform moveThis;
    //the layers the ray can hit
    public LayerMask hitLayers;

    public float timeOffSet;
    private Vector3 velocity;

    public float rayLength = 0.5f;
        
	Vector3 dir;
	Ray ray;
    RaycastHit hit_ray;
    public LayerMask hitLayers2;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;

        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers))
        {
            Vector3 targetPos = hit.point;

            dir = transform.TransformDirection(new Vector3(moveThis.transform.position.x - targetPos.x, 0, 0));
            
            isPossibleDragging = true;
        }
        else
        {
            isPossibleDragging = false;
        }


        if (Input.GetMouseButtonDown(0) && isPossibleDragging == true)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {
                isDragging = true;
            }
        }

        if (isDragging && isPossibleDragging) 
        {
            rb.isKinematic = true;

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;

        }

        if(Input.GetMouseButtonUp(0) && isDragging)
        {
            List<Transform> children = GetChildren(gameObject.transform);
            MMFeedbacks feedbacks = children[0].GetComponent<MMFeedbacks>();
            feedbacks.PlayFeedbacks();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            rb.bodyType = RigidbodyType2D.Static;
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

}
