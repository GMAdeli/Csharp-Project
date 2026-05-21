using System;
using System.Linq;
using System.Windows.Forms;

namespace MovieRental
{
    public partial class NewMovieForm : Form
    {
        private DataGridView dgvMovies;
        private TextBox txtTitle;
        private ComboBox cboGenre;
        private NumericUpDown numYear;
        private NumericUpDown numPrice;
        private CheckBox chkAvailable;
        private Button btnSave;
        private Button btnDelete;
        private Panel panelHeader;
        private Label lblHeader;
        private Label lblSelectedMovieName;
        private ErrorProvider errorProvider;

        private Movie selectedMovie;

        public NewMovieForm()
        {
            InitializeComponent();
            LoadMovies();
        }

        private void InitializeComponent()
        {
            this.Text = "Manage Movies";
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
            lblHeader.Text = "MANAGE MOVIES";
            lblHeader.ForeColor = System.Drawing.Color.White;
            lblHeader.Font = new System.Drawing.Font("Segoe UI", 18, FontStyle.Bold);
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            lblHeader.Dock = DockStyle.Fill;
            panelHeader.Controls.Add(lblHeader);

            dgvMovies = new DataGridView();
            dgvMovies.Location = new System.Drawing.Point(20, 80);
            dgvMovies.Size = new System.Drawing.Size(500, 420);
            dgvMovies.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMovies.AllowUserToAddRows = false;
            dgvMovies.AllowUserToDeleteRows = false;
            dgvMovies.ReadOnly = true;
            dgvMovies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMovies.BackgroundColor = System.Drawing.Color.White;
            dgvMovies.BorderStyle = BorderStyle.FixedSingle;
            dgvMovies.MultiSelect = false;
            dgvMovies.CellClick += DgvMovies_CellClick;

            GroupBox grpAdd = new GroupBox();
            grpAdd.Text = "ADD NEW MOVIE";
            grpAdd.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            grpAdd.Location = new System.Drawing.Point(540, 80);
            grpAdd.Size = new System.Drawing.Size(380, 280);
            grpAdd.BackColor = System.Drawing.Color.WhiteSmoke;

            Label lblTitle = new Label();
            lblTitle.Text = "Title:";
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 10);
            lblTitle.Location = new System.Drawing.Point(15, 30);
            lblTitle.Size = new System.Drawing.Size(100, 25);
            lblTitle.TextAlign = ContentAlignment.MiddleRight;

            txtTitle = new TextBox();
            txtTitle.Font = new System.Drawing.Font("Segoe UI", 10);
            txtTitle.Location = new System.Drawing.Point(120, 27);
            txtTitle.Size = new System.Drawing.Size(240, 25);

            Label lblGenre = new Label();
            lblGenre.Text = "Genre:";
            lblGenre.Font = new System.Drawing.Font("Segoe UI", 10);
            lblGenre.Location = new System.Drawing.Point(15, 70);
            lblGenre.Size = new System.Drawing.Size(100, 25);
            lblGenre.TextAlign = ContentAlignment.MiddleRight;

            cboGenre = new ComboBox();
            cboGenre.Font = new System.Drawing.Font("Segoe UI", 10);
            cboGenre.Location = new System.Drawing.Point(120, 67);
            cboGenre.Size = new System.Drawing.Size(240, 25);
            cboGenre.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGenre.DataSource = Enum.GetValues(typeof(Genre));
            cboGenre.SelectedIndex = -1;

            Label lblYear = new Label();
            lblYear.Text = "Year:";
            lblYear.Font = new System.Drawing.Font("Segoe UI", 10);
            lblYear.Location = new System.Drawing.Point(15, 110);
            lblYear.Size = new System.Drawing.Size(100, 25);
            lblYear.TextAlign = ContentAlignment.MiddleRight;

            numYear = new NumericUpDown();
            numYear.Font = new System.Drawing.Font("Segoe UI", 10);
            numYear.Location = new System.Drawing.Point(120, 107);
            numYear.Size = new System.Drawing.Size(100, 25);
            numYear.Minimum = 1940;
            numYear.Maximum = DateTime.Now.Year;
            numYear.Value = DateTime.Now.Year;

            Label lblPrice = new Label();
            lblPrice.Text = "Price (lei/day):";
            lblPrice.Font = new System.Drawing.Font("Segoe UI", 10);
            lblPrice.Location = new System.Drawing.Point(15, 150);
            lblPrice.Size = new System.Drawing.Size(100, 25);
            lblPrice.TextAlign = ContentAlignment.MiddleRight;

            numPrice = new NumericUpDown();
            numPrice.Font = new System.Drawing.Font("Segoe UI", 10);
            numPrice.Location = new System.Drawing.Point(120, 147);
            numPrice.Size = new System.Drawing.Size(100, 25);
            numPrice.Minimum = 5;
            numPrice.Maximum = 100;
            numPrice.Value = 15;
            numPrice.DecimalPlaces = 2;

            chkAvailable = new CheckBox();
            chkAvailable.Text = "Available for Rent";
            chkAvailable.Font = new System.Drawing.Font("Segoe UI", 10);
            chkAvailable.Location = new System.Drawing.Point(120, 190);
            chkAvailable.Size = new System.Drawing.Size(180, 25);
            chkAvailable.Checked = true;

            btnSave = new Button();
            btnSave.Text = "ADD MOVIE";
            btnSave.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            btnSave.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.Location = new System.Drawing.Point(120, 230);
            btnSave.Size = new System.Drawing.Size(120, 30);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Click += BtnSave_Click;

            grpAdd.Controls.Add(lblTitle);
            grpAdd.Controls.Add(txtTitle);
            grpAdd.Controls.Add(lblGenre);
            grpAdd.Controls.Add(cboGenre);
            grpAdd.Controls.Add(lblYear);
            grpAdd.Controls.Add(numYear);
            grpAdd.Controls.Add(lblPrice);
            grpAdd.Controls.Add(numPrice);
            grpAdd.Controls.Add(chkAvailable);
            grpAdd.Controls.Add(btnSave);

            GroupBox grpDelete = new GroupBox();
            grpDelete.Text = "DELETE MOVIE";
            grpDelete.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            grpDelete.Location = new System.Drawing.Point(540, 380);
            grpDelete.Size = new System.Drawing.Size(380, 120);
            grpDelete.BackColor = System.Drawing.Color.WhiteSmoke;

            Label lblSelected = new Label();
            lblSelected.Text = "Selected Movie:";
            lblSelected.Font = new System.Drawing.Font("Segoe UI", 9);
            lblSelected.Location = new System.Drawing.Point(15, 30);
            lblSelected.Size = new System.Drawing.Size(100, 25);
            lblSelected.TextAlign = ContentAlignment.MiddleRight;

            lblSelectedMovieName = new Label();
            lblSelectedMovieName.Text = "-";
            lblSelectedMovieName.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
            lblSelectedMovieName.Location = new System.Drawing.Point(120, 30);
            lblSelectedMovieName.Size = new System.Drawing.Size(240, 25);

            btnDelete = new Button();
            btnDelete.Text = "DELETE MOVIE";
            btnDelete.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            btnDelete.BackColor = System.Drawing.Color.DarkRed;
            btnDelete.ForeColor = System.Drawing.Color.White;
            btnDelete.Location = new System.Drawing.Point(120, 65);
            btnDelete.Size = new System.Drawing.Size(150, 35);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Enabled = false;
            btnDelete.Click += BtnDelete_Click;

            grpDelete.Controls.Add(lblSelected);
            grpDelete.Controls.Add(lblSelectedMovieName);
            grpDelete.Controls.Add(btnDelete);

            this.Controls.Add(panelHeader);
            this.Controls.Add(dgvMovies);
            this.Controls.Add(grpAdd);
            this.Controls.Add(grpDelete);
        }

        private void LoadMovies()
        {
            var availableMovies = Movie.GetAllMovies()
                .Where(m => m.IsAvailable)
                .Select(m => new
                {
                    m.MovieId,
                    m.MovieTitle,
                    Genre = m.Genre.ToString(),
                    m.Year,
                    Price = m.PricePerDay.ToString("F2") + " lei",
                    m.IsAvailable
                }).ToList();

            dgvMovies.DataSource = null;
            dgvMovies.DataSource = availableMovies;

            if (dgvMovies.Columns.Contains("MovieId"))
                dgvMovies.Columns["MovieId"].HeaderText = "ID";
            if (dgvMovies.Columns.Contains("MovieTitle"))
                dgvMovies.Columns["MovieTitle"].HeaderText = "Title";
            if (dgvMovies.Columns.Contains("IsAvailable"))
                dgvMovies.Columns["IsAvailable"].Visible = false;
        }

        private void DgvMovies_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            errorProvider.Clear();

            if (e.RowIndex >= 0)
            {
                int movieId = (int)dgvMovies.Rows[e.RowIndex].Cells["MovieId"].Value;
                selectedMovie = Movie.GetAllMovies().FirstOrDefault(m => m.MovieId == movieId);

                if (selectedMovie != null)
                {
                    lblSelectedMovieName.Text = selectedMovie.MovieTitle;
                    btnDelete.Enabled = true;
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                errorProvider.SetError(txtTitle, "Title is required!");
                txtTitle.Focus();
                return;
            }

            if (cboGenre.SelectedItem == null)
            {
                errorProvider.SetError(cboGenre, "Select a genre!");
                cboGenre.Focus();
                return;
            }

            if (numYear.Value < 1940)
            {
                errorProvider.SetError(numYear, "Year cannot be below 1940!");
                numYear.Focus();
                return;
            }

            if (numYear.Value > DateTime.Now.Year)
            {
                errorProvider.SetError(numYear, "Year cannot be in the future!");
                numYear.Focus();
                return;
            }

            if (numPrice.Value < 5)
            {
                errorProvider.SetError(numPrice, "Price cannot be below 5 lei!");
                numPrice.Focus();
                return;
            }

            if (numPrice.Value > 100)
            {
                errorProvider.SetError(numPrice, "Price cannot exceed 100 lei!");
                numPrice.Focus();
                return;
            }

            try
            {
                string title = txtTitle.Text;
                Genre genre = (Genre)cboGenre.SelectedItem;
                int year = (int)numYear.Value;
                double price = (double)numPrice.Value;
                bool isAvailable = chkAvailable.Checked;

                Movie newMovie = new Movie(title, genre, year, isAvailable);
                newMovie.PricePerDay = price;
                Movie.SaveToFile();

                MessageBox.Show($"Movie '{title}' saved!\nID: {newMovie.MovieId}\nPrice: {price:F2} lei/day", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtTitle.Clear();
                cboGenre.SelectedIndex = -1;
                numYear.Value = DateTime.Now.Year;
                numPrice.Value = 15;
                chkAvailable.Checked = true;
                errorProvider.Clear();
                txtTitle.Focus();
                LoadMovies();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (selectedMovie == null)
            {
                MessageBox.Show("Select a movie to delete!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Delete movie '{selectedMovie.MovieTitle}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                Movie.MoviesList.Remove(selectedMovie);
                Movie.SaveToFile();

                MessageBox.Show("Movie deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadMovies();
                selectedMovie = null;
                lblSelectedMovieName.Text = "-";
                btnDelete.Enabled = false;
                errorProvider.Clear();
            }
        }
    }
}