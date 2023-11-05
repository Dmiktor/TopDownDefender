using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private PlayerController PlayerController;

    private void Awake()
    {
        PlayerController.Init();
    }

    private void Update()
    {
        PlayerController.DoUpdate();
    }

    private void FixedUpdate()
    {
        PlayerController.DoFixedUpdate();
    }
}
