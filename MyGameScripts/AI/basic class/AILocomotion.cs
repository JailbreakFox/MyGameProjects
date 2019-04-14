using UnityEngine;
using System.Collections;

public class AILocomotion : Vehicle 
{
	private CharacterController controller;
	private Rigidbody theRigidbody;
	private Vector3 moveDistance;
	public bool displayTrack;

	// Use this for initialization
	void Start () 
	{
		controller = GetComponent<CharacterController>();
		theRigidbody = GetComponent<Rigidbody>();
		moveDistance = new Vector3(0,0,0);
		base.Start_init();
	}
	

	void FixedUpdate()
	{
		velocity += acceleration * Time.fixedDeltaTime; 
		
		if (velocity.sqrMagnitude > sqrMaxSpeed)
			velocity = velocity.normalized * maxSpeed;
		
		moveDistance = velocity * Time.fixedDeltaTime;
		
		if (isPlanar)
		{
			velocity.z = 0;
			moveDistance.z = 0;
		}

		if (displayTrack)
			//Debug.DrawLine(transform.position, transform.position + moveDistance, Color.red,30.0f);
			Debug.DrawLine(transform.position, transform.position + moveDistance, Color.black, 30.0f);
		
		if (controller != null)
		{
			//if (displayTrack)
				//Debug.DrawLine(transform.position, transform.position + moveDistance, Color.blue,20.0f);
			controller.SimpleMove(velocity);

		}
		else if (theRigidbody == null || theRigidbody.isKinematic)
		{
			transform.position += moveDistance;
		}
		else
		{
			theRigidbody.MovePosition(theRigidbody.position + moveDistance);		
		}
		
		//updata facing direction
		if (velocity.sqrMagnitude > 0.00001)
		{
			Vector3 newForward = Vector3.Slerp(transform.up, velocity, damping * Time.deltaTime);
			if (isPlanar)
				newForward.z = 0;
			transform.up = newForward;
		}

		//gameObject.animation.Play("walk");
	}
}
