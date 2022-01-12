using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestSettlementSelector : MonoBehaviour
{
    public VillageExplorerPanel ExplorerPanel;
    public VillageEditorPanel EditorPanel;
    public TestSettlementName NamePanel;

    public LayerMask CellMask;

    InputMaster inputs;
    GameObject prev = null;
    bool IsSelectorOverUI;

    private void Awake()
    {
        inputs = new InputMaster();        
    }

    private void Update()
    {
        IsSelectorOverUI = EventSystem.current.IsPointerOverGameObject();        
    }

    private void OnEnable()
    {
        inputs.SettlementSelector.Select.performed += Select_performed;
        inputs.SettlementSelector.Enable();
    }

    private void OnDisable()
    {
        inputs.SettlementSelector.Select.performed -= Select_performed;
        inputs.SettlementSelector.Disable();
    }

    private void Select_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Ray ray = Camera.main.ScreenPointToRay(inputs.SettlementSelector.Position.ReadValue<Vector2>());
        
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.TryGetComponent(out Settlement settlement))
            {
                Renderer hitRenderer = hit.collider.gameObject.GetComponent<Renderer>();

                if (hit.collider.gameObject == prev)
                {
                    NamePanel.SetText("");
                    EditorPanel.Clear();
                    ExplorerPanel.Clear();

                    hitRenderer.material.color = Color.green;
                    if (settlement is ILinkableSettlement) ClearHighlightedCellsWithinRange(settlement);

                    prev = null;
                }
                else
                {
                    if (prev)
                    {
                        prev.GetComponent<Renderer>().material.color = Color.green;
                        if (prev.TryGetComponent(out Settlement settlement1)) ClearHighlightedCellsWithinRange(settlement1);
                    }



                    hitRenderer.material.color = Color.red;
                    NamePanel.SetText(settlement.Name);
                    ExplorerPanel.SetPanelInfo(settlement);
                    EditorPanel.SetPanelInformation(settlement);

                    if (settlement is ILinkableSettlement) HighlightCellsWithinRange(settlement);                                                     
                    prev = hit.collider.gameObject;

                }
                
                
            }
            else
            {
                if (IsSelectorOverUI) return;

                if (prev)
                {
                    prev.GetComponent<Renderer>().material.color = Color.green;
                    if (prev.TryGetComponent(out Settlement settlement1)) ClearHighlightedCellsWithinRange(settlement1);
                }
                NamePanel.SetText("");
                ExplorerPanel.Clear();
                EditorPanel.Clear();
                prev = null;
            }
        }
    }

    private void HighlightCellsWithinRange(Settlement settlement)
    {
        if (settlement is ILinkableSettlement linkable) 
        {
            Collider[] cellsInRange = Physics.OverlapBox(settlement.transform.position, Vector3.one * linkable.MaximumLinkableDistance, Quaternion.identity, CellMask);
            foreach (var cell in cellsInRange)
            {
                Debug.Log("fuck");
                if (cell.TryGetComponent(out IHighlightWithinRange hl)) hl.Highlight();
            }
        }
    }

    private void ClearHighlightedCellsWithinRange(Settlement settlement)
    {
        if (settlement is ILinkableSettlement linkable)
        {
            Collider[] cellsInRange = Physics.OverlapBox(settlement.transform.position, Vector3.one * linkable.MaximumLinkableDistance, Quaternion.identity, CellMask);
            foreach (var cell in cellsInRange)
            {
                if (cell.TryGetComponent(out IHighlightWithinRange hl)) hl.UnHighlight();
            }
        }
    }
}
