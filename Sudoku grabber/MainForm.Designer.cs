namespace Sudoku_grabber
{
    partial class MainForm
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
            System.Windows.Forms.TabControl tabControl;
            System.Windows.Forms.TabPage tabPage1;
            this.imageBox = new Emgu.CV.UI.ImageBox();
            this.browseImageBtn = new System.Windows.Forms.Button();
            this.useWebcamBtn = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.captureBtn = new System.Windows.Forms.Button();
            tabControl = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            tabControl.SuspendLayout();
            tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageBox
            // 
            this.imageBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.imageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBox.Location = new System.Drawing.Point(6, 6);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(629, 513);
            this.imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imageBox.TabIndex = 2;
            this.imageBox.TabStop = false;
            // 
            // browseImageBtn
            // 
            this.browseImageBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseImageBtn.Location = new System.Drawing.Point(846, 3);
            this.browseImageBtn.Name = "browseImageBtn";
            this.browseImageBtn.Size = new System.Drawing.Size(139, 23);
            this.browseImageBtn.TabIndex = 3;
            this.browseImageBtn.Text = "Browse Image";
            this.browseImageBtn.UseVisualStyleBackColor = true;
            this.browseImageBtn.Click += new System.EventHandler(this.browseImageBtn_Click);
            // 
            // useWebcamBtn
            // 
            this.useWebcamBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.useWebcamBtn.Location = new System.Drawing.Point(991, 3);
            this.useWebcamBtn.Name = "useWebcamBtn";
            this.useWebcamBtn.Size = new System.Drawing.Size(139, 23);
            this.useWebcamBtn.TabIndex = 4;
            this.useWebcamBtn.Text = "Use Webcam";
            this.useWebcamBtn.UseVisualStyleBackColor = true;
            this.useWebcamBtn.Click += new System.EventHandler(this.useWebcamBtn_Click);
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPage1);
            tabControl.Controls.Add(this.tabPage2);
            tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl.Location = new System.Drawing.Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new System.Drawing.Size(1146, 553);
            tabControl.TabIndex = 5;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(this.captureBtn);
            tabPage1.Controls.Add(this.imageBox);
            tabPage1.Controls.Add(this.useWebcamBtn);
            tabPage1.Controls.Add(this.browseImageBtn);
            tabPage1.Location = new System.Drawing.Point(4, 22);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(1138, 527);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(991, 527);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // captureBtn
            // 
            this.captureBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.captureBtn.Location = new System.Drawing.Point(991, 32);
            this.captureBtn.Name = "captureBtn";
            this.captureBtn.Size = new System.Drawing.Size(139, 23);
            this.captureBtn.TabIndex = 5;
            this.captureBtn.Text = "Capture";
            this.captureBtn.UseVisualStyleBackColor = true;
            this.captureBtn.Click += new System.EventHandler(this.captureBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 553);
            this.Controls.Add(tabControl);
            this.Name = "MainForm";
            this.Text = "Sudoku grabber";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            tabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Emgu.CV.UI.ImageBox imageBox;
        private System.Windows.Forms.Button browseImageBtn;
        private System.Windows.Forms.Button useWebcamBtn;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button captureBtn;
    }
}

