using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Engine_03 : MonoBehaviour
{



    private PhysicsCondition m_Condition;
    private Transform m_Transform;
    private Rigidbody m_rigidbody;
    private bool flag = true;

    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
        m_Condition = new PhysicsCondition();
    }
    void Update()
    {


        if (Input.GetKey(KeyCode.W))
        {
            m_Condition.AccelerateByM();
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_Condition.DecelerateByM();
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_Transform.Rotate(Vector3.up, -2);
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_Transform.Rotate(Vector3.up, 2);
        }

        m_Condition.UpdateMomentum();
        m_Transform.Translate(Vector3.forward * m_Condition.GetV(), Space.Self);
        //m_rigidbody.MovePosition(Vector3.forward*m_Condition.GetV());
    }
}



public class PhysicsCondition
{

    private float v;
    private float momentum;
    private float m;
    private float engine_power_acc;
    private float engine_power_dec;
    private float engine_power_brake;
    private float drag_index_ground;
    private float drag_index_wind;
    public PhysicsCondition()
    {
        momentum = 0;
        m = 1;
        engine_power_acc = 0.005f;
        engine_power_dec = 0.003f;
        drag_index_wind = 0.007f;
        drag_index_ground = 0.002f;
    }
    public float GetV()
    {
        v = momentum / m;
        return v;
    }
    public void AccelerateByV(float para)
    {
        v += para;
    }
    public void AccelerateByM()
    {
        momentum += engine_power_acc;
    }
    public void DecelerateByM()
    {
        momentum -= engine_power_dec;
    }
    public void UpdateMomentum()
    {
        float tot_drag = drag_index_ground * m + drag_index_wind * v;
        if (momentum > 0)
        {
            momentum -= tot_drag;
            if (momentum < 0)
                momentum = 0;
        }
        if (momentum < 0)
        {
            momentum += tot_drag;
            if (momentum > 0)
                momentum = 0;
        }
    }
}


