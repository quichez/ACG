using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLinkExtraPanel : MonoBehaviour
{
    [SerializeField] CellActionButton _actionButton;

    private void OnEnable()
    {
        if (UnitInspector.Instance.CurrentUnit is ILinkableSettlement linkable)
        {
            List<ILinkableSettlement> list = linkable.FindLinkableSettlements();

            foreach (ILinkableSettlement link in list)
            {
                if (linkable.LinkedSettlements.Contains(link)) { Debug.Log("got it"); continue; }

                CellActionButton clone = Instantiate(_actionButton, transform);
                //clone.SetCellAction(() => LinkSettlements(linkable, link, clone), (link as Settlement).Name);
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
        if (linkable.IsSingleLinkable)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
        else
        {
            Destroy(button.gameObject);
        }

        SettlementInspector.Instance.EnableSettlementInspectorPanels();
        var link = new SettlementLink(linkTo, (int)Vector3.Magnitude((linkTo as Settlement).transform.position - (linkable as Settlement).transform.position));
        linkable.LinkSettlementTo(link);
    }

    private void OnDisable()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
