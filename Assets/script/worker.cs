using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worker : MonoBehaviour
{
    public string name { get; set; }
    public int carrer { get;set; }
    public int age { get; set; }
    public float stamina { get; set; }
    public worker(string name,int age, int carrer, float stamina)
    {
        this.name = name;
        this.carrer = carrer;
        this.age = age;
        this.stamina = stamina;
    }

}
