<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="myFootballClub.Register" %>

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
                <li><a class="nav-link" href="Login.aspx">Login</a></li>
                <li><a class="nav-link active" href="Register.aspx">Register</a></li>
            </ul>
        </nav>
    </header>

    <section class="page-hero" style="--hero-image: url('https://images.pexels.com/photos/16826138/pexels-photo-16826138.jpeg');">
        <div class="page-hero-content">
            <p class="eyebrow">Join Us</p>
            <h1>Become a Member</h1>
            <p>Create your profile to unlock match alerts and club benefits.</p>
        </div>
    </section>

    <section class="section">
        <div class="join-page-shell">
            <div class="join-page-content">
                <div class="join-info">
                    <h2>Membership Benefits</h2>
                    <p>Get exclusive news, ticket offers, and community updates.</p>
                    <ul class="join-benefits">
                        <li>Season ticket notifications</li>
                        <li>Match highlights newsletter</li>
                        <li>Club community access</li>
                    </ul>
                </div>
                <div class="join-form-card">
                    <h2>Register</h2>
                    <div class="join-form-grid">
                        <asp:TextBox ID="txtName" runat="server" CssClass="input" placeholder="Name"></asp:TextBox>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="input" placeholder="Email"></asp:TextBox>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="input" placeholder="Password"></asp:TextBox>
                        <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary" OnClick="btnRegister_Click" />
                        <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
