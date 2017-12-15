using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Executor.Classes;
using Executor.Modelo;

namespace Executor.Formularios.Visualizacao
{
    public partial class FrmVisualizarDados : Form
    {
        private string _tipoDado;

        private FrmVisualizarDados()
        {
            InitializeComponent();
        }

        public FrmVisualizarDados(string tipoDado)
        {
            InitializeComponent();
            _tipoDado = tipoDado;
        }

        private void FrmVisualizarDados_Load(object sender, EventArgs e)
        {
            Text = "Visualização dos Dados de " + ((_tipoDado == "T") ? "Treinamento" : "Validação");
            CarregarDados();
        }

        private void CarregarDados()
        {
            IList y;
            if (_tipoDado == "T")
            {
                y = (from var in Global.MonografiaEntities.DadosTreinamento
                     select new { var.Codigo, var.Elemento1.Descricao, var.Elemento1.ValorMinimo, var.Elemento1.ValorIdeal, var.Elemento1.ValorMaximo }).ToList();

            }
            else
            {
                y = (from var in Global.MonografiaEntities.DadosValidacao
                     select new { var.Codigo, var.Elemento1.Descricao, var.Elemento1.ValorMinimo, var.Elemento1.ValorIdeal, var.Elemento1.ValorMaximo }).ToList();
            }

            dataGridView1.DataSource = y;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (_tipoDado == "T")
                {
                    int lintCodigo = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    DadosTreinamento ldadosAtual = Global.MonografiaEntities.DadosTreinamento.First(x => x.Codigo == lintCodigo);
                    Image lImage = Image.FromStream(new MemoryStream(ldadosAtual.FiguraOriginal));
                    panel1.BackgroundImage = lImage;
                    lImage.Save("E:\\Imagem\\Treinamento\\" + lintCodigo + ".jpeg");
                    dataGridView2.DataSource = GerarDados(ldadosAtual.Dados);
                }
                else
                {
                    int lintCodigo = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    DadosValidacao ldadosAtual = Global.MonografiaEntities.DadosValidacao.First(x => x.Codigo == lintCodigo);
                    Image lImage = Image.FromStream(new MemoryStream(ldadosAtual.FiguraOriginal));
                    panel1.BackgroundImage = lImage;
                    lImage.Save("E:\\Imagem\\Validacao\\" + lintCodigo + ".jpeg");
                    
                    dataGridView2.DataSource = GerarDados(ldadosAtual.Dados);
                }

            }
        }

        private DataTable GerarDados(byte[] p)
        {
            DataTable ldtbDesenho = new DataTable();

            int largura = 10;
            int altura = 10;

            for (int i = 0; i < largura; i++)
            {
                ldtbDesenho.Columns.Add("C" + i);
            }

            string lstrStrings = Encoding.ASCII.GetString(p);

            List<string> llstStrings = lstrStrings.Split(';').ToList();

            llstStrings.RemoveAll(x => x == "");

            for (int posIni = 0; posIni < altura * largura; posIni += 10)
            {
                ldtbDesenho.Rows.Add(llstStrings[posIni], llstStrings[posIni + (1)], llstStrings[posIni + (2)],
                                    llstStrings[posIni + (3)], llstStrings[posIni + (4)],
                                    llstStrings[posIni + (5)], llstStrings[posIni + (6)], llstStrings[posIni + (7)],
                                    llstStrings[posIni + (8)], llstStrings[posIni + (9)]);
            }
            return ldtbDesenho;

        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.Value != null)
            {
                e.CellStyle.BackColor = (e.Value.ToString() != "0") ? Color.Black : Color.White;
            }
        }
    }
}
