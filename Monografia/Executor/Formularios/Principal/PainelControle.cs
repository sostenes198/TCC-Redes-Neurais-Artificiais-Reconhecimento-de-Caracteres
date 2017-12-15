using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Executor.Classes;
using Executor.Formularios.Geracao;
using Executor.Formularios.Insercao;
using Executor.Formularios.Validacao;
using Executor.Formularios.Visualizacao;
using Executor.RedeNeural;

namespace Executor.Formularios.Principal
{
    public partial class PainelControle : Form
    {
        public PainelControle()
        {
            InitializeComponent();
        }

        #region Eventos de Visualização de dados

        private void algoritmoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IList lLst = (from lLnqAlgoritmo in Global.MonografiaEntities.Algoritmo
                          select new
                                     {
                                         lLnqAlgoritmo.Codigo,
                                         lLnqAlgoritmo.Descricao
                                     }).ToList();

            FrmVisualizador frmVisualizador = new FrmVisualizador(lLst, "Visualizador de Algoritmos");
            frmVisualizador.Show(this);
        }

        private void elementoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IList lLst = (from lLnqElemeto in Global.MonografiaEntities.Elemento
                          select new
                                     {
                                         lLnqElemeto.Codigo,
                                         lLnqElemeto.Descricao,
                                         lLnqElemeto.ValorMinimo,
                                         lLnqElemeto.ValorIdeal,
                                         lLnqElemeto.ValorMaximo
                                     }).ToList();

            FrmVisualizador frmVisualizador = new FrmVisualizador(lLst, "Visualizador de Elementos");
            frmVisualizador.Show(this);
        }

        private void dadosRedeNeuralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IList lLst = (from lLnqElemeto in Global.MonografiaEntities.DadosRedeNeural
                          select new
                          {
                              lLnqElemeto.Codigo,
                              lLnqElemeto.NumeroNeuroniosEntrada,
                              lLnqElemeto.NumeroNeuroniosOculta,
                              lLnqElemeto.NumeroNeuroniosSaida,
                          }).ToList();

            FrmVisualizador frmVisualizador = new FrmVisualizador(lLst, "Visualizador de Dados da Rede Neural");
            frmVisualizador.Show(this);
        }

        #endregion

        private void gerarDadosRedeNeuralToolStripMenuItem_Click(object sender, EventArgs e)
        {

            const int lintNumeroCamadaEntrada = 100;
            const int lintNeuroniosCamadaSaida = 1;


            List<int> lLstNeuroniosCamadaEntrada = new List<int> {lintNumeroCamadaEntrada};

            //Regra do valor médio: Número de neurônios na camada de entrada + número de neurônios na camada de saída, dividido por 2
            //Regra do Kolmogorov:  (2 X Número de neurônios na camada de entrada) mais 1
            //Regra de Fletcher-Gloss: (2 vezes raiz da quantidade de neurônio na camada de entrada) mais quantidade de neurônios na camada de saída.
            //Regra da raiz quadrada: raiz quadrada do número de neurônios da camada de entrada vezes número de neurônios na camada de saída

            List<int> lLstNeuroniosCamadaOculta = new List<int>();
            lLstNeuroniosCamadaOculta.Add(((lintNumeroCamadaEntrada + lintNeuroniosCamadaSaida) / 2));
            lLstNeuroniosCamadaOculta.Add(( 2 * lintNumeroCamadaEntrada)+1);
            lLstNeuroniosCamadaOculta.Add((int)(2 * Math.Sqrt(Double.Parse(lintNumeroCamadaEntrada.ToString(CultureInfo.InvariantCulture)))) + lintNeuroniosCamadaSaida);
            lLstNeuroniosCamadaOculta.Add((int) Math.Sqrt(Double.Parse((lintNumeroCamadaEntrada*lintNeuroniosCamadaSaida).ToString(CultureInfo.InvariantCulture))));

          

            var lLstNeuroniosCamadaSaida = new List<int> { lintNeuroniosCamadaSaida};

            RedeNeuralConfiguracao.GerarRedesNeurais(lLstNeuroniosCamadaEntrada, lLstNeuroniosCamadaOculta, lLstNeuroniosCamadaSaida);
            

            MessageBox.Show(this, "Redes Neurais criadas com sucesso!");
        }

        private void testeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FrmGerarTreinamento frmGerarTreinamento = new FrmGerarTreinamento();
            frmGerarTreinamento.Show(this);

            //BasicNetwork retorno = RedeNeuralConfiguracao.RetornaRedeNeuralEspecifica(1);

            //RedeNeuralResultante resultante = new RedeNeuralResultante();
            //resultante.PesosResultante = new byte[1];
            //Global.monografiaEntities.RedeNeuralResultante.Add(resultante);
            //Global.monografiaEntities.SaveChanges();

            //Treinamento treinamento = new Treinamento();
            //treinamento.Algoritmo = 2;
            //treinamento.Momentum = 0;
            //treinamento.TaxaAprendizado = 0;
            //treinamento.DadosRedeNeural = 1;
            //treinamento.RedeNeuralResultante = resultante.Codigo;

            //Global.monografiaEntities.Treinamento.Add(treinamento);
            //Global.monografiaEntities.SaveChanges();

            //List<int> lIntListaDadosTreinamento =
            //    Global.monografiaEntities.TreinamentoDados.Select(x => x.Codigo).ToList();
            //ExecutorTreinamento.TreinarRedeNeural(retorno, lIntListaDadosTreinamento, 1);//, Global.AlgoritmoTreinamento.ResilientPropagation);
        }

        private void verificarTreinamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmExecutorTreinamento frmExecutorTreinamento = new FrmExecutorTreinamento();
            frmExecutorTreinamento.Show(this);
        }

        private void dadosTreinamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVisualizarDados frmVisualizarDados = new FrmVisualizarDados("T");
            frmVisualizarDados.Show(this);
        }

        private void dadosValidacaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVisualizarDados frmVisualizarDados = new FrmVisualizarDados("V");
            frmVisualizarDados.Show(this);
        }

        private void treinamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormInsercaoTreinamento frmInsercaoTreinamento = new FormInsercaoTreinamento();
            frmInsercaoTreinamento.Show(this);
        }

        private void validaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormInsercaoValidacao frmInsercaoValidacao = new FormInsercaoValidacao();
            frmInsercaoValidacao.Show(this);
        }

        private void testeToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FrmValidacaoTeste frmInsercaoValidacao = new FrmValidacaoTeste();
            frmInsercaoValidacao.Show(this);
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmConversor frmConversor = new FrmConversor();
            frmConversor.Show(this);
        }

        private void visualizarResultadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmResultadoTreinamento frmResultadoTreinamento = new FrmResultadoTreinamento();
            frmResultadoTreinamento.Show(this);
        }
    }
}
