using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    Animator m_animator;
	// Use this for initialization
	void Awake () {
        m_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        var info = m_animator.GetCurrentAnimatorStateInfo(0);
        if(info.IsName("explosion"))
        {
            if (info.normalizedTime >= 1)
                Destroy(gameObject);
        }
	}
}
