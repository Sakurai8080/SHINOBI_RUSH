using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

/// <summary>
/// プレイヤーに関わる情報を操作するクラス
/// </summary>
public class PlayerController : SingletonMonoBehaviour<PlayerController>, IDamagable
{
    #region property
    public PlayerHealth Health => _health;
    public PlayerStatus Status => _status;
    public bool onAvaterd { get; private set; }
    public bool OnDown { get; private set; }
    public Vector3 InitPlayerPos => _initPlayerPos;
    #endregion

    #region serialize
    
    [SerializeField]
    private SkinnedMeshRenderer _playerRenderer;
    #endregion

    #region private
    private PlayerHealth _health;
    private PlayerStatus _status;
    private PlayerMove _move;
    private bool _isDead = false;
    private Vector3 _initPlayerPos = new Vector3();
    private Tween _currentTween;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {
        _health = GetComponent<PlayerHealth>();
        _move = GetComponent<PlayerMove>();
    }

    private void Start()
    {
        _initPlayerPos = transform.position;

        _move.OnAvaterd
             .TakeUntilDestroy(this)
             .Subscribe(onAvaterdValue =>
             {
                 onAvaterd = onAvaterdValue;
             });

        _move.OnDown
             .TakeUntilDestroy(this)
             .Subscribe(onDownValue =>
             {
                 OnDown = onDownValue;
             });
    }
    #endregion

    #region public method
    public void Damage(float amount)
    {
        if (_isDead)
            return;

        if (_currentTween == null)
        {
            _currentTween = _playerRenderer.material.DOColor(Color.red, 0.05f)
                                                    .SetEase(Ease.InOutSine)
                                                    .SetLoops(7, LoopType.Yoyo)
                                                    .OnComplete(() =>
                                                    {
                                                        _playerRenderer.material.color = Color.white;
                                                        _currentTween = null;
                                                    });
                                     
        }

        if (_health.Damage(amount))
        {
            if (!_isDead)
                _isDead = true;
        }
    }
    #endregion

    #region private method
    #endregion
}