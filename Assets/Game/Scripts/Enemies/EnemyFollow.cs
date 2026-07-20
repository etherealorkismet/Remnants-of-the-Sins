using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EnemyFollow : MonoBehaviour
{
    public GameObject player;
    EnemyStats enemyStatsSC;
    float speed;
    public float distanceThreshold = 4f;

    private float distanceBetween;
    void Start()
    {
        enemyStatsSC = GetComponent<EnemyStats>();
        player = GameObject.FindWithTag("Player");

    }
    void Update()
    {
        distanceBetween = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x)* Mathf.Rad2Deg;

        if (distanceBetween < distanceThreshold)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, enemyStatsSC.speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }
}
