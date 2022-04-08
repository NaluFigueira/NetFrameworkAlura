## Summary

- [Configuring Identity](#configuring-identity)
- [Creating user](#creating-user)
- [Signing in](#signing-in)
- [Generating JWT Token](#generating-jwt-token)
- [Signing out](#signing-out)
- [Email confirmation](#email-confirmation)
- [Password reset](#password-reset)
- [Adding admin user and role](#adding-admin-user-and-role)
- [Adding other roles](#adding-other-roles)
- [Save role information in token](#save-role-information-in-token)

### Configuring Identity

1. Install the following packages:

- Microsoft.AspNetCore.Identity
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Proxies
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.Extensions.Identity.Stores
- MySql.EntityFrameworkCore

2. Add user database context:

```csharp
public class UserDBContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
{
    public UserDBContext(DbContextOptions<UserDBContext> opt) : base(opt)
    {
    }

    //the following code is only necessary if a key name length problem
    //occurs in the database creation migration
    //try without it first
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        int stringMaxLength = 200;

        builder.Entity<IdentityRole>(x => x.Property(m => m.Name).HasMaxLength(stringMaxLength));
        builder.Entity<IdentityRole>(x => x.Property(m => m.NormalizedName).HasMaxLength(stringMaxLength));
        builder.Entity<IdentityUser>(x => x.Property(m => m.NormalizedUserName).HasMaxLength(stringMaxLength));

        builder.Entity<IdentityUserLogin<int>>(x => x.Property(m => m.LoginProvider).HasMaxLength(stringMaxLength));
        builder.Entity<IdentityUserLogin<int>>(x => x.Property(m => m.ProviderKey).HasMaxLength(stringMaxLength));

        builder.Entity<IdentityUserToken<int>>(x => x.Property(m => m.LoginProvider).HasMaxLength(stringMaxLength));
        builder.Entity<IdentityUserToken<int>>(x => x.Property(m => m.Name).HasMaxLength(stringMaxLength));
    }
}
```

3. Open `Startup.cs`, add to `ConfigureServices` the following line:

```csharp
services.AddIdentity<IdentityUser<int>, IdentityRole<int>>()
                .AddEntityFrameworkStores<UserDBContext>();
```

4. Additional settings:

```csharp
services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
});
```

### Creating user

1. Add mapping between your user model and identity user model

```csharp
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, User>();
        CreateMap<User, IdentityUser<int>>();
    }
}
```

2. Add to your service an `UserManager` instance

```csharp
private UserManager<IdentityUser<int>> _userManager;

public YourService(UserManager<IdentityUser<int>> userManager)
{
    //...other code
    _userManager = userManager;
}
```

3. Add the following method to your service

```csharp
public Result CreateUser(CreateUserDto createDto)
{
    User user = _mapper.Map<User>(createDto);
    IdentityUser<int> identityUser = _mapper.Map<IdentityUser<int>>(user);
    Task<IdentityResult> identityResult = _userManager.CreateAsync(identityUser, createDto.Password);

    if (identityResult.Result.Succeeded) return Result.Ok();

    return Result.Fail("There was an error when creating the user, check input parameters and try again.");
}
```

### Signing in

1. Add to your service an `SignInManager` instance

```csharp
private SignInManager<IdentityUser<int>> _signInManager;

public YourService(SignInManager<IdentityUser<int>> signInManager)
{
    //...other code
   _signInManager = signInManager;
}
```

2. Add the following method to your service

```csharp
public Result SignIn(SignInRequest signInRequest)
{
    var identityResult = _signInManager
                .PasswordSignInAsync(signInRequest.Username, signInRequest.Password, false, false);

    if (identityResult.Result.Succeeded) return Result.Ok();

    return Result.Fail("Invalid username and password.");
}
```

### Generating JWT Token

1. Install package System.IdentityModel.Tokens.Jwt

2. Create token model

```csharp
public class Token
{
    public Token(string value)
    {
        Value = value;
    }

    public string Value { get; set; }
}
```

3. Create a `TokenService.cs` with the following method:

```csharp
public Token CreateToken(IdentityUser user)
{
    Claim[] userRights = new Claim[]
    {
        new Claim("username", user.UserName),
        new Claim("id", user.Id.ToString()),
    };

    //should set this key on secrets
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asdpajsdpoajdojcojopajcm"));

    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        claims: userRights,
        signingCredentials: credentials,
        expires: DateTime.UtcNow.AddHours(1)
    );

    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

    return new Token(tokenString);
}
```

4. Add to your sign in service

```csharp
public Result SignIn(SignInRequest signInRequest)
{
    //Check in the sign in section for the rest of the code

    if (identityResult.Result.Succeeded)
    {
        var identityUser = _signInManager
            .UserManager
            .Users
            .FirstOrDefault(user => user.NormalizedUserName == signInRequest.Username.ToUpper());

        Token token = _tokenService .CreateToken(identityUser);

        return Result.Ok().WithSuccess(token.Value);
    }

    //...other code
}
```

### Signing out

1. Add to your service an `SignInManager` instance

```csharp
private SignInManager<IdentityUser<int>> _signInManager;

public YourService(SignInManager<IdentityUser<int>> signInManager)
{
    //...other code
   _signInManager = signInManager;
}
```

2. Add the following method to your service

```csharp
public Result SignOut()
{
    var resultIdentity = _signInManager.SignOutAsync();

    if (resultIdentity.IsCompletedSuccessfully)
    {
        return Result.Ok().WithSuccess("User signed out.");
    }

    return Result.Fail("There was a problem while signing out, try again later.");
}
```

### Email confirmation

1. Install packages MailKit and MimeKit.

2. Open `Startup.cs`, add to `ConfigureServices` email requirement to Identity service:

```csharp
services.AddIdentity<IdentityUser<int>, IdentityRole<int>>(
                    opt => opt.SignIn.RequireConfirmedEmail = true // add this line
                )
                .AddEntityFrameworkStores<UserDBContext>()
                .AddDefaultTokenProviders(); // and this line
```

3. Add to your sign in service an email validation

```csharp
public Result SignIn(SignInRequest signInRequest)
{
    //Check in the sign in and token generation section for the rest of the code

    if(identityResult.Result.IsNotAllowed)
    {
        return Result.Fail("Needs e-mail confirmation to sign in");
    }

    //...other code
}
```

4. Add `Message.cs` and `EmailService.cs` to your project

5. Add to your create user method the following code, that way an activation link will be sended to the users email

```csharp
public Result CreateUser(CreateUserDto createDto)
{
    //Check in the sign up section for the rest of the code

    if (identityResult.Result.Succeeded) {
        var code = _userManager.GenerateEmailConfirmationTokenAsync(identityUser).Result;

        var encodedCode = HttpUtility.UrlEncode(code);

        _emailService.SendConfirmationEmail(
            new[] { identityUser.Email },
            "Link de Ativação",
            identityUser.Id,
            encodedCode
        );

        return Result.Ok().WithSuccess(code);
    }

    //...other code
}
```

6. Add to your sign up service the following method to activate the account

```csharp
public Result ActivateUserAccount(ActivateAccountRequest request)
{
    var identityUser = _userManager
                .Users
                .FirstOrDefault(user => user.Id == request.Id);

    var identityResult = _userManager
                .ConfirmEmailAsync(identityUser, request.ActivationCode).Result;

    if(identityResult.Succeeded)
    {
        return Result.Ok().WithSuccess("Account activated.");
    }

    return Result.Fail("An error occured while activating account");
}
```

### Password reset

1.  Include the following method to your sign in method, it'll generate a reset code

```csharp
//Request should contain the user email
//You could also send this through an email
//following the same steps as the email
//confirmation section
public Result GeneratePasswordResetCode(GeneratePasswordResetCodeRequest request)
{
    CustomIdentityUser identityUser = GetIdentityUserByEmail(request.Email);

    if (identityUser != null)
    {
        string resetCode = _signInManager
                        .UserManager
                        .GeneratePasswordResetTokenAsync(identityUser).Result;

        return Result.Ok().WithSuccess(resetCode);
    }

    return Result.Fail("Invalid user e-mail");
}
```

2. Add another method to the service, which will consume the previous method's result token

```csharp
//Request should contain the user email,
//the new password, a confirmation of the new password,
//and the token provided in GeneratePasswordResetCode
public Result PasswordReset(PasswordResetRequest request)
{
    CustomIdentityUser identityUser = GetIdentityUserByEmail(request.Email);

    if (identityUser != null)
    {
        IdentityResult identityResult = _signInManager
                                    .UserManager
                                    .ResetPasswordAsync(identityUser, request.Token, request.Password)
                                    .Result;

        if (identityResult.Succeeded)
        {
            return Result.Ok().WithSuccess("Password reset successful!");
        }
    }

    return Result.Fail("Invalid user e-mail or invalid token");
}
```

### Adding admin user and role

1. In your user context, on `OnModelCreating` method

```csharp
IdentityUser<int> admin = new IdentityUser
{
    UserName = "admin",
    NormalizedUserName = "ADMIN",
    Email = "admin@admin.com",
    NormalizedEmail = "ADMIN@ADMIN.COM",
    EmailConfirmed = true,
    SecurityStamp = Guid.NewGuid().ToString(),
    Id = 99999,
};

PasswordHasher<IdentityUser<int>> hasher = new PasswordHasher<IdentityUser<int>>();

admin.PasswordHash = hasher.HashPassword(admin,
                        _configuration.GetValue<string>("admininfo:password"));

builder.Entity<IdentityUser<int>>().HasData(admin); // adds admin user to users table

//adds admin role to roles table
builder.Entity<IdentityRole<int>>().HasData(
    new IdentityRole<int> { Id = 99999, Name = "admin", NormalizedName = "ADMIN" }
);

//binds admin user to admin role
builder.Entity<IdentityUserRole<int>>().HasData(
    new IdentityUserRole<int> { RoleId = 99999, UserId = 99999 }
);
```

### Adding other roles

1. Add the `IConfiguration` to the context and

```csharp
private IConfiguration _configuration;

public UserDBContext(DbContextOptions<UserDBContext> opt, IConfiguration configuration) : base(opt)
{
    _configuration = configuration;
}
```

2. Insert the following code in `OnModelCreating` method

```csharp
protected override void OnModelCreating(ModelBuilder builder)
{
    //...other code

    //Adding role regular
    builder.Entity<IdentityRole<int>>().HasData(
        new IdentityRole<int> { Id = 99997, Name = "regular", NormalizedName = "REGULAR" }
    );

    //...other code
}
```

3. To use the new role on the sign up service method

```csharp
public Result CreateUser(CreateUserDTO createUserDTO)
{
    //...other code

     _userManager.AddToRoleAsync(identityUser, "regular");

    if(identityResult.Result.Succeeded)
    {
        //...other code
    }

    //...other code
}
```

### Save role information in token

1. Update your token service, to add the user role information to generated tokens

```csharp
public Token CreateToken(CustomIdentityUser user, string role)
{
    Claim[] userRights = new Claim[]
            {
                new Claim("username", user.UserName),
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, role), //add this line
            };

    //check token generation section for more details on the rest of the code
}
```

2. Update your sign in service, to add the user role in the CreateToken call

```csharp
public Result SignIn(SignInRequest signInRequest)
{
    //Check in the token generation section for the rest of the code

    if (identityResult.Result.Succeeded)
    {
        //...other code

        Token token = _tokenService
                    .CreateToken(
                        identityUser,
                        _signInManager.UserManager.GetRolesAsync(identityUser).Result.FirstOrDefault()
                    );

        //...other code
    }

    //...other code
}
```
