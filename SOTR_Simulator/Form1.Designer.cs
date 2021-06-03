
namespace SOTR_Simulator
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.startButton = new System.Windows.Forms.Button();
            this.listOfAlgorithms = new System.Windows.Forms.ComboBox();
            this.mainLabel = new System.Windows.Forms.Label();
            this.progressDiagnostic = new System.Windows.Forms.ProgressBar();
            this.chooseDiagnosticLabel = new System.Windows.Forms.Label();
            this.algorithmDescriptionLabel = new System.Windows.Forms.Label();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.inputDataBox = new System.Windows.Forms.TextBox();
            this.inputDatalabel = new System.Windows.Forms.Label();
            this.resultBox = new System.Windows.Forms.TextBox();
            this.resultLabel = new System.Windows.Forms.Label();
            this.RandomGenButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startButton.Location = new System.Drawing.Point(12, 478);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(375, 54);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Начать диагностику";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Visible = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // listOfAlgorithms
            // 
            this.listOfAlgorithms.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listOfAlgorithms.FormattingEnabled = true;
            this.listOfAlgorithms.Items.AddRange(new object[] {
            "Диагностика Панели ",
            "Диагностика Блока коммутации",
            "Диагностика Антенно фидерная система",
            "Диагностика Фазированной решетки и БАО-ВМ"});
            this.listOfAlgorithms.Location = new System.Drawing.Point(12, 101);
            this.listOfAlgorithms.Name = "listOfAlgorithms";
            this.listOfAlgorithms.Size = new System.Drawing.Size(583, 27);
            this.listOfAlgorithms.TabIndex = 1;
            this.listOfAlgorithms.Text = "Список алгоритмов";
            this.listOfAlgorithms.SelectedIndexChanged += new System.EventHandler(this.listOfAlgorithms_SelectedIndexChanged);
            // 
            // mainLabel
            // 
            this.mainLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainLabel.AutoSize = true;
            this.mainLabel.Font = new System.Drawing.Font("Arial", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainLabel.Location = new System.Drawing.Point(177, 9);
            this.mainLabel.Name = "mainLabel";
            this.mainLabel.Size = new System.Drawing.Size(774, 38);
            this.mainLabel.TabIndex = 3;
            this.mainLabel.Text = "Симулятор работы алгоритмов диагностики СОТР";
            this.mainLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // progressDiagnostic
            // 
            this.progressDiagnostic.Location = new System.Drawing.Point(12, 538);
            this.progressDiagnostic.Name = "progressDiagnostic";
            this.progressDiagnostic.Size = new System.Drawing.Size(1173, 35);
            this.progressDiagnostic.TabIndex = 4;
            this.progressDiagnostic.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // chooseDiagnosticLabel
            // 
            this.chooseDiagnosticLabel.AutoSize = true;
            this.chooseDiagnosticLabel.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chooseDiagnosticLabel.Location = new System.Drawing.Point(7, 71);
            this.chooseDiagnosticLabel.Name = "chooseDiagnosticLabel";
            this.chooseDiagnosticLabel.Size = new System.Drawing.Size(375, 27);
            this.chooseDiagnosticLabel.TabIndex = 5;
            this.chooseDiagnosticLabel.Text = "Выберите алгоритм диагностики";
            this.chooseDiagnosticLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // algorithmDescriptionLabel
            // 
            this.algorithmDescriptionLabel.AutoSize = true;
            this.algorithmDescriptionLabel.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.algorithmDescriptionLabel.Location = new System.Drawing.Point(7, 131);
            this.algorithmDescriptionLabel.Name = "algorithmDescriptionLabel";
            this.algorithmDescriptionLabel.Size = new System.Drawing.Size(251, 27);
            this.algorithmDescriptionLabel.TabIndex = 6;
            this.algorithmDescriptionLabel.Text = "Описание алгоритма:";
            this.algorithmDescriptionLabel.Click += new System.EventHandler(this.label3_Click);
            // 
            // descriptionBox
            // 
            this.descriptionBox.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.descriptionBox.Location = new System.Drawing.Point(12, 158);
            this.descriptionBox.Multiline = true;
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.ReadOnly = true;
            this.descriptionBox.Size = new System.Drawing.Size(375, 314);
            this.descriptionBox.TabIndex = 7;
            // 
            // inputDataBox
            // 
            this.inputDataBox.Location = new System.Drawing.Point(410, 158);
            this.inputDataBox.Multiline = true;
            this.inputDataBox.Name = "inputDataBox";
            this.inputDataBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.inputDataBox.Size = new System.Drawing.Size(185, 314);
            this.inputDataBox.TabIndex = 8;
            this.inputDataBox.Click += new System.EventHandler(this.inputDataBox_Click);
            this.inputDataBox.TextChanged += new System.EventHandler(this.inputDataBox_TextChanged);
            // 
            // inputDatalabel
            // 
            this.inputDatalabel.AutoSize = true;
            this.inputDatalabel.Font = new System.Drawing.Font("Arial", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inputDatalabel.Location = new System.Drawing.Point(405, 129);
            this.inputDatalabel.Name = "inputDatalabel";
            this.inputDatalabel.Size = new System.Drawing.Size(183, 25);
            this.inputDatalabel.TabIndex = 9;
            this.inputDatalabel.Text = "Входные данные";
            // 
            // resultBox
            // 
            this.resultBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultBox.Location = new System.Drawing.Point(616, 158);
            this.resultBox.Multiline = true;
            this.resultBox.Name = "resultBox";
            this.resultBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resultBox.Size = new System.Drawing.Size(566, 374);
            this.resultBox.TabIndex = 10;
            this.resultBox.TextChanged += new System.EventHandler(this.resultBox_TextChanged);
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resultLabel.Location = new System.Drawing.Point(750, 71);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(330, 32);
            this.resultLabel.TabIndex = 11;
            this.resultLabel.Text = "Результаты Диагностики";
            // 
            // RandomGenButton
            // 
            this.RandomGenButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.RandomGenButton.AutoSize = true;
            this.RandomGenButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.RandomGenButton.Enabled = false;
            this.RandomGenButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RandomGenButton.Location = new System.Drawing.Point(410, 478);
            this.RandomGenButton.Name = "RandomGenButton";
            this.RandomGenButton.Size = new System.Drawing.Size(172, 56);
            this.RandomGenButton.TabIndex = 12;
            this.RandomGenButton.TabStop = true;
            this.RandomGenButton.Text = "Сгенерировать\r\nвходные данные";
            this.RandomGenButton.UseVisualStyleBackColor = true;
            this.RandomGenButton.Visible = false;
            this.RandomGenButton.CheckedChanged += new System.EventHandler(this.RandomGenButton_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1194, 585);
            this.Controls.Add(this.RandomGenButton);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.resultBox);
            this.Controls.Add(this.inputDatalabel);
            this.Controls.Add(this.inputDataBox);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.algorithmDescriptionLabel);
            this.Controls.Add(this.chooseDiagnosticLabel);
            this.Controls.Add(this.progressDiagnostic);
            this.Controls.Add(this.mainLabel);
            this.Controls.Add(this.listOfAlgorithms);
            this.Controls.Add(this.startButton);
            this.Name = "Form1";
            this.Text = "SotrSimulator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ComboBox listOfAlgorithms;
        private System.Windows.Forms.Label mainLabel;
        private System.Windows.Forms.ProgressBar progressDiagnostic;
        private System.Windows.Forms.Label chooseDiagnosticLabel;
        private System.Windows.Forms.Label algorithmDescriptionLabel;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.TextBox inputDataBox;
        private System.Windows.Forms.Label inputDatalabel;
        private System.Windows.Forms.TextBox resultBox;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.RadioButton RandomGenButton;
    }
}

