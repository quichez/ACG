//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.2.0
//     from Assets/Scripts/InputMaster.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputMaster : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""SettlementSelector"",
            ""id"": ""db1e16c4-20bb-4937-89db-051e8de554d5"",
            ""actions"": [
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""82ba8d05-ea70-4c01-884a-6637231d0a1e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Position"",
                    ""type"": ""PassThrough"",
                    ""id"": ""083efab1-37b3-4ada-8e79-3da3cba042de"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0f2bcf5b-da84-40ee-bab3-96a6e256fd48"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB_M"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""00f3e2e5-8213-416b-ac37-d7c4e2d579c2"",
                    ""path"": ""<Pointer>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB_M"",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KB_M"",
            ""bindingGroup"": ""KB_M"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // SettlementSelector
        m_SettlementSelector = asset.FindActionMap("SettlementSelector", throwIfNotFound: true);
        m_SettlementSelector_Select = m_SettlementSelector.FindAction("Select", throwIfNotFound: true);
        m_SettlementSelector_Position = m_SettlementSelector.FindAction("Position", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // SettlementSelector
    private readonly InputActionMap m_SettlementSelector;
    private ISettlementSelectorActions m_SettlementSelectorActionsCallbackInterface;
    private readonly InputAction m_SettlementSelector_Select;
    private readonly InputAction m_SettlementSelector_Position;
    public struct SettlementSelectorActions
    {
        private @InputMaster m_Wrapper;
        public SettlementSelectorActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Select => m_Wrapper.m_SettlementSelector_Select;
        public InputAction @Position => m_Wrapper.m_SettlementSelector_Position;
        public InputActionMap Get() { return m_Wrapper.m_SettlementSelector; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SettlementSelectorActions set) { return set.Get(); }
        public void SetCallbacks(ISettlementSelectorActions instance)
        {
            if (m_Wrapper.m_SettlementSelectorActionsCallbackInterface != null)
            {
                @Select.started -= m_Wrapper.m_SettlementSelectorActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_SettlementSelectorActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_SettlementSelectorActionsCallbackInterface.OnSelect;
                @Position.started -= m_Wrapper.m_SettlementSelectorActionsCallbackInterface.OnPosition;
                @Position.performed -= m_Wrapper.m_SettlementSelectorActionsCallbackInterface.OnPosition;
                @Position.canceled -= m_Wrapper.m_SettlementSelectorActionsCallbackInterface.OnPosition;
            }
            m_Wrapper.m_SettlementSelectorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Position.started += instance.OnPosition;
                @Position.performed += instance.OnPosition;
                @Position.canceled += instance.OnPosition;
            }
        }
    }
    public SettlementSelectorActions @SettlementSelector => new SettlementSelectorActions(this);
    private int m_KB_MSchemeIndex = -1;
    public InputControlScheme KB_MScheme
    {
        get
        {
            if (m_KB_MSchemeIndex == -1) m_KB_MSchemeIndex = asset.FindControlSchemeIndex("KB_M");
            return asset.controlSchemes[m_KB_MSchemeIndex];
        }
    }
    public interface ISettlementSelectorActions
    {
        void OnSelect(InputAction.CallbackContext context);
        void OnPosition(InputAction.CallbackContext context);
    }
}