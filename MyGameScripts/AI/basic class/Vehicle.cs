using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
	//AI角色包含的操控列表
	private Steering[] steerings;
	//AI角色最高速度
	public float maxSpeed = 10.0f;
	//AI角色能被施加的最大力
	public float maxForce = 100.0f;
	//最大速度的平方，提前算出，省cpu
	protected float sqrMaxSpeed;
	//AI角色mass
	public float mass = 1.0f;
	//AI角色velocity
	public Vector3 velocity;
	//AI角色the velocity to change the direction
	public float damping = 0.9f; //f means the num is float,it means double without f
	//操控力更新间隔，为了达到高帧率，不必要每帧更新
	public float computeInterval = 0.2f;
	//判断是否是2D游戏，若是则忽略z值
	public bool isPlanar = true;
	//计算得到的操控力
	private Vector3 steeringForce;
	//AI角色的加速度
	protected Vector3 acceleration;
	//计时器
	private float timer;

    // Start is called before the first frame update
    void Start()
    {
		steeringForce = new Vector3(0,0,0);
		sqrMaxSpeed = maxSpeed*maxSpeed;
		timer = 0;
		//获得该AI角色所包含的操控行为列表
		steerings = GetComponents<Steering>;
    }

    // Update is called once per frame
    void Update()
    {
		timer += Time.deltaTime;
		steeringForce = new Vector3(0,0,0);
		//判断计算操控力的时间是否超过了computeInterval，若是则计算
		if (timer > computeInterval)
		{
			//对操控行为列表中的操控行为力乘以权重并求和
			foreach (Steering s in steerings)
			{
				if (s.enabled)
				{
					steeringForce += s.Force()*s.weight;
				}
			}
			//使操控力不大于maxForce
			steeringForce = Vector3.ClampMagnitude(steeringForce,maxForce);
			//求出加速度
			acceleration = steeringForce/mass;
			//重新计数
			timer = 0;
		}
    }
}
