using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
//Clase jugador
public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject controller;
    public static PlayerManager PM;
    public float moveSpeed;//Velocidad bajo la que se mueve el personaje
    public bool move, breakWalls;//Booleano que determina si pude moverse o romper paredes
    public Vector3 nextPos;//El vector que determina la siguiente posicion del usuario
    public int coordinateX, coordinateY;//Coordenadas X y Y del usuario

    void Awake()
    {
        if (PM != null)
            GameObject.Destroy(PM);
        else
            PM = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        move = false;
        breakWalls = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Inicializacion de la posicion del personaje en una celda aleatoria y libre dentro del laberinto
    public void SetRandomPosition()
    {
        while (true)
        {
            coordinateX = Random.Range(0, MazeManager.MZ.mazeRows);
            coordinateY = Random.Range(0, MazeManager.MZ.mazeColumns);
            if (MazeManager.MZ.freeCells[coordinateX, coordinateY])
            {
                MazeManager.MZ.freeCells[coordinateX, coordinateY] = false;
                break;
            }
        }
        ChangePosition();
    }

    //Metodo que cambia la posicion del usuario, adecuando las coordenadas X y Y con la posicion real de esas coordenadas
    public void ChangePosition() {
        transform.position =
                new Vector3(((-MazeManager.MZ.mazeRows / 2) + coordinateY) * MazeManager.MZ.wallSize, ((transform.localScale.y / 2) + 0.55f),
                ((MazeManager.MZ.mazeColumns / 2) - coordinateX) * MazeManager.MZ.wallSize);
    }

    void WallCollision(Collider collision) {
        if (breakWalls)
        {
            bool destroy = true;
            float stopCoordinate = (MazeManager.MZ.mazeRows / 2) * MazeManager.MZ.wallSize, offset = (MazeManager.MZ.wallSize / 2);
            if (transform.position.z >= stopCoordinate + offset - 1)
            {
                if (collision.name.Contains("NWall"))
                    destroy = false;
            }
            else if (transform.position.z <= (-stopCoordinate) + offset + 1)
            {
                if (collision.name.Contains("SWall"))
                    destroy = false;
            }

            if (transform.position.x <= -stopCoordinate - offset + 1)
            {
                if (collision.name.Contains("WWall"))
                    destroy = false;
            }
            else if (transform.position.x >= stopCoordinate - offset - 1)
            {
                if (collision.name.Contains("EWall"))
                    destroy = false;
            }

            if (destroy)
                collision.gameObject.SetActive(false);
            else {
                move = false;
                transform.position -= (nextPos * 0.2f);
            }
        }
        else {
            move = false;
            transform.position -= (nextPos * 0.2f);
        }
    }

    void StopRayo() {
        moveSpeed -= 2f;
    }

    void StopBorrador(){
        breakWalls = false;
    }
}
