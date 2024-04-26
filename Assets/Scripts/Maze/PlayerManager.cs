using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
//Clase jugador
public class PlayerManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip backgroundClip;
    public AudioClip detectedClip;

    //private GameObject controller;
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
        audioSource = GetComponent<AudioSource>();
        playBackgroundMusic();
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

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Guard")){
        }
        else if (collision.CompareTag("Chest")) {
        }
    }

    public void playBackgroundMusic() {
        audioSource.clip = backgroundClip;
    }
}
