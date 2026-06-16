using System.Data;
using System.Drawing.Drawing2D;
using System.Windows.Forms.DataVisualization.Charting;

namespace AgroControl
{
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
            BuildUI();
        }

        private Panel CreateCard(Color accentColor, FontAwesome.Sharp.IconChar icon, string title, string value, string subtitle, string indicator, Color indicatorColor)
        {
            var card = new Panel
            {
                Size = new Size(245, 130),
                BackColor = Color.White,
                Margin = new Padding(8)
            };

            var accent = new Panel
            {
                Size = new Size(245, 4),
                Location = new Point(0, 0),
                BackColor = accentColor
            };
            card.Controls.Add(accent);

            var iconCtrl = new FontAwesome.Sharp.IconPictureBox
            {
                IconChar = icon,
                IconColor = accentColor,
                IconSize = 32,
                Location = new Point(15, 18),
                Size = new Size(32, 32)
            };
            card.Controls.Add(iconCtrl);

            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 45, 60),
                Location = new Point(55, 15),
                AutoSize = true
            };
            card.Controls.Add(lblValue);

            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.FromArgb(120, 130, 150),
                Location = new Point(55, 42),
                AutoSize = true
            };
            card.Controls.Add(lblTitle);

            var lblSub = new Label
            {
                Text = subtitle,
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                ForeColor = Color.FromArgb(150, 160, 180),
                Location = new Point(15, 75),
                AutoSize = true
            };
            card.Controls.Add(lblSub);

            var lblInd = new Label
            {
                Text = indicator,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = indicatorColor,
                Location = new Point(15, 95),
                AutoSize = true
            };
            card.Controls.Add(lblInd);

            card.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using var path = new GraphicsPath();
                path.AddArc(0, 0, 10, 10, 180, 90);
                path.AddArc(card.Width - 10, 0, 10, 10, 270, 90);
                path.AddArc(card.Width - 10, card.Height - 10, 10, 10, 0, 90);
                path.AddArc(0, card.Height - 10, 10, 10, 90, 90);
                path.CloseFigure();
                card.Region = new Region(path);
            };

            return card;
        }

        private DataGridView CreateTable(string[] columns, string[][] rows, int[] widths = null)
        {
            var dgv = new DataGridView
            {
                Size = new Size(480, 180),
                BackColor = Color.White,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.FromArgb(235, 238, 242),
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9),
                RowTemplate = { Height = 28, DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.FromArgb(60, 70, 85), BackColor = Color.White, SelectionBackColor = Color.FromArgb(235, 238, 250), SelectionForeColor = Color.FromArgb(60, 70, 85) } },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(100, 110, 130), BackColor = Color.FromArgb(245, 247, 250), SelectionBackColor = Color.FromArgb(245, 247, 250), Padding = new Padding(8, 0, 8, 0) },
            };

            foreach (var col in columns)
                dgv.Columns.Add(col, col);

            foreach (var row in rows)
                dgv.Rows.Add(row);

            if (widths != null)
                for (int i = 0; i < widths.Length && i < dgv.Columns.Count; i++)
                    dgv.Columns[i].Width = widths[i];

            return dgv;
        }

        private Panel CreateSectionTitle(string title)
        {
            var p = new Panel { Size = new Size(480, 30), BackColor = Color.White };
            var lbl = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 55, 70),
                Location = new Point(0, 0),
                AutoSize = true
            };
            p.Controls.Add(lbl);
            return p;
        }

        private void BuildUI()
        {
            // ─── TOP TITLE ───
            var lblDashboard = new Label
            {
                Text = "Dashboard",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 45, 60),
                Location = new Point(25, 20),
                AutoSize = true
            };
            Controls.Add(lblDashboard);

            var lblSubtitle = new Label
            {
                Text = "Overview of your greenhouse and sensor data.",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(140, 150, 170),
                Location = new Point(27, 55),
                AutoSize = true
            };
            Controls.Add(lblSubtitle);

            // ─── CARDS ───
            var cards = new Panel { Location = new Point(20, 85), Size = new Size(1060, 145), BackColor = Color.Transparent };

            cards.Controls.Add(CreateCard(Color.FromArgb(255, 90, 70), FontAwesome.Sharp.IconChar.ThermometerHalf, "Temperature", "26.4 °C", "Ideal: 22–28 °C", "+0.6 °C vs last hour", Color.FromArgb(0, 190, 100)));
            cards.Controls[^1].Location = new Point(0, 5);

            cards.Controls.Add(CreateCard(Color.FromArgb(50, 140, 255), FontAwesome.Sharp.IconChar.Tint, "Soil Moisture", "32 %", "Ideal: 30–60 %", "-5 % vs last hour", Color.FromArgb(255, 80, 60)));
            cards.Controls[^1].Location = new Point(260, 5);

            cards.Controls.Add(CreateCard(Color.FromArgb(140, 80, 255), FontAwesome.Sharp.IconChar.Droplet, "Air Humidity", "68 %", "Ideal: 60–80 %", "+3 % vs last hour", Color.FromArgb(0, 190, 100)));
            cards.Controls[^1].Location = new Point(520, 5);

            // Irrigation card
            var irrCard = new Panel { Size = new Size(245, 130), BackColor = Color.White, Location = new Point(780, 5), Margin = new Padding(8) };
            var irrAccent = new Panel { Size = new Size(245, 4), Location = new Point(0, 0), BackColor = Color.FromArgb(0, 200, 150) };
            irrCard.Controls.Add(irrAccent);
            var irrIcon = new FontAwesome.Sharp.IconPictureBox { IconChar = FontAwesome.Sharp.IconChar.Water, IconColor = Color.FromArgb(0, 200, 150), IconSize = 32, Location = new Point(15, 18), Size = new Size(32, 32) };
            irrCard.Controls.Add(irrIcon);
            var irrVal = new Label { Text = "ON", Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = Color.FromArgb(0, 200, 150), Location = new Point(55, 15), AutoSize = true };
            irrCard.Controls.Add(irrVal);
            var irrTitle = new Label { Text = "Irrigation Status", Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(120, 130, 150), Location = new Point(55, 42), AutoSize = true };
            irrCard.Controls.Add(irrTitle);
            var irrSub = new Label { Text = "Next cycle in 02:15:30", Font = new Font("Segoe UI", 8), ForeColor = Color.FromArgb(150, 160, 180), Location = new Point(15, 75), AutoSize = true };
            irrCard.Controls.Add(irrSub);
            var irrTag = new Label { Text = "Manual", Font = new Font("Segoe UI", 8, FontStyle.Bold), ForeColor = Color.FromArgb(255, 140, 0), Location = new Point(15, 95), AutoSize = true };
            irrCard.Controls.Add(irrTag);
            irrCard.Paint += (s, e) => { var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias; using var path = new GraphicsPath(); path.AddArc(0, 0, 10, 10, 180, 90); path.AddArc(irrCard.Width - 10, 0, 10, 10, 270, 90); path.AddArc(irrCard.Width - 10, irrCard.Height - 10, 10, 10, 0, 90); path.AddArc(0, irrCard.Height - 10, 10, 10, 90, 90); path.CloseFigure(); irrCard.Region = new Region(path); };
            cards.Controls.Add(irrCard);
            Controls.Add(cards);

            // ─── CHART + READINGS ───
            int midY = 240;

            var chartPanel = new Panel { Location = new Point(20, midY), Size = new Size(550, 300), BackColor = Color.White };
            chartPanel.Paint += (s, e) =>
            {
                var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
                using var path = new GraphicsPath();
                int w = chartPanel.Width, h = chartPanel.Height;
                path.AddArc(0, 0, 10, 10, 180, 90); path.AddArc(w - 10, 0, 10, 10, 270, 90);
                path.AddArc(w - 10, h - 10, 10, 10, 0, 90); path.AddArc(0, h - 10, 10, 10, 90, 90);
                path.CloseFigure(); chartPanel.Region = new Region(path);
            };

            var chartTitle = new Label { Text = "Sensor Trends (Last 24 Hours)", Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.FromArgb(50, 55, 70), Location = new Point(15, 12), AutoSize = true };
            chartPanel.Controls.Add(chartTitle);

            var chart = new Chart { Location = new Point(10, 40), Size = new Size(530, 250), BackColor = Color.White };
            var ca = chart.ChartAreas.Add("Main");
            ca.AxisX.MajorGrid.LineColor = Color.FromArgb(240, 242, 245);
            ca.AxisY.MajorGrid.LineColor = Color.FromArgb(240, 242, 245);
            ca.AxisX.LabelStyle.Font = new Font("Segoe UI", 7);
            ca.AxisY.LabelStyle.Font = new Font("Segoe UI", 7);
            ca.BackColor = Color.White;

            var s1 = chart.Series.Add("Temperature (°C)");
            s1.ChartType = SeriesChartType.Spline;
            s1.Color = Color.FromArgb(255, 90, 70);
            s1.BorderWidth = 2;
            s1.Points.AddXY("0h", 25.2); s1.Points.AddXY("4h", 26.1); s1.Points.AddXY("8h", 27.0);
            s1.Points.AddXY("12h", 26.8); s1.Points.AddXY("16h", 25.9); s1.Points.AddXY("20h", 26.4);
            s1.Points.AddXY("24h", 26.4);

            var s2 = chart.Series.Add("Soil Moisture (%)");
            s2.ChartType = SeriesChartType.Spline;
            s2.Color = Color.FromArgb(50, 140, 255);
            s2.BorderWidth = 2;
            s2.Points.AddXY("0h", 45); s2.Points.AddXY("4h", 42); s2.Points.AddXY("8h", 38);
            s2.Points.AddXY("12h", 35); s2.Points.AddXY("16h", 33); s2.Points.AddXY("20h", 32);
            s2.Points.AddXY("24h", 32);

            var s3 = chart.Series.Add("Air Humidity (%)");
            s3.ChartType = SeriesChartType.Spline;
            s3.Color = Color.FromArgb(140, 80, 255);
            s3.BorderWidth = 2;
            s3.Points.AddXY("0h", 62); s3.Points.AddXY("4h", 64); s3.Points.AddXY("8h", 65);
            s3.Points.AddXY("12h", 67); s3.Points.AddXY("16h", 66); s3.Points.AddXY("20h", 68);
            s3.Points.AddXY("24h", 68);

            chartPanel.Controls.Add(chart);
            Controls.Add(chartPanel);

            // ─── RECENT READINGS ───
            var readingsPanel = new Panel { Location = new Point(585, midY), Size = new Size(495, 300), BackColor = Color.White };
            readingsPanel.Paint += (s, e) =>
            {
                var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
                using var path = new GraphicsPath();
                int w = readingsPanel.Width, h = readingsPanel.Height;
                path.AddArc(0, 0, 10, 10, 180, 90); path.AddArc(w - 10, 0, 10, 10, 270, 90);
                path.AddArc(w - 10, h - 10, 10, 10, 0, 90); path.AddArc(0, h - 10, 10, 10, 90, 90);
                path.CloseFigure(); readingsPanel.Region = new Region(path);
            };

            var rdTitle = new Label { Text = "Recent Readings", Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.FromArgb(50, 55, 70), Location = new Point(15, 12), AutoSize = true };
            readingsPanel.Controls.Add(rdTitle);

            var rdgv = CreateTable(
                new[] { "Date", "Time", "Plant", "Temp (°C)", "Soil (%)", "Air (%)", "Status" },
                new[] {
                    new[] { "May 24, 2025", "11:30 AM", "Tomato", "26.4", "32", "68", "Low Soil Moisture" },
                    new[] { "May 24, 2025", "11:00 AM", "Lettuce", "24.8", "45", "66", "Normal" },
                    new[] { "May 24, 2025", "10:30 AM", "Pepper", "27.1", "38", "70", "Normal" },
                    new[] { "May 24, 2025", "10:00 AM", "Cucumber", "25.6", "41", "64", "Normal" },
                    new[] { "May 24, 2025", "09:30 AM", "Tomato", "25.9", "28", "67", "Low Soil Moisture" }
                },
                new[] { 80, 65, 60, 55, 50, 50, 100 }
            );
            rdgv.Location = new Point(10, 40);
            rdgv.Size = new Size(475, 180);
            rdgv.CellFormatting += (s, e) =>
            {
                if (e.ColumnIndex == 6 && e.Value != null)
                {
                    if (e.Value.ToString() == "Normal")
                        e.CellStyle.ForeColor = Color.FromArgb(0, 190, 100);
                    else if (e.Value.ToString() == "Low Soil Moisture")
                        e.CellStyle.ForeColor = Color.FromArgb(255, 140, 0);
                }
            };
            readingsPanel.Controls.Add(rdgv);

            // Status icon legend
            var legendLabel = new Label
            {
                Text = "⚠ Low Soil Moisture  ●  ✔ Normal",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(140, 150, 170),
                Location = new Point(15, 230),
                AutoSize = true
            };
            readingsPanel.Controls.Add(legendLabel);
            Controls.Add(readingsPanel);

            // ─── BOTTOM SECTION ───
            int botY = 555;

            // Plants table
            var plantsPanel = new Panel { Location = new Point(20, botY), Size = new Size(340, 150), BackColor = Color.White };
            plantsPanel.Paint += (s, e) =>
            {
                var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
                using var path = new GraphicsPath();
                int w = plantsPanel.Width, h = plantsPanel.Height;
                path.AddArc(0, 0, 10, 10, 180, 90); path.AddArc(w - 10, 0, 10, 10, 270, 90);
                path.AddArc(w - 10, h - 10, 10, 10, 0, 90); path.AddArc(0, h - 10, 10, 10, 90, 90);
                path.CloseFigure(); plantsPanel.Region = new Region(path);
            };
            var plTitle = new Label { Text = "Registered Plants", Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.FromArgb(50, 55, 70), Location = new Point(12, 10), AutoSize = true };
            plantsPanel.Controls.Add(plTitle);
            var plgv = CreateTable(new[] { "Plant", "Type", "Planted On", "Status" }, new[] {
                new[] { "Tomato", "Fruit", "Apr 28, 2025", "Active" },
                new[] { "Lettuce", "Leafy Green", "Apr 30, 2025", "Active" },
                new[] { "Pepper", "Fruit", "Apr 29, 2025", "Active" },
                new[] { "Cucumber", "Fruit", "May 01, 2025", "Active" }
            });
            plgv.Location = new Point(10, 35);
            plgv.Size = new Size(320, 105);
            plantsPanel.Controls.Add(plgv);
            Controls.Add(plantsPanel);

            // Requirements panel
            var reqPanel = new Panel { Location = new Point(375, botY), Size = new Size(340, 150), BackColor = Color.White };
            reqPanel.Paint += (s, e) =>
            {
                var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
                using var path = new GraphicsPath();
                int w = reqPanel.Width, h = reqPanel.Height;
                path.AddArc(0, 0, 10, 10, 180, 90); path.AddArc(w - 10, 0, 10, 10, 270, 90);
                path.AddArc(w - 10, h - 10, 10, 10, 0, 90); path.AddArc(0, h - 10, 10, 10, 90, 90);
                path.CloseFigure(); reqPanel.Region = new Region(path);
            };
            var rqTitle = new Label { Text = "Requirements per plant", Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.FromArgb(50, 55, 70), Location = new Point(12, 10), AutoSize = true };
            reqPanel.Controls.Add(rqTitle);

            var comboPlant = new ComboBox
            {
                Text = "Tomato",
                Font = new Font("Segoe UI", 9),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(12, 32),
                Size = new Size(150, 24),
                FlatStyle = FlatStyle.Flat
            };
            comboPlant.Items.AddRange(new[] { "Tomato", "Lettuce", "Pepper", "Cucumber" });
            comboPlant.SelectedIndex = 0;
            reqPanel.Controls.Add(comboPlant);

            var rqgv = CreateTable(new[] { "Parameter", "Ideal Range", "Current Value" }, new[] {
                new[] { "Temperature", "22–28 °C", "26.4 °C" },
                new[] { "Soil Moisture", "30–60 %", "32 %" },
                new[] { "Air Humidity", "60–80 %", "68 %" }
            });
            rqgv.Location = new Point(10, 60);
            rqgv.Size = new Size(320, 80);
            reqPanel.Controls.Add(rqgv);
            Controls.Add(reqPanel);

            // Alerts panel
            var alertPanel = new Panel { Location = new Point(730, botY), Size = new Size(350, 150), BackColor = Color.White };
            alertPanel.Paint += (s, e) =>
            {
                var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
                using var path = new GraphicsPath();
                int w = alertPanel.Width, h = alertPanel.Height;
                path.AddArc(0, 0, 10, 10, 180, 90); path.AddArc(w - 10, 0, 10, 10, 270, 90);
                path.AddArc(w - 10, h - 10, 10, 10, 0, 90); path.AddArc(0, h - 10, 10, 10, 90, 90);
                path.CloseFigure(); alertPanel.Region = new Region(path);
            };
            var alTitle = new Label { Text = "Alerts", Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.FromArgb(50, 55, 70), Location = new Point(12, 10), AutoSize = true };
            alertPanel.Controls.Add(alTitle);

            string[] alerts = {
                "⚠ Low soil moisture for Tomato\n   Soil moisture is below the ideal range.\n   11:30 AM",
                "⚡ Irrigation override active\n   Irrigation is running in manual mode.\n   09:45 AM",
                "🔧 Scheduled maintenance\n   Sensor calibration recommended.\n   May 24, 08:00 AM"
            };

            int alertY = 35;
            foreach (var alert in alerts)
            {
                var lbl = new Label { Text = alert, Font = new Font("Segoe UI", 8), ForeColor = Color.FromArgb(80, 90, 110), Location = new Point(12, alertY), AutoSize = true };
                alertPanel.Controls.Add(lbl);
                alertY += 38;
            }
            Controls.Add(alertPanel);

            // ─── STATUS BAR ───
            var statusBar = new Panel { Location = new Point(0, 720), Size = new Size(1100, 30), BackColor = Color.FromArgb(240, 242, 245), Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right };

            var dot = new Label { Text = "●", Font = new Font("Segoe UI", 8), ForeColor = Color.FromArgb(0, 190, 100), Location = new Point(15, 7), AutoSize = true };
            statusBar.Controls.Add(dot);
            var statusText = new Label { Text = "System Online", Font = new Font("Segoe UI", 8), ForeColor = Color.FromArgb(100, 110, 130), Location = new Point(30, 7), AutoSize = true };
            statusBar.Controls.Add(statusText);
            var updatedText = new Label { Text = "Last updated: May 24, 2025 11:30 AM", Font = new Font("Segoe UI", 8), ForeColor = Color.FromArgb(140, 150, 170), Location = new Point(140, 7), AutoSize = true };
            statusBar.Controls.Add(updatedText);
            var versionText = new Label { Text = "AgroControl v1.2.0", Font = new Font("Segoe UI", 8), ForeColor = Color.FromArgb(100, 110, 130), Location = new Point(980, 7), AutoSize = true };
            statusBar.Controls.Add(versionText);
            Controls.Add(statusBar);
        }
    }
}
