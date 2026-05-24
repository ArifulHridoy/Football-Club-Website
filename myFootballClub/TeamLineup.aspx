<%@ Page Title="Team Lineup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TeamLineup.aspx.cs" Inherits="myFootballClub.TeamLineup" %>

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
                <li><a class="nav-link" href="LiveScore.aspx">Live Score</a></li>
                <li><a class="nav-link active" href="TeamLineup.aspx">Team Lineup</a></li>
                <li><a class="nav-link" href="News.aspx">News</a></li>
                <li><a class="nav-link" href="Login.aspx">Login</a></li>
                <li><a class="nav-link" href="Register.aspx">Register</a></li>
            </ul>
        </nav>
    </header>

    <section class="page-hero" style="--hero-image: url('https://images.unsplash.com/photo-1508098682722-e99c43a406b2?auto=format&fit=crop&w=1200&q=80');">
        <div class="page-hero-content">
            <p class="eyebrow">Matchday</p>
            <h1>Team Lineup</h1>
            <p>View the starting XI and substitutes for each fixture.</p>
        </div>
    </section>

    <section class="section">
        <div class="section-head">
            <p class="eyebrow">Fixture</p>
            <h2>Select Fixture</h2>
        </div>
        <div class="search-bar">
            <asp:DropDownList ID="ddlFixture" runat="server" CssClass="input"></asp:DropDownList>
            <asp:Button ID="btnLoadLineup" runat="server" Text="Load Lineup" CssClass="btn btn-outline" OnClick="btnLoadLineup_Click" />
        </div>

        <div class="grid-2" style="margin-top: 20px;">
            <div class="card">
                <div class="card-body">
                    <div class="section-head">
                        <p class="eyebrow">Starting XI</p>
                        <h2>First Team</h2>
                    </div>
                    <asp:Repeater ID="rptStarting" runat="server">
                        <ItemTemplate>
                            <p class="meta"><%# Eval("Name") %> • <%# Eval("Position") %></p>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="section-head">
                        <p class="eyebrow">Substitutes</p>
                        <h2>Bench</h2>
                    </div>
                    <asp:Repeater ID="rptSubstitutes" runat="server">
                        <ItemTemplate>
                            <p class="meta"><%# Eval("Name") %> • <%# Eval("Position") %></p>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
