using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Kernel7x;
using TSI.GymTech.Entity.Models;
using TSI.GymTech.Manager.EntityManagers;

namespace TSI.GymTech.OnOff
{

    public partial class Form1 : Form
    {

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
        }


        //BOTÕES DA INTERFACE
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            //Exemplo de adição de equipamento no Kernel 7x

            //Montagem da configuração de comunicaçao
            SComConfig _rConfig;
            _rConfig.IsCatraca = 0;

            _rConfig.ModoComunicacao = SModoComunicacao.cmcOnOff;
            _rConfig.TipoComunicacao = STipoComunicacao.ctcTcpIp;
            _rConfig.Tcp.Ip = eIp.Text;
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

                addViewLine("Relógio TcpIp adicionado: " + indexAux);
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
                btnQuantidade.Enabled = false;
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
                btnQuantidade.Enabled = true;
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
                btnColetar.Enabled = false;
                this.collectRegistry(index);
                btnColetar.Enabled = true;
            }
            else
            {
                addViewLine("Selecione um equipamento.");
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
                //equipamentos da família 7x
                //
                //ATENÇÃO
                //
                //Esta rotina é um evento do próprio kernel = Evento (OnRegistro)
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

                resposta.Mensagem = emensagem.Text;
                resposta.Tempo = Convert.ToByte(numTempo.Value);
                if (txtMemo.InvokeRequired)
                {
                    if (registro.Flag == SFlagRegistro.sfrGirou)
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

                        txtMemo.Invoke(viewL, txtMemo, "Matrícula: " + registro.Matricula);
                    }
                    else
                    {
                        txtMemo.Invoke(viewL, txtMemo, "Matrícula: " + registro.Matricula);
                    }
                }
                if (cbxAcesso.InvokeRequired)
                {
                    IndexCombo combo = new IndexCombo(this.indexComboBox);
                    cbxAcesso.Invoke(combo, cbxAcesso);
                }
                //indexCombo = 1;

                if (ckbRespostaAutomatica.Checked)
                {
                    if (!string.IsNullOrEmpty(registro.Matricula))
                    {
                        int matricula = int.Parse(registro.Matricula);
                        PersonManager personManager = new PersonManager();
                        Person person = new Person();

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
                txtMemo.Invoke(viewL, txtMemo, "Exceção: " + e.Message);
            }

            eventIndex = -1;
        }
    }
}