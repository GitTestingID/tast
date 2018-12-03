using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    float m_speed = 10f;
    Vector3 m_dir = Vector3.right;
    SpriteRenderer m_sprRenderer;
    Rigidbody2D m_rigidBody;
    [SerializeField]
    GameObject m_explosionPrefab;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag.Equals("wall"))
        {
            RemoveProjectile();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("wall"))
        {
            CreateExplosion();
            RemoveProjectile();
        }
    }
    void CreateExplosion()
    {
        var obj = Instantiate(m_explosionPrefab) as GameObject;
        obj.transform.position = transform.position;
    }   
    void RemoveProjectile()
    {
        Destroy(gameObject);
    }
    public void SetProjectile(Vector3 dir, Vector3 pos)
    {
        transform.position = pos;
        m_dir = dir;
        m_sprRenderer.flipX = (dir == Vector3.left) ? true : false;
        Invoke("RemoveProjectile", 3f);
      //  m_rigidBody.AddForce(m_dir * m_speed*2, ForceMode2D.Impulse);
    }
    private void Awake()
    {
        m_sprRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }
   
	// Update is called once per frame
	void Update () {        
        transform.position += m_dir * m_speed * Time.deltaTime;
	}
}
