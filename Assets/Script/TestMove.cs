using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour {
    [SerializeField]
    Animator m_animator;
    [SerializeField]
    SpriteRenderer m_sprRenderer;
    [SerializeField]
    Rigidbody2D m_rigidbody;
    [SerializeField]
    GameObject m_misslePrefab;
    [SerializeField]
    Transform m_firePos;
    [SerializeField]
    Inventory m_myInven;
    [SerializeField]
    AudioClip m_SfxClip;
    AudioSource m_audioSource;
    float m_speed = 10f;
    Vector3 m_dir;
    
    #region Event Methods      
    void OnEvent_CreateMissle()
    {
        var obj = Instantiate(m_misslePrefab) as GameObject;
        //obj.transform.position = m_firePos.position;
        var missile = obj.GetComponent<Projectile>();
        missile.SetProjectile(transform.localRotation == Quaternion.identity ? Vector3.left : Vector3.right, m_firePos.position);
    }
    #endregion
    // Use this for initialization
    void Start () {
        m_animator = gameObject.GetComponent<Animator>();
        m_sprRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_rigidbody = gameObject.GetComponent<Rigidbody2D>();
        m_audioSource = gameObject.GetComponent<AudioSource>();
        if (m_sprRenderer == null)
        {
            Debug.Log("SpriteRenderer 객체를 찾을 수 없습니다.");
        }
        m_canJump = true;
        m_isFall = false;
        if(ActionBtn.Instance != null)
        ActionBtn.Instance.SetTargetObject(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("ground"))
        {
            m_animator.SetInteger("jump", 0);
            m_canJump = true;
            m_isFall = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (m_animator.GetInteger("jump") != 0)
        {
            if (collision.tag.Equals("ground"))
            {
                m_animator.SetInteger("jump", 0);
                m_canJump = true;
                m_isFall = false;
            }
        }
    }
    void PlayFireSfx()
    {
        //m_audioSource.clip = m_SfxClip;
        m_audioSource.PlayOneShot(m_SfxClip);
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect((Screen.width - 200) / 2, (Screen.height - 50) / 2, 200, 50), "START"))
        {
            // SceneManager.LoadSceneAsync("Game");
            LoadSceneManager.Instance.LoadSceneAsync(LoadSceneManager.SCENE_STATE.Title);
        }
    }
    void PressShoot()
    {
        m_animator.SetBool("isShoot", true);
    }
    void ReleaseShoot()
    {
        m_animator.SetBool("isShoot", false);   
    }
    bool m_canJump;
    bool m_isFall;
	// Update is called once per frame
	void Update () {
        var dir = GamePad.Instance.GetAxis();      
	    if(Input.GetKeyDown(KeyCode.LeftArrow) || (dir.x < 0))
        {
            m_dir = Vector3.left;
            m_animator.SetBool("isMove", true);
            transform.localRotation = Quaternion.identity;
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || (dir.x == 0))
        {
            m_dir = Vector3.zero;
            m_animator.SetBool("isMove", false);
        }	
        if(Input.GetKeyDown(KeyCode.RightArrow) || (dir.x > 0))
        {
            m_dir = Vector3.right;
            m_animator.SetBool("isMove", true);
            transform.localRotation = Quaternion.Euler(new Vector3(0, 180));
        }        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PressShoot();
            //PlayFireSfx();
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            ReleaseShoot();
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(!m_myInven.gameObject.activeSelf)
            {
                m_myInven.Open();
            }
            else
            {
                m_myInven.Close();
            }
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            if (m_canJump)
            {
                m_rigidbody.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
                m_animator.SetInteger("jump", 1);
                m_canJump = false;
                m_isFall = false;
            }
        }
        if(!m_canJump)
        {
            if(m_rigidbody.velocity.y < 0 && !m_isFall)
            {
                m_animator.SetInteger("jump", 2);
                m_isFall = true;
            }
        }
        /*if(m_dir != Vector3.zero)
        {
            var info = m_animator.GetCurrentAnimatorStateInfo(0);
            if(info.IsName("walk"))
                m_rigidbody.AddForce(m_dir * m_speed, ForceMode2D.Force);
        }*/
        gameObject.transform.position += m_dir * m_speed * Time.deltaTime;
    }
}
