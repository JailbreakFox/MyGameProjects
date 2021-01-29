using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Mathf;

public class move123 : MonoBehaviour
{
	public float m_speed = 5f;
	float x1;
	float x2;
	float x3;
	float x4;
	float x5=1.7f;
	float x6=0f;
	float x7;
	float x8;
	float x9;
	float x10;
	float xm;
	float ym;
	float zm;
	float x1_2;
	float x3_2;
	float kd1=1.2f;
	float kp1=0.8f;
	float k1=-2f;
	float g=9.8f;
	float s1;
	float s2;
	int shut=0;

    // Start is called before the first frame update
    void Start()
    {
		xm=GameObject.Find ("Sphere").GetComponent<sphere_position>().sphere_x-this.transform.localPosition.x;
		ym=GameObject.Find ("Sphere").GetComponent<sphere_position>().sphere_y-this.transform.localPosition.y;
		zm=GameObject.Find ("Sphere").GetComponent<sphere_position>().sphere_z-this.transform.localPosition.z;
		x1=this.transform.localPosition.x;
		x3=this.transform.localPosition.z;
		x7=-Mathf.Atan(xm/ym);
		x9=Mathf.Atan(zm/Mathf.Sqrt(xm*xm+ym*ym));
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey(KeyCode.W)|Input.GetKey(KeyCode.UpArrow)) //前
		{
			this.transform.Translate(Vector3.forward*m_speed*Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.DownArrow)) //后
		{
			this.transform.Translate(Vector3.forward *- m_speed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow)) //左
		{
			this.transform.Translate(Vector3.right *-m_speed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow)) //右
		{
			this.transform.Translate(Vector3.right * m_speed * Time.deltaTime);
		}

		xm=GameObject.Find ("Sphere").GetComponent<sphere_position>().sphere_x-this.transform.localPosition.x;
		ym=GameObject.Find ("Sphere").GetComponent<sphere_position>().sphere_y-this.transform.localPosition.y;
		zm=GameObject.Find ("Sphere").GetComponent<sphere_position>().sphere_z-this.transform.localPosition.z;
		x2=(this.transform.localPosition.x-x1)/Time.deltaTime;
		x4=(this.transform.localPosition.z-x3)/Time.deltaTime;
		x8=(-Mathf.Atan(xm/ym)-x7)/Time.deltaTime;
		x10=(Mathf.Atan(zm/Mathf.Sqrt(xm*xm+ym*ym))-x9)/Time.deltaTime;
		x1=this.transform.localPosition.x;
		x3=this.transform.localPosition.z;
		x7=-Mathf.Atan(xm/ym);
		x9=Mathf.Atan(zm/Mathf.Sqrt(xm*xm+ym*ym));

		print ("结点坐标为：x"+this.transform.localPosition.x + ",y"+this.transform.localPosition.y+",z"+this.transform.localPosition.z);
		print ("载重物坐标为：x"+GameObject.Find ("Sphere").GetComponent<sphere_position>().sphere_x+ ",y"+GameObject.Find ("Sphere").GetComponent<sphere_position>().sphere_y+",z"+GameObject.Find ("Sphere").GetComponent<sphere_position>().sphere_z);

		x1_2 = kd1*(-x2)+kp1*(-x1)-k1*x8;
		x3_2 = kd1*(-x4)+kp1*(-x3)-k1*x10;

		if (Input.GetKeyUp(KeyCode.H))
		{
			if(shut==1)
			{
				shut=0;
			}
			else
			{
				shut=1;
			}
		}

		if(shut==1)
		{
			s1=x2*Time.deltaTime+x1_2*(Time.deltaTime*Time.deltaTime)/2;
			s2=x4*Time.deltaTime+x3_2*(Time.deltaTime*Time.deltaTime)/2;
			this.transform.Translate(Vector3.right*s1);
			this.transform.Translate(Vector3.forward*s2);
		}
    }
}
