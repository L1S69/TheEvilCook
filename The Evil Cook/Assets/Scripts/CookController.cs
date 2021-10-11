using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CookController : MonoBehaviour
{
    private NavMeshAgent CookNav;
    private Vector3 RNDPos;
    [SerializeField]private GameObject PlayerObject;
    [SerializeField]private Transform PlayerTransform;
    private Transform CookTransform;
    private bool IsOn;
    private RaycastHit Hit;

    void Start()
    {
        CookTransform = GetComponent<Transform>();
        CookNav = GetComponent<NavMeshAgent>();
        GoToRandomPosition();
        IsOn = true;
        InvokeRepeating("GoToRandomPosition", 0, Random.Range(1, 3));
    }

    void Update()
    {
        Ray DirectRay = new Ray(CookTransform.position, PlayerTransform.position - CookTransform.position/*CookTransform.TransformDirection(Vector3.forward)*/);
        Physics.Raycast(DirectRay, out Hit);
    }

    private void GoToRandomPosition()
    {
        if(IsOn)
        {
            RNDPos = new Vector3(Random.Range(-13, 13), this.GetComponent<Transform>().position.y, Random.Range(-13, 13));
            CookNav.SetDestination(RNDPos);
        }
    }

    public void StartChase()
    {
        if(Hit.collider != null)
        {
            if(Hit.collider.tag == "Player")
            {
                IsOn = false;
                CookNav.speed = 7f;
                CookNav.SetDestination(PlayerTransform.position);
            }
        }
    }

    public void StopChase()
    {
        CookNav.speed = 1.5f;
        IsOn = true;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            PlayerObject.GetComponent<PlayerControler>().Die();
        }
    }
}
