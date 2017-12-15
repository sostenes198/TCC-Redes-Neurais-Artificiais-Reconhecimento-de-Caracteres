using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Encog.ML.Data;
using Encog.ML.Data.Basic;
using Encog.ML.Train;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Training.Propagation.Back;
using Encog.Neural.Networks.Training.Propagation.Resilient;
using Executor.Classes;
using Executor.Modelo;

namespace Executor.RedeNeural
{
    public class ExecutorTreinamento
    {
        private const int MaximoGeracoesTreinamento = 20000;
        private const double MargemErroTreinamento = 0.001D;


        public static BasicNetwork TreinarRedeNeural(BasicNetwork redeneural, List<int> codigosDadosTreinamento, int codigoTreinamento/*, Global.AlgoritmoTreinamento algoritmoTreinamento*/)
        {
            Treinamento lTreinamentoAtual = Global.MonografiaEntities.Treinamento.First(x => x.Codigo == codigoTreinamento);

            if (lTreinamentoAtual.NumeroGeracoes != 0)
            {
                return null;
            }


            var lLstDadosTreino = Global.MonografiaEntities.DadosTreinamento.ToList();
            List<DadosTreinamento> lLstDadosTreinoUsado = new List<DadosTreinamento>();
            foreach (DadosTreinamento dadosTreinamento in lLstDadosTreino)
            {
                if (codigosDadosTreinamento.Contains(dadosTreinamento.Codigo))
                {
                    lLstDadosTreinoUsado.Add(dadosTreinamento);
                }
            }

            switch (lTreinamentoAtual.Algoritmo)
            {
                case (int)Global.AlgoritmoTreinamento.BackPropagation:
                    return TreinarRedeBackPropagation(redeneural, GerarDados(lLstDadosTreinoUsado), lTreinamentoAtual);

                case (int)Global.AlgoritmoTreinamento.ResilientPropagation:
                    return TreinarResilientPropagation(redeneural, GerarDados(lLstDadosTreinoUsado), lTreinamentoAtual);
            }
            return null;
        }

        private static BasicNetwork TreinarResilientPropagation(BasicNetwork redeNeural, IMLDataSet trainingset, Treinamento lTreinamentoAtual)
        {
            // train the neural network
            IMLTrain train = new ResilientPropagation(redeNeural, trainingset);
            List<double> llstGeracao = new List<double>();

            int epoch = 1;
            do
            {
                train.Iteration();
                llstGeracao.Add(train.Error);
                if (epoch > MaximoGeracoesTreinamento)
                {
                    Global.MonografiaEntities = new MonografiaEntities();
                    Treinamento ltreinamentoAux = Global.MonografiaEntities.Treinamento.First(x => x.Codigo == lTreinamentoAtual.Codigo);
                    Global.MonografiaEntities.Entry(ltreinamentoAux).State = EntityState.Modified;
                    ltreinamentoAux.MinimoLocal = true;
                    Global.MonografiaEntities.SaveChanges();
                    return null;
                }
                epoch++;
            } while (train.Error > MargemErroTreinamento);

            Global.MonografiaEntities = new MonografiaEntities();

            for (int i = 0; i < llstGeracao.Count; i++)
            {
                LogTreinamento logTreinamento = new LogTreinamento();
                logTreinamento.Treinamento = lTreinamentoAtual.Codigo;
                logTreinamento.Geracao = i + 1;
                logTreinamento.MargemErro = llstGeracao[i];

                Global.MonografiaEntities.LogTreinamento.Add(logTreinamento);
            }
            Global.MonografiaEntities.SaveChanges();

            Global.MonografiaEntities = new MonografiaEntities();
            Treinamento ltreinamento = Global.MonografiaEntities.Treinamento.First(x => x.Codigo == lTreinamentoAtual.Codigo);

            Global.MonografiaEntities.Entry(ltreinamento).State = EntityState.Modified;

            ltreinamento.NumeroGeracoes = epoch;
            ltreinamento.TaxaAprendizado = lTreinamentoAtual.TaxaAprendizado;
            ltreinamento.Momentum = lTreinamentoAtual.Momentum;
            ltreinamento.RedeNeuralResultante1.PesosResultante = Encoding.ASCII.GetBytes(RedeNeuralConfiguracao.RetornarPesosRedeNeural(redeNeural));


            //Global.monografiaEntities.Entry(ltreinamento).CurrentValues.SetValues(ltreinamentoAlterado);

            Global.MonografiaEntities.SaveChanges();

            return redeNeural;
        }

        private static BasicNetwork TreinarRedeBackPropagation(BasicNetwork redeNeural, IMLDataSet trainingset, Treinamento lTreinamentoAtual)
        {
            // train the neural network
            IMLTrain train = new Backpropagation(redeNeural, trainingset, lTreinamentoAtual.TaxaAprendizado, lTreinamentoAtual.Momentum);
            List<double> llstGeracao = new List<double>();

            int epoch = 1;
            do
            {
                train.Iteration();
                llstGeracao.Add(train.Error);
                if (epoch > MaximoGeracoesTreinamento)
                {
                    Global.MonografiaEntities = new MonografiaEntities();
                    Treinamento ltreinamentoAux = Global.MonografiaEntities.Treinamento.First(x => x.Codigo == lTreinamentoAtual.Codigo);
                    Global.MonografiaEntities.Entry(ltreinamentoAux).State = EntityState.Modified;
                    ltreinamentoAux.MinimoLocal = true;
                    Global.MonografiaEntities.SaveChanges();
                    return null;
                }
                epoch++;
            } while (train.Error > MargemErroTreinamento);

            Global.MonografiaEntities = new MonografiaEntities();

            for (int i = 0; i < llstGeracao.Count; i++)
            {
                LogTreinamento logTreinamento = new LogTreinamento();
                logTreinamento.Treinamento = lTreinamentoAtual.Codigo;
                logTreinamento.Geracao = i + 1;
                logTreinamento.MargemErro = llstGeracao[i];

                Global.MonografiaEntities.LogTreinamento.Add(logTreinamento);
            }
            Global.MonografiaEntities.SaveChanges();

            Global.MonografiaEntities = new MonografiaEntities();
            Treinamento ltreinamento = Global.MonografiaEntities.Treinamento.First(x => x.Codigo == lTreinamentoAtual.Codigo);

            Global.MonografiaEntities.Entry(ltreinamento).State = EntityState.Modified;

            ltreinamento.NumeroGeracoes = epoch;
            ltreinamento.TaxaAprendizado = lTreinamentoAtual.TaxaAprendizado;
            ltreinamento.Momentum = lTreinamentoAtual.Momentum;
            ltreinamento.RedeNeuralResultante1.PesosResultante = Encoding.ASCII.GetBytes(RedeNeuralConfiguracao.RetornarPesosRedeNeural(redeNeural));


            //Global.monografiaEntities.Entry(ltreinamento).CurrentValues.SetValues(ltreinamentoAlterado);

            Global.MonografiaEntities.SaveChanges();

            return redeNeural;
        }

        public static IMLDataSet GerarDados(List<DadosTreinamento> dados)
        {
            IList<IMLDataPair> listaDados = new List<IMLDataPair>();

            foreach (DadosTreinamento treino in dados)
            {
                IMLData dado = new BasicMLData(Global.RecuperaValoresNormalizados(treino.Dados));

                var listaDoublesValores = new List<double> { treino.Elemento1.ValorIdeal };

                IMLData valor = new BasicMLData(listaDoublesValores.ToArray());

                IMLDataPair par = new BasicMLDataPair(dado, valor);

                listaDados.Add(par);
            }

            return new BasicMLDataSet(listaDados);
        }

        public static double ExecutarRedeNeural(Treinamento lTreinamento, double[] dblImagem)
        {
            BasicNetwork lBasicNetwork = RedeNeuralConfiguracao.RetornaRedeNeuralResultanteEspecifica(lTreinamento.Codigo);
            double[] lDblRetorno = new double[1];

            //double[] lDoubles = new double[dblImagem.Count()];
            //for (int i = 0; i < dblImagem.Count(); i++)
            //{
            //    lDoubles[i] = double.Parse(dblImagem[i].ToString(CultureInfo.InvariantCulture));
            //}


            lBasicNetwork.Compute(dblImagem, lDblRetorno);

            return lDblRetorno[0];
        }



    }
}