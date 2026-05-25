<%@ Page Title="Manage Fixtures" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FixturesAdmin.aspx.cs" Inherits="myFootballClub.FixturesAdmin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="CSS/style.css" rel="stylesheet" />

    <header class="site-header">
        <nav class="navbar">
            <div class="brand">
                <span class="brand-badge">Admin</span>
                <span class="brand-text">Fixture Control</span>
            </div>
            <ul class="nav-menu">
                <li><a class="nav-link" href="Admin/AdminDashboard.aspx">Dashboard</a></li>
                <li><a class="nav-link" href="Admin/AddPlayer.aspx">Add Player</a></li>
                <li><a class="nav-link" href="Admin/ManagePlayers.aspx">Manage Players</a></li>
                <li><a class="nav-link active" href="FixturesAdmin.aspx">Fixtures</a></li>
                <li><a class="nav-link" href="NewsAdmin.aspx">News</a></li>
                <li><a class="nav-link" href="Logout.aspx">Logout</a></li>
            </ul>
        </nav>
    </header>

    <section class="page-hero" style="--hero-image: url('https://images.unsplash.com/photo-1502877338535-766e1452684a?auto=format&fit=crop&w=1200&q=80');">
        <div class="page-hero-content">
            <p class="eyebrow">Matchday</p>
            <h1>Manage Fixtures</h1>
            <p>Publish upcoming matches and update results in real time.</p>
        </div>
    </section>

    <section class="section">
        <div class="grid-3">
            <div class="join-form-card">
                <h2>Add Fixture</h2>
                <div class="join-form-grid">
                    <asp:TextBox ID="txtMatchDate" runat="server" CssClass="input" placeholder="Match date (YYYY-MM-DD)"></asp:TextBox>
                    <asp:TextBox ID="txtTeam1" runat="server" CssClass="input" placeholder="Team 1"></asp:TextBox>
                    <asp:TextBox ID="txtTeam2" runat="server" CssClass="input" placeholder="Team 2"></asp:TextBox>
                    <asp:TextBox ID="txtStadium" runat="server" CssClass="input" placeholder="Stadium"></asp:TextBox>
                    <asp:TextBox ID="txtResult" runat="server" CssClass="input" placeholder="Result"></asp:TextBox>
                    <asp:TextBox ID="txtStatus" runat="server" CssClass="input" placeholder="Status (Scheduled/Live/Final)"></asp:TextBox>
                    <asp:Button ID="btnAddFixture" runat="server" Text="Add Fixture" CssClass="btn btn-primary" OnClick="btnAddFixture_Click" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
                </div>
            </div>
            <div class="card" style="grid-column: span 2;">
                <div class="card-body">
                    <div class="section-head">
                        <p class="eyebrow">Schedule</p>
                        <h2>Fixture List</h2>
                    </div>
                    <div class="table-wrap">
                        <asp:GridView ID="gvFixtures" runat="server" CssClass="ladder-table" AutoGenerateColumns="False" DataKeyNames="FixtureId" OnRowEditing="gvFixtures_RowEditing" OnRowCancelingEdit="gvFixtures_RowCancelingEdit" OnRowUpdating="gvFixtures_RowUpdating" OnRowDeleting="gvFixtures_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="FixtureId" HeaderText="ID" ReadOnly="True" />
                                <asp:BoundField DataField="MatchDate" HeaderText="Date" />
                                <asp:BoundField DataField="Team1" HeaderText="Team 1" />
                                <asp:BoundField DataField="Team2" HeaderText="Team 2" />
                                <asp:BoundField DataField="Stadium" HeaderText="Stadium" />
                                <asp:BoundField DataField="Result" HeaderText="Result" />
                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
