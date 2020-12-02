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
using System.Collections;
using System.Net;
using Newtonsoft.Json;
using System.Reflection;
using System.IO;

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
                    // Consultar os endereços das API's para procurar dados de um paciente específico
                    // Endereços:

                    // - http://intranethspm:5003/hspmsgh-api/paciente/11036480 - consulta de um paciente no censo hospitalar. 
                    // Parâmetro: RH do paciente

                    // - http://intranethspm:5003/hspmsgh-api/internacoes/11036480 - consulta de um paciente na view de Internacao. 
                    // Parâmetro: RH do paciente

                    // - http://intranethspm:5003/hspmsgh-api/pacientes/paciente/11209913   - consulta de um paciente na view cadastro de Paciente. 
                    // Parâmetro: RH do paciente
                    try
                    {
                       // Buscar data e hora atual do sistema:
                       // DateTime now = DateTime.Now;
                       // lbDataHora.Text = now.ToString();
                        string URI = "http://intranethspm:5003/hspmsgh-api/paciente/2833747";
                        WebRequest request = WebRequest.Create(URI);

                        HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(URI);
                        // Sends the HttpWebRequest and waits for a response.
                        HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                        if (httpResponse.StatusCode == HttpStatusCode.OK)
                        {
                            var reader = new StreamReader(httpResponse.GetResponseStream());

                            JsonSerializer json = new JsonSerializer();

                            var objText = reader.ReadToEnd();

                            var details = JsonConvert.DeserializeObject<Censo>(objText);
                           

                           // GridInternado.DataSource = details; // apresentação dos dados da lista
                          //  GridInternado.DataBind();
                        }


                    }

                    catch (WebException ex)
                    {
                        string err = ex.Message;
                    }
                    using (OdbcConnection cnn = new OdbcConnection(ConfigurationManager.ConnectionStrings["HospubConn"].ToString())) // Usar as views da API no lugar da consulta que era realizada no Hospub.
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

                        // Se não existir o paciente procurado, inserir os dados do paciente na base de dados do Sistema Isolamento 
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
                                    // Inserção de um novo paciente na base de dados do Sistema de Isolamento
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


                    // Inserção dos dados do Sistema Isolamento relacionados ao cadastro do Laboratorio.


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
    //Pesquisar o nome do paciente utilizando como parâmetro o seu RH.
    protected void Pesquisar_Click(object sender, EventArgs e)
    {

        //using (OdbcConnection cnn = new OdbcConnection(ConfigurationManager.ConnectionStrings["HospubConn"].ToString()))
        // {
        //try
        // {           // {
        /*
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


        } */

        lbClinicaPreenchido.Text = "Paciente não está internado";



        try
        { 

            
            
              //  string URI_2 = "http://intranethspm:5003/hspmsgh-api/pacientes/paciente/" + txbRH.Text;
            string URI_2 = "http://localhost:5003/hspmsgh-api/pacientes/paciente/" + txbRH.Text;
            //WebRequest request_2 = WebRequest.Create(URI_2);

            HttpWebRequest httpRequest_2 = (HttpWebRequest)WebRequest.Create(URI_2);
                // Sends the HttpWebRequest and waits for a response.
                HttpWebResponse httpResponse_2 = (HttpWebResponse)httpRequest_2.GetResponse();

                if (httpResponse_2.StatusCode == HttpStatusCode.OK)
                {
                    var reader_2 = new StreamReader(httpResponse_2.GetResponseStream());

                    JsonSerializer json_2 = new JsonSerializer();

                    var objText_2 = reader_2.ReadToEnd();

                    var details_2 = JsonConvert.DeserializeObject<Paciente_Cadastro>(objText_2);
                // Se o nome do Paciente for igual a null, então apresentar a mensagem de RH inválido

                    

                if(details_2.nm_nome != null)
                {
                    lbNomePreenchido.Text = details_2.nm_nome;

                }


                    else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Número de RH não existe!');", true);
                   


                    LimpaCampos();

                }


                // GridInternado.DataSource = details; // apresentação dos dados da lista
                //  GridInternado.DataBind();
            }
            

        
        }

        catch(WebException ex)
        {
            string err = ex.Message;

            //  if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound)


        }

        try
        {

            //string URI = "http://intranethspm:5003/hspmsgh-api/paciente/" + txbRH.Text;
            string URI = "http://localhost:5003/hspmsgh-api/paciente/" + txbRH.Text;
            WebRequest request = WebRequest.Create(URI);

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(URI);
            HttpWebResponse httpResponse = null;
            // Sends the HttpWebRequest and waits for a response.
            httpResponse = (HttpWebResponse)httpRequest.GetResponse();

            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                var reader = new StreamReader(httpResponse.GetResponseStream());

                JsonSerializer json = new JsonSerializer();

                var objText = reader.ReadToEnd();

                var details = JsonConvert.DeserializeObject<Censo>(objText);

                // Se o nome da Clínica for igual a Null não preencher o label da Clinica.
            
                if (details.nm_clinica != null)
                {

                    lbClinicaPreenchido.Text = details.nm_clinica;

                }

                // GridInternado.DataSource = details; // apresentação dos dados da lista
                //  GridInternado.DataBind();
            }
        }

        catch (WebException ex)
        {
            string err = ex.Message;

            //  if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound)


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



    // Objeto Cadastro de Paciente
    public class Paciente_Cadastro
    {
        public string cd_prontuario { get; set; }
        public string nm_situacao { get; set; }
        public string nm_nome { get; set; }
        public string nm_nome_social { get; set; }
        public string nm_vinculo { get; set; }
        public string nm_orgao { get; set; }
        public string cd_rf_matricula{ get; set; }
        public string in_sexo { get; set; }
        public string dc_cor { get; set; }
        public string dc_estado_civil { get; set; }
        public string cd_mae { get; set; }
        public string nm_mae { get; set; }
        public string nm_pai { get; set; }
        public string dt_data_nascimento { get; set; }
        public string nr_idade { get; set; }
        public string nm_nacionalidade { get; set; }
        public string nm_naturalidade { get; set; }
        public string sg_uf { get; set; }
        public string dc_grau_instrucao { get; set; }
        public string dc_ocupacao { get; set; }
        public string nr_ddd_fone { get; set; }
        public string nr_fone { get; set; }
        public string nr_ddd_fone_recado { get; set; }
        public string nr_fone_recado { get; set; }
        public string cd_cep { get; set; }
        public string dc_logradouro { get; set; }
        public string nr_logradouro { get; set; }
        public string dc_complemento_logradouro { get; set; }
        public string dc_bairro { get; set; }
        public string cd_ibge_cidade { get; set; }
        public string sg_uf_endereco { get; set; }
        public string tp_enderec { get; set; }
        public string in_correspondencia { get; set; }
        public string nr_rg { get; set; }
        public string dc_orgao_emissor { get; set; }
        public string sg_uf_sigla_emitiu_docto { get; set; }
        public string dt_emissao_documento { get; set; }
        public string nr_cpf { get; set; }
        public string nr_pis { get; set; }
        public string in_documentos_apresntados { get; set; }
        public string nm_certidao { get; set; }
        public string nm_cartorio { get; set; }
        public string nr_livro { get; set; }
        public string nr_folhas { get; set; }
        public string nr_termo { get; set; }
        public string dt_emissao { get; set; }
        public string nr_declaracao_nascido { get; set; }
        public string nr_cartao_saude { get; set; }
        public string nm_motivo_cadastro { get; set; }
        public string dc_documento_referencia { get; set; }
        public string nr_cartao_nacional_saude_mae { get; set; }
        public string dt_entrada_br { get; set; }
        public string dt_naturalizacao { get; set; }
        public string nr_portaria { get; set; }
        public string dc_observacao { get; set; }
}





    // Objeto Internacao

    public class Internacao
    {
        public string cd_prontuario { get; set; }
        public string nm_paciente { get; set; }
        public string in_sexo { get; set; }
        public string nr_idade { get; set; }
        public string nr_quarto { get; set; }
        public string nr_leito { get; set; }
        public string nm_ala { get; set; }
        public string nm_clinica { get; set; }
        public string nm_unidade_funcional { get; set; }
        public string nm_acomodacao { get; set; }
        public string st_leito { get; set; }
        public string dt_internacao { get; set; }
        public string dt_entrada_setor { get; set; }
        public string nm_especialidade { get; set; }
        public string nm_medico { get; set; }
        public string dt_ultimo_evento { get; set; }
        public string nm_origem { get; set; }
        public string sg_cid { get; set; }
        public string tx_observacao { get; set; }
        public string nr_convenio { get; set; }
        public string nr_plano { get; set; }
        public string nm_convenio_plano { get; set; }
        public string nr_crm_profissional { get; set; }
        public string nm_carater_internacao { get; set; }
        public string nm_origem_internacao { get; set; }
        public string nr_procedimento { get; set; }
        public string dt_alta_medica { get; set; }
        public string dt_saida_paciente { get; set; }
        public string dc_tipo_alta_medica { get; set; }
    }



    //Objeto Censo Hospitalar

    public class Censo
    {


        public string cd_prontuario { get; set; }

        public string nm_paciente { get; set; }

        public string dt_nascimento { get; set; }
        public string nr_quarto { get; set; }

        public string dt_internacao_data { get; set; }

        public string dt_internacao_hora { get; set; }
        public string nm_clinica { get; set; }

        public string in_sexo { get; set; }

        public string nr_idade { get; set; }


        public string cod_CID { get; set; }

        public string descricaoCID { get; set; }

        public string nm_unidade_funcional { get; set; }
        public string tempo { get; set; }

        public string vinculo { get; set; }




    }

}
