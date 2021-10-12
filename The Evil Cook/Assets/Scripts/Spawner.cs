using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]private GameObject Player;
    [SerializeField]private GameObject Camera;
    [SerializeField]private GameObject Cook;
    [SerializeField]private Transform PlayerSpawn;
    [SerializeField]private Transform CookSpawn;
    [SerializeField]private Transform CameraSpawn;

    [SerializeField]private Animator CookAnim;
    
    public void Respawn()
    {
        Player.GetComponent<Transform>().position = PlayerSpawn.position;
        Camera.GetComponent<Transform>().position = CameraSpawn.position;
        Camera.GetComponent<Transform>().rotation = CameraSpawn.rotation;
        Cook.GetComponent<Transform>().position = CookSpawn.position;
        CookAnim.SetBool("IsAttacking", false);
        Player.GetComponent<PlayerControler>().Spawn();
    }
}
