using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupOKCancel : MonoBehaviour {
    [SerializeField]
    UILabel m_subjectLabel;
    [SerializeField]
    UILabel m_bodyLabel;
    [SerializeField]
    UILabel m_okBtnLabel;
    [SerializeField]
    UILabel m_cancelBtnLabel;
    PopupManager.ButtonDelegate m_onClickOk;
    PopupManager.ButtonDelegate m_onClickCancel;
    UIPanel[] m_subPanels;
    public void SetUI(string subject, string body, PopupManager.ButtonDelegate onClickOk = null, PopupManager.ButtonDelegate onClickCancel = null, string okBtnStr = "OK", string cancelBtnStr = "Cancel")
    {
        m_subjectLabel.text = subject;
        m_bodyLabel.text = body;
        m_okBtnLabel.text = okBtnStr;
        m_cancelBtnLabel.text = cancelBtnStr;

        for (int i = 0; i < m_subPanels.Length; i++)
        {
            m_subPanels[i].depth = m_subPanels[0].depth + i;
        }
        m_onClickOk = onClickOk;
        m_onClickCancel = onClickCancel;
    }
    public void OnClickOK()
    {
        if (m_onClickOk != null)
        {
            m_onClickOk();
        }
        else
        {
            PopupManager.Instance.ClosePopup();
        }
    }
    public void OnClickCancel()
    {
        if (m_onClickCancel != null)
        {
            m_onClickCancel();
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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
