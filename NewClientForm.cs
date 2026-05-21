using System;
using System.Windows.Forms;

namespace MovieRental
{
    public partial class NewClientForm : Form
    {
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private TextBox txtEmail;
        private Button btnSave;
        private Button btnCancel;
        private Panel panelHeader;
        private Label lblHeader;

        public NewClientForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Add New Client";
            this.Size = new System.Drawing.Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = System.Drawing.Color.WhiteSmoke;

            panelHeader = new Panel();
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 60;
            panelHeader.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);

            lblHeader = new Label();
            lblHeader.Text = "ADD NEW CLIENT";
            lblHeader.ForeColor = System.Drawing.Color.White;
            lblHeader.Font = new System.Drawing.Font("Segoe UI", 16, FontStyle.Bold);
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            lblHeader.Dock = DockStyle.Fill;
            panelHeader.Controls.Add(lblHeader);

            Label lblFirstName = new Label();
            lblFirstName.Text = "First Name:";
            lblFirstName.Font = new System.Drawing.Font("Segoe UI", 11);
            lblFirstName.Location = new System.Drawing.Point(40, 100);
            lblFirstName.Size = new System.Drawing.Size(120, 30);

            txtFirstName = new TextBox();
            txtFirstName.Font = new System.Drawing.Font("Segoe UI", 11);
            txtFirstName.Location = new System.Drawing.Point(170, 97);
            txtFirstName.Size = new System.Drawing.Size(280, 28);

            Label lblLastName = new Label();
            lblLastName.Text = "Last Name:";
            lblLastName.Font = new System.Drawing.Font("Segoe UI", 11);
            lblLastName.Location = new System.Drawing.Point(40, 150);
            lblLastName.Size = new System.Drawing.Size(120, 30);

            txtLastName = new TextBox();
            txtLastName.Font = new System.Drawing.Font("Segoe UI", 11);
            txtLastName.Location = new System.Drawing.Point(170, 147);
            txtLastName.Size = new System.Drawing.Size(280, 28);

            Label lblEmail = new Label();
            lblEmail.Text = "Email:";
            lblEmail.Font = new System.Drawing.Font("Segoe UI", 11);
            lblEmail.Location = new System.Drawing.Point(40, 200);
            lblEmail.Size = new System.Drawing.Size(120, 30);

            txtEmail = new TextBox();
            txtEmail.Font = new System.Drawing.Font("Segoe UI", 11);
            txtEmail.Location = new System.Drawing.Point(170, 197);
            txtEmail.Size = new System.Drawing.Size(280, 28);

            btnSave = new Button();
            btnSave.Text = "SAVE";
            btnSave.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Bold);
            btnSave.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.Location = new System.Drawing.Point(170, 280);
            btnSave.Size = new System.Drawing.Size(130, 40);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button();
            btnCancel.Text = "CANCEL";
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 11);
            btnCancel.BackColor = System.Drawing.Color.LightGray;
            btnCancel.Location = new System.Drawing.Point(320, 280);
            btnCancel.Size = new System.Drawing.Size(130, 40);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.Add(panelHeader);
            this.Controls.Add(lblFirstName);
            this.Controls.Add(txtFirstName);
            this.Controls.Add(lblLastName);
            this.Controls.Add(txtLastName);
            this.Controls.Add(lblEmail);
            this.Controls.Add(txtEmail);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string firstName = txtFirstName.Text;
                string lastName = txtLastName.Text;
                string email = txtEmail.Text;

                Client newClient = new Client(firstName, lastName, email);

                MessageBox.Show($"Client '{firstName} {lastName}' saved!\nID: {newClient.ClientId}", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtFirstName.Clear();
                txtLastName.Clear();
                txtEmail.Clear();
                txtFirstName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}