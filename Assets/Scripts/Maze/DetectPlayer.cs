using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject player;
    public Color redColor;
    private Color originalColor;
    public float detectionDistance = 5f;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.Find("Player");
        originalColor = mainCamera.backgroundColor;
    }

    // Update is called once per frame
    void Update()
    {
        // Calcula la distancia entre el jugador y el guardia
        float distanceToGuard = Vector3.Distance(player.transform.position, transform.position);

        // Si la distancia es menor que el umbral de detección, cambia el color de la cámara a rojo
        if (distanceToGuard < detectionDistance){
            mainCamera.backgroundColor = redColor;
            print("fjshkjsagrhkjds");
        }
        else{
            // Restaura el color original de la cámara
            mainCamera.backgroundColor = originalColor;
        }
    }
}
