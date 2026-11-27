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
            lblTtsStatus = new Label();
            btnClearTts = new Button();
            btnStopSpeak = new Button();
            btnPause = new Button();
            btnSpeak = new Button();
            lblRate = new Label();
            trkRate = new TrackBar();
            lblRateTitle = new Label();
            cmbVoices = new ComboBox();
            lblVoiceTitle = new Label();
            txtSpeak = new TextBox();
            grpStt = new GroupBox();
            lblRecognizerTitle = new Label();
            cmbRecognizers = new ComboBox();
            btnListen = new Button();
            btnStopListen = new Button();
            btnClearStt = new Button();
            lblSttStatus = new Label();
            lstRecognized = new ListBox();
            grpTts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trkRate).BeginInit();
            grpStt.SuspendLayout();
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
            // lblTtsStatus
            // 
            lblTtsStatus.AutoSize = true;
            lblTtsStatus.Location = new Point(465, 198);
            lblTtsStatus.Name = "lblTtsStatus";
            lblTtsStatus.Size = new Size(44, 20);
            lblTtsStatus.TabIndex = 10;
            lblTtsStatus.Text = "Hazır";
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
            // btnStopSpeak
            // 
            btnStopSpeak.Location = new Point(235, 192);
            btnStopSpeak.Name = "btnStopSpeak";
            btnStopSpeak.Size = new Size(100, 32);
            btnStopSpeak.TabIndex = 8;
            btnStopSpeak.Text = "Durdur";
            btnStopSpeak.UseVisualStyleBackColor = true;
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
            // btnSpeak
            // 
            btnSpeak.Location = new Point(15, 192);
            btnSpeak.Name = "btnSpeak";
            btnSpeak.Size = new Size(100, 32);
            btnSpeak.TabIndex = 6;
            btnSpeak.Text = "Oku";
            btnSpeak.UseVisualStyleBackColor = true;
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
            // trkRate
            // 
            trkRate.Location = new Point(370, 147);
            trkRate.Minimum = -10;
            trkRate.Name = "trkRate";
            trkRate.Size = new Size(200, 56);
            trkRate.TabIndex = 4;
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
            // cmbVoices
            // 
            cmbVoices.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVoices.FormattingEnabled = true;
            cmbVoices.Location = new Point(60, 152);
            cmbVoices.Name = "cmbVoices";
            cmbVoices.Size = new Size(250, 28);
            cmbVoices.TabIndex = 2;
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
            // txtSpeak
            // 
            txtSpeak.Location = new Point(15, 30);
            txtSpeak.Multiline = true;
            txtSpeak.Name = "txtSpeak";
            txtSpeak.ScrollBars = ScrollBars.Vertical;
            txtSpeak.Size = new Size(745, 110);
            txtSpeak.TabIndex = 0;
            // 
            // grpStt
            // 
            grpStt.Controls.Add(lstRecognized);
            grpStt.Controls.Add(lblSttStatus);
            grpStt.Controls.Add(btnClearStt);
            grpStt.Controls.Add(btnStopListen);
            grpStt.Controls.Add(btnListen);
            grpStt.Controls.Add(cmbRecognizers);
            grpStt.Controls.Add(lblRecognizerTitle);
            grpStt.Location = new Point(12, 255);
            grpStt.Name = "grpStt";
            grpStt.Size = new Size(776, 270);
            grpStt.TabIndex = 1;
            grpStt.TabStop = false;
            grpStt.Text = "Konuşmayı Yazıya Çevir (Speech-to-Text)";
            // 
            // lblRecognizerTitle
            // 
            lblRecognizerTitle.AutoSize = true;
            lblRecognizerTitle.Location = new Point(15, 35);
            lblRecognizerTitle.Name = "lblRecognizerTitle";
            lblRecognizerTitle.Size = new Size(35, 20);
            lblRecognizerTitle.TabIndex = 0;
            lblRecognizerTitle.Text = "Dil :";
            // 
            // cmbRecognizers
            // 
            cmbRecognizers.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRecognizers.FormattingEnabled = true;
            cmbRecognizers.Location = new Point(60, 32);
            cmbRecognizers.Name = "cmbRecognizers";
            cmbRecognizers.Size = new Size(250, 28);
            cmbRecognizers.TabIndex = 1;
            // 
            // btnListen
            // 
            btnListen.Location = new Point(325, 30);
            btnListen.Name = "btnListen";
            btnListen.Size = new Size(150, 32);
            btnListen.TabIndex = 2;
            btnListen.Text = "Dinlemeyi Başlat";
            btnListen.UseVisualStyleBackColor = true;
            // 
            // btnStopListen
            // 
            btnStopListen.Location = new Point(485, 30);
            btnStopListen.Name = "btnStopListen";
            btnStopListen.Size = new Size(150, 32);
            btnStopListen.TabIndex = 3;
            btnStopListen.Text = "Dinlemeyi Durdur";
            btnStopListen.UseVisualStyleBackColor = true;
            // 
            // btnClearStt
            // 
            btnClearStt.Location = new Point(645, 30);
            btnClearStt.Name = "btnClearStt";
            btnClearStt.Size = new Size(115, 32);
            btnClearStt.TabIndex = 4;
            btnClearStt.Text = "Temizle";
            btnClearStt.UseVisualStyleBackColor = true;
            // 
            // lblSttStatus
            // 
            lblSttStatus.AutoSize = true;
            lblSttStatus.Location = new Point(15, 75);
            lblSttStatus.Name = "lblSttStatus";
            lblSttStatus.Size = new Size(109, 20);
            lblSttStatus.TabIndex = 5;
            lblSttStatus.Text = "Dinleme kapalı";
            // 
            // lstRecognized
            // 
            lstRecognized.FormattingEnabled = true;
            lstRecognized.HorizontalScrollbar = true;
            lstRecognized.Location = new Point(15, 100);
            lstRecognized.Name = "lstRecognized";
            lstRecognized.Size = new Size(745, 144);
            lstRecognized.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(802, 533);
            Controls.Add(grpStt);
            Controls.Add(grpTts);
            Name = "Form1";
            Text = "Speech App";
            grpTts.ResumeLayout(false);
            grpTts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trkRate).EndInit();
            grpStt.ResumeLayout(false);
            grpStt.PerformLayout();
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
        private GroupBox grpStt;
        private Button btnListen;
        private ComboBox cmbRecognizers;
        private Label lblRecognizerTitle;
        private ListBox lstRecognized;
        private Label lblSttStatus;
        private Button btnClearStt;
        private Button btnStopListen;
    }
}
