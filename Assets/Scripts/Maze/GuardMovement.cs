using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    private Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
        destination = new Vector3(-100000, -10000000, -100000);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x == destination.x && transform.position.z == destination.z){
            setDestination();
        }
    }

    public void setDestination()
    {
        int agentCoordinateX;
        int agentCoordinateY;
        while (true)
        {
            agentCoordinateX = Random.Range(0, MazeManager.MZ.mazeRows);
            agentCoordinateY = Random.Range(0, MazeManager.MZ.mazeColumns);
            if (MazeManager.MZ.freeCells[agentCoordinateX, agentCoordinateY])
            {
                MazeManager.MZ.freeCells[agentCoordinateX, agentCoordinateY] = false;
                break;
            }
        }
        agent.destination =
                new Vector3(((-MazeManager.MZ.mazeRows / 2) + agentCoordinateY) * MazeManager.MZ.wallSize, ((transform.localScale.y / 2) + 0.55f),
                ((MazeManager.MZ.mazeColumns / 2) - agentCoordinateX) * MazeManager.MZ.wallSize);
        print("Buenas");
    }
}
