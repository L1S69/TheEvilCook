using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewController : MonoBehaviour
{
    private CookController Cook;

    void Start()
    {
        Cook = GetComponentInParent<CookController>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            Cook.StartChase();
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if(col.tag == "Player")
        {
            Cook.StartChase();
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.tag == "Player")
        {
            Cook.StopChase();
        }
    }
}
