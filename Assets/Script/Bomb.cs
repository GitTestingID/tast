using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    Rigidbody2D m_rigidbody;
    SpringJoint2D m_springJoint;
    LineRenderer m_catapultFrontLine;
    LineRenderer m_catapultBackLine;
    CircleCollider2D m_cicleCol;
    [SerializeField]
    float m_maxDistance = 4f;
    float m_sqrmaxDistance;
    float m_radius;
    bool m_clickOn;
    private void OnMouseDown()
    {
        m_clickOn = true;
        m_springJoint.enabled = false;
    }
    private void OnMouseUp()
    {
        m_clickOn = false;
        m_rigidbody.isKinematic = false;
        m_springJoint.enabled = true;
    }
    void InitBand()
    {
        m_catapultBackLine.startWidth = 0.25f;
        m_catapultBackLine.endWidth = 0.25f;
        m_catapultFrontLine.startWidth = 0.29f;
        m_catapultFrontLine.endWidth = 0.29f;

        m_catapultBackLine.SetPosition(0, m_catapultBackLine.transform.position);
        m_catapultFrontLine.SetPosition(0, m_catapultFrontLine.transform.position);

        m_catapultBackLine.sortingLayerName = "Default";
        m_catapultBackLine.sortingOrder = 1;
        m_catapultFrontLine.sortingLayerName = "Degault";
        m_catapultFrontLine.sortingOrder = 3;
    }
    void DrawBand()
    {
        var catapultToLeft = transform.position - m_catapultBackLine.transform.position;
        var pos = m_catapultBackLine.transform.position + catapultToLeft.normalized * (catapultToLeft.magnitude + catapultToLeft.magnitude + m_radius);
        m_catapultBackLine.SetPosition(1, pos);
        catapultToLeft = transform.position - m_catapultFrontLine.transform.position;
        pos = m_catapultFrontLine.transform.position + catapultToLeft.normalized * (catapultToLeft.magnitude + catapultToLeft.magnitude + m_radius);
        m_catapultFrontLine.SetPosition(1, pos);

    }
    void Dragging()
    {
        var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var catapultToMouse = worldPos - m_catapultBackLine.transform.position;
        if(catapultToMouse.sqrMagnitude > m_sqrmaxDistance)
        {
            worldPos = catapultToMouse.normalized * m_maxDistance;
        }
        transform.position = new Vector3(worldPos.x, worldPos.y);
    }
    // Use this for initialization
    void Start () {
        var result = GameObject.Find("Catapult").GetComponentInChildren<LineRenderer>();
        m_catapultBackLine = result[0];
        m_catapultFrontLine = result[1];
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_springJoint = GetComponent<SpringJoint2D>();
        m_cicleCol = GetComponent<CircleCollider2D>();
        m_radius = m_cicleCol.radius * transform.localScale.x;
        m_rigidbody.isKinematic = true;
        m_sqrmaxDistance = m_maxDistance * m_sqrmaxDistance;
        m_clickOn = false;
        InitBand();
    }
	
	// Update is called once per frame
	void Update () {
        if(m_clickOn)
        {
            Dragging();
        }
        DrawBand();
	}
}
