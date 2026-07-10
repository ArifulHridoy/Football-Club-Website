# ⚽ Ariful Football Club (AFC)

[![Build Status](https://img.shields.io/badge/Build-Passing-brightgreen.svg?style=for-the-badge&logo=github)]()
[![Framework](https://img.shields.io/badge/.NET_Framework-4.7.2-512BD4?style=for-the-badge&logo=.net)]()
[![Language](https://img.shields.io/badge/Language-C%23-239120?style=for-the-badge&logo=c-sharp)]()
[![Database](https://img.shields.io/badge/Database-SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server)]()

> A robust and interactive management system for **Ariful Football Club**, tracks live scores, and keeping fans updated with the latest news.

---

## 🌟 Key Features

### 🏟️ Fan Zone (Public)
- **Latest News:** Stay updated with real-time club news and detailed articles.
- **Fixtures & Results:** Check upcoming match schedules and previous match outcomes.
- **Live Match Center:** Experience the thrill with live score updates and match events.
- **Squad Profile:** Explore the player roster and detailed player information.
- **Match Day Lineup:** See the starting XI and tactical formations for every match.

### 🛡️ Admin Command Center
- **Player Management:** Comprehensive system to add, edit, or remove players from the squad.
- **Match Control:** Real-time interface to update live scores and record match events (Goals, Cards, Subs).
- **Lineup Strategist:** Assign players to specific matches and define the starting lineup.
- **News Desk:** Publish and manage club announcements and match reports.

---

## 🛠️ Technology Stack

| Category | Technology |
| :--- | :--- |
| **Backend** | ASP.NET Web Forms (C#) |
| **Frontend** | Bootstrap 5, CSS3, jQuery |
| **Database** | MS SQL Server / LocalDB |
| **Framework** | .NET Framework 4.7.2 |

---

## 🚀 Getting Started

To get a local copy up and running, follow these simple steps:

### Prerequisites
- Visual Studio 2022 or newer
- .NET Framework 4.7.2 SDK
- SQL Server LocalDB

### Installation
1. **Clone the repository:**
   ```bash
   git clone https://github.com/ArifulHridoy/Football-Club-Website.git
   ```
2. **Open the Solution:**
   Open `myFootballClub.slnx` in Visual Studio.
3. **Database Setup:**
   - The application uses `(localdb)\MSSQLLocalDB`.
   - Ensure the database `FootballClubDB` is created or update the connection string in `Web.config`:
	 ```xml
	 <connectionStrings>
		 <add name="dbcon" connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FootballClubDB;Integrated Security=True" providerName="System.Data.SqlClient"/>
	 </connectionStrings>
	 ```
4. **Run the Project:**
   Press `F5` to start the application.

---

## 📂 Project Structure

<details>
<summary><b>Click to see the directory layout</b></summary>

```text
myFootballClub/
├── Admin/              # Admin panel pages (Manage Players, Matches, etc.)
├── App_Code/           # Data access and helper classes (DBHelper)
├── App_Start/          # Configuration for Bundling and Routing
├── Content/            # Bootstrap and Site CSS
├── CSS/                # Custom Stylesheets
├── Scripts/            # JavaScript libraries (jQuery, Bootstrap)
├── Site.Master         # Main layout template
├── Default.aspx        # Home page
└── Web.config          # Application configuration
```
</details>

---

## 🤝 Contributing

Contributions are what make the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 📄 License

Distributed under the MIT License. See `LICENSE` for more information.

---

<p align="center">
  Developed with ❤️ for Football Fans
</p>
