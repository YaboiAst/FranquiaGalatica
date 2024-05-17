using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI vacaText;
    [SerializeField] private TextMeshProUGUI galinhaText;
    [SerializeField] private TextMeshProUGUI peixeText;
    [SerializeField] private TextMeshProUGUI coelhoText;
    [SerializeField] private TextMeshProUGUI abelhaText;
    [SerializeField] private TextMeshProUGUI ratoText;

    private void Start()
    {
        BlockManager.OnTableUpdate.AddListener(UpdateHUD);
        UpdateHUD(new GameManager.TabelaValores());
    }

    private void UpdateHUD(GameManager.TabelaValores valoresAtualizados)
    {
        vacaText.text    = $"x{valoresAtualizados.qtdVacas    : 00}";
        galinhaText.text = $"x{valoresAtualizados.qtdGalinhas : 00}";
        peixeText.text   = $"x{valoresAtualizados.qtdPeixes   : 00}";
        coelhoText.text  = $"x{valoresAtualizados.qtdCoelhos  : 00}";
        abelhaText.text  = $"x{valoresAtualizados.qtdAbelhas  : 00}";
        ratoText.text    = $"x{valoresAtualizados.qtdRatos    : 00}";
    }
}
