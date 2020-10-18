using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public GameObject target;

    [SerializeField] Vector3 cameraOffset = new Vector3 (0, 5, -7);

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.transform.position + cameraOffset;
    }
}
