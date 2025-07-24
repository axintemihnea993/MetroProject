1.docker-compose up --build

# dotnet ef migrations add InitialCreate --startup-project ./MetroProject.API --project ./MetroProject.Application

2. dotnet ef database update --startup-project ./MetroProject.API --project ./MetroProject.Application
when use this command make sure that following connection string is used:
Host=localhost;Database=metrodb;Username=postgres;Password=yourpassword
in protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
and appsettings.json

After update the database put the following connection string:
Host=db;Database=metrodb;Username=postgres;Password=yourpassword
