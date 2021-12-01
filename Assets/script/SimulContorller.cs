using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SimulContorller : MonoBehaviour
{
    public static SimulContorller instance = null;
    public List<GameObject> workers_obj;
    public Queue<string> storage_obj = new Queue<string>();
    public GameObject spawn_spot;
    public Queue<IEnumerator> del_que = new Queue<IEnumerator>();
    public GameObject moving_obj;
    public GameObject button;
    public void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void stop()
    {
        Time.timeScale = 0;
    }
    IEnumerator check_worker_count()
    {
        if (storage_obj.Count > 0)
        {
            for (int i = 0; i < workers_obj.Count; i++)
            {
                if (workers_obj[i].activeSelf== false)
                {
                    workers_obj[i].SetActive(true);
                    yield return new WaitForSeconds(1f);
                }
                if (workers_obj[i].GetComponent<man_move>().working == false)
                {
                    if (del_que.Count > 0)
                    {
                        while (del_que.Count > 0)
                        {
                            yield return StartCoroutine(del_que.Dequeue());
                        }
                        break;
                    }
                    workers_obj[i].GetComponent<man_move>().start_picking(storage_obj.Dequeue());
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(this.check_worker_count());
    }
    void Start()
    {
        StartCoroutine(this.check_worker_count());
    }
    public void release_time(){
        
        Time.timeScale = SimulManager.instance.fast;

    }
    public void controll_time()
    {
        switch (SimulManager.instance.fast)
        {
            case 1.0f:
                SimulManager.instance.fast = 2.0f;
                break;
            case 2.0f:
                SimulManager.instance.fast = 3.0f;
                break;
            default:
                SimulManager.instance.fast = 1.0f;
                break;
        }
        if (Time.timeScale != 0f)
        {
            Time.timeScale = SimulManager.instance.fast;
        }
        button.transform.GetChild(0).GetComponent<Text>().text = "X" + ((int)SimulManager.instance.fast).ToString();
    }

    public IEnumerator con_worker_obj(string ID)
    {
        for(int i = 0; i < workers_obj.Count; i++)
        {
            if (workers_obj[i].name.Equals(ID))
            {
                Destroy(workers_obj[i].gameObject);
                yield return null;
            }
        }
    }
    public IEnumerator con_storage_obj(string name)
    {
        storage_obj.Enqueue(name);
        yield return null;
    }

    
}
