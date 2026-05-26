<%@ Page Title="News" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="myFootballClub.News" %>

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
                <li><a class="nav-link active" href="News.aspx">News</a></li>
                <li><a class="nav-link" href="Login.aspx">Login</a></li>
                <li><a class="nav-link" href="Register.aspx">Register</a></li>
            </ul>
        </nav>
    </header>

    <section class="page-hero" style="--hero-image: url('https://www.bpf.co.uk/Data/Content/images/football%20-%20header%202.jpg');">
        <div class="page-hero-content">
            <p class="eyebrow">Club News</p>
            <h1>Latest Updates</h1>
            <p>Stay connected to every announcement, match report, and player story.</p>
        </div>
    </section>

    <section class="section">
        <div class="section-head">
            <p class="eyebrow">Highlights</p>
            <h2>From the Club</h2>
        </div>
        <div class="news-grid">
            <asp:Repeater ID="rptNews" runat="server">
                <ItemTemplate>
                    <a class="news-card-link" href='NewsDetails.aspx?id=<%# Eval("NewsId") %>'>
                        <div class="card news-card">
                            <img src="<%# Eval("Image") %>" alt="<%# Eval("Title") %>" />
                            <div class="card-body">
                                <p class="news-meta"><%# Eval("PublishDate", "{0:dd MMM yyyy}") %></p>
                                <h3><%# Eval("Title") %></h3>
                                <p><%# Eval("ShortDescription") %></p>
                                <span class="btn btn-outline">Read more</span>
                            </div>
                        </div>
                    </a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="pager">
            <asp:Button ID="btnPrev" runat="server" Text="Previous" CssClass="btn btn-outline" OnClick="btnPrev_Click" />
            <asp:Label ID="lblPageInfo" runat="server" CssClass="meta"></asp:Label>
            <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="btn btn-outline" OnClick="btnNext_Click" />
        </div>
    </section>
</asp:Content>
