<%@ Page Title="Fixtures" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Fixtures.aspx.cs" Inherits="myFootballClub.Fixtures" %>

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
                <li><a class="nav-link active" href="Fixtures.aspx">Fixtures</a></li>
                <li><a class="nav-link" href="LiveScore.aspx">Live Score</a></li>
                <li><a class="nav-link" href="TeamLineup.aspx">Team Lineup</a></li>
                <li><a class="nav-link" href="MatchDetails.aspx">Match Details</a></li>
                <li><a class="nav-link" href="News.aspx">News</a></li>
                <li><a class="nav-link" href="Login.aspx">Login</a></li>
                <li><a class="nav-link" href="Register.aspx">Register</a></li>
            </ul>
        </nav>
    </header>

    <section class="page-hero" style="--hero-image: url('https://img.freepik.com/free-vector/blue-3d-particles-background-design_1017-15410.jpg?semt=ais_hybrid&w=740&q=80');">
        <div class="page-hero-content">
            <p class="eyebrow">Matchday</p>
            <h1>Upcoming Fixtures</h1>
            <p>All the dates you need to follow the season from kickoff to full time.</p>
        </div>
    </section>

    <section class="section">
        <div class="section-head">
            <p class="eyebrow">Schedule</p>
            <h2>Fixture Cards</h2>
        </div>
        <div class="search-bar">
            <asp:TextBox ID="txtFixtureSearch" runat="server" CssClass="input" placeholder="Search team or stadium..."></asp:TextBox>
            <asp:Button ID="btnFixtureSearch" runat="server" Text="Search" CssClass="btn btn-outline" OnClick="btnFixtureSearch_Click" />
            <asp:Button ID="btnFixtureClear" runat="server" Text="Clear" CssClass="btn btn-outline" OnClick="btnFixtureClear_Click" />
        </div>
        <div class="fixture-grid">
            <asp:Repeater ID="rptFixtures" runat="server">
                <ItemTemplate>
                    <div class="card fixture-card">
                        <img class="fixture-image" src="https://images.unsplash.com/photo-1489944440615-453fc2b6a9a9?auto=format&fit=crop&w=800&q=80" alt="Fixture" />
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
                                <span class="fixture-detail-label">Match Date</span>
                                <span class="fixture-detail-value"><%# Eval("MatchDate", "{0:dd MMM yyyy}") %></span>
                            </div>
                            <div class="fixture-detail-item">
                                <span class="fixture-detail-label">Result</span>
                                <span class="fixture-detail-value"><%# Eval("Result") %></span>
                            </div>
                            <div class="fixture-detail-item">
                                <span class="fixture-detail-label">Score</span>
                                <span class="fixture-detail-value"><%# Eval("HomeScore") %> - <%# Eval("AwayScore") %></span>
                            </div>
                            <div class="fixture-detail-item">
                                <span class="fixture-detail-label">Status</span>
                                <span class="fixture-detail-value"><%# Eval("Status") %></span>
                            </div>
                            <div class="fixture-location"><%# Eval("Stadium") %></div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section>
</asp:Content>
