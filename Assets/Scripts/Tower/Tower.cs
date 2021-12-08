using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TowerBuilder))]
public class Tower : MonoBehaviour
{
    private TowerBuilder _towerBuilder;

    private List<Block> _blocks;

    public event UnityAction<int> SizeUpdated;
    private void Start()
    {
        _towerBuilder = GetComponent<TowerBuilder>();
        _blocks = _towerBuilder.Build(); // у компонента TowerBuilder вызываем метод Build

        foreach (var block in _blocks)
        {
            block.BulletHit += OnBulletHit;
        }
        
        SizeUpdated?.Invoke(_blocks.Count);
    }

    private void OnBulletHit(Block hiteBlock)
    {
        hiteBlock.BulletHit -= OnBulletHit;

        _blocks.Remove(hiteBlock);

        foreach (var block in _blocks)
        {
            block.transform.position = new Vector3(block.transform.position.x, block.transform.position.y - block.transform.localScale.y, block.transform.position.z); // так как высота модели 0,5 то приходитьс€ указыват сдвиг в ручную. ¬ случаи изменнени модели заменить высоту
        }

        SizeUpdated?.Invoke(_blocks.Count);
    }

}
