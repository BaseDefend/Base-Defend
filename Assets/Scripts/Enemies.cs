﻿using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour {
    public float speed = 10f;
    public float starthealth = 100;
    public static float health;
    public int getGold = 50;
    public int damageEnemy = 10;

    public Transform partToMove;
    public GameObject impactEffect;
    

    private Transform target;
    private int wavepointIndex = 0;

    public Image healthbar; 

    // Use this for initialization
    void Start()
    {
        target = Waypoints.points[0];
        health = starthealth;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        partToMove.transform.LookAt(target);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoints();
        }
    }

    public void TakeDamage(int amount) {
        health -= amount;

        healthbar.fillAmount = health / starthealth;

        if (health <= 0)
        {
            Dies();
            PlayerStats.Money += getGold;
        }

    }

    void GetNextWaypoints()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];

    }

    void EndPath() {
        Destroy(gameObject);
        PlayerStats.Lives -= damageEnemy;
        SpawnManager.EnemiesAlive--;
    }

    void Dies() {
        Destroy(gameObject);
        GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);
        SpawnManager.EnemiesAlive--;
    }

}
