using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePad : SingletonMonoBehaviour<GamePad> {
    [SerializeField]
    Transform m_padBG;
    [SerializeField]
    Transform m_padBtn; 
    [SerializeField]   
    Camera m_uiCamera;
    const float m_scalePos = 0.2f;
    bool m_isMove;
    Vector3 m_basePos;
    Vector3 m_dir;
    Ray m_ray;
    RaycastHit m_rayHit;
    int m_fingerID;
    public Vector2 GetAxis()
    {
        return m_dir;
    }
    // Use this for initialization
    protected override void OnStart()
    {
        base.OnStart();
        m_fingerID = -1;
        m_uiCamera = GameObject.Find("UI Root").GetComponentInChildren<Camera>();
        m_basePos = m_padBtn.transform.position;
    }	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR || !UNITY_STANDALONE
        if (!m_isMove)
        {
            m_dir = Vector3.zero;
            m_padBtn.transform.localPosition = Vector3.zero;

            if(Input.GetKey(KeyCode.W))
            {
                m_dir += Vector3.up;
            }
            if(Input.GetKey(KeyCode.S))
            {
                m_dir += Vector3.down;
            }
            if(Input.GetKey(KeyCode.A))
            {
                m_dir += Vector3.left;
            }
            if(Input.GetKey(KeyCode.D))
            {
                m_dir += Vector3.right;
            }
            m_dir.Normalize();
            m_padBtn.transform.position += m_dir * m_scalePos;
        }
        if(Input.GetButtonDown("Fire1"))
        {
            m_ray = m_uiCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(m_ray, out m_rayHit, 1000f, 1 << LayerMask.NameToLayer("UI")))
            {
                if (m_rayHit.collider.transform.Equals(m_padBG))
                {
                    var worldPos = m_uiCamera.ScreenToWorldPoint(Input.mousePosition);
                    var dir = worldPos - m_basePos;
                    if (dir.magnitude > m_scalePos)
                    {
                        dir = dir.normalized * m_scalePos;
                    }
                    m_padBtn.transform.position = m_basePos + dir;
                    m_dir = dir / m_scalePos;
                    m_isMove = true;
                }
            }
        }
        if(m_isMove)
        {
            var worldPos = m_uiCamera.ScreenToWorldPoint(Input.mousePosition);
            var dir = worldPos - m_basePos;
            if (dir.magnitude > m_scalePos)
            {
                dir = dir.normalized * m_scalePos;
            }
            m_padBtn.transform.position = m_basePos + dir;
            m_dir = dir / m_scalePos;            
        }
        if(Input.GetButtonUp("Fire1"))
        {
            m_isMove = false;
            m_dir = Vector3.zero;
            m_padBtn.transform.localPosition = Vector3.zero;
        }
#elif UNITY_ADNROID || UNITY_IOS
       for(int i = 0; i < Input.touches.Length; i++)
        {
            if(Input.touches[i].phase == TouchPhase.Began)
            {
                m_ray = m_uiCamera.ScreenPointToRay(Input.touches[i].position);
                if (Physics.Raycast(m_ray, out m_rayHit, 1000f, 1 << LayerMask.NameToLayer("UI")))
                {
                    if (m_rayHit.collider.transform.Equals(m_padBG))
                    {
                        var worldPos = m_uiCamera.ScreenToWorldPoint(Input.mousePosition);
                        var dir = worldPos - m_basePos;
                        if (dir.magnitude > m_scalePos)
                        {
                            dir = dir.normalized * m_scalePos;
                        }
                        m_padBtn.transform.position = m_basePos + dir;
                        m_dir = dir / m_scalePos;
                        m_fingerID = Input.touches[i].fingerId;
                        m_isMove = true;
                    }
                }
            }
            if(Input.touches[i].phase == TouchPhase.Moved && Input.touches[i].fingerId == m_fingerID && m_isMove)
            {
                var worldPos = m_uiCamera.ScreenToWorldPoint(Input.touches[i].position);
                var dir = worldPos - m_basePos;
                if (dir.magnitude > m_scalePos)
                {
                    dir = dir.normalized * m_scalePos;
                }
                m_padBtn.transform.position = m_basePos + dir;
                m_dir = dir / m_scalePos;
            }
            if(Input.touches[i].phase == TouchPhase.Ended || Input.touches[i].phase == TouchPhase.Canceled)
            {
                m_isMove = false;
                m_dir = Vector3.zero;
                m_padBtn.transform.localPosition = Vector3.zero;
                m_fingerID = -1;
            }
        }         
#endif

    }
}
