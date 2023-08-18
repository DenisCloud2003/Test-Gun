using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public event EventHandler OnInteractionAction;
    
    PlayerActionInput actionInput;

    private void Awake() {
        actionInput = new PlayerActionInput();

        actionInput.Player.Enable();
        actionInput.Player.Interaction.performed += Interaction_performed;
    }

    private void Interaction_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractionAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = actionInput.Player.Movement.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
