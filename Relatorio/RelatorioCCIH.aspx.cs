using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Odbc;


public partial class CCIH_RelatorioCCIH : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }

    }






    protected void btnExportar_Click(object sender, EventArgs e)
    {
        string dtHoje = DateTime.Now.Date.Day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Date.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Date.Year.ToString().PadLeft(2, '0');

        DataTable dt = new DataTable();
        DateTime Data = DateTime.Now;

        string nome = "";
        string rh = "";
        string clinica = "";
        string microorganismo = "";
        string material = "";
        string dt_resultado = "";
        string status = "";
        string admissão = "Paciente não Internado";
        string dataDaUltimaSaida = "";
        string reinternacao = "";
        string andarCompleto = "";
        string andar = "";
        string quarto = "";
        bool isInternado = false;
        string leito = "";



        string anoMesInicial = ddlAnoInicial.SelectedItem.Text + ddlMesInicial.SelectedValue.ToString().PadLeft(2, '0');

        string anoMesFinal = ddlAnoFinal.SelectedItem.Text + ddlMesFinal.SelectedValue.ToString().PadLeft(2, '0');

        //lbDataHora.Text = Data.ToString("D") + " " + Data.ToString("T");
        dt.Columns.Add("DATA RESULTADO", System.Type.GetType("System.String"));
        dt.Columns.Add("NOME DO PACIENTE", System.Type.GetType("System.String"));
        dt.Columns.Add("REG.HOSP", System.Type.GetType("System.String"));
        dt.Columns.Add("ADM", System.Type.GetType("System.String"));
        dt.Columns.Add("SAIDA", System.Type.GetType("System.String"));
        dt.Columns.Add("CLINICA", System.Type.GetType("System.String"));
        dt.Columns.Add("ANDAR", System.Type.GetType("System.String"));
        dt.Columns.Add("QUARTO", System.Type.GetType("System.String"));
        dt.Columns.Add("LEITO", System.Type.GetType("System.String"));
        dt.Columns.Add("MICROORGANISMO", System.Type.GetType("System.String"));
        dt.Columns.Add("MATERIAL", System.Type.GetType("System.String"));

        dt.Columns.Add("STATUS", System.Type.GetType("System.String"));
        dt.Columns.Add("REINTERNAÇÃO", System.Type.GetType("System.String"));


        try
        {



            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
            {

                SqlCommand cmm = cnn.CreateCommand();
                cmm.CommandText = "SELECT   p.nome, e.rh ,e.clinica, mi.descricao as microrganismo ,ma.descricao as material,e.dt_resultado " +
                    "FROM [Isolamento].[dbo].[Exame]  as e inner join [Isolamento].[dbo].[Paciente] as p " +
                    "on e.rh = p.rh " +
                    "inner join [Isolamento].[dbo].[tipos_materiais] as ma " +
                    "on e.material = ma.cod_material " +
                    "inner join [Isolamento].[dbo].[tipos_microorganismos] as mi " +
                    "on e.microorganismo = mi.cod_microorg " +
                 " WHERE (YEAR(dt_resultado) * 100 + MONTH(dt_resultado)) >= " + anoMesInicial +
                 " AND (YEAR(dt_resultado) * 100 + MONTH(dt_resultado)) <= " + anoMesFinal;
                cnn.Open();
                SqlDataReader dr = cmm.ExecuteReader();

                while (dr.Read())
                {
                    nome = dr.GetString(0);
                    rh = dr.GetInt32(1).ToString();
                    clinica = dr.GetString(2);
                    microorganismo = dr.GetString(3);
                    material = dr.GetString(4);
                    dt_resultado = dr.GetDateTime(5).ToShortDateString();







                    try
                    {



                        using (OdbcConnection cnn2 = new OdbcConnection(ConfigurationManager.ConnectionStrings["HospubConn"].ToString()))
                        {
                            OdbcCommand cmm2 = cnn2.CreateCommand();
                            cmm2.CommandText = "select c02codleito,d02inter,c14nomec from cen02,cen14 where c14codclin = c02codclin  and i02pront =" + rh;
                            cnn2.Open();
                            OdbcDataReader dr2 = cmm2.ExecuteReader();
                            if (dr2.Read())
                            {
                                isInternado = true;
                                andarCompleto = dr2.GetString(0);

                                andar = andarCompleto.Substring(0, 2);
                                quarto = andarCompleto.Substring(3, 2);
                                leito = andarCompleto.Substring(5, 2);
                                admissão = dr2.GetDecimal(1).ToString();
                                admissão = dataFormatada(admissão);
                                status = "Internado";
                                if (andar == "99")
                                {
                                    clinica = "Leito Extra";
                                    andar = "";
                                    quarto = "";
                                }
                                else
                                    clinica = dr2.GetString(2);
                            }



                            using (OdbcConnection cnn3 = new OdbcConnection(ConfigurationManager.ConnectionStrings["HospubConn"].ToString()))
                            {
                                OdbcCommand cmm3 = cnn3.CreateCommand();
                                cmm3.CommandText = "select d15inter, d15apres,d15compos1,c14nomec,c15motivo,c15codleito from cen15,cen14 where c14codclin = c15codclin and i15pront = " + rh + " order by d15inter desc ";
                                cnn3.Open();
                                OdbcDataReader dr3 = cmm3.ExecuteReader();
                                if (dr3.Read())
                                {
                                    if (isInternado)
                                        reinternacao = "Reinternado";
                                    else
                                    {

                                        clinica = dr3.GetString(3);

                                        admissão = dr3.GetDecimal(0).ToString();
                                        admissão = dataFormatada(admissão);
                                        dataDaUltimaSaida = dr3.GetDecimal(1).ToString() + dr3.GetDecimal(2).ToString().PadLeft(2, '0');
                                        dataDaUltimaSaida = dataFormatada(dataDaUltimaSaida);
                                        status = dr3.GetString(4);
                                        if (status == "1")
                                            status = "Alta";
                                        else if (status == "2")
                                            status = "Remoção";
                                        else if (status == "3")
                                            status = "Óbito até 24 horas";
                                        else
                                            status = "Óbito após 24 horas";
                                        andarCompleto = dr3.GetString(5);
                                        andar = andarCompleto.Substring(0, 2);
                                        quarto = andarCompleto.Substring(3, 2);
                                        leito = andarCompleto.Substring(5, 2);


                                    }




                                }
                            }

                        }
                        using (OdbcConnection cnn1 = new OdbcConnection(ConfigurationManager.ConnectionStrings["HospubConn"].ToString()))
                        {

                            OdbcCommand cmm1 = cnn1.CreateCommand();
                            cmm1.CommandText = "select * from intb6 where ((ib6compos like '%OBITO%') or (ib6dtobito != '' and ib6dtobito != '00000000')) and ib6regist =" + rh;
                            cnn1.Open();
                            OdbcDataReader dr1 = cmm1.ExecuteReader();

                            if (dr1.Read())
                            {
                                status = "Obito";

                            }
                        }

                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine("{0} Exception caught.", ex);
                    }








                    dt.Rows.Add(new String[] { dt_resultado, nome, rh, admissão, dataDaUltimaSaida, clinica, andar, quarto, leito, microorganismo, material, status, reinternacao });
                    nome = "";
                    rh = "";
                    clinica = "";
                    microorganismo = "";
                    material = "";
                    dt_resultado = "";
                    status = "";
                    admissão = "Paciente não Internado";
                    dataDaUltimaSaida = "";
                    reinternacao = "";
                    andarCompleto = "";
                    andar = "";
                    quarto = "";
                    isInternado = false;
                    leito = "";

                }//while

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("{0} Exception caught.", ex);
        }
        GridInternado.DataSource = dt; // apresentação dos dados da lista
        GridInternado.DataBind();

        DateTime dtarq = DateTime.Now;

        string dia = Convert.ToString(Convert.ToInt32(dtarq.Day));//dia atual + 1 = dia seguinte
        if (dia.Length == 1)
            dia = dia.PadLeft(2, '0');

        string mes = Convert.ToString(dtarq.Month);
        if (mes.Length == 1)
            mes = mes.PadLeft(2, '0');

        string data = Convert.ToString(dia) + Convert.ToString(mes) + Convert.ToString(dtarq.Year);

        HttpResponse oResponse = System.Web.HttpContext.Current.Response;
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Isolamento" + data + ".xls");
        HttpContext.Current.Response.Charset = "UTF-8";

        GridInternado.RenderControl(hw);

        oResponse.Write(tw.ToString());
        oResponse.End();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    private string dataFormatada(string data)
    {
        return data.Substring(6, 2) + "/" + data.Substring(4, 2) + "/" + data.Substring(0, 4);

    }
    private string horaFormatada(string hora)
    {

        int horaformatada = int.Parse(hora) / 60;
        int minuto = int.Parse(hora) % 60;
        return horaformatada.ToString().PadLeft(2, '0') + ':' + minuto.ToString().PadLeft(2, '0');

    }





}
