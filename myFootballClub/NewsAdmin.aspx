<%@ Page Title="Manage News" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewsAdmin.aspx.cs" Inherits="myFootballClub.NewsAdmin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="CSS/style.css" rel="stylesheet" />

    <header class="site-header">
        <nav class="navbar">
            <div class="brand">
                <span class="brand-badge">Admin</span>
                <span class="brand-text">Newsroom</span>
            </div>
            <ul class="nav-menu">
                <li><a class="nav-link" href="Admin/AdminDashboard.aspx">Dashboard</a></li>
                <li><a class="nav-link" href="Admin/AddPlayer.aspx">Add Player</a></li>
                <li><a class="nav-link" href="Admin/ManagePlayers.aspx">Manage Players</a></li>
                <li><a class="nav-link" href="FixturesAdmin.aspx">Fixtures</a></li>
                <li><a class="nav-link active" href="NewsAdmin.aspx">News</a></li>
                <li><a class="nav-link" href="Logout.aspx">Logout</a></li>
            </ul>
        </nav>
    </header>

    <section class="page-hero" style="--hero-image: url('https://images.pexels.com/photos/16826138/pexels-photo-16826138.jpeg');">
        <div class="page-hero-content">
            <p class="eyebrow">Newsroom</p>
            <h1>Manage News</h1>
            <p>Publish the stories that keep supporters connected.</p>
        </div>
    </section>

    <section class="section">
        <div class="grid-3">
            <div class="join-form-card">
                <h2>Add News</h2>
                <div class="join-form-grid">
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="input" placeholder="Title"></asp:TextBox>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="textarea" TextMode="MultiLine" placeholder="News content"></asp:TextBox>
                    <asp:FileUpload ID="fuNewsImage" runat="server" CssClass="input" />
                    <asp:TextBox ID="txtPublishDate" runat="server" CssClass="input" placeholder="Publish date (YYYY-MM-DD)"></asp:TextBox>
                    <asp:Button ID="btnAddNews" runat="server" Text="Add News" CssClass="btn btn-primary" OnClick="btnAddNews_Click" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
                </div>
            </div>
            <div class="card" style="grid-column: span 2;">
                <div class="card-body">
                    <div class="section-head">
                        <p class="eyebrow">Articles</p>
                        <h2>News List</h2>
                    </div>
                    <div class="table-wrap">
                        <asp:GridView ID="gvNews" runat="server" CssClass="ladder-table" AutoGenerateColumns="False" DataKeyNames="NewsId" OnRowEditing="gvNews_RowEditing" OnRowCancelingEdit="gvNews_RowCancelingEdit" OnRowUpdating="gvNews_RowUpdating" OnRowDeleting="gvNews_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="NewsId" HeaderText="ID" ReadOnly="True" />
                                <asp:BoundField DataField="Title" HeaderText="Title" />
                                <asp:TemplateField HeaderText="Description">

    <ItemTemplate>
        <%# Eval("Description") != null && Eval("Description").ToString().Length > 80
            ? Eval("Description").ToString().Substring(0, 80) + "..."
            : Eval("Description") %>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:TextBox ID="txtEditDescription"
            runat="server"
            Text='<%# Bind("Description") %>'
            TextMode="MultiLine"
            Rows="6"
            Width="100%"
            CssClass="textarea">
        </asp:TextBox>
    </EditItemTemplate>

</asp:TemplateField>
                                <asp:BoundField DataField="Image" HeaderText="Image" />
                                <asp:BoundField DataField="PublishDate" HeaderText="Publish Date" />
                                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
