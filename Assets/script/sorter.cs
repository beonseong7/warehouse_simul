using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sorter : MonoBehaviour
{
    [SerializeField] Transform A_feeder;
    [SerializeField] Transform B_feeder;
    [SerializeField] Transform C_feeder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.name)
        {
            case "SH_001":
                collision.transform.SetParent(A_feeder);
                collision.transform.localPosition= new Vector3(0, A_feeder.childCount+1, 0);
                break;
            case "SH_002":
                collision.transform.SetParent(B_feeder);
                collision.transform.localPosition = new Vector3(0, B_feeder.childCount + 1, 0);
                break;
            case "SH_003":
                collision.transform.SetParent(C_feeder);
                collision.transform.localPosition = new Vector3(0, C_feeder.childCount + 1, 0);
                break;
            default:
                Debug.Log("Error");
                break;
        }

    }
}
