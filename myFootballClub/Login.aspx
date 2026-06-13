<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="myFootballClub.Login" %>

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
                <li><a class="nav-link" href="TeamLineup.aspx">Team Lineup</a></li>
                <li><a class="nav-link" href="MatchDetails.aspx">Match Details</a></li>
                <li><a class="nav-link" href="News.aspx">News</a></li>
                <li><a class="nav-link active" href="Login.aspx">Login</a></li>
                <li><a class="nav-link" href="Register.aspx">Register</a></li>
            </ul>
        </nav>
    </header>

    <section class="page-hero" style="--hero-image: url('https://images.pexels.com/photos/16826138/pexels-photo-16826138.jpeg');">
        <div class="page-hero-content">
            <p class="eyebrow">Member Access</p>
            <h1>Welcome Back</h1>
            <p>Sign in to manage your profile, tickets, and club updates.</p>
        </div>
    </section>

    <section class="section">
        <div class="join-page-shell">
            <div class="join-page-content">
                <div class="join-info">
                    <h2>Member Portal</h2>
                    <p>Access exclusive matchday content, squad updates, and club benefits.</p>
                    <ul class="join-benefits">
                        <li>Priority ticket alerts</li>
                        <li>Exclusive player news</li>
                        <li>Matchday reminders</li>
                    </ul>
                </div>
                <div class="join-form-card">
                    <h2>Login</h2>
                    <div class="join-form-grid">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="input" placeholder="Email"></asp:TextBox>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="input" placeholder="Password"></asp:TextBox>
                        <asp:CheckBox ID="chkRemember" runat="server" Text="Remember Me" />
                        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
                        <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
