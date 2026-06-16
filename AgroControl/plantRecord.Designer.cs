namespace AgroControl
{
    partial class plantRecord
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
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 500);
            FormBorderStyle = FormBorderStyle.None;
            Name = "plantRecord";
            Text = "Plant Records";
            BackColor = Color.FromArgb(245, 247, 250);
        }
    }
}
