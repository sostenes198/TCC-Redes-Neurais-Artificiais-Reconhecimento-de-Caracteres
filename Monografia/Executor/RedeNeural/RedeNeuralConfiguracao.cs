using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Encog.Engine.Network.Activation;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Neural.Networks.Structure;
using Executor.Classes;
using Executor.Modelo;

namespace Executor.RedeNeural
{
    internal class RedeNeuralConfiguracao
    {
        public static BasicNetwork RetornaRedeNeuralEspecifica(int intCodigoRedeNeural)
        {
            IEnumerable<DadosRedeNeural> lEnuDadosRedeNeural =
                from lLnqDadosRede in Global.MonografiaEntities.DadosRedeNeural
                where lLnqDadosRede.Codigo == intCodigoRedeNeural
                select lLnqDadosRede;

            if (!lEnuDadosRedeNeural.Any())
            {
                throw new Exception("Não existe a rede neural informada!");
            }


            var network = new BasicNetwork();

            network.AddLayer(new BasicLayer(null, true, lEnuDadosRedeNeural.First().NumeroNeuroniosEntrada));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), true,
                                            lEnuDadosRedeNeural.First().NumeroNeuroniosOculta));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), false,
                                            lEnuDadosRedeNeural.First().NumeroNeuroniosSaida));
            network.Structure.FinalizeStructure();

            var reader = new StreamReader(new MemoryStream(lEnuDadosRedeNeural.First().Pesos));

            string conteudo = reader.ReadToEnd();

            string[] pesos = conteudo.Split(';');
            var listapesos = new List<double>();

            foreach (string peso in pesos)
            {
                if (peso.Trim().Length != 0)
                {
                    listapesos.Add(double.Parse(peso.Replace(".", ",")));
                }
            }

            NetworkCODEC.ArrayToNetwork(listapesos.ToArray(), network);

            return network;
        }

        public static void GerarRedesNeurais(List<int> lstNeuroniosEntrada, List<int> lstNeuroniosOculta, List<int> lstNeuroniosSaida)
        {
            foreach (int intNeuroniosSaida in lstNeuroniosSaida)
            {
                foreach (int intNeuroniosOculta in lstNeuroniosOculta)
                {
                    foreach (int intNeuroniosEntrada in lstNeuroniosEntrada)
                    {
                        int saida = intNeuroniosSaida;
                        int oculta = intNeuroniosOculta;
                        int entrada = intNeuroniosEntrada;

                        IEnumerable<DadosRedeNeural> lEnumerable =
                            Global.MonografiaEntities.DadosRedeNeural.Where(
                                x =>
                                x.NumeroNeuroniosEntrada == entrada &&
                                x.NumeroNeuroniosOculta == oculta &&
                                x.NumeroNeuroniosSaida == saida);

                        if (!lEnumerable.Any())
                        {
                            GerarRedeNeural(intNeuroniosEntrada, intNeuroniosOculta, intNeuroniosSaida);
                        }
                    }
                }
            }
        }

        private static void GerarRedeNeural(int intNeuroniosEntrada, int intNeuroniosOculta, int intNeuroniosSaida)
        {
            var lDadosRedeNeural = new DadosRedeNeural
                                                   {
                                                       NumeroNeuroniosEntrada = intNeuroniosEntrada,
                                                       NumeroNeuroniosOculta = intNeuroniosOculta,
                                                       NumeroNeuroniosSaida = intNeuroniosSaida
                                                   };

            var network = new BasicNetwork();

            network.AddLayer(new BasicLayer(null, true, intNeuroniosEntrada));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, intNeuroniosOculta));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), false, intNeuroniosSaida));
            network.Structure.FinalizeStructure();

            network.Reset();

            lDadosRedeNeural.Pesos = Encoding.ASCII.GetBytes(RetornarPesosRedeNeural(network));

            Global.MonografiaEntities.DadosRedeNeural.Add(lDadosRedeNeural);
            Global.MonografiaEntities.SaveChanges();
        }

        public static string RetornarPesosRedeNeural(BasicNetwork network)
        {
            double[] pesos = NetworkCODEC.NetworkToArray(network);
            return pesos.Aggregate("", (current, peso) => current + (peso.ToString(CultureInfo.InvariantCulture) + ";"));
        }

        public static BasicNetwork RetornaRedeNeuralResultanteEspecifica(int intCodigoTreinamento)
        {
            Treinamento lTreinamento = Global.MonografiaEntities.Treinamento.First(x => x.Codigo == intCodigoTreinamento);

            DadosRedeNeural lEnuDadosRedeNeural =
                Global.MonografiaEntities.DadosRedeNeural.First(x => x.Codigo == lTreinamento.DadosRedeNeural);

            IEnumerable<RedeNeuralResultante> lEnuDadosRedeNeuralResultante =
                from lLnqDadosRede in Global.MonografiaEntities.RedeNeuralResultante
                where lLnqDadosRede.Codigo == lTreinamento.RedeNeuralResultante
                select lLnqDadosRede;

            if (!lEnuDadosRedeNeuralResultante.Any())
            {
                throw new Exception("Não existe a rede neural informada!");
            }


            var network = new BasicNetwork();

            network.AddLayer(new BasicLayer(null, true, lEnuDadosRedeNeural.NumeroNeuroniosEntrada));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), true,
                                            lEnuDadosRedeNeural.NumeroNeuroniosOculta));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), false,
                                            lEnuDadosRedeNeural.NumeroNeuroniosSaida));
            network.Structure.FinalizeStructure();

            var reader = new StreamReader(new MemoryStream(lEnuDadosRedeNeuralResultante.First().PesosResultante));

            string conteudo = reader.ReadToEnd();

            string[] pesos = conteudo.Split(';');
            var listapesos = new List<double>();

            foreach (string peso in pesos)
            {
                if (peso.Trim().Length != 0)
                {
                    listapesos.Add(double.Parse(peso.Replace(".", ",")));
                }
            }

            NetworkCODEC.ArrayToNetwork(listapesos.ToArray(), network);

            return network;
        }

        //public static BasicNetwork ConfigurarNovaRedeNeural()
        //{
        //    //if (_network == null)
        //    //{
        //    _network = RetornaRedeNeuralVazia();

        //    _network.Reset();

        //    SalvarDadosRedeNeural();
        //    // }

        //    return (BasicNetwork)_network.Clone();
        //}

        //public static BasicNetwork ConfigurarUltimaRedeNeural()
        //{
        //    if (_network == null)
        //    {
        //        _network = RetornaRedeNeuralVazia();
        //        CarregarDadosRedeNeural(NumeroNeuroniosCamadaOculta);
        //    }

        //    return (BasicNetwork)_network.Clone();
        //}

        ////private static void CarregarDadosRedeNeural(int numeroNeuroniosCamadaOculta)
        ////{
        ////    DadosRedeNeural dados = Global.Contexto.DadosRedeNeural.ToList().First(x => x.NumeroNeuroniosOculta == numeroNeuroniosCamadaOculta);

        ////    var reader = new StreamReader(dados.Pesos);

        ////    string conteudo = reader.ReadToEnd();

        ////    string[] pesos = conteudo.Split(';');
        ////    var listapesos = new List<double>();
        ////    foreach (string peso in pesos)
        ////    {
        ////        if (peso.Trim().Length != 0)
        ////        {
        ////            listapesos.Add(double.Parse(peso.Replace(".", ",")));
        ////        }
        ////    }
        ////    NetworkCODEC.ArrayToNetwork(listapesos.ToArray(), _network);

        ////}

        ////public static void CarregarDadosRedeNeural(int numeroNeuroniosCamadaOculta, BasicNetwork redeneural)
        ////{
        ////    DadosRedeNeural dados = Global.Contexto.DadosRedeNeural.ToList().First(x => x.NumeroNeuroniosOculta == numeroNeuroniosCamadaOculta);

        ////    var reader = new StreamReader(dados.Pesos);

        ////    string conteudo = reader.ReadToEnd();

        ////    string[] pesos = conteudo.Split(';');
        ////    var listapesos = new List<double>();
        ////    foreach (string peso in pesos)
        ////    {
        ////        if (peso.Trim().Length != 0)
        ////        {
        ////            listapesos.Add(double.Parse(peso.Replace(".", ",")));
        ////        }
        ////    }
        ////    NetworkCODEC.ArrayToNetwork(listapesos.ToArray(), redeneural);

        ////}

        //private static void SalvarDadosRedeNeural()
        //{

        //    string nomeArquivo = Global.PathDiretorioBancoDados + "/" + string.Format("RedeNeural_{0}_{1}_{2}_{3}.txt",
        //                         NumeroNeuroniosEntrada, NumeroNeuroniosCamadaOculta, NumeroNeuroniosSaida, DateTime.Now.ToShortDateString().Replace(@"/", "-"));

        //    var dadosRedeNeural = new DadosRedeNeural
        //    {
        //        NumeroNeuroniosEntrada = NumeroNeuroniosEntrada,
        //        NumeroNeuroniosOculta = NumeroNeuroniosCamadaOculta,
        //        NumeroNeuroniosSaida = NumeroNeuroniosSaida,
        //        Pesos = nomeArquivo,
        //    };
        //    File.Create(nomeArquivo).Close();

        //    var x = new StreamWriter(nomeArquivo);
        //    x.Write(RetornarPesosRedeNeural(_network));
        //    x.Close();


        //    Global.Contexto.DadosRedeNeural.InsertOnSubmit(dadosRedeNeural);
        //    Global.Contexto.SubmitChanges();
        //}

        //public static void SalvarDadosRedeNeural(BasicNetwork rede, int numeroNeuroniosCamadaOculta)
        //{
        //    rede.Reset();
        //    string nomeArquivo = Global.PathDiretorioBancoDados + "/" + string.Format("RedeNeural_{0}_{1}_{2}_{3}.txt",
        //                         NumeroNeuroniosEntrada, numeroNeuroniosCamadaOculta, NumeroNeuroniosSaida, DateTime.Now.ToShortDateString().Replace(@"/", "-"));

        //    var dadosRedeNeural = new DadosRedeNeural
        //    {
        //        NumeroNeuroniosEntrada = NumeroNeuroniosEntrada,
        //        NumeroNeuroniosOculta = numeroNeuroniosCamadaOculta,
        //        NumeroNeuroniosSaida = NumeroNeuroniosSaida,
        //        Pesos = nomeArquivo,
        //    };
        //    File.Create(nomeArquivo).Close();

        //    var x = new StreamWriter(nomeArquivo);
        //    x.Write(RetornarPesosRedeNeural(rede));
        //    x.Close();


        //    Global.Contexto.DadosRedeNeural.InsertOnSubmit(dadosRedeNeural);
        //    Global.Contexto.SubmitChanges();
        //}


    }
}
