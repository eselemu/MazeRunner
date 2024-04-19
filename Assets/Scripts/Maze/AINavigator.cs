using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class AINavigator : MonoBehaviour
{
    public static AINavigator AINav;
    public NavMeshSurface surface;
    List<NavMeshAgent> agents;
    public GameObject agentPrefab;
    //private NavMeshAgent agent;
    private int agentCoordinateX, agentCoordinateY;
    private int amountGuards;
    private bool guardsMoving;
    void Awake()
    {
        if (AINav != null)
            GameObject.Destroy(AINav);
        else
            AINav = this;
    }
    //public NavMeshSurface surface;
    void Start()
    {
        agents = new List<NavMeshAgent>();
        amountGuards = 2;
        guardsMoving = false;
    }
    void Update()
    {
        if (guardsMoving) {
            for (int i = 0; i < agents.Count; i++) {
                if (agents[i].remainingDistance <= 0.001f)
                    setDestination(i);
            }
        }
    }

    public void BakeNavMesh() {
        //navMeshData = NavMeshBuilder.BuildNavMeshData(new NavMeshBuildSettings(), );
        surface.BuildNavMesh();
        Invoke("startGuards", 5f);
        /*GameObject agentObject = Instantiate(agentPrefab, Vector3.zero, Quaternion.identity);
        agent = agentObject.GetComponent<NavMeshAgent>();
        SetAgentRandomPosition();
        setDestination();*/
        //agentObject.GetComponent<GuardMovement>().setDestination();
    }

    public void startGuards() {
        for (int i = 0; i < amountGuards; i++)
        {
            GameObject agentObject = Instantiate(agentPrefab, Vector3.zero, Quaternion.identity);
            agents.Add(agentObject.GetComponent<NavMeshAgent>());
            SetAgentRandomPosition(i);
            setDestination(i);
        }
        guardsMoving = true;
    }

    public void SetAgentRandomPosition(int agentIndex)
    {
        while (true)
        {
            agentCoordinateX = Random.Range(0, MazeManager.MZ.mazeRows);
            agentCoordinateY = Random.Range(0, MazeManager.MZ.mazeColumns);
            if (MazeManager.MZ.freeCells[agentCoordinateX, agentCoordinateY])
            {
                //MazeManager.MZ.freeCells[agentCoordinateX, agentCoordinateY] = false;
                break;
            }
        }
        agents[agentIndex].transform.position =
                new Vector3(((-MazeManager.MZ.mazeRows / 2) + agentCoordinateY) * MazeManager.MZ.wallSize, ((transform.localScale.y / 2) + 0.55f),
                ((MazeManager.MZ.mazeColumns / 2) - agentCoordinateX) * MazeManager.MZ.wallSize);
    }
    public void setDestination(int agentIndex)
    {
        while (true)
        {
            agentCoordinateX = Random.Range(0, MazeManager.MZ.mazeRows);
            agentCoordinateY = Random.Range(0, MazeManager.MZ.mazeColumns);
            if (MazeManager.MZ.freeCells[agentCoordinateX, agentCoordinateY])
            {
                //MazeManager.MZ.freeCells[agentCoordinateX, agentCoordinateY] = false;
                break;
            }
        }
        agents[agentIndex].destination =
                new Vector3(((-MazeManager.MZ.mazeRows / 2) + agentCoordinateY) * MazeManager.MZ.wallSize, ((transform.localScale.y / 2) + 0.55f),
                ((MazeManager.MZ.mazeColumns / 2) - agentCoordinateX) * MazeManager.MZ.wallSize);
    }
}
