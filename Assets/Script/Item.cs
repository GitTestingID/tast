using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    [SerializeField]
    UI2DSprite m_iconSpr;
    [SerializeField]
    UILabel m_nameLabel;
    [SerializeField]
    UISprite m_selectSpr;
    Inventory m_myInven;      
    public void SetItem(Inventory.ITEM_TYPE type)
    {
        m_nameLabel.text = type.ToString();
        m_iconSpr.sprite2D = m_myInven.GetItemSprite(type);
        m_selectSpr.color = Color.white;
    }
    public void SelectItem()
    {
        m_myInven.SelectItem(this);
    }
    public void Select()
    {
        m_selectSpr.color = Color.yellow;
    }
    public void UnSelect()
    {
        m_selectSpr.color = Color.white;
    }
    public bool IsSelect()
    {
        return m_selectSpr.color == Color.yellow;
    }
	// Use this for initialization
	void Awake () {
        m_myInven = GameObject.Find("Panel_Inventroy").GetComponent<Inventory>();        

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
