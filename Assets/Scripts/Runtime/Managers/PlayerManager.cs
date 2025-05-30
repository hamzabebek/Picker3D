using Assets.Scripts.Runtime.Controllers.Player;
using Runtime.Signals;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables
    public byte StageValue;

    internal ForceBallsToPoolCommand ForceCommend;

    #endregion

    #region Serialized Variables
    [SerializeField] private PlayerMovementController movementController;
    [SerializeField] private PlayerMeshController meshController;
    [SerializeField] private PlayerPhysicsController physicsController;
    #endregion

    #region Private Variables

    private PlayerData _data;

    #endregion

    #endregion

    private void Awake()
    {
        _data = GetPlayerData();
        SendDataToControllers();

        Init();
    }

    private PlayerData GetPlayerData()
    {
        return Resources.Load<CD_Player>("Data/CD_Player").Data;
    }

    private void SendDataToControllers()
    {
        movementController.SetData(_data.MovementData);
        meshController.SetData(_data.MeshData);
    }

    private void Init()
    {
        ForceCommend = new ForceBallsToPoolCommand(this, _data.ForceData);
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        InputSignals.Instance.onInputTaken += () => movementController.IsReadyToMove(true);
        InputSignals.Instance.onInputReleased += () => movementController.IsReadyToMove(false);
        InputSignals.Instance.onInputDragged += OnInputDragged;
        UISignals.Instance.onPlay += () => movementController.IsReadyToPlay(true);
        CoreGameSignals.Instance.onLevelSuccessful += () => movementController.IsReadyToPlay(false);
        CoreGameSignals.Instance.onLevelFailed += () => movementController.IsReadyToPlay(false);
        CoreGameSignals.Instance.onStageAreaEntered += () => movementController.IsReadyToPlay(false);
        CoreGameSignals.Instance.onStageAreaSuccessful += OnStageAreaSuccessful;
        CoreGameSignals.Instance.onFinishAreaEntered += OnFinishAreaEntered;
        CoreGameSignals.Instance.onReset += OnReset;
    }

    private void OnInputDragged(HorizontalInputParams inputParams)
    {
        movementController.UpdateInput(inputParams);
    }

    private void OnStageAreaSuccessful(byte value)
    {
        StageValue = ++value;
    }

    private void OnFinishAreaEntered()
    {
        CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
    }

    private void OnReset()
    {
        StageValue = 0;
        movementController.OnReset();
        physicsController.OnReset();
        meshController.OnReset();
    }

    private void UnSubscribeEvents()
    {
        InputSignals.Instance.onInputTaken -= () => movementController.IsReadyToMove(true);
        InputSignals.Instance.onInputReleased -= () => movementController.IsReadyToMove(false);
        InputSignals.Instance.onInputDragged -= OnInputDragged;
        UISignals.Instance.onPlay -= () => movementController.IsReadyToPlay(true);
        CoreGameSignals.Instance.onLevelSuccessful -= () => movementController.IsReadyToPlay(false);
        CoreGameSignals.Instance.onLevelFailed -= () => movementController.IsReadyToPlay(false);
        CoreGameSignals.Instance.onStageAreaEntered -= () => movementController.IsReadyToPlay(false);
        CoreGameSignals.Instance.onStageAreaSuccessful -= OnStageAreaSuccessful;
        CoreGameSignals.Instance.onFinishAreaEntered -= OnFinishAreaEntered;
        CoreGameSignals.Instance.onReset -= OnReset;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

}
