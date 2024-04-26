using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject player;
    public float detectionDistance = 5f;
    bool detected;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.Find("Player");
        detected = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Calcula la distancia entre el jugador y el guardia
        float distanceToGuard = Vector3.Distance(player.transform.position, transform.position);

        // Si la distancia es menor que el umbral de detección, cambia el color de la cámara a rojo
        if (distanceToGuard < detectionDistance){
            mainCamera.GetComponent<Effects>().enabled = true;
            detected = true;
        }
        else if(detected){
            mainCamera.GetComponent<Effects>().enabled = false;
            detected = false;
        }
    }

    private void OnDestroy()
    {
        if(mainCamera != null)
            mainCamera.GetComponent<Effects>().enabled = false;
    }
}
