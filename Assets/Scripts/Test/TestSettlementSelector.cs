using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// The class name is a misnomer at the moment.
/// This should be called something like SelectorManager or something.
/// This class manages the UIs that appear when an object in game is clicked.
/// </summary>
public class TestSettlementSelector : MonoBehaviour
{
    public static TestSettlementSelector Instance { get; private set; }
    
    [SerializeField] VillageExplorerPanel _explorerPanel;
    [SerializeField] VillageEditorPanel _editorPanel;
    [SerializeField] TestSettlementName _namePanel;
    [SerializeField] SettlementLinkPanel _linkPanel;

    public VillageExplorerPanel ExplorerPanel => _explorerPanel;
    public VillageEditorPanel EditorPanel => _editorPanel;
    public TestSettlementName NamePanel => _namePanel;
    public SettlementLinkPanel LinkPanel => _linkPanel;


    public LayerMask CellMask;
    public LayerMask SettlementMask;

    InputMaster inputs;
    GameObject prev = null;

    Coroutine camMovement;
    public void EnableSettlementSelectorPanels(bool active = true)
    {
        ExplorerPanel.gameObject.SetActive(active);
        EditorPanel.gameObject.SetActive(active);
        NamePanel.gameObject.SetActive(active);
        LinkPanel.gameObject.SetActive(active);
    }

    bool IsSelectorOverUI;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

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
        if (IsSelectorOverUI) return;
        

        Ray ray = Camera.main.ScreenPointToRay(inputs.SettlementSelector.Position.ReadValue<Vector2>());
        
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject hitObj = hit.collider.gameObject;
            if (camMovement != null) StopCoroutine(camMovement);
            camMovement = StartCoroutine(CameraManager.Instance.MoveCamera(hit.transform));

            if (hitObj.TryGetComponent(out ISelectable selectable)) 
            {
                if (hitObj == prev)
                {
                    selectable.OnDeselect();
                    prev = null;
                }
                else if (prev)
                {
                    if (prev.TryGetComponent(out ISelectable prevSelect))
                    {
                        prevSelect.OnDeselect();
                    }
                    selectable.OnSelect();
                    prev = hitObj;
                }
                else
                {
                    selectable.OnSelect();
                    prev = hitObj;
                }
            }
            else 
            {
                Debug.Log("How did I get here?");
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
public interface ISelectable
{
    void OnSelect();
    void OnDeselect();
}