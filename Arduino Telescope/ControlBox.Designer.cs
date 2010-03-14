namespace ASCOM.Arduino
{
    partial class ControlBox
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
            this.buttonNorth = new System.Windows.Forms.Button();
            this.buttonSouth = new System.Windows.Forms.Button();
            this.buttonEast = new System.Windows.Forms.Button();
            this.buttonWest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonNorth
            // 
            this.buttonNorth.Location = new System.Drawing.Point(52, 12);
            this.buttonNorth.Name = "buttonNorth";
            this.buttonNorth.Size = new System.Drawing.Size(40, 40);
            this.buttonNorth.TabIndex = 0;
            this.buttonNorth.Text = "N";
            this.buttonNorth.UseVisualStyleBackColor = true;
            this.buttonNorth.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonNorth_Down);
            this.buttonNorth.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Halt);
            // 
            // buttonSouth
            // 
            this.buttonSouth.Location = new System.Drawing.Point(52, 112);
            this.buttonSouth.Name = "buttonSouth";
            this.buttonSouth.Size = new System.Drawing.Size(40, 40);
            this.buttonSouth.TabIndex = 0;
            this.buttonSouth.Text = "S";
            this.buttonSouth.UseVisualStyleBackColor = true;
            this.buttonSouth.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonSouth_Down);
            this.buttonSouth.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Halt);
            // 
            // buttonEast
            // 
            this.buttonEast.Location = new System.Drawing.Point(95, 62);
            this.buttonEast.Name = "buttonEast";
            this.buttonEast.Size = new System.Drawing.Size(40, 40);
            this.buttonEast.TabIndex = 0;
            this.buttonEast.Text = "E";
            this.buttonEast.UseVisualStyleBackColor = true;
            this.buttonEast.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonEast_Down);
            this.buttonEast.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Halt);
            // 
            // buttonWest
            // 
            this.buttonWest.Location = new System.Drawing.Point(9, 62);
            this.buttonWest.Name = "buttonWest";
            this.buttonWest.Size = new System.Drawing.Size(40, 40);
            this.buttonWest.TabIndex = 0;
            this.buttonWest.Text = "W";
            this.buttonWest.UseVisualStyleBackColor = true;
            this.buttonWest.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonWest_Down);
            this.buttonWest.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Halt);
            // 
            // ControlBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(144, 228);
            this.Controls.Add(this.buttonWest);
            this.Controls.Add(this.buttonEast);
            this.Controls.Add(this.buttonSouth);
            this.Controls.Add(this.buttonNorth);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ControlBox";
            this.Text = "Control Box";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        

        #endregion

        private System.Windows.Forms.Button buttonNorth;
        private System.Windows.Forms.Button buttonSouth;
        private System.Windows.Forms.Button buttonEast;
        private System.Windows.Forms.Button buttonWest;

        private ASCOM.Arduino.Telescope telescope;
    }
}