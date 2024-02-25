using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListKeep : MonoBehaviour
{
    public static ListKeep instance;

    [SerializeField] private GameObject KeepListWindow;
    [SerializeField] private ItemKeep ItemKeepPrefab;
    [SerializeField] private ItemLine ItemLinePrefab;
    [SerializeField] private Transform KeepListParent; 
    public List<ItemKeep> _KeepItemList = new List<ItemKeep>();
    public List<ItemLine> _LineItemList = new List<ItemLine>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateKeepItemList();
    }

    void ScaleObjectWithScreenSize(GameObject obj)
    {
        RectTransform rectTransform = obj.GetComponent<RectTransform>();

        // Get the screen dimensions
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // You may need to adjust these values based on your specific requirements
        float scaleFactorX = screenWidth / 1920f; // 1920 is a reference width
        float scaleFactorY = screenHeight / 1080f; // 1080 is a reference height

        // Apply the scale to the object's RectTransform
        rectTransform.localScale = new Vector3(scaleFactorX, scaleFactorY, 1f);
    }

    public void UpdateKeepItemList()
    {
        for (int i = 0; i < _KeepItemList.Count; i++)
        {
            Destroy(_KeepItemList[i].gameObject);
        }
        _KeepItemList.Clear();

        for (int j = 0; j < GameManager.instace.playerList.Count; j++)
        {
            //generate a new list with update info
            for (int i = 0; i < GameManager.instace.playerList[j].Keep.Count; i++)
            {

                //skip empty 
                if (GameManager.instace.playerList[j].Keep.Count == 0)
                {
                    continue;
                }

                ItemKeep newItem = Instantiate(ItemKeepPrefab);
                newItem.itemKeepParent = this;
                newItem.SetItemName(GameManager.instace.playerList[j].Keep[i].CardName);
                newItem.SetPriceInItem((GameManager.instace.playerList[j].Keep[i].price).ToString());
                newItem.transform.SetParent(KeepListParent);
                ScaleObjectWithScreenSize(newItem.gameObject);
                _KeepItemList.Add(newItem);
                /*
                ItemLine lineItem = Instantiate(ItemLinePrefab);
                lineItem.itemLineParent = this;
                lineItem.transform.SetParent(KeepListParent);
                _LineItemList.Add(lineItem);*/
            }
        }
    }
}
