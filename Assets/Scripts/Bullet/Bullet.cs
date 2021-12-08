using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed; // поля для ввода скорости пули
    [SerializeField] private float _bounceForse; //сила отскока пули
    [SerializeField] private float _bounceRadius;

    private Vector3 _moveDiraction; // направление пули

    private void Start()
    {
        _moveDiraction = Vector3.forward; // задаем направление 
    }

    private void Update()
    {
        transform.Translate(_moveDiraction * _speed * Time.deltaTime); // двигаем пулую
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Block block)) // проверяем столькновение с объектом блок
        {
            block.Break(); // вызываем у объекта блок меторд Break
            Destroy(gameObject); // разрушаем саму пулю
        }
        if (other.TryGetComponent(out Obstracle obstracle))
        {
            Bounce();
        }
    }

    private void Bounce()
    {
        _moveDiraction = Vector3.back + Vector3.up;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.AddExplosionForce(_bounceForse, transform.position + new Vector3(0, -1, 1), _bounceRadius);
    }
}
