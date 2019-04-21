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
            this.label2 = new System.Windows.Forms.Label();
            this.cbxEquipments = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.eIp = new System.Windows.Forms.TextBox();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.cbxAcesso = new System.Windows.Forms.ComboBox();
            this.numTempo = new System.Windows.Forms.NumericUpDown();
            this.emensagem = new System.Windows.Forms.TextBox();
            this.lblAcesso = new System.Windows.Forms.Label();
            this.lblTempo = new System.Windows.Forms.Label();
            this.lblMensagem = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.ckbRespostaAutomatica = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbOffline = new System.Windows.Forms.RadioButton();
            this.rbOnline = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTempo)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbxEquipments);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.eIp);
            this.groupBox1.Controls.Add(this.btnAdicionar);
            this.groupBox1.Controls.Add(this.cbxAcesso);
            this.groupBox1.Controls.Add(this.numTempo);
            this.groupBox1.Controls.Add(this.emensagem);
            this.groupBox1.Controls.Add(this.lblAcesso);
            this.groupBox1.Controls.Add(this.lblTempo);
            this.groupBox1.Controls.Add(this.lblMensagem);
            this.groupBox1.Location = new System.Drawing.Point(11, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(471, 133);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resposta personalizada";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(224, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Equipamento:";
            // 
            // cbxEquipments
            // 
            this.cbxEquipments.FormattingEnabled = true;
            this.cbxEquipments.Location = new System.Drawing.Point(301, 61);
            this.cbxEquipments.Name = "cbxEquipments";
            this.cbxEquipments.Size = new System.Drawing.Size(158, 21);
            this.cbxEquipments.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(224, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Endereço IP:";
            // 
            // eIp
            // 
            this.eIp.Location = new System.Drawing.Point(301, 27);
            this.eIp.Name = "eIp";
            this.eIp.Size = new System.Drawing.Size(83, 20);
            this.eIp.TabIndex = 6;
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(390, 26);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(69, 23);
            this.btnAdicionar.TabIndex = 8;
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
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
            this.cbxAcesso.Location = new System.Drawing.Point(85, 27);
            this.cbxAcesso.Name = "cbxAcesso";
            this.cbxAcesso.Size = new System.Drawing.Size(112, 21);
            this.cbxAcesso.TabIndex = 5;
            // 
            // numTempo
            // 
            this.numTempo.Location = new System.Drawing.Point(85, 62);
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
            this.numTempo.Size = new System.Drawing.Size(112, 20);
            this.numTempo.TabIndex = 4;
            this.numTempo.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // emensagem
            // 
            this.emensagem.Location = new System.Drawing.Point(85, 95);
            this.emensagem.Name = "emensagem";
            this.emensagem.Size = new System.Drawing.Size(374, 20);
            this.emensagem.TabIndex = 3;
            this.emensagem.Text = "Liberado";
            // 
            // lblAcesso
            // 
            this.lblAcesso.AutoSize = true;
            this.lblAcesso.Location = new System.Drawing.Point(17, 30);
            this.lblAcesso.Name = "lblAcesso";
            this.lblAcesso.Size = new System.Drawing.Size(45, 13);
            this.lblAcesso.TabIndex = 2;
            this.lblAcesso.Text = "Acesso:";
            // 
            // lblTempo
            // 
            this.lblTempo.AutoSize = true;
            this.lblTempo.Location = new System.Drawing.Point(15, 62);
            this.lblTempo.Name = "lblTempo";
            this.lblTempo.Size = new System.Drawing.Size(43, 13);
            this.lblTempo.TabIndex = 1;
            this.lblTempo.Text = "Tempo:";
            // 
            // lblMensagem
            // 
            this.lblMensagem.AutoSize = true;
            this.lblMensagem.Location = new System.Drawing.Point(17, 98);
            this.lblMensagem.Name = "lblMensagem";
            this.lblMensagem.Size = new System.Drawing.Size(62, 13);
            this.lblMensagem.TabIndex = 0;
            this.lblMensagem.Text = "Mensagem:";
            // 
            // txtMemo
            // 
            this.txtMemo.Enabled = false;
            this.txtMemo.Location = new System.Drawing.Point(11, 240);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMemo.Size = new System.Drawing.Size(471, 207);
            this.txtMemo.TabIndex = 4;
            // 
            // ckbRespostaAutomatica
            // 
            this.ckbRespostaAutomatica.AutoSize = true;
            this.ckbRespostaAutomatica.Location = new System.Drawing.Point(350, 79);
            this.ckbRespostaAutomatica.Margin = new System.Windows.Forms.Padding(2);
            this.ckbRespostaAutomatica.Name = "ckbRespostaAutomatica";
            this.ckbRespostaAutomatica.Size = new System.Drawing.Size(132, 17);
            this.ckbRespostaAutomatica.TabIndex = 9;
            this.ckbRespostaAutomatica.Text = "Resposta automatica?";
            this.ckbRespostaAutomatica.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbOffline);
            this.groupBox2.Controls.Add(this.rbOnline);
            this.groupBox2.Location = new System.Drawing.Point(13, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(158, 64);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tipo de Gerencimanto";
            // 
            // rbOffline
            // 
            this.rbOffline.AutoSize = true;
            this.rbOffline.Location = new System.Drawing.Point(83, 29);
            this.rbOffline.Name = "rbOffline";
            this.rbOffline.Size = new System.Drawing.Size(58, 17);
            this.rbOffline.TabIndex = 12;
            this.rbOffline.TabStop = true;
            this.rbOffline.Text = "Off-line";
            this.rbOffline.UseVisualStyleBackColor = true;
            this.rbOffline.CheckedChanged += new System.EventHandler(this.rbOffline_CheckedChanged);
            // 
            // rbOnline
            // 
            this.rbOnline.AutoSize = true;
            this.rbOnline.Location = new System.Drawing.Point(9, 29);
            this.rbOnline.Name = "rbOnline";
            this.rbOnline.Size = new System.Drawing.Size(58, 17);
            this.rbOnline.TabIndex = 11;
            this.rbOnline.TabStop = true;
            this.rbOnline.Text = "On-line";
            this.rbOnline.UseVisualStyleBackColor = true;
            this.rbOnline.CheckedChanged += new System.EventHandler(this.rbOnline_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(247, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 465);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ckbRespostaAutomatica);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exemplo de Coleta de Registros";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTempo)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.ComboBox cbxAcesso;
        private System.Windows.Forms.NumericUpDown numTempo;
        private System.Windows.Forms.TextBox emensagem;
        private System.Windows.Forms.Label lblAcesso;
        private System.Windows.Forms.Label lblTempo;
        private System.Windows.Forms.Label lblMensagem;
        private System.Windows.Forms.CheckBox ckbRespostaAutomatica;
        private System.Windows.Forms.ComboBox cbxEquipments;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox eIp;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbOnline;
        private System.Windows.Forms.RadioButton rbOffline;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}

