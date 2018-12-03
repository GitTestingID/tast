using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupOK : MonoBehaviour {
    [SerializeField]
    UILabel m_subjectLabel;
    [SerializeField]
    UILabel m_bodyLabel;
    [SerializeField]
    UILabel m_okBtnLabel;
    PopupManager.ButtonDelegate m_onClickOk;
    UIPanel[] m_subPanels; 
    public void SetUI(string subject, string body, PopupManager.ButtonDelegate onClickOk = null,string okBtnStr = "OK")
    {
        m_subjectLabel.text = subject;
        m_bodyLabel.text = body;
        m_okBtnLabel.text = okBtnStr;

        for(int i = 0; i < m_subPanels.Length; i++ )
        {
            m_subPanels[i].depth = m_subPanels[0].depth + i;
        }
        m_onClickOk = onClickOk;
    }
    public void OnClickOK()
    {
        if(m_onClickOk != null)
        {
            m_onClickOk();
        }
        else
        {
            PopupManager.Instance.ClosePopup();
        }
    }
    private void Awake()
    {
        m_subPanels = GetComponentsInChildren<UIPanel>();
    }
    // Use this for initialization
    void Start () {        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
