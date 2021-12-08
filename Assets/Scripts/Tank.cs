using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tank : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint; // точка выстрела
    [SerializeField] private Bullet _bulletTempalate; // prefab пули
    [SerializeField] private float _delayBetweemShoots; // задержка между выстрелами
    [SerializeField] private float _recoilDistance;

    private float _timeAfterShoot; // время с последнего выстрела

    private void Update()
    {
        _timeAfterShoot += Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            if(_timeAfterShoot > _delayBetweemShoots)
            {
                Shoot();
                transform.DOMoveZ(transform.position.z - _recoilDistance, _delayBetweemShoots / 2).SetLoops(2, LoopType.Yoyo);
                _timeAfterShoot = 0;
            }
        }

    }

    private void Shoot()
    {
        Instantiate(_bulletTempalate, _shootPoint.position, Quaternion.identity);
    }
}
