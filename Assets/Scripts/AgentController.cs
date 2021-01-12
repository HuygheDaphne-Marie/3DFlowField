using System.Collections;
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
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();

        foreach (GameObject agent in activeAgents)
        {

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
            //GameObject newAgent = agentPrefab.sp
            //activeAgents.Add()
        }
    }
    void DestroyAgents()
    {
        activeAgents.Clear();
    }
}
