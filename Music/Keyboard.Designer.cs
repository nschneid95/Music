
namespace Music
{
    partial class Keyboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
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
        private void InitializeComponent() {
            this.keyLabel = new System.Windows.Forms.Label();
            this.keyBox = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // keyLabel
            // 
            this.keyLabel.AutoSize = true;
            this.keyLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.keyLabel.Location = new System.Drawing.Point(42, 43);
            this.keyLabel.Name = "keyLabel";
            this.keyLabel.Size = new System.Drawing.Size(54, 30);
            this.keyLabel.TabIndex = 0;
            this.keyLabel.Text = "Key:";
            // 
            // keyBox
            // 
            this.keyBox.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.keyBox.FormattingEnabled = true;
            this.keyBox.Items.AddRange(new object[] {
            "440"});
            this.keyBox.Location = new System.Drawing.Point(102, 44);
            this.keyBox.Name = "keyBox";
            this.keyBox.Size = new System.Drawing.Size(215, 38);
            this.keyBox.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Music.Buttons.play;
            this.pictureBox1.Location = new System.Drawing.Point(388, 44);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(109, 63);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Keyboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1557, 664);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.keyBox);
            this.Controls.Add(this.keyLabel);
            this.Name = "Keyboard";
            this.Text = "Keyboard";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label keyLabel;
        private System.Windows.Forms.ComboBox keyBox;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}