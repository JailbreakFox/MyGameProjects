using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{


    private Transform m_Transform;
    private bool flag = true;
    // Use this for initialization
    void Start()
    {

        m_Transform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            m_Transform.Translate(Vector3.up, Space.Self);
        }
        if (flag == true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                m_Transform.Translate(Vector3.forward * 0.2f, Space.Self);
            }

            if (Input.GetKey(KeyCode.S))
            {
                m_Transform.Translate(Vector3.back * 0.2f, Space.Self);
            }

            if (Input.GetKey(KeyCode.A))
            {
                m_Transform.Rotate(Vector3.up, -3);
            }

            if (Input.GetKey(KeyCode.D))
            {
                m_Transform.Rotate(Vector3.up, 3);
            }
        }
       
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.name == "ground")
        {
            flag = true;
        }
    }
    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.name == "ground")
        {
            flag = false;
        }
    }
}
