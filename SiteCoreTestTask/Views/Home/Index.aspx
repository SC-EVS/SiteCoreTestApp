<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Домашняя страница
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%: ViewData["Message"] %></h2>
    <form action="<%= Url.Action("SeoAnalysis","Home") %>" method="post" align="center">
    Введите сюда текст, либо URL:
    <br />
    <textarea rows="10" cols="60" name="textOrUrl"> </textarea><br />
    <input id="eachWordOnPage" type="checkbox" value="true" name="eachWordOnPage" checked="checked" />каждое
    стоп-слово на странице
    <br />
    <input type="hidden" value="false" name="eachWordOnPage" />
    <input id="eachWordOnPageInMetaTags" type="checkbox" value="true" name="eachWordOnPageInMetaTags" />Каждое
    стоп-слово на странице в мета-тегах
    <br />
    <input type="hidden" value="false" name="eachWordOnPageInMetaTags" />
    <input id="externalLinks" type="checkbox" value="true" name="externalLinks" />Искать
    внешние и внутренние ссылки
    <br />
    <input type="hidden" value="false" name="externalLinks" />
    <button type="submit">
        Анализировать!</button>
    </form>
    <%  Dictionary<string, int> stopWordsOnPage = ViewData["StopWordsOnPage"] as Dictionary<string, int>;%>
    <%  Dictionary<string, int> stopWordsInMetaDict = ViewData["StopWordsInMetaTags"] as Dictionary<string, int>;%>
    <% IList<string> externalLinks = ViewData["ExternalLinks"] as IList<string>;%>


    <h2 >Количество стоп-слов в мета-тегах:</h2>
    <%= Ajax.ActionLink("OrderDesc", "SortCollection", new { collname = "stopWordsInMetaDict", sort = true }, new AjaxOptions { UpdateTargetId = "TableSortedMeta" })%>
     <%= Ajax.ActionLink("OrderAsc", "SortCollection", new { collname = "stopWordsInMetaDict", sort = false }, new AjaxOptions { UpdateTargetId = "TableSortedMeta" })%>
  
     <div id="TableSortedMeta">
    <%Html.RenderAction("SortCollection", new { collname = "stopWordsInMetaDict" });%>
      </div>
    
    <h2 >Количество стоп-слов в тексте страницы:</h2>
      <%= Ajax.ActionLink("OrderDesc", "SortCollection", new { collname = "StopWordsOnPage", sort=true }, new AjaxOptions { UpdateTargetId = "TableSorted" })%>
      <%= Ajax.ActionLink("OrderAsc", "SortCollection", new { collname = "StopWordsOnPage", sort=false }, new AjaxOptions { UpdateTargetId = "TableSorted" })%>
      <div id="TableSorted">
      <%Html.RenderAction("SortCollection", new { collname = "StopWordsOnPage" });%>
      </div>
       <h2 >Количество внешних ссылок: <%=externalLinks==null?0:externalLinks.Count%></h2>

      <h2 >Данные, помещенные для теста</h2>

      
     <a href ="<%= Url.Action("SeoAnalysis","Home") %>">sdfs</a>
      <a href ="https://vk.com/">sdfs</a>
       <a href ="https://vk.com/">sdfs</a>
        <a href ="vk.com">sdfs</a>
        <br />
        { "word1", "word2", "word3"
        { "word1", "word2", "word3"
        { "word1", "word2", "word3"


        <META NAME="Description" CONTENT="word1 статьи word3 системы ucoz"> 
<META NAME="Keywords" CONTENT="Ucoz, уникальные, статьи, word2, обучение, решение, проблемы, помощь"> 
<META NAME="Robots" CONTENT="word2"> 
<META NAME="Revisit-After" CONTENT="1 word1"> 
<META NAME="Author" CONTENT="igoshin word1"> 
</asp:Content>
