using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [System.Serializable] public class LevelInfo
    {
        public BlockManager.Dropavel[] levelDrops;
        public GameManager.TabelaValores objetivos;
        [MinValue(0)] public int travelCost;
    }

    [SerializeField] private LevelInfo levelInfo;
    private Button _levelButton;

    private void Start()
    {
        _levelButton = GetComponent<Button>();
        _levelButton.onClick.AddListener(() => GameManager.Instance.LoadCurrentLevel(levelInfo));
    }
}
