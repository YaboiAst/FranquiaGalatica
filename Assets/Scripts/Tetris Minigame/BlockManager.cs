using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class BlockManager : MonoBehaviour
{
    public enum Tipo {Vaca, Galinha, Peixe, Coelho, Abelha, Rato}
    
    [System.Serializable]
    public class Dropavel
    {
        public Tipo tipo;
        public GameObject prefabBlock;
        [MinValue(0), MaxValue(100)] public int chanceSpawn;
    }

    [SerializeField] private Transform blockPosition;
    
    [SerializeField] private Dropavel[] tabelaDeDrops;
    
    [Space(20)]
    
    [SerializeField] private GameManager.TabelaValores objetivos;
    private GameManager.TabelaValores _progressoAtual;
    
    // Load Info
    private BlockController _loadedBlock;
    
    // Stack Info
    public static Transform Ground = null;
    private Stack<BlockController> _pilha;

    public static readonly UnityEvent<Transform> OnStackSet = new();
    public static readonly UnityEvent<BlockController> OnStackAdd = new();
    public static readonly UnityEvent<GameManager.TabelaValores> OnTableUpdate = new();
    
    public static readonly UnityEvent OnUpdateCamera = new();
    
    private void Start()
    {
        // Load info from Game Manager
        var gm = GameManager.Instance;
        var curLevel = gm.currentLevel;
        
        if (gm is null) return;

        tabelaDeDrops = new Dropavel[curLevel.levelDrops.Length];
        for (var i = 0; i < tabelaDeDrops.Length; i++)
        {
            tabelaDeDrops[i] = curLevel.levelDrops[i];
        }

        objetivos = new GameManager.TabelaValores();
        objetivos.Copy(curLevel.objetivos);
        
        // Initialize 
        _progressoAtual = new GameManager.TabelaValores();
        
        _loadedBlock = null;
        Ground = null;
        _pilha = new Stack<BlockController>();
        
        BlockController.OnBlockDrop.AddListener(LoadBlock);
        OnStackSet.AddListener((Transform t) => Ground = t);
        OnStackAdd.AddListener(UpdatePilha);
        
        GameManager.OnLose.AddListener(() => this.enabled = false);
        GameManager.OnWin.AddListener(()  => this.enabled = false);
        
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
            case Tipo.Peixe:
                _progressoAtual.qtdPeixes++;
                break;
            case Tipo.Coelho:
                _progressoAtual.qtdCoelhos++;
                break;
            case Tipo.Abelha:
                _progressoAtual.qtdAbelhas++;
                break;
            case Tipo.Rato:
                _progressoAtual.qtdRatos++;
                break;
            default: break;
        }
        
        OnTableUpdate?.Invoke(_progressoAtual);
        
        if(_progressoAtual.qtdVacas    < objetivos.qtdVacas   ) return;
        if(_progressoAtual.qtdGalinhas < objetivos.qtdGalinhas) return;
        if(_progressoAtual.qtdPeixes   < objetivos.qtdPeixes  ) return;
        if(_progressoAtual.qtdCoelhos  < objetivos.qtdCoelhos ) return;
        if(_progressoAtual.qtdAbelhas  < objetivos.qtdAbelhas ) return;
        if(_progressoAtual.qtdRatos    < objetivos.qtdRatos   ) return;

        var gm = GameManager.Instance;

        gm.totalRecursos.qtdVacas    += _progressoAtual.qtdVacas;
        gm.totalRecursos.qtdGalinhas += _progressoAtual.qtdGalinhas;
        gm.totalRecursos.qtdPeixes   += _progressoAtual.qtdPeixes;
        gm.totalRecursos.qtdCoelhos  += _progressoAtual.qtdCoelhos;
        gm.totalRecursos.qtdAbelhas  += _progressoAtual.qtdAbelhas;
        gm.totalRecursos.qtdRatos    += _progressoAtual.qtdRatos;
        
        GameManager.OnWin?.Invoke();
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
