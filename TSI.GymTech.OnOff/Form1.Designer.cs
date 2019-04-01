namespace TSI.GymTech.OnOff
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxAcesso = new System.Windows.Forms.ComboBox();
            this.numTempo = new System.Windows.Forms.NumericUpDown();
            this.emensagem = new System.Windows.Forms.TextBox();
            this.lblAcesso = new System.Windows.Forms.Label();
            this.lblTempo = new System.Windows.Forms.Label();
            this.lblMensagem = new System.Windows.Forms.Label();
            this.btnColetar = new System.Windows.Forms.Button();
            this.btnQuantidade = new System.Windows.Forms.Button();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.eIp = new System.Windows.Forms.TextBox();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabTcpIp = new System.Windows.Forms.TabPage();
            this.cbxEquipments = new System.Windows.Forms.ComboBox();
            this.ckbRespostaAutomatica = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTempo)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabTcpIp.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxAcesso);
            this.groupBox1.Controls.Add(this.numTempo);
            this.groupBox1.Controls.Add(this.emensagem);
            this.groupBox1.Controls.Add(this.lblAcesso);
            this.groupBox1.Controls.Add(this.lblTempo);
            this.groupBox1.Controls.Add(this.lblMensagem);
            this.groupBox1.Location = new System.Drawing.Point(15, 240);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(628, 164);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resposta personalizada";
            // 
            // cbxAcesso
            // 
            this.cbxAcesso.FormattingEnabled = true;
            this.cbxAcesso.Items.AddRange(new object[] {
            "Negado",
            "Libera Entrada",
            "Libera Saída",
            "Revista",
            "Lib Ambos os lados"});
            this.cbxAcesso.Location = new System.Drawing.Point(113, 124);
            this.cbxAcesso.Margin = new System.Windows.Forms.Padding(4);
            this.cbxAcesso.Name = "cbxAcesso";
            this.cbxAcesso.Size = new System.Drawing.Size(148, 24);
            this.cbxAcesso.TabIndex = 5;
            // 
            // numTempo
            // 
            this.numTempo.Location = new System.Drawing.Point(113, 78);
            this.numTempo.Margin = new System.Windows.Forms.Padding(4);
            this.numTempo.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numTempo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTempo.Name = "numTempo";
            this.numTempo.Size = new System.Drawing.Size(149, 22);
            this.numTempo.TabIndex = 4;
            this.numTempo.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // emensagem
            // 
            this.emensagem.Location = new System.Drawing.Point(113, 33);
            this.emensagem.Margin = new System.Windows.Forms.Padding(4);
            this.emensagem.Name = "emensagem";
            this.emensagem.Size = new System.Drawing.Size(148, 22);
            this.emensagem.TabIndex = 3;
            this.emensagem.Text = "Liberado";
            // 
            // lblAcesso
            // 
            this.lblAcesso.AutoSize = true;
            this.lblAcesso.Location = new System.Drawing.Point(29, 128);
            this.lblAcesso.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAcesso.Name = "lblAcesso";
            this.lblAcesso.Size = new System.Drawing.Size(54, 17);
            this.lblAcesso.TabIndex = 2;
            this.lblAcesso.Text = "Acesso";
            // 
            // lblTempo
            // 
            this.lblTempo.AutoSize = true;
            this.lblTempo.Location = new System.Drawing.Point(27, 78);
            this.lblTempo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTempo.Name = "lblTempo";
            this.lblTempo.Size = new System.Drawing.Size(52, 17);
            this.lblTempo.TabIndex = 1;
            this.lblTempo.Text = "Tempo";
            // 
            // lblMensagem
            // 
            this.lblMensagem.AutoSize = true;
            this.lblMensagem.Location = new System.Drawing.Point(27, 33);
            this.lblMensagem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMensagem.Name = "lblMensagem";
            this.lblMensagem.Size = new System.Drawing.Size(77, 17);
            this.lblMensagem.TabIndex = 0;
            this.lblMensagem.Text = "Mensagem";
            // 
            // btnColetar
            // 
            this.btnColetar.Location = new System.Drawing.Point(127, 205);
            this.btnColetar.Margin = new System.Windows.Forms.Padding(4);
            this.btnColetar.Name = "btnColetar";
            this.btnColetar.Size = new System.Drawing.Size(100, 28);
            this.btnColetar.TabIndex = 7;
            this.btnColetar.Text = "Coletar";
            this.btnColetar.UseVisualStyleBackColor = true;
            this.btnColetar.Click += new System.EventHandler(this.btnColetar_Click);
            // 
            // btnQuantidade
            // 
            this.btnQuantidade.Location = new System.Drawing.Point(19, 205);
            this.btnQuantidade.Margin = new System.Windows.Forms.Padding(4);
            this.btnQuantidade.Name = "btnQuantidade";
            this.btnQuantidade.Size = new System.Drawing.Size(100, 28);
            this.btnQuantidade.TabIndex = 6;
            this.btnQuantidade.Text = "Quantidade de Registros";
            this.btnQuantidade.UseVisualStyleBackColor = true;
            this.btnQuantidade.Click += new System.EventHandler(this.btnQuantidade_Click);
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(253, 31);
            this.btnAdicionar.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(100, 28);
            this.btnAdicionar.TabIndex = 5;
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "IP :";
            // 
            // eIp
            // 
            this.eIp.Location = new System.Drawing.Point(63, 31);
            this.eIp.Margin = new System.Windows.Forms.Padding(4);
            this.eIp.Name = "eIp";
            this.eIp.Size = new System.Drawing.Size(159, 22);
            this.eIp.TabIndex = 3;
            this.eIp.Text = "192.168.0.217";
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(15, 411);
            this.txtMemo.Margin = new System.Windows.Forms.Padding(4);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMemo.Size = new System.Drawing.Size(632, 358);
            this.txtMemo.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabTcpIp);
            this.tabControl1.Location = new System.Drawing.Point(15, 15);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(633, 182);
            this.tabControl1.TabIndex = 5;
            // 
            // tabTcpIp
            // 
            this.tabTcpIp.Controls.Add(this.label1);
            this.tabTcpIp.Controls.Add(this.eIp);
            this.tabTcpIp.Controls.Add(this.btnAdicionar);
            this.tabTcpIp.Location = new System.Drawing.Point(4, 25);
            this.tabTcpIp.Margin = new System.Windows.Forms.Padding(4);
            this.tabTcpIp.Name = "tabTcpIp";
            this.tabTcpIp.Padding = new System.Windows.Forms.Padding(4);
            this.tabTcpIp.Size = new System.Drawing.Size(625, 153);
            this.tabTcpIp.TabIndex = 0;
            this.tabTcpIp.Text = "TcpIp";
            this.tabTcpIp.UseVisualStyleBackColor = true;
            // 
            // cbxEquipments
            // 
            this.cbxEquipments.FormattingEnabled = true;
            this.cbxEquipments.Location = new System.Drawing.Point(235, 205);
            this.cbxEquipments.Margin = new System.Windows.Forms.Padding(4);
            this.cbxEquipments.Name = "cbxEquipments";
            this.cbxEquipments.Size = new System.Drawing.Size(209, 24);
            this.cbxEquipments.TabIndex = 8;
            // 
            // ckbRespostaAutomatica
            // 
            this.ckbRespostaAutomatica.AutoSize = true;
            this.ckbRespostaAutomatica.Location = new System.Drawing.Point(451, 207);
            this.ckbRespostaAutomatica.Name = "ckbRespostaAutomatica";
            this.ckbRespostaAutomatica.Size = new System.Drawing.Size(171, 21);
            this.ckbRespostaAutomatica.TabIndex = 9;
            this.ckbRespostaAutomatica.Text = "Resposta automatica?";
            this.ckbRespostaAutomatica.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 785);
            this.Controls.Add(this.ckbRespostaAutomatica);
            this.Controls.Add(this.cbxEquipments);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnColetar);
            this.Controls.Add(this.btnQuantidade);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exemplo de Coleta de Registros";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTempo)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabTcpIp.ResumeLayout(false);
            this.tabTcpIp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnColetar;
        private System.Windows.Forms.Button btnQuantidade;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox eIp;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabTcpIp;
        private System.Windows.Forms.ComboBox cbxAcesso;
        private System.Windows.Forms.NumericUpDown numTempo;
        private System.Windows.Forms.TextBox emensagem;
        private System.Windows.Forms.Label lblAcesso;
        private System.Windows.Forms.Label lblTempo;
        private System.Windows.Forms.Label lblMensagem;
        private System.Windows.Forms.ComboBox cbxEquipments;
        private System.Windows.Forms.CheckBox ckbRespostaAutomatica;
    }
}

