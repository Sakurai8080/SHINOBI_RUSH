using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="MyScritable/EnemyData")]
public class EnemyData : ScriptableObject
{
    #region property
    public EnemyType EnemyType => _enemyType;
    public int HP => _hp;
    public float AttackAmount => _attackAmount;
    #endregion

    #region serialize
    [Header("変数")]
    [Tooltip("敵の種類")]
    [SerializeField]
    private EnemyType _enemyType = default;

    [Tooltip("敵のHP")]
    [SerializeField]
    private int _hp = 10;

    [Tooltip("敵の攻撃力")]
    [SerializeField]
    private float _attackAmount = 1;
    #endregion
}

public enum EnemyType
{
    Wave1_Enemy1,
    Wave1_Enemy2,
    Wave2_Enemy1,
    Wave2_Enemy2,
    Wave3_Enemy1,
    Wave3_Enemy2,
}