namespace SpeechApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            grpTts = new GroupBox();
            txtSpeak = new TextBox();
            lblVoiceTitle = new Label();
            cmbVoices = new ComboBox();
            lblRateTitle = new Label();
            trkRate = new TrackBar();
            lblRate = new Label();
            btnSpeak = new Button();
            btnPause = new Button();
            btnStopSpeak = new Button();
            btnClearTts = new Button();
            lblTtsStatus = new Label();
            grpTts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trkRate).BeginInit();
            SuspendLayout();
            // 
            // grpTts
            // 
            grpTts.Controls.Add(lblTtsStatus);
            grpTts.Controls.Add(btnClearTts);
            grpTts.Controls.Add(btnStopSpeak);
            grpTts.Controls.Add(btnPause);
            grpTts.Controls.Add(btnSpeak);
            grpTts.Controls.Add(lblRate);
            grpTts.Controls.Add(trkRate);
            grpTts.Controls.Add(lblRateTitle);
            grpTts.Controls.Add(cmbVoices);
            grpTts.Controls.Add(lblVoiceTitle);
            grpTts.Controls.Add(txtSpeak);
            grpTts.Location = new Point(12, 12);
            grpTts.Name = "grpTts";
            grpTts.Size = new Size(776, 235);
            grpTts.TabIndex = 0;
            grpTts.TabStop = false;
            grpTts.Text = "Metni Sesli Oku (Text-to-Speech)";
            // 
            // txtSpeak
            // 
            txtSpeak.Location = new Point(15, 30);
            txtSpeak.Multiline = true;
            txtSpeak.Name = "txtSpeak";
            txtSpeak.ScrollBars = ScrollBars.Vertical;
            txtSpeak.Size = new Size(745, 110);
            txtSpeak.TabIndex = 0;
            // 
            // lblVoiceTitle
            // 
            lblVoiceTitle.AutoSize = true;
            lblVoiceTitle.Location = new Point(15, 155);
            lblVoiceTitle.Name = "lblVoiceTitle";
            lblVoiceTitle.Size = new Size(38, 20);
            lblVoiceTitle.TabIndex = 1;
            lblVoiceTitle.Text = "Ses :";
            // 
            // cmbVoices
            // 
            cmbVoices.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVoices.FormattingEnabled = true;
            cmbVoices.Location = new Point(60, 152);
            cmbVoices.Name = "cmbVoices";
            cmbVoices.Size = new Size(250, 28);
            cmbVoices.TabIndex = 2;
            // 
            // lblRateTitle
            // 
            lblRateTitle.AutoSize = true;
            lblRateTitle.Location = new Point(330, 155);
            lblRateTitle.Name = "lblRateTitle";
            lblRateTitle.Size = new Size(38, 20);
            lblRateTitle.TabIndex = 3;
            lblRateTitle.Text = "Hız :";
            // 
            // trkRate
            // 
            trkRate.Location = new Point(370, 147);
            trkRate.Minimum = -10;
            trkRate.Name = "trkRate";
            trkRate.Size = new Size(200, 56);
            trkRate.TabIndex = 4;
            // 
            // lblRate
            // 
            lblRate.AutoSize = true;
            lblRate.Location = new Point(580, 155);
            lblRate.Name = "lblRate";
            lblRate.Size = new Size(17, 20);
            lblRate.TabIndex = 5;
            lblRate.Text = "0";
            // 
            // btnSpeak
            // 
            btnSpeak.Location = new Point(15, 192);
            btnSpeak.Name = "btnSpeak";
            btnSpeak.Size = new Size(100, 32);
            btnSpeak.TabIndex = 6;
            btnSpeak.Text = "Oku";
            btnSpeak.UseVisualStyleBackColor = true;
            // 
            // btnPause
            // 
            btnPause.Location = new Point(125, 192);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(100, 32);
            btnPause.TabIndex = 7;
            btnPause.Text = "Duraklat";
            btnPause.UseVisualStyleBackColor = true;
            // 
            // btnStopSpeak
            // 
            btnStopSpeak.Location = new Point(235, 192);
            btnStopSpeak.Name = "btnStopSpeak";
            btnStopSpeak.Size = new Size(100, 32);
            btnStopSpeak.TabIndex = 8;
            btnStopSpeak.Text = "Durdur";
            btnStopSpeak.UseVisualStyleBackColor = true;
            // 
            // btnClearTts
            // 
            btnClearTts.Location = new Point(345, 192);
            btnClearTts.Name = "btnClearTts";
            btnClearTts.Size = new Size(100, 32);
            btnClearTts.TabIndex = 9;
            btnClearTts.Text = "Temizle";
            btnClearTts.UseVisualStyleBackColor = true;
            // 
            // lblTtsStatus
            // 
            lblTtsStatus.AutoSize = true;
            lblTtsStatus.Location = new Point(465, 198);
            lblTtsStatus.Name = "lblTtsStatus";
            lblTtsStatus.Size = new Size(44, 20);
            lblTtsStatus.TabIndex = 10;
            lblTtsStatus.Text = "Hazır";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(802, 533);
            Controls.Add(grpTts);
            Name = "Form1";
            Text = "Speech App";
            grpTts.ResumeLayout(false);
            grpTts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trkRate).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpTts;
        private Label lblVoiceTitle;
        private TextBox txtSpeak;
        private ComboBox cmbVoices;
        private TrackBar trkRate;
        private Label lblRateTitle;
        private Button btnSpeak;
        private Label lblRate;
        private Button btnClearTts;
        private Button btnStopSpeak;
        private Button btnPause;
        private Label lblTtsStatus;
    }
}
