using System.Collections;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class result_script : MonoBehaviour
{
    int box_count = 0;
    Stopwatch watch = new Stopwatch();
    [SerializeField] Text tt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        watch.Start();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "box")
        {
            Destroy(collision.gameObject);
            box_count++;
            tt.text = "마지막 피킹 완료시간:" + watch.ElapsedMilliseconds + "ms\n"
                + "완료한 화물  수:" + box_count.ToString()+"개\n"
                +"일자:"+ DateTime.Now.ToString(("yyyy-MM-dd"));
        }
    }
}
