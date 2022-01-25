using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinkSettlementActionPanel : MonoBehaviour
{
    [SerializeField] CellActionButton _actionButton;

    private void OnEnable()
    {
        Debug.Log("enabled");
        if(SettlementInspector.Instance.CurrentSettlement is ILinkableSettlement linkable)
        {
            Debug.Log("linkable");
            List<ILinkableSettlement> list = linkable.FindLinkableSettlements();

            foreach (ILinkableSettlement link in list)
            {
                Debug.Log((link as Settlement).Name);
                if (linkable.LinkedSettlements.Contains(link)) { Debug.Log("got it"); continue; }

                CellActionButton clone = Instantiate(_actionButton, transform);
                clone.SetCellAction(() => LinkSettlements(linkable,link,clone),(link as Settlement).Name);
            }
        }
    }

    /// <summary>
    /// This is a one-way link from current ILinkableSettlement to the target ILinkableSettlement.
    /// </summary>
    /// <param name="linkable"></param>
    /// <param name="linkTo"></param>
    private void LinkSettlements(ILinkableSettlement linkable, ILinkableSettlement linkTo, CellActionButton button = null)
    {
        linkable.LinkSettlementTo(linkTo);
        Destroy(button.gameObject);
        TestSettlementSelector.Instance.EnableSettlementSelectorPanels(true);
    }



    private void OnDisable()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
