using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Laboratorio_Laboratorio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lbUser.Text = User.Identity.Name;
        if (!IsPostBack)
        {
            string dtHoje = DateTime.Now.Date.ToShortDateString();
            txbData.Text = dtHoje;
        }
       
    }
    protected void btnGravar_Click(object sender, EventArgs e)
    {
        if (lbNomePreenchido.Text == "") // your condition
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Atenção! Digite o RH e clique no botão [Pesquisar] para aparecer o nome do Paciente.');", true);
            txbRH.Text = "";
        }
        else
        {
            RequiredFieldValidator1.Enabled = false;

            //tabela Exame
            DateTime data_resultado_exame = Convert.ToDateTime(txbData.Text);
            string codMicroorg = ddlMicroorganismo.SelectedValue;
            string codMaterial = ddlMaterial.SelectedValue;

            string rh = txbRH.Text;
            string clinica = lbClinicaPreenchido.Text;
            string contato = txbContato.Text;
            DateTime dt_cadastro = DateTime.Now;
            DateTime dt_ultima_atualizacao = DateTime.Now;
            string usuario = lbUser.Text;

            //tabela paciente
            string nomePaciente = lbNomePreenchido.Text;
            string dt_nascimento = "";
            char sexo;

            //insert paciente
            using (SqlConnection cnn4 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
            {
                SqlCommand cmm4 = cnn4.CreateCommand();
                cmm4.CommandText = "SELECT * FROM [Isolamento].[dbo].[Exame] WHERE  microorganismo = " + codMicroorg + "and material = " + codMaterial + "and rh =" + rh + " and dt_resultado = '" + converterData2(data_resultado_exame) + "'";
                cnn4.Open();
                SqlDataReader dr4 = cmm4.ExecuteReader();
                if (dr4.Read())
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Atenção! Já existe um registro com este número de RH, data do resultado do exame, Microorganismo e Material.' );", true);
                    LimpaCampos();
                    dr4.Close();
                }
                else
                {
                    using (OdbcConnection cnn = new OdbcConnection(ConfigurationManager.ConnectionStrings["HospubConn"].ToString()))
                    {
                        OdbcCommand cmm = cnn.CreateCommand();
                        cmm.CommandText = "select ib6regist, concat(ib6pnome,ib6compos) as nome , ib6dtnasc,ib6sexo from intb6  where ib6regist =" + rh;
                        cnn.Open();
                        OdbcDataReader dr1 = cmm.ExecuteReader();

                        if (!dr1.Read())
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Número de RH não existe" + rh + "!);", true);
                            dr1.Close();

                        }
                        else
                        {
                            string rh2 = dr1.GetDecimal(0).ToString();
                            string nomeCompleto = dr1.GetString(1);
                            dt_nascimento = dr1.GetString(2);
                            sexo = dr1.GetChar(3);
                            using (SqlConnection cnn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
                            {

                                SqlCommand cmm2 = cnn2.CreateCommand();
                                cmm2.CommandText = "SELECT rh FROM Paciente WHERE rh = " + rh2;
                                cnn2.Open();
                                SqlDataReader dr2 = cmm2.ExecuteReader();
                                if (!dr2.Read())
                                {
                                    dr2.Close();
                                    using (SqlConnection cnn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
                                    {
                                        SqlCommand cmm1 = cnn1.CreateCommand();
                                        cmm1.CommandText = "INSERT INTO Paciente (rh, nome, dt_nasc, sexo, obito) VALUES (@rh,@nome,@dt_nascimento, @sexo,@obito)";

                                        cmm1.Parameters.Add("@rh", SqlDbType.VarChar).Value = rh2;
                                        cmm1.Parameters.Add("@nome", SqlDbType.VarChar).Value = nomeCompleto;
                                        cmm1.Parameters.Add("@dt_nascimento", SqlDbType.Date).Value = converterData(dt_nascimento);
                                        cmm1.Parameters.Add("@sexo", SqlDbType.Char).Value = sexo;
                                        cmm1.Parameters.Add("@obito", SqlDbType.Bit).Value = false;

                                        try
                                        {
                                            cnn1.Open();
                                            cmm1.ExecuteNonQuery();
                                        }
                                        catch (Exception ex)
                                        {
                                            string err = ex.Message;
                                            
                                        }

                                    }//using

                                }//if


                            }//using


                        }//else

                    }//using





                    using (SqlConnection cnn3 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringIsolamento"].ToString()))
                    {
                        SqlCommand cmm3 = cnn3.CreateCommand();
                        cmm3.CommandText = "INSERT INTO Exame (dt_resultado, microorganismo, material, rh,clinica,contato, dt_cadastro,dt_ultima_atualizacao,usuario) VALUES (@data_resultado_exame,@microorganismo,@material, @rh,@clinica,@contato,@dt_cadastro, @dt_ultima_atualizacao,@usuario)";

                        cmm3.Parameters.Add("@data_resultado_exame", SqlDbType.Date).Value = data_resultado_exame;
                        cmm3.Parameters.Add("@microorganismo", SqlDbType.VarChar).Value = codMicroorg;
                        cmm3.Parameters.Add("@material", SqlDbType.VarChar).Value = codMaterial;
                        cmm3.Parameters.Add("@rh", SqlDbType.VarChar).Value = rh;
                        cmm3.Parameters.Add("@clinica", SqlDbType.VarChar).Value = clinica;
                        cmm3.Parameters.Add("@contato", SqlDbType.VarChar).Value = contato;
                        cmm3.Parameters.Add("@dt_cadastro", SqlDbType.DateTime).Value = dt_cadastro;
                        cmm3.Parameters.Add("@dt_ultima_atualizacao", SqlDbType.DateTime).Value = dt_ultima_atualizacao;
                        cmm3.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario;

                        try
                        {
                            cnn3.Open();
                            cmm3.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            string err = ex.Message;
                            
                        }
                    }//using
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Gravação realizada com sucesso!' );", true);
                    LimpaCampos();
                }//else
            }

        }//else do required control
    }
    protected void Pesquisar_Click(object sender, EventArgs e)
    {
       using (OdbcConnection cnn = new OdbcConnection(ConfigurationManager.ConnectionStrings["HospubConn"].ToString()))
        {
            try
            {
                OdbcCommand cmm = cnn.CreateCommand();
                cmm.CommandText = "Select  concat(ib6pnome,ib6compos) from intb6 where ib6regist = " + txbRH.Text;
                cnn.Open();
                OdbcDataReader dr = cmm.ExecuteReader();

                if (dr.Read())
                {
                    lbNomePreenchido.Text = dr.GetString(0);
                    dr.Close();
                }


                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Número de RH não existe!');", true);
                    dr.Close();


                    LimpaCampos();

                }
             
                OdbcCommand cmm1 = cnn.CreateCommand();
                cmm1.CommandText = "select c14nomec from cen02 ,cen14, intb6 where i02pront = ib6regist and c14codclin = c02codclin and ib6regist =" + txbRH.Text;
             
                OdbcDataReader dr1 = cmm1.ExecuteReader();

                if (dr1.Read())
                {
                    lbClinicaPreenchido.Text = dr1.GetString(0);

                }


                else
                {
                    lbClinicaPreenchido.Text = "Paciente não está internado";


                }
            }

            catch (Exception ex)
            {
                String erro = ex.Message;
            }
        }
   
    }
    public void LimpaCampos()
    {
        lbNomePreenchido.Text = "";
        lbClinicaPreenchido.Text = "";
      
         
        ddlMicroorganismo.SelectedIndex = 0;
        ddlMaterial.SelectedIndex = 0;
      
        
           
        txbContato.Text = "";
        txbRH.Text = "";
        string dtHoje = DateTime.Now.Date.ToShortDateString();
        txbData.Text = dtHoje;
        
    }
    public string converterData(string data)
    {
        string ano = data.Substring(0, 4);
        string mes = data.Substring(4, 2);
        string dia = data.Substring(6, 2);
        string dataBanco = ano + "-" + mes + "-" + dia;


        return dataBanco;
    }
    public string converterData2(DateTime data)
    {
        string ano = data.Year.ToString();
        string mes = data.Month.ToString();
        
        string dia = data.Day.ToString();
        string dataBanco = ano + "-" + mes + "-" + dia;


        return dataBanco;
    }
}
