using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private bool isMenu = false;
    [SerializeField, ShowIf("isMenu")] private TextMeshProUGUI currencyText;
    
    [SerializeField] private TextMeshProUGUI vacaText;
    [SerializeField] private TextMeshProUGUI galinhaText;
    [SerializeField] private TextMeshProUGUI peixeText;
    [SerializeField] private TextMeshProUGUI coelhoText;
    [SerializeField] private TextMeshProUGUI abelhaText;
    [SerializeField] private TextMeshProUGUI ratoText;

    private void Start()
    {
        if (isMenu)
        {
            GameManager.OnResourceUpdate.AddListener(UpdateHUD);
            UpdateHUD(GameManager.Instance.totalRecursos, GameManager.Instance.totalCurrency);
        }
        else {
            BlockManager.OnTableUpdate.AddListener(UpdateHUD);
            UpdateHUD(new GameManager.TabelaValores());
        }
    }

    private void UpdateHUD(GameManager.TabelaValores valoresAtualizados)
    {
        var valoresObjetivo = GameManager.Instance.currentLevel.objetivos;
        
        vacaText.text    = $"{valoresAtualizados.qtdVacas    : 00} /{(valoresObjetivo.qtdVacas    != 0 ? valoresObjetivo.qtdVacas    : " --") : 00}";
        galinhaText.text = $"{valoresAtualizados.qtdGalinhas : 00} /{(valoresObjetivo.qtdGalinhas != 0 ? valoresObjetivo.qtdGalinhas : " --") : 00}";
        peixeText.text   = $"{valoresAtualizados.qtdPeixes   : 00} /{(valoresObjetivo.qtdPeixes   != 0 ? valoresObjetivo.qtdPeixes   : " --") : 00}";
        coelhoText.text  = $"{valoresAtualizados.qtdCoelhos  : 00} /{(valoresObjetivo.qtdCoelhos  != 0 ? valoresObjetivo.qtdCoelhos  : " --") : 00}";
        abelhaText.text  = $"{valoresAtualizados.qtdAbelhas  : 00} /{(valoresObjetivo.qtdAbelhas  != 0 ? valoresObjetivo.qtdAbelhas  : " --") : 00}";
        ratoText.text    = $"{valoresAtualizados.qtdRatos    : 00} /{(valoresObjetivo.qtdRatos    != 0 ? valoresObjetivo.qtdRatos    : " --") : 00}";
    }
    
    private void UpdateHUD(GameManager.TabelaValores valoresAtualizados, int currencyAtualizada)
    {
        currencyText.text = $"{currencyAtualizada : 0000}";
        
        vacaText.text    = $"x{valoresAtualizados.qtdVacas    : 00}";
        galinhaText.text = $"x{valoresAtualizados.qtdGalinhas : 00}";
        peixeText.text   = $"x{valoresAtualizados.qtdPeixes   : 00}";
        coelhoText.text  = $"x{valoresAtualizados.qtdCoelhos  : 00}";
        abelhaText.text  = $"x{valoresAtualizados.qtdAbelhas  : 00}";
        ratoText.text    = $"x{valoresAtualizados.qtdRatos    : 00}";
    }
}
