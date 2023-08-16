using ConsoleSelenium.Infra;
using OpenQA.Selenium;
using prmToolkit.Selenium;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSelenium.Job.SAVarejo
{
    public class Scrapping
    {
        public static async void Data()
        {
            IWebDriver webDriver = GetWebDrive.WebDrive();
            webDriver.Manage().Window.Maximize();

            webDriver.LoadPage(TimeSpan.FromSeconds(65), "https://www.savarejo.com.br/gds-guia-de-sortimento");

            var cookieWarning = webDriver.FindElement(By.XPath("/html/body/div[1]/button"));
            cookieWarning.Click();

            for (int i = 0; i < 14; i++)
            {
                var countingLi = webDriver.ExecuteJavaScript($"return document.getElementsByClassName('tab-pane')[{i}].getElementsByTagName('ul')[0].getElementsByTagName('li').length").ToString();
                for (int e = 0; e < Convert.ToInt32(countingLi); e++)
                {
                    var urlPagina = webDriver.ExecuteJavaScript($"return document.getElementsByClassName('tab-pane')[{i}].getElementsByTagName('ul')[0].getElementsByTagName('li')[{e}].getElementsByTagName('a')[0].href");
                    webDriver.LoadPage(TimeSpan.FromSeconds(65), urlPagina.ToString());

                    //Descer a tela
                    webDriver.ExecuteJavaScript("window.scrollTo(0, 500)");

                    //Clicar Area IV 
                    var AreaIV = webDriver.FindElement(By.XPath("//*[@id=\"18568\"]/a"));
                    AreaIV.Click();

                    //Pegar dados 2022, e 2021 separados por linhas em variáveis

                    //Ano de 2022
                    var primeiro = webDriver.FindElement(By.XPath("//*[@id=\"conteudoInfoEsquerda\"]/ul/li[1]")).Text;
                    var segundo = webDriver.FindElement(By.XPath("//*[@id=\"conteudoInfoEsquerda\"]/ul/li[2]")).Text;
                    var terceiro = webDriver.FindElement(By.XPath("//*[@id=\"conteudoInfoEsquerda\"]/ul/li[3]")).Text;
                    var quarto = webDriver.FindElement(By.XPath("//*[@id=\"conteudoInfoEsquerda\"]/ul/li[4]")).Text;
                    var quinto = webDriver.FindElement(By.XPath("//*[@id=\"conteudoInfoEsquerda\"]/ul/li[5]")).Text;

                    string[] ano2022 = new string[5] { primeiro, segundo, terceiro, quarto, quinto };

                    //Ano de 2021
                    var first = webDriver.FindElement(By.XPath("//*[@id=\"conteudoInfoDireita\"]/ul/li[1]")).Text;
                    var second = webDriver.FindElement(By.XPath("//*[@id=\"conteudoInfoDireita\"]/ul/li[2]")).Text;
                    var third = webDriver.FindElement(By.XPath("//*[@id=\"conteudoInfoDireita\"]/ul/li[3]")).Text;
                    var fourth = webDriver.FindElement(By.XPath("//*[@id=\"conteudoInfoDireita\"]/ul/li[4]")).Text;
                    var fiveth = webDriver.FindElement(By.XPath("//*[@id=\"conteudoInfoDireita\"]/ul/li[5]")).Text;

                    string[] ano2021 = new string[5] { first, second, third, fourth, fiveth };


                    //Pegar dados adicionais (3 varíaveis e unir separando com vírgula)
                    var data1 = webDriver.FindElement(By.XPath("//*[@id=\"detalhes-pesquisa\"]/ul/li[1]")).Text;
                    var data2 = webDriver.FindElement(By.XPath("//*[@id=\"detalhes-pesquisa\"]/ul/li[2]")).Text;
                    var data3 = webDriver.FindElement(By.XPath("//*[@id=\"detalhes-pesquisa\"]/ul/li[3]")).Text;

                    string[] dadosAdicionais = new string[] { data1, data2, data3 };

                    //Pegar o Top fornecedor (verificar se existe a classe de 1 a 5)



                    var posicao1 = "";
                    var posicao2 = "";
                    var posicao3 = "";
                    var posicao4 = "";
                    var posicao5 = "";





                    try
                    {
                        var titulo = webDriver.ExecuteJavaScript("return document.querySelector(\"#search_tab_0 > div.top-providers > div > div.chart-podium--body > div.chart-podium--pos01 > div.chart-podium--pos-body > div.chart-podium--element01 > span\").innerText").ToString();
                        var porcentagem = webDriver.ExecuteJavaScript("return document.querySelector(\"#search_tab_0 > div.top-providers > div > div.chart-podium--body > div.chart-podium--pos01 > div.chart-podium--pos-footer > span\").innerHTML").ToString();
                        posicao1 = "1 " + titulo + " " + porcentagem;

                    }
                    catch (Exception)
                    {


                    }

                    try
                    {
                        var titulo = webDriver.ExecuteJavaScript("return document.querySelector(\"#search_tab_0 > div.top-providers > div > div.chart-podium--body > div.chart-podium--pos02 > div.chart-podium--pos-body > div.chart-podium--element01 > span\").innerText").ToString();
                        var porcentagem = webDriver.ExecuteJavaScript("return document.querySelector(\"#search_tab_0 > div.top-providers > div > div.chart-podium--body > div.chart-podium--pos02 > div.chart-podium--pos-footer > span\").innerHTML").ToString();
                        posicao2 = "2 " + titulo + " " + porcentagem;
                    }
                    catch (Exception)
                    {


                    }

                    try
                    {
                        var titulo = webDriver.ExecuteJavaScript("return document.querySelector(\"#search_tab_0 > div.top-providers > div > div.chart-podium--body > div.chart-podium--pos03 > div.chart-podium--pos-body > div.chart-podium--element01 > span\").innerText").ToString();
                        var porcentagem = webDriver.ExecuteJavaScript("return document.querySelector(\"#search_tab_0 > div.top-providers > div > div.chart-podium--body > div.chart-podium--pos03 > div.chart-podium--pos-footer > span\").innerHTML").ToString();
                        posicao3 = "3 " + titulo + " " + porcentagem;

                    }
                    catch (Exception)
                    {


                    }

                    try
                    {

                        var titulo = webDriver.ExecuteJavaScript("return document.querySelector(\"#search_tab_0 > div.top-providers > div > div.chart-podium--body > div.chart-podium--pos04 > div.chart-podium--pos-body > div.chart-podium--element01 > span\").innerText").ToString();
                        var porcentagem = webDriver.ExecuteJavaScript("return document.querySelector(\"#search_tab_0 > div.top-providers > div > div.chart-podium--body > div.chart-podium--pos04 > div.chart-podium--pos-footer > span\").innerHTML").ToString();
                        posicao4 = "4 " + titulo + " " + porcentagem;
                    }
                    catch (Exception)
                    {


                    }

                    try
                    {
                        var titulo = webDriver.ExecuteJavaScript("return document.querySelector(\"#search_tab_0 > div.top-providers > div > div.chart-podium--body > div.chart-podium--pos05 > div.chart-podium--pos-body > div.chart-podium--element01 > span\").innerText").ToString();
                        var porcentagem = webDriver.ExecuteJavaScript("return document.querySelector(\"#search_tab_0 > div.top-providers > div > div.chart-podium--body > div.chart-podium--pos05 > div.chart-podium--pos-footer > span\").innerHTML").ToString();
                        posicao5 = "5 " + titulo + " " + porcentagem;
                    }
                    catch (Exception)
                    {


                    }






                    using (SqlConnection openCon = new SqlConnection("Data Source=DESKTOP-MMO76AK;Initial Catalog=ScrappingSA;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                    {
                        openCon.Open();
                        string saveStaff = "INSERT INTO [dbo].[SAVarejo] ([urlPagina],[ano2022],[ano2021],[dadosAdicionais],[posicao1],[posicao2] ,[posicao3],[posicao4],[posicao5])" +
                            "VALUES(@urlPagina, @ano2022, @ano2021, @dadosAdicionais, @posicao1, @posicao2, @posicao3, @posicao4, @posicao5)";

                        using (SqlCommand querySaveStaff = new SqlCommand(saveStaff, openCon))
                        {
                            querySaveStaff.Parameters.AddWithValue("@urlPagina", urlPagina);
                            querySaveStaff.Parameters.AddWithValue("@ano2022", string.Join(", ", ano2022));
                            querySaveStaff.Parameters.AddWithValue("@ano2021", string.Join(", ", ano2021));
                            querySaveStaff.Parameters.AddWithValue("@dadosAdicionais", string.Join(", ", dadosAdicionais));
                            querySaveStaff.Parameters.AddWithValue("@posicao1", posicao1);
                            querySaveStaff.Parameters.AddWithValue("@posicao2", posicao2);
                            querySaveStaff.Parameters.AddWithValue("@posicao3", posicao3);
                            querySaveStaff.Parameters.AddWithValue("@posicao4", posicao4);
                            querySaveStaff.Parameters.AddWithValue("@posicao5", posicao5);


                            querySaveStaff.ExecuteNonQuery();
                        }
                    }


                    //if (!!(bool)webDriver.ExecuteJavaScript($"document.getElementsByClassName('chart-podium--pos01')"))

                    //{
                    //    var Top1 = webDriver.FindElement(By.XPath("//*[@id=\"search_tab_0\"]/div[3]/div/div[2]/div[1]/div[1]/div[1]/span")).Text;
                    //    var percentualTop1 = webDriver.FindElement(By.XPath("//*[@id=\"search_tab_0\"]/div[3]/div/div[2]/div[1]/div[2]/span")).Text;
                    //    Top1 = Top1 + ": " + percentualTop1;

                    //} else if ((bool)webDriver.ExecuteJavaScript("getElementByClassName(chart-podium--pos02))"))
                    //{
                    //    var Top2 = webDriver.FindElement(By.XPath("//*[@id=\"search_tab_0\"]/div[3]/div/div[2]/div[2]/div[1]/div[1]/span")).Text;
                    //    var percentualTop2 = webDriver.FindElement(By.XPath("//*[@id=\"search_tab_0\"]/div[3]/div/div[2]/div[2]/div[2]/span")).Text;
                    //    Top2 = Top2 + ": " + percentualTop2;
                    //} else if ((bool)webDriver.ExecuteJavaScript("getElementByClassName(chart-podium--pos03))"))
                    //{
                    //    var Top3 = webDriver.FindElement(By.XPath("//*[@id=\"search_tab_0\"]/div[3]/div/div[2]/div[3]/div[1]/div[1]/span")).Text;
                    //    var percentualTop3 = webDriver.FindElement(By.XPath("//*[@id=\"search_tab_0\"]/div[3]/div/div[2]/div[3]/div[2]/span")).Text;
                    //    Top3 = Top3 + ": " + percentualTop3;
                    //} else if ((bool)webDriver.ExecuteJavaScript("getElementByClassName(chart-podium--pos04))"))
                    //{
                    //    var Top4 = webDriver.FindElement(By.XPath("//*[@id=\"search_tab_0\"]/div[3]/div/div[2]/div[4]/div[1]/div[1]/span")).Text;
                    //    var percentualTop4 = webDriver.FindElement(By.XPath("//*[@id=\"search_tab_0\"]/div[3]/div/div[2]/div[4]/div[2]/span")).Text;
                    //    Top4 = Top4 + ": " + percentualTop4;
                    //} else if ((bool)webDriver.ExecuteJavaScript("getElementByClassName(chart-podium--pos05))"))
                    //{
                    //    var Top5 = webDriver.FindElement(By.XPath("//*[@id=\"search_tab_0\"]/div[3]/div/div[2]/div[5]/div[1]/div[1]/span")).Text;
                    //    var percentualTop5 = webDriver.FindElement(By.XPath("//*[@id=\"search_tab_0\"]/div[3]/div/div[2]/div[5]/div[2]/span")).Text;
                    //    Top5 = Top5 + ": " + percentualTop5;
                    //}

                    //Salvar no banco local




                }

            }
            //var cookieWarning = webDriver.FindElement(By.XPath("/html/body/div[1]/button"));
            //    cookieWarning.Click();

            //    var MerceariaDeAltoGiro = webDriver.FindElement(By.XPath("/html/body/main/div/div/div[2]/div[1]/div[1]/div[2]/ul/li[1]/a"));
            //    var AcucarRefinado = webDriver.FindElement(By.XPath("/html/body/main/div/div/div[2]/div[1]/div[1]/div[2]/ul/li[1]/div/ul/li[1]/a"));

            //    var categorias = AcucarRefinado.GetAttribute("href");
            //    webDriver.LoadPage(TimeSpan.FromSeconds(65), categorias);


            //    //var AreaIV = webDriver.FindElement(By.Id("18568"));
            //    var AreaIV = webDriver.FindElement(By.XPath("//*[@id=\"18568\"]/a"));
            //    AreaIV.Click();

            //    Task.Delay(1850);


            //var NomeArea = webDriver.FindElement(By.XPath("//*[@id=\"h4Titulo\"]")).Text;

            //if (NomeArea == "Área IV Grande SP")
            //{



            //    var ranking2022 = webDriver.FindElement(By.XPath("/html/body/main/div/div/div[5]/div[1]/div/div[2]/div[3]/div[1]/div[2]/div/div[1]/div[2]/div/div/div[2]/div[1]/div[2]")).Text;

            //    var ranking2021 = webDriver.FindElement(By.XPath("/html/body/main/div/div/div[5]/div[1]/div/div[2]/div[3]/div[1]/div[2]/div/div[1]/div[2]/div/div/div[2]/div[2]/div[2]")).Text;

            //    var dadosAdicionais = webDriver.FindElement(By.XPath("//*[@id=\"detalhes-pesquisa\"]")).Text;
            //};


            //webDriver.LoadPage(TimeSpan.FromSeconds(65), SP);

        }
    }
}

