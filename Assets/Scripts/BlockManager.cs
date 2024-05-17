using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class BlockManager : MonoBehaviour
{
    public enum Tipo {Vaca, Galinha, Coelho, Rato, Abelha, Peixe}
    
    [System.Serializable]
    public class Dropavel
    {
        public Tipo tipo;
        public GameObject prefabBlock;
        [MinValue(0), MaxValue(100)] public int chanceSpawn;
    }

    [System.Serializable]
    public class TabelaValores
    {
        public int qtdVacas;
        public int qtdGalinhas;
        public int qtdCoelhos;
        public int qtdRatos;
        public int qtdAbelhas;
        public int qtdPeixes;

        public TabelaValores()
        {
            qtdVacas = 0;
            qtdGalinhas = 0;
            qtdCoelhos = 0;
            qtdRatos = 0;
            qtdAbelhas = 0;
            qtdPeixes = 0;
        }
    }

    [SerializeField] private Transform blockPosition;
    
    [SerializeField] private Dropavel[] tabelaDeDrops;
    
    [Space(20)]
    
    [SerializeField] private TabelaValores objetivos;
    private TabelaValores _progressoAtual;
    
    // Load Info
    private BlockController _loadedBlock;
    
    // Stack Info
    public static Transform Ground = null;
    private Stack<BlockController> _pilha;

    public static readonly UnityEvent<Transform> OnStackSet = new();
    public static readonly UnityEvent<BlockController> OnStackAdd = new();
    public static readonly UnityEvent<TabelaValores> OnTableUpdate = new();
    
    public static readonly UnityEvent OnUpdateCamera = new();
    
    private void Start()
    {
        _progressoAtual = new TabelaValores();
        
        _loadedBlock = null;
        _pilha = new Stack<BlockController>();
        
        BlockController.OnBlockDrop.AddListener(LoadBlock);
        OnStackSet.AddListener((Transform t) => Ground = t);
        OnStackAdd.AddListener(UpdatePilha);
        
        LoadBlock();
    }

    private void UpdatePilha(BlockController block)
    {
        block.transform.SetParent(Ground);
        _pilha.Push(block);

        switch (block.RetType())
        {
            case Tipo.Vaca:
                _progressoAtual.qtdVacas++;
                break;
            case Tipo.Galinha:
                _progressoAtual.qtdGalinhas++;
                break;
            case Tipo.Coelho:
                _progressoAtual.qtdCoelhos++;
                break;
            case Tipo.Rato:
                _progressoAtual.qtdRatos++;
                break;
            case Tipo.Abelha:
                _progressoAtual.qtdAbelhas++;
                break;
            case Tipo.Peixe:
                _progressoAtual.qtdPeixes++;
                break;
            default: break;
        }
        
        OnTableUpdate?.Invoke(_progressoAtual);
        
        if(_progressoAtual.qtdVacas    < objetivos.qtdVacas   ) return;
        if(_progressoAtual.qtdGalinhas < objetivos.qtdGalinhas) return;
        if(_progressoAtual.qtdCoelhos  < objetivos.qtdCoelhos ) return;
        if(_progressoAtual.qtdRatos    < objetivos.qtdRatos   ) return;
        if(_progressoAtual.qtdAbelhas  < objetivos.qtdAbelhas ) return;
        if(_progressoAtual.qtdPeixes   < objetivos.qtdPeixes  ) return;

        Debug.Log("Alcançou a meta!");
        // EndLevel
    }

    private void LoadBlock()
    {
        if(_loadedBlock is not null) return;
        
        var temp = 0;
        var randChance = Random.Range(0, 100);
        foreach (var drop in tabelaDeDrops)
        {
            if (randChance < temp + drop.chanceSpawn)
            {
                var go = Instantiate(drop.prefabBlock, blockPosition.position, blockPosition.rotation, this.transform);
                _loadedBlock = go.GetComponent<BlockController>();
                _loadedBlock.LoadType(drop.tipo);
                break;
            }
            else temp += drop.chanceSpawn;
        }
    }

    private void Update()
    {
        if(_loadedBlock is null) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _loadedBlock.Drop();
            _loadedBlock = null;
        }
    }
}
