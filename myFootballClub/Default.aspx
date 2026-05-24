<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="myFootballClub._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="CSS/style.css" rel="stylesheet" />

    <header class="site-header">
        <nav class="navbar">
            <div class="brand">
                <span class="brand-badge">FC</span>
                <span class="brand-text">Football Club</span>
            </div>
            <ul class="nav-menu">
                <li><a class="nav-link active" href="Default.aspx">Home</a></li>
                <li><a class="nav-link" href="Players.aspx">Players</a></li>
                <li><a class="nav-link" href="Fixtures.aspx">Fixtures</a></li>
                <li><a class="nav-link" href="LiveScore.aspx">Live Score</a></li>
                <li><a class="nav-link" href="TeamLineup.aspx">Team Lineup</a></li>
                <li><a class="nav-link" href="MatchDetails.aspx">Match Details</a></li>
                <li><a class="nav-link" href="News.aspx">News</a></li>
                <li><a class="nav-link" href="Login.aspx">Login</a></li>
                <li><a class="nav-link" href="Register.aspx">Register</a></li>
            </ul>
        </nav>
    </header>

    <section class="hero">
        <div class="hero-media"></div>
        <div class="hero-overlay"></div>
        <div class="hero-content">
            <p class="eyebrow">Season 2026</p>
            <h1>Unite. Fight. Win.</h1>
            <p class="hero-copy">Stay on top of matchday updates, squad highlights, and the latest club news. Every fixture matters.</p>
            <div class="hero-actions">
                <a class="btn btn-primary" href="Players.aspx">Meet the Squad</a>
                <a class="btn btn-outline" href="Fixtures.aspx">See Fixtures</a>
            </div>
        </div>
        <div class="hero-stats">
            <article>
                <h3>18</h3>
                <p>League Wins</p>
            </article>
            <article>
                <h3>42</h3>
                <p>Goals Scored</p>
            </article>
            <article>
                <h3>12K</h3>
                <p>Club Members</p>
            </article>
        </div>
    </section>

    <section class="section">
        <div class="section-head">
            <p class="eyebrow">Club Hub</p>
            <h2>Everything You Need</h2>
        </div>
        <div class="grid-3">
            <div class="card">
                <img src="https://e0.365dm.com/26/03/1600x900/skysports-premier-league-fixtures_7204726.jpg?20260327154315" alt="Fixtures" />
                <div class="card-body">
                    <p class="meta">Matchday</p>
                    <h3>Fixtures</h3>
                    <p>Plan your week with our full match calendar and results.</p>
                    <a class="btn btn-outline" href="Fixtures.aspx">View fixtures</a>
                </div>
            </div>
            <div class="card">
                <img src="https://www.fcbarcelona.com/photo-resources/2026/05/10/c3020ae2-058d-4d91-8d44-b7d7bf7684c3/JC027929.jpg?width=1200&height=750" alt="Players" />
                <div class="card-body">
                    <p class="meta">Squad</p>
                    <h3>Players</h3>
                    <p>Get to know the talent powering our first team.</p>
                    <a class="btn btn-outline" href="Players.aspx">View players</a>
                </div>
            </div>
            <div class="card">
                <img src="https://melbournecityfc.com.au/wp-content/uploads/sites/6/2026/04/MC_2526_Partnerships_TAC_TeamNews_1920x1080FINALS.jpg" alt="News" />
                <div class="card-body">
                    <p class="meta">Updates</p>
                    <h3>News</h3>
                    <p>Catch every announcement, interview, and match report.</p>
                    <a class="btn btn-outline" href="News.aspx">Read news</a>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
