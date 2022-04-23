using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampline : MonoBehaviour
{
    public Vector3 dir = new Vector3(0.5f, 0.5f, 0);
    public float force = 10;

    public SpriteRenderer renderer;
    public bool isPush = false;
    public Collider coll;
    float t;

    public Sprite normal;
    public Sprite active;

    public AudioClip activate_sound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (t <= 0)
        {
            coll.enabled = false;
            renderer.sprite = normal;
        }
        t -= Time.deltaTime;
    }

    public void Activate()
    {
        coll.enabled = true;
        renderer.sprite = active;
        t = 0.1f;

        GetComponent<AudioSource>().PlayOneShot(activate_sound);
    }

    private void OnTriggerEnter(Collider other)
    {
        Entity e = other.GetComponent<Entity>();
        if (e)
        {
            if (Random.value > .5f)
                e.motor.PlayFlush();
            if (Random.value > .5f)
                e.motor.PlayScream();
            e.motor.rb.velocity = dir * force;
        }
    }
}
