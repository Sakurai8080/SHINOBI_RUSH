using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    [SerializeField]
    private float _scaleChangeAmount = 1.0f;
    #endregion

    #region private
    private Rigidbody _rb;

    private float _moveSpeed = 0.5f;

    private float _currentAttackAmount = 1.0f;

    private Coroutine currentCoroutine = default;

    private Vector3 _initialScale;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _initialScale = transform.localScale;
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTag.Enemy))
        {
            IDamagable target = other.GetComponent<IDamagable>();
            target.Damage(_currentAttackAmount);
            currentCoroutine = StartCoroutine(ExplosionCoroutine());
        }
    }
    #endregion

    #region public method
    public void SetAttackAmount(float amount)
    {
        _currentAttackAmount += amount;
    }

    public void SetVelocity(Vector3 enemyDir)
    {
        _rb.velocity = enemyDir * _moveSpeed;
    }
    #endregion

    #region private method
    #endregion


    #region coroutine method
    private IEnumerator ExplosionCoroutine()
    {
        Debug.Log("爆発コルーチンスタート");
        Vector3 currentScale = _initialScale;
        float targetScaleMagnitude = _initialScale.magnitude * 10; 

        while(currentScale.magnitude <= targetScaleMagnitude)
        {
            currentScale += _initialScale * _scaleChangeAmount * Time.deltaTime;
            transform.localScale = currentScale;
        }
        yield return null;
        gameObject.SetActive(false);
        transform.localScale = _initialScale;
    }
    #endregion
}