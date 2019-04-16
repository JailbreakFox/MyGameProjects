using UnityEngine;
using System.Collections;

public class SteeringPlayer : Steering {
	
	public float m_speed = 5f;
	public float m_acc = 0.5f;
	private Vector3 desiredVelocity;
	private Vector3 desiredVelocity_plus;
	private Vehicle m_vehicle;
	private float maxSpeed;
	private bool isPlanar;
	
	
	void Start () {
		m_vehicle = GetComponent<Vehicle>();
		maxSpeed = m_vehicle.maxSpeed;
		isPlanar = m_vehicle.isPlanar;
	}
	
	void Update()
    {

    }

	public override Vector3 Force()
	{
		desiredVelocity_plus = new Vector3(0,0,0);

		if (Input.GetKey(KeyCode.W)|Input.GetKey(KeyCode.UpArrow)) //前
		{
			desiredVelocity_plus += new Vector3(0,1,0);
		}
		if (Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.DownArrow)) //后
		{
			desiredVelocity_plus -= new Vector3(0,1,0);
		}
		if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow)) //左
		{
			desiredVelocity_plus -= new Vector3(1,0,0);
		}
		if (Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow)) //右
		{
			desiredVelocity_plus += new Vector3(1,0,0);
		}

		desiredVelocity = desiredVelocity_plus.normalized * maxSpeed;
		if (isPlanar)
			desiredVelocity.z = 0;
		return (desiredVelocity - m_vehicle.velocity);
	}
}

