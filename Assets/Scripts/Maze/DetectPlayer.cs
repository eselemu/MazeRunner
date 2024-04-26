using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject player;
    private float detectionDistance = 20f;
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
            PlayerManager.PM.playDetectedMusic();
        }
        else if(detected){
            mainCamera.GetComponent<Effects>().enabled = false;
            detected = false;
            PlayerManager.PM.playBackgroundMusic();
        }
    }

    private void OnDestroy()
    {
        if (mainCamera != null){
            mainCamera.GetComponent<Effects>().enabled = false;
            PlayerManager.PM.playBackgroundMusic();
        }
    }
}
