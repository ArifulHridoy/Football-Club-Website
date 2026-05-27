<%@ Page Title="Match Events" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MatchEvents.aspx.cs" Inherits="myFootballClub.Admin.MatchEvents" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/style.css" rel="stylesheet" />

    <header class="site-header">
        <nav class="navbar">
            <div class="brand">
                <span class="brand-badge">Admin</span>
                <span class="brand-text">Match Events</span>
            </div>
            <ul class="nav-menu">
                <li><a class="nav-link" href="AdminDashboard.aspx">Dashboard</a></li>
                <li><a class="nav-link" href="AddPlayer.aspx">Add Player</a></li>
                <li><a class="nav-link" href="ManagePlayers.aspx">Manage Players</a></li>
                <li><a class="nav-link" href="../FixturesAdmin.aspx">Fixtures</a></li>
                <li><a class="nav-link" href="LiveMatch.aspx">Live Match</a></li>
                <li><a class="nav-link" href="SelectLineup.aspx">Select Lineup</a></li>
                <li><a class="nav-link active" href="MatchEvents.aspx">Match Events</a></li>
                <li><a class="nav-link" href="../NewsAdmin.aspx">News</a></li>
                <li><a class="nav-link" href="../Logout.aspx">Logout</a></li>
            </ul>
        </nav>
    </header>

    <section class="page-hero" style="--hero-image: url('https://img.freepik.com/free-vector/blue-3d-particles-background-design_1017-15410.jpg?semt=ais_hybrid&w=740&q=80');">
        <div class="page-hero-content">
            <p class="eyebrow">Matchday</p>
            <h1>Match Events</h1>
            <p>Log goals, assists, cards, and substitutions in real time.</p>
        </div>
    </section>

    <section class="section">
        <div class="grid-3">
            <div class="join-form-card">
                <h2>Add Event</h2>
                <div class="join-form-grid">
                    <asp:DropDownList ID="ddlFixture" runat="server" CssClass="input" AutoPostBack="True" OnSelectedIndexChanged="ddlFixture_SelectedIndexChanged"></asp:DropDownList>
                    <asp:DropDownList ID="ddlEventType" runat="server" CssClass="input">
                        <asp:ListItem Text="Goal" Value="Goal" />
                        <asp:ListItem Text="Assist" Value="Assist" />
                        <asp:ListItem Text="Yellow Card" Value="YellowCard" />
                        <asp:ListItem Text="Red Card" Value="RedCard" />
                        <asp:ListItem Text="Substitution" Value="Substitution" />
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlPlayer" runat="server" CssClass="input"></asp:DropDownList>
                    <asp:DropDownList ID="ddlAssist" runat="server" CssClass="input"></asp:DropDownList>
                    <asp:TextBox ID="txtMinute" runat="server" CssClass="input" placeholder="Minute"></asp:TextBox>
                    <asp:DropDownList ID="ddlScoreSide" runat="server" CssClass="input">
                        <asp:ListItem Text="Home Goal" Value="Home" />
                        <asp:ListItem Text="Away Goal" Value="Away" />
                    </asp:DropDownList>
                    <asp:Button ID="btnAddEvent" runat="server" Text="Add Event" CssClass="btn btn-primary" OnClick="btnAddEvent_Click" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
                </div>
            </div>
            <div class="card" style="grid-column: span 2;">
                <div class="card-body">
                    <div class="section-head">
                        <p class="eyebrow">Timeline</p>
                        <h2>Event Log</h2>
                    </div>
                    <div class="table-wrap">
                        <asp:GridView ID="gvEvents" runat="server" CssClass="ladder-table" AutoGenerateColumns="False" DataKeyNames="EventId" OnRowDeleting="gvEvents_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="Minute" HeaderText="Minute" />
                                <asp:BoundField DataField="EventType" HeaderText="Event" />
                                <asp:BoundField DataField="PlayerName" HeaderText="Player" />
                                <asp:BoundField DataField="AssistName" HeaderText="Assist" />
                                <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
