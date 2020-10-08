using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Odbc;
using System.Data;
using System.Globalization;
using System.Web.Services;

public partial class Pesquisa_Pesquisa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
   
    public static string[] GetClientes(string prefixo)
    {
        List<string> clientes = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "SELECT [nome],[rh] FROM [Isolamento].[dbo].[Paciente]  where obito ='0' and nome like @Texto +'%'";
                cmd.Parameters.AddWithValue("@Texto", prefixo);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        clientes.Add(string.Format("{0}-{1}", sdr["nome"], sdr["rh"]));
                    }
                }
                conn.Close();
            }
        }
        return clientes.ToArray();
    }



    

  
    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        string rh = txtRH.Text;
        hfCustomerId.Value = "";
        txtProcurar.Text = "";
        Pesquisar(rh);
        
        

    }

    public void Pesquisar(string rh)
    {
        string dataExame = "";


        string nome = "";
        string microorganismo = "";
        string sitio = "";
        string nomePaciente = "";

        bool exameExpirado = false;






        DataTable dt = new DataTable();

        dt.Columns.Add("Rh", System.Type.GetType("System.String"));
        dt.Columns.Add("Nome", System.Type.GetType("System.String"));
        dt.Columns.Add("Microorganismo", System.Type.GetType("System.String"));
        dt.Columns.Add("Sitio", System.Type.GetType("System.String"));
        dt.Columns.Add("Data", System.Type.GetType("System.String"));

        try
        {
            if (!TesteObito(rh))
            {
                using (OdbcConnection cnn4 = new OdbcConnection(ConfigurationManager.ConnectionStrings["HospubConn"].ToString()))
                {

                    OdbcCommand cmm4 = cnn4.CreateCommand();
                    cmm4.CommandText = "select ib6pnome, ib6compos from intb6 where ib6regist = " + rh;
                    cnn4.Open();
                    OdbcDataReader dr4 = cmm4.ExecuteReader();

                    if (dr4.Read())
                    {
                        nomePaciente = dr4.GetString(0) + dr4.GetString(1);
                    }
                }
                int count = 0;
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
                {
                    SqlCommand cmm = cnn.CreateCommand();
                    cmm.CommandText = "SELECT p.rh as RH ,p.nome  as Nome ,m.descricao as Microorganismo ,ma.descricao as Sitio , convert(varchar, e.dt_resultado, 103) as 'Data' FROM [Isolamento].[dbo].[Exame] as e "
                    + " INNER JOIN [Isolamento].[dbo].[Paciente] as p ON e.rh = p.rh "
                    + " INNER JOIN [Isolamento].[dbo].[tipos_microorganismos] as  m ON e.microorganismo = m.cod_microorg "
                    + " INNER JOIN [Isolamento].[dbo].[tipos_materiais] as  ma ON e.material = ma.cod_material where p.rh = " + rh + "  order by dt_resultado ";
                    cnn.Open();
                    SqlDataReader dr = cmm.ExecuteReader();

                    while (dr.Read())
                    {
                        DateTime dtSaidaAnterior = new DateTime(0001, 01, 01, 0, 0, 0);
                        DateTime dtSaida = new DateTime();
                        DateTime dtEntrada = new DateTime();
                        bool imprime = false;
                        int count2 = 0;
                        dataExame = dr.GetString(4);
                        DateTime dtAtual = DateTime.Now;




                        DateTime dtExame = Convert.ToDateTime(dataExame);
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
                                string dtEntrada1 = Convert.ToString(dr2.GetDecimal(2));
                                dtEntrada1 = dtEntrada1.Substring(6, 2) + "/" + dtEntrada1.Substring(4, 2) + "/" + dtEntrada1.Substring(0, 4);
                                dtEntrada = Convert.ToDateTime(dtEntrada1);
                                dtSaida2 = dtSaida2.PadLeft(2, '0');
                                string data = dtSaida2 + "/" + dtSaida1.Substring(4, 2) + "/" + dtSaida1.Substring(0, 4);

                                dtSaida = Convert.ToDateTime(data);
                                dtSaida = dtSaida.AddDays(15);





                                if ((dtExame <= dtSaida))
                                {
                                    dtSaida = dtSaida.AddDays(-15);

                                    count2 = count2 + 1;
                                    imprime = true;

                                    DateTime comparar = new DateTime(0001, 01, 01, 0, 0, 0);
                                    if (dtSaidaAnterior.CompareTo(comparar) != 0)
                                    {
                                        if ((dtEntrada - dtSaidaAnterior).Days <= 180)
                                        {


                                            imprime = true;

                                        }
                                        else
                                        {
                                            exameExpirado = true;
                                            imprime = false;
                                            break;
                                        }
                                    }



                                    dtSaidaAnterior = dtSaida;

                                }




                            }//while existir Data de Saida

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
                                        count = count + 1;
                                        nome = dr.GetString(1);
                                        microorganismo = dr.GetString(2);
                                        sitio = dr.GetString(3);
                                        dt.Rows.Add(new String[] { rh, nome, microorganismo, sitio, dataExame });
                                    }
                                    else
                                    {
                                        exameExpirado = true;
                                    }

                                }
                                if (dataInternacao.CompareTo(comparar) != 0)
                                {

                                    if (count2 == 0)
                                    {

                                        if (dataInternacao <= dtExame)
                                        {
                                            count2 = count2 + 1;

                                            count = count + 1;

                                            microorganismo = dr.GetString(2);
                                            sitio = dr.GetString(3);
                                            dt.Rows.Add(new String[] { rh, nomePaciente, microorganismo, sitio, dataExame });

                                        }
                                        else
                                        {
                                            exameExpirado = true;
                                        }

                                    }

                                }
                            }
                            if (count2 == 0)
                            {
                                if ((dtAtual - dtExame).Days <= 180)
                                {
                                    count = count + 1;

                                    microorganismo = dr.GetString(2);
                                    sitio = dr.GetString(3);
                                    dt.Rows.Add(new String[] { rh, nomePaciente, microorganismo, sitio, dataExame });

                                }
                                else
                                {
                                    exameExpirado = true;
                                }


                            }
                        }


                    }
                    if (dt.Rows.Count > 0)
                    {
                        var orderedRows = from row in dt.AsEnumerable()
                                          let date = DateTime.Parse(row.Field<string>("Data"))
                                          orderby date descending
                                          select row;
                        DataTable tblOrdered = orderedRows.CopyToDataTable();
                        gvDados.DataSource = tblOrdered; // apresentação dos dados da lista
                        gvDados.DataBind();
                        nomePaciente = "";

                        microorganismo = "";
                        sitio = "";
                        dataExame = "";
                    }
                    if (count == 0)
                    {
                        if (exameExpirado)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Atenção! O paciente " + nomePaciente.Replace("'", " ") + " possui exame que já expirou o prazo de validade. ');", true);
                            txtRH.Text = "";
                            hfCustomerId.Value = "";
                            txtProcurar.Text = "";
                        }
                        else
                            PacienteSemBacteria(rh);
                    }
                    rh = "";
                }//using






            }//if óbito
        }//try

        catch (Exception ex)
        {
            Console.WriteLine("{0} Exception caught.", ex);
        }
    }
    public bool  TesteObito(string rh)
    {
        string status = "";

        bool bstatus = false;
        using (OdbcConnection cnn7 = new OdbcConnection(ConfigurationManager.ConnectionStrings["HospubConn"].ToString()))
        {
            OdbcCommand cmm7 = cnn7.CreateCommand();
            cmm7.CommandText = "select c15motivo from cen15 where  i15pront = " + rh;
            cnn7.Open();
            OdbcDataReader dr7 = cmm7.ExecuteReader();
            if (dr7.Read())
            {
                status = dr7.GetString(0);
                if (status == "3")
                    bstatus = true;
                else if (status == "4")
                    bstatus = true;
                else
                    bstatus = false;
            }
            dr7.Close();
        }
        using (OdbcConnection cnn8 = new OdbcConnection(ConfigurationManager.ConnectionStrings["HospubConn"].ToString()))
        {

            OdbcCommand cmm8 = cnn8.CreateCommand();
            cmm8.CommandText = "select * from intb6 where ((ib6compos like '%OBITO%') or (ib6dtobito != '' and ib6dtobito != '00000000')) and ib6regist =" + rh;
            cnn8.Open();
            OdbcDataReader dr8 = cmm8.ExecuteReader();

            if (dr8.Read())
            {
                bstatus = true;

            }
            dr8.Close();
        }

        if (bstatus)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Este RH é de um paciente com ÓBITO!');", true);


            gvDados.DataSource = null;
            gvDados.DataBind();
            txtRH.Text = "";
            txtProcurar.Text = "";
            hfCustomerId.Value = "";
        }
        return bstatus;
    }
    public void PegarDadosSistemaIsolado(string rh)
    {
        using (SqlConnection cnn5 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
        {
            SqlCommand cmm5 = cnn5.CreateCommand();
            cmm5.CommandText = "SELECT p.rh as RH ,p.nome  as Nome ,m.descricao as Microorganismo ,ma.descricao as Sitio , convert(varchar, e.dt_resultado, 103) as 'Data' FROM [Isolamento].[dbo].[Exame] as e "
            + " INNER JOIN [Isolamento].[dbo].[Paciente] as p ON e.rh = p.rh "
            + " INNER JOIN [Isolamento].[dbo].[tipos_microorganismos] as  m ON e.microorganismo = m.cod_microorg "
            + " INNER JOIN [Isolamento].[dbo].[tipos_materiais] as  ma ON e.material = ma.cod_material where p.rh = " + rh + "  order by dt_resultado desc";
            cnn5.Open();
            SqlDataReader dr5 = cmm5.ExecuteReader();

            if (dr5.HasRows)
            {
                gvDados.DataSource = dr5;
                gvDados.DataBind();

            }
            else
            {

                PacienteSemBacteria(rh);


            }
        }
    }
    public void PegarDadosSistemaIsoladoNaoInternado(string rh)
    {
        using (SqlConnection cnn5 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
        {
            SqlCommand cmm5 = cnn5.CreateCommand();
            cmm5.CommandText = "SELECT p.rh as RH ,p.nome  as Nome ,m.descricao as Microorganismo ,ma.descricao as Sitio , convert(varchar, e.dt_resultado, 103) as 'Data' FROM [Isolamento].[dbo].[Exame] as e "
            + " INNER JOIN [Isolamento].[dbo].[Paciente] as p ON e.rh = p.rh "
            + " INNER JOIN [Isolamento].[dbo].[tipos_microorganismos] as  m ON e.microorganismo = m.cod_microorg "
            + " INNER JOIN [Isolamento].[dbo].[tipos_materiais] as  ma ON e.material = ma.cod_material where p.rh = " + rh + "  order by dt_resultado desc";
            cnn5.Open();
            SqlDataReader dr5 = cmm5.ExecuteReader();

            if (dr5.HasRows)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Paciente NÃO foi INTERNADO no Hospital, mas POSSUI MDR.');", true);

                gvDados.DataSource = dr5;
                gvDados.DataBind();

            }
            else
            {

                PacienteSemBacteria(rh);


            }
        }
    }

    public void PacienteSemBacteria(string rh)
    {
        gvDados.DataSource = null;
        gvDados.DataBind();
        txtRH.Text = "";
        hfCustomerId.Value = "";
        txtProcurar.Text = "";
        using (OdbcConnection cnn6 = new OdbcConnection(ConfigurationManager.ConnectionStrings["HospubConn"].ToString()))
        {

            OdbcCommand cmm6 = cnn6.CreateCommand();
            cmm6.CommandText = "select concat(ib6pnome,ib6compos) as Nome from intb6 where ib6regist = " + rh;
            cnn6.Open();
            OdbcDataReader dr6 = cmm6.ExecuteReader();

            if (dr6.Read())
            {


                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Atenção! O paciente " + dr6.GetString(0).Replace("'"," ") + " não consta no Banco de Dados com Bactéria Multiresistente. ');", true);
                dr6.Close();

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Não existe este número de RH!');", true);
                dr6.Close();
            }

        }
    }

    private string dataFormatada(string data)
    {
        return data.Substring(0, 4) +  "-" + data.Substring(4, 2) + "-" +data.Substring(6, 2) ;

    }
    protected void btnPesquisarNome_Click(object sender, EventArgs e)
    {
        string rh = hfCustomerId.Value;
        txtRH.Text = hfCustomerId.Value;
        Pesquisar(rh);
    }
}
