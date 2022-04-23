using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager my;

    public int money;
    public int man_count;

    public PlayerInput input;

    void Awake()
    {
        my = this;
    }

    
    void Update()
    {
        
    }
}
