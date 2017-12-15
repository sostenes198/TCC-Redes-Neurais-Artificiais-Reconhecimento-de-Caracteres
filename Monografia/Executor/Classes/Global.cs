using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge;
using AForge.Imaging;
using AForge.Math.Geometry;
using Executor.Formularios.Visualizacao;
using Executor.Modelo;
using Image = System.Drawing.Image;

namespace Executor.Classes
{
    public static class Global
    { 
        internal static MonografiaEntities MonografiaEntities = new MonografiaEntities();

        public enum AlgoritmoTreinamento
        {
            BackPropagation = 1,
            ResilientPropagation = 2
        }

        public enum TipoImagem
        {
            Treinamento,
            Validacao
        }


        public static Bitmap MakeGrayscale(Bitmap original)
        {
            //make an empty bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    //get the pixel from the original image
                    Color originalColor = original.GetPixel(i, j);

                    //create the grayscale version of the pixel
                    int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .59)
                        + (originalColor.B * .11));

                    //create the color object
                    Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, newColor);
                }
            }

            return newBitmap;
        }

        public static Bitmap InverterCores(Bitmap original)
        {
            //make an empty bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    //get the pixel from the original image
                    Color originalColor = original.GetPixel(i, j);
                    Color newColor;
                    //create the grayscale version of the pixel
                    if (originalColor.R == 251 && originalColor.G == 251 && originalColor.B == 251)
                    {
                        newColor = Color.Black;
                    }
                    else
                    {
                        newColor = Color.White;
                    }

                    //int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .59)
                    //    + (originalColor.B * .11));

                    //create the color object
                    //Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, newColor);
                }
            }

            return newBitmap;
        }

        public static Bitmap ReverterCores(Bitmap original)
        {
            //make an empty bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    //get the pixel from the original image
                    Color originalColor = original.GetPixel(i, j);
                    Color newColor;
                    //create the grayscale version of the pixel
                    if (originalColor.R == Color.Black.R && 
                        originalColor.G == Color.Black.G && 
                        originalColor.B == Color.Black.B)
                    {
                        newColor = Color.White;
                    }
                    else
                    {
                        newColor = Color.Black;
                    }

                    //int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .59)
                    //    + (originalColor.B * .11));

                    //create the color object
                    //Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, newColor);
                }
            }

            return newBitmap;
        }


        //public static Bitmap ResizeImage(Image imgToResize, Size size)
        //{
        //    int sourceWidth = imgToResize.Width;
        //    int sourceHeight = imgToResize.Height;

        //    float nPercentW = (size.Width / (float)sourceWidth);
        //    float nPercentH = (size.Height / (float)sourceHeight);

        //    float nPercent = nPercentH < nPercentW ? nPercentH : nPercentW;

        //    int destWidth = (int)(sourceWidth * nPercent);
        //    int destHeight = (int)(sourceHeight * nPercent);

        //    Bitmap b = new Bitmap(destWidth, destHeight);

        //    Graphics g = Graphics.FromImage(b);
        //    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //    Image x = SetGrayscale(imgToResize as Bitmap);
        //    g.DrawImage(x, 0, 0, destWidth, destHeight);
        //    g.Dispose();

        //    return b;
        //}

        public static Bitmap Resize(Bitmap _currentBitmap, int newWidth, int newHeight)
        {
            if (newWidth != 0 && newHeight != 0)
            {
                Bitmap temp = (Bitmap)_currentBitmap;
                Bitmap bmap = new Bitmap(newWidth, newHeight, temp.PixelFormat);

                double nWidthFactor = (double)temp.Width / (double)newWidth;
                double nHeightFactor = (double)temp.Height / (double)newHeight;

                int cx, cy, fr_x, fr_y;
                Color color1 = new Color();
                Color color2 = new Color();
                Color color3 = new Color();
                Color color4 = new Color();
                byte nRed, nGreen, nBlue;

                byte bp1, bp2;

                for (int x = 0; x < bmap.Width; ++x)
                {
                    for (int y = 0; y < bmap.Height; ++y)
                    {

                        fr_x = (int)Math.Floor(x * nWidthFactor);
                        fr_y = (int)Math.Floor(y * nHeightFactor);
                        cx = fr_x + 1;
                        if (cx >= temp.Width) cx = fr_x;
                        cy = fr_y + 1;
                        if (cy >= temp.Height) cy = fr_y;
                        double fx = x * nWidthFactor - fr_x;
                        double fy = y * nHeightFactor - fr_y;
                        double nx = 1.0 - fx;
                        double ny = 1.0 - fy;

                        color1 = temp.GetPixel(fr_x, fr_y);
                        color2 = temp.GetPixel(cx, fr_y);
                        color3 = temp.GetPixel(fr_x, cy);
                        color4 = temp.GetPixel(cx, cy);

                        // Blue
                        bp1 = (byte)(nx * color1.B + fx * color2.B);

                        bp2 = (byte)(nx * color3.B + fx * color4.B);

                        nBlue = (byte)(ny * (double)(bp1) + fy * (double)(bp2));

                        // Green
                        bp1 = (byte)(nx * color1.G + fx * color2.G);

                        bp2 = (byte)(nx * color3.G + fx * color4.G);

                        nGreen = (byte)(ny * (double)(bp1) + fy * (double)(bp2));

                        // Red
                        bp1 = (byte)(nx * color1.R + fx * color2.R);

                        bp2 = (byte)(nx * color3.R + fx * color4.R);

                        nRed = (byte)(ny * (double)(bp1) + fy * (double)(bp2));

                        bmap.SetPixel(x, y, System.Drawing.Color.FromArgb
                (255, nRed, nGreen, nBlue));
                    }
                }
                return (Bitmap)bmap.Clone();
            }
            return null;
        }


        public static Bitmap SetGrayscale(Bitmap _currentBitmap)
        {
            Bitmap temp = _currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    byte gray = (byte)(.299 * c.R + .587 * c.G + .114 * c.B);

                    bmap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            return (Bitmap)bmap.Clone();
        }

        public static List<Byte> MakeArrayByte(Bitmap original)
        {
            List<Byte> bytes = new List<byte>();


            //make an empty bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    //get the pixel from the original image
                    Color originalColor = original.GetPixel(i, j);

                    if (originalColor.B == originalColor.G && originalColor.G == originalColor.R)
                    {
                        bytes.Add(0);
                    }
                    else
                    {
                        bytes.Add(1);
                    }

                    ////create the grayscale version of the pixel
                    //int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .59)
                    //    + (originalColor.B * .11));

                    ////create the color object
                    //Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);

                    ////set the new image's pixel to the grayscale version
                    //newBitmap.SetPixel(i, j, newColor);
                }
            }

            return bytes;
        }

        public static List<Byte> MakeArrayByteOriginal(Bitmap original)
        {

            MemoryStream ms = new MemoryStream();
            original.Save(ms, ImageFormat.Bmp);
            return ms.ToArray().ToList();
        }

        public static Bitmap RetornaBitmapBaseByteArray(byte[] byteArray)
        {
            ImageConverter ic = new ImageConverter();
            Image img = (Image)ic.ConvertFrom(byteArray);
            Bitmap bitmap1 = new Bitmap(img);
            return bitmap1;
        }

        public static string BuscarNomeElemento(double dblValor)
        {
            try
            {
                Double ldblValor = Double.Parse(Decimal.Round(Decimal.Parse(dblValor.ToString(CultureInfo.InvariantCulture).Replace('.', ',')), 2).ToString(CultureInfo.InvariantCulture).Replace('.', ','));
                Elemento lElemento =
                    MonografiaEntities.Elemento.First(x => x.ValorMinimo <= ldblValor && x.ValorMaximo >= ldblValor);
                return lElemento.Descricao;
            }
            catch (Exception)
            {
                return "Não sei o que é isso: " + dblValor;
            }
        }

        ////make an empty bitmap the same size as original
        //Bitmap newBitmap = new Bitmap(original.Width, original.Height);

        //for (int i = 0; i < original.Width; i++)
        //{
        //    for (int j = 0; j < original.Height; j++)
        //    {
        //        //get the pixel from the original image
        //        Color originalColor = original.GetPixel(i, j);

        //        if (originalColor.B == originalColor.G && originalColor.G == originalColor.R)
        //        {
        //            bytes.Add(0);
        //        }
        //        else
        //        {
        //            bytes.Add(1);
        //        }

        //        ////create the grayscale version of the pixel
        //        //int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .59)
        //        //    + (originalColor.B * .11));

        //        ////create the color object
        //        //Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);

        //        ////set the new image's pixel to the grayscale version
        //        //newBitmap.SetPixel(i, j, newColor);
        //    }
        //}

        //    return bytes;
        //}

        public static double[] RecuperaValoresNormalizados(byte[] valorBanco)
        {
            string lstr = Encoding.ASCII.GetString(valorBanco);

            List<double> llstValores = new List<double>();

            string[] lstrPartes = lstr.Split(';');

            foreach (string lstrParte in lstrPartes)
            {
                if (lstrParte.Trim().Length != 0)
                    llstValores.Add(double.Parse(lstrParte));
            }
            return llstValores.ToArray();
        }

        public static double[] retornaDesenhoNormalizado(byte[] lBytesImagemOriginal)
        {
            Bitmap lbtmOriginal = RetornaBitmapBaseByteArray(lBytesImagemOriginal);
            

            Bitmap lbtmBlackWhite = Global.MakeGrayscale(lbtmOriginal);


            lbtmOriginal.Save("E:\\Temp\\TESTE11.bmp");
            //// load an image
            //System.Drawing.Bitmap image = (Bitmap)Bitmap.FromFile(fileName);
            AForge.Imaging.Image.FormatImage(ref lbtmOriginal);
            // create filter
            //AForge.Imaging.Filters.Median filter = new AForge.Imaging.Filters.Median(); 
            // apply filter
            //System.Drawing.Bitmap newImage = filter.Apply(lbtmOriginal);
            Bitmap invertido = Global.InverterCores(lbtmOriginal);
            invertido.Save("E:\\Temp\\TESTE12.bmp");
            // locate objects using blob counter
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.ProcessImage(invertido);
            Blob[] blobs = blobCounter.GetObjectsInformation();
            // create Graphics object to draw on the image and a pen
            Graphics g = Graphics.FromImage(invertido);
            Pen bluePen = new Pen(Color.Blue, 2);
            // check each object and draw circle around objects, which
            // are recognized as circles
            List<IntPoint> corners;
            int lintMinX = 99999;
            int lintMaxX = 0;
            int lintMinY = 99999;
            int lintMaxY = 0;

            for (int i = 0, n = blobs.Length; i < n; i++)
            {
                List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blobs[i]);
                corners = PointsCloud.FindQuadrilateralCorners(edgePoints);

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

            if(lintMinX == 99999 && lintMaxX == 0 && lintMinY == 99999 && lintMaxY == 0)
            {
                MessageBox.Show("Não foi desenhado nada nessa imagem!");
                return null;
            }

            int lintSizeX = lintMinX - 5 + lintMaxX - lintMinX + 10;
            int lintSizeY = lintMinY - 5 + lintMaxY - lintMinY + 10;


            int lintAjusteX = (lintSizeX < 100) ? 10 : lintSizeX - 100;
            int lintAjusteY = (lintSizeY < 100) ? 10 : lintSizeY - 100;

            Rectangle corte = new Rectangle((lintMinX < 5)
                                                ? lintMinX
                                                : lintMinX - 5,
                                            (lintMinY < 5)
                                                ? lintMinY
                                                : lintMinY - 5,
                                            lintMaxX - lintMinX + lintAjusteX,
                                            lintMaxY - lintMinY + lintAjusteY);

            if(corte.Width > 100 || corte.Height >100)
            {
                MessageBox.Show("A imagem esta muito próximo das extremidades, isso impossibilita o corte da imagem!\nPor favor refaça o desenho!");
                return null;
            }

            Image lImage = cropImage(invertido, corte);
            lImage.Save("E:\\Temp\\TESTE13.bmp");

            //Reverter as cores
            Image lImageReve = Global.ReverterCores((Bitmap)lImage);
            lImageReve.Save("E:\\Temp\\TESTE14.bmp");
            //Resize
            Bitmap lbtmFinal = Global.Resize((Bitmap)lImageReve, 100, 100);
            lbtmFinal.Save("E:\\Temp\\TESTE15.bmp");


            bluePen.Dispose();
            g.Dispose();


            int contatotal = 0;
            int contaNiveisCinza = 0;
            int contaTotalNiveisCinza = 0;

            int contadorA = 0;
            int contadorB = 0;

            int andaA = lbtmFinal.Height / 10;
            int andaB = lbtmFinal.Width / 10;

            int controlaPos = 0;

            int a;
            int b;
            List<MapaBits> llstMapa = new List<MapaBits>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (a = contadorA; a < andaA; a++)
                    {
                        for (b = contadorB; b < andaB; b++)
                        {
                            Color x = lbtmFinal.GetPixel(b, a);
                            if (x.R != 255 && x.G != 255 && x.B != 255)
                            {
                                contaNiveisCinza++;
                                contaTotalNiveisCinza++;
                            }
                            contatotal++;
                        }
                    }
                    contadorB += 10;
                    andaB += 10;
                    llstMapa.Add(new MapaBits(i, j, contaNiveisCinza, 0));
                    controlaPos++;
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
            List<double> llstValores = new List<double>();

            foreach (MapaBits mapaBitse in llstMapa)
            {
                llstValores.Add(mapaBitse.Normalizado);
            }

            return llstValores.ToArray();
        }

        public static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, PixelFormat.Format1bppIndexed);// bmpImage.PixelFormat);
            return bmpCrop;
        }

        public static byte[] RetornaListaBytesValoresNormalizados(IEnumerable<MapaBits> llstMapa)
        {
            List<double> llstValores = llstMapa.Select(mapaBits => mapaBits.Normalizado).ToList();
            string lstrValores = llstValores.Aggregate("", (current, llstValore) => current + (llstValore + ";"));

            return Encoding.ASCII.GetBytes(lstrValores);
        }

    }
}
