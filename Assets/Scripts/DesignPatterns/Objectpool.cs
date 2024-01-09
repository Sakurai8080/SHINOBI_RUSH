using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;
using UniRx.Triggers;

public class Objectpool<T> where T : Object
{
    #region property
    #endregion

    #region serialize
    #endregion

    #region private
    private Queue<T> _pool;

    private T _object;

    private Transform _parent;
    #endregion

    #region Constant
    private const uint DEFAULT_LIMIT = 50;
    #endregion

    #region Event
    private Subject<Unit> _returnSubject = new Subject<Unit>();
    #endregion

    #region public method
    public Objectpool(T obj, Transform parant)
    {
        _pool = new Queue<T>();
        _object = obj;
        _parent = parant;
    }

    public T Rent(uint limit = DEFAULT_LIMIT)
    {
        if (_pool.Count >0)
        {
            return _pool.Dequeue();
        }
        else
        {
            if (_parent.childCount >= limit)
            {
                return null;
            }

            var obj = Object.Instantiate(_object, _parent);

            //インターフェース取得
            try
            {
                var pool = obj as IPoolable;

                //非アクティブになったらQueueに戻る処理登録
                pool.InactiveObserver
                    .Subscribe(_ =>
                    {
                        _pool.Enqueue(obj);
                    });

                _returnSubject.Subscribe(_ => pool.ReturnPool());
            }
            catch
            {
                Debug.LogError($"インターフェースが継承されていません。オブジェクト名:{obj.name}");
            }
            finally
            {
                //Debug.LogError($"インターフェースが継承されていません。オブジェクト名:{obj.name}");
            }
            return obj;
        }
    }

    /// <summary>
    /// 使用中のオブジェクトを全てプールに戻す通知
    /// </summary>
    public void Return()
    {
        _returnSubject.OnNext(Unit.Default);
    }
    #endregion

    #region private method
    #endregion
}