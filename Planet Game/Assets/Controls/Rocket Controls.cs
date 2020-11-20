// GENERATED AUTOMATICALLY FROM 'Assets/Controls/Rocket Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @RocketControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @RocketControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Rocket Controls"",
    ""maps"": [
        {
            ""name"": ""RocketFlight"",
            ""id"": ""cacef559-9837-4943-9a66-1540325e8e16"",
            ""actions"": [
                {
                    ""name"": ""Turn Left"",
                    ""type"": ""Button"",
                    ""id"": ""82b6ef35-3fb4-4ecf-ad1a-b0b19327960f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Turn Right"",
                    ""type"": ""Button"",
                    ""id"": ""8bab6671-0148-4607-b5ee-dc90a4305a4f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Forward"",
                    ""type"": ""Button"",
                    ""id"": ""2de98c28-9433-488d-913a-56313642dc9e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Backward"",
                    ""type"": ""Button"",
                    ""id"": ""8038592b-d5e4-4e5d-823a-c0533a4494e4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""00b3880f-9ba0-4e83-b36f-5ef67f2dd2c1"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f83c9f9f-5b00-44f8-94a0-7718b6608beb"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""854f1fc7-2f56-47d2-ac6f-01aed64335e8"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""New control scheme"",
                    ""action"": ""Turn Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""58061275-29f3-47ea-aae2-3508a1b52fcc"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0122035a-0faa-41d9-a7b3-8fadf0f4f8a5"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""New control scheme"",
                    ""action"": ""Forward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c65dfa36-3632-4ee3-a364-a7b399911bd4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Forward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bf9ba32f-6e29-493b-987a-a8f7b6bbc143"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""New control scheme"",
                    ""action"": ""Backward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""228f83eb-9fb1-4e61-af93-2eec4dd874bd"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Backward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""New control scheme"",
            ""bindingGroup"": ""New control scheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // RocketFlight
        m_RocketFlight = asset.FindActionMap("RocketFlight", throwIfNotFound: true);
        m_RocketFlight_TurnLeft = m_RocketFlight.FindAction("Turn Left", throwIfNotFound: true);
        m_RocketFlight_TurnRight = m_RocketFlight.FindAction("Turn Right", throwIfNotFound: true);
        m_RocketFlight_Forward = m_RocketFlight.FindAction("Forward", throwIfNotFound: true);
        m_RocketFlight_Backward = m_RocketFlight.FindAction("Backward", throwIfNotFound: true);
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

    // RocketFlight
    private readonly InputActionMap m_RocketFlight;
    private IRocketFlightActions m_RocketFlightActionsCallbackInterface;
    private readonly InputAction m_RocketFlight_TurnLeft;
    private readonly InputAction m_RocketFlight_TurnRight;
    private readonly InputAction m_RocketFlight_Forward;
    private readonly InputAction m_RocketFlight_Backward;
    public struct RocketFlightActions
    {
        private @RocketControls m_Wrapper;
        public RocketFlightActions(@RocketControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @TurnLeft => m_Wrapper.m_RocketFlight_TurnLeft;
        public InputAction @TurnRight => m_Wrapper.m_RocketFlight_TurnRight;
        public InputAction @Forward => m_Wrapper.m_RocketFlight_Forward;
        public InputAction @Backward => m_Wrapper.m_RocketFlight_Backward;
        public InputActionMap Get() { return m_Wrapper.m_RocketFlight; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RocketFlightActions set) { return set.Get(); }
        public void SetCallbacks(IRocketFlightActions instance)
        {
            if (m_Wrapper.m_RocketFlightActionsCallbackInterface != null)
            {
                @TurnLeft.started -= m_Wrapper.m_RocketFlightActionsCallbackInterface.OnTurnLeft;
                @TurnLeft.performed -= m_Wrapper.m_RocketFlightActionsCallbackInterface.OnTurnLeft;
                @TurnLeft.canceled -= m_Wrapper.m_RocketFlightActionsCallbackInterface.OnTurnLeft;
                @TurnRight.started -= m_Wrapper.m_RocketFlightActionsCallbackInterface.OnTurnRight;
                @TurnRight.performed -= m_Wrapper.m_RocketFlightActionsCallbackInterface.OnTurnRight;
                @TurnRight.canceled -= m_Wrapper.m_RocketFlightActionsCallbackInterface.OnTurnRight;
                @Forward.started -= m_Wrapper.m_RocketFlightActionsCallbackInterface.OnForward;
                @Forward.performed -= m_Wrapper.m_RocketFlightActionsCallbackInterface.OnForward;
                @Forward.canceled -= m_Wrapper.m_RocketFlightActionsCallbackInterface.OnForward;
                @Backward.started -= m_Wrapper.m_RocketFlightActionsCallbackInterface.OnBackward;
                @Backward.performed -= m_Wrapper.m_RocketFlightActionsCallbackInterface.OnBackward;
                @Backward.canceled -= m_Wrapper.m_RocketFlightActionsCallbackInterface.OnBackward;
            }
            m_Wrapper.m_RocketFlightActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TurnLeft.started += instance.OnTurnLeft;
                @TurnLeft.performed += instance.OnTurnLeft;
                @TurnLeft.canceled += instance.OnTurnLeft;
                @TurnRight.started += instance.OnTurnRight;
                @TurnRight.performed += instance.OnTurnRight;
                @TurnRight.canceled += instance.OnTurnRight;
                @Forward.started += instance.OnForward;
                @Forward.performed += instance.OnForward;
                @Forward.canceled += instance.OnForward;
                @Backward.started += instance.OnBackward;
                @Backward.performed += instance.OnBackward;
                @Backward.canceled += instance.OnBackward;
            }
        }
    }
    public RocketFlightActions @RocketFlight => new RocketFlightActions(this);
    private int m_NewcontrolschemeSchemeIndex = -1;
    public InputControlScheme NewcontrolschemeScheme
    {
        get
        {
            if (m_NewcontrolschemeSchemeIndex == -1) m_NewcontrolschemeSchemeIndex = asset.FindControlSchemeIndex("New control scheme");
            return asset.controlSchemes[m_NewcontrolschemeSchemeIndex];
        }
    }
    public interface IRocketFlightActions
    {
        void OnTurnLeft(InputAction.CallbackContext context);
        void OnTurnRight(InputAction.CallbackContext context);
        void OnForward(InputAction.CallbackContext context);
        void OnBackward(InputAction.CallbackContext context);
    }
}
