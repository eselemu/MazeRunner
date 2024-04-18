using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//Clase que controla el movimiento con respecto a la entrada del ususario
public class FirstPersonController : MonoBehaviour
{
    //Entero que guarda los grados para la rotacion del personaje. Desired son los grados que debe rotar, current son los grados
    //actuales del personaje, sign determina el signo del giro.
    int desiredDegrees, currentDegrees, signDegrees;
    //Flotante que guarda la velocidad en la que el personaje rota
    float speedRotation;
    //Booleano que almacena si el personaje se encuentra rotando
    bool rotating;

    void Start()
    {
        //Al iniciar el obejto se inicializan los valores base de las vars
        currentDegrees = 0;
        signDegrees = 1;
        speedRotation = 0.005f;
        rotating = false;
        //El personaje inicia por default moviendose hacia al frente
        PlayerManager.PM.nextPos = Vector3.forward;
    }

    void Update()
    {
        DirectionSlideInput();
        //If player is able to move, increase it's position considering it's nextPosition and base move speed
        if(PlayerManager.PM.move)
            transform.position += (PlayerManager.PM.nextPos * (PlayerManager.PM.moveSpeed) * Time.deltaTime);
    }
    //Function that determines the slide direction of the user and change the player's movement according the direction
    private void DirectionSlideInput()
    {
        if (!rotating && Input.anyKey) {
            Vector3 aux = PlayerManager.PM.nextPos;
            PlayerManager.PM.move = false;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                desiredDegrees = 90;
                signDegrees = 1;
                InvokeRepeating("RotatePlayer", 0, speedRotation);
                PlayerManager.PM.nextPos = new Vector3(((int)aux.x ^ 1) * aux.z, 0, ((int)aux.z ^ 1) * -aux.x);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                desiredDegrees = 90;
                signDegrees = -1;
                InvokeRepeating("RotatePlayer", 0, speedRotation);
                PlayerManager.PM.nextPos = new Vector3(((int)aux.x ^ 1) * -aux.z, 0, ((int)aux.z ^ 1) * aux.x);
            }
            else if (Input.GetKey(KeyCode.DownArrow)) {
                desiredDegrees = 180;
                signDegrees = 1;
                InvokeRepeating("RotatePlayer", 0, speedRotation);
                PlayerManager.PM.nextPos = new Vector3(aux.x * -1, 0, aux.z * -1);
            }
            else if (Input.GetKey(KeyCode.UpArrow)) {
                PlayerManager.PM.move = true;
            }
        }
    }
    //Metodo que rota al usuario de acuerdo con los grados establecidos
    void RotatePlayer() {
        transform.eulerAngles += new Vector3(0, 1 * signDegrees, 0);
        currentDegrees += 1;
        rotating = true;
        //Cuando los grados actuales son mayores o iguales a los deseados, la rotacion termina
        if (currentDegrees >= desiredDegrees)
        {
            currentDegrees = 0;
            PlayerManager.PM.move = true;
            rotating = false;
            CancelInvoke("RotatePlayer");
        }
    }
}