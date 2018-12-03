using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : DontDestroy<PopupManager> {

    public delegate void ButtonDelegate();

    const int m_popupDepth = 1000;
    const int m_popupDepthGap = 10;
    [SerializeField]
    GameObject m_okPopupPrefab = null;
    [SerializeField]
    GameObject m_okCancelPopupPrefab = null;
    List<GameObject> m_popupList = new List<GameObject>();

    public void OpenOKPopup(string subject, string body, ButtonDelegate onClickOK = null, string okBtnStr = "OK")
    {
        var obj = Instantiate(m_okPopupPrefab) as GameObject;

        obj.transform.SetParent(transform);
        obj.GetComponent<UIPanel>().depth = m_popupDepth + m_popupList.Count * m_popupDepthGap;
        var popup = obj.GetComponent<PopupOK>();
        popup.SetUI(subject, body, onClickOK, okBtnStr);

        m_popupList.Add(obj);
    }
    public void OpenOKCancelPopup(string subject, string body, ButtonDelegate onClickOK = null, ButtonDelegate onClickCancel = null, string okBtnStr = "OK", string cancelBtnStr = "Cancel")
    {
        var obj = Instantiate(m_okCancelPopupPrefab) as GameObject;

        obj.transform.SetParent(transform);
        obj.GetComponent<UIPanel>().depth = m_popupDepth + m_popupList.Count * m_popupDepthGap;
        var popup = obj.GetComponent<PopupOKCancel>();
        popup.SetUI(subject, body, onClickOK, onClickCancel, okBtnStr, cancelBtnStr);

        m_popupList.Add(obj);
    }
    public void ClosePopup()
    {
        if(m_popupList.Count > 0)
        {
            Destroy(m_popupList[m_popupList.Count - 1]);
            m_popupList.RemoveAt(m_popupList.Count - 1);
        }
    }
    public bool ProcessEscape()
    {
        if(m_popupList.Count > 0)
        {
            ClosePopup();
            return true;
        }
        return false;
    }
    protected override void OnAwake()
    {
        base.OnAwake();
        m_okPopupPrefab = Resources.Load(@"Popup\PopupOK") as GameObject;
        m_okCancelPopupPrefab = Resources.Load(@"Popup\PopupOKCancel") as GameObject;

    }
    // Use this for initialization
    protected override void OnStart()
    {
        base.OnStart();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
