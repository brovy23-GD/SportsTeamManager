<p align="center">

&#x20; <img src="https://user-gen-media-assets.s3.amazonaws.com/gpt4o\_images/64c21535-c2a5-4f28-a006-5ae5cb895b05.png" alt="Bobby Rovy Chicago Army GitHub banner" />

</p>



\# 🏅 Sports Team Manager  

\### Assignment 10.3 – WPF Desktop App • EF Core • SQL Server LocalDB • MSSA Project



\---



\## 🎥 Demo Video (Coming Soon)



A full video demonstration will be added here.



\---



\## 📖 Project Summary



Sports Team Manager is a \*\*WPF desktop application\*\* that manages sports teams with full CRUD functionality.  

The focus of this assignment is on \*\*backend skills\*\*: C#, \*\*Entity Framework Core\*\*, \*\*SQL Server LocalDB\*\*, database \*\*migrations\*\*, and basic querying, with a simple WPF UI on top.



This project is part of \*\*Assignment 10.3 – SportsTeamManager (WPF)\*\* in the MSSA Cloud Application Development program.



\---



\## ⭐ Core Features



\- Full \*\*CRUD operations\*\* for sports teams  

\- \*\*DataGrid\*\* bound directly to EF Core query results  

\- \*\*ComboBox\*\* for selecting a sport  

\- SQL Server LocalDB handled through \*\*EF Core\*\*  

\- Database \*\*migrations\*\* to evolve the schema safely  

\- Built‑in \*\*search\*\* by team name, city, or sport  

\- Automatic \*\*team count\*\* display in the UI  



> 💡 \*\*Seed Data:\*\*  

> On first run, the app automatically seeds three Chicago teams into the database:  

> - Chicago Bulls (Basketball)  

> - Chicago Bears (Football)  

> - Chicago Cubs (Baseball)  



\---



\## 🔄 App Workflow (High Level)



```mermaid

flowchart TD

&#x20;   A\[User launches WPF app] --> B\[MainWindow checks for seed data]

&#x20;   B --> C\[Seed default Chicago teams if database is empty]

&#x20;   C --> D\[Load all SportsTeams into DataGrid]

&#x20;   D --> E\[User adds / updates / deletes a team]

&#x20;   E --> F\[EF Core SaveChanges to SQL Server LocalDB]

&#x20;   F --> G\[Reload teams + update team count]

&#x20;   D --> H\[User types in Search box]

&#x20;   H --> I\[LINQ filter over SportsTeams table]

&#x20;   I --> G

```



\---



\## 🧱 Tech Stack



\- \*\*Language:\*\* C#  

\- \*\*Framework:\*\* .NET (WPF)  

\- \*\*UI:\*\* WPF (XAML + code-behind)  

\- \*\*ORM:\*\* Entity Framework Core  

\- \*\*Database:\*\* SQL Server LocalDB (`(localdb)\\\\MSSQLLocalDB`)  

\- \*\*Other:\*\* LINQ, EF Core migrations, basic data binding  



WPF is used as a \*\*front-end shell\*\*, while most of the learning is around EF Core, migrations, and backend logic.



\---



\## 🗂️ Folder Structure



```text

SportsTeamManager/

├── SportsTeamManager.sln

├── README.md

├── .gitignore

└── SportsTeamManager/

&#x20;   ├── App.xaml

&#x20;   ├── App.xaml.cs

&#x20;   ├── MainWindow.xaml

&#x20;   ├── MainWindow.xaml.cs          // event handlers + CRUD + search

&#x20;   ├── Data/

&#x20;   │   ├── AppDbContext.cs         // EF Core DbContext

&#x20;   │   └── Migrations/             // EF Core migration files + snapshot

&#x20;   ├── Models/

&#x20;   │   └── SportsTeam.cs           // entity model for EF Core

&#x20;   └── Properties/                 // WPF app metadata

```



Update folder names if your solution is organized slightly differently.



\---



\## 🧩 Architecture Overview



```mermaid

flowchart LR

&#x20;   UI\[WPF UI\\n(MainWindow + controls)] --> B\[Code-behind\\n(event handlers)]

&#x20;   B --> C\[EF Core\\n(AppDbContext)]

&#x20;   C --> D\[(SQL Server LocalDB\\nSportsTeamDB)]

```



\- The \*\*WPF UI\*\* (XAML + code-behind) handles buttons, text boxes, DataGrid, ComboBox, and search input.  

\- The \*\*code-behind\*\* calls \*\*AppDbContext\*\* to query and save `SportsTeam` records.  

\- \*\*EF Core\*\* maps the `SportsTeam` model to the \*\*SportsTeamDB\*\* LocalDB database using migrations.



\---



\## 🗃️ Database Model Example



\### AppDbContext



```csharp

using Microsoft.EntityFrameworkCore;

using SportsTeamManager.Models;



namespace SportsTeamManager.Data

{

&#x20;   public class AppDbContext : DbContext

&#x20;   {

&#x20;       public DbSet<SportsTeam> SportsTeams => Set<SportsTeam>();



&#x20;       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

&#x20;       {

&#x20;           optionsBuilder.UseSqlServer(

&#x20;               "Server=(localdb)\\\\MSSQLLocalDB;Database=SportsTeamDB;Trusted\_Connection=True;");

&#x20;       }

&#x20;   }

}

```



\### SportsTeam Entity



```csharp

using System.ComponentModel.DataAnnotations;



namespace SportsTeamManager.Models

{

&#x20;   public class SportsTeam

&#x20;   {

&#x20;       \[Key]

&#x20;       public int TeamId { get; set; }

&#x20;       public string? Name { get; set; }

&#x20;       public string? City { get; set; }

&#x20;       public string? Sport { get; set; }

&#x20;       public int FoundedYear { get; set; }

&#x20;       public int ChampionshipsWon { get; set; }

&#x20;   }

}

```



EF Core maps this class into a `SportsTeams` table with `TeamId` as the identity primary key, plus the additional fields for team metadata.



\---



\## 💻 Sample CRUD and Search Logic (MainWindow.xaml.cs)



\### Seeding Data and Initial Load



```csharp

public partial class MainWindow : Window

{

&#x20;   private readonly AppDbContext \_context = new AppDbContext();



&#x20;   public MainWindow()

&#x20;   {

&#x20;       InitializeComponent();



&#x20;       // Seed default Chicago teams if database is empty

&#x20;       if (!\_context.SportsTeams.Any())

&#x20;       {

&#x20;           \_context.SportsTeams.Add(new SportsTeam

&#x20;           {

&#x20;               Name = "Chicago Bulls",

&#x20;               City = "Chicago",

&#x20;               Sport = "Basketball",

&#x20;               FoundedYear = 1966,

&#x20;               ChampionshipsWon = 6

&#x20;           });



&#x20;           \_context.SportsTeams.Add(new SportsTeam

&#x20;           {

&#x20;               Name = "Chicago Bears",

&#x20;               City = "Chicago",

&#x20;               Sport = "Football",

&#x20;               FoundedYear = 1919,

&#x20;               ChampionshipsWon = 1

&#x20;           });



&#x20;           \_context.SportsTeams.Add(new SportsTeam

&#x20;           {

&#x20;               Name = "Chicago Cubs",

&#x20;               City = "Chicago",

&#x20;               Sport = "Baseball",

&#x20;               FoundedYear = 1876,

&#x20;               ChampionshipsWon = 3

&#x20;           });



&#x20;           \_context.SaveChanges();

&#x20;       }



&#x20;       LoadTeams();



&#x20;       UpdateButton.IsEnabled = false;

&#x20;       DeleteButton.IsEnabled = false;

&#x20;   }



&#x20;   private void LoadTeams()

&#x20;   {

&#x20;       var teams = \_context.SportsTeams.ToList();

&#x20;       TeamsDataGrid.ItemsSource = teams;

&#x20;       TeamCountTextBlock.Text = $"Total Teams: {teams.Count}";

&#x20;   }

}

```



\### Validation Helper



```csharp

private bool ValidateInputs()

{

&#x20;   if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||

&#x20;       string.IsNullOrWhiteSpace(CityTextBox.Text) ||

&#x20;       SportComboBox.SelectedItem == null)

&#x20;   {

&#x20;       MessageBox.Show("Name, City, and Sport are required.");

&#x20;       return false;

&#x20;   }



&#x20;   if (!int.TryParse(FoundedYearTextBox.Text, out \_) ||

&#x20;       !int.TryParse(ChampionshipsTextBox.Text, out \_))

&#x20;   {

&#x20;       MessageBox.Show("Founded Year and Championships must be numbers.");

&#x20;       return false;

&#x20;   }



&#x20;   return true;

}

```



\### Create (Add)



```csharp

private void AddButton\_Click(object sender, RoutedEventArgs e)

{

&#x20;   if (!ValidateInputs()) return;



&#x20;   var team = new SportsTeam

&#x20;   {

&#x20;       Name = NameTextBox.Text,

&#x20;       City = CityTextBox.Text,

&#x20;       Sport = (SportComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),

&#x20;       FoundedYear = int.Parse(FoundedYearTextBox.Text),

&#x20;       ChampionshipsWon = int.Parse(ChampionshipsTextBox.Text)

&#x20;   };



&#x20;   \_context.SportsTeams.Add(team);

&#x20;   \_context.SaveChanges();

&#x20;   LoadTeams();

&#x20;   ClearInputs();

}

```



\### Update



```csharp

private void UpdateButton\_Click(object sender, RoutedEventArgs e)

{

&#x20;   if (TeamsDataGrid.SelectedItem is not SportsTeam selected) return;

&#x20;   if (!ValidateInputs()) return;



&#x20;   selected.Name = NameTextBox.Text;

&#x20;   selected.City = CityTextBox.Text;

&#x20;   selected.Sport = (SportComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

&#x20;   selected.FoundedYear = int.Parse(FoundedYearTextBox.Text);

&#x20;   selected.ChampionshipsWon = int.Parse(ChampionshipsTextBox.Text);



&#x20;   \_context.SaveChanges();

&#x20;   LoadTeams();

}

```



\### Delete



```csharp

private void DeleteButton\_Click(object sender, RoutedEventArgs e)

{

&#x20;   if (TeamsDataGrid.SelectedItem is not SportsTeam selected) return;



&#x20;   var result = MessageBox.Show(

&#x20;       $"Are you sure you want to delete '{selected.Name}'?",

&#x20;       "Confirm Delete",

&#x20;       MessageBoxButton.YesNo,

&#x20;       MessageBoxImage.Warning);



&#x20;   if (result == MessageBoxResult.Yes)

&#x20;   {

&#x20;       \_context.SportsTeams.Remove(selected);

&#x20;       \_context.SaveChanges();

&#x20;       LoadTeams();

&#x20;       ClearInputs();

&#x20;   }

}

```



\### Selection Handling and Clear



```csharp

private void TeamsDataGrid\_SelectionChanged(object sender, SelectionChangedEventArgs e)

{

&#x20;   if (TeamsDataGrid.SelectedItem is SportsTeam selected)

&#x20;   {

&#x20;       NameTextBox.Text = selected.Name;

&#x20;       CityTextBox.Text = selected.City;



&#x20;       SportComboBox.SelectedItem = SportComboBox.Items

&#x20;           .Cast<ComboBoxItem>()

&#x20;           .FirstOrDefault(i => i.Content.ToString() == selected.Sport);



&#x20;       FoundedYearTextBox.Text = selected.FoundedYear.ToString();

&#x20;       ChampionshipsTextBox.Text = selected.ChampionshipsWon.ToString();



&#x20;       UpdateButton.IsEnabled = true;

&#x20;       DeleteButton.IsEnabled = true;

&#x20;   }

&#x20;   else

&#x20;   {

&#x20;       UpdateButton.IsEnabled = false;

&#x20;       DeleteButton.IsEnabled = false;

&#x20;   }

}



private void ClearButton\_Click(object sender, RoutedEventArgs e)

{

&#x20;   ClearInputs();

&#x20;   TeamsDataGrid.UnselectAll();

}



private void ClearInputs()

{

&#x20;   NameTextBox.Text = "";

&#x20;   CityTextBox.Text = "";

&#x20;   SportComboBox.SelectedItem = null;

&#x20;   FoundedYearTextBox.Text = "";

&#x20;   ChampionshipsTextBox.Text = "";

}

```



\### Search (Live Filter)



```csharp

private void SearchTextBox\_TextChanged(object sender, TextChangedEventArgs e)

{

&#x20;   string search = SearchTextBox.Text.ToLower();



&#x20;   var filtered = \_context.SportsTeams

&#x20;       .Where(t =>

&#x20;           t.Name.ToLower().Contains(search) ||

&#x20;           t.City.ToLower().Contains(search) ||

&#x20;           t.Sport.ToLower().Contains(search))

&#x20;       .ToList();



&#x20;   TeamsDataGrid.ItemsSource = filtered;

&#x20;   TeamCountTextBlock.Text = $"Total Teams: {filtered.Count}";

}

```



\---



\## 🚀 How to Run



1\. \*\*Clone\*\* the repository.  

2\. Open the solution in \*\*Visual Studio\*\*: `SportsTeamManager.sln`.  

3\. Restore NuGet packages for \*\*EF Core\*\* and the \*\*SQL Server\*\* provider.  

4\. From \*\*Package Manager Console\*\*, run:

&#x20;  - `Update-Database`

5\. Set `SportsTeamManager` as the \*\*Startup Project\*\*.  

6\. Press \*\*F5\*\* or `Ctrl + F5` to run the application.



On first run, the app seeds three \*\*Chicago teams\*\* into the LocalDB database if it is empty.



\---



\## 📌 Future Improvements



\- Add a \*\*Player\*\* entity and relate it to `SportsTeam` (one-to-many)  

\- Add a small \*\*Web API\*\* layer on top of this EF Core backend  

\- Move logic from code-behind into a more \*\*MVVM-style\*\* structure  

\- Add validation summaries and more robust error handling  

\- Implement stats dashboards and additional filtering/sorting with LINQ  



\---



\## 👨‍💻 Author



\*\*Bobby Rovy\*\*  

U.S. Army Veteran • Software Engineer in Transition  

MSSA Cloud Application Development (2026)

