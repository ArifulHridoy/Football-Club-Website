<%@ Page Title="Manage Players" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManagePlayers.aspx.cs" Inherits="myFootballClub.Admin.ManagePlayers" %>

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
                <li><a class="nav-link" href="AddPlayer.aspx">Add Player</a></li>
                <li><a class="nav-link active" href="ManagePlayers.aspx">Manage Players</a></li>
                <li><a class="nav-link" href="../FixturesAdmin.aspx">Fixtures</a></li>
                <li><a class="nav-link" href="../NewsAdmin.aspx">News</a></li>
                <li><a class="nav-link" href="../Logout.aspx">Logout</a></li>
            </ul>
        </nav>
    </header>

    <section class="page-hero" style="--hero-image: url('https://img.freepik.com/free-vector/blue-3d-particles-background-design_1017-15410.jpg?semt=ais_hybrid&w=740&q=80');">
        <div class="page-hero-content">
            <p class="eyebrow">Roster Control</p>
            <h1>Manage Players</h1>
            <p>Update player details, stats, and photos in one place.</p>
        </div>
    </section>

    <section class="section">
        <div class="section-head">
            <p class="eyebrow">Squad</p>
            <h2>Player Directory</h2>
        </div>
        <div class="table-wrap">
            <asp:GridView ID="gvPlayers" runat="server" CssClass="ladder-table" AutoGenerateColumns="False" DataKeyNames="PlayerId" OnRowEditing="gvPlayers_RowEditing" OnRowCancelingEdit="gvPlayers_RowCancelingEdit" OnRowUpdating="gvPlayers_RowUpdating" OnRowDeleting="gvPlayers_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="PlayerId" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Position" HeaderText="Position" />
                    <asp:BoundField DataField="Goals" HeaderText="Goals" />
                    <asp:BoundField DataField="Assists" HeaderText="Assists" />
                    <asp:BoundField DataField="Appearances" HeaderText="Appearances" />
                    <asp:BoundField DataField="Photo" HeaderText="Photo URL" />
                    <asp:BoundField DataField="FitnessStatus" HeaderText="Fitness" />
                    <asp:BoundField DataField="InjuryStatus" HeaderText="Injury" />
                    <asp:BoundField DataField="InjuryNotes" HeaderText="Injury Notes" />
                    <asp:BoundField DataField="RecoveryDate" HeaderText="Recovery Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </section>
</asp:Content>
