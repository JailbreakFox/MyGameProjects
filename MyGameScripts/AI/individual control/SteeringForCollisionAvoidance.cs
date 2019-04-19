using UnityEngine;
using System.Collections;

public class SteeringForCollisionAvoidance : Steering 
{
	public bool isPlanar;	
	private Vector3 desiredVelocity;
	private Vehicle m_vehicle;
	private float maxSpeed;
	private float maxForce;
	public float avoidanceForce;
	public float MAX_SEE_AHEAD;
	private GameObject[] allColliders;

	void Start () 
	{
		m_vehicle = GetComponent<Vehicle>();
		maxSpeed = m_vehicle.maxSpeed;
		maxForce = m_vehicle.maxForce;
		isPlanar = m_vehicle.isPlanar;
		//avoidanceForce = 20.0f;
		if (avoidanceForce > maxForce)
			avoidanceForce = maxForce;

		//MAX_SEE_AHEAD = 20.0f;
		allColliders = GameObject.FindGameObjectsWithTag("obstacle");
	}

	public override Vector3 Force()
	{
		RaycastHit hit;
		Vector3 force = new Vector3(0,0,0);
		//Debug.DrawLine(transform.position, transform.position + transform.forward * 10);

		Vector3 velocity = m_vehicle.velocity;
		Vector3 normalizedVelocity = velocity.normalized;

		//Debug.DrawLine(transform.position, transform.position + normalizedVelocity * MAX_SEE_AHEAD * (velocity.magnitude / maxSpeed));
		
		//碰撞体collider需要设置成3D的，2D的经测试检测不到
		//if (Physics.Raycast(transform.position, normalizedVelocity, out hit, MAX_SEE_AHEAD))
		if (Physics.Raycast(transform.position, normalizedVelocity, out hit, MAX_SEE_AHEAD * velocity.magnitude / maxSpeed))
		{
			//Vector3 ahead = transform.position + normalizedVelocity * MAX_SEE_AHEAD;
			Vector3 ahead = transform.position + normalizedVelocity * MAX_SEE_AHEAD * (velocity.magnitude / maxSpeed);
			force = ahead - hit.collider.transform.position;
			force *= avoidanceForce; 

			if (isPlanar)
				force.z = 0;

			//Debug.DrawLine(transform.position, transform.position + force);	
			//change color use when there is only one AI in the scene, or multiple actions will conflict
			foreach (GameObject c in allColliders)
			{
				Renderer render = c.GetComponent<Renderer>();
				if (hit.collider.gameObject == c)
				{
					render.material.color = Color.black;//Color.green;
				}
				else
					render.material.color = Color.white;//Color.gray;
			}
		}
		else
		{
			foreach (GameObject c in allColliders)
			{
				Renderer render = c.GetComponent<Renderer>();
				render.material.color = Color.white;//Color.gray;
			}
		}

		/*
		if (Physics.Raycast(transform.position, transform.forward, out hit, maxSpeed))
		{
			if (hit.collider.tag != "ground")
			{	
				Vector3 ahead = transform.position + transform.forward * maxSpeed;
				force = ahead - hit.collider.transform.position;
				force *= maxForce;

				if (isPlanar)
					force.y = 0;
			
				Debug.DrawLine(transform.position, transform.position + force);			
			}
		}*/

		return force;
	}
}
