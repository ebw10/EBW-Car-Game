using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camcontEBW : MonoBehaviour {

    public Transform target;
    		
	void FixedUpdate ()
    {
        Vector3 newPosition = target.position;
        newPosition.z = -10;

        transform.position = newPosition;
	}
}
