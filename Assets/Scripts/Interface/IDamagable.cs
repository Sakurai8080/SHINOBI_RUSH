public interface IDamagable
{
    #region public method
    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="amount">ダメージ量</param>
    void Damage(float amount);
    #endregion
}