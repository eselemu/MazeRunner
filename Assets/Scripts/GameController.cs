using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController GM;
    void Awake()
    {
        if (GM != null)
            GameObject.Destroy(GM);
        else
            GM = this;
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
