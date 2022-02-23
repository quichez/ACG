using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACG.Inspectors;

public class UnitCellInspector : Inspector
{
    public static UnitCellInspector Instance;
    public Cell CurrentCell { get; private set; }

    [SerializeField] CellTitlePanel _cellTitlePanel;
    [SerializeField] CellTypePanel _cellTypePanel;
    [SerializeField] CellResourcePanel _cellResourcePanel;
    [SerializeField] UnitCellActionsPanel _unitCellActionPanel;
    public UnitCellActionsExtraPanel unitCellActionsExtraPanel => GetComponentInChildren<UnitCellActionsExtraPanel>(true);

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        //DontDestroyOnLoad(this);
    }

    public override void FillPanels(IInspectable ins)
    {
        if(ins is Cell cell)
        {
            _cellTitlePanel.gameObject.SetActive(true);
            _cellTypePanel.gameObject.SetActive(true);
            _cellResourcePanel.gameObject.SetActive(true);
            _unitCellActionPanel.gameObject.SetActive(true);

            _cellTitlePanel.SetCellTitleText(cell.name);
            _cellTypePanel?.SetCellTypeText(cell.GetCellType());
            _unitCellActionPanel.FillPanelWithButtons(cell);
            _cellResourcePanel.SetCellResourceText(cell as ICellResources);
        }
    }

    public override void ClearPanels()
    {
        // Move towards putting this stuff in OnEnable and OnDisable
        _cellTitlePanel.gameObject.SetActive(false);
        _cellTypePanel?.SetCellTypeText("");
        _cellResourcePanel.ClearCellResourceText();
        _unitCellActionPanel.ClearPanelButtons();

        _cellTypePanel.gameObject.SetActive(false);
        _cellResourcePanel.gameObject.SetActive(false);
        _unitCellActionPanel.gameObject.SetActive(false);
        unitCellActionsExtraPanel.gameObject.SetActive(false);

    }

    public void EnableUnitCellActionExtraPanel(bool enb) => unitCellActionsExtraPanel.gameObject.SetActive(enb);
}
