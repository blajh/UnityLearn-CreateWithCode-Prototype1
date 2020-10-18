using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCounter : MonoBehaviour
{
    private int boxCount = 0;
    private int temp;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            boxCount++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        temp = 0;
        foreach (Transform child in transform)
        {
            temp++;
        }

        boxCount = temp;
    }

    public bool ObstaclesAreGone()
    {
        if (boxCount == 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    
    public void ReduceCount()
    {
        //boxCount--;
    }
    
}



