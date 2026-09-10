# StaskoTravel ✈️🌍

StaskoTravel is a full-stack **ASP.NET Core MVC** web application designed to help users plan trips, organize activities, check local weather forecasts, and calculate real-time currency conversions seamlessly.

---

## 🌟 Key Features

* **Trip & Activity Management:** Create, edit, and organize trip itineraries with structured activity schedules.
* **Dynamic Activity Search:** Built-in autocomplete search interface for quick activity lookup.
* **Live Weather Integration:** Automatically fetches geocoding coordinates and real-time weather conditions for activities using the **Open-Meteo API**.
* **Real-time Currency Conversion:** Automatically converts activity costs from local trip currency to the user's preferred home currency using the **Frankfurter API**.
* **Currency Auto-complete Search:** Easily search through global currency codes supported by the **VatComply API**.
* **User Identity & Custom Claims:** Managed by ASP.NET Core Identity, utilizing a custom `UserClaimsPrincipalFactory` to handle user preferences like `HomeCurrency` in authentication cookies without database overhead.
* **Responsive Bootstrap 5 UI:** Modern, mobile-friendly design with custom card layouts, floating labels, and interactive components.

---

## 🛠️ Tech Stack

### **Backend**
* **Framework:** ASP.NET Core MVC (.NET 8+)
* **Database:** SQL Server
* **ORM:** Entity Framework Core
* **Authentication:** ASP.NET Core Identity (with Custom Claims)

### **Frontend**
* **Styling:** Bootstrap 5, Bootstrap Icons
* **Scripting:** Vanilla JavaScript (ES6+ async/await, Fetch API)

### **External APIs**
* **[Open-Meteo Geocoding & Forecast API](https://open-meteo.com/):** Location coordinates & activity weather data.
* **[Frankfurter API](https://www.frankfurter.app/):** Foreign exchange rates and currency conversions.
* **[VatComply API](https://www.vatcomply.com/):** Global currencies database lookup.

---

## 🚀 Getting Started

### Prerequisites

Ensure you have the following installed on your local machine:
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB or Express)
* [Git](https://git-scm.com/)

---

### Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/stasko456/StaskoTravel.git
   cd StaskoTravel
   ```

2. **Configure Database Connection:**
   Open `appsettings.json` (or `appsettings.Development.json`) and update the SQL Server connection string to match your local setup:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=StaskoTravelDb;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
   }
   ```

3. **Apply Database Migrations:**
   Run the EF Core migrations to create the database schema:
   ```bash
   dotnet ef database update
   ```
   *or via Package Manager Console in Visual Studio:*
   ```powershell
   Update-Database
   ```

4. **Run the Application:**
   ```bash
   dotnet run
   ```
   Navigate to `https://localhost:7123` (or the port indicated in your console) in your web browser.

---

## 📂 Project Structure

```text
StaskoTravel/
├── Controllers/         # MVC Controllers (ActivityController, UserController, etc.)
├── Data/                # DbContext & Database Configurations
├── Factories/           # Custom Identity Claims Factories
├── Models/              # Domain entities & View Models
├── Views/               # Razor Views (_Layout, Activity, User, etc.)
└── wwwroot/
    ├── css/             # Custom Stylesheets
    └── js/              # Modular JavaScript files (search, weather, currency)
```

---

## 📝 License

This project is open-source and available under the [MIT License](LICENSE).
