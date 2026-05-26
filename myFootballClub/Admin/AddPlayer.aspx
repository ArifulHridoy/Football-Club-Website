<%@ Page Title="Add Player" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddPlayer.aspx.cs" Inherits="myFootballClub.Admin.AddPlayer" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/style.css" rel="stylesheet" />

    <header class="site-header">
        <nav class="navbar">
            <div class="brand">
                <span class="brand-badge">Admin</span>
                <span class="brand-text">Player Management</span>
            </div>
            <ul class="nav-menu">
                <li><a class="nav-link" href="AdminDashboard.aspx">Dashboard</a></li>
                <li><a class="nav-link active" href="AddPlayer.aspx">Add Player</a></li>
                <li><a class="nav-link" href="ManagePlayers.aspx">Manage Players</a></li>
                <li><a class="nav-link" href="../FixturesAdmin.aspx">Fixtures</a></li>
                <li><a class="nav-link" href="../NewsAdmin.aspx">News</a></li>
                <li><a class="nav-link" href="../Logout.aspx">Logout</a></li>
            </ul>
        </nav>
    </header>

    <section class="page-hero" style="--hero-image: url('https://img.freepik.com/free-vector/blue-3d-particles-background-design_1017-15410.jpg?semt=ais_hybrid&w=740&q=80');">
        <div class="page-hero-content">
            <p class="eyebrow">Roster</p>
            <h1>Add Player</h1>
            <p>Build the squad with new player profiles and stats.</p>
        </div>
    </section>

    <section class="section">
        <div class="join-form-card">
            <h2>Player Details</h2>
            <div class="join-form-grid">
                <asp:TextBox ID="txtName" runat="server" CssClass="input" placeholder="Player name"></asp:TextBox>
                <asp:TextBox ID="txtPosition" runat="server" CssClass="input" placeholder="Position"></asp:TextBox>
                <asp:TextBox ID="txtGoals" runat="server" CssClass="input" placeholder="Goals"></asp:TextBox>
                <asp:TextBox ID="txtAssists" runat="server" CssClass="input" placeholder="Assists"></asp:TextBox>
                <asp:TextBox ID="txtAppearances" runat="server" CssClass="input" placeholder="Appearances"></asp:TextBox>
                <asp:TextBox ID="txtFitnessStatus" runat="server" CssClass="input" placeholder="Fitness Status (Fit/Unfit)"></asp:TextBox>
                <asp:TextBox ID="txtInjuryStatus" runat="server" CssClass="input" placeholder="Injury Status (Available/Injured)"></asp:TextBox>
                <asp:TextBox ID="txtPhotoUrl" runat="server" CssClass="input" placeholder="Photo URL"></asp:TextBox>
                <asp:Button ID="btnAdd" runat="server" Text="Add Player" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
            </div>
        </div>
    </section>
</asp:Content>
