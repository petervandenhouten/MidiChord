﻿namespace Sanford.Multimedia.Midi.UI
{
    partial class InstrumentDialog
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
            this.outputLabel = new System.Windows.Forms.Label();
            this.instrumentComboBox = new System.Windows.Forms.ComboBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.buttonPreviewSound = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(13, 13);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(122, 13);
            this.outputLabel.TabIndex = 13;
            this.outputLabel.Text = "General MIDI Instrument";
            // 
            // instrumentComboBox
            // 
            this.instrumentComboBox.AccessibleDescription = "Select a General MIDI Instrument";
            this.instrumentComboBox.AccessibleName = "MIDI Instrument";
            this.instrumentComboBox.AccessibleRole = System.Windows.Forms.AccessibleRole.DropList;
            this.instrumentComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.instrumentComboBox.FormattingEnabled = true;
            this.instrumentComboBox.Location = new System.Drawing.Point(16, 29);
            this.instrumentComboBox.Name = "instrumentComboBox";
            this.instrumentComboBox.Size = new System.Drawing.Size(152, 21);
            this.instrumentComboBox.TabIndex = 12;
            // 
            // cancelButton
            // 
            this.cancelButton.AccessibleDescription = "The cancel button";
            this.cancelButton.AccessibleName = "Cancel";
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(127, 79);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 11;
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
            this.okButton.TabIndex = 10;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // buttonPreviewSound
            // 
            this.buttonPreviewSound.Image = global::MidiChord.Properties.Resources.Sound;
            this.buttonPreviewSound.Location = new System.Drawing.Point(174, 27);
            this.buttonPreviewSound.Name = "buttonPreviewSound";
            this.buttonPreviewSound.Size = new System.Drawing.Size(28, 23);
            this.buttonPreviewSound.TabIndex = 14;
            this.buttonPreviewSound.UseVisualStyleBackColor = true;
            this.buttonPreviewSound.Click += new System.EventHandler(this.buttonPreviewSound_Click);
            // 
            // InstrumentDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 114);
            this.Controls.Add(this.buttonPreviewSound);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.instrumentComboBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InstrumentDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "MIDI Instrument";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InstrumentDialog_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.ComboBox instrumentComboBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button buttonPreviewSound;
    }
}