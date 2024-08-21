using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    //the object to move
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
    

    public Rigidbody2D rb;

    public bool NeedToGoToMouse = true;
    public bool BeShooted = true;



    private void Start()
    {
        Cursor.visible = true;
    }
    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;



        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers))
        {
            Vector3 targetPos = hit.point;

            dir = transform.TransformDirection(new Vector3(moveThis.transform.position.x - targetPos.x, 0, 0));


            ray = new Ray(moveThis.transform.position, dir * rayLength);
            Debug.DrawRay(moveThis.transform.position, ray.direction, Color.green);

            if (!Physics.Raycast(ray, out hit_ray, Mathf.Infinity, hitLayers2) && NeedToGoToMouse)
            {
                Vector3 ForceToGoToMouse = Vector3.SmoothDamp(moveThis.position, new Vector3(targetPos.x, targetPos.y, 0), ref velocity, timeOffSet); 

                moveThis.transform.position = ForceToGoToMouse; 
            }





        }
    
    }


}
