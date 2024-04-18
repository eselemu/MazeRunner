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
                    StartCoroutine(coroutinePaintKey(selectedKey));
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
        key.GetComponent<MeshRenderer>().material.color = Color.blue;
        yield return new WaitForSeconds(pauseClick);
        key.GetComponent<MeshRenderer>().material.color = Color.white;
    }
    IEnumerator coroutineLightRandonKey()
    {
        for (int t = 0; t < 5; t++)
        {
            int randIndex = Random.Range(0, 7);
            indexKeys.Add(randIndex);
            keys[randIndex].GetComponent<MeshRenderer>().material.color = Color.green;
            yield return new WaitForSeconds(pauseDisplay);
            keys[randIndex].GetComponent<MeshRenderer>().material.color = Color.white;
        }
        timeToPlayMessage.text = "Your turn!";
        yield return new WaitForSeconds(1f);
        timeToPlayMessage.text = "";


        playing = true;
    }
}
