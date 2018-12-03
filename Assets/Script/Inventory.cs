using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    [SerializeField]
    Sprite[] m_itemSpr;
    [SerializeField]
    GameObject m_itemPrefab;
    [SerializeField]
    UIGrid m_grid;
    [SerializeField]
    TweenScale m_windowTween;
    List<Item> m_itemList = new List<Item>();
    Item m_curSelItem; 
    public enum ITEM_TYPE
    {
        None = -1,
        Coin,
        Ball,
        Bomb,
        Bowling,
        Magnet,
        Max
    }
    void ResetPostionGrid()
    {
        m_grid.Reposition();
    }
    public void CreateItem()
    {
        var obj = Instantiate(m_itemPrefab) as GameObject;        
        var type = (ITEM_TYPE)Random.Range((int)ITEM_TYPE.Coin, (int)ITEM_TYPE.Max);
        var item = obj.GetComponent<Item>();
        item.SetItem(type);
        obj.transform.SetParent(m_grid.transform);
        obj.transform.localScale = Vector3.one;
        m_itemList.Add(item);
        ResetPostionGrid();
    }
    public void SelectItem(Item item)
    {
        /*for(int i = 0; i < m_itemList.Count; i++)
        {
            if(m_itemList[i].IsSelect())
            {
                m_itemList[i].UnSelect();                
                break;
            }
        }*/
        if(m_curSelItem != null)
        {
            var select = m_itemList.Find((obj) => obj.IsSelect());
            select.UnSelect();
            m_curSelItem = null;
        }
        /*for (int i = 0; i < m_itemList.Count; i++)
        {
            if (m_itemList[i] == item)
            {
                m_curSelItem = item;
                m_itemList[i].Select();
                break;
            }
        }*/       
        if (!item.IsSelect())
        {
            m_curSelItem = item;
            item.Select();
        }
    }
    bool m_isSet;
    public void RemoveItem()
    {
        if(m_curSelItem != null)
        {
            m_itemList.Remove(m_curSelItem);
            Destroy(m_curSelItem.gameObject);
            m_curSelItem = null;
            m_isSet = true;
            //ResetPostionGrid();
        }
    }
    public void Open()
    {
        gameObject.SetActive(true);
        m_windowTween.ResetToBeginning();
        m_windowTween.PlayForward();
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public Sprite GetItemSprite(ITEM_TYPE type)
    {
        return m_itemSpr[(int)type];
    }
	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);	
	}
    int m_frame;
	// Update is called once per frame
	void Update () {
		if(m_isSet)
        {
            m_frame++;
            if(m_frame > 1)
            {
                ResetPostionGrid();
                m_frame = 0;
                m_isSet = false;
            }
        }
	}
}
