using BusinessLogic;
using Entities;
using System.Drawing.Drawing2D;

namespace AgroControl
{
    public partial class Interfaz : Form
    {
        private Usuario _usuarioLogueado;
        private Button _activeMenu;

        public Interfaz()
        {
            InitializeComponent();
        }

        public Interfaz(Usuario usuarioQueIngreso)
        {
            InitializeComponent();
            _usuarioLogueado = usuarioQueIngreso;
            label2.Text = _usuarioLogueado.Nombre;
            this.Text = "AgroControl - " + _usuarioLogueado.Nombre;
            HighlightMenu(btnDasboard);
            openSonForm(new dashboard());
            panelTitleBar.Paint += PanelTitleBar_Paint;
        }

        private void PanelTitleBar_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var rect = panelTitleBar.ClientRectangle;
            using var brush = new LinearGradientBrush(rect, Color.FromArgb(84, 86, 240), Color.FromArgb(120, 80, 240), LinearGradientMode.Horizontal);
            g.FillRectangle(brush, rect);
        }

        private void HighlightMenu(Button btn)
        {
            foreach (Control c in panelMenu.Controls)
            {
                if (c is Button b && b != iconButton8)
                {
                    b.BackColor = Color.FromArgb(84, 86, 240);
                    b.ForeColor = Color.WhiteSmoke;
                }
            }
            if (btn != null)
            {
                btn.BackColor = Color.FromArgb(110, 112, 255);
                btn.ForeColor = Color.White;
                _activeMenu = btn;
            }
        }

        private void openSonForm(Form SonForm)
        {
            if (panelDesktop.Controls.Count > 0)
                panelDesktop.Controls.RemoveAt(0);

            SonForm.TopLevel = false;
            SonForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(SonForm);
            panelDesktop.Tag = SonForm;
            SonForm.Show();
        }

        private void btnDasboard_Click(object sender, EventArgs e)
        {
            label1.Text = "Dashboard";
            HighlightMenu(btnDasboard);
            openSonForm(new dashboard());
        }

        private void btnReadingLog_Click(object sender, EventArgs e)
        {
            label1.Text = "Reading Log";
            HighlightMenu(btnReadingLog);
            openSonForm(new readingLog());
        }

        private void btnCharts_Click(object sender, EventArgs e)
        {
            label1.Text = "Charts";
            HighlightMenu(btnCharts);
            openSonForm(new charts());
        }

        private void btnPlantRecords_Click(object sender, EventArgs e)
        {
            label1.Text = "Plant Records";
            HighlightMenu(btnPlantRecords);
            openSonForm(new plantRecord());
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            label1.Text = "Settings";
            HighlightMenu(btnSettings);
            openSonForm(new settings());
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            label1.Text = "Help";
            HighlightMenu(null);
            openSonForm(new helpForm());
        }

        private void Interfaz_Load(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void pictureBox1_Click_1(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
    }
}
