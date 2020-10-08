using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Drawing;
using System.Collections;

public partial class CCIH : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();



        string nome = "";
        string rh = "";
        string clinica = "";
        string andar = "";
        ArrayList microorganismo = new ArrayList();
        string nomesMicroorganismos = "";
        string isolado = "CONTATO";
        string leito = "";
        bool flag = false;
        string dataExame = "";

        string andarCompleto = "";








        if (!IsPostBack)
        {
            DateTime Data = DateTime.Now;
            DataTable dt8 = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dtCloned = new DataTable();


            lbDataHora.Text = Data.ToString("D") + " " + Data.ToString("T");
            dt.Columns.Add("NOME", System.Type.GetType("System.String"));
            dt.Columns.Add("RH", System.Type.GetType("System.String"));
            dt.Columns.Add("CLINICA REAL", System.Type.GetType("System.String"));
            dt.Columns.Add("ANDAR", System.Type.GetType("System.String"));
            dt.Columns.Add("LEITO", System.Type.GetType("System.String"));
            dt.Columns.Add("MICRORGANISMO", System.Type.GetType("System.String"));
            dt.Columns.Add("PRECAUÇÃO", System.Type.GetType("System.String"));
            try
            {



                using (OdbcConnection cnn = new OdbcConnection(ConfigurationManager.ConnectionStrings["HospubConn"].ToString()))
                {
                    OdbcCommand cmm = cnn.CreateCommand();
                    cmm.CommandText = "select i02pront from cen02";
                    cnn.Open();

                    OdbcDataAdapter da = new OdbcDataAdapter(cmm);



                    da.Fill(dt8);
                    dtCloned = dt8.Clone();
                    dtCloned.Columns[0].DataType = typeof(Int32);
                    foreach (DataRow row in dt8.Rows)
                    {
                        dtCloned.ImportRow(row);
                    }
                    cnn.Close();
                }
                using (SqlConnection cnn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
                {
                    SqlCommand cmm1 = cnn1.CreateCommand();
                    cmm1.CommandText = "SELECT rh FROM [Isolamento].[dbo].[Paciente]";

                    cnn1.Open();
                    SqlDataAdapter da1 = new SqlDataAdapter(cmm1);

                    da1.Fill(dt1);
                    cnn1.Close();   

                }
                var rhPacienteIntenadoBacteria = from row1 in dtCloned.AsEnumerable()
                                                 join row2 in dt1.AsEnumerable()
                                                 on row1.Field<int>("i02pront") equals row2.Field<int>("rh")
                                                 select row1.ItemArray.Concat(row2.ItemArray.Where(r2 => row1.ItemArray.Contains(r2) == false)).ToArray();


                foreach (var item in rhPacienteIntenadoBacteria)
                {


                    rh = item[0].ToString();

                    using ( OdbcConnection cnn8 = new OdbcConnection(ConfigurationManager.ConnectionStrings["HospubConn"].ToString()))
                    {
                        OdbcCommand cmm8 = cnn8.CreateCommand();
                        cmm8.CommandText = "select c02codleito from cen02 where i02pront =" + rh;
                        cnn8.Open();

                        OdbcDataReader dr8 = cmm8.ExecuteReader();

                        if (dr8.Read())
                        {
                            andarCompleto = dr8.GetString(0);
                        }

                    }

                    andar = andarCompleto.Substring(0, 2);
                    leito = andarCompleto.Substring(5, 2);
                    if (andar == "99")
                    {

                        andar = "Leito Extra";
                        leito = "";
                    }
                    using (OdbcConnection cnn5 = new OdbcConnection(ConfigurationManager.ConnectionStrings["HospubConn"].ToString()))
                    {

                        OdbcCommand cm5 = cnn5.CreateCommand();
                        cm5.CommandText = "select c14nomec from cen02 ,cen14, intb6 where i02pront = ib6regist and c14codclin = c02clinreal and ib6regist =" + rh;
                        cnn5.Open();
                        OdbcDataReader dr5 = cm5.ExecuteReader();

                        if (!dr5.Read())
                        {
                            using (OdbcConnection cnn6 = new OdbcConnection(ConfigurationManager.ConnectionStrings["HospubConn"].ToString()))
                            {

                                OdbcCommand cm6 = cnn6.CreateCommand();
                                cm6.CommandText = "select c14nomec from cen02 ,cen14, intb6 where i02pront = ib6regist and c14codclin = c02codclin and ib6regist =" + rh;
                                cnn6.Open();
                                OdbcDataReader dr6 = cm6.ExecuteReader();

                                if (dr6.Read())
                                {
                                    clinica = dr6.GetString(0);
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Erro de consulta neste " + rh + "!);", true);
                                    dr5.Close();
                                    dr6.Close();
                                }
                            }


                        }
                        else
                        {
                            clinica = dr5.GetString(0);




                        }
                    }
                    using (OdbcConnection cnn4 = new OdbcConnection(ConfigurationManager.ConnectionStrings["HospubConn"].ToString()))
                    {

                        OdbcCommand cmm4 = cnn4.CreateCommand();
                        cmm4.CommandText = "select ib6pnome, ib6compos from intb6 where ib6regist = " + rh;
                        cnn4.Open();
                        OdbcDataReader dr4 = cmm4.ExecuteReader();

                        if (dr4.Read())
                        {
                            nome = dr4.GetString(0) + dr4.GetString(1);
                        }
                    }

                    using (SqlConnection cnn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
                    {
                        SqlCommand cmm1 = cnn1.CreateCommand();
                        cmm1.CommandText = "SELECT  convert(varchar, e.dt_resultado, 103) as 'Data' , mi.descricao as microrganismo " +
                            "FROM [Isolamento].[dbo].[Exame]  as e inner join [Isolamento].[dbo].[Paciente] as p " +
                            "on e.rh = p.rh " +
                            "inner join [Isolamento].[dbo].[tipos_microorganismos] as mi " +
                            "on e.microorganismo = mi.cod_microorg  where e.rh = " + rh;

                        cnn1.Open();
                        SqlDataReader dr1 = cmm1.ExecuteReader();
                        while (dr1.Read())
                        {
                            bool imprime = false;
                            DateTime dtSaidaAnterior = new DateTime(0001, 01, 01, 0, 0, 0);
                            DateTime dtSaida = new DateTime();
                            DateTime dtEntrada = new DateTime();


                            int count2 = 0;
                            dataExame = dr1.GetString(0);
                            DateTime dtAtual = DateTime.Now;
                            string nomeMicr = dr1.GetString(1);
                            //rh = dr1.GetInt32(1).ToString();

                            DateTime dtExame = Convert.ToDateTime(dataExame);
                            using (OdbcConnection cnn10 = new OdbcConnection(ConfigurationManager.ConnectionStrings["HospubConn"].ToString()))
                            {
                                        OdbcCommand cmm10 = cnn10.CreateCommand();
                                cmm10.CommandText = "select d15apres,d15compos1,d15inter from cen15 where i15pront = " + rh;
                                cnn10.Open();
                                OdbcDataReader dr10 = cmm10.ExecuteReader();

                                if (dr10.Read())
                                {
                                    string dtEntrada10 = Convert.ToString(dr10.GetDecimal(2));
                                    dtEntrada10 = dtEntrada10.Substring(6, 2) + "/" + dtEntrada10.Substring(4, 2) + "/" + dtEntrada10.Substring(0, 4);
                                    DateTime dtEntrada2 = Convert.ToDateTime(dtEntrada10);
                                    DateTime dtComparacao = dtExame.AddDays(180);
                                    if ( dtComparacao > dtEntrada2)
                                    {

                                        using (OdbcConnection cnn2 = new OdbcConnection(ConfigurationManager.ConnectionStrings["HospubConn"].ToString()))
                                        {
                                            OdbcCommand cmm2 = cnn2.CreateCommand();
                                            cmm2.CommandText = "select d15apres,d15compos1,d15inter from cen15 where i15pront = " + rh;
                                            cnn2.Open();
                                            OdbcDataReader dr2 = cmm2.ExecuteReader();

                                            while (dr2.Read())
                                            {
                                                string dtSaida1 = Convert.ToString(dr2.GetDecimal(0));
                                                string dtSaida2 = Convert.ToString(dr2.GetDecimal(1));

                                                dtSaida2 = dtSaida2.PadLeft(2, '0');
                                                string data = dtSaida2 + "/" + dtSaida1.Substring(4, 2) + "/" + dtSaida1.Substring(0, 4);
                                                dtSaida = Convert.ToDateTime(data);
                                                string dtEntrada1 = Convert.ToString(dr2.GetDecimal(2));
                                                dtEntrada1 = dtEntrada1.Substring(6, 2) + "/" + dtEntrada1.Substring(4, 2) + "/" + dtEntrada1.Substring(0, 4);
                                                dtEntrada = Convert.ToDateTime(dtEntrada1);

                                                dtSaida = dtSaida.AddDays(15);


                                                if (dtExame <= dtSaida)
                                                {
                                                    count2 = count2 + 1;
                                                    imprime = true;
                                                    dtSaida = dtSaida.AddDays(-15);
                                                    DateTime comparar = new DateTime(0001, 01, 01, 0, 0, 0);
                                                    if (dtSaidaAnterior.CompareTo(comparar) != 0)
                                                    {
                                                        if ((dtEntrada - dtSaidaAnterior).Days <= 180)
                                                        {


                                                            imprime = true;

                                                        }
                                                        else
                                                        {
                                                            imprime = false;
                                                            break;
                                                        }
                                                    }


                                                    dtSaidaAnterior = dtSaida;


                                                }






                                            }//while existir Data de Saida
                                        }
                                    }//using 2
                                }//if dr10

                                string dtInternacao = "";
                                using (OdbcConnection cnn3 = new OdbcConnection(ConfigurationManager.ConnectionStrings["HospubConn"].ToString()))
                                {
                                    DateTime dataInternacao = new DateTime(0001, 01, 01, 0, 0, 0);
                                    OdbcCommand cmm3 = cnn3.CreateCommand();
                                    cmm3.CommandText = "select d02inter from cen02 where i02pront = " + rh;
                                    cnn3.Open();
                                    OdbcDataReader dr3 = cmm3.ExecuteReader();

                                    if (dr3.Read())
                                    {
                                        dtInternacao = Convert.ToString(dr3.GetDecimal(0));
                                        dtInternacao = dtInternacao.Substring(6, 2) + "/" + dtInternacao.Substring(4, 2) + "/" + dtInternacao.Substring(0, 4);
                                        dataInternacao = Convert.ToDateTime(dtInternacao);
                                    }
                                    DateTime comparar = new DateTime(0001, 01, 01, 0, 0, 0);
                                    if (imprime)
                                    {


                                        if (((dtAtual - dtSaida).Days <= 180) || ((dataInternacao.CompareTo(comparar) != 0) && ((dataInternacao - dtSaida).Days <= 180)))
                                        {
                                            flag = true;

                                            if (microorganismo.Count == 0)
                                                microorganismo.Add(nomeMicr);
                                            else
                                            {
                                                int total = microorganismo.Count;
                                                bool contador = true;
                                                for (int i = 0; i < total; i++)
                                                {
                                                    if (microorganismo[i].ToString() == nomeMicr)
                                                        contador = false;
                                                }
                                                if (contador)
                                                    microorganismo.Add(dr1.GetString(1));

                                            }
                                        }

                                    }
                                    if (dataInternacao.CompareTo(comparar) != 0)
                                    {

                                        if (count2 == 0)
                                        {

                                            if (dataInternacao <= dtExame)
                                            {
                                                flag = true;
                                                count2 = count2 + 1;



                                                if (microorganismo.Count == 0)
                                                    microorganismo.Add(nomeMicr);
                                                else
                                                {
                                                    int total = microorganismo.Count;
                                                    bool contador = true;
                                                    for (int i = 0; i < total; i++)
                                                    {
                                                        if (microorganismo[i].ToString() == nomeMicr)
                                                            contador = false;
                                                    }
                                                    if (contador)
                                                        microorganismo.Add(dr1.GetString(1));

                                                }
                                            }

                                        }
                                    }
                                }//using dtInternacao
                                if (count2 == 0)
                                {
                                    if ((dtAtual - dtExame).Days <= 180)
                                    {
                                        flag = true;
                                        if (microorganismo.Count == 0)
                                            microorganismo.Add(nomeMicr);
                                        else
                                        {
                                            int total = microorganismo.Count;
                                            bool contador = true;
                                            for (int i = 0; i < total; i++)
                                            {
                                                if (microorganismo[i].ToString() == nomeMicr)
                                                    contador = false;
                                            }
                                            if (contador)
                                                microorganismo.Add(dr1.GetString(1));


                                        }//else
                                    }//if
                                }//if
                            }//using 
                        }//while
                    } //using
                    if (flag == true)
                    {
                        bool passou = false;
                        foreach (string element in microorganismo)
                        {
                            if (passou == false)
                            {
                                passou = true;
                                nomesMicroorganismos = element;
                            }
                            else
                                nomesMicroorganismos += "/ " + element;
                        }
                        passou = true;
                        dt.Rows.Add(new String[] { nome, rh, clinica, andar, leito, nomesMicroorganismos, isolado });
                        nome = "";
                        rh = "";
                        clinica = "";
                        microorganismo.Clear();
                        andar = "";
                        flag = false;
                    }



                }//while

            }//using




            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }
            const string SortByClause = "[nome] ASC";
            dt.DefaultView.Sort = SortByClause;
            GridInternado.DataSource = dt; // apresentação dos dados da lista
            GridInternado.DataBind();
        }
    }
    protected void btnExportar_Click(object sender, EventArgs e)
    {
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



}

