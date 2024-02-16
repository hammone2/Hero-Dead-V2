using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public Transform patrolRoute;
    public List<Transform> locations;

    private int locationIndex = 0;
    private NavMeshAgent agent;
    private bool patroling = true;

    private int _lives = 3;
    public int EnemyLives
    {
        get { return _lives; }

        private set
        {
            _lives = value;

            if (_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy Down.");
            }
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    void Update()
    {
        if (patroling == true)
        {
            if (agent.remainingDistance < 0.2f && !agent.pathPending)
            {
                MoveToNextPatrolLocation();
            }
        }
        else
        {
            agent.destination = player.position;
        }
        
    }

    void InitializePatrolRoute()
    {
        foreach(Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (patroling == true) 
        {
            if (locations.Count == 0)
                return;

            agent.destination = locations[locationIndex].position;
            locationIndex = (locationIndex + 1) % locations.Count;
        }
        //else
        //{
            //agent.destination = player.position;
        //}
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            patroling = false;
            //agent.destination = player.position;
            Debug.Log("Player detected - attack!");

        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
        {
            patroling = true;
            Debug.Log("Player out of range, resume patrol");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            EnemyLives -= 1;
            Debug.Log("Enemy hit!");
        }
    }
}
