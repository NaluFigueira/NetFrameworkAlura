<h1 align="center">
    ASP.NET Core REST APIs
</h1>

<h4 align="center">
  Alura Training
</h4>
<p align="center">
  <img alt="GitHub top language" src="https://img.shields.io/github/languages/top/NaluFigueira/NetFrameworkAlura.svg">

  <img alt="GitHub language count" src="https://img.shields.io/github/languages/count/NaluFigueira/NetFrameworkAlura.svg">

  <img alt="Repository size" src="https://img.shields.io/github/repo-size/NaluFigueira/NetFrameworkAlura.svg">
  <a href="https://github.com/NaluFigueira/NetFrameworkAlura/commits/master">
    <img alt="GitHub last commit" src="https://img.shields.io/github/last-commit/NaluFigueira/NetFrameworkAlura.svg">
  </a>

  <a href="https://github.com/NaluFigueira/NetFrameworkAlura/issues">
    <img alt="Repository issues" src="https://img.shields.io/github/issues/NaluFigueira/NetFrameworkAlura.svg">
  </a>
</p>

<p align="center">
  <a href="#bulb-main-features">Technologies</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#rocket-technologies">Technologies</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#information_source-how-to-use">How To Use</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
</p>

## :bulb: Main Features

This project documents everything learned on Alura's ASP.NET Core REST APIs training. For now the main project approaches the following topics:

- Essential procedures with HTTP methods
- Entities relationships
- Management, authentication and authorization of users with Identity

## :rocket: Technologies

This project was developed with the following technologies:

- [ASP.NET Core](https://dotnet.microsoft.com/en-us/apps/aspnet) and its tools: Authentication Jwt Bearer, Authorization and Identity
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) and its tools: Proxies and Tools
- [AutoMapper](https://github.com/AutoMapper/AutoMapper)
- [FluentResults](https://github.com/altmann/FluentResults)
- [MailKit and MimeKit](http://www.mimekit.net/)
- [MySQL Community Server](https://dev.mysql.com/downloads/mysql/)
- [Visual Studio](https://visualstudio.microsoft.com/)

## :information_source: How To Use

To clone and run this application, you'll need [Git](https://git-scm.com) installed on your computer. From your command line:

```bash
# Clone this repository
$ git clone https://github.com/NaluFigueira/NetFrameworkAlura

# Go into the repository
$ cd NetFrameworkAlura
```

Open the `1_MoviesAPI.sln` in Visual Studio, on Manage NuGet Packages feature, add the following packages to `1_MoviesAPI` project (this project is using .NET 5.0):

| Package name                                        | Version |
| --------------------------------------------------- | ------- |
| AutoMapper                                          | 11.0.1  |
| AutoMapper.Extensions.Microsoft.DependencyInjection | 11.0.0  |
| FluentResults                                       | 3.2.0   |
| Microsoft.AspNetCore.Authentication.JwtBearer       | 3.1.10  |
| Microsoft.AspNetCore.Authorization                  | 3.1.0   |
| Microsoft.EntityFrameworkCore                       | 3.1.10  |
| Microsoft.EntityFrameworkCore.Proxies               | 3.1.10  |
| Microsoft.EntityFrameworkCore.Tools                 | 3.1.10  |
| MySql.Data.EntityFrameworkCore                      | 8.0.22  |

And the following to the `2_UsuarioAPI` project:

| Package name                                        | Version |
| --------------------------------------------------- | ------- |
| AutoMapper                                          | 11.0.1  |
| AutoMapper.Extensions.Microsoft.DependencyInjection | 11.0.0  |
| FluentResults                                       | 3.2.0   |
| MailKit                                             | 3.1.1   |
| Microsoft.AspNetCore.Identity                       | 2.2.0   |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore   | 3.1.10  |
| Microsoft.EntityFrameworkCore                       | 3.1.10  |
| Microsoft.EntityFrameworkCore.Proxies               | 3.1.10  |
| Microsoft.EntityFrameworkCore.Tools                 | 3.1.10  |
| Microsoft.Extensions.Identity.Stores                | 3.1.10  |
| MimeKit                                             | 3.1.1   |
| MySql.Data.EntityFrameworkCore                      | 8.0.22  |
| Swashbuckle.AspNetCore                              | 5.6.3   |
| System.IdentityModel.Tokens.Jwt                     | 6.16.0  |

Fill your the example file `secrets.json` with your database and gmail credentials. It's recommended that you create a test gmail account, in order to test user signing, as it's necessary to get account creation confirmation link.

:warning: In order to receive signing confirmation email it's necessary to [activate Gmail's Less secure apps settings](https://hotter.io/docs/email-accounts/secure-app-gmail/).

After that run the following command in the projects folder to set user secrets:

```bash
# Windows
$ type .\secrets.json | dotnet user-secrets set

# Mac/Linux
$ cat ./secrets.json | dotnet user-secrets set
```

To apply database migrations, you can either use [EF Core tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet) or [Package Manager Console in Visual Studio](https://docs.microsoft.com/en-us/ef/core/cli/powershell).

```bash
# With EF Core tools
$ dotnet ef database update

# With Visual Studio Package Manager Console
$ Update-Database
```

---

Made with ♥ by Ana Figueira :wave: [Get in touch!](https://www.linkedin.com/in/ana-lu%C3%ADsa-chaves-figueira-38792218a/)
