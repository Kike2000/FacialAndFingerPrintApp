namespace MicroSisPlani
{
    partial class Frm_Login
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Login));
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.ElDivider1 = new Klik.Windows.Forms.v1.EntryLib.ELDivider();
            this.pnl_titulo = new System.Windows.Forms.Panel();
            this.btn_Salir = new System.Windows.Forms.Button();
            this.txt_pass = new Klik.Windows.Forms.v1.EntryLib.ELEntryBox();
            this.txt_usu = new Klik.Windows.Forms.v1.EntryLib.ELEntryBox();
            this.btn_Aceptar = new Klik.Windows.Forms.v1.EntryLib.ELButton();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ElDivider1)).BeginInit();
            this.pnl_titulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_pass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_usu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Aceptar)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 20;
            this.bunifuElipse1.TargetControl = this;
            // 
            // ElDivider1
            // 
            this.ElDivider1.FadeStyle = Klik.Windows.Forms.v1.EntryLib.DividerFadeStyles.Center;
            this.ElDivider1.LineColor = System.Drawing.Color.Brown;
            this.ElDivider1.LineSize = 1;
            this.ElDivider1.Location = new System.Drawing.Point(306, 146);
            this.ElDivider1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ElDivider1.Name = "ElDivider1";
            this.ElDivider1.Size = new System.Drawing.Size(428, 28);
            this.ElDivider1.TabIndex = 22;
            this.ElDivider1.VisualStyle = Klik.Windows.Forms.v1.Common.ControlVisualStyles.Custom;
            // 
            // pnl_titulo
            // 
            this.pnl_titulo.BackColor = System.Drawing.Color.White;
            this.pnl_titulo.Controls.Add(this.btn_Salir);
            this.pnl_titulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_titulo.Location = new System.Drawing.Point(0, 0);
            this.pnl_titulo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnl_titulo.Name = "pnl_titulo";
            this.pnl_titulo.Size = new System.Drawing.Size(1056, 30);
            this.pnl_titulo.TabIndex = 23;
            this.pnl_titulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnl_titulo_MouseMove);
            // 
            // btn_Salir
            // 
            this.btn_Salir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Salir.FlatAppearance.BorderSize = 0;
            this.btn_Salir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Salir.ForeColor = System.Drawing.Color.White;
            this.btn_Salir.Image = ((System.Drawing.Image)(resources.GetObject("btn_Salir.Image")));
            this.btn_Salir.Location = new System.Drawing.Point(16, 4);
            this.btn_Salir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Salir.Name = "btn_Salir";
            this.btn_Salir.Size = new System.Drawing.Size(55, 26);
            this.btn_Salir.TabIndex = 464;
            this.btn_Salir.UseVisualStyleBackColor = true;
            this.btn_Salir.Click += new System.EventHandler(this.btn_Salir_Click);
            // 
            // txt_pass
            // 
            this.txt_pass.CaptionStyle.CaptionSize = 0;
            this.txt_pass.CaptionStyle.FlashStyle.PaintType = Klik.Windows.Forms.v1.Common.PaintTypes.Solid;
            this.txt_pass.CaptionStyle.FlashStyle.SolidColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(240)))), ((int)(((byte)(191)))));
            this.txt_pass.CaptionStyle.Office2007Scheme = Klik.Windows.Forms.v1.Common.Office2007Schemes.ModernBlack;
            this.txt_pass.CaptionStyle.TextStyle.ForeColor = System.Drawing.Color.White;
            this.txt_pass.EditBoxStyle.BackColor = System.Drawing.Color.White;
            this.txt_pass.EditBoxStyle.BorderStyle.BorderType = Klik.Windows.Forms.v1.Common.BorderTypes.None;
            this.txt_pass.EditBoxStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pass.EditBoxStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txt_pass.EditBoxStyle.Office2007Scheme = Klik.Windows.Forms.v1.Common.Office2007Schemes.ModernBlack;
            this.txt_pass.EditBoxStyle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_pass.Location = new System.Drawing.Point(322, 303);
            this.txt_pass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_pass.Name = "txt_pass";
            this.txt_pass.Size = new System.Drawing.Size(384, 42);
            this.txt_pass.TabIndex = 453;
            this.txt_pass.ValidationStyle.PasswordChar = '\0';
            this.txt_pass.ValidationStyle.UseSystemPasswordChar = true;
            this.txt_pass.Value = "";
            this.txt_pass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_pass_KeyDown);
            // 
            // txt_usu
            // 
            this.txt_usu.CaptionStyle.CaptionSize = 0;
            this.txt_usu.CaptionStyle.FlashStyle.PaintType = Klik.Windows.Forms.v1.Common.PaintTypes.Solid;
            this.txt_usu.CaptionStyle.FlashStyle.SolidColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(240)))), ((int)(((byte)(191)))));
            this.txt_usu.CaptionStyle.Office2007Scheme = Klik.Windows.Forms.v1.Common.Office2007Schemes.ModernBlack;
            this.txt_usu.CaptionStyle.TextStyle.ForeColor = System.Drawing.Color.White;
            this.txt_usu.EditBoxStyle.BackColor = System.Drawing.Color.White;
            this.txt_usu.EditBoxStyle.BorderStyle.BorderType = Klik.Windows.Forms.v1.Common.BorderTypes.None;
            this.txt_usu.EditBoxStyle.BorderStyle.GradientStartColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txt_usu.EditBoxStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_usu.EditBoxStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txt_usu.EditBoxStyle.Office2007Scheme = Klik.Windows.Forms.v1.Common.Office2007Schemes.ModernBlack;
            this.txt_usu.EditBoxStyle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_usu.Location = new System.Drawing.Point(322, 219);
            this.txt_usu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_usu.Name = "txt_usu";
            this.txt_usu.Size = new System.Drawing.Size(384, 42);
            this.txt_usu.TabIndex = 450;
            this.txt_usu.ValidationStyle.PasswordChar = '\0';
            this.txt_usu.Value = "";
            this.txt_usu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_usu_KeyDown);
            // 
            // btn_Aceptar
            // 
            this.btn_Aceptar.BorderStyle.EdgeRadius = 7;
            this.btn_Aceptar.BorderStyle.SmoothingMode = Klik.Windows.Forms.v1.Common.SmoothingModes.AntiAlias;
            this.btn_Aceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Aceptar.FlashStyle.PaintType = Klik.Windows.Forms.v1.Common.PaintTypes.Solid;
            this.btn_Aceptar.FlashStyle.SolidColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(240)))), ((int)(((byte)(191)))));
            this.btn_Aceptar.ForegroundImageStyle.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.btn_Aceptar.ForegroundImageStyle.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_Aceptar.Location = new System.Drawing.Point(366, 380);
            this.btn_Aceptar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Aceptar.Name = "btn_Aceptar";
            this.btn_Aceptar.Office2007Scheme = Klik.Windows.Forms.v1.Common.Office2007Schemes.ClassicBlack;
            this.btn_Aceptar.Size = new System.Drawing.Size(299, 52);
            this.btn_Aceptar.TabIndex = 458;
            this.btn_Aceptar.TextStyle.Text = "Iniciar Sesion";
            this.btn_Aceptar.TextStyle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_Aceptar.Click += new System.EventHandler(this.btn_Aceptar_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label1.Location = new System.Drawing.Point(410, 112);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(205, 29);
            this.Label1.TabIndex = 20;
            this.Label1.Text = "Ingrese los datos";
            // 
            // Label12
            // 
            this.Label12.BackColor = System.Drawing.Color.White;
            this.Label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label12.ForeColor = System.Drawing.Color.SkyBlue;
            this.Label12.Image = ((System.Drawing.Image)(resources.GetObject("Label12.Image")));
            this.Label12.Location = new System.Drawing.Point(332, 236);
            this.Label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(27, 25);
            this.Label12.TabIndex = 452;
            // 
            // Label13
            // 
            this.Label13.BackColor = System.Drawing.Color.White;
            this.Label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label13.ForeColor = System.Drawing.Color.SkyBlue;
            this.Label13.Image = ((System.Drawing.Image)(resources.GetObject("Label13.Image")));
            this.Label13.Location = new System.Drawing.Point(332, 320);
            this.Label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(27, 25);
            this.Label13.TabIndex = 454;
            // 
            // Frm_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1056, 559);
            this.Controls.Add(this.btn_Aceptar);
            this.Controls.Add(this.Label12);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.txt_pass);
            this.Controls.Add(this.txt_usu);
            this.Controls.Add(this.pnl_titulo);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.ElDivider1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Frm_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Frm_Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ElDivider1)).EndInit();
            this.pnl_titulo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txt_pass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_usu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Aceptar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        internal Klik.Windows.Forms.v1.EntryLib.ELButton btn_Aceptar;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.Label Label13;
        internal Klik.Windows.Forms.v1.EntryLib.ELEntryBox txt_pass;
        internal Klik.Windows.Forms.v1.EntryLib.ELEntryBox txt_usu;
        internal System.Windows.Forms.Panel pnl_titulo;
        internal Klik.Windows.Forms.v1.EntryLib.ELDivider ElDivider1;
        private System.Windows.Forms.Button btn_Salir;
        internal System.Windows.Forms.Label Label1;
    }
}