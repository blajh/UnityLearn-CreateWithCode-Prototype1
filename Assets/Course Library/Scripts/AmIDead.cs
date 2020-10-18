using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmIDead : MonoBehaviour
{

    public GameObject parent;
    private BoxCounter boxCounter;

    // Start is called before the first frame update
    void Start()
    {
        boxCounter = parent.GetComponent<BoxCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -1)
        {
            boxCounter.ReduceCount();
        }
    }
}
