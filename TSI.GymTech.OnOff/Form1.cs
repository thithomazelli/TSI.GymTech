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
using TSI.GymTech.Manager.EntityManagers;

using System.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TSI.GymTech.Entity.Configurations;

namespace TSI.GymTech.OnOff
{

    public partial class Form1 : Form
    {
        private Person _currPerson;
        private GateConfiguration _gateConfig;

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
            cbxAcesso.SelectedIndex = 1;
            rbOnline.Checked = true;
        }


        //BOT�ES DA INTERFACE
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            //Exemplo de adi��o de equipamento no Kernel 7x
            //Montagem da configura��o de comunica�ao

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
                cbxEquipments.Items.Add(indexAux.ToString());
                kernel7x.SetSincronizar(indexAux, false);
                addViewLine("Rel�gio TcpIp adicionado: " + eIp.Text);
            }
            else
            {
                addViewLine("Falha: " +
                    kernel7x.ErrorDescription(kernel7x.KernelLastError));
            }
        }

        private void btnQuantidade_Click(object sender, EventArgs e)
        {
            int index = cbxEquipments.SelectedIndex;
            if (index > -1)
            {
                //btnQuantidade.Enabled = false;
                int qtty = getRegistryQtty(index);
                if (qtty > -1)
                {
                    addViewLine(qtty + " registros encontrados");
                }
                else
                {
                    addViewLine("Falha: " +
                        kernel7x.ErrorDescription(kernel7x.KernelLastError));

                }
                //btnQuantidade.Enabled = true;
            }
            else
            {
                addViewLine("Selecione um equipamento.");
            }
        }

        private void btnColetar_Click(object sender, EventArgs e)
        {
            int index = cbxEquipments.SelectedIndex;
            if (index > -1)
            {
                //btnColetar.Enabled = false;
                this.collectRegistry(index);
                //btnColetar.Enabled = true;
            }
            else
            {
                addViewLine("Selecione um equipamento.");
            }
        }


        /*=======================================================================
         * 
         *                  ROTINAS DE COMUNICA��O COM O KERNEL
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

            //Detecta a quantidade de registros offline no rel�gio
            int regQtty = kernel7x.get_RecebeQtRegistros(pThreadIndex);

            //Verifica c�digo de erro da �ltima opera��o
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

            //Verificando se h� registros para coletar...
            if (getRegistryQtty(pThreadIndex) > 0)
            {

                while (kernel7x.get_RecebePacote(pThreadIndex))
                {
                    SRegistro registro;
                    int qtpacote = kernel7x.get_QuantRegsColetados(pThreadIndex);
                    for (int i = 0; i < qtpacote; i++)
                    {
                        getRegistroGeneric(pThreadIndex, out registro, false);
                        txtMemo.Invoke(viewL, txtMemo, "Matr�cula: " + registro.Matricula);
                        txtMemo.Invoke(viewL, txtMemo, "Data e Hora: " + registro.DataHora);
                    }
                    txtMemo.Invoke(viewL, txtMemo, "Excluindo pacote...");
                    if (kernel7x.get_ApagaUltimoPacote(pThreadIndex))
                    {
                        txtMemo.Invoke(viewL, txtMemo, "Pacote exclu�do");
                    }

                    txtMemo.Invoke(viewL, txtMemo, "Recebendo pacote " + pThreadIndex);
                }


                txtMemo.Invoke(viewL, txtMemo, "Coleta FINALIZADA");
            }
            else
            {
                txtMemo.Invoke(viewL, txtMemo, "N�o h� registros para coletar");
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
                txtMemo.Invoke(viewL, txtMemo, "H� " + pCount + " registros em " + pThreadIndex);

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
                txtMemo.Invoke(viewL, txtMemo, "Exce��o: " + e.Message);
            }
        }




        private void onlineRegistryEventHandler(int pThreadIndex)
        {
            if (this.eventIndex == -1)
            {
                this.eventIndex = pThreadIndex;
                Thread regThread = new Thread(this.onlineRegistryEventHandlerThd);
                regThread.Start();

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
                //equipamentos da fam�lia 7x
                //
                //ATEN��O
                //
                //Esta rotina � um evento do pr�prio kernel = Evento (OnRegistro)
                //====================================================================================

                // Reset currPerson object
                _currPerson = null;

                //Recebe solicita��o do kernel
                SRegistro registro;
                /*
                 * Vari�veis do Sregistros
                 */
                bool Saida = false;
                bool MasterLiberou = false;
                bool FuncaoLiberou = false;
                bool AcessoNegado = false;

                txtMemo.Invoke(viewL, txtMemo, "Evento online recebido : " + pThreadIndex);

                //Seu tratamento de acesso e ponto
                SResposta resposta = new SResposta();

                //Recebe do rel�gio ou catraca em tempo real
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

                // 
                GetPersonById(registro.Matricula);

                if (txtMemo.InvokeRequired)
                {
                    if (registro.Flag == SFlagRegistro.sfrGirou)
                    {
                        /*
                         * ============================================================
                         * Neste if quando catraca, vem a flag de giro informando se a 
                         * catraca girou ou n�o
                         * 
                         * OBSERVA��ES
                         * n�o � necess�rio enviar outra mensagem para catraca aqui
                         * est� apenas para informa��o de exemplo, suas rotinas
                         * neste if pode ser para outras afinidades ex: 
                         * grava��o em banco, montagem de log etc....
                         * ============================================================
                         */

                        txtMemo.Invoke(viewL, txtMemo, "Matr�cula: " + registro.Matricula);
                    }
                    else
                    {
                        txtMemo.Invoke(viewL, txtMemo, "Matr�cula: " + registro.Matricula);
                    }
                }
                //if (cbxAcesso.InvokeRequired)
                //{
                //    IndexCombo combo = new IndexCombo(this.indexComboBox);
                //    cbxAcesso.Invoke(combo, cbxAcesso);
                //}
                //indexCombo = 1;

                if (ckbRespostaAutomatica.Checked)
                {
                    if (!string.IsNullOrEmpty(registro.Matricula))
                    {
                        int matricula = int.Parse(registro.Matricula);
                        Person person = new Person();

                        PersonManager personManager = new PersonManager();
                        person = personManager.FindById(matricula).Data;

                        if (person != null)
                        {
                            switch (person.Status)
                            {
                                case Entity.Enumerates.PersonStatus.Inactive:
                                    resposta.Acesso = SAcessoOnline.canNegado;
                                    break;
                                case Entity.Enumerates.PersonStatus.Active:
                                    resposta.Acesso = SAcessoOnline.canAmbosLados;
                                    break;
                                case Entity.Enumerates.PersonStatus.Blocked:
                                    resposta.Acesso = SAcessoOnline.canNegado;
                                    break;
                                default:
                                    break;
                            }
                        }
                        
                    }
                }
                else
                {
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

            }
            catch (Exception e)
            {
                txtMemo.Invoke(viewL, txtMemo, "Exce��o: " + e.Message);
            }

            eventIndex = -1;
        }

        private void rbOnline_CheckedChanged(object sender, EventArgs e)
        {
            // Update combobox with equipaments inserted in database
            if (rbOnline.Checked)
            {
                GetAllAccessControl();
                EnableDisableFields(false);
            }
        }

        private void rbOffline_CheckedChanged(object sender, EventArgs e)
        {
            // Clear combobox equipaments 
            if (rbOffline.Checked)
            {
                cbxEquipments.Items.Clear();
                EnableDisableFields(true);
            }
        }

        private void EnableDisableFields(bool status)
        {
            cbxAcesso.Enabled = status;
            numTempo.Enabled = status;
            emensagem.Enabled = status;
            eIp.Enabled = status;
            btnAdicionar.Enabled = status;
        }

        private async void GetAllAccessControl()
        {
            string URI = "http://localhost/webapi/accesscontrol/getall";
            IEnumerable<AccessControl> accessControlList = null;

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(URI))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        accessControlList = JsonConvert.DeserializeObject<AccessControl[]>(jsonString).ToList();
                    }
                    else
                    {
                        MessageBox.Show("N�o foi poss�vel obter acesso aos equipamentos cadastrados: " + response.StatusCode);
                    }
                }
            }

            //Exemplo de adi��o de equipamento no Kernel 7x
            //Montagem da configura��o de comunica�ao
            foreach(var accessControl in accessControlList)
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
                    addViewLine("Rel�gio TcpIp adicionado: " + accessControl.Name);
                }
                else
                {
                    addViewLine("Falha: " +
                        kernel7x.ErrorDescription(kernel7x.KernelLastError));
                }
            }
        }

        private async void GetPersonById(string matricula)
        {
            int id;
            
            if (Int32.TryParse(matricula, out id))
            {
                string URI = "http://localhost/webapi/person/getbyid/?id=" + id;
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync(URI))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonString = await response.Content.ReadAsStringAsync();
                            _currPerson = JsonConvert.DeserializeObject<Person>(jsonString);
                            _gateConfig = _currPerson.GetGateConfig();
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetPersonById("870");

            int matricula = int.Parse("870");
            Person person = new Person();

            PersonManager personManager = new PersonManager();
            person = personManager.FindById(matricula).Data;

            //MessageBox.Show("GateMessage : " + _gateConfig.GateMessage);
            //MessageBox.Show("GateStatus : " + _gateConfig.GateStatus);
        }
    }
}