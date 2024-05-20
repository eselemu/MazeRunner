using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float velocidadRotacion;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform transformPersonaje;
    [SerializeField] private Camera camaraPersonaje;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject thirdPerson;
    [SerializeField] private GameObject firstPerson;

    private Vector3 movimiento;
    private float rotacionX;
    private bool isFirstPerson;
    private void Start()
    {
        isFirstPerson = true;
    }

    private void Update()
    {
        MovimientoDelPersonaje();
        MovimientoDeCamara();
        ChangeCamera();
    }

    void MovimientoDelPersonaje()
    {
        float movX = Input.GetAxis("Horizontal");
        float movZ = Input.GetAxis("Vertical");

        movimiento = transform.right * movX + transform.forward * movZ;
        characterController.SimpleMove(movimiento * velocidadMovimiento);
    }

    void MovimientoDeCamara()
    {
        float ratonX = Input.GetAxis("Mouse X") * velocidadRotacion;
        float ratonY = Input.GetAxis("Mouse Y") * velocidadRotacion;

        rotacionX -= ratonY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);

        camaraPersonaje.transform.localRotation = Quaternion.Euler(rotacionX, 0, 0);
        gun.transform.localRotation = Quaternion.Euler(rotacionX, 0, 0);
        transformPersonaje.Rotate(Vector3.up * ratonX);
    }
    void ChangeCamera() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            isFirstPerson = !isFirstPerson;
            if (isFirstPerson){
                camaraPersonaje.transform.position = firstPerson.transform.position;
            }
            else {
                camaraPersonaje.transform.position = thirdPerson.transform.position;
            }
        }
    }

}
