using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{
    public TrailRenderer trail;

    public Vector3 posA;
    public Vector3 posB;

    public bool isTouched = false;

    public float touch_timer;
    public Vector3 drag_dir;

    public int mouseButton = 0;

    public Vector3 worldPos;

    void Start()
    {
        
    }

    
    void Update()
    {
        if(Application.isMobilePlatform)
            ControlTouch();
        else
            ControlPC();
    }

    void Check()
    {
        Collider[] colls = Physics.OverlapBox(posA, new Vector3(0.2f, 0.2f, 10f));
        
        for (int i = 0; i < colls.Length; i++)
        {
            Rigidbody r = colls[i].GetComponent<Rigidbody>();
            if (r)
            {
                //r.AddForce(drag_dir * 100);
                r.velocity = drag_dir * 3;
            }
        }
    }

    void ControlTouch()
    {

    }

    void ControlPC()
    {
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos = GameUtility.MouseWorldPosOnPlane(Camera.main, Vector3.back, Input.mousePosition) ;

        if (Input.GetMouseButtonDown(mouseButton))
        {
            posA = worldPos;
            posB = worldPos;
            isTouched = true;

            ApplyPosition();
            trail.Clear();
        }
        if (isTouched)
        {
            touch_timer += Time.deltaTime;
            posB = worldPos;
        }
        if (Input.GetMouseButtonUp(mouseButton))
        {
            isTouched = false;
            drag_dir = (posB - posA);
            Check();
        }

        if (isTouched)
        {
            ApplyPosition();
        }
    }

    Vector3 GetMousePos()
    {
        return new Vector3(0, 0, 0);
    }

    void ApplyPosition()
    {
        
        trail.transform.position = worldPos;
    }
}
