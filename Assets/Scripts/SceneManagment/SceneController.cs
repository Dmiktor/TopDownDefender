using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private void Awake()
    {
        playerController.Init();
    }
    private void OnDisable()
    {
        playerController.UnSubscribe();
    }
    private void Update()
    {
        playerController.DoUpdate();
    }

    private void FixedUpdate()
    {
        playerController.DoFixedUpdate();
    }
}
