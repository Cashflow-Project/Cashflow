using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellObjFromYourself : MonoBehaviour
{
    public static SellObjFromYourself instance;

    [SerializeField] private GameObject SellListWindow;
    [SerializeField] private SellItemUI SellItemUIPrefab;
    [SerializeField] private Transform sellListParent;
    private List<SellItemUI> _investItemList = new List<SellItemUI>();

    // Start is called before the first frame update
    void Start()
    {
        UpdateInvestItemList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CancelList()
    {
        UIController.instance.ChooseBigSmall.SetActive(false);
        UIController.instance.SmallPayButton.SetActive(true);
        UIController.instance.SellButton.SetActive(false);


        UIController.instance.BigPayButton.SetActive(false);
        UIController.instance.payButton.SetActive(false);
        UIController.instance.cancelButton.SetActive(true);
        UIController.instance.drawButton.SetActive(false);
    }

    private void UpdateInvestItemList()
    {
        //clear the current list of item
        for (int i = 0; i < _investItemList.Count; i++)
        {
            Destroy(_investItemList[i].gameObject);
        }

        _investItemList.Clear();

        for(int j = 0;j < GameManager.instace.playerList.Count; j++)
        {
            //generate a new list with update info
            for (int i = 0; i < GameManager.instace.playerList[j].InvestList.Count; i++)
            {
                //skip empty 
                if (GameManager.instace.playerList[j].InvestList.Count == 0)
                {
                    continue;
                }

                SellItemUI newItem = Instantiate(SellItemUIPrefab);
                newItem.sellObjFromYourselfParent = this;
                newItem.SetItemName(GameManager.instace.playerList[j].InvestList[i].CardName);
                newItem.SetPriceInItem(GameManager.instace.playerList[j].InvestList[i].sumValue.ToString());
                newItem.transform.SetParent(sellListParent);

                _investItemList.Add(newItem);
            }
        }
       
    }
}
