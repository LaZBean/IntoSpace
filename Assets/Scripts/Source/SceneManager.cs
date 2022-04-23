using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager i;

    public Rocket rocket;

    public Transform rocket_start_transform;
    public GameObject rocket_prefab;
    public float spawnTime = 0.5f;


    public Transform people_target_transform;
    public List<Entity> entities;
    public GameObject[] people_prefabs;
    public float timeToNextSpawn = 1f;
    float spawn_t;
    public int max_chars = 100;

    public float rocket_spawn_timer = 0;


    public int curStage = 0;
    public int[] peopleForStage;
    public float[] peopleSpawnRates;


    private void Awake()
    {
        i = this;
    }

    void Start()
    {
        StartStage();
    }

    public void StartStage()
    {
        
        UIEventPanel.i.EventBriefing((curStage+1)+" \nSend " + peopleForStage[curStage] + " people");
        
        
    }

    public void NewStage()
    {
        curStage++;
        StartStage();
    }


    void Update()
    {
        if (!GameManager.i.isPlaying) return;

        spawn_t -= Time.deltaTime;
        if(spawn_t <= 0)
        {
            SpawnRandomChar();
            spawn_t = peopleSpawnRates[curStage];
        }

        rocket_spawn_timer -= Time.deltaTime;
        if (rocket_spawn_timer <= 0 && rocket == null)
        {
            SpawnRocket();
            rocket_spawn_timer = 1f;
        }
            
    }

    public void CalculateLaunch()
    {
        float d = UILaunchIndicator.i.GetDeviationValue();

        int a = (rocket.entities_inside.Count * 10) - 100;
        int b = 0;
        if (d > .1f)
            b = Mathf.RoundToInt(100 * d);

        print((a - b) + "$ got");
        PlayerManager.my.money += (a - b);

        PlayerManager.my.man_count += rocket.entities_inside.Count;

        

        CheckWinLose();

        rocket_spawn_timer = 3f;
        rocket = null;
    }

    void CheckWinLose()
    {
        if (rocket.entities_inside.Count > 45)
        {
            UIEventPanel.i.EventFail("Rocket crashed");
        }
        else if (PlayerManager.my.money <= 0)
        {
            UIEventPanel.i.EventFail("Money is over");
        }
        else if(PlayerManager.my.man_count >= peopleForStage[curStage])
        {
            UIEventPanel.i.EventWin((curStage+1) + "");
        }
    }

    

    public void SpawnRandomChar()
    {
        GameObject pref = people_prefabs[Random.Range(0, people_prefabs.Length)];
        Vector3 spawn_pos = new Vector3(
            3 * ((Random.value > .5f)? 1f : -1f),
            0.5f,
            Random.value * -1f);

        GameObject g = (GameObject)Instantiate(pref, spawn_pos, Quaternion.identity);
        Entity e = g.GetComponent<Entity>();
        e.motor.target = people_target_transform.position + new Vector3(0, 0, 0f);
        entities.Add(e);
    }

    public void SpawnRocket()
    {
        GameObject g = (GameObject)Instantiate(rocket_prefab, rocket_start_transform.position + new Vector3(0,-2, 0), Quaternion.identity);
        Rocket r = g.GetComponent<Rocket>();
        rocket = r;

        StartCoroutine(ISpawnRocket());
    }

    IEnumerator ISpawnRocket()
    {
        float t = 0;
        Vector3 sp = rocket.transform.position;

        while (t<=1)
        {
            t += Time.deltaTime * spawnTime;
            rocket.transform.position = Vector3.Lerp(sp, rocket_start_transform.position, t);
            yield return null;
        }
        
        yield return new WaitForSeconds(1f);
        rocket.isReady = true;
    }

    public void Launch()
    {
        if(rocket != null && rocket.isReady)
        {
            StartCoroutine(ILaunch());
            UILaunchIndicator.i.Highlight();
        }
        
    }

    IEnumerator ILaunch()
    {

        //yield return new WaitForSeconds(0f);

        for (int i = 0; i < entities.Count; i++)
        {
            Vector3 dir = (entities[i].motor.target - entities[i].transform.position);
            if (dir.magnitude < 1f && Random.value >= 0.5f) {
                StartCoroutine( entities[i].motor.IJump(rocket.transform.position, Random.value*3f));
            }
            else
            {
                //entities[i].motor.target = Vector3.zero;
            }
               
        }

        rocket.Launch();
        
        yield return new WaitForSeconds(3f);
        CalculateLaunch();
        //SpawnRocket();

        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].motor.target = people_target_transform.position;
        }
    }
}
