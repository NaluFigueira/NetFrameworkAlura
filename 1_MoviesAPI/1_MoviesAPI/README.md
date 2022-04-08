### Removing default files and settings from VS project

After creating the solution on Visual Studio, remove the following files that will not be used:

- API -> Controllers -> WeatherForecastController.cs
- API -> WeatherForecast.cs

To avoid opening the browser every time when running the solution, go to the file `launchSettings.json` and remove the lines bellow in the API settings:

```json
"dotnetRunMessages": "true",
"launchBrowser": true,
"launchUrl": "swagger"
```

And also in IIS Express settings:

```json
"launchBrowser": true,
"launchUrl": "swagger"
```

### Controller

```csharp
[ApiController]
[Route("[controller]")]
public class YourController : ControllerBase
{
    //Your code here
}
```

### Models

```csharp
public class Movie
{
    public enum MovieGenre
    {
        Action,
        Comedy,
        Drama,
        Musical,
        Thriller,
        Horror
    }

    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Title field is required")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Director field is required")]
    public string Director { get; set; }

    [EnumDataType(typeof(MovieGenre), ErrorMessage = "Movie genre value must be between 0 and 6")]
    public MovieGenre Genre { get; set; }

    [Range(60, 300, ErrorMessage = "Duration must be between 60 and 300 minutes")]
    public int Duration { get; set; }

    [JsonIgnore]
    public virtual List<Session> Sessions { get; set; }

}
```

Check the full list of attributes validation [here](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-6.0).

> :warning: **It is recommended to use models only to manage migrations, to use it on the controller and service layers it's recommended to create a DTO inside `Data/DTOs` folder.**

### Connecting with database using Entity Framework

1. Install packages `Microsoft.EntityFrameworkCore`,  `Microsoft.EntityFrameworkCore.Tools` and `MySQL.EntityFrameworkCore`.
2. Create `Data` folder in project.
3. Create new class `YourContext.cs`.
4. Include `ConnectionStrings` in `user-secrets`:

```
dotnet user-secrets set "ConnectionStrings:YourConnectionName" "server=localhost;port=3306;database=YOUR_DATABASE;user=YOUR_SERVER_USER;password=YOUR_SERVER_PASSWORD"
```

5. Open `Startup.cs`, add to `ConfigureServices` the following line:

```csharp
services.AddDbContext<YourContext>(opts => opts.UseLazyLoadingProxies()
        .UseMySQL(Configuration.GetConnectionString("YourConnectionName")));
```

6. Create initial migration

```bash
# With EF Core tools
$ dotnet ef migrations add InitialCreate

# With Visual Studio Package Manager Console
$ Add-Migration InitialCreate
```

7. Update your database

```bash
# With EF Core tools
$ dotnet ef database update

# With Visual Studio Package Manager Console
$ Update-Database
```

Example of context class:

```csharp
public class MovieContext : DbContext
{
    public MovieContext(DbContextOptions<MovieContext> opt) : base(opt)
    {

    }
}
```

### Managing database context

- Creating context instance in service layer class:

```csharp
private YourContext _context;

public YourService(YourContext context)
{
    _context = context;
}
```

- Where query

```csharp
List<Entity> foundEntities = _context.Entities.Where(entity => entity.Attribute == searchedAttribute).ToList();
```

- First or default query

```csharp
Entity foundEntity = _context.Entities.FirstOrDefault(entity => entity.PrimaryKey == searchedPrimaryKey);
```

- N:N relationship query

```csharp
List<Cinema> cinemas = _context.Cinemas.ToList();

IEnumerable<Cinema> query = from cinema in cinemas
                        where cinema.Sessions.Any(session =>
                        session.Movie.Title == movieName)
                        select cinema;

cinemas = query.ToList();
```

- Insert query

```csharp
_context.Entities.Add(newEntity);
_context.SaveChanges();
```

- Update query

```csharp
_mapper.Map(updateEntityDTO, foundEntity); //check mapper section for more details
_context.SaveChanges();
```

- Delete query

```csharp
_context.Remove(foundEntity);
_context.SaveChanges();
```

### Using AutoMapper

1. Install packages `AutoMapper` and `AutoMapper.Extension`.
2. Open `Startup.cs`, add to `ConfigureServices` the following line:

```csharp
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
```

3. Create a `Profiles` folder to include your mapper files.

### Managing mappers

- Profile example:

```csharp
public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<CreateMovieDTO, Movie>();
        CreateMap<Movie, GetMovieDTO>();
        CreateMap<UpdateMovieDTO, Movie>();
    }
}
```

- Creating mapper instance in service layer class:

```csharp
private IMapper _mapper;

public YourService(IMapper mapper)
{
    _mapper = mapper;
}
```

- Common mapping

```csharp
GetMovieDTO getMovieDTO = _mapper.Map<GetMovieDTO>(foundMovie);
```

- As long as you have a map between two classes, you can map lists of these classes:

```csharp
/*
foundMovies is a list of Movie
since there is a map of Movie to GetMovieDTO
to map a list of Movie to a list of GetMovieDTO
just use the following
*/
List<GetMovieDTO> getMovieDTOs = _mapper.Map<List<GetMovieDTO>>(foundMovies);
```

- When both instances are passed as parameters, the Map function will update the information on the destination instance.

```csharp
_mapper.Map(updateMovieDTO, foundMovie);
```

For more information on AutoMapper, click [here](https://docs.automapper.org/).

### Examples of endpoints

#### POST

```csharp
[HttpPost]
[Authorize(Roles = "admin")]
public IActionResult CreateMovie([FromBody] CreateMovieDTO createMovieDTO)
{
    GetMovieDTO createdMovie = _service.CreateMovie(createMovieDTO);

    return CreatedAtAction(nameof(GetMovieById), new { Id = createdMovie.Id }, createdMovie);
}
```

#### GET

```csharp
public IActionResult GetSessions()
{
    List<GetSessionDTO> foundSessions = _service.GetSessions();

    if(foundSessions != null) return Ok(foundSessions);

    return NotFound();
}
```

#### GET WITH FILTER

```csharp
[HttpGet]
[Authorize(Roles = "admin, regular", Policy = "MinimumAge")]
public IActionResult GetMovies([FromQuery] MovieGenre? genre = null)
{
    List<GetMovieDTO> foundMovies = _service.GetMovies(genre);

    //in the service, its used a where query to find
    //the movie by genre, check Managing database context
    //for details

    if (foundMovies != null) return Ok(foundMovies);

    return NotFound();
}
```

#### GET BY ID

```csharp
[HttpGet("{id}")]
[Authorize(Roles = "admin, regular", Policy = "MinimumAge")]
public IActionResult GetMovieById(int id)
{
    GetMovieDTO foundMovie = _service.GetMovieById(id);

    if (foundMovie != null) return Ok(foundMovie);

    return NotFound();
}
```

#### UPDATE

```csharp
[HttpPut("{id}")]
public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieDTO updateMovieDTO)
{
    Result result = _service.UpdateMovie(id, updateMovieDTO);

    if (result.IsSuccess) return NoContent();

    return NotFound();
}
```

#### DELETE

```csharp
[HttpDelete("{id}")]
public IActionResult DeleteMovie(int id)
{
    Result result = _service.DeleteMovie(id);

    if (result.IsSuccess) return NoContent();

    return NotFound();
}
```

### Entity Relationships

#### 1:1

For the following example, we'll consider a relationship between a cinema and an address. A cinema can only have a single address, and vice-versa.

1. Enable lazy loading

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(
            opts => opts.UseLazyLoadingProxies()
                    .UseMySQL(Configuration.GetConnectionString("CinemaConnection")));
    }
}
```

1. Add the following information to the cinema model:

```csharp
//it's a virtual attribute so we can get the full address when
//listing our cinemas information
public virtual Address Address { get; set; }

public int AddressId { get; set; }
```

> Entity Framework (EF) uses lazy loading by default in properties marked as virtual.
> Lazy loading is a mechanism which loads information on demand, this mechanism
> make entities lighter, since their association only occurs when the method that
> has the associated data is called. When setting a property as virtual, EF will know
> that it needs to create proxy instances that will substitute that property.

2. Add the following information to the address model:

```csharp
//JsonIgnore will tell EF that the property information
//should not be displayed when a request for the property
//instance is made
[JsonIgnore]
public virtual Cinema Cinema { get; set; }
```

3. Add the following lines to the context:

```csharp
protected override void OnModelCreating(ModelBuilder builder)
{
    builder.Entity<Address>()
        .HasOne(address => address.Cinema)
        .WithOne(cinema => cinema.Address)
        .HasForeignKey<Cinema>(cinema => cinema.AddressId);
}
```

Step 3 will create a migration that adds a foreign key to the cinemas table to reference it's address id.
Thus concluding the construction of the 1:1 relationship between those instances.

#### 1:n

For the following example, we'll consider a relationship between a cinema and an manager. A cinema can have many managers, but someone can only be a manager to a single cinema.

1. Add the managers attribute to the cinema model

```csharp
public virtual List<Manager> Managers { get; set; }
```

2. Add the cinema and cinema id attribute to the manager model

```csharp
public virtual Cinema Cinema { get; set; }
public int CinemaId { get; set; }
```

3. Add the following lines to the context:

```csharp
protected override void OnModelCreating(ModelBuilder builder)
{
    builder.Entity<Manager>()
            .HasOne(manager => manager.Cinema)
            .WithMany(cinema => cinema.Managers)
            .HasForeignKey(manager => manager.CinemaId);
            //.IsRequired(false) to allow a manager to exist without
            //a cinema id (cinema id would be a nullable column)
}
```

Step 3 will create a migration that adds a foreign key to the managers table to reference it's cinema id. Also, if `IsRequired` option is not set as false, EF will add by default a cascade deletion method to the managers table, therefore if a cinema is deleted, its manager will also be deleted. That can be changed by altering `onDelete: ReferentialAction.Cascade` option in the migration file to the desired method or adding `IsRequired(false)` to the builder `onModelCreating` method.

4. To avoid loops, we're going to use AutoMapper to not return the manager's cinema data and id when getting the cinema information.

```csharp
//this can be used to avoid the use of JsonIgnore
CreateMap<Cinema, GetCinemaDTO>()
            .ForMember(cinema => cinema.Managers, opts => opts
            .MapFrom(cinema => cinema.Managers.Select
            (manager => new { manager.Id, manager.Name })));
```

Thus concluding the construction of the 1:n relationship between those instances.

#### n:n

For the following example, we'll consider a relationship between a cinema and a movie. A cinema can screen many movies, and a movie can be screened by multiple cinemas. Therefore we need to create an entity that represents this relationship, which will be called Session.

1. Creating Session model:

```csharp
public class Sessao
{
    [Key]
    [Required]
    public int Id { get; set; }
    public virtual Cinema Cinema { get; set; }
    public virtual Movie Movie { get; set; }
    public int MovieId { get; set; }
    public int CinemaId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    //EndTime can be calculated by getting
    //movie duration
}
```

2. Add session information to cinema and movie models:

```csharp
[JsonIgnore]
public virtual List<Session> Sessions { get; set; }
```

3. Add the following lines to the context:

```csharp
protected override void OnModelCreating(ModelBuilder builder)
{
    builder.Entity<Session>()
            .HasOne(session => session.Movie)
            .WithMany(movie => movie.Sessions)
            .HasForeignKey(session => session.MovieId);

    builder.Entity<Session>()
            .HasOne(session => session.Cinema)
            .WithMany(cinema => cinema.Sessions)
            .HasForeignKey(session => session.CinemaId);
    }

    public DbSet<Session> Sessions { get; set; }
}
```

Step 3 will create a migration that adds the sessions table to the database, a foreign key to the session associated cinema id and another to the movie id. sThus concluding the construction of the n:n relationship between those instances.
