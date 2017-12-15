using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Executor.Classes;
using Executor.Modelo;

namespace Executor.Formularios.Visualizacao
{
    public partial class FrmResultadoTreinamento : Form
    {
        public FrmResultadoTreinamento()
        {
            InitializeComponent();
        }

        private void FrmResultadoTreinamento_Load(object sender, EventArgs e)
        {
            CarregarGrid();
        }

        private void CarregarGrid()
        {
            Global.MonografiaEntities = new MonografiaEntities();

            var lvar = (from llnq in Global.MonografiaEntities.Treinamento
                        join j in Global.MonografiaEntities.TreinamentoDados on llnq.Codigo equals j.Treinamento into tj
                        join v in Global.MonografiaEntities.ValidacaoDados on llnq.Codigo equals v.Treinamento into tv
                        select new
                                   {
                                       llnq.Codigo,
                                       llnq.Algoritmo1.Descricao,
                                       llnq.Momentum,
                                       llnq.TaxaAprendizado,
                                       llnq.DadosRedeNeural,
                                       llnq.DadosRedeNeural1.NumeroNeuroniosEntrada,
                                       llnq.DadosRedeNeural1.NumeroNeuroniosOculta,
                                       llnq.DadosRedeNeural1.NumeroNeuroniosSaida,
                                       llnq.NumeroGeracoes,
                                       llnq.NumeroExemplos,
                                       MinimoLocal = (llnq.MinimoLocal)?"Sim":"Não",
                                       TreinamentoAcertos = tj.Count(x => x.Validado),
                                       TreinamentoAcertosQuadrado = tj.Count(x => x.Validado && x.DadosTreinamento1.Elemento == 1),
                                       TreinamentoAcertosTriangulo = tj.Count(x => x.Validado && x.DadosTreinamento1.Elemento == 2),
                                       TreinamentoAcertosCirculo = tj.Count(x => x.Validado && x.DadosTreinamento1.Elemento == 3),
                                       ValidacaoAcertos = tv.Count(x => x.Validado),
                                       ValidacaoAcertosQuadrado = tv.Count(x => x.Validado && x.DadosValidacao1.Elemento == 1),
                                       ValidacaoAcertosTriangulo = tv.Count(x => x.Validado && x.DadosValidacao1.Elemento == 2),
                                       ValidacaoAcertosCirculo = tv.Count(x => x.Validado && x.DadosValidacao1.Elemento == 3)
                                   }).ToList();

            dataGridView1.DataSource = lvar;

        }
    }
}
