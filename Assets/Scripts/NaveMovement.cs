using DG.Tweening;
using UnityEngine;

public class NaveMovement : MonoBehaviour
{
    /*
     *  Mudar isso pra um level manager
     *  Para "carregar" um bloco na fila, o sistema pega algum elemento da tabela aleatoriamente
     */
    // public enum Tipo {Vaca, Galinha}
    //
    // public class Dropavel
    // {
    //     public Tipo tipo;
    //     public GameObject tipoPrefab;
    //
    //      public int spawnChance;
    // }
    //
    // public Dropavel[] tabelaDeAnimais;

    [Header("Movement")]
    [SerializeField] private Transform startPoint, endPoint;
    [SerializeField] private float shipVelocity;
    private int _direction;

    private Vector3 GetDirection() => _direction == 1 ? startPoint.position : endPoint.position;
    private void Start()
    {
        transform.position = startPoint.position;
        _direction = -1;
        MoveTowards();
    }

    private void MoveTowards()
    {
        transform.DOMoveX(GetDirection().x, shipVelocity)
            .SetEase(Ease.Linear)
            .OnComplete(UpdateDirection);
    }

    private void UpdateDirection()
    {
        var sequence = DOTween.Sequence();

        sequence.AppendInterval(0.5f);
        sequence.AppendCallback(() => _direction *= -1);
        sequence.AppendCallback(MoveTowards);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(startPoint.position, endPoint.position);
    }
}
