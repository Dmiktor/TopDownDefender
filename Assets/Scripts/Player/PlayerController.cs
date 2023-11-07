using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerInputValidator playerInputValidator;
    [SerializeField] private PlayerCameraTargetController playerCameraTargetController;
    [SerializeField] private Camera playerCamera;


    public PlayerInputValidator PlayerInputValidator => playerInputValidator;

    internal void Init()
    {
        playerMovement.Subscribe(this);
        PlayerInputValidator.Init(playerCamera);
        playerCameraTargetController.Subscribe(this);
    }
    internal void UnSubscribe()
    {
        playerCameraTargetController.UnSubscribe(this);
        playerMovement.UnSubscribe(this);
    }

    internal void DoFixedUpdate()
    {
        playerMovement.DoFixedUpdate();
        playerCameraTargetController.DoFixedUpdate();
    }

    internal void DoUpdate()
    {
        playerInputValidator.DoUpdate();
    }


}
