using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACG.Inspectors;

public class CellInspector : Inspector
{
    public static CellInspector Instance;
    [SerializeField] CellTitlePanel _cellTitlePanel;
    [SerializeField] CellActionPanel _cellActionPanel;
    [SerializeField] CellTypePanel _cellTypePanel;
    [SerializeField] CellResourcePanel _cellResourcePanel;


    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        //DontDestroyOnLoad(this);
    }

    public override void Fill(IInspectable ins)
    {
        if(ins is Cell cell)
        {
            _cellTitlePanel.gameObject.SetActive(true);
            _cellTypePanel.gameObject.SetActive(true);
            _cellResourcePanel.gameObject.SetActive(true);
            _cellActionPanel.gameObject.SetActive(true);

            _cellTitlePanel.SetCellTitleText(cell.name);
            _cellTypePanel?.SetCellTypeText(cell.GetCellType());
            _cellActionPanel.FillPanelWithButtons(cell);
            _cellResourcePanel.SetCellResourceText(cell as ICellResources);
        }
    }

    public override void Clear()
    {
        _cellTitlePanel.SetCellTitleText("");
        _cellTypePanel?.SetCellTypeText("");
        _cellResourcePanel.ClearCellResourceText();
        _cellActionPanel.ClearPanelButtons();

        _cellTitlePanel.gameObject.SetActive(false);
        _cellTypePanel.gameObject.SetActive(false);
        _cellResourcePanel.gameObject.SetActive(false);
        _cellActionPanel.gameObject.SetActive(false);

    }

}
