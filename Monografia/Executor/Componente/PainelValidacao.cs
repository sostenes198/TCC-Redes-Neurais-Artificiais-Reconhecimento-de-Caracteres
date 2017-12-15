using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Executor.Classes;
using Executor.Modelo;

namespace Executor.Componente
{
    public partial class PainelValidacao : UserControl
    {
        //[Category("Personalizado"), DefaultValue(Global.TipoImagem.Treinamento)]
        //public Global.TipoImagem TipoImagem { get; set; }


        public PainelValidacao()
        {
            InitializeComponent();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            controleDesenho.LimparDesenho();
        }

        private void btnAcao_Click(object sender, EventArgs e)
        {
            byte[] original;

            byte[] redimensionada = RetornaArrayDesenho(out original);

            int lintCodigo = int.Parse(txtCodigoTreinamento.Text);
            Treinamento lTreinamento = Global.MonografiaEntities.Treinamento.FirstOrDefault(x => x.Codigo == lintCodigo);

            if (lTreinamento == null)
            {
                MessageBox.Show("Não é possível testar um Treinamento que não exista!");
                return;
            }

            if (lTreinamento.MinimoLocal)
            {
                MessageBox.Show("Não é possível testar um Treinamento que tenha atingido o Minimo Local!");
                return;
            }


            Double[] llstDouble = Global.retornaDesenhoNormalizado(original);

            if (llstDouble == null)
                return;


            Double ldlbResultado = RedeNeural.ExecutorTreinamento.ExecutarRedeNeural(lTreinamento, llstDouble);



            MessageBox.Show("Isso é um " + Global.BuscarNomeElemento(ldlbResultado) + "\nValor: " + ldlbResultado);
        }

        //public FormasGeometricas RetornaFormaSelecionada()
        //{
        //    if (rbtCirculo.Checked)
        //    {
        //        return FormasGeometricas.Circulo;
        //    }
        //    if (rbtQuadrado.Checked)
        //    {
        //        return FormasGeometricas.Quadrado;
        //    }
        //    //if (rbtTriangulo.Checked)
        //    else
        //    {
        //        return FormasGeometricas.Triangulo;
        //    }
        //}

        public byte[] RetornaArrayDesenho(out byte[] byteOriginal)
        {

            Bitmap bmap = new Bitmap(controleDesenho.Width, controleDesenho.Height);

            using (Graphics g = Graphics.FromImage(bmap))
            {
                //int esquerda = ((this.ParentForm.WindowState == FormWindowState.Normal) ? ((this.Parent.Left < 0) ? this.Parent.Left * -1 : this.Parent.Left) : 0) + this.Left + controleDesenho.Left + 8;
                //int topo = this.Top + controleDesenho.Top + 48;

                //G.CopyFromScreen(new Point(esquerda, topo), Point.Empty, controleDesenho.Size);

                int width = controleDesenho.Size.Width;
                int height = controleDesenho.Size.Height;

                Bitmap bm = new Bitmap(width, height);
                controleDesenho.DrawToBitmap(bm, new Rectangle(0, 0, width, height));

                //bm.Save(@"C:\Temp\Teste3.bmp", ImageFormat.Bmp);

                byteOriginal = Global.MakeArrayByteOriginal(bm).ToArray();

                bmap = Global.Resize(bm, 15, 15);
                //bmap.Save(@"C:\Temp\Teste2.bmp", ImageFormat.Bmp);

                //if (TipoImagem == TipoImagem.Treinamento)
                //{
                //    bmap.Save(Global.RetornaCaminhoCompletoImagemTreinamento());
                //}
                //else if (TipoImagem == TipoImagem.Validacao)
                //{
                //    bmap.Save(Global.RetornaCaminhoCompletoImagemValidacao());
                //}

                List<byte> sdf = Global.MakeArrayByte(bmap);


                return sdf.ToArray();
            }
        }


        private static Bitmap resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return b;
        }


    }
}
