namespace DryerKiln
{
    partial class setUp
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
            this.buttonCaiDatDuLieu = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonCaiDatDuLieu
            // 
            this.buttonCaiDatDuLieu.Location = new System.Drawing.Point(12, 12);
            this.buttonCaiDatDuLieu.Name = "buttonCaiDatDuLieu";
            this.buttonCaiDatDuLieu.Size = new System.Drawing.Size(111, 23);
            this.buttonCaiDatDuLieu.TabIndex = 2;
            this.buttonCaiDatDuLieu.Text = "Cài Đặt Dữ Liệu";
            this.buttonCaiDatDuLieu.UseVisualStyleBackColor = true;
            this.buttonCaiDatDuLieu.Click += new System.EventHandler(this.buttonCaiDatDuLieu_Click);
            // 
            // setUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 58);
            this.Controls.Add(this.buttonCaiDatDuLieu);
            this.Name = "setUp";
            this.Text = "Cài Đặt";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCaiDatDuLieu;
    }
}