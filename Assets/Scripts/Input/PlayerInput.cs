// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""InGame"",
            ""id"": ""7d47dacc-d3d6-4223-8334-362135b39ee9"",
            ""actions"": [
                {
                    ""name"": ""InputPosition"",
                    ""type"": ""Value"",
                    ""id"": ""ff031c84-e787-4de8-85a4-0117ddd58864"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Press"",
                    ""type"": ""Button"",
                    ""id"": ""70faca7d-5ecc-43f8-86f2-c1b8b8d35810"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DoublePress"",
                    ""type"": ""Button"",
                    ""id"": ""40cb14e4-6b42-4a85-b3b4-eb3c34e196e8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6e31f037-89c3-4065-9d5c-579c478c25a2"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""InputPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4b2dcfa-c9ae-4543-a37b-8300ed9b6b1d"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""InputPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e18f20a1-5f07-4344-ae95-ccf816ed5427"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a47bdef1-609e-4dd0-8537-a92559f7df85"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0fa6df82-db19-4457-b064-4697f1e02712"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""DoublePress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e417573c-2190-4715-ba71-d9c1466e2544"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": ""MultiTap"",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""DoublePress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse"",
            ""bindingGroup"": ""Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // InGame
        m_InGame = asset.FindActionMap("InGame", throwIfNotFound: true);
        m_InGame_InputPosition = m_InGame.FindAction("InputPosition", throwIfNotFound: true);
        m_InGame_Press = m_InGame.FindAction("Press", throwIfNotFound: true);
        m_InGame_DoublePress = m_InGame.FindAction("DoublePress", throwIfNotFound: true);
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

    // InGame
    private readonly InputActionMap m_InGame;
    private IInGameActions m_InGameActionsCallbackInterface;
    private readonly InputAction m_InGame_InputPosition;
    private readonly InputAction m_InGame_Press;
    private readonly InputAction m_InGame_DoublePress;
    public struct InGameActions
    {
        private @PlayerInput m_Wrapper;
        public InGameActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @InputPosition => m_Wrapper.m_InGame_InputPosition;
        public InputAction @Press => m_Wrapper.m_InGame_Press;
        public InputAction @DoublePress => m_Wrapper.m_InGame_DoublePress;
        public InputActionMap Get() { return m_Wrapper.m_InGame; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InGameActions set) { return set.Get(); }
        public void SetCallbacks(IInGameActions instance)
        {
            if (m_Wrapper.m_InGameActionsCallbackInterface != null)
            {
                @InputPosition.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnInputPosition;
                @InputPosition.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnInputPosition;
                @InputPosition.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnInputPosition;
                @Press.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnPress;
                @Press.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnPress;
                @Press.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnPress;
                @DoublePress.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnDoublePress;
                @DoublePress.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnDoublePress;
                @DoublePress.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnDoublePress;
            }
            m_Wrapper.m_InGameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @InputPosition.started += instance.OnInputPosition;
                @InputPosition.performed += instance.OnInputPosition;
                @InputPosition.canceled += instance.OnInputPosition;
                @Press.started += instance.OnPress;
                @Press.performed += instance.OnPress;
                @Press.canceled += instance.OnPress;
                @DoublePress.started += instance.OnDoublePress;
                @DoublePress.performed += instance.OnDoublePress;
                @DoublePress.canceled += instance.OnDoublePress;
            }
        }
    }
    public InGameActions @InGame => new InGameActions(this);
    private int m_MouseSchemeIndex = -1;
    public InputControlScheme MouseScheme
    {
        get
        {
            if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
            return asset.controlSchemes[m_MouseSchemeIndex];
        }
    }
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    public interface IInGameActions
    {
        void OnInputPosition(InputAction.CallbackContext context);
        void OnPress(InputAction.CallbackContext context);
        void OnDoublePress(InputAction.CallbackContext context);
    }
}
