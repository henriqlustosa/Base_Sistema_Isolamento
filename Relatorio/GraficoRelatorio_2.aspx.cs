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

public partial class Relatorio_GraficoRelatorio_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnCarregar_Click(object sender, EventArgs e)
    {
        DataTable dt2 = new DataTable();
        string anoMesInicial = ddlAnoInicial2.SelectedItem.Text + ddlMesInicial2.SelectedValue.ToString().PadLeft(2, '0');
        string anoMesFinal = ddlAnoFinal2.SelectedItem.Text + ddlMesFinal2.SelectedValue.ToString().PadLeft(2, '0');
        string grupoSitio = ddlGrupos.SelectedItem.Text;
        try
        {



            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
            {

                SqlCommand cmm = cnn.CreateCommand();
                cmm.CommandText = "SELECT    microorganismo,  ISNULL([0], 0) AS VIVO, ISNULL([1], 0) AS MORTO,(ISNULL([0], 0) + ISNULL([1], 0)) AS TOTAL FROM " +
                                "( SELECT microorganismo,obito,COUNT (*) as Total FROM " +
                                "(SELECT distinct e.[rh],[nome],[sexo],[obito] as obito,m.descricao as microorganismo " +
                                "FROM [Isolamento].[dbo].[Paciente] as p inner join [Isolamento].[dbo].[Exame] as e " +
                                "on e.rh = p.rh  " +
                                "inner join [Isolamento].[dbo].[tipos_microorganismos] as m " +
                                "on e.microorganismo = m.cod_microorg " +
                                "inner join [Isolamento].[dbo].[tipos_materiais] as mat " +
                                "on e.material = mat.cod_material " +
                                "inner join [Isolamento].[dbo].[Material_Grupo_Materiais] as matgrupo " +
                                "on matgrupo.cod_material = mat.cod_material " +
                                "inner join [Isolamento].[dbo].[Grupo_Materiais] as grupo " +
                                "on matgrupo.cod_grupo_materiais = grupo.cod_grupo_materiais " +
                                "WHERE ((YEAR(e.dt_resultado) * 100 + MONTH(e.dt_resultado)) >= " +anoMesInicial +
                                "AND (YEAR(e.dt_resultado) * 100 + MONTH(e.dt_resultado)) <=" + anoMesFinal + ")and grupo.descricao = '" + grupoSitio + "' ) " +
                                "as p group by p.microorganismo , p.obito) as pr PIVOT (SUM(pr.Total) FOR pr.obito IN ([0], [1]) )as pvt ORDER BY pvt.microorganismo";

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
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("{0} Exception caught.", ex);
        }
    }

    protected void btnCarregar2_Click(object sender, EventArgs e)
    {
        DataTable dt1 = new DataTable();
        string anoMesInicial = ddlAnoInicial.SelectedItem.Text + ddlMesInicial.SelectedValue.ToString().PadLeft(2, '0');
        string anoMesFinal = ddlAnoFinal.SelectedItem.Text + ddlMesFinal.SelectedValue.ToString().PadLeft(2, '0');
        string microrganismos = ddlmicrorganismos.SelectedItem.Text;
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
          " AND ((YEAR(e.dt_resultado) * 100 + MONTH(e.dt_resultado)) <= " + anoMesFinal + ") and m.descricao = '" + microrganismos +"') as p " +
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


            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("{0} Exception caught.", ex);
        }
    }
    
}
