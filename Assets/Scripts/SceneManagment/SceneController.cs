using MoreMountains.Tools;
using Unity.VisualScripting;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    [SerializeField] private PlayerController playerController;
    [SerializeField] private EnemyManager enemyManager;
    public static SceneController Instance { get; private set; }

    public EnemyManager EnemyManager => enemyManager;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        enemyManager.Init();
        playerController.Init();
    }
    private void OnDisable()
    {
        enemyManager.Exit();
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
