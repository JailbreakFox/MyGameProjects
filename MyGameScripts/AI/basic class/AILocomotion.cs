using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILocomotion : Vehicle
{
	//AI角色的角色控制器
	private CharacterController controller;
	//AI角色的Rigidbody
	private Rigidbody theRigidbody;
	//AI角色每次的移动距离
	private Vector3 moveDistance;

    // Start is called before the first frame update
    void Start()
    {
        //如果有角色控制器，则获得该控制器
		controller = GetComponent<CharacterController>();
		//如果有Rigidbody，则获取它
		theRigidbody = GetComponent<Rigidbody>();
		//离散时间内需要移动的距离
		moveDistance = new Vector3(0,0,0);
		//调用基类的start函数，进行所需初始化
		base.start();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		//计算速度
		velocity += acceleration*Time.fixedDeltaTime;
		//限制速度低于最大速度
		if (velocity.sqrMagnitude > sqrMaxSpeed)
		{
			velocity = velocity.normalized*maxSpeed;
		}
		//计算离散时间内移动距离
		moveDistance = velocity*Time.fixedDeltaTime;
		//如果是2D游戏，就将z置为0
		if (isPlanar)
		{
			velocity.y = 0;
			moveDistance.y = 0;
		}

		//如果AI角色已经添加了角色控制器，则使用控制器移动
		if (controller != null)
			controller.SimpleMove(velocity);
		//如果AI角色没有角色控制器也没有Rigidbody，或者拥有Rigidbody但是要用动力学方式控制运动
		else if (theRigidbody == null || theRigidbody.isKinematic)
			transform.position += moveDistance;
		//用Rigidbody来控制AI角色
		else
			theRigidbody.MovePosition(theRigidbody.position + moveDistance);
		//如果速度大于一个阈值（防止抖动），则更新朝向
		if (velocity.sqrMagnitude > 0.00001)
		{
			//通过当前朝向与速度方向的插值，计算新的朝向
			Vector3 newForward = newForward;
		}
		//播放行走动画
		gameObject.animation.Play("idle");
    }
}
