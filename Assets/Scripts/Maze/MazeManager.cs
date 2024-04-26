using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class MazeManager : MonoBehaviour
{
    public static MazeManager MZ;
    public int mazeRows;//Cantidad de Filas en el Laberinto
    public int mazeColumns;//Cantidad de Columnas en el Laberinto
    public float wallSize;//Tama�o de pared

    //Prefabs para instanciar durante la ejecuci�n del Juego
    public GameObject wallPrefab;
    public GameObject floor;
    public GameObject chest;

    MazeGenerator maze;//Objeto Maze, con el Laberinto ya generado

    public bool[,] freeCells;//Celdas Libres

    public NavMeshSurface surface;

    void Awake()
    {
        if (MZ != null)
            GameObject.Destroy(MZ);
        else
            MZ = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Inicializaci�n de arreglos y variables
        freeCells = new bool[mazeRows, mazeColumns];

        //Se instancia el objeto maze, generando el Laberinto
        maze = new MazeGenerator();

        InitializeFreeCells();

        //Renderizaci�n de la escena;
        RenderMaze();

        PlayerManager.PM.SetRandomPosition();

        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RenderMaze() {
        //Renderizaci�n del suelo del Laberinto
        //GameObject floor = Instantiate(floorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        floor.transform.localScale = new Vector3(mazeRows, 1, mazeColumns);
        //surface.BuildNavMesh();

        for (int r = 0; r < mazeRows; r++)
        {
            for (int c = 0; c < mazeColumns; c++)
            {
                //Posici�n de la celda con respecto a la fila y la columna
                Vector3 position = new Vector3(((-mazeRows / 2) + c) * wallSize, 0, ((mazeColumns / 2) - r) * wallSize);

                //West - Left
                //Renderizaci�n de la pared oeste si es que existe
                if (maze.cells[r, c].wExists)
                {
                    maze.cells[r, c].wWall = Instantiate(wallPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    maze.cells[r, c].wWall.transform.position = 
                        position + new Vector3(-wallSize / 2, maze.cells[r, c].wWall.transform.localScale.y / 2, 0);
                    maze.cells[r, c].wWall.transform.eulerAngles = new Vector3(0, 90, 0);
                    maze.cells[r, c].wWall.transform.localScale = new Vector3(wallSize, maze.cells[r, c].wWall.transform.localScale.y, maze.cells[r, c].wWall.transform.localScale.z);
                    maze.cells[r, c].wWall.name = "WWall " + r + ", " + c;
                }


                //East - right
                //Renderizaci�n de la pared este si es que existe
                if (maze.cells[r, c].eExists)
                {
                    maze.cells[r, c].eWall = Instantiate(wallPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    maze.cells[r, c].eWall.transform.position = 
                        position + new Vector3(wallSize / 2, maze.cells[r, c].eWall.transform.localScale.y / 2, 0);
                    maze.cells[r, c].eWall.transform.eulerAngles = new Vector3(0, 90, 0);
                    maze.cells[r, c].eWall.transform.localScale = new Vector3(wallSize, maze.cells[r, c].eWall.transform.localScale.y, maze.cells[r, c].eWall.transform.localScale.z);
                    maze.cells[r, c].eWall.name = "EWall " + r + ", " + c;
                }

                //North - up
                //Renderizaci�n de la pared norte si es que existe
                if (maze.cells[r, c].nExists)
                {
                    maze.cells[r, c].nWall = Instantiate(wallPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    maze.cells[r, c].nWall.transform.position = 
                        position + new Vector3(0, maze.cells[r, c].nWall.transform.localScale.y / 2, wallSize / 2);
                    maze.cells[r, c].nWall.transform.localScale = new Vector3(wallSize, maze.cells[r, c].nWall.transform.localScale.y, maze.cells[r, c].nWall.transform.localScale.z);
                    maze.cells[r, c].nWall.name = "NWall " + r + ", " + c;
                }

                //South - bottom
                //Renderizaci�n de la pared sur si es que existe
                if (maze.cells[r, c].sExists)
                {
                    maze.cells[r, c].sWall = Instantiate(wallPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    maze.cells[r, c].sWall.transform.position = 
                        position + new Vector3(0, maze.cells[r, c].sWall.transform.localScale.y / 2, -wallSize / 2);
                    maze.cells[r, c].sWall.transform.localScale = new Vector3(wallSize, maze.cells[r, c].sWall.transform.localScale.y, maze.cells[r, c].sWall.transform.localScale.z);
                    maze.cells[r, c].sWall.name = "SWall " + r + ", " + c;
                }
            }
        }

        AINavigator.AINav.BakeNavMesh();
        chest.transform.position = new Vector3(0, 0, 0);
    }

    void InitializeFreeCells() {
        //Inicializaci�n de todas las celdas a libres
        for (int rows = 0; rows < mazeRows; rows++) {
            for (int columns = 0; columns < mazeColumns; columns++)
                freeCells[rows, columns] = true;
        }
    }
}
