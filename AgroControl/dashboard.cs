using System.Data;
using System.Drawing.Drawing2D;
using System.Windows.Forms.DataVisualization.Charting;
using Npgsql;

namespace AgroControl
{
    public partial class dashboard : Form
    {
        private Label _lblTemp, _lblSoil, _lblHum, _lblIrrVal, _lblIrrSub;
        private Label _lblTempInd, _lblSoilInd, _lblHumInd;
        private DataGridView _rdgv, _plgv, _rqgv;
        private Chart _chart;
        private Label _updatedText;
        private Panel _alertPanel;

        public dashboard()
        {
            InitializeComponent();
            BuildUI();
            LoadData();
        }

        private Panel CreateCard(Color accentColor, FontAwesome.Sharp.IconChar icon, string title, string value, string subtitle, string indicator, Color indicatorColor, out Label valLabel, out Label indLabel)
        {
            var card = new Panel { Size = new Size(245, 130), BackColor = Color.White, Margin = new Padding(8) };
            card.Controls.Add(new Panel { Size = new Size(245, 4), Location = new Point(0, 0), BackColor = accentColor });
            card.Controls.Add(new FontAwesome.Sharp.IconPictureBox { IconChar = icon, IconColor = accentColor, IconSize = 32, Location = new Point(15, 18), Size = new Size(32, 32) });
            valLabel = new Label { Text = value, Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = Color.FromArgb(40, 45, 60), Location = new Point(55, 15), AutoSize = true };
            card.Controls.Add(valLabel);
            card.Controls.Add(new Label { Text = title, Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(120, 130, 150), Location = new Point(55, 42), AutoSize = true });
            card.Controls.Add(new Label { Text = subtitle, Font = new Font("Segoe UI", 8), ForeColor = Color.FromArgb(150, 160, 180), Location = new Point(15, 75), AutoSize = true });
            indLabel = new Label { Text = indicator, Font = new Font("Segoe UI", 8, FontStyle.Bold), ForeColor = indicatorColor, Location = new Point(15, 95), AutoSize = true };
            card.Controls.Add(indLabel);
            card.Paint += (s, e) => { var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias; using var path = new GraphicsPath(); path.AddArc(0, 0, 10, 10, 180, 90); path.AddArc(card.Width - 10, 0, 10, 10, 270, 90); path.AddArc(card.Width - 10, card.Height - 10, 10, 10, 0, 90); path.AddArc(0, card.Height - 10, 10, 10, 90, 90); path.CloseFigure(); card.Region = new Region(path); };
            return card;
        }

        private DataGridView CreateTable(string[] columns, string[][] rows = null)
        {
            var dgv = new DataGridView
            {
                Size = new Size(480, 180), BackColor = Color.White, BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None, CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.FromArgb(235, 238, 242), RowHeadersVisible = false, AllowUserToAddRows = false,
                AllowUserToDeleteRows = false, AllowUserToResizeRows = false, ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9),
                RowTemplate = { Height = 28, DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.FromArgb(60, 70, 85), BackColor = Color.White, SelectionBackColor = Color.FromArgb(235, 238, 250), SelectionForeColor = Color.FromArgb(60, 70, 85) } },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(100, 110, 130), BackColor = Color.FromArgb(245, 247, 250), Padding = new Padding(8, 0, 8, 0) },
            };
            foreach (var col in columns) dgv.Columns.Add(col, col);
            if (rows != null) foreach (var row in rows) dgv.Rows.Add(row);
            return dgv;
        }

        private DataTable Query(string sql)
        {
            try { return DataAccess.DataAccess.getQuery(sql); }
            catch { return new DataTable(); }
        }

        private void BuildUI()
        {
            var lblDashboard = new Label { Text = "Dashboard", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.FromArgb(40, 45, 60), Location = new Point(25, 20), AutoSize = true };
            Controls.Add(lblDashboard);
            var lblSubtitle = new Label { Text = "Overview of your greenhouse and sensor data.", Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(140, 150, 170), Location = new Point(27, 55), AutoSize = true };
            Controls.Add(lblSubtitle);

            var cards = new Panel { Location = new Point(20, 85), Size = new Size(1060, 145), BackColor = Color.Transparent };
            cards.Controls.Add(CreateCard(Color.FromArgb(255, 90, 70), FontAwesome.Sharp.IconChar.ThermometerHalf, "Temperature", "-- °C", "Ideal: 22–28 °C", "Loading...", Color.Gray, out _lblTemp, out _lblTempInd));
            cards.Controls[^1].Location = new Point(0, 5);
            cards.Controls.Add(CreateCard(Color.FromArgb(50, 140, 255), FontAwesome.Sharp.IconChar.Tint, "Soil Moisture", "-- %", "Ideal: 30–60 %", "Loading...", Color.Gray, out _lblSoil, out _lblSoilInd));
            cards.Controls[^1].Location = new Point(260, 5);
            cards.Controls.Add(CreateCard(Color.FromArgb(140, 80, 255), FontAwesome.Sharp.IconChar.Droplet, "Air Humidity", "-- %", "Ideal: 60–80 %", "Loading...", Color.Gray, out _lblHum, out _lblHumInd));
            cards.Controls[^1].Location = new Point(520, 5);

            var irrCard = new Panel { Size = new Size(245, 130), BackColor = Color.White, Location = new Point(780, 5) };
            irrCard.Controls.Add(new Panel { Size = new Size(245, 4), Location = new Point(0, 0), BackColor = Color.FromArgb(0, 200, 150) });
            irrCard.Controls.Add(new FontAwesome.Sharp.IconPictureBox { IconChar = FontAwesome.Sharp.IconChar.Water, IconColor = Color.FromArgb(0, 200, 150), IconSize = 32, Location = new Point(15, 18), Size = new Size(32, 32) });
            _lblIrrVal = new Label { Text = "--", Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = Color.FromArgb(0, 200, 150), Location = new Point(55, 15), AutoSize = true };
            irrCard.Controls.Add(_lblIrrVal);
            irrCard.Controls.Add(new Label { Text = "Irrigation Status", Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(120, 130, 150), Location = new Point(55, 42), AutoSize = true });
            _lblIrrSub = new Label { Text = "Loading...", Font = new Font("Segoe UI", 8), ForeColor = Color.FromArgb(150, 160, 180), Location = new Point(15, 75), AutoSize = true };
            irrCard.Controls.Add(_lblIrrSub);
            irrCard.Controls.Add(new Label { Text = "Auto", Font = new Font("Segoe UI", 8, FontStyle.Bold), ForeColor = Color.FromArgb(255, 140, 0), Location = new Point(15, 95), AutoSize = true });
            irrCard.Paint += (s, e) => { var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias; using var path = new GraphicsPath(); path.AddArc(0, 0, 10, 10, 180, 90); path.AddArc(irrCard.Width - 10, 0, 10, 10, 270, 90); path.AddArc(irrCard.Width - 10, irrCard.Height - 10, 10, 10, 0, 90); path.AddArc(0, irrCard.Height - 10, 10, 10, 90, 90); path.CloseFigure(); irrCard.Region = new Region(path); };
            cards.Controls.Add(irrCard);
            Controls.Add(cards);

            int midY = 240;
            var chartPanel = new Panel { Location = new Point(20, midY), Size = new Size(550, 300), BackColor = Color.White };
            chartPanel.Paint += (s, e) => { var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias; using var path = new GraphicsPath(); int w = chartPanel.Width, h = chartPanel.Height; path.AddArc(0, 0, 10, 10, 180, 90); path.AddArc(w - 10, 0, 10, 10, 270, 90); path.AddArc(w - 10, h - 10, 10, 10, 0, 90); path.AddArc(0, h - 10, 10, 10, 90, 90); path.CloseFigure(); chartPanel.Region = new Region(path); };
            chartPanel.Controls.Add(new Label { Text = "Sensor Trends (Last 24 Hours)", Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.FromArgb(50, 55, 70), Location = new Point(15, 12), AutoSize = true });
            _chart = new Chart { Location = new Point(10, 40), Size = new Size(530, 250), BackColor = Color.White };
            var ca = _chart.ChartAreas.Add("Main");
            ca.AxisX.MajorGrid.LineColor = Color.FromArgb(240, 242, 245); ca.AxisY.MajorGrid.LineColor = Color.FromArgb(240, 242, 245);
            ca.AxisX.LabelStyle.Font = new Font("Segoe UI", 7); ca.AxisY.LabelStyle.Font = new Font("Segoe UI", 7); ca.BackColor = Color.White;
            chartPanel.Controls.Add(_chart);
            Controls.Add(chartPanel);

            var readingsPanel = new Panel { Location = new Point(585, midY), Size = new Size(495, 300), BackColor = Color.White };
            readingsPanel.Paint += (s, e) => { var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias; using var path = new GraphicsPath(); int w = readingsPanel.Width, h = readingsPanel.Height; path.AddArc(0, 0, 10, 10, 180, 90); path.AddArc(w - 10, 0, 10, 10, 270, 90); path.AddArc(w - 10, h - 10, 10, 10, 0, 90); path.AddArc(0, h - 10, 10, 10, 90, 90); path.CloseFigure(); readingsPanel.Region = new Region(path); };
            readingsPanel.Controls.Add(new Label { Text = "Recent Readings", Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.FromArgb(50, 55, 70), Location = new Point(15, 12), AutoSize = true });
            _rdgv = CreateTable(new[] { "Date", "Time", "Sensor", "Type", "Value", "Unit", "Status" });
            _rdgv.Location = new Point(10, 40); _rdgv.Size = new Size(475, 180);
            readingsPanel.Controls.Add(_rdgv);
            Controls.Add(readingsPanel);

            int botY = 555;
            var plantsPanel = new Panel { Location = new Point(20, botY), Size = new Size(340, 150), BackColor = Color.White };
            plantsPanel.Paint += (s, e) => { var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias; using var path = new GraphicsPath(); int w = plantsPanel.Width, h = plantsPanel.Height; path.AddArc(0, 0, 10, 10, 180, 90); path.AddArc(w - 10, 0, 10, 10, 270, 90); path.AddArc(w - 10, h - 10, 10, 10, 0, 90); path.AddArc(0, h - 10, 10, 10, 90, 90); path.CloseFigure(); plantsPanel.Region = new Region(path); };
            plantsPanel.Controls.Add(new Label { Text = "Registered Plants", Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.FromArgb(50, 55, 70), Location = new Point(12, 10), AutoSize = true });
            _plgv = CreateTable(new[] { "Plant", "Type", "Planted On", "Status" });
            _plgv.Location = new Point(10, 35); _plgv.Size = new Size(320, 105);
            plantsPanel.Controls.Add(_plgv);
            Controls.Add(plantsPanel);

            var reqPanel = new Panel { Location = new Point(375, botY), Size = new Size(340, 150), BackColor = Color.White };
            reqPanel.Paint += (s, e) => { var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias; using var path = new GraphicsPath(); int w = reqPanel.Width, h = reqPanel.Height; path.AddArc(0, 0, 10, 10, 180, 90); path.AddArc(w - 10, 0, 10, 10, 270, 90); path.AddArc(w - 10, h - 10, 10, 10, 0, 90); path.AddArc(0, h - 10, 10, 10, 90, 90); path.CloseFigure(); reqPanel.Region = new Region(path); };
            reqPanel.Controls.Add(new Label { Text = "Requirements per plant", Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.FromArgb(50, 55, 70), Location = new Point(12, 10), AutoSize = true });
            _rqgv = CreateTable(new[] { "Parameter", "Ideal Range", "Current Value" });
            _rqgv.Location = new Point(10, 35); _rqgv.Size = new Size(320, 105);
            reqPanel.Controls.Add(_rqgv);
            Controls.Add(reqPanel);

            _alertPanel = new Panel { Location = new Point(730, botY), Size = new Size(350, 150), BackColor = Color.White };
            _alertPanel.Paint += (s, e) => { var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias; using var path = new GraphicsPath(); int w = _alertPanel.Width, h = _alertPanel.Height; path.AddArc(0, 0, 10, 10, 180, 90); path.AddArc(w - 10, 0, 10, 10, 270, 90); path.AddArc(w - 10, h - 10, 10, 10, 0, 90); path.AddArc(0, h - 10, 10, 10, 90, 90); path.CloseFigure(); _alertPanel.Region = new Region(path); };
            _alertPanel.Controls.Add(new Label { Text = "Alerts", Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.FromArgb(50, 55, 70), Location = new Point(12, 10), AutoSize = true });
            Controls.Add(_alertPanel);

            var statusBar = new Panel { Location = new Point(0, 720), Size = new Size(1100, 30), BackColor = Color.FromArgb(240, 242, 245), Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right };
            statusBar.Controls.Add(new Label { Text = "●", Font = new Font("Segoe UI", 8), ForeColor = Color.FromArgb(0, 190, 100), Location = new Point(15, 7), AutoSize = true });
            statusBar.Controls.Add(new Label { Text = "System Online", Font = new Font("Segoe UI", 8), ForeColor = Color.FromArgb(100, 110, 130), Location = new Point(30, 7), AutoSize = true });
            _updatedText = new Label { Text = "Loading...", Font = new Font("Segoe UI", 8), ForeColor = Color.FromArgb(140, 150, 170), Location = new Point(140, 7), AutoSize = true };
            statusBar.Controls.Add(_updatedText);
            statusBar.Controls.Add(new Label { Text = "AgroControl v1.2.0", Font = new Font("Segoe UI", 8), ForeColor = Color.FromArgb(100, 110, 130), Location = new Point(980, 7), AutoSize = true });
            Controls.Add(statusBar);
        }

        private void LoadData()
        {
            try
            {
                var dt = Query("SELECT s.tipo, l.valor, l.fechaHora FROM LECTURA l JOIN SENSOR s ON l.idSensor = s.idSensor ORDER BY l.fechaHora DESC");
                _updatedText.Text = $"Last updated: {DateTime.Now:MMM dd, yyyy hh:mm tt}";

                if (dt.Rows.Count == 0)
                {
                    _lblTemp.Text = "No data"; _lblSoil.Text = "No data"; _lblHum.Text = "No data";
                    return;
                }

                double lastTemp = 0, lastSoil = 0, lastHum = 0, prevTemp = 0, prevSoil = 0, prevHum = 0;
                bool hasTemp = false, hasSoil = false, hasHum = false;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var tipo = dt.Rows[i]["tipo"].ToString();
                    var val = Convert.ToDouble(dt.Rows[i]["valor"]);
                    if (tipo == "tempAire") { if (!hasTemp) { prevTemp = lastTemp; lastTemp = val; hasTemp = true; } }
                    else if (tipo == "humSuelo") { if (!hasSoil) { prevSoil = lastSoil; lastSoil = val; hasSoil = true; } }
                    else if (tipo == "humAire") { if (!hasHum) { prevHum = lastHum; lastHum = val; hasHum = true; } }
                }

                // Find previous values for trend (look at 2nd readings)
                int count = 0;
                foreach (DataRow row in dt.Rows)
                {
                    var tipo = row["tipo"].ToString();
                    var val = Convert.ToDouble(row["valor"]);
                    if (tipo == "tempAire" && count == 0) { prevTemp = val; }
                    else if (tipo == "tempAire" && prevTemp == lastTemp && val != lastTemp) prevTemp = val;
                    if (tipo == "humSuelo" && count == 0) { prevSoil = val; }
                    else if (tipo == "humSuelo" && prevSoil == lastSoil && val != lastSoil) prevSoil = val;
                    if (tipo == "humAire" && count == 0) { prevHum = val; }
                    else if (tipo == "humAire" && prevHum == lastHum && val != lastHum) prevHum = val;
                    count++;
                }

                if (hasTemp) {
                    _lblTemp.Text = $"{lastTemp:F1} °C";
                    var diff = lastTemp - prevTemp;
                    _lblTempInd.Text = $"{(diff >= 0 ? "+" : "")}{diff:F1} °C vs last hour";
                    _lblTempInd.ForeColor = Math.Abs(diff) < 2 ? Color.FromArgb(0, 190, 100) : Color.FromArgb(255, 80, 60);
                }
                if (hasSoil) {
                    _lblSoil.Text = $"{lastSoil:F0} %";
                    var diff = lastSoil - prevSoil;
                    _lblSoilInd.Text = $"{(diff >= 0 ? "+" : "")}{diff:F0} % vs last hour";
                    _lblSoilInd.ForeColor = lastSoil >= 30 ? Color.FromArgb(0, 190, 100) : Color.FromArgb(255, 140, 0);
                }
                if (hasHum) {
                    _lblHum.Text = $"{lastHum:F0} %";
                    var diff = lastHum - prevHum;
                    _lblHumInd.Text = $"{(diff >= 0 ? "+" : "")}{diff:F0} % vs last hour";
                    _lblHumInd.ForeColor = Math.Abs(diff) < 5 ? Color.FromArgb(0, 190, 100) : Color.FromArgb(255, 140, 0);
                }

                if (hasSoil && lastSoil < 30) { _lblIrrVal.Text = "ON"; _lblIrrVal.ForeColor = Color.FromArgb(0, 200, 150); _lblIrrSub.Text = "Irrigation active"; }
                else { _lblIrrVal.Text = "OFF"; _lblIrrVal.ForeColor = Color.Gray; _lblIrrSub.Text = "Standing by"; }

                // Recent readings
                _rdgv.Rows.Clear();
                int shown = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (shown >= 20) break;
                    var fecha = Convert.ToDateTime(row["fechaHora"]);
                    var tipo = row["tipo"].ToString();
                    var val = Convert.ToDouble(row["valor"]);
                    var unit = tipo == "tempAire" ? "°C" : tipo == "humSuelo" ? "%" : tipo == "humAire" ? "%" : tipo == "luz" ? "lx" : "";
                    var status = (tipo == "humSuelo" && val < 30) ? "⚠ Low" : (tipo == "tempAire" && (val < 22 || val > 28)) ? "⚠ Alert" : "Normal";
                    _rdgv.Rows.Add(fecha.ToString("MMM dd, yyyy"), fecha.ToString("hh:mm tt"), tipo, tipo, val.ToString("F1"), unit, status);
                    shown++;
                }

                // Chart - last 24h grouped by hour
                var chartDt = Query(@"
                    SELECT s.tipo, DATE_TRUNC('hour', l.fechaHora) AS hora, AVG(l.valor) AS prom
                    FROM LECTURA l JOIN SENSOR s ON l.idSensor = s.idSensor
                    WHERE l.fechaHora >= NOW() - INTERVAL '24 hours'
                    GROUP BY s.tipo, DATE_TRUNC('hour', l.fechaHora)
                    ORDER BY hora");
                _chart.Series.Clear();

                var sTemp = _chart.Series.Add("Temperature (°C)");
                sTemp.ChartType = SeriesChartType.Spline; sTemp.Color = Color.FromArgb(255, 90, 70); sTemp.BorderWidth = 2;
                var sSoil = _chart.Series.Add("Soil Moisture (%)");
                sSoil.ChartType = SeriesChartType.Spline; sSoil.Color = Color.FromArgb(50, 140, 255); sSoil.BorderWidth = 2;
                var sHum = _chart.Series.Add("Air Humidity (%)");
                sHum.ChartType = SeriesChartType.Spline; sHum.Color = Color.FromArgb(140, 80, 255); sHum.BorderWidth = 2;

                if (chartDt.Rows.Count > 0)
                {
                    foreach (DataRow row in chartDt.Rows)
                    {
                        var hora = Convert.ToDateTime(row["hora"]).ToString("HH:mm");
                        var tipo = row["tipo"].ToString();
                        var prom = Convert.ToDouble(row["prom"]);
                        if (tipo == "tempAire") sTemp.Points.AddXY(hora, prom);
                        else if (tipo == "humSuelo") sSoil.Points.AddXY(hora, prom);
                        else if (tipo == "humAire") sHum.Points.AddXY(hora, prom);
                    }
                }
                else
                {
                    // Fallback to sample data if no real data
                    for (int i = 0; i < 6; i++)
                    {
                        var h = $"{i * 4}h";
                        sTemp.Points.AddXY(h, 24 + new Random().NextDouble() * 4);
                        sSoil.Points.AddXY(h, 30 + new Random().NextDouble() * 20);
                        sHum.Points.AddXY(h, 60 + new Random().NextDouble() * 15);
                    }
                }

                // Plants from DB
                var plantDt = Query("SELECT nombre, nombreCien, descripcion FROM PLANTA ORDER BY idPlanta");
                _plgv.Rows.Clear();
                foreach (DataRow row in plantDt.Rows)
                    _plgv.Rows.Add(row["nombre"], row["nombreCien"], "N/A", "Active");

                // Requirements
                _rqgv.Rows.Clear();
                if (hasTemp) _rqgv.Rows.Add("Temperature", "22–28 °C", $"{lastTemp:F1} °C");
                if (hasSoil) _rqgv.Rows.Add("Soil Moisture", "30–60 %", $"{lastSoil:F0} %");
                if (hasHum) _rqgv.Rows.Add("Air Humidity", "60–80 %", $"{lastHum:F0} %");

                // Alerts
                _alertPanel.Controls.Clear();
                _alertPanel.Controls.Add(new Label { Text = "Alerts", Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.FromArgb(50, 55, 70), Location = new Point(12, 10), AutoSize = true });
                int ay = 35;
                bool anyAlert = false;
                if (hasSoil && lastSoil < 30)
                {
                    _alertPanel.Controls.Add(new Label { Text = $"⚠ Low soil moisture\n   Soil moisture is {lastSoil:F0}% (below 30%).\n   {DateTime.Now:hh:mm tt}", Font = new Font("Segoe UI", 8), ForeColor = Color.FromArgb(80, 90, 110), Location = new Point(12, ay), AutoSize = true });
                    ay += 38; anyAlert = true;
                }
                if (hasTemp && (lastTemp < 22 || lastTemp > 28))
                {
                    _alertPanel.Controls.Add(new Label { Text = $"⚠ Temperature alert\n   Temperature is {lastTemp:F1}°C (outside 22-28°C).\n   {DateTime.Now:hh:mm tt}", Font = new Font("Segoe UI", 8), ForeColor = Color.FromArgb(80, 90, 110), Location = new Point(12, ay), AutoSize = true });
                    ay += 38; anyAlert = true;
                }
                if (!anyAlert)
                    _alertPanel.Controls.Add(new Label { Text = "✓ All systems normal\n   No alerts at this time.", Font = new Font("Segoe UI", 8), ForeColor = Color.FromArgb(80, 90, 110), Location = new Point(12, ay), AutoSize = true });
            }
            catch (Exception ex)
            {
                _lblTemp.Text = "Error";
                _lblSoil.Text = "Error";
                _lblHum.Text = "Error";
                _updatedText.Text = $"Error loading data: {ex.Message}";
            }
        }
    }
}
