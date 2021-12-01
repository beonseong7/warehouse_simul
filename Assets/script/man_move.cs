using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class man_move : MonoBehaviour
{
    public worker stat;
    [SerializeField]NavMeshAgent man;
    [SerializeField] GameObject spot;
    [SerializeField] float delay = 1;
    [SerializeField] Transform A_Shoes_objective;
    [SerializeField] Transform B_Shoes_objective;
    [SerializeField] Transform C_Shoes_objective;
    [SerializeField] GameObject objective;
    public bool working = false;
    public bool isitright = false;
    public man_move(worker worker)
    {
        this.stat = worker;
    }
    // Start is called before the first frame update
    void Start()
    {
        man = this.transform.GetComponent<NavMeshAgent>();
        A_Shoes_objective = SimulManager.instance.A_Shoes_objective;
        B_Shoes_objective = SimulManager.instance.B_Shoes_objective;
        C_Shoes_objective = SimulManager.instance.C_Shoes_objective;
        spot = SimulManager.instance.belt_Spawn;
    }
    public void start_picking(string ID)
    {
        working = true;
        string[] str = this.gameObject.name.Split('_');
        Debug.Log(str.Length);
        stat = new worker(str[0], int.Parse(str[1]),int.Parse( str[2]), float.Parse(str[3]));
        if (stat.carrer > 12)
        {
            stat.carrer = 12;
        }
        switch (ID)
        {
            case "SH_001":
                objective = A_Shoes_objective.GetChild(0).gameObject;
                objective.gameObject.name = "SH_001";
                break;
            case "SH_002":
                objective = B_Shoes_objective.GetChild(0).gameObject;
                objective.gameObject.name = "SH_002";
                break;
            case "SH_003":
                objective = C_Shoes_objective.GetChild(0).gameObject;
                objective.gameObject.name = "SH_003";
                break;
            default:
                objective = C_Shoes_objective.GetChild(0).gameObject;
                objective.gameObject.name = "SH_003";
                break;

        }
        objective.transform.SetParent(SimulContorller.instance.moving_obj.transform);
        man.SetDestination(objective.transform.position);
        man.isStopped = false;
        StartCoroutine(this.calcuratin_path());
    }
    IEnumerator calcuratin_path()
    {
        if (!man.pathPending)
        {
            StartCoroutine(this.AI());
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(this.calcuratin_path());
        }
    }
    // Update is called once per frame
    IEnumerator AI()
    {
        
        if(man.velocity.sqrMagnitude >=0.25f*0.25f && man.remainingDistance <= 1.0f)
        {
            if (objective.gameObject.tag == "box")
            {
                objective.transform.SetParent(this.transform);
                objective.GetComponent<Rigidbody>().isKinematic = true;
                objective.transform.localPosition = new Vector3(0, spot.transform.childCount + 1, 0);
                objective = spot;
                man.SetDestination(objective.transform.position);
                if (stat.carrer < 3)
                {
                    man.isStopped = true;
                    yield return new WaitForSeconds(3 - stat.carrer);
                    man.isStopped = false;
                }
                StartCoroutine(this.calcuratin_path());
            }
            else if (objective.gameObject.tag == "feeder" && this.transform.childCount > 0)
            {
                transform.LookAt(objective.gameObject.transform);
                StartCoroutine(this.set_feeder());
            }
        }
        if(man.remainingDistance <=4.0f && isitright == false &&stat.carrer < 4 && objective.gameObject.tag == "box")
        {
            man.isStopped = true;
            yield return new WaitForSeconds(4 - stat.carrer);
            man.isStopped = false;
            isitright = true;
        }
        else if (stat.stamina > 100 + 8.3 * stat.carrer)
        {
            man.isStopped = true;
            yield return new WaitForSeconds(stat.age/4);
            stat.stamina = 0f;
            man.isStopped = false;
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(this.AI());
    }
    IEnumerator set_feeder() {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            var tmp = this.transform.GetChild(i).transform;
            tmp.SetParent(objective.transform);
            tmp.localPosition = new Vector3(0, 1, 0);
            tmp.rotation = Quaternion.Euler(0, 0, 0);
            yield return new WaitForSeconds(delay);
        }
        stat.stamina += 10f;
        man.isStopped=true;
        StopAllCoroutines();
        working = false;
        isitright = false;
    }
}
