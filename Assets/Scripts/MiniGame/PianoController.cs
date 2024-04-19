using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PianoController : MonoBehaviour
{
    public List<GameObject> keys;
    public TextMeshProUGUI timeToPlayMessage;
    List<int> indexKeys;
    List<AudioSource> audioSources;
    //List<int> selectedIndexKeys;
    GameObject selectedKey;
    int pauseDisplay;
    int indexCounter;
    float pauseClick;
    bool playing;

    void Start()
    {
        indexCounter = 0;
        pauseDisplay = 1;
        pauseClick = 0.5f;
        playing = false;
        indexKeys = new List<int>();
        audioSources = new List<AudioSource>();
        StartCoroutine(coroutineLightRandonKey());
    }
    void Update()
    {
        if (playing)
            GetMouseObject();
    }

    void GetMouseObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                selectedKey = hit.transform.gameObject;
                int selectedIndex = int.Parse(hit.transform.name);
                if (selectedIndex == indexKeys[indexCounter])
                {
                    StartCoroutine(coroutinePaintKey(selectedKey));
                    selectedKey.GetComponent<AudioSource>().Play();
                }
                else
                    EndGame();
                indexCounter++;
                if (indexCounter >= 5)
                    playing = false;
            }
        }
    }

    void EndGame()
    {
        playing = false;
        for (int i = 0; i < keys.Count; i++)
        {
            keys[i].GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
    IEnumerator coroutinePaintKey(GameObject key)
    {
        Material prevMaterial = key.GetComponent<MeshRenderer>().material;
        Color prevColor = key.GetComponent<MeshRenderer>().material.color;
        key.GetComponent<AudioSource>().Play();
        key.GetComponent<MeshRenderer>().material.color = Color.blue;
        yield return new WaitForSeconds(pauseClick);
        key.GetComponent<MeshRenderer>().material = prevMaterial;
        key.GetComponent<MeshRenderer>().material.color = prevColor;

    }
    IEnumerator coroutineLightRandonKey()
    {
        for (int t = 0; t < 5; t++)
        {
            int randIndex = Random.Range(0, 7);
            indexKeys.Add(randIndex);
            keys[randIndex].GetComponent<AudioSource>().Play();
            Material prevMaterial = keys[randIndex].GetComponent<MeshRenderer>().material;
            Color prevColor = keys[randIndex].GetComponent<MeshRenderer>().material.color;
            keys[randIndex].GetComponent<MeshRenderer>().material.color = Color.green;
            yield return new WaitForSeconds(pauseDisplay);
            keys[randIndex].GetComponent<MeshRenderer>().material = prevMaterial;
            keys[randIndex].GetComponent<MeshRenderer>().material.color = prevColor;

        }

        timeToPlayMessage.text = "Your turn!";
        yield return new WaitForSeconds(1f);
        timeToPlayMessage.text = "";


        playing = true;
    }
}
