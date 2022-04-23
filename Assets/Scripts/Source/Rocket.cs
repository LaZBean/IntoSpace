using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] bool _isReady = false;
    public SpriteRenderer door;
    public Vector3 door_open_pos;
    public Vector3 door_close_pos;
    public float door_openTime = 2f;

    public List<Entity> entities_inside;
    public List<Entity> entities_outside;
    public int inside_char_layer = -5;
    public Transform char_holder;
    public Transform char_out_holder;

    [SerializeField] bool _isLaunched = false;
    public bool isLaunched
    {
        get { return _isLaunched; }
        set { 
            _isLaunched = value;
            fly_timer = 0;
        }
    }

    public float fly_speed = 1f;
    float fly_timer;
    public AnimationCurve launch_speed_curve;
    public ParticleSystem truster_effect;
    public ParticleSystem smoke_effect;
    public ParticleSystem fire_effect;

    public AudioSource audio;
    public AudioClip launch_sound;

    public bool isReady
    {
        get { return _isReady; }
        set
        {
            _isReady = value;
            if (value)
            {
                OpenDoor();
            }
            else
            {
                //CloseDoor();
            }
        }
    }

    void Start()
    {
        
    }

    float bob_t;

    void Update()
    {
        if(isLaunched)
        {
            transform.position += Vector3.up * launch_speed_curve.Evaluate(fly_timer) * fly_speed * Time.deltaTime;

            fly_timer += Time.deltaTime;
        }

        if(entities_inside.Count > 45)
        {
            if(!fire_effect.isPlaying)
                fire_effect.Play();//.gameObject.SetActive(true);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, 0.2f);
            if (bob_t <= 0)
            {
                transform.eulerAngles = Vector3.forward * Random.Range(-5f, 5f);
                bob_t = 0.2f;
            }
            bob_t -= Time.deltaTime;
        }
        else
        {
            fire_effect.Stop();
        }
    }

    void AddChar(Entity e)
    {
        entities_inside.Add(e);
        SceneManager.i.entities.Remove(e);
        e.motor.rb.isKinematic = true;
        e.transform.parent = char_holder;
        e.transform.localPosition = Vector3.zero;
        e.animator.renderer.sortingOrder = inside_char_layer;
        e.motor.target = Vector3.zero;
    }

    /*private void OnTriggerEnter(Collider c)
    {
        if (!isReady) return;
        Entity e = c.GetComponent<Entity>();
        if (e)
        {
            AddChar(e);
        }
            
    }*/

    private void OnTriggerStay(Collider c)
    {
        if (!isReady) return;
        Entity e = c.GetComponent<Entity>();
        if (e)
        {
            AddChar(e);
        }
    }

    private void OnCollisionEnter(Collision c)
    {
        Entity e = c.collider.GetComponent<Entity>();
        if (e != null)
        {
            entities_outside.Add(e);
            SceneManager.i.entities.Remove(e);
            e.motor.isHold = true;
            e.transform.parent = char_out_holder;
            e.motor.rb.isKinematic = true;
        }
    }

    public void OpenDoor()
    {
        StartCoroutine(IOpenDoor());
    }

    

    IEnumerator IOpenDoor()
    {
        float t = 0;

        while (t <= 1)
        {
            t += Time.deltaTime * door_openTime;
            door.transform.localPosition = Vector3.Lerp(door_close_pos, door_open_pos, t);
            yield return null;
        }
    }

    public void CloseDoor()
    {
        StartCoroutine(ICloseDoor());
    }

    IEnumerator ICloseDoor()
    {
        float t = 0;

        while (t <= 1)
        {
            t += Time.deltaTime * door_openTime;
            door.transform.localPosition = Vector3.Lerp(door_open_pos, door_close_pos, t);
            yield return null;
        }
    }

    public void Launch()
    {
        StartCoroutine(ILaunch());
    }

    IEnumerator ILaunch()
    {
        isReady = false;
        yield return StartCoroutine(ICloseDoor());
        audio.PlayOneShot(launch_sound);
        truster_effect.Play();
        smoke_effect.Play();
        yield return new WaitForSeconds(0.5f);
        isLaunched = true;
        Destroy(gameObject, 10);
        yield return new WaitForSeconds(1f);
        smoke_effect.Stop();
        
    }
}
