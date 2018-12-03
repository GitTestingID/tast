using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleController : MonoBehaviour {
    bool m_isToggle;
    string m_userID = "ID를 입력 해주세요";    
    public void GoNextScene()
    {
        LoadSceneManager.Instance.LoadSceneAsync(LoadSceneManager.SCENE_STATE.Game);
    }
    /*private void OnGUI()
    {
        if(GUI.Button(new Rect((Screen.width - 200)/2, (Screen.height - 50)/2, 200, 50), "START"))
        {
            // SceneManager.LoadSceneAsync("Game");
            LoadSceneManager.Instance.LoadSceneAsync("Game");
        }
        GUILayout.BeginArea(new Rect(2, Screen.height - 400, 150, 400), "옵션화면", GUI.skin.window);
        if(GUILayout.Button("START"))
        {
            SceneManager.LoadScene("Game");
        }
        m_isToggle = GUILayout.Toggle(m_isToggle, "Stage01");
        if (m_isToggle)
        {
            GUILayout.BeginHorizontal(GUILayout.Width(50));
            GUILayout.Label("Stage01 활성화");
            GUILayout.EndHorizontal();
        }
        else
        {
            GUILayout.BeginHorizontal(GUILayout.Width(50));
            GUILayout.Label("Stage01 비활성화");
            GUILayout.EndHorizontal();
        }
        m_userID = GUILayout.TextField(m_userID, 15);
        GUILayout.EndArea();
       
    }*/
    // Use this for initialization
    void Start () {     
    }

    
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            if (Random.Range(0, 100) % 2 == 0)
                PopupManager.Instance.OpenOKPopup("공지사항", "정기정검 안내\r\n일시: 2018.11.05~ 2018.11.06", null, "확인");
            else
                PopupManager.Instance.OpenOKCancelPopup("에러", "네트워크 상태가 좋지 않습니다.\r\n 다시 시도하시겠습니까?", null, null, "예", "아니오");
        }
	}
}
