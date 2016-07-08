namespace MeetingAgent
{
    partial class frmMain
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
            this.tlpOuter = new System.Windows.Forms.TableLayoutPanel();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnLoadVideo = new System.Windows.Forms.Button();
            this.lblTime = new System.Windows.Forms.Label();
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.ibOriginal = new Emgu.CV.UI.ImageBox();
            this.tbVideoPosition = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ibProcessing4 = new Emgu.CV.UI.ImageBox();
            this.ibProcessing3 = new Emgu.CV.UI.ImageBox();
            this.ibProcessing2 = new Emgu.CV.UI.ImageBox();
            this.ibProcessing1 = new Emgu.CV.UI.ImageBox();
            this.ofdChooseVideo = new System.Windows.Forms.OpenFileDialog();
            this.tlpOuter.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ibOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbVideoPosition)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ibProcessing4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibProcessing3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibProcessing2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibProcessing1)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpOuter
            // 
            this.tlpOuter.ColumnCount = 2;
            this.tlpOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOuter.Controls.Add(this.tlpButtons, 0, 2);
            this.tlpOuter.Controls.Add(this.txtMessages, 0, 3);
            this.tlpOuter.Controls.Add(this.ibOriginal, 0, 0);
            this.tlpOuter.Controls.Add(this.tbVideoPosition, 0, 1);
            this.tlpOuter.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tlpOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpOuter.Location = new System.Drawing.Point(0, 0);
            this.tlpOuter.Name = "tlpOuter";
            this.tlpOuter.RowCount = 4;
            this.tlpOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 600F));
            this.tlpOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpOuter.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOuter.Size = new System.Drawing.Size(993, 552);
            this.tlpOuter.TabIndex = 0;
            // 
            // tlpButtons
            // 
            this.tlpButtons.ColumnCount = 3;
            this.tlpOuter.SetColumnSpan(this.tlpButtons, 2);
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpButtons.Controls.Add(this.btnLoadVideo, 0, 0);
            this.tlpButtons.Controls.Add(this.lblTime, 2, 0);
            this.tlpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButtons.Location = new System.Drawing.Point(3, 633);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.RowCount = 1;
            this.tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.Size = new System.Drawing.Size(987, 44);
            this.tlpButtons.TabIndex = 0;
            // 
            // btnLoadVideo
            // 
            this.btnLoadVideo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLoadVideo.AutoSize = true;
            this.btnLoadVideo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLoadVideo.Location = new System.Drawing.Point(129, 10);
            this.btnLoadVideo.Name = "btnLoadVideo";
            this.btnLoadVideo.Size = new System.Drawing.Size(71, 23);
            this.btnLoadVideo.TabIndex = 0;
            this.btnLoadVideo.Text = "Load Video";
            this.btnLoadVideo.UseVisualStyleBackColor = true;
            this.btnLoadVideo.Click += new System.EventHandler(this.btnLoadVideo_Click);
            // 
            // lblTime
            // 
            this.lblTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(801, 15);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(43, 13);
            this.lblTime.TabIndex = 1;
            this.lblTime.Text = "0:00:00";
            // 
            // txtMessages
            // 
            this.tlpOuter.SetColumnSpan(this.txtMessages, 2);
            this.txtMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessages.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessages.Location = new System.Drawing.Point(3, 683);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.Size = new System.Drawing.Size(987, 96);
            this.txtMessages.TabIndex = 1;
            this.txtMessages.WordWrap = false;
            // 
            // ibOriginal
            // 
            this.ibOriginal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ibOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ibOriginal.Enabled = false;
            this.ibOriginal.Location = new System.Drawing.Point(3, 3);
            this.ibOriginal.Name = "ibOriginal";
            this.ibOriginal.Size = new System.Drawing.Size(490, 594);
            this.ibOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ibOriginal.TabIndex = 2;
            this.ibOriginal.TabStop = false;
            // 
            // tbVideoPosition
            // 
            this.tbVideoPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbVideoPosition.Location = new System.Drawing.Point(3, 603);
            this.tbVideoPosition.Name = "tbVideoPosition";
            this.tbVideoPosition.Size = new System.Drawing.Size(490, 24);
            this.tbVideoPosition.TabIndex = 3;
            this.tbVideoPosition.Scroll += new System.EventHandler(this.tbVideoPosition_Scroll);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.ibProcessing4, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.ibProcessing3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ibProcessing2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ibProcessing1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(499, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(491, 594);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // ibProcessing4
            // 
            this.ibProcessing4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ibProcessing4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ibProcessing4.Enabled = false;
            this.ibProcessing4.Location = new System.Drawing.Point(248, 300);
            this.ibProcessing4.Name = "ibProcessing4";
            this.ibProcessing4.Size = new System.Drawing.Size(240, 291);
            this.ibProcessing4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ibProcessing4.TabIndex = 7;
            this.ibProcessing4.TabStop = false;
            // 
            // ibProcessing3
            // 
            this.ibProcessing3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ibProcessing3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ibProcessing3.Enabled = false;
            this.ibProcessing3.Location = new System.Drawing.Point(3, 300);
            this.ibProcessing3.Name = "ibProcessing3";
            this.ibProcessing3.Size = new System.Drawing.Size(239, 291);
            this.ibProcessing3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ibProcessing3.TabIndex = 6;
            this.ibProcessing3.TabStop = false;
            // 
            // ibProcessing2
            // 
            this.ibProcessing2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ibProcessing2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ibProcessing2.Enabled = false;
            this.ibProcessing2.Location = new System.Drawing.Point(248, 3);
            this.ibProcessing2.Name = "ibProcessing2";
            this.ibProcessing2.Size = new System.Drawing.Size(240, 291);
            this.ibProcessing2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ibProcessing2.TabIndex = 5;
            this.ibProcessing2.TabStop = false;
            // 
            // ibProcessing1
            // 
            this.ibProcessing1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ibProcessing1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ibProcessing1.Enabled = false;
            this.ibProcessing1.Location = new System.Drawing.Point(3, 3);
            this.ibProcessing1.Name = "ibProcessing1";
            this.ibProcessing1.Size = new System.Drawing.Size(239, 291);
            this.ibProcessing1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ibProcessing1.TabIndex = 3;
            this.ibProcessing1.TabStop = false;
            // 
            // ofdChooseVideo
            // 
            this.ofdChooseVideo.Filter = "Video Files (*.avi)|*.avi";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 552);
            this.Controls.Add(this.tlpOuter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMain";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tlpOuter.ResumeLayout(false);
            this.tlpOuter.PerformLayout();
            this.tlpButtons.ResumeLayout(false);
            this.tlpButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ibOriginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbVideoPosition)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ibProcessing4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibProcessing3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibProcessing2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibProcessing1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpOuter;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.TextBox txtMessages;
        private Emgu.CV.UI.ImageBox ibOriginal;
        private System.Windows.Forms.Button btnLoadVideo;
        private System.Windows.Forms.OpenFileDialog ofdChooseVideo;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.TrackBar tbVideoPosition;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Emgu.CV.UI.ImageBox ibThres;
        private Emgu.CV.UI.ImageBox ibProcessing1;
        private Emgu.CV.UI.ImageBox ibProcessing2;
        private Emgu.CV.UI.ImageBox ibProcessing3;
        private Emgu.CV.UI.ImageBox ibProcessing4;
    }
}

