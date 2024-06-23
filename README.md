# Hotel Reservation Application - README

## 1. Introduction
This application is a hotel reservation system built using ASP .NET Core, leveraging modern tools and best practices. The project adheres to SOLID principles, ensuring the code is clean, maintainable, and scalable.

### Project Theme:
The site represents an application for a hotel where users can book rooms of their choice. Visitors can view all rooms and make reservations based on specified criteria. If a room is occupied at the selected date, a message will be displayed indicating its unavailability. If the room is available but the user is not logged in, they will be prompted to log in to make a reservation. Logged-in users can read detailed information about specific rooms, leave comments, and make reservations.

### Users:
- **Visitors (unauthorized users):** Can browse rooms, view room details, search for rooms using filters, and contact the admin via email.
- **Registered/Logged-in Users:** Have the same privileges as visitors, plus the ability to leave comments, make reservations, and manage their own comments.
- **Admins:** Have additional privileges, including managing all system users, use cases, and searching logs. Admins can modify any part of the system through their admin panel.

## 1.2 Features
- **User Registration and Login:** Implemented using JWT tokens for authentication and authorization.
- **Database Design:** Created using the Code-First approach.
- **Validation:** Detailed validation for each endpoint, ensuring that changes to the system state adhere to defined rules. Error codes:
  - **422:** Unprocessable Entity for validation errors.
  - **409:** Conflict for entity relation issues (e.g., deleting a comment linked to a room).
  - **201:** Created for successful inserts.
  - **204:** No Content for successful updates/deletes.
- **Email Notifications:** Sends emails to users upon successful registration.
- **Search and Filtering:** Supports search and filtering for all entities, with pagination to control the number of results per page (default 10).
- **Activity Logging:** Logs all user activities, whether unauthorized or authorized.
- **JSON Data Format:** Returns data in JSON format.
- **AutoMapper:** Used for most DTO objects where necessary.
- **File Uploads:** Supports image uploads for users, services, and rooms during insertion.
- **Soft Delete:** Implemented for deletions.

### User Roles and Functionalities
#### Unauthorized User:
- View all rooms and their details.
- Register and log in.
- Send messages.

#### Registered/Logged-in User:
- All functionalities available to unauthorized users, plus:
  - Filter rooms based on criteria.
  - View all comments related to a room.
  - CRUD operations on their own comments.

#### Admin:
- Full CRUD operations on all entities.
- Search system logs based on username, use case name, and date range.
- Search all rooms and view detailed information.

## 1.3 Code Structure

### Key Components:
- **DTOs (Data Transfer Objects):** Used to transfer data between layers.
- **Queries:** Implemented using Entity Framework (EF) for retrieving data.
- **Use Cases:** Represent individual functionalities of the system.
- **Profiles:** Configured using AutoMapper for mapping domain models to DTOs.
- **Validation:** Ensures that all inputs meet defined criteria.

### Sample Code:
Below is a simplified version of some key parts of the application, demonstrating the structure and functionality:

#### Get Reservations Query:
```csharp
using Application.DTO;
using Application.DTO.Reservations;
using Application.UseCases.Queries.Reservations;
using DataAccess;
using Domain;
using Implementation.Extensions;
using AutoMapper;
using System;
using System.Linq;

namespace Implementation.UseCases.Queries.Reservations
{
    public class EfGetReservationsQuery : EfUseCase, IGetReservationsQuery
    {
        private readonly IMapper _mapper;
        public EfGetReservationsQuery(HotelHorizonContext context, IMapper mapper) 
            : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 38;
        public string Name => "Search reservations";

        public PagedResponse<ReservationDTO> Execute(SearchReservation search)
        {
            IQueryable<Reservation> query = Context.Reservations.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.User.FirstName.Contains(search.Keyword.ToLower()) ||
                                         x.User.LastName.Contains(search.Keyword.ToLower()));
            }
            if (search.CheckIn.HasValue)
            {
                query = query.Where(x => x.CheckIn >= search.CheckIn);
            }
            if (search.CheckOut.HasValue)
            {
                query = query.Where(x => x.CheckOut <= search.CheckOut);
            }
            if (search.IsActive.HasValue)
            {
                query = query.Where(x => x.IsActive == search.IsActive.Value);
            }

            return query.Paged<ReservationDTO, Reservation>(search, _mapper);
        }
    }
}
```

#### Get Actor Method:
```csharp
using Application;
using Implementation;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace API.Core
{
    public class JwtApplicationActorProvider : IApplicationActorProvider
    {
        private readonly string authorizationHeader;
        private readonly ITokenStorage _tokenStorage;

        public JwtApplicationActorProvider(string authorizationHeader, ITokenStorage storage)
        {
            this.authorizationHeader = authorizationHeader;
            _tokenStorage = storage;
        }

        public IApplicationActor GetActor()
        {
            if (authorizationHeader.Split("Bearer ").Length != 2)
            {
                return new UnathorizedActor();
            }

            string token = authorizationHeader.Split("Bearer ")[1];
            var handler = new JwtSecurityTokenHandler();
            var tokenObj = handler.ReadJwtToken(token);
            var claims = tokenObj.Claims;

            var jtiClaim = claims.FirstOrDefault(x => x.Type == "jti")?.Value;

            if (string.IsNullOrEmpty(jtiClaim) || !Guid.TryParse(jtiClaim, out Guid jtiGuid))
            {
                return new UnathorizedActor();
            }

            if (!_tokenStorage.Exists(jtiGuid))
            {
                return new UnathorizedActor();
            }

            var actor = new Actor
            {
                Email = claims.FirstOrDefault(x => x.Type == "Username")?.Value,
                Username = claims.FirstOrDefault(x => x.Type == "Username")?.Value,
                FirstName = claims.FirstOrDefault(x => x.Type == "FirstName")?.Value,
                LastName = claims.FirstOrDefault(x => x.Type == "LastName")?.Value,
                Id = int.Parse(claims.FirstOrDefault(x => x.Type == "Id")?.Value ?? "0"),
                AllowedUseCases = JsonConvert.DeserializeObject<List<int>>(claims.FirstOrDefault(x => x.Type == "UseCaseIds")?.Value)
            };

            return actor;
        }
    }
}
```

## 2. Running the Application
1. Clone the repository.
2. Set up the database using the provided migrations.
3. Configure the appsettings.json file with your database connection string and other settings.
4. Build and run the application.

## 3. Conclusion
This hotel reservation application is built with a focus on scalability, maintainability, and adherence to best practices. By using modern technologies and following SOLID principles, the application ensures a robust and flexible system for managing hotel reservations.

Enjoy using the application!
