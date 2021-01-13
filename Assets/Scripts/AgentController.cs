﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    public FlowField flowField;
    public GameObject agentPrefab;
    public int agentsPerSpawn;
    public float agentMoveSpeed;

    public List<GameObject> activeAgents;

    // Start is called before the first frame update
    void Start()
    {
        activeAgents = new List<GameObject>();

        // TEMP
        flowField.CalculateFlowField();
        SpawnAgents();
    }

    // Update is called once per frame
    void Update()
    {
        if (flowField == null)
        {
            return;
        }

        HandleInput();

        foreach (GameObject agent in activeAgents)
        {
            Cell agentCell = flowField.CellFromWorldPos(agent.transform.position);
            Vector3 moveDirection = agentCell.bestDirection;

            Rigidbody agentRigidBody = agent.GetComponent<Rigidbody>();
            agentRigidBody.velocity = moveDirection * agentMoveSpeed;
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SpawnAgents();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DestroyAgents();
        }
    }

    void SpawnAgents()
    {
        for(int i = 0; i < agentsPerSpawn; i++)
        {
            Cell randomCellToSpawnIn = flowField.GetRandomTraverseableCell();
            GameObject newAgent = Instantiate(agentPrefab);
            newAgent.transform.parent = transform;
            activeAgents.Add(newAgent);

            if (randomCellToSpawnIn != null)
            {
                newAgent.transform.position = randomCellToSpawnIn.worldPos;
            }
        }
    }
    void DestroyAgents()
    {
        foreach(GameObject agent in activeAgents)
        {
            Destroy(agent);
        }
        activeAgents.Clear();
    }
}
