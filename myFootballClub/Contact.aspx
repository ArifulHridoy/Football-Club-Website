<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="myFootballClub.Contact" %>

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
                <li><a class="nav-link" href="News.aspx">News</a></li>
                <li><a class="nav-link active" href="Contact.aspx">Contact</a></li>
                <li><a class="nav-link" href="Login.aspx">Login</a></li>
            </ul>
        </nav>
    </header>

    <section class="page-hero" style="--hero-image: url('https://images.unsplash.com/photo-1500530855697-b586d89ba3ee?auto=format&fit=crop&w=1200&q=80');">
        <div class="page-hero-content">
            <p class="eyebrow">Get in Touch</p>
            <h1>Contact the Club</h1>
            <p>Send us a message and our team will reach you soon.</p>
        </div>
    </section>

    <section class="section">
        <div class="contact-layout">
            <div class="contact-card">
                <h2>Club Office</h2>
                <p>We are available during weekdays for enquiries.</p>
                <ul class="contact-list">
                    <li>Email: hello@footballclub.com</li>
                    <li>Phone: +1 (555) 012-3456</li>
                    <li>Address: 88 Stadium Avenue, City</li>
                </ul>
            </div>
            <div class="contact-form-wrap">
                <h2>Send a Message</h2>
                <div class="join-form-grid">
                    <asp:TextBox ID="txtContactName" runat="server" CssClass="input" placeholder="Name"></asp:TextBox>
                    <asp:TextBox ID="txtContactEmail" runat="server" CssClass="input" placeholder="Email"></asp:TextBox>
                    <asp:TextBox ID="txtContactSubject" runat="server" CssClass="input" placeholder="Subject"></asp:TextBox>
                    <asp:TextBox ID="txtContactMessage" runat="server" CssClass="textarea" TextMode="MultiLine" placeholder="Message"></asp:TextBox>
                    <asp:Button ID="btnSend" runat="server" Text="Send Message" CssClass="btn btn-primary" OnClick="btnSend_Click" />
                    <asp:Label ID="lblContactMessage" runat="server" CssClass="message"></asp:Label>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
