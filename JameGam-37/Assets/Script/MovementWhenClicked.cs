using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementWhenClicked : MonoBehaviour
{
    private bool isDragging = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {
                isDragging = true;
            }
        }

        if (isDragging)
        {
            // Désactive temporairement les effets physiques sur l'objet pendant le déplacement
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            // Réactive les effets physiques sur l'objet
            rb.isKinematic = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDragging)
        {
            Rigidbody2D otherRb = collision.collider.GetComponent<Rigidbody2D>();
            if (otherRb != null)
            {
                // Ajustez la force appliquée pendant la collision
                Vector2 forceDirection = (otherRb.position - rb.position).normalized;
                otherRb.AddForce(forceDirection * 2f, ForceMode2D.Impulse);
            }
        }
    }
}
