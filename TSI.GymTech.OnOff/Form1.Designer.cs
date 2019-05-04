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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dtGridView = new System.Windows.Forms.DataGridView();
            this.PersonName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PersonId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccessType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MessageDisplayed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbOffline = new System.Windows.Forms.RadioButton();
            this.rbOnline = new System.Windows.Forms.RadioButton();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.eIp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxEquipments = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.cbxAcesso = new System.Windows.Forms.ComboBox();
            this.numTempo = new System.Windows.Forms.NumericUpDown();
            this.emensagem = new System.Windows.Forms.TextBox();
            this.lblAcesso = new System.Windows.Forms.Label();
            this.lblTempo = new System.Windows.Forms.Label();
            this.lblMensagem = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLogAdmin = new System.Windows.Forms.TextBox();
            this.txtMatricula = new System.Windows.Forms.TextBox();
            this.btnProcurar = new System.Windows.Forms.Button();
            this.lblMatricula = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridView)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTempo)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TSI.GymTech.OnOff.Properties.Resources.Login;
            this.pictureBox1.Location = new System.Drawing.Point(544, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(469, 548);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Location = new System.Drawing.Point(12, 59);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(514, 483);
            this.tabControl.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dtGridView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(506, 457);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Log de Acesso";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dtGridView
            // 
            this.dtGridView.AllowUserToAddRows = false;
            this.dtGridView.AllowUserToDeleteRows = false;
            this.dtGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PersonName,
            this.PersonId,
            this.AccessType,
            this.MessageDisplayed});
            this.dtGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtGridView.GridColor = System.Drawing.SystemColors.Window;
            this.dtGridView.Location = new System.Drawing.Point(3, 3);
            this.dtGridView.Name = "dtGridView";
            this.dtGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dtGridView.RowHeadersVisible = false;
            this.dtGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dtGridView.Size = new System.Drawing.Size(500, 451);
            this.dtGridView.TabIndex = 0;
            // 
            // PersonName
            // 
            this.PersonName.FillWeight = 125.5579F;
            this.PersonName.HeaderText = "Nome";
            this.PersonName.Name = "PersonName";
            // 
            // PersonId
            // 
            this.PersonId.FillWeight = 67.66593F;
            this.PersonId.HeaderText = "Matrícula";
            this.PersonId.Name = "PersonId";
            // 
            // AccessType
            // 
            this.AccessType.FillWeight = 81.21828F;
            this.AccessType.HeaderText = "Acesso";
            this.AccessType.Name = "AccessType";
            // 
            // MessageDisplayed
            // 
            this.MessageDisplayed.FillWeight = 125.5579F;
            this.MessageDisplayed.HeaderText = "Mensagem Exibida";
            this.MessageDisplayed.Name = "MessageDisplayed";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.txtMemo);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(506, 457);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Configurações";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbOffline);
            this.groupBox2.Controls.Add(this.rbOnline);
            this.groupBox2.Location = new System.Drawing.Point(10, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(159, 64);
            this.groupBox2.TabIndex = 17;
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
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(10, 234);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.ReadOnly = true;
            this.txtMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMemo.Size = new System.Drawing.Size(484, 213);
            this.txtMemo.TabIndex = 16;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.eIp);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbxEquipments);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnAdicionar);
            this.groupBox1.Controls.Add(this.cbxAcesso);
            this.groupBox1.Controls.Add(this.numTempo);
            this.groupBox1.Controls.Add(this.emensagem);
            this.groupBox1.Controls.Add(this.lblAcesso);
            this.groupBox1.Controls.Add(this.lblTempo);
            this.groupBox1.Controls.Add(this.lblMensagem);
            this.groupBox1.Location = new System.Drawing.Point(10, 86);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(484, 133);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resposta personalizada";
            // 
            // eIp
            // 
            this.eIp.Location = new System.Drawing.Point(296, 27);
            this.eIp.Name = "eIp";
            this.eIp.Size = new System.Drawing.Size(101, 20);
            this.eIp.TabIndex = 12;
            this.eIp.Text = "192.168.1.236";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Equipamento:";
            // 
            // cbxEquipments
            // 
            this.cbxEquipments.FormattingEnabled = true;
            this.cbxEquipments.Location = new System.Drawing.Point(296, 61);
            this.cbxEquipments.Name = "cbxEquipments";
            this.cbxEquipments.Size = new System.Drawing.Size(176, 21);
            this.cbxEquipments.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(219, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Endereço IP:";
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(403, 26);
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
            this.cbxAcesso.Location = new System.Drawing.Point(80, 27);
            this.cbxAcesso.Name = "cbxAcesso";
            this.cbxAcesso.Size = new System.Drawing.Size(121, 21);
            this.cbxAcesso.TabIndex = 5;
            // 
            // numTempo
            // 
            this.numTempo.Location = new System.Drawing.Point(80, 62);
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
            this.numTempo.Size = new System.Drawing.Size(121, 20);
            this.numTempo.TabIndex = 4;
            this.numTempo.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // emensagem
            // 
            this.emensagem.Location = new System.Drawing.Point(80, 95);
            this.emensagem.Name = "emensagem";
            this.emensagem.Size = new System.Drawing.Size(392, 20);
            this.emensagem.TabIndex = 3;
            this.emensagem.Text = "Liberado";
            // 
            // lblAcesso
            // 
            this.lblAcesso.AutoSize = true;
            this.lblAcesso.Location = new System.Drawing.Point(12, 30);
            this.lblAcesso.Name = "lblAcesso";
            this.lblAcesso.Size = new System.Drawing.Size(45, 13);
            this.lblAcesso.TabIndex = 2;
            this.lblAcesso.Text = "Acesso:";
            // 
            // lblTempo
            // 
            this.lblTempo.AutoSize = true;
            this.lblTempo.Location = new System.Drawing.Point(10, 62);
            this.lblTempo.Name = "lblTempo";
            this.lblTempo.Size = new System.Drawing.Size(43, 13);
            this.lblTempo.TabIndex = 1;
            this.lblTempo.Text = "Tempo:";
            // 
            // lblMensagem
            // 
            this.lblMensagem.AutoSize = true;
            this.lblMensagem.Location = new System.Drawing.Point(12, 98);
            this.lblMensagem.Name = "lblMensagem";
            this.lblMensagem.Size = new System.Drawing.Size(62, 13);
            this.lblMensagem.TabIndex = 0;
            this.lblMensagem.Text = "Mensagem:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.txtLogAdmin);
            this.tabPage3.Controls.Add(this.txtMatricula);
            this.tabPage3.Controls.Add(this.btnProcurar);
            this.tabPage3.Controls.Add(this.lblMatricula);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(506, 457);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Administração";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Log: ";
            // 
            // txtLogAdmin
            // 
            this.txtLogAdmin.Location = new System.Drawing.Point(76, 44);
            this.txtLogAdmin.Multiline = true;
            this.txtLogAdmin.Name = "txtLogAdmin";
            this.txtLogAdmin.ReadOnly = true;
            this.txtLogAdmin.Size = new System.Drawing.Size(404, 389);
            this.txtLogAdmin.TabIndex = 3;
            // 
            // txtMatricula
            // 
            this.txtMatricula.Location = new System.Drawing.Point(76, 18);
            this.txtMatricula.Name = "txtMatricula";
            this.txtMatricula.Size = new System.Drawing.Size(109, 20);
            this.txtMatricula.TabIndex = 2;
            // 
            // btnProcurar
            // 
            this.btnProcurar.Location = new System.Drawing.Point(191, 16);
            this.btnProcurar.Name = "btnProcurar";
            this.btnProcurar.Size = new System.Drawing.Size(75, 23);
            this.btnProcurar.TabIndex = 1;
            this.btnProcurar.Text = "Procurar";
            this.btnProcurar.UseVisualStyleBackColor = true;
            this.btnProcurar.Click += new System.EventHandler(this.btnProcurar_Click);
            // 
            // lblMatricula
            // 
            this.lblMatricula.AutoSize = true;
            this.lblMatricula.Location = new System.Drawing.Point(12, 21);
            this.lblMatricula.Name = "lblMatricula";
            this.lblMatricula.Size = new System.Drawing.Size(58, 13);
            this.lblMatricula.TabIndex = 0;
            this.lblMatricula.Text = "Matrícula: ";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(71, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(387, 37);
            this.lblTitle.TabIndex = 14;
            this.lblTitle.Text = "GymTech - Controle de Acessos";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1009, 547);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GymTech - Coleta de Registros";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridView)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTempo)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbOffline;
        private System.Windows.Forms.RadioButton rbOnline;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox eIp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxEquipments;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.ComboBox cbxAcesso;
        private System.Windows.Forms.NumericUpDown numTempo;
        private System.Windows.Forms.TextBox emensagem;
        private System.Windows.Forms.Label lblAcesso;
        private System.Windows.Forms.Label lblTempo;
        private System.Windows.Forms.Label lblMensagem;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dtGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn PersonName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PersonId;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccessType;
        private System.Windows.Forms.DataGridViewTextBoxColumn MessageDisplayed;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLogAdmin;
        private System.Windows.Forms.TextBox txtMatricula;
        private System.Windows.Forms.Button btnProcurar;
        private System.Windows.Forms.Label lblMatricula;
    }
}

