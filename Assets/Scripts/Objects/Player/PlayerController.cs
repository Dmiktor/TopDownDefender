using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerInputValidator playerInputValidator;
    [SerializeField] private PlayerCameraTargetController playerCameraTargetController;
    [SerializeField] private PlayerAbilityController playerAbilityController;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerLookTarget playerLookTarget;
    public PlayerInputValidator PlayerInputValidator => playerInputValidator;
    public PlayerMovement PlayerMovement => playerMovement;

    internal void Init()
    {
        playerHealth.Init();
        playerAbilityController.init(this);
        PlayerMovement.Subscribe(this);
        PlayerInputValidator.Init(playerCamera);
        playerCameraTargetController.Subscribe(this);
        playerLookTarget.Subscribe(this);
    }
    internal void UnSubscribe()
    {
        playerCameraTargetController.UnSubscribe(this);
        playerAbilityController.UnSubscribe();
        PlayerMovement.UnSubscribe(this);
        playerLookTarget.UnSubscribe(this);
    }

    internal void DoFixedUpdate()
    {
        PlayerMovement.DoFixedUpdate();
    }

    internal void DoUpdate()
    {
        playerInputValidator.DoUpdate();
    }
}
