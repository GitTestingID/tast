using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picking : MonoBehaviour {
    Ray m_ray;
    RaycastHit m_rayHit;
    [SerializeField]
    Camera m_mainCamera;
    public GameObject GetPickObject()
    {
        m_ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(m_ray, out m_rayHit, 1000f, -1))
        {
            return m_rayHit.collider.gameObject;
        }
        return null;
    }
	// Use this for initialization
	void Start () {
		
	}

    GameObject m_selectObject;
    Vector3 m_orgPos;
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            m_selectObject = GetPickObject();
            if (m_selectObject != null)
                m_orgPos = m_selectObject.transform.position;
        }
        if(Input.GetMouseButtonUp(0))
        {
            if (m_selectObject != null)
            {
                m_selectObject.transform.position = m_orgPos;
                m_selectObject = null;
            }
        }
        if(m_selectObject != null)
        {
            m_selectObject.transform.position = m_orgPos + Vector3.back * 10;
            Debug.DrawRay(m_ray.origin, m_ray.direction * m_rayHit.distance, Color.red);
        }

	}
}
