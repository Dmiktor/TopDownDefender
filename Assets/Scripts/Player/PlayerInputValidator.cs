using System;
using UnityEngine;

public class PlayerInputValidator : MonoBehaviour
{
    private Camera playerCamera; 
    
    public Action OnDash;
    public Action<Vector2> OnMove;
    public Action<Vector2> OnMousePosition;

    internal void Init(Camera playerCamera)
    {
        this.playerCamera = playerCamera;
    }
    public void DoUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnDash?.Invoke();
        }

        Vector2 moveVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        OnMove?.Invoke(moveVector.normalized);

        OnMousePosition?.Invoke(playerCamera.ScreenToWorldPoint(Input.mousePosition));
    }

    
}

