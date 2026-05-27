<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="myFootballClub.Admin.AdminDashboard" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/style.css" rel="stylesheet" />

    <header class="site-header">
        <nav class="navbar">
            <div class="brand">
                <span class="brand-badge">Admin</span>
                <span class="brand-text">Control Center</span>
            </div>
            <ul class="nav-menu">
                <li><a class="nav-link active" href="AdminDashboard.aspx">Dashboard</a></li>
                <li><a class="nav-link" href="AddPlayer.aspx">Add Player</a></li>
                <li><a class="nav-link" href="ManagePlayers.aspx">Manage Players</a></li>
                <li><a class="nav-link" href="../FixturesAdmin.aspx">Fixtures</a></li>
                <li><a class="nav-link" href="LiveMatch.aspx">Live Match</a></li>
                <li><a class="nav-link" href="SelectLineup.aspx">Select Lineup</a></li>
                <li><a class="nav-link" href="MatchEvents.aspx">Match Events</a></li>
                <li><a class="nav-link" href="../NewsAdmin.aspx">News</a></li>
                <li><a class="nav-link" href="../Logout.aspx">Logout</a></li>
            </ul>
        </nav>
    </header>

    <section class="page-hero" style="--hero-image: url('https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRtbUwak-CIx-wM3-YN4YJ_aC6NZuyzktFW3Q&s');">
        <div class="page-hero-content">
            <p class="eyebrow">Admin Tools</p>
            <h1>Dashboard</h1>
            <p>Manage every part of the club experience from one place.</p>
            <div class="hero-stats">
                <article>
                    <h3><asp:Label ID="lblPlayerCount" runat="server" Text="0"></asp:Label></h3>
                    <p>Players</p>
                </article>
                <article>
                    <h3><asp:Label ID="lblNewsCount" runat="server" Text="0"></asp:Label></h3>
                    <p>News Articles</p>
                </article>
            </div>
        </div>
    </section>

    <section class="section">
        <div class="section-head">
            <p class="eyebrow">Operations</p>
            <h2>Quick Actions</h2>
        </div>
        <div class="grid-3">
            <div class="card">
                <img src="https://www.sportmonks.com/wp-content/uploads/2023/06/Premier-League-champions-22-23-1200x675.jpg" alt="Add Player" />
                <div class="card-body">
                    <p class="meta">Players</p>
                    <h3>Add Player</h3>
                    <p>Create new player profiles with stats and images.</p>
                    <a class="btn btn-outline" href="AddPlayer.aspx">Add player</a>
                </div>
            </div>
            <div class="card">
                <img src="https://www.coachingsoccerweekly.com/wp-content/uploads/2017/02/CoachPlayer.jpg" alt="Manage Players" />
                <div class="card-body">
                    <p class="meta">Roster</p>
                    <h3>Manage Players</h3>
                    <p>Edit profiles, update stats, and keep the squad fresh.</p>
                    <a class="btn btn-outline" href="ManagePlayers.aspx">Manage roster</a>
                </div>
            </div>
            <div class="card">
                <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTU2ZDkM4ssUhKH9CoEkWRYGHWiGL8PL4b7Jw&s" alt="Fixtures" />
                <div class="card-body">
                    <p class="meta">Schedule</p>
                    <h3>Fixtures</h3>
                    <p>Publish match schedules, results, and stadium details.</p>
                    <a class="btn btn-outline" href="../FixturesAdmin.aspx">Manage fixtures</a>
                </div>
            </div>
            <div class="card">
                <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQMMwoPmW3Ld39IrhPYohvvFyj4XHjWBe_AHA&s" alt="Live Match" />
                <div class="card-body">
                    <p class="meta">Matchday</p>
                    <h3>Live Match Control</h3>
                    <p>Update scores, status, and live match data.</p>
                    <a class="btn btn-outline" href="LiveMatch.aspx">Open live control</a>
                </div>
            </div>
            <div class="card">
                <img src="https://cdn.vectorstock.com/i/1000v/09/11/football-team-formation-starting-list-or-lineups-vector-43950911.jpg" alt="Select Lineup" />
                <div class="card-body">
                    <p class="meta">Squad</p>
                    <h3>Select Lineup</h3>
                    <p>Choose starting XI and bench players.</p>
                    <a class="btn btn-outline" href="SelectLineup.aspx">Select lineup</a>
                </div>
            </div>
            <div class="card">
                <img src="https://img.evbuc.com/https%3A%2F%2Fcdn.evbuc.com%2Fimages%2F1183862371%2F563515756969%2F1%2Foriginal.20260505-215220?crop=focalpoint&fit=crop&w=640&auto=format%2Ccompress&q=75&sharp=10&fp-x=0.5&fp-y=0.108&s=73a5c952181f074928e84a8213aeade6" alt="Match Events" />
                <div class="card-body">
                    <p class="meta">Events</p>
                    <h3>Match Events</h3>
                    <p>Log goals, assists, cards, and substitutions.</p>
                    <a class="btn btn-outline" href="MatchEvents.aspx">Manage events</a>
                </div>
            </div>
            <div class="card">
                <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS39u76rYwAr_V2ROafyq5pyET7dMlxJgfbWw&s" alt="News" />
                <div class="card-body">
                    <p class="meta">Newsroom</p>
                    <h3>News</h3>
                    <p>Share updates, announcements, and media highlights.</p>
                    <a class="btn btn-outline" href="../NewsAdmin.aspx">Manage news</a>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
