﻿namespace Oilfield
{
    partial class WorldCreationForm
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
            this.worldWidthTB = new System.Windows.Forms.TextBox();
            this.worldHeightTB = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.IterationsTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.IterationsErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.WidthErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.HeightErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.IterationsErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // worldWidthTB
            // 
            this.worldWidthTB.Location = new System.Drawing.Point(13, 13);
            this.worldWidthTB.Name = "worldWidthTB";
            this.worldWidthTB.Size = new System.Drawing.Size(100, 20);
            this.worldWidthTB.TabIndex = 0;
            this.worldWidthTB.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            this.worldWidthTB.Leave += new System.EventHandler(this.WorldWidthTB_Leave);
            // 
            // worldHeightTB
            // 
            this.worldHeightTB.Location = new System.Drawing.Point(13, 39);
            this.worldHeightTB.Name = "worldHeightTB";
            this.worldHeightTB.Size = new System.Drawing.Size(100, 20);
            this.worldHeightTB.TabIndex = 1;
            this.worldHeightTB.Leave += new System.EventHandler(this.WorldHeightTB_Leave);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(13, 113);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(94, 113);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(132, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "World width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "World height";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(13, 65);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(64, 17);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Training";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged_1);
            // 
            // IterationsTextBox
            // 
            this.IterationsTextBox.Enabled = false;
            this.IterationsTextBox.Location = new System.Drawing.Point(13, 87);
            this.IterationsTextBox.Name = "IterationsTextBox";
            this.IterationsTextBox.Size = new System.Drawing.Size(100, 20);
            this.IterationsTextBox.TabIndex = 7;
            this.IterationsTextBox.Text = "5000";
            this.IterationsTextBox.Leave += new System.EventHandler(this.TextBox1_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(132, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Iterations";
            // 
            // IterationsErrorProvider
            // 
            this.IterationsErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.IterationsErrorProvider.ContainerControl = this;
            // 
            // WidthErrorProvider
            // 
            this.WidthErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.WidthErrorProvider.ContainerControl = this;
            // 
            // HeightErrorProvider
            // 
            this.HeightErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.HeightErrorProvider.ContainerControl = this;
            // 
            // WorldCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 148);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.IterationsTextBox);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.worldHeightTB);
            this.Controls.Add(this.worldWidthTB);
            this.Name = "WorldCreationForm";
            this.Text = "WorldCreationForm";
            this.Load += new System.EventHandler(this.WorldCreationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.IterationsErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox worldWidthTB;
        private System.Windows.Forms.TextBox worldHeightTB;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox IterationsTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ErrorProvider IterationsErrorProvider;
        private System.Windows.Forms.ErrorProvider WidthErrorProvider;
        private System.Windows.Forms.ErrorProvider HeightErrorProvider;
    }
}