using System.Drawing.Drawing2D;

namespace AgroControl
{
    public partial class helpForm : Form
    {
        public helpForm()
        {
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            var title = new Label
            {
                Text = "Help Center",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 45, 60),
                Location = new Point(25, 20),
                AutoSize = true
            };
            Controls.Add(title);

            var subtitle = new Label
            {
                Text = "Manual de uso, guía de conexión Arduino y solución de errores.",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(140, 150, 170),
                Location = new Point(27, 55),
                AutoSize = true
            };
            Controls.Add(subtitle);

            string[][] sections = new[] {
                new[] { "📡 Connection Setup", "Configure la conexión con Supabase:\n1. Abra Settings\n2. Ingrese el host, puerto y credenciales\n3. Verifique que Cloudflare WARP esté activo\n4. Pruebe la conexión" },
                new[] { "🌱 Plant Management", "Registro y monitoreo de plantas:\n1. Vaya a Plant Records\n2. Agregue nuevas plantas con nombre y tipo\n3. Vea los requerimientos ideales\n4. Monitoree el estado de cada planta" },
                new[] { "📊 Sensor Data", "Lecturas de sensores Arduino:\n1. Los sensores envían datos cada 30 min\n2. Vea el histórico en Reading Log\n3. Filtre por fecha o tipo de sensor\n4. Los gráficos muestran tendencias 24h" },
                new[] { "⚠️ Troubleshooting", "Problemas comunes:\n• Si no ve datos, verifique WARP\n• Si el login falla, revise credenciales\n• Si los gráficos están vacíos, espere la\n  próxima lectura de sensores\n• Para errores de BD, contacte al admin" }
            };

            int y = 90;
            foreach (var sec in sections)
            {
                var panel = new Panel
                {
                    Location = new Point(25, y),
                    Size = new Size(950, 110),
                    BackColor = Color.White
                };
                panel.Paint += (s, e) =>
                {
                    var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
                    using var path = new GraphicsPath();
                    int w = panel.Width, h = panel.Height;
                    path.AddArc(0, 0, 10, 10, 180, 90); path.AddArc(w - 10, 0, 10, 10, 270, 90);
                    path.AddArc(w - 10, h - 10, 10, 10, 0, 90); path.AddArc(0, h - 10, 10, 10, 90, 90);
                    path.CloseFigure(); panel.Region = new Region(path);
                };

                var secTitle = new Label
                {
                    Text = sec[0],
                    Font = new Font("Segoe UI", 13, FontStyle.Bold),
                    ForeColor = Color.FromArgb(70, 72, 230),
                    Location = new Point(15, 10),
                    AutoSize = true
                };
                panel.Controls.Add(secTitle);

                var secBody = new Label
                {
                    Text = sec[1],
                    Font = new Font("Segoe UI", 10),
                    ForeColor = Color.FromArgb(80, 90, 110),
                    Location = new Point(15, 38),
                    AutoSize = true
                };
                panel.Controls.Add(secBody);

                Controls.Add(panel);
                y += 125;
            }

            var footer = new Label
            {
                Text = "AgroControl v1.2.0 | Documentación completa en: github.com/AgroControl",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(140, 150, 170),
                Location = new Point(25, y + 10),
                AutoSize = true
            };
            Controls.Add(footer);
        }
    }
}
