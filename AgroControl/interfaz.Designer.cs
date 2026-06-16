namespace AgroControl
{
    partial class Interfaz
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Interfaz));
            panelMenu = new Panel();
            iconButton8 = new FontAwesome.Sharp.IconButton();
            btnSettings = new FontAwesome.Sharp.IconButton();
            btnPlantRecords = new FontAwesome.Sharp.IconButton();
            btnCharts = new FontAwesome.Sharp.IconButton();
            btnReadingLog = new FontAwesome.Sharp.IconButton();
            btnDasboard = new FontAwesome.Sharp.IconButton();
            panelLogo = new Panel();
            logoLabel = new Label();
            panelTitleBar = new Panel();
            label2 = new Label();
            label1 = new Label();
            panelDesktop = new Panel();
            panelMenu.SuspendLayout();
            panelLogo.SuspendLayout();
            panelTitleBar.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(70, 72, 230);
            panelMenu.Controls.Add(iconButton8);
            panelMenu.Controls.Add(btnSettings);
            panelMenu.Controls.Add(btnPlantRecords);
            panelMenu.Controls.Add(btnCharts);
            panelMenu.Controls.Add(btnReadingLog);
            panelMenu.Controls.Add(btnDasboard);
            panelMenu.Controls.Add(panelLogo);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(240, 700);
            panelMenu.TabIndex = 0;
            // 
            // iconButton8
            // 
            iconButton8.Dock = DockStyle.Bottom;
            iconButton8.FlatAppearance.BorderSize = 0;
            iconButton8.FlatStyle = FlatStyle.Flat;
            iconButton8.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            iconButton8.ForeColor = Color.FromArgb(200, 210, 255);
            iconButton8.IconChar = FontAwesome.Sharp.IconChar.CircleInfo;
            iconButton8.IconColor = Color.FromArgb(200, 210, 255);
            iconButton8.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton8.IconSize = 22;
            iconButton8.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton8.Location = new Point(0, 646);
            iconButton8.Name = "iconButton8";
            iconButton8.Padding = new Padding(15, 0, 0, 0);
            iconButton8.Size = new Size(240, 54);
            iconButton8.TabIndex = 7;
            iconButton8.Text = "Help";
            iconButton8.TextAlign = ContentAlignment.MiddleLeft;
            iconButton8.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton8.UseVisualStyleBackColor = true;
            iconButton8.Click += btnHelp_Click;
            // 
            // btnSettings
            // 
            btnSettings.Dock = DockStyle.Top;
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            btnSettings.ForeColor = Color.FromArgb(200, 210, 255);
            btnSettings.IconChar = FontAwesome.Sharp.IconChar.Key;
            btnSettings.IconColor = Color.FromArgb(200, 210, 255);
            btnSettings.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSettings.IconSize = 22;
            btnSettings.ImageAlign = ContentAlignment.MiddleLeft;
            btnSettings.Location = new Point(0, 366);
            btnSettings.Name = "btnSettings";
            btnSettings.Padding = new Padding(15, 0, 0, 0);
            btnSettings.Size = new Size(240, 50);
            btnSettings.TabIndex = 5;
            btnSettings.Text = "Settings";
            btnSettings.TextAlign = ContentAlignment.MiddleLeft;
            btnSettings.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnPlantRecords
            // 
            btnPlantRecords.Dock = DockStyle.Top;
            btnPlantRecords.FlatAppearance.BorderSize = 0;
            btnPlantRecords.FlatStyle = FlatStyle.Flat;
            btnPlantRecords.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            btnPlantRecords.ForeColor = Color.FromArgb(200, 210, 255);
            btnPlantRecords.IconChar = FontAwesome.Sharp.IconChar.PlantWilt;
            btnPlantRecords.IconColor = Color.FromArgb(200, 210, 255);
            btnPlantRecords.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnPlantRecords.IconSize = 22;
            btnPlantRecords.ImageAlign = ContentAlignment.MiddleLeft;
            btnPlantRecords.Location = new Point(0, 316);
            btnPlantRecords.Name = "btnPlantRecords";
            btnPlantRecords.Padding = new Padding(15, 0, 0, 0);
            btnPlantRecords.Size = new Size(240, 50);
            btnPlantRecords.TabIndex = 4;
            btnPlantRecords.Text = "Plant Records";
            btnPlantRecords.TextAlign = ContentAlignment.MiddleLeft;
            btnPlantRecords.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnPlantRecords.UseVisualStyleBackColor = true;
            btnPlantRecords.Click += btnPlantRecords_Click;
            // 
            // btnCharts
            // 
            btnCharts.Dock = DockStyle.Top;
            btnCharts.FlatAppearance.BorderSize = 0;
            btnCharts.FlatStyle = FlatStyle.Flat;
            btnCharts.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            btnCharts.ForeColor = Color.FromArgb(200, 210, 255);
            btnCharts.IconChar = FontAwesome.Sharp.IconChar.ChartColumn;
            btnCharts.IconColor = Color.FromArgb(200, 210, 255);
            btnCharts.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCharts.IconSize = 22;
            btnCharts.ImageAlign = ContentAlignment.MiddleLeft;
            btnCharts.Location = new Point(0, 266);
            btnCharts.Name = "btnCharts";
            btnCharts.Padding = new Padding(15, 0, 0, 0);
            btnCharts.Size = new Size(240, 50);
            btnCharts.TabIndex = 3;
            btnCharts.Text = "Charts";
            btnCharts.TextAlign = ContentAlignment.MiddleLeft;
            btnCharts.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCharts.UseVisualStyleBackColor = true;
            btnCharts.Click += btnCharts_Click;
            // 
            // btnReadingLog
            // 
            btnReadingLog.Dock = DockStyle.Top;
            btnReadingLog.FlatAppearance.BorderSize = 0;
            btnReadingLog.FlatStyle = FlatStyle.Flat;
            btnReadingLog.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            btnReadingLog.ForeColor = Color.FromArgb(200, 210, 255);
            btnReadingLog.IconChar = FontAwesome.Sharp.IconChar.Book;
            btnReadingLog.IconColor = Color.FromArgb(200, 210, 255);
            btnReadingLog.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnReadingLog.IconSize = 22;
            btnReadingLog.ImageAlign = ContentAlignment.MiddleLeft;
            btnReadingLog.Location = new Point(0, 216);
            btnReadingLog.Name = "btnReadingLog";
            btnReadingLog.Padding = new Padding(15, 0, 0, 0);
            btnReadingLog.Size = new Size(240, 50);
            btnReadingLog.TabIndex = 2;
            btnReadingLog.Text = "Reading Log";
            btnReadingLog.TextAlign = ContentAlignment.MiddleLeft;
            btnReadingLog.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnReadingLog.UseVisualStyleBackColor = true;
            btnReadingLog.Click += btnReadingLog_Click;
            // 
            // btnDasboard
            // 
            btnDasboard.Dock = DockStyle.Top;
            btnDasboard.FlatAppearance.BorderSize = 0;
            btnDasboard.FlatStyle = FlatStyle.Flat;
            btnDasboard.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            btnDasboard.ForeColor = Color.FromArgb(200, 210, 255);
            btnDasboard.IconChar = FontAwesome.Sharp.IconChar.House;
            btnDasboard.IconColor = Color.FromArgb(200, 210, 255);
            btnDasboard.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDasboard.IconSize = 22;
            btnDasboard.ImageAlign = ContentAlignment.MiddleLeft;
            btnDasboard.Location = new Point(0, 166);
            btnDasboard.Name = "btnDasboard";
            btnDasboard.Padding = new Padding(15, 0, 0, 0);
            btnDasboard.Size = new Size(240, 50);
            btnDasboard.TabIndex = 1;
            btnDasboard.Text = "Dashboard";
            btnDasboard.TextAlign = ContentAlignment.MiddleLeft;
            btnDasboard.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDasboard.UseVisualStyleBackColor = true;
            btnDasboard.Click += btnDasboard_Click;
            // 
            // panelLogo
            // 
            panelLogo.BackColor = Color.FromArgb(60, 62, 220);
            panelLogo.Controls.Add(logoLabel);
            panelLogo.Dock = DockStyle.Top;
            panelLogo.Location = new Point(0, 0);
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new Size(240, 166);
            panelLogo.TabIndex = 0;
            // 
            // logoLabel
            // 
            logoLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            logoLabel.ForeColor = Color.White;
            logoLabel.Location = new Point(0, 55);
            logoLabel.Name = "logoLabel";
            logoLabel.Size = new Size(240, 50);
            logoLabel.TabIndex = 0;
            logoLabel.Text = "AgroControl";
            logoLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelTitleBar
            // 
            panelTitleBar.Controls.Add(label2);
            panelTitleBar.Controls.Add(label1);
            panelTitleBar.Dock = DockStyle.Top;
            panelTitleBar.Location = new Point(240, 0);
            panelTitleBar.Name = "panelTitleBar";
            panelTitleBar.Size = new Size(1020, 50);
            panelTitleBar.TabIndex = 1;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Right;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            label2.ForeColor = Color.White;
            label2.Location = new Point(870, 12);
            label2.Name = "label2";
            label2.Size = new Size(140, 25);
            label2.TabIndex = 6;
            label2.Text = "admin";
            label2.TextAlign = ContentAlignment.MiddleRight;
            label2.Click += label2_Click;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(20, 12);
            label1.Name = "label1";
            label1.Size = new Size(300, 25);
            label1.TabIndex = 5;
            label1.Text = "Dashboard";
            label1.Click += label1_Click;
            // 
            // panelDesktop
            // 
            panelDesktop.BackColor = Color.FromArgb(245, 247, 250);
            panelDesktop.Dock = DockStyle.Fill;
            panelDesktop.Location = new Point(240, 50);
            panelDesktop.Name = "panelDesktop";
            panelDesktop.Size = new Size(1020, 650);
            panelDesktop.TabIndex = 2;
            // 
            // Interfaz
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1260, 700);
            Controls.Add(panelDesktop);
            Controls.Add(panelTitleBar);
            Controls.Add(panelMenu);
            Name = "Interfaz";
            Text = "AgroControl";
            Load += Interfaz_Load;
            panelMenu.ResumeLayout(false);
            panelLogo.ResumeLayout(false);
            panelTitleBar.ResumeLayout(false);
            ResumeLayout(false);
        }

        private FontAwesome.Sharp.IconButton btnDasboard;
        private FontAwesome.Sharp.IconButton btnSettings;
        private FontAwesome.Sharp.IconButton btnPlantRecords;
        private FontAwesome.Sharp.IconButton btnCharts;
        private FontAwesome.Sharp.IconButton btnReadingLog;
        private FontAwesome.Sharp.IconButton iconButton8;
        private Panel panelMenu;
        private Panel panelTitleBar;
        private Panel panelDesktop;
        private Panel panelLogo;
        private Label logoLabel;
        private Label label1;
        private Label label2;
    }
}
