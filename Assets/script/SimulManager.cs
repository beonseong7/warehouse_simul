using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
public class SimulManager : MonoBehaviour
{
    public static SimulManager instance =null;
    public List<worker> workers { get; private set; }
    public List<storage> storages { get; private set; }
    public GameObject Contents;
    public Button Contents_Btn;
    public List<InputField> worker_stat = new List<InputField>();
    public Dropdown storage_kind;
    public InputField storage_Count;
    public GameObject Storage_Contents;
    public GameObject spawn_spot;
    public GameObject simul_obj;
    public float fast = 1.0f;
    public Transform A_Shoes_objective;
    public Transform B_Shoes_objective;
    public Transform C_Shoes_objective;
    public GameObject belt_Spawn;
    public Text result_Text;
    public void Awake()
    {
        if(null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void Start()
    {
        workers = new List<worker>();
        storages = new List<storage>();
        FileStream test = new FileStream("D:/bsroom/unityproject/ware_hose/Assets/Resources/Data/Storage.txt", FileMode.OpenOrCreate);
        StreamReader All_StorageText = new StreamReader(test);
        string line;

       while((line=All_StorageText.ReadLine())!=null)
        {
            Debug.Log(line);
            Dropdown.OptionData op = new Dropdown.OptionData();
            op.text = line;
            storage_kind.options.Add(op);
        }
        All_StorageText.Close();
        StartCoroutine(this.check_storage());
    }
    public void AddStorage()
    {
        if (storage_kind.captionText.text.ToString() != "" && storage_Count.text.ToString() !="")
        {
            for (int j = 0; j < int.Parse(storage_Count.text.ToString()); j++)
            {
                Button tmp = Instantiate(Contents_Btn);
                tmp.transform.SetParent(Storage_Contents.transform);
                tmp.name = storage_kind.captionText.text.ToString();
                tmp.transform.GetChild(0).GetComponent<Text>().text = storage_kind.captionText.text.ToString();
                tmp.onClick.AddListener(() => { view_storage(tmp.gameObject.name.ToString()); });
            }
            for(int i = 0; i < storages.Count; i++)
            {
                if (storages[i].ID.Equals(storage_kind.captionText.text.ToString()))
                {
                    storages[i].count += int.Parse(storage_Count.text.ToString());
                    return;
                }
            }
            storages.Add(new storage(storage_kind.captionText.text.ToString(), int.Parse(storage_Count.text.ToString())));
        }
       
    }
    IEnumerator check_storage()
    {
        yield return SimulContorller.instance.storage_obj.Count < 1;
        for (int i = 0; i < storages.Count; i++)
        {
            if (SimulContorller.instance.storage_obj.Count < 1 && storages[i].count >0)
            {
                Debug.Log(storages.Count);
                yield return SimulContorller.instance.con_storage_obj(storages[i].ID);
                yield return StartCoroutine(DelStorage(storages[i].ID));
            }
        }
        StartCoroutine(this.check_storage());
    }
    public void view_storage(string tmp)
    {
        for(int i = 0; i < storage_kind.options.Count; i++)
        {
            if (storage_kind.options[i].text.ToString().Equals(tmp))
            {
                storage_kind.value=i;
                storage_Count.text = "1";
            }
        }
        
    }
    public void Save_Data()
    {
        if (result_Text.text != "")
        {
            string path = "Assets/Resources/Data/Record.txt";
            StreamWriter sw;
            if (File.Exists(path) == false)
            {
                sw = new StreamWriter(path);
            }
            else
            {
                FileStream test = new FileStream(path, FileMode.Append);
                sw = new StreamWriter(test);
            }
            sw.WriteLine(result_Text.text.ToString() + "\n");
            sw.Close();
        }
    }
    public void Reload()
    {
        SceneManager.LoadScene(0);
    }
    public void Exit_Program()
    {
        Application.Quit();
    }
    public void AddWorker()
    {
        for(int i = 0; i < worker_stat.Count; i++)
        {
            if (worker_stat[i].text == "")
            {
                Debug.Log("Error");
                return;
            }
        }
        workers.Add(new worker(worker_stat[0].text.ToString(), int.Parse(worker_stat[1].text), int.Parse(worker_stat[2].text), float.Parse(worker_stat[3].text)));
        Button tmp= Instantiate(Contents_Btn);
        tmp.transform.SetParent(Contents.transform);
        tmp.name = worker_stat[0].text;
        tmp.transform.GetChild(0).GetComponent<Text>().text = worker_stat[0].text;
        tmp.onClick.AddListener(() => { view_worker(tmp.gameObject.name.ToString()); });
        GameObject G_tmp = Instantiate(simul_obj);
        G_tmp.gameObject.name = worker_stat[0].text.ToString() + "_" + worker_stat[1].text.ToString() + "_" + worker_stat[2].text.ToString() + "_" + worker_stat[3].text.ToString();
        G_tmp.transform.SetParent(spawn_spot.transform);
        G_tmp.transform.localPosition = Vector3.zero;
        SimulContorller.instance.workers_obj.Add(G_tmp);
    }

    public void view_worker(string name)
    {
        Debug.Log("WTF");
        for(int i = 0; i < workers.Count; i++)
        {
            if (workers[i].name.Equals(name))
            {
                worker_stat[0].text = workers[i].name;
                worker_stat[1].text = workers[i].age.ToString();
                worker_stat[2].text = workers[i].carrer.ToString();
                worker_stat[3].text = workers[i].stamina.ToString();
                return;
            }
        }
    }
    public void DelStorage()
    {
        Destroy(Storage_Contents.transform.Find(storage_kind.captionText.text.ToString()).gameObject);
        for(int i = 0; i < storages.Count; i++)
        {
            if (storages[i].ID.Equals(storage_kind.captionText.text.ToString()))
            {
                storages[i].count -= 1;
                Debug.Log(storages[i].count);
                return;
            }
        }
    }
    public IEnumerator DelStorage(string tmp)
    {
        Destroy(Storage_Contents.transform.Find(tmp).gameObject);
        for (int i = 0; i < storages.Count; i++)
        {
            if (storages[i].ID.Equals(tmp))
            {
                storages[i].count -= 1;
                yield return null;
            }
        }
    }
    public void DelWorker()
    {
        worker tmp = new worker(worker_stat[0].text.ToString(), int.Parse(worker_stat[1].text), int.Parse(worker_stat[2].text), float.Parse(worker_stat[3].text));
        workers.Remove(tmp);
        Destroy(Contents.transform.Find(worker_stat[0].text.ToString()).gameObject);
        SimulContorller.instance.del_que.Enqueue(SimulContorller.instance.con_worker_obj(tmp.name));

    }
}
