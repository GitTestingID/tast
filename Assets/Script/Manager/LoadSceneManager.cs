using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class LoadSceneManager : DontDestroy<LoadSceneManager> {
    public enum SCENE_STATE
    {
        None = -1,
        CI,
        Title,
        GamePad,
        Lobby,
        Game
    }
    SCENE_STATE m_state;
    SCENE_STATE m_loadState;

    [SerializeField]
    UILabel m_progressLabel;
    AsyncOperation m_loadingTask = null;   

    public void SetState(SCENE_STATE state)
    {
        m_state = state;
    }
    public void LoadScene(SCENE_STATE scene)
    {
        if (m_loadState != SCENE_STATE.None)
            return;
        SetState(scene);
        SceneManager.LoadScene(scene.ToString());
    }
    public void LoadSceneAsync(SCENE_STATE scene)
    {
        if (m_loadState != SCENE_STATE.None)
            return;
        m_loadState = scene;
        m_loadingTask = SceneManager.LoadSceneAsync(scene.ToString());
    }
    public void LoadSceneMerge(SCENE_STATE scene)
    {
        if (m_loadState != SCENE_STATE.None)
            return;
        m_loadState = scene;
        m_loadingTask = SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive);
    }
    protected override void OnStart()
    {
        base.OnStart();
    }
    protected override void OnAwake()
    {
        base.OnAwake();
        m_loadState = SCENE_STATE.None;
        m_state = SCENE_STATE.Title;
    }
    // Update is called once per frame
    void Update () {
		if(m_loadingTask != null && m_loadState != SCENE_STATE.GamePad)
        {
            if(m_loadingTask.isDone)
            {
                m_loadingTask = null;
                SetState(m_loadState);
                m_loadState = SCENE_STATE.None;
                m_progressLabel.text = string.Empty;
            }
            else
            {
               m_progressLabel.text =  string.Format("{0:#.##}%",Mathf.Round(m_loadingTask.progress) * 100);
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!PopupManager.Instance.ProcessEscape())
            {
                switch(m_state)
                {
                    case SCENE_STATE.Title:
                        PopupManager.Instance.OpenOKCancelPopup("Notice", "게임을 종료하시겠습니까?", ()=> {
#if !UNITY_EDITOR
                            Application.Quit();
#else
                            EditorApplication.isPlaying = false;
                            PopupManager.Instance.ClosePopup();
#endif
                        }, null, "예", "아니오");
                        break;
                    case SCENE_STATE.Lobby:
                        PopupManager.Instance.OpenOKCancelPopup("Notice", "타이틀로 돌아가시겠습니까?", ()=> {
                            LoadSceneAsync(SCENE_STATE.Title);
                            PopupManager.Instance.ClosePopup();
                        }, null, "예", "아니오");
                        break;
                    case SCENE_STATE.Game:
                        PopupManager.Instance.OpenOKCancelPopup("Notice", "로비로 돌아가시겠습니까?", ()=> {
                            LoadSceneAsync(SCENE_STATE.Lobby);
                            PopupManager.Instance.ClosePopup();
                        }, null, "예", "아니오");
                        break;
                }
            }
        }
	}
}
