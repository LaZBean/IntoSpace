using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public EntityMotor motor;

    public SimpleSpriteAnimator animator;

    public float basicAnimSpeed = 0.5f;
    public Vector2 randomSpeedRange = new Vector2(0.2f, 0.8f);

    void Start()
    {
        motor.speed = Random.Range(randomSpeedRange.x, randomSpeedRange.y);
    }

    
    void Update()
    {
        if(motor.velocity.x < 0)
        {
            animator.renderer.flipX = true;
        }
        else if(motor.velocity.x > 0)
        {
            animator.renderer.flipX = false;
        }

        if (motor.isHold)
        {
            animator.gridY = 2;
        }
        else if(motor.velocity.magnitude >= 0.02f)
        {
            animator.gridY = 1;
            animator.timeMultiplier = motor.speed/basicAnimSpeed;
        }
        else
        {
            animator.gridY = 0;
        }
    }


    private void OnDestroy()
    {
        SceneManager.i.entities.Remove(this);
    }
}
