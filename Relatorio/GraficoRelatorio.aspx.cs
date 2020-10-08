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

public partial class Relatorio_GraficoRelatorio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnCarregar_Click(object sender, EventArgs e)
    {
        DataTable dt1 = new DataTable(); DataTable dt2 = new DataTable(); DataTable dt3 = new DataTable();
        string anoMesInicial = ddlAnoInicial.SelectedItem.Text + ddlMesInicial.SelectedValue.ToString().PadLeft(2, '0');

        string anoMesFinal = ddlAnoFinal.SelectedItem.Text + ddlMesFinal.SelectedValue.ToString().PadLeft(2, '0');


        try
        {



            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
            {

                SqlCommand cmm = cnn.CreateCommand();
                cmm.CommandText = "SELECT obito,count(*) as Total " +
                "FROM( select distinct  e.rh ,obito from [Isolamento].[dbo].[Paciente]as p inner join [Isolamento].[dbo].[Exame] as e on e.rh = p.rh "+
                "where (YEAR(e.dt_resultado) * 100 + MONTH(e.dt_resultado)) >= " + anoMesInicial +
                " AND (YEAR(e.dt_resultado) * 100 + MONTH(e.dt_resultado)) <= " + anoMesFinal + ") as p GROUP BY p.obito";

              cnn.Open();
                SqlDataReader dr = cmm.ExecuteReader();
             
                while (dr.Read())
                {
                    string situacao = dr.GetBoolean(0).ToString();
                    if (situacao.Equals("False"))
                        situacao = "Vivo";
                    else
                        situacao = "Morto";
                    ChartTaxaDeObito.Series["Series1"]["PieLabelStyle"] = "Outside";
                    ChartTaxaDeObito.Series["Series1"]["PieLineColor"] = "Black";

                    ChartTaxaDeObito.Series["Series1"].IsValueShownAsLabel = true;
                    ChartTaxaDeObito.Series["Series1"].Points.AddXY(situacao, dr.GetInt32(1));
                  
                    


                }
                ChartTaxaDeObito.Visible = true;

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("{0} Exception caught.", ex);
        }

        try
        {



            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
            {

                SqlCommand cmm = cnn.CreateCommand();
                cmm.CommandText = "SELECT sexo,count(*) as Total " +
"FROM( select distinct  e.rh ,sexo from [Isolamento].[dbo].[Paciente]as p inner join [Isolamento].[dbo].[Exame] as e on e.rh = p.rh " +
"where (YEAR(e.dt_resultado) * 100 + MONTH(e.dt_resultado)) >= " + anoMesInicial +
          " AND (YEAR(e.dt_resultado) * 100 + MONTH(e.dt_resultado)) <= " + anoMesFinal + ") as p GROUP BY p.sexo";

                cnn.Open();
                SqlDataReader dr = cmm.ExecuteReader();

                while (dr.Read())
                {
                    string situacao = dr.GetString(0);
                    if (situacao.Equals("1"))
                        situacao = "Masculino";
                    else
                        situacao = "Feminino";
                    ChartSexo.Series["Series1"]["PieLabelStyle"] = "Outside";
                    ChartSexo.Series["Series1"]["PieLineColor"] = "Black";

                    ChartSexo.Series["Series1"].IsValueShownAsLabel = true;
                    ChartSexo.Series["Series1"].Points.AddXY(situacao, dr.GetInt32(1));




                }
                ChartSexo.Visible = true;

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("{0} Exception caught.", ex);
        }
        try
        {



            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
            {

                SqlCommand cmm = cnn.CreateCommand();
                cmm.CommandText = "SELECT  SUM(CASE WHEN idade < 5 THEN 1 ELSE 0 END) AS [0-4]," +
        "SUM(CASE WHEN idade BETWEEN 5 AND 9 THEN 1 ELSE 0 END) AS [5-9], " +
        "SUM(CASE WHEN idade BETWEEN 10 AND 19 THEN 1 ELSE 0 END) AS [10-19], " +
        "SUM(CASE WHEN idade BETWEEN 20 AND 29 THEN 1 ELSE 0 END) AS [20-29], " +
        "SUM(CASE WHEN idade BETWEEN 30 AND 39 THEN 1 ELSE 0 END) AS [30-39], " +
        "SUM(CASE WHEN idade BETWEEN 40 AND 49 THEN 1 ELSE 0 END) AS [40-49], " +
        "SUM(CASE WHEN idade BETWEEN 50 AND 59 THEN 1 ELSE 0 END) AS [50-59], " +
        "SUM(CASE WHEN idade BETWEEN 60 AND 69 THEN 1 ELSE 0 END) AS [60-69], " +
        "SUM(CASE WHEN idade BETWEEN 70 AND 79 THEN 1 ELSE 0 END) AS [70-79], " +
        "SUM(CASE WHEN idade >79 THEN 1 ELSE 0 END) AS [80- +]  FROM (SELECT distinct e.[rh],[nome],convert ( int ,DATEDIFF(d, dt_nasc, getdate())/365.25) as idade,[sexo],[obito] " +
      "FROM [Isolamento].[dbo].[Paciente]as p inner join [Isolamento].[dbo].[Exame] as e on e.rh = p.rh  " +
 "inner join [Isolamento].[dbo].[tipos_microorganismos] as m on e.microorganismo = m.cod_microorg " +
                "where obito = 0 AND (YEAR(e.dt_resultado) * 100 + MONTH(e.dt_resultado)) >=  " + anoMesInicial +
          "AND (YEAR(e.dt_resultado) * 100 + MONTH(e.dt_resultado)) <= " + anoMesFinal+" ) as p" ;



                cnn.Open();
                SqlDataReader dr = cmm.ExecuteReader();

                if (dr.Read())
                {





                    ChartIdadeObito.Series["Vivo"].Points.AddXY("0-4", dr.GetInt32(0));
                    ChartIdadeObito.Series["Vivo"].Points.AddXY("5-9", dr.GetInt32(1));
                    ChartIdadeObito.Series["Vivo"].Points.AddXY("10-19", dr.GetInt32(2));
                    ChartIdadeObito.Series["Vivo"].Points.AddXY("20-29", dr.GetInt32(3));
                    ChartIdadeObito.Series["Vivo"].Points.AddXY("30-39", dr.GetInt32(4));
                    ChartIdadeObito.Series["Vivo"].Points.AddXY("40-49", dr.GetInt32(5));
                    ChartIdadeObito.Series["Vivo"].Points.AddXY("50-59", dr.GetInt32(6));
                    ChartIdadeObito.Series["Vivo"].Points.AddXY("60-69", dr.GetInt32(7));
                    ChartIdadeObito.Series["Vivo"].Points.AddXY("70-79", dr.GetInt32(8));
                    ChartIdadeObito.Series["Vivo"].Points.AddXY("80- +", dr.GetInt32(9));





                }
               

            }

            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
            {

                SqlCommand cmm = cnn.CreateCommand();
                cmm.CommandText = "SELECT  SUM(CASE WHEN idade < 5 THEN 1 ELSE 0 END) AS [0-4]," +
        "SUM(CASE WHEN idade BETWEEN 5 AND 9 THEN 1 ELSE 0 END) AS [5-9], " +
        "SUM(CASE WHEN idade BETWEEN 10 AND 19 THEN 1 ELSE 0 END) AS [10-19], " +
        "SUM(CASE WHEN idade BETWEEN 20 AND 29 THEN 1 ELSE 0 END) AS [20-29], " +
        "SUM(CASE WHEN idade BETWEEN 30 AND 39 THEN 1 ELSE 0 END) AS [30-39], " +
        "SUM(CASE WHEN idade BETWEEN 40 AND 49 THEN 1 ELSE 0 END) AS [40-49], " +
        "SUM(CASE WHEN idade BETWEEN 50 AND 59 THEN 1 ELSE 0 END) AS [50-59], " +
        "SUM(CASE WHEN idade BETWEEN 60 AND 69 THEN 1 ELSE 0 END) AS [60-69], " +
        "SUM(CASE WHEN idade BETWEEN 70 AND 79 THEN 1 ELSE 0 END) AS [70-79], " +
        "SUM(CASE WHEN idade >79 THEN 1 ELSE 0 END) AS [80- +]  FROM (SELECT distinct e.[rh],[nome],convert ( int ,DATEDIFF(d, dt_nasc, getdate())/365.25) as idade,[sexo],[obito] " +
      "FROM [Isolamento].[dbo].[Paciente]as p inner join [Isolamento].[dbo].[Exame] as e on e.rh = p.rh  " +
 "inner join [Isolamento].[dbo].[tipos_microorganismos] as m on e.microorganismo = m.cod_microorg " +
                "where obito = 1 AND (YEAR(e.dt_resultado) * 100 + MONTH(e.dt_resultado)) >=  " + anoMesInicial +
          "AND (YEAR(e.dt_resultado) * 100 + MONTH(e.dt_resultado)) <= " + anoMesFinal + " ) as p";



                cnn.Open();
                SqlDataReader dr = cmm.ExecuteReader();

                if (dr.Read())
                {





                    ChartIdadeObito.Series["Morto"].Points.AddXY("0-4", dr.GetInt32(0));
                    ChartIdadeObito.Series["Morto"].Points.AddXY("5-9", dr.GetInt32(1));
                    ChartIdadeObito.Series["Morto"].Points.AddXY("10-19", dr.GetInt32(2));
                    ChartIdadeObito.Series["Morto"].Points.AddXY("20-29", dr.GetInt32(3));
                    ChartIdadeObito.Series["Morto"].Points.AddXY("30-39", dr.GetInt32(4));
                    ChartIdadeObito.Series["Morto"].Points.AddXY("40-49", dr.GetInt32(5));
                    ChartIdadeObito.Series["Morto"].Points.AddXY("50-59", dr.GetInt32(6));
                    ChartIdadeObito.Series["Morto"].Points.AddXY("60-69", dr.GetInt32(7));
                    ChartIdadeObito.Series["Morto"].Points.AddXY("70-79", dr.GetInt32(8));
                    ChartIdadeObito.Series["Morto"].Points.AddXY("80- +", dr.GetInt32(9));





                }
                

            }
            ChartIdadeObito.Visible = true;
            ChartIdadeObito.Series["Morto"].IsValueShownAsLabel = true;
            ChartIdadeObito.Series["Vivo"].IsValueShownAsLabel = true;

        }
        catch (Exception ex)
        {
            Console.WriteLine("{0} Exception caught.", ex);
        }
        try
        {



            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
            {

                SqlCommand cmm = cnn.CreateCommand();
                cmm.CommandText = "SELECT  SUM(CASE WHEN idade < 5 THEN 1 ELSE 0 END) AS [0-4]," +
        "SUM(CASE WHEN idade BETWEEN 5 AND 9 THEN 1 ELSE 0 END) AS [5-9], " +
        "SUM(CASE WHEN idade BETWEEN 10 AND 19 THEN 1 ELSE 0 END) AS [10-19], " +
        "SUM(CASE WHEN idade BETWEEN 20 AND 29 THEN 1 ELSE 0 END) AS [20-29], " +
        "SUM(CASE WHEN idade BETWEEN 30 AND 39 THEN 1 ELSE 0 END) AS [30-39], " +
        "SUM(CASE WHEN idade BETWEEN 40 AND 49 THEN 1 ELSE 0 END) AS [40-49], " +
        "SUM(CASE WHEN idade BETWEEN 50 AND 59 THEN 1 ELSE 0 END) AS [50-59], " +
        "SUM(CASE WHEN idade BETWEEN 60 AND 69 THEN 1 ELSE 0 END) AS [60-69], " +
        "SUM(CASE WHEN idade BETWEEN 70 AND 79 THEN 1 ELSE 0 END) AS [70-79], " +
        "SUM(CASE WHEN idade >79 THEN 1 ELSE 0 END) AS [80- +]  FROM (SELECT distinct e.[rh],[nome],convert ( int ,DATEDIFF(d, dt_nasc, getdate())/365.25) as idade,[sexo],[obito] " +
      "FROM [Isolamento].[dbo].[Paciente]as p inner join [Isolamento].[dbo].[Exame] as e on e.rh = p.rh  " +
 "inner join [Isolamento].[dbo].[tipos_microorganismos] as m on e.microorganismo = m.cod_microorg " +
                "where obito = 0 AND (YEAR(e.dt_resultado) * 100 + MONTH(e.dt_resultado)) >=  " + anoMesInicial +
          "AND (YEAR(e.dt_resultado) * 100 + MONTH(e.dt_resultado)) <= " + anoMesFinal+
  "Union " +
  "SELECT distinct e.[rh],[nome],DATEDIFF(yy, dt_nasc, dtobito) as idade,[sexo],[obito] " +
  "FROM [Isolamento].[dbo].[Paciente] as p inner join [Isolamento].[dbo].[Exame] as e on e.rh = p.rh " +
 "inner join [Isolamento].[dbo].[tipos_microorganismos] as m on e.microorganismo = m.cod_microorg " +
    "where obito =1 AND (YEAR(e.dt_resultado) * 100 + MONTH(e.dt_resultado)) >= " + anoMesInicial  + 
           "AND (YEAR(e.dt_resultado) * 100 + MONTH(e.dt_resultado)) <= " + anoMesFinal +
           " ) as p ";
  


                cnn.Open();
                SqlDataReader dr = cmm.ExecuteReader();

                if (dr.Read())
                {
                  
                
             

                    
                    ChartIdade.Series["Idade"].Points.AddXY("0-4", dr.GetInt32(0));
                    ChartIdade.Series["Idade"].Points.AddXY("5-9", dr.GetInt32(1));
                    ChartIdade.Series["Idade"].Points.AddXY("10-19", dr.GetInt32(2));
                    ChartIdade.Series["Idade"].Points.AddXY("20-29", dr.GetInt32(3));
                    ChartIdade.Series["Idade"].Points.AddXY("30-39", dr.GetInt32(4));
                    ChartIdade.Series["Idade"].Points.AddXY("40-49", dr.GetInt32(5));
                    ChartIdade.Series["Idade"].Points.AddXY("50-59", dr.GetInt32(6));
                    ChartIdade.Series["Idade"].Points.AddXY("60-69", dr.GetInt32(7));
                    ChartIdade.Series["Idade"].Points.AddXY("70-79", dr.GetInt32(8));
                    ChartIdade.Series["Idade"].Points.AddXY("80- +", dr.GetInt32(9));
                   




                }
                ChartIdade.Visible = true;
               

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("{0} Exception caught.", ex);
        }

      try
        {



            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
            {

                SqlCommand cmm = cnn.CreateCommand();
                cmm.CommandText = "SELECT     grupo,  ISNULL([0], 0) AS VIVO, ISNULL([1], 0) AS MORTO,(ISNULL([0], 0) + ISNULL([1], 0)) AS TOTAL FROM(" +
"SELECT grupo,obito,COUNT (*) as Total FROM( SELECT distinct e.[rh],[nome],[sexo],[obito] as obito,m.descricao as microorganismo " +
 " ,g.descricao as grupo FROM [Isolamento].[dbo].[Paciente] as p inner join [Isolamento].[dbo].[Exame] as e on e.rh = p.rh " +
" inner join [Isolamento].[dbo].[tipos_microorganismos] as m on e.microorganismo = m.cod_microorg " +
 " inner join [Isolamento].[dbo].[Material_Grupo_Materiais] as mg on e.material = mg.cod_material " +
 " inner join [Isolamento].[dbo].[Grupo_Materiais] as g on mg.cod_grupo_materiais = g.cod_grupo_materiais   WHERE (YEAR(e.dt_resultado) * 100 + MONTH(e.dt_resultado)) >= " + anoMesInicial +
          " AND (YEAR(e.dt_resultado) * 100 + MONTH(e.dt_resultado)) <= " + anoMesFinal + ") as p " +
 " group by p.grupo , p.obito) as pr PIVOT (SUM(pr.Total) FOR pr.obito IN ([0], [1]) )as pvt ORDER BY pvt.grupo";

                cnn.Open();

                SqlDataAdapter da1 = new SqlDataAdapter(cmm);

                da1.Fill(dt1);
                cnn.Close();
                ChartGrupoMateriais.DataSource = dt1;
                ChartGrupoMateriais.Series["Vivo"].XValueMember = "grupo";
                ChartGrupoMateriais.Series["Morto"].XValueMember = "grupo";
                ChartGrupoMateriais.Series["Vivo"].YValueMembers = "VIVO";
                ChartGrupoMateriais.Series["Morto"].YValueMembers = "MORTO";
                ChartGrupoMateriais.Visible = true;
                ChartGrupoMateriais.Series["Morto"].IsValueShownAsLabel = true;
                ChartGrupoMateriais.Series["Vivo"].IsValueShownAsLabel = true;


            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("{0} Exception caught.", ex);
        }

      try
      {



          using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
          {

              SqlCommand cmm = cnn.CreateCommand();
              cmm.CommandText = "SELECT     sexo,  ISNULL([0], 0) AS VIVO, ISNULL([1], 0) AS MORTO,(ISNULL([0], 0) + ISNULL([1], 0)) AS TOTAL FROM(" +
" SELECT sexo,obito,COUNT (*) as Total FROM( SELECT distinct e.[rh],[nome],[sexo],[obito] as obito " +
 " FROM [Isolamento].[dbo].[Paciente] as p inner join [Isolamento].[dbo].[Exame] as e on e.rh = p.rh" +
  " WHERE (YEAR(e.dt_resultado) * 100 + MONTH(e.dt_resultado)) >= "+ anoMesInicial +
           " AND (YEAR(e.dt_resultado) * 100 + MONTH(e.dt_resultado)) <= " + anoMesFinal + ") as p" +
 " group by p.sexo , p.obito) as pr PIVOT (SUM(pr.Total) FOR pr.obito IN ([0], [1]) )as pvt ORDER BY pvt.sexo";

              cnn.Open();

             

              SqlDataReader dr = cmm.ExecuteReader();

              while (dr.Read())
              {

                  string situacao = dr.GetString(0);
                  if (situacao.Equals("1"))
                      situacao = "Masculino";
                  else
                      situacao = "Feminino";



                  ChartSexoObito.Series["Vivo"].Points.AddXY(situacao, dr.GetInt32(1));
                  ChartSexoObito.Series["Morto"].Points.AddXY(situacao, dr.GetInt32(2));
              





              }
              ChartSexoObito.Series["Vivo"].IsValueShownAsLabel = true;
              ChartSexoObito.Series["Morto"].IsValueShownAsLabel = true;
              ChartSexoObito.Visible = true;

          }
      }
      catch (Exception ex)
      {
          Console.WriteLine("{0} Exception caught.", ex);
      }
       try
        {



            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
            {

                SqlCommand cmm = cnn.CreateCommand();
                cmm.CommandText = "SELECT     microorganismo,  ISNULL([0], 0) AS VIVO, ISNULL([1], 0) AS MORTO,(ISNULL([0], 0) + ISNULL([1], 0)) AS TOTAL FROM(" +
"SELECT microorganismo,obito,COUNT (*) as Total FROM( SELECT distinct e.[rh],[nome],[sexo],[obito] as obito,m.descricao as microorganismo " +
 "  FROM [Isolamento].[dbo].[Paciente] as p inner join [Isolamento].[dbo].[Exame] as e on e.rh = p.rh " +
" inner join [Isolamento].[dbo].[tipos_microorganismos] as m on e.microorganismo = m.cod_microorg " + 
 "   WHERE (YEAR(e.dt_resultado) * 100 + MONTH(e.dt_resultado)) >= " + anoMesInicial +
          " AND (YEAR(e.dt_resultado) * 100 + MONTH(e.dt_resultado)) <= " + anoMesFinal + ") as p " +
 " group by p.microorganismo , p.obito) as pr PIVOT (SUM(pr.Total) FOR pr.obito IN ([0], [1]) )as pvt ORDER BY pvt.microorganismo";

                cnn.Open();

                SqlDataAdapter da1 = new SqlDataAdapter(cmm);

                da1.Fill(dt2);
                cnn.Close();
                ChartMicrorganismo.DataSource = dt2;
                ChartMicrorganismo.Series["Vivo"].XValueMember = "microorganismo";
                ChartMicrorganismo.Series["Morto"].XValueMember = "microorganismo";
                ChartMicrorganismo.Series["Vivo"].YValueMembers = "VIVO";
                ChartMicrorganismo.Series["Morto"].YValueMembers = "MORTO";
                ChartMicrorganismo.Visible = true;

                ChartMicrorganismo.ChartAreas[0].AxisX.Interval = 1;
                ChartMicrorganismo.Series["Morto"].IsValueShownAsLabel = true;
                ChartMicrorganismo.Series["Vivo"].IsValueShownAsLabel = true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("{0} Exception caught.", ex);
        }
    }

}
