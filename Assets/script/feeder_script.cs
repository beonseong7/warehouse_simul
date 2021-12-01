using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feeder_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(this.feeding());
    }

    // Update is called once per frame
    IEnumerator feeding()
    {
        yield return new WaitForSeconds(0.5f);
        if (this.transform.childCount > 0)
        {
            for(int i = 0; i < this.transform.childCount; i++)
            {
                var tmp = this.transform.GetChild(i);
                tmp.localPosition = new Vector3(0, 1, 0);
                tmp.SetParent(null);
                tmp.GetComponent<Rigidbody>().isKinematic=false;
                yield return new WaitForSeconds(0.5f);
            }
        }
        StartCoroutine(this.feeding());
    }
}
