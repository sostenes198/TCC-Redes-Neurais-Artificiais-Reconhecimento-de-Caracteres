using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Executor.Classes;
using Executor.Modelo;

namespace Executor.Formularios.Geracao
{
    public partial class FrmGerarTreinamento : Form
    {
        public FrmGerarTreinamento()
        {
            InitializeComponent();
        }

        private void FrmGerarTreinamento_Load(object sender, EventArgs e)
        {
            PreencherDadosTreinamento();
        }

        private void PreencherDadosTreinamento()
        {
            var x = (from lLnqTreinamento in Global.MonografiaEntities.Treinamento
                     select new
                                {
                                    CodigoTreinamento = lLnqTreinamento.Codigo,
                                    Algoritmo = lLnqTreinamento.Algoritmo1.Descricao,
                                    lLnqTreinamento.Momentum,
                                    lLnqTreinamento.TaxaAprendizado,
                                    lLnqTreinamento.DadosRedeNeural1.NumeroNeuroniosEntrada,
                                    lLnqTreinamento.DadosRedeNeural1.NumeroNeuroniosOculta,
                                    lLnqTreinamento.DadosRedeNeural1.NumeroNeuroniosSaida,
                                    lLnqTreinamento.MinimoLocal,
                                    lLnqTreinamento.NumeroExemplos,
                                    Epocas = lLnqTreinamento.NumeroGeracoes,
                                }).ToList();


            dataGridView1.DataSource = x;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IList<double> lLstMomentum = new List<double>();
            IList<double> lLstTaxaAprendizado = new List<double>();

            if (nudMomentumFrequencia.Value == 0 && nudTaxaAprendizadoFrequencia.Value == 0)
            {
                lLstMomentum.Add(double.Parse(nudMomentumInicial.Value.ToString(CultureInfo.InvariantCulture).Replace(".", ",")));
                lLstTaxaAprendizado.Add(double.Parse(nudTaxaAprendizadoInicial.Value.ToString(CultureInfo.InvariantCulture).Replace(".", ",")));
            }
            else
            {
                for (decimal ldecValor = nudMomentumInicial.Value; ldecValor <= nudMomentumFinal.Value; ldecValor += nudMomentumFrequencia.Value)
                {
                    lLstMomentum.Add(double.Parse(ldecValor.ToString(CultureInfo.InvariantCulture).Replace(".", ",")));
                }

                for (decimal ldecValor = nudTaxaAprendizadoInicial.Value; ldecValor <= nudTaxaAprendizadoFinal.Value; ldecValor += nudTaxaAprendizadoFrequencia.Value)
                {
                    lLstTaxaAprendizado.Add(double.Parse(ldecValor.ToString(CultureInfo.InvariantCulture).Replace(".", ",")));
                }
            }
            int lintNumeroExemplos = RetornarNumeroExemplos();
            int lintCodigoDadosRedeNeural = int.Parse(txtCodigoDadosRedeNeural.Text);


            foreach (double ldecValorTaxaAprendizado in lLstTaxaAprendizado)
            {
                foreach (double ldecValorMomentum in lLstMomentum)
                {
                    GerarTreinamento(ldecValorTaxaAprendizado, ldecValorMomentum, lintNumeroExemplos, lintCodigoDadosRedeNeural);
                }
            }

            MessageBox.Show("Treinamentos gerados com sucesso!");
        }

        private int RetornarNumeroExemplos()
        {
            if (rbtExemplo10.Checked)
            {
                return 10;
            }
            if (rbtExemplo30.Checked)
            {
                return 20;
            }
            if (rbtExemplo50.Checked)
            {
                return 30;
            }
            return 0;
        }

        private void GerarTreinamento(double ldecValorTaxaAprendizado, double ldecValorMomentum, int lintNumeroExemplos, int lintCodigoDadosRedeNeural)
        {
            IList<Treinamento> lEnuTreinamentos = (from treinamento in Global.MonografiaEntities.Treinamento
                                                   where treinamento.Momentum == ldecValorMomentum &&
                                                         treinamento.TaxaAprendizado == ldecValorTaxaAprendizado &&
                                                         treinamento.NumeroExemplos == lintNumeroExemplos &&
                                                         treinamento.DadosRedeNeural == lintCodigoDadosRedeNeural
                                                   select treinamento).ToList();

            if (!lEnuTreinamentos.Any())
            {

                IList<Elemento> lelementos = Global.MonografiaEntities.Elemento.ToList();

                int lintProximoCodigotreinamento;
                try
                {
                    lintProximoCodigotreinamento = Global.MonografiaEntities.Treinamento.Max(x => x.Codigo);
                }
                catch (Exception)
                {
                    lintProximoCodigotreinamento = 0;
                }
                Treinamento novotreinamento = new Treinamento
                                                  {
                                                      Codigo = lintProximoCodigotreinamento + 1,
                                                      Algoritmo = 1,
                                                      Momentum = ldecValorMomentum,
                                                      TaxaAprendizado = ldecValorTaxaAprendizado,
                                                      NumeroExemplos = lintNumeroExemplos,
                                                      RedeNeuralResultante1 = new RedeNeuralResultante { PesosResultante = new byte[0] },
                                                      DadosRedeNeural = lintCodigoDadosRedeNeural,

                                                  };

                Global.MonografiaEntities.Treinamento.Add(novotreinamento);
                Global.MonografiaEntities.SaveChanges();

                int lintProximoCodigotreinamentodados;
                try
                {
                    lintProximoCodigotreinamentodados = Global.MonografiaEntities.TreinamentoDados.Max(x => x.Codigo);
                }
                catch (Exception)
                {
                    lintProximoCodigotreinamentodados = 0;
                }

                foreach (Elemento elemento in lelementos)
                {
                    IEnumerable<DadosTreinamento> lEnuDadosTreinamento = (from llnqDadosTreinamento in Global.MonografiaEntities.DadosTreinamento
                                                                          where llnqDadosTreinamento.Elemento == elemento.Codigo
                                                                          select llnqDadosTreinamento).Take(lintNumeroExemplos);

                    foreach (DadosTreinamento dadosTreinamento in lEnuDadosTreinamento)
                    {
                        lintProximoCodigotreinamentodados = lintProximoCodigotreinamentodados + 1;
                        TreinamentoDados novotreinamentodados = new TreinamentoDados
                                                                    {
                                                                        Codigo = lintProximoCodigotreinamentodados,
                                                                        DadosTreinamento = dadosTreinamento.Codigo,
                                                                        Treinamento = novotreinamento.Codigo,
                                                                        Validado = false
                                                                    };

                        Global.MonografiaEntities.TreinamentoDados.Add(novotreinamentodados);

                    }
                    Global.MonografiaEntities.SaveChanges();
                }

                int lintProximoCodigoValidacaoDados;
                try
                {
                    lintProximoCodigoValidacaoDados = Global.MonografiaEntities.ValidacaoDados.Max(x => x.Codigo);
                }
                catch (Exception)
                {
                    lintProximoCodigoValidacaoDados = 0;
                }

                IEnumerable<DadosValidacao> lenuDadosValidacao = Global.MonografiaEntities.DadosValidacao;

                foreach (DadosValidacao dadosTreinamento in lenuDadosValidacao)
                {
                    lintProximoCodigotreinamentodados = lintProximoCodigoValidacaoDados + 1;
                    ValidacaoDados novoValidacaodados = new ValidacaoDados
                    {
                        Codigo = lintProximoCodigotreinamentodados,
                        DadosValidacao = dadosTreinamento.Codigo,
                        Treinamento = novotreinamento.Codigo,
                        Validado = false
                    };

                    Global.MonografiaEntities.ValidacaoDados.Add(novoValidacaodados);

                }
                Global.MonografiaEntities.SaveChanges();


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            List<int> llstNumeroExemplos = new List<int>() { 10, 30, 50 };

            List<DadosRedeNeural> llstDadosRedeNeural = new MonografiaEntities().DadosRedeNeural.ToList();

            IList<Elemento> lelementos = Global.MonografiaEntities.Elemento.ToList();


            foreach (int lintNumeroExemplos in llstNumeroExemplos)
            {
                foreach (DadosRedeNeural dadosRedeNeural in llstDadosRedeNeural)
                {
                    int lintProximoCodigotreinamento;
                    try
                    {
                        lintProximoCodigotreinamento = Global.MonografiaEntities.Treinamento.Max(x => x.Codigo);
                    }
                    catch (Exception)
                    {
                        lintProximoCodigotreinamento = 0;
                    }
                    Treinamento novotreinamento = new Treinamento
                    {
                        Codigo = lintProximoCodigotreinamento + 1,
                        Algoritmo = 2,
                        Momentum = 0,
                        TaxaAprendizado = 0,
                        NumeroExemplos = lintNumeroExemplos,
                        RedeNeuralResultante1 = new RedeNeuralResultante { PesosResultante = new byte[0] },
                        DadosRedeNeural = dadosRedeNeural.Codigo,

                    };

                    Global.MonografiaEntities.Treinamento.Add(novotreinamento);
                    Global.MonografiaEntities.SaveChanges();

                    int lintProximoCodigotreinamentodados;
                    try
                    {
                        lintProximoCodigotreinamentodados = Global.MonografiaEntities.TreinamentoDados.Max(x => x.Codigo);
                    }
                    catch (Exception)
                    {
                        lintProximoCodigotreinamentodados = 0;
                    }

                    foreach (Elemento elemento in lelementos)
                    {
                        IEnumerable<DadosTreinamento> lEnuDadosTreinamento = (from llnqDadosTreinamento in Global.MonografiaEntities.DadosTreinamento
                                                                              where llnqDadosTreinamento.Elemento == elemento.Codigo
                                                                              select llnqDadosTreinamento).Take(lintNumeroExemplos);

                        foreach (DadosTreinamento dadosTreinamento in lEnuDadosTreinamento)
                        {
                            lintProximoCodigotreinamentodados = lintProximoCodigotreinamentodados + 1;
                            TreinamentoDados novotreinamentodados = new TreinamentoDados
                            {
                                Codigo = lintProximoCodigotreinamentodados,
                                DadosTreinamento = dadosTreinamento.Codigo,
                                Treinamento = novotreinamento.Codigo,
                                Validado = false
                            };

                            Global.MonografiaEntities.TreinamentoDados.Add(novotreinamentodados);
                        }
                        Global.MonografiaEntities.SaveChanges();
                    }

                    int lintProximoCodigoValidacaoDados;
                    try
                    {
                        lintProximoCodigoValidacaoDados = Global.MonografiaEntities.ValidacaoDados.Max(x => x.Codigo);
                    }
                    catch (Exception)
                    {
                        lintProximoCodigoValidacaoDados = 0;
                    }

                    IEnumerable<DadosValidacao> lenuDadosValidacao = Global.MonografiaEntities.DadosValidacao;

                    foreach (DadosValidacao dadosTreinamento in lenuDadosValidacao)
                    {
                        lintProximoCodigotreinamentodados = lintProximoCodigoValidacaoDados + 1;
                        ValidacaoDados novoValidacaodados = new ValidacaoDados
                        {
                            Codigo = lintProximoCodigotreinamentodados,
                            DadosValidacao = dadosTreinamento.Codigo,
                            Treinamento = novotreinamento.Codigo,
                            Validado = false
                        };

                        Global.MonografiaEntities.ValidacaoDados.Add(novoValidacaodados);

                    }
                    Global.MonografiaEntities.SaveChanges();
                }
            }


        }
    }
}
