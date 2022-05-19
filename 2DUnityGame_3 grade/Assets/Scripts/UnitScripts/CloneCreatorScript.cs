using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneCreatorScript : MonoBehaviour
{
    public void GetClone(Vector3 pos, Transform parent,  Quaternion rotation)
    {
        Instantiate(gameObject, pos, rotation, parent);
    }
    
}
