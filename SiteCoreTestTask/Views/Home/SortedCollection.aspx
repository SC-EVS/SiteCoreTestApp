<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IOrderedEnumerable<KeyValuePair<string,int>>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SortedCollection
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <%--<%if (Model != null && Model.Count()!=0)
      {%>
    <% foreach (KeyValuePair<string, int> kvp in Model)
       { %>
       <%= kvp.Key%>  <%= kvp.Value%>
    <%  } %>
    <%} %>--%>

  <% if (Model != null)
     {%>  

            <table border="2" width="350" >
                    <tr align="center">
                        <th>
                            Стоп-слово
                        </th>
                         <th>
                            Количество
                        </th>
                    </tr>
                    <% foreach (var item in Model)
                       { %>
                    <tr align="center">
                        <td>
                            <%= item.Key%>
                        </td>

                        <td>
                            <%= item.Value %>                       
                        </td>
                    </tr>
                    <% } %>
                </table>
                  <%} %>
</asp:Content>
