using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject player;
    public GameObject gunOut;
    private float shootingRange;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start(){
        transform.position = player.transform.position;
        shootingRange = 200f;
    }

    // Update is called once per frame
    void Update()
    {
        HandleShot();
    }

    void HandleShot() {
        if (Input.GetMouseButtonDown(0)) {
            audioSource.Play();
            RaycastHit hit;
            if (Physics.Raycast(gunOut.transform.position, transform.forward, out hit, shootingRange)) {
                //print(hit.transform.gameObject);
                if (hit.transform.gameObject.CompareTag("Guard"))
                    Destroy(hit.transform.gameObject);
            }
        }
    }
}
