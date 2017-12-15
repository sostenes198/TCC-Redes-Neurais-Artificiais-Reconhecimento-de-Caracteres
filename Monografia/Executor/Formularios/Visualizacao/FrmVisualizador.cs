using System;
using System.Collections;
using System.Windows.Forms;

namespace Executor.Formularios.Visualizacao
{
    public partial class FrmVisualizador : Form
    {
        private readonly IList _lLstDados;
        private readonly string _strTituloFormulario;

        public FrmVisualizador(IList lLstDados, string strTituloFormulario)
        {
            _lLstDados = lLstDados;
            _strTituloFormulario = strTituloFormulario;
            InitializeComponent();
        }

        private void FrmVisualizador_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _lLstDados;
            this.Text = _strTituloFormulario;
        }
    }
}
