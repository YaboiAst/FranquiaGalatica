using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
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

    [SerializeField] private Dropavel[] tabelaDeDrops;
    [SerializeField] private Transform blockPosition;
    
    // Load Info
    private GameObject _loadedBlock;
    
    // Stack Info
    public static Transform Ground = null;
    private Stack<BlockController> _pilha;

    public static UnityEvent<Transform> OnStackSet = new();
    public static UnityEvent<BlockController> OnStackAdd = new();
    public static UnityEvent OnUpdateCamera = new();
    
    private void Start()
    {
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
        
        // Atualizar contadores?
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
                _loadedBlock = Instantiate(drop.prefabBlock, blockPosition.position, blockPosition.rotation, this.transform);
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
            _loadedBlock.transform.SetParent(null);
            _loadedBlock.GetComponent<BlockController>().Drop();
            _loadedBlock = null;
        }
    }
}
