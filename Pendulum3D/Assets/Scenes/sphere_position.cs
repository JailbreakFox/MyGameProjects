using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphere_position : MonoBehaviour
{
	public float sphere_x;
	public float sphere_y;
	public float sphere_z; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sphere_x = this.transform.localPosition.x;
		sphere_y = this.transform.localPosition.y;
		sphere_z = this.transform.localPosition.z;
    }
}
