using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AForge;
using AForge.Imaging;
using Executor.Classes;
using Executor.Modelo;
using Executor.Formularios.Visualizacao;
using Image = System.Drawing.Image;
using System.IO;

namespace Executor.Formularios
{
    public partial class FrmConversor : Form
    {
        public FrmConversor()
        {
            InitializeComponent();
        }

        private void Button2Click(object sender, EventArgs e)
        {
            ConverterDadosTreinamento();
            ConverterDadosValidacao();
        }


        private void ConverterDadosTreinamento()
        {
            using (MonografiaEntities monografiaEntities = new MonografiaEntities())
            {
                List<DadosTreinamento> lLstDadosTreinamento = monografiaEntities.DadosTreinamento.ToList();

                foreach (DadosTreinamento dadosTreinamento in lLstDadosTreinamento)
                {
                    byte[] lBytesImagemOriginal = dadosTreinamento.FiguraOriginal;

                    Bitmap lbtmOriginal = Global.RetornaBitmapBaseByteArray(lBytesImagemOriginal);
                    string lstrDiretorioImagens = "E:\\Imagens\\Transformacoes\\Treinamento\\" + dadosTreinamento.Codigo + "\\";
                    if (!Directory.Exists(lstrDiretorioImagens))
                    {
                        Directory.CreateDirectory(lstrDiretorioImagens);
                    }
                    lbtmOriginal.Save(lstrDiretorioImagens + "Fase1.bmp");
                    //// load an image
                    //System.Drawing.Bitmap image = (Bitmap)Bitmap.FromFile(fileName);
                    AForge.Imaging.Image.FormatImage(ref lbtmOriginal);
                    // create filter
                    //AForge.Imaging.Filters.Median filter = new AForge.Imaging.Filters.Median(); 
                    // apply filter
                    //System.Drawing.Bitmap newImage = filter.Apply(lbtmOriginal);
                    Bitmap invertido = Global.InverterCores(lbtmOriginal);
                    invertido.Save(lstrDiretorioImagens + "Fase2.bmp");
                    // locate objects using blob counter
                    BlobCounter blobCounter = new BlobCounter();
                    blobCounter.ProcessImage(invertido);
                    Blob[] blobs = blobCounter.GetObjectsInformation();
                    // create Graphics object to draw on the image and a pen
                    Graphics g = Graphics.FromImage(invertido);
                    Pen bluePen = new Pen(Color.Blue, 2);
                    // check each object and draw circle around objects, which
                    // are recognized as circles
                    int lintMinX = 99999;
                    int lintMaxX = 0;
                    int lintMinY = 99999;
                    int lintMaxY = 0;

                    for (int i = 0, n = blobs.Length; i < n; i++)
                    {
                        List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blobs[i]);


                        foreach (IntPoint edgePoint in edgePoints)
                        {
                            if (edgePoint.X < lintMinX)
                            {
                                lintMinX = edgePoint.X;
                            }
                            if (edgePoint.Y < lintMinY)
                            {
                                lintMinY = edgePoint.Y;
                            }


                            if (edgePoint.X > lintMaxX)
                            {
                                lintMaxX = edgePoint.X;
                            }
                            if (edgePoint.Y > lintMaxY)
                            {
                                lintMaxY = edgePoint.Y;
                            }
                        }

                        // g.DrawPolygon(bluePen, ToPointsArray(corners));

                    }

                    int lintSizeX = lintMinX - 5 + lintMaxX - lintMinX + 10;
                    int lintSizeY = lintMinY - 5 + lintMaxY - lintMinY + 10;


                    int lintAjusteX = (lintSizeX < 100) ? 10 : lintSizeX - 100;
                    int lintAjusteY = (lintSizeY < 100) ? 10 : lintSizeY - 100;

                    Image lImage = Global.cropImage(invertido, new Rectangle((lintMinX < 5) ? lintMinX : lintMinX - 5, (lintMinY < 5) ? lintMinY : lintMinY - 5, lintMaxX - lintMinX + lintAjusteX, lintMaxY - lintMinY + lintAjusteY));
                    lImage.Save(lstrDiretorioImagens + "Fase3.bmp");

                    //Reverter as cores
                    Image lImageReve = Global.ReverterCores((Bitmap)lImage);
                    lImageReve.Save(lstrDiretorioImagens + "Fase4.bmp");
                    //Resize
                    Bitmap lbtmFinal = Global.Resize((Bitmap)lImageReve, 100, 100);

                    lbtmFinal.Save(lstrDiretorioImagens + "Fase5.bmp");
                    lbtmFinal.Save("E:\\Imagens\\Treinamento\\" + dadosTreinamento.Codigo + ".bmp");


                    bluePen.Dispose();
                    g.Dispose();

                    int contaNiveisCinza = 0;

                    int contadorA = 0;
                    int contadorB = 0;

                    int andaA = lbtmFinal.Height / 10;
                    int andaB = lbtmFinal.Width / 10;

                    List<MapaBits> llstMapa = new List<MapaBits>();
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            for (int a = contadorA; a < andaA; a++)
                            {
                                for (int b = contadorB; b < andaB; b++)
                                {
                                    Color x = lbtmFinal.GetPixel(b, a);
                                    if (x.R != 255 && x.G != 255 && x.B != 255)
                                    {
                                        contaNiveisCinza++;
                                    }
                                }
                            }
                            contadorB += 10;
                            andaB += 10;
                            llstMapa.Add(new MapaBits(i, j, contaNiveisCinza, 0));
                            contaNiveisCinza = 0;
                        }
                        contadorA += 10;
                        contadorB = 0;
                        andaA += 10;
                        andaB = 10;
                    }

                    foreach (MapaBits lclsMapaBits in llstMapa)
                    {
                        lclsMapaBits.Normalizado = Math.Round((lclsMapaBits.NumeroBitsRegiao == 0) ? 0 : 1D / lclsMapaBits.NumeroBitsRegiao, 4);// / maior;
                    }
                    Global.MonografiaEntities = new MonografiaEntities();
                    DadosTreinamento lTreinamentoDados = Global.MonografiaEntities.DadosTreinamento.First(x => x.Codigo == dadosTreinamento.Codigo);

                    Global.MonografiaEntities.Entry(lTreinamentoDados).State = EntityState.Modified;
                    lTreinamentoDados.Dados = Global.RetornaListaBytesValoresNormalizados(llstMapa);
                    Global.MonografiaEntities.SaveChanges();

                }
            }

        }

        private void ConverterDadosValidacao()
        {


            using (MonografiaEntities monografiaEntities = new MonografiaEntities())
            {
                List<DadosValidacao> lLstDadosValidacao = monografiaEntities.DadosValidacao.ToList();

                foreach (DadosValidacao dadosValidacao in lLstDadosValidacao)
                {
                    byte[] lBytesImagemOriginal = dadosValidacao.FiguraOriginal;

                    Bitmap lbtmOriginal = Global.RetornaBitmapBaseByteArray(lBytesImagemOriginal);

                    string lstrDiretorioImagens = "E:\\Imagens\\Transformacoes\\validacao\\" + dadosValidacao.Codigo + "\\";
                    if (!Directory.Exists(lstrDiretorioImagens))
                    {
                        Directory.CreateDirectory(lstrDiretorioImagens);
                    }


                    lbtmOriginal.Save(lstrDiretorioImagens + "Fase1.bmp");
                    //// load an image
                    //System.Drawing.Bitmap image = (Bitmap)Bitmap.FromFile(fileName);
                    AForge.Imaging.Image.FormatImage(ref lbtmOriginal);
                    // create filter
                    //AForge.Imaging.Filters.Median filter = new AForge.Imaging.Filters.Median(); 
                    // apply filter
                    //System.Drawing.Bitmap newImage = filter.Apply(lbtmOriginal);
                    Bitmap invertido = Global.InverterCores(lbtmOriginal);
                    invertido.Save(lstrDiretorioImagens + "Fase2.bmp");
                    // locate objects using blob counter
                    BlobCounter blobCounter = new BlobCounter();
                    blobCounter.ProcessImage(invertido);
                    Blob[] blobs = blobCounter.GetObjectsInformation();
                    // create Graphics object to draw on the image and a pen
                    Graphics g = Graphics.FromImage(invertido);
                    // check each object and draw circle around objects, which
                    // are recognized as circles
                    int lintMinX = 99999;
                    int lintMaxX = 0;
                    int lintMinY = 99999;
                    int lintMaxY = 0;

                    for (int i = 0, n = blobs.Length; i < n; i++)
                    {
                        List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blobs[i]);

                        foreach (IntPoint edgePoint in edgePoints)
                        {
                            if (edgePoint.X < lintMinX)
                            {
                                lintMinX = edgePoint.X;
                            }
                            if (edgePoint.Y < lintMinY)
                            {
                                lintMinY = edgePoint.Y;
                            }


                            if (edgePoint.X > lintMaxX)
                            {
                                lintMaxX = edgePoint.X;
                            }
                            if (edgePoint.Y > lintMaxY)
                            {
                                lintMaxY = edgePoint.Y;
                            }
                        }

                        // g.DrawPolygon(bluePen, ToPointsArray(corners));

                    }

                    int lintSizeX = lintMinX - 5 + lintMaxX - lintMinX + 10;
                    int lintSizeY = lintMinY - 5 + lintMaxY - lintMinY + 10;


                    int lintAjusteX = (lintSizeX < 100) ? 10 : lintSizeX - 100;
                    int lintAjusteY = (lintSizeY < 100) ? 10 : lintSizeY - 100;

                    Image lImage = Global.cropImage(invertido, new Rectangle((lintMinX < 5) ? lintMinX : lintMinX - 5, (lintMinY < 5) ? lintMinY : lintMinY - 5, lintMaxX - lintMinX + lintAjusteX, lintMaxY - lintMinY + lintAjusteY));
                    lImage.Save(lstrDiretorioImagens + "Fase3.bmp");

                    //Reverter as cores
                    Image lImageReve = Global.ReverterCores((Bitmap)lImage);
                    lImageReve.Save(lstrDiretorioImagens + "Fase4.bmp");
                    //Resize
                    Bitmap lbtmFinal = Global.Resize((Bitmap)lImageReve, 100, 100);

                    lbtmFinal.Save(lstrDiretorioImagens + "Fase5.bmp");
                    lbtmFinal.Save("E:\\Imagens\\Validacao\\" + dadosValidacao.Codigo + ".bmp");

                    g.Dispose();



                    int contaNiveisCinza = 0;

                    int contadorA = 0;
                    int contadorB = 0;

                    int andaA = lbtmFinal.Height / 10;
                    int andaB = lbtmFinal.Width / 10;

                    List<MapaBits> llstMapa = new List<MapaBits>();
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            for (int a = contadorA; a < andaA; a++)
                            {
                                for (int b = contadorB; b < andaB; b++)
                                {
                                    Color x = lbtmFinal.GetPixel(b, a);
                                    if (x.R != 255 && x.G != 255 && x.B != 255)
                                    {
                                        contaNiveisCinza++;
                                    }
                                }
                            }
                            contadorB += 10;
                            andaB += 10;
                            llstMapa.Add(new MapaBits(i, j, contaNiveisCinza, 0));
                            contaNiveisCinza = 0;
                        }
                        contadorA += 10;
                        contadorB = 0;
                        andaA += 10;
                        andaB = 10;
                    }

                    foreach (MapaBits lclsMapaBits in llstMapa)
                    {
                        lclsMapaBits.Normalizado = (lclsMapaBits.NumeroBitsRegiao == 0) ? 0 : 1D / lclsMapaBits.NumeroBitsRegiao;// / maior;
                    }
                    Global.MonografiaEntities = new MonografiaEntities();
                    DadosValidacao lDadosValidacao = Global.MonografiaEntities.DadosValidacao.First(x => x.Codigo == dadosValidacao.Codigo);

                    Global.MonografiaEntities.Entry(lDadosValidacao).State = EntityState.Modified;
                    lDadosValidacao.Dados = Global.RetornaListaBytesValoresNormalizados(llstMapa);
                    Global.MonografiaEntities.SaveChanges();
                }
            }
        }
    }
}
