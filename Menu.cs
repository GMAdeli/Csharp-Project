using System;
using System.Linq;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace MovieRental
{
    public partial class MenuForm : Form
    {
        private Button btnAddMovie;
        private Button btnAddClient;
        private Button btnRentMovie;
        private Button btnRentedMovies;
        private Button btnStatistics;
        private Button btnShortcuts;
        private Button btnExit;
        private Panel panelHeader;
        private Label lblHeader;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblMovieCount;
        private ToolStripStatusLabel lblClientCount;
        private ToolStripStatusLabel lblActiveRentals;
        private ToolStripStatusLabel lblDateTime;
        private Timer timer;

        public MenuForm()
        {
            this.Text = "Movie Rental Store";
            this.Size = new System.Drawing.Size(500, 680);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = System.Drawing.Color.WhiteSmoke;

            panelHeader = new Panel();
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 80;
            panelHeader.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);

            lblHeader = new Label();
            lblHeader.Text = "MOVIE RENTAL STORE";
            lblHeader.ForeColor = System.Drawing.Color.White;
            lblHeader.Font = new System.Drawing.Font("Segoe UI", 18, FontStyle.Bold);
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            lblHeader.Dock = DockStyle.Fill;
            panelHeader.Controls.Add(lblHeader);

            btnAddMovie = new Button();
            btnAddMovie.Text = "&Add Movie";
            btnAddMovie.Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold);
            btnAddMovie.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);
            btnAddMovie.ForeColor = System.Drawing.Color.White;
            btnAddMovie.Location = new System.Drawing.Point(125, 100);
            btnAddMovie.Size = new System.Drawing.Size(250, 40);
            btnAddMovie.FlatStyle = FlatStyle.Flat;
            btnAddMovie.Click += BtnAddMovie_Click;

            btnAddClient = new Button();
            btnAddClient.Text = "Add Cl&ient";
            btnAddClient.Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold);
            btnAddClient.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);
            btnAddClient.ForeColor = System.Drawing.Color.White;
            btnAddClient.Location = new System.Drawing.Point(125, 155);
            btnAddClient.Size = new System.Drawing.Size(250, 40);
            btnAddClient.FlatStyle = FlatStyle.Flat;
            btnAddClient.Click += BtnAddClient_Click;

            btnRentMovie = new Button();
            btnRentMovie.Text = "&Rent Movie";
            btnRentMovie.Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold);
            btnRentMovie.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);
            btnRentMovie.ForeColor = System.Drawing.Color.White;
            btnRentMovie.Location = new System.Drawing.Point(125, 210);
            btnRentMovie.Size = new System.Drawing.Size(250, 40);
            btnRentMovie.FlatStyle = FlatStyle.Flat;
            btnRentMovie.Click += BtnRentMovie_Click;

            btnRentedMovies = new Button();
            btnRentedMovies.Text = "Rented Mo&vies";
            btnRentedMovies.Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold);
            btnRentedMovies.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);
            btnRentedMovies.ForeColor = System.Drawing.Color.White;
            btnRentedMovies.Location = new System.Drawing.Point(125, 265);
            btnRentedMovies.Size = new System.Drawing.Size(250, 40);
            btnRentedMovies.FlatStyle = FlatStyle.Flat;
            btnRentedMovies.Click += BtnRentedMovies_Click;

            btnStatistics = new Button();
            btnStatistics.Text = "&Statistics";
            btnStatistics.Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold);
            btnStatistics.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);
            btnStatistics.ForeColor = System.Drawing.Color.White;
            btnStatistics.Location = new System.Drawing.Point(125, 320);
            btnStatistics.Size = new System.Drawing.Size(250, 40);
            btnStatistics.FlatStyle = FlatStyle.Flat;
            btnStatistics.Click += BtnStatistics_Click;

            btnShortcuts = new Button();
            btnShortcuts.Text = "&Shortcuts";
            btnShortcuts.Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold);
            btnShortcuts.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);
            btnShortcuts.ForeColor = System.Drawing.Color.White;
            btnShortcuts.Location = new System.Drawing.Point(125, 375);
            btnShortcuts.Size = new System.Drawing.Size(250, 40);
            btnShortcuts.FlatStyle = FlatStyle.Flat;
            btnShortcuts.Click += BtnShortcuts_Click;

            btnExit = new Button();
            btnExit.Text = "E&xit";
            btnExit.Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold);
            btnExit.BackColor = System.Drawing.Color.DarkRed;
            btnExit.ForeColor = System.Drawing.Color.White;
            btnExit.Location = new System.Drawing.Point(125, 430);
            btnExit.Size = new System.Drawing.Size(250, 40);
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Click += (s, e) => Application.Exit();

            statusStrip = new StatusStrip();
            lblMovieCount = new ToolStripStatusLabel();
            lblClientCount = new ToolStripStatusLabel();
            lblActiveRentals = new ToolStripStatusLabel();
            lblDateTime = new ToolStripStatusLabel();

            lblMovieCount.Text = "Movies: 0";
            lblClientCount.Text = "Clients: 0";
            lblActiveRentals.Text = "Active Rentals: 0";
            lblDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            statusStrip.Items.Add(lblMovieCount);
            statusStrip.Items.Add(new ToolStripStatusLabel(" | "));
            statusStrip.Items.Add(lblClientCount);
            statusStrip.Items.Add(new ToolStripStatusLabel(" | "));
            statusStrip.Items.Add(lblActiveRentals);
            statusStrip.Items.Add(new ToolStripStatusLabel(" | "));
            statusStrip.Items.Add(lblDateTime);

            this.Controls.Add(panelHeader);
            this.Controls.Add(btnAddMovie);
            this.Controls.Add(btnAddClient);
            this.Controls.Add(btnRentMovie);
            this.Controls.Add(btnRentedMovies);
            this.Controls.Add(btnStatistics);
            this.Controls.Add(btnShortcuts);
            this.Controls.Add(btnExit);
            this.Controls.Add(statusStrip);

            UpdateStatusStrip();
            StartTimer();
        }

        private void StartTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public void UpdateStatusStrip()
        {
            int movieCount = Movie.GetAllMovies().Count;
            int clientCount = Client.GetAllClients().Count;
            int activeRentals = Rental.RentalsList.Count(r => r.ReturnDate == null);

            lblMovieCount.Text = $"Movies: {movieCount}";
            lblClientCount.Text = $"Clients: {clientCount}";
            lblActiveRentals.Text = $"Active Rentals: {activeRentals}";
        }

        private void BtnAddMovie_Click(object sender, EventArgs e)
        {
            NewMovieForm movieForm = new NewMovieForm();
            movieForm.ShowDialog();
            UpdateStatusStrip();
        }

        private void BtnAddClient_Click(object sender, EventArgs e)
        {
            NewClientForm clientForm = new NewClientForm();
            clientForm.ShowDialog();
            UpdateStatusStrip();
        }

        private void BtnRentMovie_Click(object sender, EventArgs e)
        {
            RentMovieForm rentForm = new RentMovieForm();
            rentForm.ShowDialog();
            UpdateStatusStrip();
        }

        private void BtnRentedMovies_Click(object sender, EventArgs e)
        {
            RentedMoviesForm rentedForm = new RentedMoviesForm();
            rentedForm.ShowDialog();
            UpdateStatusStrip();
        }

        private void BtnStatistics_Click(object sender, EventArgs e)
        {
            StatisticsForm statsForm = new StatisticsForm();
            statsForm.ShowDialog();
        }

        private void BtnShortcuts_Click(object sender, EventArgs e)
        {
            string shortcuts =
                "=== KEYBOARD SHORTCUTS ===\n\n" +
                "MAIN MENU:\n" +
                "Alt + A = Add Movie\n" +
                "Alt + I = Add Client\n" +
                "Alt + R = Rent Movie\n" +
                "Alt + V = Rented Movies\n" +
                "Alt + S = Statistics\n" +
                "Alt + H = Shortcuts\n" +
                "Alt + X = Exit\n\n" +
                "ADD/EDIT FORMS:\n" +
                "Alt + A = Save/Add\n" +
                "Alt + D = Delete\n" +
                "Alt + U = Update Email\n\n" +
                "RENT MOVIE FORM:\n" +
                "Alt + R = Rent\n" +
                "Alt + C = Cancel\n\n" +
                "RENTED MOVIES FORM:\n" +
                "Alt + R = Return\n" +
                "Alt + E = Export Report\n" +
                "Right-click = Sort options";

            MessageBox.Show(shortcuts, "Keyboard Shortcuts", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}