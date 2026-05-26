<%@ Page Title="News Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewsDetails.aspx.cs" Inherits="myFootballClub.NewsDetails" %>

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
                <li><a class="nav-link active" href="News.aspx">News</a></li>
                <li><a class="nav-link" href="Login.aspx">Login</a></li>
                <li><a class="nav-link" href="Register.aspx">Register</a></li>
            </ul>
        </nav>
    </header>

    <section class="page-hero" style="--hero-image: url('https://images.unsplash.com/photo-1518600506278-4e8ef466b810?auto=format&fit=crop&w=1200&q=80');">
        <div class="page-hero-content">
            <p class="eyebrow">Club News</p>
            <h1>News Detail</h1>
            <p>Full story and matchday coverage from the club.</p>
        </div>
    </section>

    <section class="section">
        <asp:Panel ID="pnlNews" runat="server" CssClass="article-page" Visible="false">
            <div class="article-panel">
                <img id="imgNews" runat="server" class="article-media" alt="News" />
                <div class="article-byline">
                    <h2 id="lblTitle" runat="server"></h2>
                    <div class="byline-meta">
                        <span class="byline-date" id="lblDate" runat="server"></span>
                        <span class="byline-category">Club Update</span>
                    </div>
                </div>
                <div class="article-body">
                    <p id="lblDescription" runat="server"></p>
                </div>
            </div>
            <a class="article-back" href="News.aspx">Back to news</a>
        </asp:Panel>
        <asp:Panel ID="pnlComments" runat="server" CssClass="section-dark" Visible="false">
            <div class="section-head">
                <p class="eyebrow">Community</p>
                <h2>Comments</h2>
            </div>
            <div class="join-form-card">
                <div class="join-form-grid">
                    <asp:TextBox ID="txtCommentName" runat="server" CssClass="input" placeholder="Your name"></asp:TextBox>
                    <asp:TextBox ID="txtCommentEmail" runat="server" CssClass="input" placeholder="Email"></asp:TextBox>
                    <asp:TextBox ID="txtComment" runat="server" CssClass="textarea" TextMode="MultiLine" placeholder="Write your comment"></asp:TextBox>
                    <asp:Button ID="btnSubmitComment" runat="server" Text="Post Comment" CssClass="btn btn-primary" OnClick="btnSubmitComment_Click" />
                    <asp:Label ID="lblCommentMessage" runat="server" CssClass="message"></asp:Label>
                </div>
            </div>
            <asp:Repeater ID="rptComments" runat="server">
                <HeaderTemplate><div class="comment-list"></HeaderTemplate>
                <ItemTemplate>
                    <div class="comment-card">
                        <div class="comment-meta"><%# Eval("UserName") %> • <%# Eval("CreatedAt", "{0:dd MMM yyyy}") %></div>
                        <p><%# Eval("CommentText") %></p>
                    </div>
                </ItemTemplate>
                <FooterTemplate></div></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
        <asp:Panel ID="pnlNotFound" runat="server" CssClass="card" Visible="false">
            <div class="card-body">
                <h2>News not found</h2>
                <p>The requested article could not be found.</p>
                <a class="btn btn-outline" href="News.aspx">Back to news</a>
            </div>
        </asp:Panel>
    </section>
</asp:Content>
