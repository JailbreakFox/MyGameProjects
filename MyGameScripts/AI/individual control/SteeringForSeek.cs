using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForSeek : Steering
{
	//需要寻找的目标物体
	public GameObject target;
	//预期速度
	private Vector3 desiredVelocity;
	//获得被操控AI角色，以便获取这个AI角色的最大速度等信息
	private Vehicle m_vehicle;
	//最大速度
	private float maxSpeed;
	//是否是2D游戏
	private bool isPlanar;

    // Start is called before the first frame update
    void Start()
    {
        //获取被操控智能体的允许最大速度，是否为2D游戏角色
		m_vehicle = GetComponent<Vehicle>();
		maxSpeed = m_vehicle.maxSpeed;
		isPlanar = m_vehicle.isPlanar;
    }

	public override Vector3 Force()
	{
		//计算预期速度
		desiredVelocity = (target.transform.position - transform.position).normalized*maxSpeed;
		if (isPlanar)
			desiredVelocity.z = 0;
		//返回操控力，即预期速度与当前速度的差
		return (desiredVelocity - m_vehicle.velocity);
	}
}
