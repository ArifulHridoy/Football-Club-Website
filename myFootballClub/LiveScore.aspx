<%@ Page Title="Live Scores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LiveScore.aspx.cs" Inherits="myFootballClub.LiveScore" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="CSS/style.css" rel="stylesheet" />

    <header class="site-header">
        <nav class="navbar">
            <div class="brand">
                <span class="brand-badge">FC</span>
                <span class="brand-text">Football Club</span>
            </div>
            <ul class="nav-menu">
                <li><a class="nav-link" href="Default.aspx">Home</a></li>
                <li><a class="nav-link" href="Players.aspx">Players</a></li>
                <li><a class="nav-link" href="Fixtures.aspx">Fixtures</a></li>
                <li><a class="nav-link active" href="LiveScore.aspx">Live Score</a></li>
                <li><a class="nav-link" href="News.aspx">News</a></li>
                <li><a class="nav-link" href="Login.aspx">Login</a></li>
                <li><a class="nav-link" href="Register.aspx">Register</a></li>
            </ul>
        </nav>
    </header>

    <section class="page-hero" style="--hero-image: url('https://images.unsplash.com/photo-1502877338535-766e1452684a?auto=format&fit=crop&w=1200&q=80');">
        <div class="page-hero-content">
            <p class="eyebrow">Matchday</p>
            <h1>Live Scores</h1>
            <p>Follow the latest scores and live status for every fixture.</p>
        </div>
    </section>

    <section class="section">
        <div class="section-head">
            <p class="eyebrow">Live Center</p>
            <h2>Scoreboard</h2>
        </div>
        <div class="fixture-grid">
            <asp:Repeater ID="rptLiveScores" runat="server">
                <ItemTemplate>
                    <div class="card fixture-card">
                        <div class="fixture-matchup">
                            <div class="fixture-team">
                                <span class="fixture-team-name"><%# Eval("Team1") %></span>
                            </div>
                            <span class="fixture-vs">VS</span>
                            <div class="fixture-team">
                                <span class="fixture-team-name"><%# Eval("Team2") %></span>
                            </div>
                        </div>
                        <div class="fixture-details">
                            <div class="fixture-detail-item">
                                <span class="fixture-detail-label">Score</span>
                                <span class="fixture-detail-value"><%# Eval("HomeScore") %> - <%# Eval("AwayScore") %></span>
                            </div>
                            <div class="fixture-detail-item">
                                <span class="fixture-detail-label">Status</span>
                                <span class="fixture-detail-value"><%# Eval("Status") %></span>
                            </div>
                            <div class="fixture-detail-item">
                                <span class="fixture-detail-label">Kickoff</span>
                                <span class="fixture-detail-value"><%# Eval("MatchDate", "{0:dd MMM yyyy}") %></span>
                            </div>
                            <div class="fixture-location"><%# Eval("Stadium") %></div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section>
</asp:Content>
