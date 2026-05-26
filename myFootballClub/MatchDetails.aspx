<%@ Page Title="Match Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MatchDetails.aspx.cs" Inherits="myFootballClub.MatchDetails" %>

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
                <li><a class="nav-link active" href="MatchDetails.aspx">Match Details</a></li>
                <li><a class="nav-link" href="News.aspx">News</a></li>
                <li><a class="nav-link" href="Login.aspx">Login</a></li>
                <li><a class="nav-link" href="Register.aspx">Register</a></li>
            </ul>
        </nav>
    </header>

    <section class="page-hero" style="--hero-image: url('https://images.unsplash.com/photo-1517927033932-b3d18e61fb3a?auto=format&fit=crop&w=1200&q=80');">
        <div class="page-hero-content">
            <p class="eyebrow">Matchday</p>
            <h1>Match Details</h1>
            <p>Review scorers and assists for each fixture.</p>
        </div>
    </section>

    <section class="section">
        <div class="section-head">
            <p class="eyebrow">Fixture</p>
            <h2>Select Fixture</h2>
        </div>
        <div class="search-bar">
            <asp:DropDownList ID="ddlFixture" runat="server" CssClass="input"></asp:DropDownList>
            <asp:Button ID="btnLoadDetails" runat="server" Text="Load Details" CssClass="btn btn-outline" OnClick="btnLoadDetails_Click" />
        </div>

        <div class="card" style="margin-top: 20px;">
            <div class="card-body">
                <div class="section-head">
                    <p class="eyebrow">Goals</p>
                    <h2>Scorers & Assists</h2>
                </div>
                <div class="table-wrap">
                    <asp:GridView ID="gvEvents" runat="server" CssClass="ladder-table" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="Minute" HeaderText="Minute" />
                            <asp:BoundField DataField="Scorer" HeaderText="Scorer" />
                            <asp:BoundField DataField="Assist" HeaderText="Assist" />
                            <asp:BoundField DataField="EventType" HeaderText="Event" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="section-head" style="margin-top: 24px;">
                    <p class="eyebrow">Timeline</p>
                    <h2>Match Timeline</h2>
                </div>
                <asp:Repeater ID="rptTimeline" runat="server">
                    <ItemTemplate>
                        <p class="meta"><%# Eval("Minute") %>' - <%# Eval("EventSummary") %></p>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>
</asp:Content>
