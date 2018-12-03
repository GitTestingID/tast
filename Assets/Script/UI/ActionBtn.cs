using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBtn : SingletonMonoBehaviour<ActionBtn> {
    GameObject m_targetObject;

    public void SetTargetObject(GameObject gObj)
    {
        m_targetObject = gObj;
    }
    public void OnPress()
    {
        Debug.Log("on");
        if(m_targetObject != null)
        {
            m_targetObject.SendMessage("PressShoot");
        }
    }
    public void OnRelease()
    {
        Debug.Log("off");
        if (m_targetObject != null)
        {
            m_targetObject.SendMessage("ReleaseShoot");
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
