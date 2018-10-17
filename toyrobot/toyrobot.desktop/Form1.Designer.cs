﻿namespace toyrobot.desktop
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Faces = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.lblReport = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(758, 144);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "Place";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Place);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(863, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "X";
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(854, 146);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(37, 22);
            this.txtX.TabIndex = 2;
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(906, 146);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(37, 22);
            this.txtY.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(912, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Y";
            // 
            // Faces
            // 
            this.Faces.FormattingEnabled = true;
            this.Faces.Location = new System.Drawing.Point(959, 144);
            this.Faces.Name = "Faces";
            this.Faces.Size = new System.Drawing.Size(121, 24);
            this.Faces.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(966, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "F";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(757, 202);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Move";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.MoveByFace);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(757, 263);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 26);
            this.button3.TabIndex = 8;
            this.button3.Text = "<< LEFT";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.TurnLeft);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(838, 263);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(88, 25);
            this.button4.TabIndex = 9;
            this.button4.Text = "RIGHT >>";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.TurnRight);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(758, 320);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 29);
            this.button5.TabIndex = 10;
            this.button5.Text = "Report";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.ReportPostion);
            // 
            // lblReport
            // 
            this.lblReport.AutoSize = true;
            this.lblReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReport.ForeColor = System.Drawing.Color.Blue;
            this.lblReport.Location = new System.Drawing.Point(758, 403);
            this.lblReport.Name = "lblReport";
            this.lblReport.Size = new System.Drawing.Size(0, 25);
            this.lblReport.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 692);
            this.Controls.Add(this.lblReport);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Faces);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Toy Robot";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Faces;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label lblReport;
    }
}

