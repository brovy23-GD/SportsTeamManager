using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SportsTeamManager.Data;
using SportsTeamManager.Models;

namespace SportsTeamManager
{
    public partial class MainWindow : Window
    {
        private readonly AppDbContext _context = new AppDbContext();

        public MainWindow()
        {
            InitializeComponent();
            if (!_context.SportsTeams.Any())
            {
                _context.SportsTeams.Add(new SportsTeam
                {
                    Name = "Chicago Bulls",
                    City = "Chicago",
                    Sport = "Basketball",
                    FoundedYear = 1966,
                    ChampionshipsWon = 6
                });

                _context.SportsTeams.Add(new SportsTeam
                {
                    Name = "Chicago Bears",
                    City = "Chicago",
                    Sport = "Football",
                    FoundedYear = 1919,
                    ChampionshipsWon = 1
                });

                _context.SportsTeams.Add(new SportsTeam
                {
                    Name = "Chicago Cubs",
                    City = "Chicago",
                    Sport = "Baseball",
                    FoundedYear = 1876,
                    ChampionshipsWon = 3
                });

                _context.SaveChanges();
            }

            LoadTeams();

           // var all = _context.SportsTeams.ToList();
           // MessageBox.Show("Loaded: " + all.Count);

            UpdateButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
        }


        private void LoadTeams()
        {
            var teams = _context.SportsTeams.ToList();
            TeamsDataGrid.ItemsSource = teams;
            TeamCountTextBlock.Text = $"Total Teams: {teams.Count}";
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(CityTextBox.Text) ||
                SportComboBox.SelectedItem == null)
            {
                MessageBox.Show("Name, City, and Sport are required.");
                return false;
            }

            if (!int.TryParse(FoundedYearTextBox.Text, out _) ||
                !int.TryParse(ChampionshipsTextBox.Text, out _))
            {
                MessageBox.Show("Founded Year and Championships must be numbers.");
                return false;
            }

            return true;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs()) return;

            var team = new SportsTeam
            {
                Name = NameTextBox.Text,
                City = CityTextBox.Text,
                Sport = (SportComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                FoundedYear = int.Parse(FoundedYearTextBox.Text),
                ChampionshipsWon = int.Parse(ChampionshipsTextBox.Text)
            };

            _context.SportsTeams.Add(team);
            _context.SaveChanges();
            LoadTeams();
            ClearInputs();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeamsDataGrid.SelectedItem is not SportsTeam selected) return;
            if (!ValidateInputs()) return;

            selected.Name = NameTextBox.Text;
            selected.City = CityTextBox.Text;
            selected.Sport = (SportComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            selected.FoundedYear = int.Parse(FoundedYearTextBox.Text);
            selected.ChampionshipsWon = int.Parse(ChampionshipsTextBox.Text);

            _context.SaveChanges();
            LoadTeams();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeamsDataGrid.SelectedItem is not SportsTeam selected) return;

            var result = MessageBox.Show(
                $"Are you sure you want to delete '{selected.Name}'?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _context.SportsTeams.Remove(selected);
                _context.SaveChanges();
                LoadTeams();
                ClearInputs();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
            TeamsDataGrid.UnselectAll();
        }

        private void ClearInputs()
        {
            NameTextBox.Text = "";
            CityTextBox.Text = "";
            SportComboBox.SelectedItem = null;
            FoundedYearTextBox.Text = "";
            ChampionshipsTextBox.Text = "";
        }

        private void TeamsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TeamsDataGrid.SelectedItem is SportsTeam selected)
            {
                NameTextBox.Text = selected.Name;
                CityTextBox.Text = selected.City;

                SportComboBox.SelectedItem = SportComboBox.Items
                    .Cast<ComboBoxItem>()
                    .FirstOrDefault(i => i.Content.ToString() == selected.Sport);

                FoundedYearTextBox.Text = selected.FoundedYear.ToString();
                ChampionshipsTextBox.Text = selected.ChampionshipsWon.ToString();

                UpdateButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
            }
            else
            {
                UpdateButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = SearchTextBox.Text.ToLower();

            var filtered = _context.SportsTeams
                .Where(t =>
                    t.Name.ToLower().Contains(search) ||
                    t.City.ToLower().Contains(search) ||
                    t.Sport.ToLower().Contains(search))
                .ToList();

            TeamsDataGrid.ItemsSource = filtered;
            TeamCountTextBlock.Text = $"Total Teams: {filtered.Count}";
            //MessageBox.Show(filtered.Count.ToString());


        }
    }
}
