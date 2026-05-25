<%@ Page Title="Players" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Players.aspx.cs" Inherits="myFootballClub.Players" %>

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
                <li><a class="nav-link active" href="Players.aspx">Players</a></li>
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

    <section class="page-hero" style="--hero-image: url('https://img.freepik.com/free-vector/blue-3d-particles-background-design_1017-15410.jpg?semt=ais_hybrid&w=740&q=80');">
        <div class="page-hero-content">
            <p class="eyebrow">First Team</p>
            <h1>Meet the Squad</h1>
            <p>Discover the players who bring the club to life every match day.</p>
        </div>
    </section>

    <section class="section">
        <div class="section-head">
            <p class="eyebrow">Lineup</p>
            <h2>Player Roster</h2>
        </div>
        <div class="search-bar">
            <asp:TextBox ID="txtPlayerSearch" runat="server" CssClass="input" placeholder="Search players..."></asp:TextBox>
            <asp:Button ID="btnPlayerSearch" runat="server" Text="Search" CssClass="btn btn-outline" OnClick="btnPlayerSearch_Click" />
            <asp:Button ID="btnPlayerClear" runat="server" Text="Clear" CssClass="btn btn-outline" OnClick="btnPlayerClear_Click" />
        </div>
        <div class="grid-4">
            <asp:Repeater ID="rptPlayers" runat="server">
                <ItemTemplate>
                    <div class="card player-roster-card">
                        <div class="player-media">
                            <img class="player-photo" src="<%# Eval("Photo") %>" alt="<%# Eval("Name") %>" />
                            <span class="player-badge"><%# Eval("Position") %></span>
                        </div>
                        <h3><%# Eval("Name") %></h3>
                        <p class="player-roster-meta">Goals: <%# Eval("Goals") %></p>
                        <p class="meta">Appearances: <%# Eval("Appearances") %> • Assists: <%# Eval("Assists") %></p>
                        <p class="meta">Fitness: <%# Eval("FitnessStatus") %> • Injury: <%# Eval("InjuryStatus") %></p>
                        <ul class="player-stats">
                            <li>Position: <%# Eval("Position") %></li>
                            <li>Season Goals: <%# Eval("Goals") %></li>
                            <li>Assists: <%# Eval("Assists") %></li>
                            <li>Appearances: <%# Eval("Appearances") %></li>
                            <li>Fitness: <%# Eval("FitnessStatus") %></li>
                            <li>Injury: <%# Eval("InjuryStatus") %></li>
                        </ul>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section>
</asp:Content>
