# <b>ASP.NET Core 6.x MVC Boiler plate</b>
 
An ASP.NET Core 6.x MVC project template for a quick start. 

Versions:  
Framework <b>.NET 6.x</b>  
Language <b>C# 10</b>

# Functionalities / Features

<ul>
  <li>
    User/Role Management with <b>ASP.NET Identity</b>
  </li>
 <li>
  Hangfire 
   <ol>Dashboard</ol>
   <ol>Authorization</ol>
    <ol>Custom Queue/Worker management</ol>
 </li>
 <li>
  Swagger
    <ol>API Documentation</ol>
 </li>
 <li>
  Logging with Serilog
    <ol>SQL Implementation </ol>
     <ol>Custom RequestId </ol>
 </li>
<li>
  Admin Panel
  <ol>Campaign Management</ol>
  <ol>API Credential Management</ol>
  <ol>Hangfire Dashboard Access</ol>
</li>
<li>
  Campaign Management 
  <ol>Campaign State Management by market (Open / PreSeason / PostSeason)</ol>
</li>
 <li>
  Response Caching
 </li>
  <li>
  Markup Minification
 </li>
 <li>
  Response Compression
 </li>
 <li>
   Error Handling
 </li>
 <li>
   Generic Repository
 </li>
 <li>
 Code First
 </li>
   <li>
   Generic Response Wrapper as a Middleware
 </li>
 <li>
   RESTful API support
 </li>
 <li>
   Routing
 </li>
  <li>
    Cache Services 
  </li>
  <li>
    Globalization and Localization
     <ol>Determining the culture by the URL or a route parameter</ol>
  </li>
   <li>
    Site Settings Management 
  </li>
  <li>
   Encyription 
  </li>
  <li>
   Automated Tests
    <ol>A scalable test automation infrastructure with Selenium + NUnit + Bogus </ol>
    <ol>Fakers</ol>
    <ol>User Tests</ol>
    <ol>Browser drivers (Chrome, Edge, Firefox)</ol>
  </li>
</ul>

# How to run on your local machine? 
You just need to replace the connection strings for the main and hangfire in appsettings.json with yours. Check the user privileges in the connection strings, the user should be able to create a database. 

![image](https://user-images.githubusercontent.com/1719611/116728657-1845e600-a9de-11eb-8200-45b84251a4e5.png)


IIS Express should be chosen from the launch configuration drop down.

![image](https://user-images.githubusercontent.com/1719611/116729510-2811fa00-a9df-11eb-8239-bcbb8529ccec.png)


If you need to work with multiple environments then replace the connection strings in equivalent appsettings.{env}.json. 

![image](https://user-images.githubusercontent.com/1719611/116729709-660f1e00-a9df-11eb-9620-b711dd73ce70.png)


# Layers and Structure

![image](https://user-images.githubusercontent.com/1719611/116734556-4e3a9880-a9e5-11eb-8e94-259bc1aa38bb.png)


# 1-Data 

Contains only database CRUD operations via a generic repository. 
<ul>
 <li>
    BoilerPlate.Data
     <ol>Data Contexts</ol>
     <ol>Repositories</ol>
    <ol>Extensions</ol>
  </li>
   <li>
    BoilerPlate.Data.Contracts
     <ol>Contracts</ol>
  </li>
   <li>
    BoilerPlate.Data.Entities
     <ol>Database Objects</ol>
  </li>
</ul>

# 2-Service

All business services, contracts and entities are in this layer.
Contains only database CRUD operations via a generic repository. 
<ul>
 <li>
    BoilerPlate.Service
     <ol>Business Services</ol>
  </li>
   <li>
      BoilerPlate.Service.Contracts
     <ol>Contracts</ol>
  </li>
   <li>
   BoilerPlate.Service.Entities
     <ol>Business Objects</ol>
  </li>
</ul>

# 3-Web

Web application and it's entities place in this layer.
<ul>
 <li>
    BoilerPlate.Web
     <ol>MVC Application</ol>
  </li>
   <li>
      BoilerPlate.Service.Contracts
     <ol>Contracts</ol>
  </li>
   <li>
   BoilerPlate.Web.Entities
     <ol>Models and API request/response models</ol>
  </li>
</ul>

# 4-Test

Test automations with Selenium + NUnit + Bogus

<ul>
 <li>
    BoilerPlate.Test
     <ol>Drivers</ol>
    <ol>Fakers</ol>
    <ol>PageObjects</ol>
    <ol>UserTests</ol>
</ul>

# 5-Common
<ul>
 <li>
    BoilerPlate.Bootstrapper
     <ol>Dependencies</ol>
     <ol>Middlewares</ol>
     <ol>Attributes</ol>
     <ol>Localization</ol>
  </li>
   <li>
      BoilerPlate.Utils
     <ol>Configuration</ol>
     <ol>Constants</ol>
     <ol>Enums</ol>
     <ol>Exceptions</ol>
     <ol>Extensions</ol>
      <ol>Helpers</ol>
  </li>
   <li>
   BoilerPlate.Web.Entities
     <ol>View Models </ol>
     <ol>API Models</ol>
  </li>
</ul>


