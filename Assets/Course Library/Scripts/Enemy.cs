using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float Speed;
    private Rigidbody rbEnemy;
    private GameObject player;
    void Start()
    {
        Speed= 1.0f;
        rbEnemy = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }


    void Update()
    {
        if (player!= null)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            rbEnemy.AddForce(lookDirection*Speed);
        }

        if (transform.position.y < -10f) Destroy(gameObject);
    }
}
