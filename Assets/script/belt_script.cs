using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class belt_script : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] List<GameObject> box;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < box.Count; i++)
        {
            box[i].transform.Translate(this.transform.right*speed*Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        box.Add(collision.gameObject);
    }
    private void OnCollisionExit(Collision collision)
    {
        box.Remove(collision.gameObject);
    }
}
