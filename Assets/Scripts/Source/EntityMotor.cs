using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMotor : MonoBehaviour
{
    public Vector3 target;
    public Vector3 velocity;

    public float speed = 2f;
    public float jumpForce = 1f;

    public Rigidbody rb;

    public bool isHold;

    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        Vector3 move = Vector3.zero;

        //if(SceneManager.i.rocket != null && SceneManager.i.rocket.)

        if(target != Vector3.zero)
        {
            velocity = (target - transform.position);

            if (velocity.magnitude <= 0.1f)
            {
                target = Vector3.zero;
                velocity = Vector3.zero;
            }


        }
        else
        {
            velocity = Vector3.zero;
        }


        velocity = GameUtility.DirNormalize(velocity.normalized);
        move = velocity * speed * Time.deltaTime;
        transform.position += move;


        if (transform.position.y < -10)
            Destroy(gameObject);
    }

    public void Jump(Vector3 t, float d = 0)
    {

        
    }

    public IEnumerator IJump(Vector3 t, float d=0)
    {
        yield return new WaitForSeconds(d);
        Vector3 dir = ((t + Vector3.up * 1) - transform.position).normalized;
        rb.AddForce(dir * jumpForce * 100);

        if(Random.value > .5f)
            PlayFlush();

        target = Vector3.zero;
    }


    //
    public AudioClip[] flushs_audio;
    public AudioClip[] scream_audio;

    public void PlayScream()
    {
        AudioClip c = scream_audio[Random.Range(0, scream_audio.Length)];
        GetComponent<AudioSource>().PlayOneShot(c);
    }

    public void PlayFlush()
    {
        AudioClip c = flushs_audio[Random.Range(0, flushs_audio.Length)];
        GetComponent<AudioSource>().PlayOneShot(c);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, target);
    }
}
