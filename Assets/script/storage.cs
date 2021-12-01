using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storage : MonoBehaviour
{
    public string ID { get; set; }
    public int count { get; set; }
    public storage(string ID,int count)
    {
        this.ID = ID;
        this.count = count;
    }
}
