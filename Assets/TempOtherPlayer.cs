using UnityEngine;
using System.Collections;

public class TempOtherPlayer : MonoBehaviour {
    
    public Transform target;

	void Start () {
        if(!target)
            target = GameObject.FindGameObjectWithTag("Player").transform;    
	}
	
	void Update () {
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
	}
}
