i<%@ Page Title="Live Match Update" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LiveMatch.aspx.cs" Inherits="myFootballClub.Admin.LiveMatch" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/style.css" rel="stylesheet" />

    <header class="site-header">
        <nav class="navbar">
            <div class="brand">
                <span class="brand-badge">Admin</span>
                <span class="brand-text">Live Match</span>
            </div>
            <ul class="nav-menu">
                <li><a class="nav-link" href="AdminDashboard.aspx">Dashboard</a></li>
                <li><a class="nav-link" href="AddPlayer.aspx">Add Player</a></li>
                <li><a class="nav-link" href="ManagePlayers.aspx">Manage Players</a></li>
                <li><a class="nav-link" href="../FixturesAdmin.aspx">Fixtures</a></li>
                <li><a class="nav-link active" href="LiveMatch.aspx">Live Match</a></li>
                <li><a class="nav-link" href="../NewsAdmin.aspx">News</a></li>
                <li><a class="nav-link" href="../Logout.aspx">Logout</a></li>
            </ul>
        </nav>
    </header>

    <section class="page-hero" style="--hero-image: url('https://images.unsplash.com/photo-1502877338535-766e1452684a?auto=format&fit=crop&w=1200&q=80');">
        <div class="page-hero-content">
            <p class="eyebrow">Matchday Control</p>
            <h1>Live Match Update</h1>
            <p>Start matches, update scores, and track scorers in real time.</p>
        </div>
    </section>

    <section class="section">
        <div class="grid-3">
            <div class="join-form-card">
                <h2>Match Control</h2>
                <div class="join-form-grid">
                    <asp:DropDownList ID="ddlFixture" runat="server" CssClass="input" AutoPostBack="True" OnSelectedIndexChanged="ddlFixture_SelectedIndexChanged"></asp:DropDownList>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="input">
                        <asp:ListItem Text="Upcoming" Value="Upcoming" />
                        <asp:ListItem Text="Live" Value="Live" />
                        <asp:ListItem Text="Half Time" Value="Half Time" />
                        <asp:ListItem Text="Finished" Value="Finished" />
                    </asp:DropDownList>
                    <asp:TextBox ID="txtHomeScore" runat="server" CssClass="input" placeholder="Home score"></asp:TextBox>
                    <asp:TextBox ID="txtAwayScore" runat="server" CssClass="input" placeholder="Away score"></asp:TextBox>
                    <asp:Button ID="btnUpdateMatch" runat="server" Text="Update Match" CssClass="btn btn-primary" OnClick="btnUpdateMatch_Click" />
                    <asp:Label ID="lblMatchMessage" runat="server" CssClass="message"></asp:Label>
                </div>
            </div>
            <div class="card" style="grid-column: span 2;">
                <div class="card-body">
                    <div class="section-head">
                        <p class="eyebrow">Goals</p>
                        <h2>Add Goal Event</h2>
                    </div>
                    <div class="join-form-grid">
                        <asp:DropDownList ID="ddlScorer" runat="server" CssClass="input"></asp:DropDownList>
                    <asp:DropDownList ID="ddlScoreSide" runat="server" CssClass="input">
                        <asp:ListItem Text="Home Goal" Value="Home" />
                        <asp:ListItem Text="Away Goal" Value="Away" />
                    </asp:DropDownList>
                        <asp:DropDownList ID="ddlAssist" runat="server" CssClass="input"></asp:DropDownList>
                        <asp:TextBox ID="txtMinute" runat="server" CssClass="input" placeholder="Minute (e.g. 45)" />
                        <asp:Button ID="btnAddGoal" runat="server" Text="Add Goal" CssClass="btn btn-outline" OnClick="btnAddGoal_Click" />
                        <asp:Label ID="lblGoalMessage" runat="server" CssClass="message"></asp:Label>
                    </div>
                    <div class="section-head" style="margin-top: 24px;">
                        <p class="eyebrow">Lineup</p>
                        <h2>Starting XI & Substitutes</h2>
                    </div>
                    <div class="join-form-grid">
                        <asp:DropDownList ID="ddlPlayer" runat="server" CssClass="input"></asp:DropDownList>
                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="input">
                            <asp:ListItem Text="Starting XI" Value="Starting" />
                            <asp:ListItem Text="Substitute" Value="Substitute" />
                        </asp:DropDownList>
                        <asp:Button ID="btnAddLineup" runat="server" Text="Add to Lineup" CssClass="btn btn-outline" OnClick="btnAddLineup_Click" />
                        <asp:Label ID="lblLineupMessage" runat="server" CssClass="message"></asp:Label>
                    </div>
                    <div class="table-wrap" style="margin-top: 20px;">
                        <asp:GridView ID="gvLineup" runat="server" CssClass="ladder-table" AutoGenerateColumns="False" DataKeyNames="LineupId" OnRowDeleting="gvLineup_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="PlayerName" HeaderText="Player" />
                                <asp:BoundField DataField="Role" HeaderText="Role" />
                                <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="section-head" style="margin-top: 24px;">
                        <p class="eyebrow">Events</p>
                        <h2>Goal Timeline</h2>
                    </div>
                    <div class="table-wrap">
                        <asp:GridView ID="gvGoalEvents" runat="server" CssClass="ladder-table" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="Minute" HeaderText="Minute" />
                                <asp:BoundField DataField="PlayerName" HeaderText="Scorer" />
                                <asp:BoundField DataField="AssistName" HeaderText="Assist" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
