using ConsoleSelenium.Infra;
using OpenQA.Selenium;
using prmToolkit.Selenium;
using Selenium.Internal.SeleniumEmulation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ConsoleSelenium.Job
{
    public class Scrapping
    {
        public static async Task MREstoque()
        {
            IWebDriver webDriver = GetWebDrive.WebDrive();
            webDriver.Manage().Window.Maximize();

            List<String> Urls = ListUrl.GetUrl();

            try
            {
                foreach (var Url in Urls)
                {
                    webDriver.LoadPage(TimeSpan.FromSeconds(65), Url);

                    await Task.Delay(1000);

                    //Example javascript
                    //var Title = webDriver.ExecuteJavaScript($"return document.querySelector(\"#product-detail > div > div.product-data.flex__item--grow.flex.flex--column > h1\").innerText");
                    //Console.WriteLine(Title);

                    //Example FindElement
                    var TitleByXPath = webDriver.FindElement(By.XPath("/html/body/section/section[2]/div[2]/div/div/div[3]/h1")).Text;
                    //Console.WriteLine(TitleByXPath);

                    var CategoryByXPath = webDriver.FindElement(By.XPath("//*[@id=\"main\"]/section[2]/div[1]/ul/li[3]")).Text;
                    //Console.WriteLine(CategoryByXPath);

                    var SubByXPath = webDriver.FindElement(By.XPath("//*[@id=\"main\"]/section[2]/div[1]/ul/li[4]")).Text;
                    //Console.WriteLine(SubByXPath);

                    var FinalSubByXPath = webDriver.FindElement(By.XPath("//*[@id=\"main\"]/section[2]/div[1]/ul/li[5]/a")).Text;
                    //Console.WriteLine(FinalSubByXPath);

                    //var FichaByXPath = webDriver.FindElement(By.XPath("//*[@id=\"main\"]/section[2]/div[3]/div/div/div")).Text;
                    //Console.WriteLine(FichaByXPath);

                    var QuantidadeEmbalagem = webDriver.FindElement(By.XPath("/html/body/section/section[2]/div[3]/div/div/div/p[1]")).Text;
                    QuantidadeEmbalagem = QuantidadeEmbalagem.Replace("QuantidadeEmbalagem :", "");

                    var UnidadeBasica = webDriver.FindElement(By.XPath("/html/body/section/section[2]/div[3]/div/div/div/p[2]")).Text;
                    UnidadeBasica = UnidadeBasica.Replace("UnidadeBasica :", "");

                    var Complemento = webDriver.FindElement(By.XPath("/html/body/section/section[2]/div[3]/div/div/div/p[3]")).Text;
                    Complemento = Complemento.Replace("Complemento :", "");

                    var PesoLiquido = webDriver.FindElement(By.XPath("/html/body/section/section[2]/div[3]/div/div/div/p[4]")).Text;
                    PesoLiquido = PesoLiquido.Replace("PesoLiquido :", "");

                    var PesoBruto = webDriver.FindElement(By.XPath("/html/body/section/section[2]/div[3]/div/div/div/p[5]")).Text;
                    PesoBruto = PesoBruto.Replace("PesoBruto :", "");

                    var CodigoEmbalagem = webDriver.FindElement(By.XPath("/html/body/section/section[2]/div[3]/div/div/div/p[6]")).Text;
                    CodigoEmbalagem = CodigoEmbalagem.Replace("CodigoEmbalagem :", "");

                    var DescricaoEmbalagem = webDriver.FindElement(By.XPath("/html/body/section/section[2]/div[3]/div/div/div/p[7]")).Text;
                    DescricaoEmbalagem = DescricaoEmbalagem.Replace("DescricaoEmbalagem :", "");

                    var Ncm = webDriver.FindElement(By.XPath("/html/body/section/section[2]/div[3]/div/div/div/p[8]")).Text;
                    Ncm = Ncm.Replace("Ncm :", "");

                    var CodigoBarrasProduto = webDriver.FindElement(By.XPath("/html/body/section/section[2]/div[3]/div/div/div/p[9]")).Text;
                    CodigoBarrasProduto = CodigoBarrasProduto.Replace("CodigoBarrasProduto :", "");

                    var CodigoBarrasUnidade = webDriver.FindElement(By.XPath("/html/body/section/section[2]/div[3]/div/div/div/p[10]")).Text;
                    CodigoBarrasUnidade = CodigoBarrasUnidade.Replace("CodigoBarrasUnidade :", "");

                    var CodigoItem = webDriver.FindElement(By.XPath("/html/body/section/section[2]/div[3]/div/div/div/p[11]")).Text;
                    CodigoItem = CodigoItem.Replace("CodigoItem :", "");

                    var DescricaoItem = webDriver.FindElement(By.XPath("/html/body/section/section[2]/div[3]/div/div/div/p[12]")).Text;
                    DescricaoItem = DescricaoItem.Replace("DescricaoItem :", "");

                    var TipoProduto = webDriver.FindElement(By.XPath("/html/body/section/section[2]/div[3]/div/div/div/p[13]")).Text;
                    TipoProduto = TipoProduto.Replace("TipoProduto :", "");

                    var Marca = webDriver.FindElement(By.XPath("/html/body/section/section[2]/div[3]/div/div/div/p[14]")).Text;
                    Marca = Marca.Replace("Marca :", "");

                    var QuantidadeMultiplaVenda = webDriver.FindElement(By.XPath("/html/body/section/section[2]/div[3]/div/div/div/p[15]")).Text;
                    QuantidadeMultiplaVenda = QuantidadeMultiplaVenda.Replace("QuantidadeMultiplaVenda :", "");

                    var ImageByXPath = webDriver.FindElement(By.XPath("/html/body/section/section[2]/div[2]/div/div/div[1]/div/nav/ul/li/img"));

                    var GetImgUrl = ImageByXPath.GetAttribute("src");
                    GetImgUrl = GetImgUrl.Replace("/470/", "/1024/");



                    using (WebClient webClient = new WebClient())
                    {
                        byte[] data = webClient.DownloadData(GetImgUrl.ToString());

                        using (MemoryStream mem = new MemoryStream(data))
                        {
                            using (var yourImage = Image.FromStream(mem))
                            {
                                // If you want it as Png
                                yourImage.Save(string.Concat(@"C:/ScrappingImages/", CodigoBarrasUnidade, ".JPEG"));

                            }
                        }
                    }

                    using (SqlConnection openCon = new SqlConnection("Data Source=DESKTOP-MMO76AK;Initial Catalog=ScrappingDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                    {
                        openCon.Open();
                        string saveStaff = "INSERT into dbo.DadosProduto (Titulo, Categoria, SubCategoria, SubCategoriaFinal,QuantidadeEmbalagem, UnidadeBasica, Complemento, PesoLiquido, PesoBruto, CodigoEmbalagem, DescricaoEmbalagem, Ncm, CodigoBarrasProduto, CodigoBarrasUnidade, CodigoItem, DescricaoItem, TipoProduto, Marca, QuantidadeMultiplaVenda)" +
                                                       " VALUES (@TitleByXPath, @CategoryByXPath, @SubByXPath, @FinalSubByXPath, @QuantidadeEmbalagem, @UnidadeBasica, @Complemento, @PesoLiquido, @PesoBruto, @CodigoEmbalagem, @DescricaoEmbalagem, @Ncm, @CodigoBarrasProduto, @CodigoBarrasUnidade, @CodigoItem, @DescricaoItem, @TipoProduto, @Marca, @QuantidadeMultiplaVenda)";

                        using (SqlCommand querySaveStaff = new SqlCommand(saveStaff, openCon))
                        {
                            querySaveStaff.Parameters.AddWithValue("@TitleByXPath", TitleByXPath);
                            querySaveStaff.Parameters.AddWithValue("@CategoryByXPath", CategoryByXPath);
                            querySaveStaff.Parameters.AddWithValue("@SubByXPath", SubByXPath);
                            querySaveStaff.Parameters.AddWithValue("@FinalSubByXPath", FinalSubByXPath);
                            querySaveStaff.Parameters.AddWithValue("@QuantidadeEmbalagem", QuantidadeEmbalagem);
                            querySaveStaff.Parameters.AddWithValue("@UnidadeBasica", UnidadeBasica);
                            querySaveStaff.Parameters.AddWithValue("@Complemento", Complemento);
                            querySaveStaff.Parameters.AddWithValue("@PesoLiquido", PesoLiquido);
                            querySaveStaff.Parameters.AddWithValue("@PesoBruto", PesoBruto);
                            querySaveStaff.Parameters.AddWithValue("@CodigoEmbalagem", CodigoEmbalagem);
                            querySaveStaff.Parameters.AddWithValue("@DescricaoEmbalagem", DescricaoEmbalagem);
                            querySaveStaff.Parameters.AddWithValue("@Ncm", Ncm);
                            querySaveStaff.Parameters.AddWithValue("@CodigoBarrasProduto", CodigoBarrasProduto);
                            querySaveStaff.Parameters.AddWithValue("@CodigoBarrasUnidade", CodigoBarrasUnidade);
                            querySaveStaff.Parameters.AddWithValue("@CodigoItem", CodigoItem);
                            querySaveStaff.Parameters.AddWithValue("@DescricaoItem", DescricaoItem);
                            querySaveStaff.Parameters.AddWithValue("@TipoProduto", TipoProduto);
                            querySaveStaff.Parameters.AddWithValue("@Marca", Marca);
                            querySaveStaff.Parameters.AddWithValue("@QuantidadeMultiplaVenda", QuantidadeMultiplaVenda);


                            querySaveStaff.ExecuteNonQuery();
                        }
                    }




                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                webDriver.Close();
                webDriver.Dispose();
            }
        }

    }
}
