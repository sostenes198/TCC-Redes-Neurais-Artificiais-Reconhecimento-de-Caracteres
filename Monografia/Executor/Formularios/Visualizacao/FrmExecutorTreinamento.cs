using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Encog.Neural.Networks;
using Executor.Classes;
using Executor.Modelo;
using Executor.RedeNeural;
using ZedGraph;

namespace Executor.Formularios.Visualizacao
{
    public partial class FrmExecutorTreinamento : Form
    {
        public FrmExecutorTreinamento()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool lblnAcompanhado = (MessageBox.Show("Deseja acompanhar a execução?", "Pergunta:", MessageBoxButtons.YesNo) == DialogResult.Yes);

            foreach (Treinamento treinamento in Global.MonografiaEntities.Treinamento)
            {
                if (treinamento.NumeroGeracoes == 0 && treinamento.MinimoLocal != true)
                {
                    BasicNetwork redeNeuralSemTreinamento = RedeNeuralConfiguracao.RetornaRedeNeuralEspecifica(treinamento.DadosRedeNeural);
                    List<int> lIntListaDadosTreinamento = Global.MonografiaEntities.TreinamentoDados.Where(y => y.Treinamento == treinamento.Codigo).Select(x => x.DadosTreinamento).ToList();
                    BasicNetwork redeNeuralTreinada = ExecutorTreinamento.TreinarRedeNeural(redeNeuralSemTreinamento, lIntListaDadosTreinamento, treinamento.Codigo);
                    if (redeNeuralTreinada == null)
                        continue;

                    List<TreinamentoDados> llstTreinamentoDados = new MonografiaEntities().TreinamentoDados.Where(x => x.Treinamento == treinamento.Codigo).ToList();

                    foreach (TreinamentoDados treinamentoDados in llstTreinamentoDados)
                    {
                        double[] ldblValores = Global.RecuperaValoresNormalizados(treinamentoDados.DadosTreinamento1.Dados);
                        double[] ldblResultado = new double[1];
                        redeNeuralTreinada.Compute(ldblValores, ldblResultado);

                        Global.MonografiaEntities = new MonografiaEntities();
                        TreinamentoDados ltreinamento = Global.MonografiaEntities.TreinamentoDados.First(x => x.Codigo == treinamentoDados.Codigo);

                        Global.MonografiaEntities.Entry(ltreinamento).State = EntityState.Modified;

                        ltreinamento.Validado = ValidacaoTreinamento(ldblResultado[0], treinamentoDados.DadosTreinamento1.Elemento1);
                        ltreinamento.ValorRetornado = ldblResultado[0];

                        Global.MonografiaEntities.SaveChanges();
                    }

                    List<ValidacaoDados> llstValidacaoDados = new MonografiaEntities().ValidacaoDados.Where(x => x.Treinamento == treinamento.Codigo).ToList();

                    foreach (ValidacaoDados validacaoDados in llstValidacaoDados)
                    {
                        double[] ldblValores = Global.RecuperaValoresNormalizados(validacaoDados.DadosValidacao1.Dados);
                        double[] ldblResultado = new double[1];
                        redeNeuralTreinada.Compute(ldblValores, ldblResultado);

                        Global.MonografiaEntities = new MonografiaEntities();
                        ValidacaoDados ltreinamento = Global.MonografiaEntities.ValidacaoDados.First(x => x.Codigo == validacaoDados.Codigo);

                        Global.MonografiaEntities.Entry(ltreinamento).State = EntityState.Modified;

                        ltreinamento.Validado = ValidacaoTreinamento(ldblResultado[0], validacaoDados.DadosValidacao1.Elemento1);
                        ltreinamento.ValorRetornado = ldblResultado[0];

                        Global.MonografiaEntities.SaveChanges();
                    }
                }
                if (lblnAcompanhado)
                    AtualizarTudo();
            }

            MessageBox.Show("Treinamentos executados com sucesso!");
        }

        private bool ValidacaoTreinamento(double dblValor, Elemento elemento)
        {
            return (dblValor > elemento.ValorMinimo && dblValor < elemento.ValorMaximo);
        }

        private void FrmExecutorTreinamento_Load(object sender, EventArgs e)
        {
            AtualizarTudo();
        }

        private void AtualizarTudo()
        {
            IList lLsttreinamento = (from lLnqTreinamento in Global.MonografiaEntities.Treinamento
                                     select new
                                     {
                                         lLnqTreinamento.Codigo,
                                         lLnqTreinamento.Algoritmo1.Descricao,
                                         lLnqTreinamento.Momentum,
                                         lLnqTreinamento.TaxaAprendizado,
                                         lLnqTreinamento.NumeroExemplos,
                                         lLnqTreinamento.NumeroGeracoes,
                                         lLnqTreinamento.MinimoLocal,
                                     }).ToList();

            dataGridView1.DataSource = lLsttreinamento;

            AtualizaCoresGrade();
        }

        private void AtualizaCoresGrade()
        {
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {

                dataGridView1.Rows[r.Index].DefaultCellStyle.BackColor = Convert.ToDecimal(dataGridView1[4, r.Index].Value) > 0
                    ? Color.LightGreen
                    : Color.PaleGoldenrod;
            }
            button1.Focus();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index != -1)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                    ExibirGraficoTreinamento(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void ExibirGraficoTreinamento(string strCodigoTreinaemnto)
        {
            int lIntCodigoTreinamento = int.Parse(strCodigoTreinaemnto);

            PointPairList list = new PointPairList();

            foreach (LogTreinamento log in Global.MonografiaEntities.LogTreinamento.Where(log => log.Treinamento == lIntCodigoTreinamento))
            {
                list.Add(log.Geracao, log.MargemErro);
            }
            zedGraphControl1.GraphPane = new GraphPane();
            // Get a reference to the GraphPane instance in the ZedGraphControl
            GraphPane myPane = zedGraphControl1.GraphPane;

            Treinamento lTreinamento = Global.MonografiaEntities.Treinamento.FirstOrDefault(x => x.Codigo == lIntCodigoTreinamento);

            // Set the titles
            if (lTreinamento == null)
            {
                myPane.Title.Text = "Épocas X Margem Erro";
            }
            else if (lTreinamento.Algoritmo == 1)
            {
                myPane.Title.Text = string.Format("Algoritmo: Back Propagation, Quantidade Exemplos: {0}, Momentum: {1}, Taxa Aprendizado: {2}\n" +
                                                  "Configuração Rede Neural: {3} - Neurônios Entrada: {4}- Oculta {5} - Saída: {6} \n" +
                                                  "Épocas X Margem Erro",
                    lTreinamento.NumeroExemplos * 3, lTreinamento.Momentum, lTreinamento.TaxaAprendizado, (lTreinamento.DadosRedeNeural1.Codigo == 5) ? 3 : lTreinamento.DadosRedeNeural1.Codigo,
                    lTreinamento.DadosRedeNeural1.NumeroNeuroniosEntrada, lTreinamento.DadosRedeNeural1.NumeroNeuroniosOculta,
                    lTreinamento.DadosRedeNeural1.NumeroNeuroniosSaida);
                zedGraphControl1.SaveFileDialog.FileName = string.Format("BP-{0}-Config{1}-{2}ex.png",lTreinamento.Codigo,lTreinamento.DadosRedeNeural,lTreinamento.NumeroExemplos*3);
            }
            else if (lTreinamento.Algoritmo == 2)
            {
                myPane.Title.Text = string.Format("Algoritmo: Resilient Propagation, Quantidade Exemplos: {0}\n" +
                                                  "Configuração Rede Neural: {1} - Neurônios Entrada: {2}- Oculta {3} - Saída: {4} \n" +
                                                  "Épocas X Margem Erro",
                                                  lTreinamento.NumeroExemplos * 3, (lTreinamento.DadosRedeNeural1.Codigo == 5) ? 3 : lTreinamento.DadosRedeNeural1.Codigo,
                                                  lTreinamento.DadosRedeNeural1.NumeroNeuroniosEntrada,
                                                  lTreinamento.DadosRedeNeural1.NumeroNeuroniosOculta,
                                                  lTreinamento.DadosRedeNeural1.NumeroNeuroniosSaida);
                zedGraphControl1.SaveFileDialog.FileName = string.Format("RP-{0}-Config{1}-{2}ex.png", lTreinamento.Codigo, lTreinamento.DadosRedeNeural, lTreinamento.NumeroExemplos *3);
            }
            // Set the axis labels

            myPane.XAxis.Title.Text = "Épocas";
            myPane.YAxis.Title.Text = "Margem Erro";

            // Make up some data points based on the Sine function


            // Generate a red curve with diamond symbols, and "Alpha" in the legend
            LineItem myCurve = myPane.AddCurve("", list, Color.Red, SymbolType.None);
            // Fill the symbols with white
            myCurve.Symbol.Fill = new Fill(Color.White);

            //// Generate a blue curve with circle symbols, and "Beta" in the legend
            //myCurve = myPane.AddCurve( "Beta",
            //    list2, Color.Blue, SymbolType.Circle );
            //// Fill the symbols with white
            //myCurve.Symbol.Fill = new Fill( Color.White );
            //// Associate this curve with the Y2 axis
            //myCurve.IsY2Axis = true;

            // Show the x axis grid
            myPane.XAxis.MajorGrid.IsVisible = false;

            // Make the Y axis scale red
            myPane.YAxis.Scale.FontSpec.FontColor = Color.Red;
            myPane.YAxis.Title.FontSpec.FontColor = Color.Red;
            // turn off the opposite tics so the Y tics don't show up on the Y2 axis
            myPane.YAxis.MajorTic.IsOpposite = false;
            myPane.YAxis.MinorTic.IsOpposite = false;
            // Don't display the Y zero line
            myPane.YAxis.MajorGrid.IsZeroLine = false;
            // Align the Y axis labels so they are flush to the axis
            myPane.YAxis.Scale.Align = AlignP.Inside;
            // Manually set the axis range
            myPane.YAxis.Scale.Min = 0;
            myPane.YAxis.Scale.Max = 0.5;

            // Enable the Y2 axis display
            myPane.Y2Axis.IsVisible = true;
            // Make the Y2 axis scale blue
            myPane.Y2Axis.Scale.FontSpec.FontColor = Color.Blue;
            myPane.Y2Axis.Title.FontSpec.FontColor = Color.Blue;
            // turn off the opposite tics so the Y2 tics don't show up on the Y axis
            myPane.Y2Axis.MajorTic.IsOpposite = false;
            myPane.Y2Axis.MinorTic.IsOpposite = false;
            // Display the Y2 axis grid lines
            myPane.Y2Axis.MajorGrid.IsVisible = true;
            // Align the Y2 axis labels so they are flush to the axis
            myPane.Y2Axis.Scale.Align = AlignP.Inside;

            // Fill the axis background with a gradient
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGray, 45.0f);

            // Add a text box with instructions
            //TextObj text = new TextObj(
            //    "Zoom: left mouse & drag\nPan: middle mouse & drag\nContext Menu: right mouse",
            //    0.05f, 0.95f, CoordType.ChartFraction, AlignH.Left, AlignV.Bottom );
            //text.FontSpec.StringAlignment = StringAlignment.Near;
            //myPane.GraphObjList.Add( text );

            // Enable scrollbars if needed
            zedGraphControl1.IsShowHScrollBar = true;
            zedGraphControl1.IsShowVScrollBar = true;
            zedGraphControl1.IsAutoScrollRange = true;
            zedGraphControl1.IsScrollY2 = true;

            //// OPTIONAL: Show tooltips when the mouse hovers over a point
            //zedGraphControl1.IsShowPointValues = true;
            //zedGraphControl1.PointValueEvent += new ZedGraphControl.PointValueHandler(MyPointValueHandler);

            //// OPTIONAL: Add a custom context menu item
            //zedGraphControl1.ContextMenuBuilder += new ZedGraphControl.ContextMenuBuilderEventHandler(
            //                MyContextMenuBuilder );
             
            //// OPTIONAL: Handle the Zoom Event
            //zedGraphControl1.ZoomEvent += new ZedGraphControl.ZoomEventHandler(MyZoomEvent);

            // Size the control to fit the window
            //SetSize();

            // Tell ZedGraph to calculate the axis ranges
            // Note that you MUST call this after enabling IsAutoScrollRange, since AxisChange() sets
            // up the proper scrolling parameters
            zedGraphControl1.AxisChange(); 
            
            // Make sure the Graph gets redrawn
            zedGraphControl1.Invalidate();
        }


    }
}
