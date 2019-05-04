using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Kernel7x;
using TSI.GymTech.Entity.Models;
using TSI.GymTech.Entity.Enumerates;
using TSI.GymTech.Manager.EntityManagers;

using System.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TSI.GymTech.Entity.Configurations;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.IO;

namespace TSI.GymTech.OnOff
{

    public partial class Form1 : Form
    {
        private CultureInfo _cultureInfo = new CultureInfo("pt");
        private ResourceManager _resourceManager = new ResourceManager(typeof(Entity.App_LocalResources.GateStatusType));

        //Método da API
        [DllImport("wininet.dll")]
        private extern static Boolean InternetGetConnectedState(out int Description, int ReservedValue);

        private Alternativo kernel7x; //Declarando Kernel
        private int indexCombo;
        int collectIndex;
        
        int eventIndex;

        delegate void ViewLine(TextBox textBox, string texto);
        delegate void IndexCombo(ComboBox combo);
        
        public Form1()
        {
            InitializeComponent();
            kernel7x = new Alternativo(); //Instanciando Kernel
            //kernel7x.OnRegistro += new IAlternativoEvents_OnRegistroEventHandler(
            //    onlineRegistryEventHandler);//Definindo evento para online
            //kernel7x.OnExistOff += new IAlternativoEvents_OnExistOffEventHandler(
            //    offlineRegistryEventHandler);

            kernel7x.OnRegistro += onlineRegistryEventHandler;
            kernel7x.OnExistOff += offlineRegistryEventHandler;
            kernel7x.OnProgresso += this.progressEventHandler;
            
            this.collectIndex = -1;
            this.eventIndex = -1;
        }
        
        /*=======================================================================
         * 
         *                      ROTINAS AUXILIARES
         * 
         *=======================================================================*/
        
        private short booltoshort(bool value)
        {
            if (value) return 1; else return 0;
        }

        private void indexComboBox(ComboBox combo)
        {
            indexCombo = cbxAcesso.SelectedIndex;
        }

        private void preeche(TextBox textBox, string texto)
        {
            addViewLine(texto);
            return;
        }

        private void addViewLine(String linha)
        {
            /*int tam = txtMemo.Lines.Length + 1;
            String[] texto = new String[tam];
            for (int i = 0; i < texto.Length - 1; i++)
            {
                texto[i] = txtMemo.Lines[i];
            }
            texto[texto.Length - 1] = linha;
            txtMemo.Lines = texto;*/

            txtMemo.AppendText(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff - ") + linha + "\r\n");
        }
        
        /*=======================================================================
         * 
         *                          ROTINAS DO FORM
         * 
         *=======================================================================*/
        
        //Form load
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            { 
                cbxAcesso.SelectedIndex = 1;
                rbOnline.Checked = true;
            }
            catch(Exception ex)
            {
                //Pending: error to the log file
            }
        }
        
        //BOTÕES DA INTERFACE
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                //Exemplo de adição de equipamento no Kernel 7x
                //Montagem da configuração de comunicaçao

                SComConfig _rConfig;
                _rConfig.IsCatraca = 0;

                _rConfig.Tcp.Ip = eIp.Text;
                _rConfig.ModoComunicacao = SModoComunicacao.cmcOnOff;
                _rConfig.TipoComunicacao = STipoComunicacao.ctcTcpIp;
                _rConfig.Tcp.MAC = "";
                _rConfig.Tcp.Porta = 3000;
                _rConfig.Serial.NumeroRelogio = 1;

                //Criando thread para o equipamento tcpip
                int indexAux = kernel7x.get_AdicionaCardTcpIp(_rConfig.Tcp.Ip,
                    _rConfig.Tcp.MAC, _rConfig.Tcp.Porta, false, _rConfig.ModoComunicacao);

                if (indexAux >= 0)
                {
                    //cbxEquipments.Items.Add(indexAux.ToString());
                    cbxEquipments.Items.Add(eIp.Text);
                    kernel7x.SetSincronizar(indexAux, false);
                    addViewLine("Relógio TcpIp adicionado: " + eIp.Text);
                }
                else
                {
                    addViewLine("Falha: " +
                        kernel7x.ErrorDescription(kernel7x.KernelLastError));
                }
            }
            catch(Exception ex)
            {
                //Pending: error to the log file
            }
        }

        /*=======================================================================
         * 
         *                  ROTINAS DE COMUNICAÇÃO COM O KERNEL
         * 
         *=======================================================================*/
        
        private void getRegistroGeneric(int pThreadIndex, out SRegistro registro,
            bool pTipoOnline)
        {

            //=============================================================================
            //Montar os registros para ser usado no modo OnLine ou para gravar em um SGBD
            //=============================================================================

            registro = new SRegistro();
            bool _rSaida;
            bool _rMasterLiberou;
            bool _rFuncaoLiberou;
            bool _rAcessoNegado;
            if (pTipoOnline)
            {
                kernel7x.RegistroOn(pThreadIndex, out registro.NumeroRelogio,
                    out registro.Funcao, out registro.Matricula, out registro.DataHora,
                    out registro.Flag, out _rSaida, out _rMasterLiberou,
                    out _rFuncaoLiberou, out _rAcessoNegado,
                    out registro.Tipo.FonteEntrada, out registro.Tipo.TipoNegado);
            }
            else
            {
                kernel7x.RegistroOff(pThreadIndex, out registro.NumeroRelogio,
                    out registro.Funcao, out registro.Matricula, out registro.DataHora,
                    out registro.Flag, out _rSaida, out _rMasterLiberou,
                    out _rFuncaoLiberou, out _rAcessoNegado,
                    out registro.Tipo.FonteEntrada, out registro.Tipo.TipoNegado);
            }
            registro.Tipo.MasterLiberou = booltoshort(_rMasterLiberou);
            registro.Tipo.FuncaoLiberou = booltoshort(_rFuncaoLiberou);
            registro.Tipo.AcessoNegado = booltoshort(_rAcessoNegado);
        }

        private int getRegistryQtty(int pThreadIndex)
        {
            ViewLine viewL = new ViewLine(this.preeche);
            txtMemo.Invoke(viewL, txtMemo, "Recebendo qtde de registros para " + pThreadIndex);

            //Detecta a quantidade de registros offline no relógio
            int regQtty = kernel7x.get_RecebeQtRegistros(pThreadIndex);

            //Verifica código de erro da última operação
            if (kernel7x.get_ThreadLastError(pThreadIndex) != 0)
            {
                //Caso tenha retornado erro, retorna 0
                regQtty = 0;
            }
            else
            {
                txtMemo.Invoke(viewL, txtMemo, regQtty + " registros em " + pThreadIndex);
            }
            return regQtty;
        }

        private void collectRegistry()
        {
            collectRegistry(this.collectIndex);
        }

        private void collectRegistry(int pThreadIndex)
        {
            //Rotina exemplo de coleta
            ViewLine viewL = new ViewLine(this.preeche);

            txtMemo.Invoke(viewL, txtMemo, "Inicializando coleta para " + pThreadIndex);

            //Verificando se há registros para coletar...
            if (getRegistryQtty(pThreadIndex) > 0)
            {

                while (kernel7x.get_RecebePacote(pThreadIndex))
                {
                    SRegistro registro;
                    int qtpacote = kernel7x.get_QuantRegsColetados(pThreadIndex);
                    for (int i = 0; i < qtpacote; i++)
                    {
                        getRegistroGeneric(pThreadIndex, out registro, false);
                        txtMemo.Invoke(viewL, txtMemo, "Matrícula: " + registro.Matricula);
                        txtMemo.Invoke(viewL, txtMemo, "Data e Hora: " + registro.DataHora);
                    }
                    txtMemo.Invoke(viewL, txtMemo, "Excluindo pacote...");
                    if (kernel7x.get_ApagaUltimoPacote(pThreadIndex))
                    {
                        txtMemo.Invoke(viewL, txtMemo, "Pacote excluído");
                    }

                    txtMemo.Invoke(viewL, txtMemo, "Recebendo pacote " + pThreadIndex);
                }
                
                txtMemo.Invoke(viewL, txtMemo, "Coleta FINALIZADA");
            }
            else
            {
                txtMemo.Invoke(viewL, txtMemo, "Não há registros para coletar");
            }

            this.collectIndex = -1;
        }
        
        /*=======================================================================
         * 
         *                  TRATAMENTO DE EVENTOS DO KERNEL 
         * 
         *=======================================================================*/
        
        public void progressEventHandler(int pThreadIndex, int pByte, int pByteMax, int pBuffer, int pBufferMax)
        {
            Application.DoEvents();
        }
        
        private void offlineRegistryEventHandler(int pThreadIndex, int pCount, byte pNumRel)
        {
            ViewLine viewL = new ViewLine(this.preeche);
            try
            {
                txtMemo.Invoke(viewL, txtMemo, "Há " + pCount + " registros em " + pThreadIndex);

                if (this.collectIndex == -1)
                {
                    this.collectIndex = pThreadIndex;
                    txtMemo.Invoke(viewL, txtMemo, "Chamando thread coleta");
                    Thread collectThread = new Thread(this.collectRegistry);
                    collectThread.Start();
                }
            }
            catch (Exception e)
            {
                txtMemo.Invoke(viewL, txtMemo, "Exceção: " + e.Message);
            }
        }
        
        private void onlineRegistryEventHandler(int pThreadIndex)
        {
            try
            {
                if (this.eventIndex == -1)
                {
                    this.eventIndex = pThreadIndex;
                    Thread regThread = new Thread(this.onlineRegistryEventHandlerThd);
                    regThread.Start();
                }
            }
            catch (Exception ex)
            {
                //Pending: error to the log file
            }
        }

        private void onlineRegistryEventHandlerThd()
        {
            int pThreadIndex = eventIndex;
            
            ViewLine viewL = new ViewLine(this.preeche);
            //txtMemo.Invoke(viewL, txtMemo, "Evento online recebido : " + pThreadIndex);

            try
            {
                //======================================================================================
                //Esta rotina demonstra um pequeno exemplo de como se trabalha OnLine nos 
                //equipamentos da família 7x
                //
                //ATENÇÃO
                // 
                //Esta rotina é um evento do própriokernel = Evento (OnRegistro)
                //====================================================================================
                
                //Recebe solicitação do kernel
                SRegistro registro;
                /*
                 * Variáveis do Sregistros
                 */
                bool Saida = false;
                bool MasterLiberou = false;
                bool FuncaoLiberou = false;
                bool AcessoNegado = false;

                txtMemo.Invoke(viewL, txtMemo, "Evento online recebido : " + pThreadIndex);

                //Seu tratamento de acesso e ponto
                SResposta resposta = new SResposta();

                //Recebe do relógio ou catraca em tempo real
                this.kernel7x.RegistroOn(pThreadIndex,
                    out registro.NumeroRelogio,
                    out registro.Funcao,
                    out registro.Matricula,
                    out registro.DataHora,
                    out registro.Flag,
                    out Saida,
                    out MasterLiberou,
                    out FuncaoLiberou,
                    out AcessoNegado,
                    out registro.Tipo.FonteEntrada,
                    out registro.Tipo.TipoNegado);
                registro.Tipo.Saida = booltoshort(Saida);
                registro.Tipo.MasterLiberou = booltoshort(MasterLiberou);
                registro.Tipo.FuncaoLiberou = booltoshort(FuncaoLiberou);
                registro.Tipo.AcessoNegado = booltoshort(AcessoNegado);
                resposta.Tempo = Convert.ToByte(numTempo.Value);
                
                Person person = null;
                GateConfiguration gateConfig = null;
                bool personFound = false;
                
                if (rbOnline.Checked)
                {
                    if (!IsConnected())
                    {
                        resposta.Acesso = SAcessoOnline.canNegado;
                        resposta.Mensagem = "Por problemas de conexão com a internet, não foi possével localizar a matrícula: " + registro.Matricula;

                        int.TryParse(registro.Matricula, out int matricula);
                        PrepareLogInfo(viewL, matricula, resposta.Mensagem);
                    }
                    else if (!string.IsNullOrEmpty(registro.Matricula) && int.TryParse(registro.Matricula, out int matricula))
                    {
                        PersonManager personManager = new PersonManager();
                        person = personManager.FindById(matricula).Data;

                        if (person != null)
                        {
                            personFound = true;
                            gateConfig = person.GetGateConfig();

                            switch (gateConfig.GateStatus)
                            {
                                case GateStatusType.Denied:
                                    resposta.Acesso = SAcessoOnline.canNegado;
                                    resposta.Mensagem = gateConfig.GateMessage;
                                    CreateAccessLog(person, gateConfig);
                                    PrepareLogInfo(viewL, person.PersonId, resposta.Mensagem);

                                    var newGridViewRowBlocked = new string[]
                                    {
                                        person.Name,
                                        person.PersonId.ToString(),
                                        _resourceManager.GetString(gateConfig.GateStatus.ToString(), _cultureInfo),
                                        gateConfig.GateMessage
                                    };

                                    dtGridView.Invoke((MethodInvoker)delegate
                                    {
                                        AddNewGridViewRow(newGridViewRowBlocked);
                                    });

                                    break;

                                case GateStatusType.AllowedEntry:
                                    resposta.Acesso = SAcessoOnline.canLibEntrada;
                                    resposta.Mensagem = gateConfig.GateMessage;
                                    break;

                                case GateStatusType.AllowedExit:
                                    resposta.Acesso = SAcessoOnline.canLibSaida;
                                    resposta.Mensagem = gateConfig.GateMessage?.Replace("   ", " ");
                                    break;

                                case GateStatusType.AllowedBothSides:
                                    resposta.Acesso = SAcessoOnline.canAmbosLados;
                                    resposta.Mensagem = gateConfig.GateMessage;
                                    break;
                            }
                        }
                    }

                    if (!personFound)
                    {
                        resposta.Acesso = SAcessoOnline.canNegado;
                        resposta.Mensagem = "Não foi possível localizar a matrícula: " + registro.Matricula;

                        int.TryParse(registro.Matricula, out int matricula);
                        PrepareLogInfo(viewL, matricula, resposta.Mensagem);
                    }
                }
                else
                {
                    resposta.Mensagem = emensagem.Text;

                    if (cbxAcesso.InvokeRequired)
                    {
                        IndexCombo combo = new IndexCombo(this.indexComboBox);
                        cbxAcesso.Invoke(combo, cbxAcesso);
                    }
                    indexCombo = 1;

                    switch (indexCombo)
                    {
                        case 0:
                            resposta.Acesso = SAcessoOnline.canNegado;
                            break;
                        case 1:
                            resposta.Acesso = SAcessoOnline.canLibEntrada;
                            break;
                        case 2:
                            resposta.Acesso = SAcessoOnline.canLibSaida;
                            break;
                        case 3:
                            resposta.Acesso = SAcessoOnline.canRevista;
                            break;
                        case 4:
                            resposta.Acesso = SAcessoOnline.canAmbosLados;
                            break;
                    }
                }

                //Envia resposta ao equipamento
                kernel7x.RespostaOn(pThreadIndex, resposta.Acesso, resposta.Mensagem, resposta.Tempo);
                
                if (personFound && registro.Flag == SFlagRegistro.sfrGirou)
                {
                    /*
                        * ============================================================
                        * Neste if quando catraca, vem a flag de giro informando se a 
                        * catraca girou ou não
                        * 
                        * OBSERVAÇÕES
                        * não é necessário enviar outra mensagem para catraca aqui
                        * está apenas para informação de exemplo, suas rotinas
                        * neste if pode ser para outras afinidades ex: 
                        * gravação em banco, montagem de log etc....
                        * ============================================================
                        */

                    CreateAccessLog(person, gateConfig);
                    PrepareLogInfo(viewL, person.PersonId, resposta.Mensagem);

                    var newGridViewRow = new string[]
                    {
                        person.Name,
                        person.PersonId.ToString(),
                        _resourceManager.GetString(gateConfig.GateStatus.ToString(), _cultureInfo),
                        gateConfig.GateMessage
                    };

                    dtGridView.Invoke((MethodInvoker)delegate
                    {
                        AddNewGridViewRow(newGridViewRow);
                    });
                    //if (InvokeRequired)
                    //{
                    //    ReceberMensagemCallback callback = AddNewGridViewRow;
                    //    Invoke(callback, newGridViewRow);
                    //}
                }
            }
            catch (Exception e)
            {
                txtMemo.Invoke(viewL, txtMemo, "Exceção: " + e.Message);
            }

            eventIndex = -1;
        }

        delegate void ReceberMensagemCallback(string[] parameters);
        void AddNewGridViewRow(string[] parameters)
        {
            dtGridView.Rows.Add(parameters);
        }

        private void rbOnline_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // Update combobox with equipaments inserted in database
                if (rbOnline.Checked)
                {
                    if (!IsConnected())
                    {
                        MessageBox.Show("Não existe conexão ativa com a internet.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        rbOffline.Checked = true;
                    }
                    else
                    {
                        cbxEquipments.Items.Clear();
                        GetAllAccessControl();
                        cbxEquipments.SelectedIndex = 0;
                        EnableDisableFields(false);
                    }
                }
            }
            catch (Exception ex)
            {
                //Pending: error to the log file
            }
        }

        private void rbOffline_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // Clear combobox equipaments 
                if (rbOffline.Checked)
                {
                    cbxEquipments.Items.Clear();
                    cbxEquipments.Text = string.Empty;
                    EnableDisableFields(true);
                }
            }
            catch (Exception ex)
            {
                //Pending: error to the log file}
            }
        }

        private void EnableDisableFields(bool status)
        {
            try
            {
                cbxAcesso.Enabled = status;
                numTempo.Enabled = status;
                emensagem.Enabled = status;
                eIp.Enabled = status;
                btnAdicionar.Enabled = status;
            }
            catch (Exception ex)
            {
                //Pending: error to the log file
            }
        }

        private void GetAllAccessControl()
        {
            try
            {
                if (IsConnected())
                { 
                    AccessControlManager accessControlManager = new AccessControlManager();
                    IEnumerable<AccessControl> accessControlList = accessControlManager.FindAll().Data;

                    //Exemplo de adição de equipamento no Kernel 7x
                    //Montagem da configuração de comunicaçao
                    foreach (var accessControl in accessControlList)
                    {
                        SComConfig _rConfig;
                        _rConfig.IsCatraca = 0;

                        _rConfig.Tcp.Ip = accessControl.IpAddress;
                        _rConfig.ModoComunicacao = SModoComunicacao.cmcOnOff;
                        _rConfig.TipoComunicacao = STipoComunicacao.ctcTcpIp;
                        _rConfig.Tcp.MAC = "";
                        _rConfig.Tcp.Porta = 3000;
                        _rConfig.Serial.NumeroRelogio = 1;

                        //Criando thread para o equipamento tcpip
                        int indexAux = kernel7x.get_AdicionaCardTcpIp(_rConfig.Tcp.Ip,
                            _rConfig.Tcp.MAC, _rConfig.Tcp.Porta, false, _rConfig.ModoComunicacao);

                        if (indexAux >= 0)
                        {
                            cbxEquipments.Items.Add(accessControl.Name);
                            kernel7x.SetSincronizar(indexAux, false);
                            addViewLine("Relógio TcpIp adicionado: " + accessControl.Name);
                        }
                        else
                        {
                            addViewLine("Falha: " +
                                kernel7x.ErrorDescription(kernel7x.KernelLastError));
                        }
                    }
                }
                else
                {
                    //var equipaments = System.Configuration.ConfigurationSettings.AppSettings.Get("equipaments");
                    //var name = System.Configuration.ConfigurationSettings.AppSettings["defaultAccessControlName"].ToString();
                    //var ip = System.Configuration.ConfigurationSettings.AppSettings["defaultAccessControlIp"].ToString();
                }
            }
            catch (Exception ex)
            {
                //Pending: error to the log file
            }
        }

        private void CreateAccessLog(Person person, GateConfiguration gateConfig)
        {
            try
            { 
                AccessControlManager accessControlManager = new AccessControlManager();
                AccessLogManager accessLogManager = new AccessLogManager();
                AccessLog newAccessLog = new AccessLog();

                newAccessLog.PersonId = person.PersonId;
                newAccessLog.AccessControlId = accessControlManager.FindAll().Data.FirstOrDefault().AccessControlId;
                newAccessLog.AccessType = gateConfig.GateStatus;
                newAccessLog.MessageDisplayed = gateConfig.GateStatus != GateStatusType.Denied ?
                    gateConfig.GateMessage : gateConfig.GateMessage?.Replace("   ", " ");

                newAccessLog.CreateUserId = person.PersonId;
                newAccessLog.CreateDate = DateTime.Now;
                newAccessLog.ModifyUserId = person.PersonId;
                newAccessLog.ModifyDate = DateTime.Now;

                accessLogManager.Create(newAccessLog);
            }
            catch (Exception ex)
            {
                //Pending: error to the log file
            }
        }

        public static bool CreateLogInfo(string[] messages)
        {
            var filePath = @"C:\GymTech\logs";
            var fileName = DateTime.Now.ToString("yyyy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + ".txt";
            bool exists = Directory.Exists(filePath);
            
            try
            {
                if (!exists)
                    Directory.CreateDirectory(filePath);

                if (!File.Exists(filePath))
                {
                    using (StreamWriter sw = File.CreateText(filePath + @"\" + fileName))
                    {
                        foreach (var message in messages)
                        {
                            sw.WriteLine(message);
                        }
                    }
                    return true;
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filePath + @"\" + fileName))
                    {
                        foreach (var message in messages)
                        {
                            sw.WriteLine(message);
                        }
                    }
                    return true;
                }

            }
            catch (Exception e)
            {
                return false;
            }
        }

        // Um método que verifica se esta conectado
        private bool IsConnected()
        {
            try
            { 
                int Description;
                return InternetGetConnectedState(out Description, 0);
            }
            catch(Exception ex)
            {
                //Pending: error to the log file
                return false;
            }
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            ViewLine viewL = new ViewLine(this.preeche);

            if (!IsConnected())
            {
                MessageBox.Show("Não exite conexão ativa com a internet.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!string.IsNullOrEmpty(txtMatricula.Text) && int.TryParse(txtMatricula.Text, out int matricula))
            {
                PersonManager personManager = new PersonManager();
                Person person = personManager.FindById(matricula).Data;

                if (person != null)
                {
                    GateConfiguration gateConfig = person.GetGateConfig();

                    txtLogAdmin.Invoke((MethodInvoker)delegate
                    {
                        txtLogAdmin.Text += "Matrícula: " + person.PersonId + "\r\n";
                        txtLogAdmin.Text += "Mensagem: " + gateConfig.GateMessage.Replace("   ", " ") + "\r\n";
                        txtLogAdmin.Text += "Status: " + _resourceManager.GetString(gateConfig.GateStatus.ToString(), _cultureInfo) + "\r\n";
                        txtLogAdmin.Text += "######################################################";
                    });

                    PrepareLogInfo(viewL, person.PersonId, gateConfig.GateMessage.Replace("   ", " "));
                }
                else
                {
                    MessageBox.Show("Não foi encontrado nenhum aluno com a matrícula: " + matricula, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void PrepareLogInfo(ViewLine viewL, int matricula, string resposta)
        {
            txtMemo.Invoke(viewL, txtMemo, "Matrícula: " + matricula);
            txtMemo.Invoke(viewL, txtMemo, "Mensagem: " + resposta);
            
            var logInfo = new string[]
            {
                DateTime.Now.ToString() + " - Matrícula: " + matricula,
                DateTime.Now.ToString() + " - Mensagem: " + resposta
            };

            CreateLogInfo(logInfo);
        }

        //private void btnQuantidade_Click(object sender, EventArgs e)
        //{
        //    int index = cbxEquipments.SelectedIndex;
        //    if (index > -1)
        //    {
        //        //btnQuantidade.Enabled = false;
        //        int qtty = getRegistryQtty(index);
        //        if (qtty > -1)
        //        {
        //            addViewLine(qtty + " registros encontrados");
        //        }
        //        else
        //        {
        //            addViewLine("Falha: " +
        //                kernel7x.ErrorDescription(kernel7x.KernelLastError));

        //        }
        //        //btnQuantidade.Enabled = true;
        //    }
        //    else
        //    {
        //        addViewLine("Selecione um equipamento.");
        //    }
        //}

        //private void btnColetar_Click(object sender, EventArgs e)
        //{
        //    int index = cbxEquipments.SelectedIndex;
        //    if (index > -1)
        //    {
        //        //btnColetar.Enabled = false;
        //        this.collectRegistry(index);
        //        //btnColetar.Enabled = true;
        //    }
        //    else
        //    {
        //        addViewLine("Selecione um equipamento.");
        //    }
        //}

        //private async void GetAllAccessControl()
        //{
        //    string URI = "https://academiakalinauskas.com.br/gymtech/webapi/accesscontrol/getall";
        //    IEnumerable<AccessControl> accessControlList = null;

        //    using (var client = new HttpClient())
        //    {
        //        using (var response = await client.GetAsync(URI))
        //        {
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var jsonString = await response.Content.ReadAsStringAsync();
        //                accessControlList = JsonConvert.DeserializeObject<AccessControl[]>(jsonString).ToList();
        //            }
        //            else
        //            {
        //                MessageBox.Show("Não foi possível obter acesso aos equipamentos cadastrados: " + response.StatusCode);
        //            }
        //        }
        //    }

        //    //Exemplo de adição de equipamento no Kernel 7x
        //    //Montagem da configuração de comunicaçao
        //    foreach(var accessControl in accessControlList)
        //    { 
        //        SComConfig _rConfig;
        //        _rConfig.IsCatraca = 0;

        //        _rConfig.Tcp.Ip = accessControl.IpAddress;
        //        _rConfig.ModoComunicacao = SModoComunicacao.cmcOnOff;
        //        _rConfig.TipoComunicacao = STipoComunicacao.ctcTcpIp;
        //        _rConfig.Tcp.MAC = "";
        //        _rConfig.Tcp.Porta = 3000;
        //        _rConfig.Serial.NumeroRelogio = 1;

        //        //Criando thread para o equipamento tcpip
        //        int indexAux = kernel7x.get_AdicionaCardTcpIp(_rConfig.Tcp.Ip,
        //            _rConfig.Tcp.MAC, _rConfig.Tcp.Porta, false, _rConfig.ModoComunicacao);

        //        if (indexAux >= 0)
        //        {
        //            cbxEquipments.Items.Add(accessControl.Name);
        //            kernel7x.SetSincronizar(indexAux, false);
        //            addViewLine("Relógio TcpIp adicionado: " + accessControl.Name);
        //        }
        //        else
        //        {
        //            addViewLine("Falha: " +
        //                kernel7x.ErrorDescription(kernel7x.KernelLastError));
        //        }
        //    }
        //}

        //private async void GetPersonById(string matricula)
        //{
        //    if (Int32.TryParse(matricula, out int id))
        //    {
        //        string URI = "https://academiakalinauskas.com.br/gymtech/webapi/person/getbyid/?id=" + id;
        //        using (var client = new HttpClient())
        //        {
        //            using (var response = await client.GetAsync(URI))
        //            {
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    var jsonString = await response.Content.ReadAsStringAsync();
        //                    _currPerson = JsonConvert.DeserializeObject<Person>(jsonString);
        //                    _gateConfig = _currPerson.GetGateConfig();
        //                }
        //            }
        //        }
        //    }
        //}
    }
}