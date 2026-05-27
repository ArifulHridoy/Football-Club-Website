<%@ Page Title="Select Lineup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SelectLineup.aspx.cs" Inherits="myFootballClub.Admin.SelectLineup" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/style.css" rel="stylesheet" />

    <header class="site-header">
        <nav class="navbar">
            <div class="brand">
                <span class="brand-badge">Admin</span>
                <span class="brand-text">Lineup Control</span>
            </div>
            <ul class="nav-menu">
                <li><a class="nav-link" href="AdminDashboard.aspx">Dashboard</a></li>
                <li><a class="nav-link" href="AddPlayer.aspx">Add Player</a></li>
                <li><a class="nav-link" href="ManagePlayers.aspx">Manage Players</a></li>
                <li><a class="nav-link" href="../FixturesAdmin.aspx">Fixtures</a></li>
                <li><a class="nav-link" href="LiveMatch.aspx">Live Match</a></li>
                <li><a class="nav-link active" href="SelectLineup.aspx">Select Lineup</a></li>
                <li><a class="nav-link" href="MatchEvents.aspx">Match Events</a></li>
                <li><a class="nav-link" href="../NewsAdmin.aspx">News</a></li>
                <li><a class="nav-link" href="../Logout.aspx">Logout</a></li>
            </ul>
        </nav>
    </header>

    <section class="page-hero" style="--hero-image: url('https://img.freepik.com/free-vector/blue-3d-particles-background-design_1017-15410.jpg?semt=ais_hybrid&w=740&q=80');">
        <div class="page-hero-content">
            <p class="eyebrow">Matchday</p>
            <h1>Select Lineup</h1>
            <p>Choose the starting XI and substitutes before kickoff.</p>
        </div>
    </section>

    <section class="section">
        <div class="grid-3">
            <div class="join-form-card">
                <h2>Lineup Selection</h2>
                <div class="join-form-grid">
                    <asp:DropDownList ID="ddlFixture" runat="server" CssClass="input" AutoPostBack="True" OnSelectedIndexChanged="ddlFixture_SelectedIndexChanged"></asp:DropDownList>
                    <asp:DropDownList ID="ddlPlayer" runat="server" CssClass="input"></asp:DropDownList>
                    <asp:DropDownList ID="ddlRole" runat="server" CssClass="input">
                        <asp:ListItem Text="Starting XI" Value="Starting" />
                        <asp:ListItem Text="Substitute" Value="Substitute" />
                    </asp:DropDownList>
                    <asp:Button ID="btnAddLineup" runat="server" Text="Add to Lineup" CssClass="btn btn-primary" OnClick="btnAddLineup_Click" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
                </div>
            </div>
            <div class="card" style="grid-column: span 2;">
                <div class="card-body">
                    <div class="section-head">
                        <p class="eyebrow">Squad</p>
                        <h2>Selected Lineup</h2>
                    </div>
                    <div class="table-wrap">
                        <asp:GridView ID="gvLineup" runat="server" CssClass="ladder-table" AutoGenerateColumns="False" DataKeyNames="LineupId" OnRowDeleting="gvLineup_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="PlayerName" HeaderText="Player" />
                                <asp:BoundField DataField="Role" HeaderText="Role" />
                                <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
