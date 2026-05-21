using System;
using System.Windows.Forms;

namespace MovieRental
{
    public partial class NewMovieForm : Form
    {
        private TextBox txtTitle;
        private ComboBox cboGenre;
        private NumericUpDown numYear;
        private CheckBox chkAvailable;
        private Button btnSave;
        private Button btnCancel;
        private Panel panelHeader;
        private Label lblHeader;

        public NewMovieForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Add New Movie";
            this.Size = new System.Drawing.Size(500, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = System.Drawing.Color.WhiteSmoke;

            panelHeader = new Panel();
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 60;
            panelHeader.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);

            lblHeader = new Label();
            lblHeader.Text = "ADD NEW MOVIE";
            lblHeader.ForeColor = System.Drawing.Color.White;
            lblHeader.Font = new System.Drawing.Font("Segoe UI", 16, FontStyle.Bold);
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            lblHeader.Dock = DockStyle.Fill;
            panelHeader.Controls.Add(lblHeader);

            Label lblTitle = new Label();
            lblTitle.Text = "Title:";
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 11);
            lblTitle.Location = new System.Drawing.Point(40, 100);
            lblTitle.Size = new System.Drawing.Size(100, 30);

            txtTitle = new TextBox();
            txtTitle.Font = new System.Drawing.Font("Segoe UI", 11);
            txtTitle.Location = new System.Drawing.Point(150, 97);
            txtTitle.Size = new System.Drawing.Size(300, 28);

            Label lblGenre = new Label();
            lblGenre.Text = "Genre:";
            lblGenre.Font = new System.Drawing.Font("Segoe UI", 11);
            lblGenre.Location = new System.Drawing.Point(40, 150);
            lblGenre.Size = new System.Drawing.Size(100, 30);

            cboGenre = new ComboBox();
            cboGenre.Font = new System.Drawing.Font("Segoe UI", 11);
            cboGenre.Location = new System.Drawing.Point(150, 147);
            cboGenre.Size = new System.Drawing.Size(300, 28);
            cboGenre.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGenre.DataSource = Enum.GetValues(typeof(Genre));
            cboGenre.SelectedIndex = -1;

            Label lblYear = new Label();
            lblYear.Text = "Year:";
            lblYear.Font = new System.Drawing.Font("Segoe UI", 11);
            lblYear.Location = new System.Drawing.Point(40, 200);
            lblYear.Size = new System.Drawing.Size(100, 30);

            numYear = new NumericUpDown();
            numYear.Font = new System.Drawing.Font("Segoe UI", 11);
            numYear.Location = new System.Drawing.Point(150, 197);
            numYear.Size = new System.Drawing.Size(120, 28);
            numYear.Minimum = 1940;
            numYear.Maximum = DateTime.Now.Year;
            numYear.Value = DateTime.Now.Year;

            chkAvailable = new CheckBox();
            chkAvailable.Text = "Available for Rent";
            chkAvailable.Font = new System.Drawing.Font("Segoe UI", 11);
            chkAvailable.Location = new System.Drawing.Point(150, 250);
            chkAvailable.Size = new System.Drawing.Size(200, 30);
            chkAvailable.Checked = true;

            btnSave = new Button();
            btnSave.Text = "SAVE";
            btnSave.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Bold);
            btnSave.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.Location = new System.Drawing.Point(150, 320);
            btnSave.Size = new System.Drawing.Size(130, 40);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button();
            btnCancel.Text = "CANCEL";
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 11);
            btnCancel.BackColor = System.Drawing.Color.LightGray;
            btnCancel.Location = new System.Drawing.Point(320, 320);
            btnCancel.Size = new System.Drawing.Size(130, 40);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.Add(panelHeader);
            this.Controls.Add(lblTitle);
            this.Controls.Add(txtTitle);
            this.Controls.Add(lblGenre);
            this.Controls.Add(cboGenre);
            this.Controls.Add(lblYear);
            this.Controls.Add(numYear);
            this.Controls.Add(chkAvailable);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string title = txtTitle.Text;

                if (string.IsNullOrWhiteSpace(title))
                {
                    MessageBox.Show("Enter a title!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboGenre.SelectedItem == null)
                {
                    MessageBox.Show("Select a genre!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Genre genre = (Genre)cboGenre.SelectedItem;
                int year = (int)numYear.Value;
                bool isAvailable = chkAvailable.Checked;

                Movie newMovie = new Movie(title, genre, year, isAvailable);

                MessageBox.Show($"Movie '{title}' saved!\nID: {newMovie.MovieId}", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtTitle.Clear();
                cboGenre.SelectedIndex = -1;
                numYear.Value = DateTime.Now.Year;
                chkAvailable.Checked = true;
                txtTitle.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}