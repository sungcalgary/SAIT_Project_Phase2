namespace Project2_Team5
{
    partial class frmUpdateProduct
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
            this.txtProdID = new System.Windows.Forms.TextBox();
            this.lbSupNameAdded = new System.Windows.Forms.ListBox();
            this.btnAddProd = new System.Windows.Forms.Button();
            this.btnRemoveProd = new System.Windows.Forms.Button();
            this.lbSupName = new System.Windows.Forms.ListBox();
            this.lblProdID = new System.Windows.Forms.Label();
            this.lblProdName = new System.Windows.Forms.Label();
            this.txtProdName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtProdID
            // 
            this.txtProdID.BackColor = System.Drawing.SystemColors.Window;
            this.txtProdID.Enabled = false;
            this.txtProdID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProdID.Location = new System.Drawing.Point(144, 24);
            this.txtProdID.Name = "txtProdID";
            this.txtProdID.ReadOnly = true;
            this.txtProdID.Size = new System.Drawing.Size(100, 26);
            this.txtProdID.TabIndex = 0;
            // 
            // lbSupNameAdded
            // 
            this.lbSupNameAdded.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSupNameAdded.FormattingEnabled = true;
            this.lbSupNameAdded.ItemHeight = 20;
            this.lbSupNameAdded.Location = new System.Drawing.Point(560, 96);
            this.lbSupNameAdded.Name = "lbSupNameAdded";
            this.lbSupNameAdded.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbSupNameAdded.Size = new System.Drawing.Size(300, 184);
            this.lbSupNameAdded.TabIndex = 4;
            // 
            // btnAddProd
            // 
            this.btnAddProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddProd.Location = new System.Drawing.Point(464, 96);
            this.btnAddProd.Name = "btnAddProd";
            this.btnAddProd.Size = new System.Drawing.Size(80, 30);
            this.btnAddProd.TabIndex = 5;
            this.btnAddProd.Text = "Add";
            this.btnAddProd.UseVisualStyleBackColor = true;
            this.btnAddProd.Click += new System.EventHandler(this.btnAddProd_Click);
            // 
            // btnRemoveProd
            // 
            this.btnRemoveProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveProd.Location = new System.Drawing.Point(464, 248);
            this.btnRemoveProd.Name = "btnRemoveProd";
            this.btnRemoveProd.Size = new System.Drawing.Size(80, 30);
            this.btnRemoveProd.TabIndex = 6;
            this.btnRemoveProd.Text = "Remove";
            this.btnRemoveProd.UseVisualStyleBackColor = true;
            this.btnRemoveProd.Click += new System.EventHandler(this.btnRemoveProd_Click);
            // 
            // lbSupName
            // 
            this.lbSupName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSupName.FormattingEnabled = true;
            this.lbSupName.ItemHeight = 20;
            this.lbSupName.Location = new System.Drawing.Point(144, 96);
            this.lbSupName.Name = "lbSupName";
            this.lbSupName.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbSupName.Size = new System.Drawing.Size(300, 184);
            this.lbSupName.TabIndex = 7;
            // 
            // lblProdID
            // 
            this.lblProdID.AutoSize = true;
            this.lblProdID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProdID.Location = new System.Drawing.Point(24, 24);
            this.lblProdID.Name = "lblProdID";
            this.lblProdID.Size = new System.Drawing.Size(85, 20);
            this.lblProdID.TabIndex = 8;
            this.lblProdID.Text = "Product ID";
            // 
            // lblProdName
            // 
            this.lblProdName.AutoSize = true;
            this.lblProdName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProdName.Location = new System.Drawing.Point(24, 56);
            this.lblProdName.Name = "lblProdName";
            this.lblProdName.Size = new System.Drawing.Size(110, 20);
            this.lblProdName.TabIndex = 9;
            this.lblProdName.Text = "Product Name";
            // 
            // txtProdName
            // 
            this.txtProdName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProdName.Location = new System.Drawing.Point(144, 56);
            this.txtProdName.Name = "txtProdName";
            this.txtProdName.Size = new System.Drawing.Size(240, 26);
            this.txtProdName.TabIndex = 10;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(680, 312);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(776, 312);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(464, 224);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "<<<<<";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(488, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 20);
            this.label2.TabIndex = 17;
            this.label2.Text = ">>>>>";
            // 
            // frmUpdateProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(884, 361);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtProdName);
            this.Controls.Add(this.lblProdName);
            this.Controls.Add(this.lblProdID);
            this.Controls.Add(this.lbSupName);
            this.Controls.Add(this.btnRemoveProd);
            this.Controls.Add(this.btnAddProd);
            this.Controls.Add(this.lbSupNameAdded);
            this.Controls.Add(this.txtProdID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmUpdateProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TravelExperts - Products Panel";
            this.Load += new System.EventHandler(this.frmUpdateProduct_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtProdID;
        private System.Windows.Forms.ListBox lbSupNameAdded;
        private System.Windows.Forms.Button btnAddProd;
        private System.Windows.Forms.Button btnRemoveProd;
        private System.Windows.Forms.ListBox lbSupName;
        private System.Windows.Forms.Label lblProdID;
        private System.Windows.Forms.Label lblProdName;
        private System.Windows.Forms.TextBox txtProdName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}