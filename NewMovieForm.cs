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
        private CheckBox chkAvailable;
        private Button btnSave;
        private Button btnDelete;
        private Button btnCancel;
        private Panel panelHeader;
        private Label lblHeader;
        private Label lblSelectedMovieName;

        private Movie selectedMovie;

        public NewMovieForm()
        {
            InitializeComponent();
            LoadMovies();
        }

        private void InitializeComponent()
        {
            this.Text = "Manage Movies";
            this.Size = new System.Drawing.Size(900, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = System.Drawing.Color.WhiteSmoke;

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
            dgvMovies.Size = new System.Drawing.Size(500, 400);
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
            grpAdd.Size = new System.Drawing.Size(330, 220);
            grpAdd.BackColor = System.Drawing.Color.WhiteSmoke;

            Label lblTitle = new Label();
            lblTitle.Text = "Title:";
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 10);
            lblTitle.Location = new System.Drawing.Point(15, 30);
            lblTitle.Size = new System.Drawing.Size(80, 25);

            txtTitle = new TextBox();
            txtTitle.Font = new System.Drawing.Font("Segoe UI", 10);
            txtTitle.Location = new System.Drawing.Point(100, 27);
            txtTitle.Size = new System.Drawing.Size(210, 25);

            Label lblGenre = new Label();
            lblGenre.Text = "Genre:";
            lblGenre.Font = new System.Drawing.Font("Segoe UI", 10);
            lblGenre.Location = new System.Drawing.Point(15, 70);
            lblGenre.Size = new System.Drawing.Size(80, 25);

            cboGenre = new ComboBox();
            cboGenre.Font = new System.Drawing.Font("Segoe UI", 10);
            cboGenre.Location = new System.Drawing.Point(100, 67);
            cboGenre.Size = new System.Drawing.Size(210, 25);
            cboGenre.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGenre.DataSource = Enum.GetValues(typeof(Genre));
            cboGenre.SelectedIndex = -1;

            Label lblYear = new Label();
            lblYear.Text = "Year:";
            lblYear.Font = new System.Drawing.Font("Segoe UI", 10);
            lblYear.Location = new System.Drawing.Point(15, 110);
            lblYear.Size = new System.Drawing.Size(80, 25);

            numYear = new NumericUpDown();
            numYear.Font = new System.Drawing.Font("Segoe UI", 10);
            numYear.Location = new System.Drawing.Point(100, 107);
            numYear.Size = new System.Drawing.Size(100, 25);
            numYear.Minimum = 1940;
            numYear.Maximum = DateTime.Now.Year;
            numYear.Value = DateTime.Now.Year;

            chkAvailable = new CheckBox();
            chkAvailable.Text = "Available for Rent";
            chkAvailable.Font = new System.Drawing.Font("Segoe UI", 10);
            chkAvailable.Location = new System.Drawing.Point(100, 145);
            chkAvailable.Size = new System.Drawing.Size(180, 25);
            chkAvailable.Checked = true;

            btnSave = new Button();
            btnSave.Text = "ADD MOVIE";
            btnSave.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            btnSave.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.Location = new System.Drawing.Point(100, 180);
            btnSave.Size = new System.Drawing.Size(120, 30);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Click += BtnSave_Click;

            grpAdd.Controls.Add(lblTitle);
            grpAdd.Controls.Add(txtTitle);
            grpAdd.Controls.Add(lblGenre);
            grpAdd.Controls.Add(cboGenre);
            grpAdd.Controls.Add(lblYear);
            grpAdd.Controls.Add(numYear);
            grpAdd.Controls.Add(chkAvailable);
            grpAdd.Controls.Add(btnSave);

            GroupBox grpDelete = new GroupBox();
            grpDelete.Text = "DELETE MOVIE";
            grpDelete.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            grpDelete.Location = new System.Drawing.Point(540, 320);
            grpDelete.Size = new System.Drawing.Size(330, 120);
            grpDelete.BackColor = System.Drawing.Color.WhiteSmoke;

            Label lblSelected = new Label();
            lblSelected.Text = "Selected Movie:";
            lblSelected.Font = new System.Drawing.Font("Segoe UI", 9);
            lblSelected.Location = new System.Drawing.Point(15, 30);
            lblSelected.Size = new System.Drawing.Size(100, 25);

            lblSelectedMovieName = new Label();
            lblSelectedMovieName.Text = "-";
            lblSelectedMovieName.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
            lblSelectedMovieName.Location = new System.Drawing.Point(120, 30);
            lblSelectedMovieName.Size = new System.Drawing.Size(190, 25);

            btnDelete = new Button();
            btnDelete.Text = "DELETE MOVIE";
            btnDelete.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            btnDelete.BackColor = System.Drawing.Color.DarkRed;
            btnDelete.ForeColor = System.Drawing.Color.White;
            btnDelete.Location = new System.Drawing.Point(100, 65);
            btnDelete.Size = new System.Drawing.Size(150, 35);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Enabled = false;
            btnDelete.Click += BtnDelete_Click;

            grpDelete.Controls.Add(lblSelected);
            grpDelete.Controls.Add(lblSelectedMovieName);
            grpDelete.Controls.Add(btnDelete);

            btnCancel = new Button();
            btnCancel.Text = "CLOSE";
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 10);
            btnCancel.BackColor = System.Drawing.Color.LightGray;
            btnCancel.Location = new System.Drawing.Point(770, 470);
            btnCancel.Size = new System.Drawing.Size(100, 35);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.Add(panelHeader);
            this.Controls.Add(dgvMovies);
            this.Controls.Add(grpAdd);
            this.Controls.Add(grpDelete);
            this.Controls.Add(btnCancel);
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
            }
        }
    }
}