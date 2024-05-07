namespace Final_Projesi_İki_Ders_Bir_Arada_
{
    partial class veritabani_ayari
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
            this.karsilama_label = new System.Windows.Forms.Label();
            this.tel_no_label = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.giris_buton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // karsilama_label
            // 
            this.karsilama_label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.karsilama_label.AutoSize = true;
            this.karsilama_label.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.karsilama_label.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.karsilama_label.Location = new System.Drawing.Point(76, 9);
            this.karsilama_label.Name = "karsilama_label";
            this.karsilama_label.Size = new System.Drawing.Size(164, 28);
            this.karsilama_label.TabIndex = 1;
            this.karsilama_label.Text = "VERİTABANINIZ";
            // 
            // tel_no_label
            // 
            this.tel_no_label.AutoSize = true;
            this.tel_no_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tel_no_label.ForeColor = System.Drawing.Color.DarkGray;
            this.tel_no_label.Location = new System.Drawing.Point(43, 52);
            this.tel_no_label.Name = "tel_no_label";
            this.tel_no_label.Size = new System.Drawing.Size(241, 19);
            this.tel_no_label.TabIndex = 2;
            this.tel_no_label.Text = "Lütfen Server name isminizi seçiniz";
            this.tel_no_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.radioButton1.Location = new System.Drawing.Point(12, 97);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(89, 17);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Bilgisayar ismi";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.radioButton2.Location = new System.Drawing.Point(140, 97);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(165, 17);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Bilgisayar ismi\\SQLEXPRESS";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // giris_buton
            // 
            this.giris_buton.BackColor = System.Drawing.Color.LimeGreen;
            this.giris_buton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.giris_buton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.giris_buton.Location = new System.Drawing.Point(95, 138);
            this.giris_buton.Name = "giris_buton";
            this.giris_buton.Size = new System.Drawing.Size(103, 38);
            this.giris_buton.TabIndex = 5;
            this.giris_buton.Text = "DEVAM ET";
            this.giris_buton.UseVisualStyleBackColor = false;
            this.giris_buton.Click += new System.EventHandler(this.giris_buton_Click);
            // 
            // veritabani_ayari
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(41)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(317, 188);
            this.Controls.Add(this.giris_buton);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.tel_no_label);
            this.Controls.Add(this.karsilama_label);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "veritabani_ayari";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "veritabani_ayari";
            this.Load += new System.EventHandler(this.veritabani_ayari_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label karsilama_label;
        private System.Windows.Forms.Label tel_no_label;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Button giris_buton;
    }
}