using System;
using UnityEngine;

public class PlayerAbilityController : MonoBehaviour
{
    [SerializeField] private PlayerAbility dash;
    [SerializeField] private PlayerAbility attack;
    [SerializeField] private PlayerAbility firstAbility;
    [SerializeField] private PlayerAbility secondAbility;
    [SerializeField] private PlayerAbility thirdAbility;
    private PlayerController controller;

    public PlayerAbility Dash { get; set; }
    public PlayerAbility Attack { get; set; }
    public PlayerAbility FirstAbility { get; set; }
    public PlayerAbility SecondAbility { get; set; }
    public PlayerAbility ThirdAbility { get; set; }

    public void init(PlayerController playerController)
    {
        controller = playerController;

        Dash = Instantiate(dash);
        Attack = Instantiate(attack);
        FirstAbility = Instantiate(firstAbility);
        SecondAbility = Instantiate(secondAbility);
        ThirdAbility = Instantiate(thirdAbility);

        Subscribe();
    }
    public void Subscribe()
    {
        Dash.Init(controller);
        Attack.Init(controller);
        FirstAbility.Init(controller);
        SecondAbility.Init(controller);
        ThirdAbility.Init(controller);
    }

    public void UnSubscribe()
    {
        Dash.ExitAbility();
        Attack.ExitAbility();
        FirstAbility.ExitAbility();
        SecondAbility.ExitAbility();
        ThirdAbility.ExitAbility();
    }
}
