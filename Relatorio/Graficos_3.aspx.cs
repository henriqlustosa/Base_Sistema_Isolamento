using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

public partial class Relatorio_Graficos_3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnCarregar3_Click(object sender, EventArgs e)
    {
         string anoPesquis = ddlAno2.SelectedItem.Text;

            string microrganismos = ddlmicrorganismos2.SelectedItem.Text;
          
            if (anoPesquis == "Tudo")
            {
                int ano = 0;
               
               
                try
                {
                    using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
                    {

                        SqlCommand cmm = cnn.CreateCommand();
                        cmm.CommandText = "SELECT DISTINCT YEAR([dt_resultado]) AS Ano FROM [Isolamento].[dbo].[Exame] ORDER BY Ano DESC";



                        cnn.Open();
                        SqlDataReader dr = cmm.ExecuteReader();

                        while (dr.Read())
                        {
                            
                          
                            ano = dr.GetInt32(0);
                            try
                            {


                                using (SqlConnection cnn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
                                {

									SqlCommand cmm1 = cnn1.CreateCommand();
									cmm1.CommandText = "SELECT (CASE WHEN Q2.TheMonth =1 THEN 'JAN'  WHEN Q2.TheMonth = 2 THEN 'FEV'  WHEN Q2.TheMonth  = 3 THEN 'MAR' WHEN Q2.TheMonth =4 THEN 'ABR' WHEN Q2.TheMonth = 5 THEN 'MAI' WHEN Q2.TheMonth  = 6 THEN 'JUN' " +
				                    "WHEN Q2.TheMonth =7 THEN 'JUL' WHEN Q2.TheMonth = 8 THEN 'AGO'  WHEN Q2.TheMonth  = 9 THEN 'SET' WHEN Q2.TheMonth =10 THEN 'OUT' WHEN Q2.TheMonth = 11 THEN 'NOV' WHEN Q2.TheMonth  = 12 THEN 'DEZ'" +
				                    "ELSE NULL END) as Mes ,  ISNULL(Q1.Total, 0 ) FROM" +
				                    "(" +
				                    "SELECT mes , count(*) as Total FROM ( " +
				                    "SELECT  e.[rh],mic.descricao, MONTH(e.dt_resultado) as mes " +
				                    "FROM [Isolamento].[dbo].[tipos_microorganismos] as mic inner join " +
				                    "[Isolamento].[dbo].[Exame] as e  on mic.cod_microorg = e.microorganismo where mic.descricao ='" + microrganismos + "' and Year(e.dt_resultado) = " + ano + ") p " +
				                    " group by p.mes " +
				                    ") Q1 " +
				                    "RIGHT JOIN " +
				                    "(" +
				                    "SELECT TOP (DATEDIFF(MONTH, '2012-01-01', '2013-01-31'))" +
				                    "TheMonth = MONTH(DATEADD(MONTH, number, '2012-01-01')) " +
				                    "FROM [master].dbo.spt_values WHERE [type] = N'P' ORDER BY number " +
				                    ") Q2 " +
				                    " ON Q1.Mes = Q2.TheMonth";
									;

                                   

                                    cnn1.Open();
                                    
                                    SqlDataReader dr1 = cmm1.ExecuteReader();
                                    ChartDistribuicaoMic.Series.Add("MicrorganismosAno" + ano.ToString());
                                    ChartDistribuicaoMic.Series["MicrorganismosAno" + ano.ToString()].ChartType = SeriesChartType.Line;

                                   Legend secondLegend = new Legend();

                                    this.ChartDistribuicaoMic.Legends.Add(secondLegend);

                                    ChartDistribuicaoMic.Series["MicrorganismosAno" + ano.ToString()].LegendText = ano.ToString();
                                    while (dr1.Read())
                                    {
                                        
                                        ChartDistribuicaoMic.Series["MicrorganismosAno" + ano.ToString()].Points.AddXY(dr1.GetString(0), dr1.GetInt32(1));
                                      
                                    }
                                    if (ano == 2018)
                                    ChartDistribuicaoMic.Series["MicrorganismosAno" + ano.ToString()].Color = Color.Red;
                                    else if (ano == 2017)
                                        ChartDistribuicaoMic.Series["MicrorganismosAno" + ano.ToString()].Color = Color.Green;
                                    else if (ano == 2016)
                                        ChartDistribuicaoMic.Series["MicrorganismosAno" + ano.ToString()].Color = Color.Orange;
                                    else if (ano == 2015)
                                        ChartDistribuicaoMic.Series["MicrorganismosAno" + ano.ToString()].Color = Color.Blue;
                                    else
                                        ChartDistribuicaoMic.Series["MicrorganismosAno" + ano.ToString()].Color = Color.Indigo;
                                    
                                    
                                    ChartDistribuicaoMic.Visible = true;
                                  




                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("{0} Exception caught.", ex);
                            }



                        }
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine("{0} Exception caught.", ex);
                }
            }





            else
            {
                try
                {


                    using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
                    {

						SqlCommand cmm = cnn.CreateCommand();
						cmm.CommandText = "SELECT (CASE WHEN Q2.TheMonth =1 THEN 'JAN'  WHEN Q2.TheMonth = 2 THEN 'FEV'  WHEN Q2.TheMonth  = 3 THEN 'MAR' WHEN Q2.TheMonth =4 THEN 'ABR' WHEN Q2.TheMonth = 5 THEN 'MAI' WHEN Q2.TheMonth  = 6 THEN 'JUN' " +
			"WHEN Q2.TheMonth =7 THEN 'JUL' WHEN Q2.TheMonth = 8 THEN 'AGO'  WHEN Q2.TheMonth  = 9 THEN 'SET' WHEN Q2.TheMonth =10 THEN 'OUT' WHEN Q2.TheMonth = 11 THEN 'NOV' WHEN Q2.TheMonth  = 12 THEN 'DEZ'" +
			"ELSE NULL END) as Mes ,  ISNULL(Q1.Total, 0 ) FROM" +
			"(" +
			"SELECT mes , count(*) as Total FROM ( " +
			"SELECT e.[rh],mic.descricao, MONTH(e.dt_resultado) as mes " +
			"FROM [Isolamento].[dbo].[tipos_microorganismos] as mic inner join " +
			"[Isolamento].[dbo].[Exame] as e  on mic.cod_microorg = e.microorganismo where mic.descricao ='" + microrganismos + "' and Year(e.dt_resultado) = " + anoPesquis + ") p " +
			" group by p.mes " +
			") Q1 " +
			"RIGHT JOIN " +
			"(" +
			"SELECT TOP (DATEDIFF(MONTH, '2012-01-01', '2013-01-31'))" + 
			"TheMonth = MONTH(DATEADD(MONTH, number, '2012-01-01')) " +
			"FROM [master].dbo.spt_values WHERE [type] = N'P' ORDER BY number " +
			") Q2 " +
			" ON Q1.Mes = Q2.TheMonth";



                        cnn.Open();
                        SqlDataReader dr = cmm.ExecuteReader();
                        ChartDistribuicaoMic.Series.Add("MicrorganismosAno");
                        ChartDistribuicaoMic.Series["MicrorganismosAno"].ChartType = SeriesChartType.Line;
                        Legend secondLegend = new Legend();

                        this.ChartDistribuicaoMic.Legends.Add(secondLegend);
                        ChartDistribuicaoMic.Series["MicrorganismosAno"].LegendText = anoPesquis;
                        while (dr.Read())
                        {
                            ChartDistribuicaoMic.Series["MicrorganismosAno"].Points.AddXY(dr.GetString(0), dr.GetInt32(1));

                        }
                        ChartDistribuicaoMic.Series["MicrorganismosAno"].Color = Color.Red;
                        ChartDistribuicaoMic.Visible = true;


                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("{0} Exception caught.", ex);
                }
            }
    }
    protected void btnCarregar4_Click(object sender, EventArgs e)
    {

        string anoPesquis = ddlAno3.SelectedItem.Text;

        string grupoSitio = dllGrupoSitios.SelectedItem.Text;
        if (anoPesquis == "Tudo")
        {
            int ano = 0;

            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
                {

                    SqlCommand cmm = cnn.CreateCommand();
                    cmm.CommandText = "SELECT DISTINCT YEAR([dt_resultado]) AS Ano FROM [Isolamento].[dbo].[Exame] ORDER BY Ano DESC";



                    cnn.Open();
                    SqlDataReader dr = cmm.ExecuteReader();

                    while (dr.Read())
                    {


                        ano = dr.GetInt32(0);
                        try
                        {


                            using (SqlConnection cnn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
                            {

                                SqlCommand cmm1 = cnn1.CreateCommand();
                                cmm1.CommandText = "SELECT (CASE WHEN Q2.TheMonth =1 THEN 'Jan'  WHEN Q2.TheMonth = 2 THEN 'Fev'  WHEN Q2.TheMonth  = 3 THEN 'Mar' WHEN Q2.TheMonth =4 THEN 'Abr' WHEN Q2.TheMonth = 5 THEN 'Mai' WHEN Q2.TheMonth  = 6 THEN 'Jun' " +
            "WHEN Q2.TheMonth =7 THEN 'Jul' WHEN Q2.TheMonth = 8 THEN 'Ago'  WHEN Q2.TheMonth  = 9 THEN 'Set' WHEN Q2.TheMonth =10 THEN 'Out' WHEN Q2.TheMonth = 11 THEN 'Nov' WHEN Q2.TheMonth  = 12 THEN 'Dez' " +
            "ELSE NULL END) as Mes ,  ISNULL(Q1.Total, 0 ) FROM" +
            "(" +

			"SELECT mes , count(*) as Total FROM (SELECT  e.rh , mic.descricao as mic, tm.descricao as tm,gm.descricao as gm, MONTH(e.dt_resultado) as mes FROM [Isolamento].[dbo].[Exame] as e " +
			"inner join [Isolamento].[dbo].[tipos_microorganismos] as mic " +
			"on mic.cod_microorg = e.microorganismo " +
			"inner join [Isolamento].[dbo].tipos_materiais as tm " +
			"on tm.cod_material = e.material " +
			"inner join [Isolamento].[dbo].Material_Grupo_Materiais as mg " +
			"on mg.cod_material= tm.cod_material " +
			"inner join [Isolamento].[dbo].Grupo_Materiais as gm " +
			"on gm.cod_grupo_materiais = mg.cod_grupo_materiais where gm.descricao  = '" + grupoSitio + "'  and Year(e.dt_resultado) = " + ano + ")  p " +
			"group by p.mes " +
			") Q1 " +
			"RIGHT JOIN " +
			"( " +
			"SELECT TOP (DATEDIFF(MONTH, '2012-01-01', '2013-01-31')) " +
			"TheMonth = MONTH(DATEADD(MONTH, number, '2012-01-01')) " +
			"FROM [master].dbo.spt_values WHERE [type] = N'P' ORDER BY number " +
			") Q2 " +
			"ON Q1.Mes = Q2.TheMonth";



                                cnn1.Open();

                                SqlDataReader dr1 = cmm1.ExecuteReader();
                                ChartGrupoSitio.Series.Add("GrupoSitioAno" + ano.ToString());
                                ChartGrupoSitio.Series["GrupoSitioAno" + ano.ToString()].ChartType = SeriesChartType.Line;

                                Legend secondLegend = new Legend();

                                this.ChartGrupoSitio.Legends.Add(secondLegend);

                                ChartGrupoSitio.Series["GrupoSitioAno" + ano.ToString()].LegendText = ano.ToString();
                                while (dr1.Read())
                                {

                                    ChartGrupoSitio.Series["GrupoSitioAno" + ano.ToString()].Points.AddXY(dr1.GetString(0), dr1.GetInt32(1));

                                }
                                if (ano == 2018)
                                    ChartGrupoSitio.Series["GrupoSitioAno" + ano.ToString()].Color = Color.Red;
                                else if (ano == 2017)
                                    ChartGrupoSitio.Series["GrupoSitioAno" + ano.ToString()].Color = Color.Green;
                                else if (ano == 2016)
                                    ChartGrupoSitio.Series["GrupoSitioAno" + ano.ToString()].Color = Color.Orange;
                                else if (ano == 2015)
                                    ChartGrupoSitio.Series["GrupoSitioAno" + ano.ToString()].Color = Color.Blue;
                                else
                                    ChartGrupoSitio.Series["GrupoSitioAno" + ano.ToString()].Color = Color.Indigo;


                                ChartGrupoSitio.Visible = true;





                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("{0} Exception caught.", ex);
                        }



                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }
        }
    


        else
        {

            try
            {




                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
                {

                    SqlCommand cmm = cnn.CreateCommand();
                    cmm.CommandText = "SELECT (CASE WHEN Q2.TheMonth =1 THEN 'Jan'  WHEN Q2.TheMonth = 2 THEN 'Fev'  WHEN Q2.TheMonth  = 3 THEN 'Mar' WHEN Q2.TheMonth =4 THEN 'Abr' WHEN Q2.TheMonth = 5 THEN 'Mai' WHEN Q2.TheMonth  = 6 THEN 'Jun' " +
    "WHEN Q2.TheMonth =7 THEN 'Jul' WHEN Q2.TheMonth = 8 THEN 'Ago'  WHEN Q2.TheMonth  = 9 THEN 'Set' WHEN Q2.TheMonth =10 THEN 'Out' WHEN Q2.TheMonth = 11 THEN 'Nov' WHEN Q2.TheMonth  = 12 THEN 'Dez' " +
    "ELSE NULL END) as Mes ,  ISNULL(Q1.Total, 0 ) FROM" +
    "(" +

	"SELECT mes , count(*) as Total FROM (SELECT  e.rh , mic.descricao as mic, tm.descricao as tm,gm.descricao as gm, MONTH(e.dt_resultado) as mes FROM [Isolamento].[dbo].[Exame] as e " +
	"inner join [Isolamento].[dbo].[tipos_microorganismos] as mic " +
	"on mic.cod_microorg = e.microorganismo " +
	"inner join [Isolamento].[dbo].tipos_materiais as tm " +
	"on tm.cod_material = e.material " +
	"inner join [Isolamento].[dbo].Material_Grupo_Materiais as mg " +
	"on mg.cod_material= tm.cod_material " +
	"inner join [Isolamento].[dbo].Grupo_Materiais as gm " +
	"on gm.cod_grupo_materiais = mg.cod_grupo_materiais where gm.descricao  = '" + grupoSitio + "'  and Year(e.dt_resultado) = " + anoPesquis + ")  p " +
	"group by p.mes " +
	") Q1 " +
	"RIGHT JOIN " +
	"( " +
	"SELECT TOP (DATEDIFF(MONTH, '2012-01-01', '2013-01-31')) " +
	"TheMonth = MONTH(DATEADD(MONTH, number, '2012-01-01')) " +
	"FROM [master].dbo.spt_values WHERE [type] = N'P' ORDER BY number " +
	") Q2 " +
	"ON Q1.Mes = Q2.TheMonth";



                    cnn.Open();
                    SqlDataReader dr = cmm.ExecuteReader();
                    ChartGrupoSitio.Series.Add("GrupoSitioAno");
                    ChartGrupoSitio.Series["GrupoSitioAno"].ChartType = SeriesChartType.Line;
                    Legend secondLegend = new Legend();

                    this.ChartGrupoSitio.Legends.Add(secondLegend);
                    ChartGrupoSitio.Series["GrupoSitioAno"].LegendText = anoPesquis;
                   
                    while (dr.Read())
                    {
                        ChartGrupoSitio.Series["GrupoSitioAno"].Points.AddXY(dr.GetString(0), dr.GetInt32(1));

                    }
                    ChartGrupoSitio.Series["GrupoSitioAno"].Color = Color.Red;
                    ChartGrupoSitio.Visible = true;


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }
        }
    }
}
