using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{

    public Rigidbody enemyRigidBody;
    public NavMeshAgent navMeshAgent;

    public GameObject player;

    public GameObject ambientBGM;
    public GameObject conflictBGM;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        ambientBGM = GameObject.Find("Ambient");
        conflictBGM = GameObject.Find("Conflict");

    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ambientBGM.gameObject.SetActive(false);
            conflictBGM.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ambientBGM.gameObject.SetActive(true);
            conflictBGM.gameObject.SetActive(false);
        }
    }
}
