namespace FacialRecognition
{
    partial class Recognizer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Recognizer));
            this.button1 = new System.Windows.Forms.Button();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.Btn_Cerrar_TabPers = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(40, 134);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 55);
            this.button1.TabIndex = 0;
            this.button1.Text = "Abrir Camara";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // imageBox1
            // 
            this.imageBox1.Location = new System.Drawing.Point(198, 72);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(230, 215);
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(577, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Number of faces";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(617, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "0";
            this.label2.Visible = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(631, 41);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 9;
            this.button4.Text = "Back to start";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Btn_Cerrar_TabPers
            // 
            this.Btn_Cerrar_TabPers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Cerrar_TabPers.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.Btn_Cerrar_TabPers.FlatAppearance.BorderSize = 0;
            this.Btn_Cerrar_TabPers.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.Btn_Cerrar_TabPers.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightCoral;
            this.Btn_Cerrar_TabPers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCoral;
            this.Btn_Cerrar_TabPers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Cerrar_TabPers.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Cerrar_TabPers.Image")));
            this.Btn_Cerrar_TabPers.Location = new System.Drawing.Point(731, 0);
            this.Btn_Cerrar_TabPers.Name = "Btn_Cerrar_TabPers";
            this.Btn_Cerrar_TabPers.Size = new System.Drawing.Size(31, 34);
            this.Btn_Cerrar_TabPers.TabIndex = 435;
            this.Btn_Cerrar_TabPers.UseVisualStyleBackColor = true;
            this.Btn_Cerrar_TabPers.Click += new System.EventHandler(this.Btn_Cerrar_TabPers_Click);
            // 
            // Recognizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 356);
            this.Controls.Add(this.Btn_Cerrar_TabPers);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imageBox1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Recognizer";
            this.Text = "Recognizer";
            this.Load += new System.EventHandler(this.Recognizer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private Emgu.CV.UI.ImageBox imageBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button4;
        internal System.Windows.Forms.Button Btn_Cerrar_TabPers;
    }
}