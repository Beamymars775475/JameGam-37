using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class MovableOnVertical : MonoBehaviour
{

    public float maxToMove;
    private bool isAbleToSound = true;
    private bool isDragging = false;
    private Rigidbody2D rb;

    public Vector3 initialPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && (mousePosition.y > initialPosition.y-maxToMove && mousePosition.y < initialPosition.y+maxToMove))
        {
            
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {
                isDragging = true;
            }

        }

        if (isDragging && (mousePosition.y > initialPosition.y-maxToMove && mousePosition.y < initialPosition.y+maxToMove))
        {
            rb.isKinematic = true;


            transform.position = new Vector3(gameObject.transform.position.x, mousePosition.y, 0);

            if(isAbleToSound)
            {
                StartCoroutine(cooldownAnimation(0.15f));
                List<Transform> children = GetChildren(gameObject.transform);
                MMFeedbacks feedbacks = children[0].GetComponent<MMFeedbacks>();
                feedbacks.PlayFeedbacks();
            }

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDragging)
        {
            Rigidbody2D otherRb = collision.collider.GetComponent<Rigidbody2D>();
            if (otherRb != null)
            {
                Vector2 forceDirection = (otherRb.position - rb.position).normalized;
                otherRb.AddForce(forceDirection * 2f, ForceMode2D.Impulse);
            }
        }
    }

    IEnumerator cooldownAnimation(float cooldown)
    {
        isAbleToSound = false;
        yield return new WaitForSeconds(cooldown);
        isAbleToSound = true;
    }
}
