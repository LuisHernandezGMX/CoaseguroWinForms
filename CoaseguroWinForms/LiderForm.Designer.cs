namespace CoaseguroWinForms
{
    partial class LiderForm
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
            if (disposing && (components != null)) {
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPrimaNeta = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLimiteMaximoResponsabilidad = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblMontoCoaseguradoras = new System.Windows.Forms.Label();
            this.lblPorcentajeCoaseguradoras = new System.Windows.Forms.Label();
            this.gridCoaseguradoras = new System.Windows.Forms.DataGridView();
            this.CompaniaCoaseguradoras = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PorcentajeCoaseguradoras = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MontoCoaseguradoras = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPorcentajeGMX = new System.Windows.Forms.Label();
            this.lblMontoGMX = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.gridFee = new System.Windows.Forms.DataGridView();
            this.CompaniasFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PorcentajeFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MontoFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupMetodoPago = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.rdbEstadoCuenta = new System.Windows.Forms.RadioButton();
            this.groupComisionAgente = new System.Windows.Forms.GroupBox();
            this.rdbLider100 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.groupSiniestros = new System.Windows.Forms.GroupBox();
            this.lblMontoSiniestro = new System.Windows.Forms.Label();
            this.txtMontoSiniestro = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.rdbSiniestro100 = new System.Windows.Forms.RadioButton();
            this.rdbSiniestroParticipacion = new System.Windows.Forms.RadioButton();
            this.groupGarantiaPago = new System.Windows.Forms.GroupBox();
            this.cmbGarantiaPago = new System.Windows.Forms.ComboBox();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.rdbContratoSeguro = new System.Windows.Forms.RadioButton();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.btnAtras = new System.Windows.Forms.Button();
            this.btnSuspender = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCoaseguradoras)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridFee)).BeginInit();
            this.groupMetodoPago.SuspendLayout();
            this.groupComisionAgente.SuspendLayout();
            this.groupSiniestros.SuspendLayout();
            this.groupGarantiaPago.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(538, 411);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Características del Coaseguro";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.35802F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.64198F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel3.Controls.Add(this.lblPrimaNeta, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblLimiteMaximoResponsabilidad, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label9, 2, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(10, 20);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(518, 51);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // lblPrimaNeta
            // 
            this.lblPrimaNeta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPrimaNeta.AutoSize = true;
            this.lblPrimaNeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrimaNeta.Location = new System.Drawing.Point(405, 31);
            this.lblPrimaNeta.Name = "lblPrimaNeta";
            this.lblPrimaNeta.Size = new System.Drawing.Size(110, 13);
            this.lblPrimaNeta.TabIndex = 5;
            this.lblPrimaNeta.Text = "$ 0.00";
            this.lblPrimaNeta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tipo de Coaseguro";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLimiteMaximoResponsabilidad
            // 
            this.lblLimiteMaximoResponsabilidad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLimiteMaximoResponsabilidad.AutoSize = true;
            this.lblLimiteMaximoResponsabilidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLimiteMaximoResponsabilidad.Location = new System.Drawing.Point(129, 31);
            this.lblLimiteMaximoResponsabilidad.Name = "lblLimiteMaximoResponsabilidad";
            this.lblLimiteMaximoResponsabilidad.Size = new System.Drawing.Size(270, 13);
            this.lblLimiteMaximoResponsabilidad.TabIndex = 3;
            this.lblLimiteMaximoResponsabilidad.Text = "$ 0.00";
            this.lblLimiteMaximoResponsabilidad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(129, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(270, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Límite Máximo de Responsabilidad al 100%";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "LÍDER";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(405, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Prima Neta";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel2);
            this.groupBox3.Controls.Add(this.gridCoaseguradoras);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.ForeColor = System.Drawing.Color.Navy;
            this.groupBox3.Location = new System.Drawing.Point(10, 159);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(518, 240);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Coaseguradoras";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.62185F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.37815F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblMontoCoaseguradoras, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblPorcentajeCoaseguradoras, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(13, 203);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.05882F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(491, 24);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(193, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "TOTAL DE LA PARTICIPACIÓN";
            // 
            // lblMontoCoaseguradoras
            // 
            this.lblMontoCoaseguradoras.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblMontoCoaseguradoras.AutoSize = true;
            this.lblMontoCoaseguradoras.Location = new System.Drawing.Point(445, 5);
            this.lblMontoCoaseguradoras.Name = "lblMontoCoaseguradoras";
            this.lblMontoCoaseguradoras.Size = new System.Drawing.Size(43, 13);
            this.lblMontoCoaseguradoras.TabIndex = 2;
            this.lblMontoCoaseguradoras.Text = "$ 0.00";
            // 
            // lblPorcentajeCoaseguradoras
            // 
            this.lblPorcentajeCoaseguradoras.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPorcentajeCoaseguradoras.AutoSize = true;
            this.lblPorcentajeCoaseguradoras.Location = new System.Drawing.Point(339, 5);
            this.lblPorcentajeCoaseguradoras.Name = "lblPorcentajeCoaseguradoras";
            this.lblPorcentajeCoaseguradoras.Size = new System.Drawing.Size(23, 13);
            this.lblPorcentajeCoaseguradoras.TabIndex = 1;
            this.lblPorcentajeCoaseguradoras.Text = "0%";
            // 
            // gridCoaseguradoras
            // 
            this.gridCoaseguradoras.AllowUserToAddRows = false;
            this.gridCoaseguradoras.AllowUserToDeleteRows = false;
            this.gridCoaseguradoras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCoaseguradoras.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CompaniaCoaseguradoras,
            this.PorcentajeCoaseguradoras,
            this.MontoCoaseguradoras});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridCoaseguradoras.DefaultCellStyle = dataGridViewCellStyle7;
            this.gridCoaseguradoras.Location = new System.Drawing.Point(13, 20);
            this.gridCoaseguradoras.Name = "gridCoaseguradoras";
            this.gridCoaseguradoras.ReadOnly = true;
            this.gridCoaseguradoras.Size = new System.Drawing.Size(491, 176);
            this.gridCoaseguradoras.TabIndex = 0;
            // 
            // CompaniaCoaseguradoras
            // 
            this.CompaniaCoaseguradoras.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CompaniaCoaseguradoras.HeaderText = "Compañías Participantes";
            this.CompaniaCoaseguradoras.Name = "CompaniaCoaseguradoras";
            this.CompaniaCoaseguradoras.ReadOnly = true;
            this.CompaniaCoaseguradoras.Width = 158;
            // 
            // PorcentajeCoaseguradoras
            // 
            this.PorcentajeCoaseguradoras.HeaderText = "% Participación";
            this.PorcentajeCoaseguradoras.Name = "PorcentajeCoaseguradoras";
            this.PorcentajeCoaseguradoras.ReadOnly = true;
            // 
            // MontoCoaseguradoras
            // 
            this.MontoCoaseguradoras.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MontoCoaseguradoras.HeaderText = "Monto de Participación";
            this.MontoCoaseguradoras.Name = "MontoCoaseguradoras";
            this.MontoCoaseguradoras.ReadOnly = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.ForeColor = System.Drawing.Color.Navy;
            this.groupBox2.Location = new System.Drawing.Point(10, 77);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(518, 75);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "GMX Seguros";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.2F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.8F));
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblPorcentajeGMX, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblMontoGMX, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(500, 42);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(243, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "GRUPO MEXICANO DE SEGUROS, S.A. de C.V.";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(258, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "% Participación";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(359, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Monto de Participación";
            // 
            // lblPorcentajeGMX
            // 
            this.lblPorcentajeGMX.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPorcentajeGMX.AutoSize = true;
            this.lblPorcentajeGMX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPorcentajeGMX.Location = new System.Drawing.Point(343, 25);
            this.lblPorcentajeGMX.Name = "lblPorcentajeGMX";
            this.lblPorcentajeGMX.Size = new System.Drawing.Size(10, 13);
            this.lblPorcentajeGMX.TabIndex = 3;
            this.lblPorcentajeGMX.Text = "-";
            // 
            // lblMontoGMX
            // 
            this.lblMontoGMX.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblMontoGMX.AutoSize = true;
            this.lblMontoGMX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoGMX.Location = new System.Drawing.Point(460, 25);
            this.lblMontoGMX.Name = "lblMontoGMX";
            this.lblMontoGMX.Size = new System.Drawing.Size(37, 13);
            this.lblMontoGMX.TabIndex = 4;
            this.lblMontoGMX.Text = "$ 0.00";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.gridFee);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.Navy;
            this.groupBox4.Location = new System.Drawing.Point(13, 430);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(537, 189);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Fee por Administración";
            // 
            // gridFee
            // 
            this.gridFee.AllowUserToAddRows = false;
            this.gridFee.AllowUserToDeleteRows = false;
            this.gridFee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFee.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CompaniasFee,
            this.PorcentajeFee,
            this.MontoFee});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridFee.DefaultCellStyle = dataGridViewCellStyle8;
            this.gridFee.Location = new System.Drawing.Point(7, 20);
            this.gridFee.Name = "gridFee";
            this.gridFee.Size = new System.Drawing.Size(520, 152);
            this.gridFee.TabIndex = 0;
            this.gridFee.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gridFee_EditingControlShowing);
            // 
            // CompaniasFee
            // 
            this.CompaniasFee.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CompaniasFee.HeaderText = "Compañías Participante";
            this.CompaniasFee.Name = "CompaniasFee";
            this.CompaniasFee.ReadOnly = true;
            this.CompaniasFee.Width = 152;
            // 
            // PorcentajeFee
            // 
            this.PorcentajeFee.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.PorcentajeFee.HeaderText = "% Participación";
            this.PorcentajeFee.Name = "PorcentajeFee";
            this.PorcentajeFee.Width = 109;
            // 
            // MontoFee
            // 
            this.MontoFee.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MontoFee.HeaderText = "Monto de Participación";
            this.MontoFee.Name = "MontoFee";
            this.MontoFee.ReadOnly = true;
            // 
            // groupMetodoPago
            // 
            this.groupMetodoPago.Controls.Add(this.radioButton2);
            this.groupMetodoPago.Controls.Add(this.rdbEstadoCuenta);
            this.groupMetodoPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupMetodoPago.ForeColor = System.Drawing.Color.Navy;
            this.groupMetodoPago.Location = new System.Drawing.Point(557, 13);
            this.groupMetodoPago.Name = "groupMetodoPago";
            this.groupMetodoPago.Size = new System.Drawing.Size(360, 55);
            this.groupMetodoPago.TabIndex = 2;
            this.groupMetodoPago.TabStop = false;
            this.groupMetodoPago.Text = "Método de Pago";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(250, 20);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(94, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Por conceptos";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // rdbEstadoCuenta
            // 
            this.rdbEstadoCuenta.AutoSize = true;
            this.rdbEstadoCuenta.Checked = true;
            this.rdbEstadoCuenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbEstadoCuenta.Location = new System.Drawing.Point(7, 20);
            this.rdbEstadoCuenta.Name = "rdbEstadoCuenta";
            this.rdbEstadoCuenta.Size = new System.Drawing.Size(109, 17);
            this.rdbEstadoCuenta.TabIndex = 0;
            this.rdbEstadoCuenta.TabStop = true;
            this.rdbEstadoCuenta.Text = "Estado de cuenta";
            this.rdbEstadoCuenta.UseVisualStyleBackColor = true;
            this.rdbEstadoCuenta.CheckedChanged += new System.EventHandler(this.rdbEstadoCuenta_CheckedChanged);
            // 
            // groupComisionAgente
            // 
            this.groupComisionAgente.Controls.Add(this.rdbLider100);
            this.groupComisionAgente.Controls.Add(this.radioButton4);
            this.groupComisionAgente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupComisionAgente.ForeColor = System.Drawing.Color.Navy;
            this.groupComisionAgente.Location = new System.Drawing.Point(557, 74);
            this.groupComisionAgente.Name = "groupComisionAgente";
            this.groupComisionAgente.Size = new System.Drawing.Size(360, 60);
            this.groupComisionAgente.TabIndex = 3;
            this.groupComisionAgente.TabStop = false;
            this.groupComisionAgente.Text = "Pago de Comisión al Agente";
            // 
            // rdbLider100
            // 
            this.rdbLider100.AutoSize = true;
            this.rdbLider100.Checked = true;
            this.rdbLider100.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbLider100.Location = new System.Drawing.Point(6, 19);
            this.rdbLider100.Name = "rdbLider100";
            this.rdbLider100.Size = new System.Drawing.Size(90, 17);
            this.rdbLider100.TabIndex = 2;
            this.rdbLider100.TabStop = true;
            this.rdbLider100.Text = "Líder al 100%";
            this.rdbLider100.UseVisualStyleBackColor = true;
            this.rdbLider100.CheckedChanged += new System.EventHandler(this.rdbLider100_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton4.Location = new System.Drawing.Point(232, 19);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(122, 17);
            this.radioButton4.TabIndex = 3;
            this.radioButton4.Text = "Pago a participación";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // groupSiniestros
            // 
            this.groupSiniestros.Controls.Add(this.label10);
            this.groupSiniestros.Controls.Add(this.lblMontoSiniestro);
            this.groupSiniestros.Controls.Add(this.txtMontoSiniestro);
            this.groupSiniestros.Controls.Add(this.label8);
            this.groupSiniestros.Controls.Add(this.rdbSiniestro100);
            this.groupSiniestros.Controls.Add(this.rdbSiniestroParticipacion);
            this.groupSiniestros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupSiniestros.ForeColor = System.Drawing.Color.Navy;
            this.groupSiniestros.Location = new System.Drawing.Point(557, 141);
            this.groupSiniestros.Name = "groupSiniestros";
            this.groupSiniestros.Size = new System.Drawing.Size(360, 117);
            this.groupSiniestros.TabIndex = 4;
            this.groupSiniestros.TabStop = false;
            this.groupSiniestros.Text = "Siniestros";
            // 
            // lblMontoSiniestro
            // 
            this.lblMontoSiniestro.AutoSize = true;
            this.lblMontoSiniestro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoSiniestro.Location = new System.Drawing.Point(251, 85);
            this.lblMontoSiniestro.Name = "lblMontoSiniestro";
            this.lblMontoSiniestro.Size = new System.Drawing.Size(37, 13);
            this.lblMontoSiniestro.TabIndex = 2;
            this.lblMontoSiniestro.Text = "$ 0.00";
            // 
            // txtMontoSiniestro
            // 
            this.txtMontoSiniestro.Enabled = false;
            this.txtMontoSiniestro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontoSiniestro.Location = new System.Drawing.Point(161, 82);
            this.txtMontoSiniestro.Name = "txtMontoSiniestro";
            this.txtMontoSiniestro.Size = new System.Drawing.Size(64, 20);
            this.txtMontoSiniestro.TabIndex = 1;
            this.txtMontoSiniestro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMontoSiniestro_KeyPress);
            this.txtMontoSiniestro.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMontoSiniestro_KeyUp);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(24, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(131, 30);
            this.label8.TabIndex = 0;
            this.label8.Text = "Monto máximo para pago automático de siniestro";
            // 
            // rdbSiniestro100
            // 
            this.rdbSiniestro100.AutoSize = true;
            this.rdbSiniestro100.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbSiniestro100.Location = new System.Drawing.Point(7, 42);
            this.rdbSiniestro100.Name = "rdbSiniestro100";
            this.rdbSiniestro100.Size = new System.Drawing.Size(148, 17);
            this.rdbSiniestro100.TabIndex = 4;
            this.rdbSiniestro100.Text = "Pago del siniestro al 100%";
            this.rdbSiniestro100.UseVisualStyleBackColor = true;
            // 
            // rdbSiniestroParticipacion
            // 
            this.rdbSiniestroParticipacion.AutoSize = true;
            this.rdbSiniestroParticipacion.Checked = true;
            this.rdbSiniestroParticipacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbSiniestroParticipacion.Location = new System.Drawing.Point(7, 19);
            this.rdbSiniestroParticipacion.Name = "rdbSiniestroParticipacion";
            this.rdbSiniestroParticipacion.Size = new System.Drawing.Size(180, 17);
            this.rdbSiniestroParticipacion.TabIndex = 4;
            this.rdbSiniestroParticipacion.TabStop = true;
            this.rdbSiniestroParticipacion.Text = "Pago del siniestro a participación";
            this.rdbSiniestroParticipacion.UseVisualStyleBackColor = true;
            this.rdbSiniestroParticipacion.CheckedChanged += new System.EventHandler(this.rdbSiniestroParticipacion_CheckedChanged);
            // 
            // groupGarantiaPago
            // 
            this.groupGarantiaPago.Controls.Add(this.cmbGarantiaPago);
            this.groupGarantiaPago.Controls.Add(this.radioButton8);
            this.groupGarantiaPago.Controls.Add(this.rdbContratoSeguro);
            this.groupGarantiaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupGarantiaPago.ForeColor = System.Drawing.Color.Navy;
            this.groupGarantiaPago.Location = new System.Drawing.Point(557, 265);
            this.groupGarantiaPago.Name = "groupGarantiaPago";
            this.groupGarantiaPago.Size = new System.Drawing.Size(360, 102);
            this.groupGarantiaPago.TabIndex = 5;
            this.groupGarantiaPago.TabStop = false;
            this.groupGarantiaPago.Text = "Garantía de Pago";
            // 
            // cmbGarantiaPago
            // 
            this.cmbGarantiaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGarantiaPago.Enabled = false;
            this.cmbGarantiaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGarantiaPago.FormattingEnabled = true;
            this.cmbGarantiaPago.Location = new System.Drawing.Point(122, 43);
            this.cmbGarantiaPago.Name = "cmbGarantiaPago";
            this.cmbGarantiaPago.Size = new System.Drawing.Size(121, 21);
            this.cmbGarantiaPago.TabIndex = 2;
            this.cmbGarantiaPago.SelectedIndexChanged += new System.EventHandler(this.cmbGarantiaPago_SelectedIndexChanged);
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton8.Location = new System.Drawing.Point(8, 44);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(108, 17);
            this.radioButton8.TabIndex = 1;
            this.radioButton8.Text = "Otro (especificar):";
            this.radioButton8.UseVisualStyleBackColor = true;
            // 
            // rdbContratoSeguro
            // 
            this.rdbContratoSeguro.AutoSize = true;
            this.rdbContratoSeguro.Checked = true;
            this.rdbContratoSeguro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbContratoSeguro.Location = new System.Drawing.Point(7, 20);
            this.rdbContratoSeguro.Name = "rdbContratoSeguro";
            this.rdbContratoSeguro.Size = new System.Drawing.Size(259, 17);
            this.rdbContratoSeguro.TabIndex = 0;
            this.rdbContratoSeguro.TabStop = true;
            this.rdbContratoSeguro.Text = " De acuerdo a la Ley sobre el Contrato de Seguro";
            this.rdbContratoSeguro.UseVisualStyleBackColor = true;
            this.rdbContratoSeguro.CheckedChanged += new System.EventHandler(this.rdbContratoSeguro_CheckedChanged);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Location = new System.Drawing.Point(563, 430);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(75, 23);
            this.btnSiguiente.TabIndex = 6;
            this.btnSiguiente.Text = "Siguiente";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            // 
            // btnAtras
            // 
            this.btnAtras.Location = new System.Drawing.Point(679, 430);
            this.btnAtras.Name = "btnAtras";
            this.btnAtras.Size = new System.Drawing.Size(75, 23);
            this.btnAtras.TabIndex = 7;
            this.btnAtras.Text = "Atrás";
            this.btnAtras.UseVisualStyleBackColor = true;
            // 
            // btnSuspender
            // 
            this.btnSuspender.Location = new System.Drawing.Point(789, 430);
            this.btnSuspender.Name = "btnSuspender";
            this.btnSuspender.Size = new System.Drawing.Size(75, 23);
            this.btnSuspender.TabIndex = 8;
            this.btnSuspender.Text = "Suspender";
            this.btnSuspender.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(228, 85);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "%";
            // 
            // LiderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(940, 626);
            this.Controls.Add(this.btnSuspender);
            this.Controls.Add(this.btnAtras);
            this.Controls.Add(this.btnSiguiente);
            this.Controls.Add(this.groupGarantiaPago);
            this.Controls.Add(this.groupSiniestros);
            this.Controls.Add(this.groupComisionAgente);
            this.Controls.Add(this.groupMetodoPago);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LiderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Emisión - Tipo de Negociación";
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCoaseguradoras)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridFee)).EndInit();
            this.groupMetodoPago.ResumeLayout(false);
            this.groupMetodoPago.PerformLayout();
            this.groupComisionAgente.ResumeLayout(false);
            this.groupComisionAgente.PerformLayout();
            this.groupSiniestros.ResumeLayout(false);
            this.groupSiniestros.PerformLayout();
            this.groupGarantiaPago.ResumeLayout(false);
            this.groupGarantiaPago.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLimiteMaximoResponsabilidad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPorcentajeGMX;
        private System.Windows.Forms.Label lblMontoGMX;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView gridCoaseguradoras;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPorcentajeCoaseguradoras;
        private System.Windows.Forms.Label lblMontoCoaseguradoras;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView gridFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompaniaCoaseguradoras;
        private System.Windows.Forms.DataGridViewTextBoxColumn PorcentajeCoaseguradoras;
        private System.Windows.Forms.DataGridViewTextBoxColumn MontoCoaseguradoras;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompaniasFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn PorcentajeFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn MontoFee;
        private System.Windows.Forms.GroupBox groupMetodoPago;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton rdbEstadoCuenta;
        private System.Windows.Forms.GroupBox groupComisionAgente;
        private System.Windows.Forms.RadioButton rdbLider100;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.GroupBox groupSiniestros;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMontoSiniestro;
        private System.Windows.Forms.RadioButton rdbSiniestro100;
        private System.Windows.Forms.RadioButton rdbSiniestroParticipacion;
        private System.Windows.Forms.Label lblMontoSiniestro;
        private System.Windows.Forms.GroupBox groupGarantiaPago;
        private System.Windows.Forms.RadioButton rdbContratoSeguro;
        private System.Windows.Forms.ComboBox cmbGarantiaPago;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnAtras;
        private System.Windows.Forms.Button btnSuspender;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblPrimaNeta;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}

