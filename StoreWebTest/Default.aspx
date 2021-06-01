<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StoreWebTest._Default" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="/Content/Site.css">

    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <table>
            <tr>
                <td class="tdSep"></td>
                <td class="td2"></td>
                <td class="tdSep"></td>
                <td class="td4"></td>
                <td class="tdSep"></td>
                <td></td>
                <td class="tdSep"></td>
                <td></td>
                <td class="tdSep"></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Label runat="server" Text="Product"></asp:Label>
                </td>
                <td></td>
                <td>
                    <asp:Label runat="server" Text="Quantitie"></asp:Label>
                </td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlProduct"></asp:DropDownList>
                </td>
                <td></td>
                <td>
                    <asp:TextBox runat="server" ID="tbQuantitie" TextMode="Number"></asp:TextBox>
                </td>
                <td></td>
                <td>
                    <asp:Button runat="server" ID="btnAddToCart" CssClass="button" Text="Add to Cart" OnClick="btnAddToCart_Click" />
                </td>
                <td></td>
                <td rowspan="5">
                    <asp:TextBox runat="server" ID="tbCart" Enabled="false" TextMode="MultiLine" CssClass="cart"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td>
                    <asp:Button runat="server" ID="btnRemoveFromCart" CssClass="button" Text="Remove from cart" OnClick="btnRemoveFromCart_Click" />
                </td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td>
                    <asp:Button runat="server" ID="btnTotal" CssClass="button" Text="Total" OnClick="btnTotal_Click" />
                </td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
        </table>

</ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
