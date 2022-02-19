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
public class SelectorManager : MonoBehaviour
{
    public static SelectorManager Instance { get; private set; }
    
    [SerializeField] ResourcePanel _explorerPanel;
    [SerializeField] VillageEditorPanel _editorPanel;
    [SerializeField] UnitNamePanel _namePanel;
    [SerializeField] SettlementLinkPanel _linkPanel;

    public VillageEditorPanel EditorPanel => _editorPanel;

    public LayerMask CellMask;
    public LayerMask SettlementMask;

    InputMaster inputs;
    GameObject prev = null;

    Coroutine camMovement;   

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

    public void SetCurrentSelection(GameObject gameObject) => prev = gameObject;
}
public interface ISelectable
{
    void OnSelect();
    void OnDeselect();
}