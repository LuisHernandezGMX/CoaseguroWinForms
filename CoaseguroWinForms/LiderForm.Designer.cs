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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gridCoaseguradoras = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPorcentajeGMX = new System.Windows.Forms.Label();
            this.lblParticipacionGMX = new System.Windows.Forms.Label();
            this.lblLimiteMaximoResponsabilidad = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPorcentajeCoaseguradoras = new System.Windows.Forms.Label();
            this.lblMontoCoaseguradoras = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.gridFee = new System.Windows.Forms.DataGridView();
            this.CompaniaCoaseguradoras = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PorcentajeCoaseguradoras = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MontoCoaseguradoras = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompaniasFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PorcentajeFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MontoFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCoaseguradoras)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridFee)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.lblLimiteMaximoResponsabilidad);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
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
            // gridCoaseguradoras
            // 
            this.gridCoaseguradoras.AllowUserToAddRows = false;
            this.gridCoaseguradoras.AllowUserToDeleteRows = false;
            this.gridCoaseguradoras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCoaseguradoras.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CompaniaCoaseguradoras,
            this.PorcentajeCoaseguradoras,
            this.MontoCoaseguradoras});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridCoaseguradoras.DefaultCellStyle = dataGridViewCellStyle1;
            this.gridCoaseguradoras.Location = new System.Drawing.Point(13, 20);
            this.gridCoaseguradoras.Name = "gridCoaseguradoras";
            this.gridCoaseguradoras.ReadOnly = true;
            this.gridCoaseguradoras.Size = new System.Drawing.Size(491, 176);
            this.gridCoaseguradoras.TabIndex = 0;
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
            this.tableLayoutPanel1.Controls.Add(this.lblParticipacionGMX, 2, 1);
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
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(243, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "GRUPO MEXICANO DE SEGUROS, S.A. de C.V.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(258, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "% Participación";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(359, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Monto de Participación";
            // 
            // lblPorcentajeGMX
            // 
            this.lblPorcentajeGMX.AutoSize = true;
            this.lblPorcentajeGMX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPorcentajeGMX.Location = new System.Drawing.Point(258, 21);
            this.lblPorcentajeGMX.Name = "lblPorcentajeGMX";
            this.lblPorcentajeGMX.Size = new System.Drawing.Size(27, 13);
            this.lblPorcentajeGMX.TabIndex = 3;
            this.lblPorcentajeGMX.Text = "70%";
            // 
            // lblParticipacionGMX
            // 
            this.lblParticipacionGMX.AutoSize = true;
            this.lblParticipacionGMX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParticipacionGMX.Location = new System.Drawing.Point(359, 21);
            this.lblParticipacionGMX.Name = "lblParticipacionGMX";
            this.lblParticipacionGMX.Size = new System.Drawing.Size(70, 13);
            this.lblParticipacionGMX.TabIndex = 4;
            this.lblParticipacionGMX.Text = "$ 700,000.00";
            // 
            // lblLimiteMaximoResponsabilidad
            // 
            this.lblLimiteMaximoResponsabilidad.AutoSize = true;
            this.lblLimiteMaximoResponsabilidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLimiteMaximoResponsabilidad.Location = new System.Drawing.Point(242, 43);
            this.lblLimiteMaximoResponsabilidad.Name = "lblLimiteMaximoResponsabilidad";
            this.lblLimiteMaximoResponsabilidad.Size = new System.Drawing.Size(76, 13);
            this.lblLimiteMaximoResponsabilidad.TabIndex = 3;
            this.lblLimiteMaximoResponsabilidad.Text = "$1,000,000.00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(242, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "LÍDER";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Límite Máximo de Responsabilidad al 100%";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tipo de Coaseguro";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.62185F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.37815F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 136F));
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblPorcentajeCoaseguradoras, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblMontoCoaseguradoras, 2, 0);
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
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "TOTAL DE LA PARTICIPACIÓN";
            // 
            // lblPorcentajeCoaseguradoras
            // 
            this.lblPorcentajeCoaseguradoras.AutoSize = true;
            this.lblPorcentajeCoaseguradoras.Location = new System.Drawing.Point(196, 0);
            this.lblPorcentajeCoaseguradoras.Name = "lblPorcentajeCoaseguradoras";
            this.lblPorcentajeCoaseguradoras.Size = new System.Drawing.Size(30, 13);
            this.lblPorcentajeCoaseguradoras.TabIndex = 1;
            this.lblPorcentajeCoaseguradoras.Text = "30%";
            // 
            // lblMontoCoaseguradoras
            // 
            this.lblMontoCoaseguradoras.AutoSize = true;
            this.lblMontoCoaseguradoras.Location = new System.Drawing.Point(357, 0);
            this.lblMontoCoaseguradoras.Name = "lblMontoCoaseguradoras";
            this.lblMontoCoaseguradoras.Size = new System.Drawing.Size(82, 13);
            this.lblMontoCoaseguradoras.TabIndex = 2;
            this.lblMontoCoaseguradoras.Text = "$ 300,000.00";
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridFee.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridFee.Location = new System.Drawing.Point(7, 20);
            this.gridFee.Name = "gridFee";
            this.gridFee.Size = new System.Drawing.Size(520, 152);
            this.gridFee.TabIndex = 0;
            this.gridFee.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gridFee_EditingControlShowing);
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
            // LiderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1049, 626);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LiderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Emisión - Tipo de Negociación";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCoaseguradoras)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridFee)).EndInit();
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
        private System.Windows.Forms.Label lblParticipacionGMX;
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
    }
}

