using System;
using UnityEngine;

public class PlayerInputValidator : MonoBehaviour
{
    private Camera playerCamera; 
    
    public Action<Vector2> OnMove;
    public Action<Vector2> OnMousePosition;

    public Action OnDashTrigger;
    public Action OnAttackTrigger;
    public Action<bool> OnSprint;
    public Action OnFirstAbilityTrigger;
    public Action OnSecondAbilityTrigger;
    public Action OnThirdAbilityTrigger;

    public Action OnDashExitTrigger;
    public Action OnAttackExitTrigger;
    public Action OnFirstAbilityExitTrigger;
    public Action OnSecondAbilityExitTrigger;
    public Action OnThirdAbilityExitTrigger;

    internal void Init(Camera playerCamera)
    {
        this.playerCamera = playerCamera;
    }
    public void DoUpdate()
    {
        ReadMovement();
        ReadDashInput();
        ReadAttackInput();
        ReadFirstAbilityInput();
        ReadSecondAbilityInput();
        ReadMousePosition();
        ReadThirdAbilityInput();
        ReadSprint();
    }
    private void ReadDashInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnDashTrigger?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnDashTrigger?.Invoke();
        }
    }
    private void ReadAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnAttackTrigger?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            OnAttackExitTrigger?.Invoke();
        }
    }
    private void ReadFirstAbilityInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            OnFirstAbilityTrigger?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            OnFirstAbilityExitTrigger?.Invoke();
        }
    }
    private void ReadSecondAbilityInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnSecondAbilityTrigger?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            OnSecondAbilityExitTrigger?.Invoke();
        }
    }
    private void ReadThirdAbilityInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnThirdAbilityTrigger?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            OnThirdAbilityExitTrigger?.Invoke();
        }
    }
    private void ReadMovement()
    {
        Vector2 moveVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        OnMove?.Invoke(moveVector.normalized);
    }
    private void ReadSprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            OnSprint?.Invoke(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            OnSprint?.Invoke(false);
        }

    }
    private void ReadMousePosition()
    {
        OnMousePosition?.Invoke(playerCamera.ScreenToWorldPoint(Input.mousePosition));
    }
}

