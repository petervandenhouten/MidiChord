namespace Sanford.Multimedia.Midi.UI
{
    partial class BeatDialog
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
            this.inputLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this._numericBpm = new System.Windows.Forms.NumericUpDown();
            this.buttonTap = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._numericBpm)).BeginInit();
            this.SuspendLayout();
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Location = new System.Drawing.Point(52, 9);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(115, 13);
            this.inputLabel.TabIndex = 16;
            this.inputLabel.Text = "Beats per minute (bpm)";
            // 
            // cancelButton
            // 
            this.cancelButton.AccessibleDescription = "The cancel button";
            this.cancelButton.AccessibleName = "Cancel";
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(127, 79);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 15;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.AccessibleDescription = "The okay button.";
            this.okButton.AccessibleName = "Okay";
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(16, 79);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 14;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // _numericBpm
            // 
            this._numericBpm.Location = new System.Drawing.Point(33, 35);
            this._numericBpm.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._numericBpm.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numericBpm.Name = "_numericBpm";
            this._numericBpm.Size = new System.Drawing.Size(75, 20);
            this._numericBpm.TabIndex = 17;
            this._numericBpm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._numericBpm.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buttonTap
            // 
            this.buttonTap.Location = new System.Drawing.Point(114, 35);
            this.buttonTap.Name = "buttonTap";
            this.buttonTap.Size = new System.Drawing.Size(75, 23);
            this.buttonTap.TabIndex = 18;
            this.buttonTap.Text = "tap";
            this.buttonTap.UseVisualStyleBackColor = true;
            this.buttonTap.Click += new System.EventHandler(this.buttonTap_Click);
            // 
            // BeatDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 114);
            this.Controls.Add(this.buttonTap);
            this.Controls.Add(this._numericBpm);
            this.Controls.Add(this.inputLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BeatDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Beats per minute";
            ((System.ComponentModel.ISupportInitialize)(this._numericBpm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.NumericUpDown _numericBpm;
        private System.Windows.Forms.Button buttonTap;
    }
}