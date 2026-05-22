using System;
using System.Linq;
using System.Windows.Forms;

namespace MovieRental
{
    public partial class NewClientForm : Form
    {
        private DataGridView dgvClients;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private TextBox txtEmail;
        private TextBox txtEditEmail;
        private Button btnSave;
        private Button btnUpdate;
        private Panel panelHeader;
        private Label lblHeader;
        private ErrorProvider errorProvider;
        private Label lblSelectedClientName;
        private Client selectedClient;

        public NewClientForm()
        {
            InitializeComponent();
            LoadClients();
        }

        private void InitializeComponent()
        {
            this.Text = "Manage Clients";
            this.Size = new System.Drawing.Size(950, 580);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = System.Drawing.Color.WhiteSmoke;

            errorProvider = new ErrorProvider();

            panelHeader = new Panel();
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 60;
            panelHeader.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);

            lblHeader = new Label();
            lblHeader.Text = "MANAGE CLIENTS";
            lblHeader.ForeColor = System.Drawing.Color.White;
            lblHeader.Font = new System.Drawing.Font("Segoe UI", 18, FontStyle.Bold);
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            lblHeader.Dock = DockStyle.Fill;
            panelHeader.Controls.Add(lblHeader);

            dgvClients = new DataGridView();
            dgvClients.Location = new System.Drawing.Point(20, 80);
            dgvClients.Size = new System.Drawing.Size(500, 420);
            dgvClients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClients.AllowUserToAddRows = false;
            dgvClients.AllowUserToDeleteRows = false;
            dgvClients.ReadOnly = true;
            dgvClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClients.BackgroundColor = System.Drawing.Color.White;
            dgvClients.BorderStyle = BorderStyle.FixedSingle;
            dgvClients.MultiSelect = false;
            dgvClients.CellClick += DgvClients_CellClick;

            GroupBox grpAdd = new GroupBox();
            grpAdd.Text = "ADD NEW CLIENT";
            grpAdd.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            grpAdd.Location = new System.Drawing.Point(540, 80);
            grpAdd.Size = new System.Drawing.Size(380, 220);
            grpAdd.BackColor = System.Drawing.Color.WhiteSmoke;

            Label lblFirstName = new Label();
            lblFirstName.Text = "First Name:";
            lblFirstName.Font = new System.Drawing.Font("Segoe UI", 10);
            lblFirstName.Location = new System.Drawing.Point(15, 30);
            lblFirstName.Size = new System.Drawing.Size(100, 25);
            lblFirstName.TextAlign = ContentAlignment.MiddleRight;

            txtFirstName = new TextBox();
            txtFirstName.Font = new System.Drawing.Font("Segoe UI", 10);
            txtFirstName.Location = new System.Drawing.Point(120, 27);
            txtFirstName.Size = new System.Drawing.Size(240, 25);

            Label lblLastName = new Label();
            lblLastName.Text = "Last Name:";
            lblLastName.Font = new System.Drawing.Font("Segoe UI", 10);
            lblLastName.Location = new System.Drawing.Point(15, 70);
            lblLastName.Size = new System.Drawing.Size(100, 25);
            lblLastName.TextAlign = ContentAlignment.MiddleRight;

            txtLastName = new TextBox();
            txtLastName.Font = new System.Drawing.Font("Segoe UI", 10);
            txtLastName.Location = new System.Drawing.Point(120, 67);
            txtLastName.Size = new System.Drawing.Size(240, 25);

            Label lblEmail = new Label();
            lblEmail.Text = "Email:";
            lblEmail.Font = new System.Drawing.Font("Segoe UI", 10);
            lblEmail.Location = new System.Drawing.Point(15, 110);
            lblEmail.Size = new System.Drawing.Size(100, 25);
            lblEmail.TextAlign = ContentAlignment.MiddleRight;

            txtEmail = new TextBox();
            txtEmail.Font = new System.Drawing.Font("Segoe UI", 10);
            txtEmail.Location = new System.Drawing.Point(120, 107);
            txtEmail.Size = new System.Drawing.Size(240, 25);

            btnSave = new Button();
            btnSave.Text = "&Add Client";
            btnSave.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            btnSave.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.Location = new System.Drawing.Point(120, 160);
            btnSave.Size = new System.Drawing.Size(120, 30);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Click += BtnSave_Click;

            grpAdd.Controls.Add(lblFirstName);
            grpAdd.Controls.Add(txtFirstName);
            grpAdd.Controls.Add(lblLastName);
            grpAdd.Controls.Add(txtLastName);
            grpAdd.Controls.Add(lblEmail);
            grpAdd.Controls.Add(txtEmail);
            grpAdd.Controls.Add(btnSave);

            GroupBox grpEdit = new GroupBox();
            grpEdit.Text = "EDIT CLIENT EMAIL";
            grpEdit.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            grpEdit.Location = new System.Drawing.Point(540, 320);
            grpEdit.Size = new System.Drawing.Size(380, 150);
            grpEdit.BackColor = System.Drawing.Color.WhiteSmoke;

            Label lblSelected = new Label();
            lblSelected.Text = "Selected Client:";
            lblSelected.Font = new System.Drawing.Font("Segoe UI", 9);
            lblSelected.Location = new System.Drawing.Point(15, 30);
            lblSelected.Size = new System.Drawing.Size(100, 25);
            lblSelected.TextAlign = ContentAlignment.MiddleRight;

            lblSelectedClientName = new Label();
            lblSelectedClientName.Text = "-";
            lblSelectedClientName.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
            lblSelectedClientName.Location = new System.Drawing.Point(120, 30);
            lblSelectedClientName.Size = new System.Drawing.Size(240, 25);

            Label lblEditEmail = new Label();
            lblEditEmail.Text = "New Email:";
            lblEditEmail.Font = new System.Drawing.Font("Segoe UI", 10);
            lblEditEmail.Location = new System.Drawing.Point(15, 65);
            lblEditEmail.Size = new System.Drawing.Size(100, 25);
            lblEditEmail.TextAlign = ContentAlignment.MiddleRight;

            txtEditEmail = new TextBox();
            txtEditEmail.Font = new System.Drawing.Font("Segoe UI", 10);
            txtEditEmail.Location = new System.Drawing.Point(120, 62);
            txtEditEmail.Size = new System.Drawing.Size(240, 25);

            btnUpdate = new Button();
            btnUpdate.Text = "&Update Email";
            btnUpdate.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            btnUpdate.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);
            btnUpdate.ForeColor = System.Drawing.Color.White;
            btnUpdate.Location = new System.Drawing.Point(120, 100);
            btnUpdate.Size = new System.Drawing.Size(150, 35);
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Enabled = false;
            btnUpdate.Click += BtnUpdate_Click;

            grpEdit.Controls.Add(lblSelected);
            grpEdit.Controls.Add(lblSelectedClientName);
            grpEdit.Controls.Add(lblEditEmail);
            grpEdit.Controls.Add(txtEditEmail);
            grpEdit.Controls.Add(btnUpdate);

            this.Controls.Add(panelHeader);
            this.Controls.Add(dgvClients);
            this.Controls.Add(grpAdd);
            this.Controls.Add(grpEdit);
        }

        private void LoadClients()
        {
            var clients = Client.GetAllClients().Select(c => new
            {
                c.ClientId,
                c.FirstName,
                c.LastName,
                c.Email
            }).ToList();

            dgvClients.DataSource = null;
            dgvClients.DataSource = clients;

            if (dgvClients.Columns.Contains("ClientId"))
                dgvClients.Columns["ClientId"].HeaderText = "ID";
            if (dgvClients.Columns.Contains("FirstName"))
                dgvClients.Columns["FirstName"].HeaderText = "First Name";
            if (dgvClients.Columns.Contains("LastName"))
                dgvClients.Columns["LastName"].HeaderText = "Last Name";
            if (dgvClients.Columns.Contains("Email"))
                dgvClients.Columns["Email"].HeaderText = "Email";
        }

        private void DgvClients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            errorProvider.Clear();

            if (e.RowIndex >= 0)
            {
                int clientId = (int)dgvClients.Rows[e.RowIndex].Cells["ClientId"].Value;
                selectedClient = Client.GetAllClients().FirstOrDefault(c => c.ClientId == clientId);

                if (selectedClient != null)
                {
                    lblSelectedClientName.Text = $"{selectedClient.FirstName} {selectedClient.LastName}";
                    txtEditEmail.Text = selectedClient.Email;
                    btnUpdate.Enabled = true;
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                errorProvider.SetError(txtFirstName, "First name is required!");
                txtFirstName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                errorProvider.SetError(txtLastName, "Last name is required!");
                txtLastName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                errorProvider.SetError(txtEmail, "Email is required!");
                txtEmail.Focus();
                return;
            }

            if (!txtEmail.Text.Contains("@"))
            {
                errorProvider.SetError(txtEmail, "Enter a valid email address!");
                txtEmail.Focus();
                return;
            }

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
                errorProvider.Clear();
                txtFirstName.Focus();
                LoadClients();

                var mainForm = Application.OpenForms.OfType<MenuForm>().FirstOrDefault();
                if (mainForm != null) mainForm.UpdateStatusStrip();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedClient == null)
            {
                MessageBox.Show("Select a client to update!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(txtEditEmail.Text))
            {
                errorProvider.SetError(txtEditEmail, "Email is required!");
                txtEditEmail.Focus();
                return;
            }

            if (!txtEditEmail.Text.Contains("@"))
            {
                errorProvider.SetError(txtEditEmail, "Enter a valid email address!");
                txtEditEmail.Focus();
                return;
            }

            selectedClient.Email = txtEditEmail.Text;
            Client.SaveToFile();

            MessageBox.Show($"Client '{selectedClient.FirstName} {selectedClient.LastName}' email updated!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadClients();
            btnUpdate.Enabled = false;
            lblSelectedClientName.Text = "-";
            txtEditEmail.Clear();
            errorProvider.Clear();

            var mainForm = Application.OpenForms.OfType<MenuForm>().FirstOrDefault();
            if (mainForm != null) mainForm.UpdateStatusStrip();
        }
    }
}