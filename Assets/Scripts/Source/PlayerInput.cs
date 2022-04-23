using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Touch[] touches;

    void Start()
    {
        
    }

    
    void Update()
    {
        




        if (Input.GetKey(KeyCode.X))
        {
            UIManager.i.middle_button.Press();
        }
        if (Input.GetKey(KeyCode.Z))
        {
            UIManager.i.left_button.Press();
        }
        if (Input.GetKey(KeyCode.C))
        {
            UIManager.i.right_button.Press();
        }



        if (Input.GetKeyUp(KeyCode.X))
        {
            UIManager.i.middle_button.UnPress();
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            UIManager.i.left_button.UnPress();
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            UIManager.i.right_button.UnPress();
        }
    }


    public void Launch()
    {
        SceneManager.i.Launch();
    }
}
