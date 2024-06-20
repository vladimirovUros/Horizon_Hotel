using Application;
using Application.UseCases;
using Application.UseCases.Commands.Beds;
using Application.UseCases.Commands.Comments;
using Application.UseCases.Commands.Messages;
using Application.UseCases.Commands.Reservations;
using Application.UseCases.Commands.Rooms;
using Application.UseCases.Commands.Services;
using Application.UseCases.Commands.Types;
using Application.UseCases.Commands.Users;
using Application.UseCases.Queries.AuditLogs;
using Application.UseCases.Queries.Beds;
using Application.UseCases.Queries.Comments;
using Application.UseCases.Queries.Messages;
using Application.UseCases.Queries.Reservations;
using Application.UseCases.Queries.Rooms;
using Application.UseCases.Queries.Services;
using Application.UseCases.Queries.Types;
using Application.UseCases.Queries.Users;
using AutoMapper;
using Implementation.Email;
using Implementation.Extensions;
using Implementation.Logging.UseCases;
using Implementation.Profiles.Types;
using Implementation.UseCases;
using Implementation.UseCases.Commands.Beds;
using Implementation.UseCases.Commands.Comments;
using Implementation.UseCases.Commands.Messages;
using Implementation.UseCases.Commands.Reservations;
using Implementation.UseCases.Commands.Rooms;
using Implementation.UseCases.Commands.Services;
using Implementation.UseCases.Commands.Types;
using Implementation.UseCases.Commands.Users;
using Implementation.UseCases.Queries.AuditLogs;
using Implementation.UseCases.Queries.Beds;
using Implementation.UseCases.Queries.Comments;
using Implementation.UseCases.Queries.Messages;
using Implementation.UseCases.Queries.Reservations;
using Implementation.UseCases.Queries.Rooms;
using Implementation.UseCases.Queries.Services;
using Implementation.UseCases.Queries.Types;
using Implementation.UseCases.Queries.Users;
using Implementation.Validators.Beds;
using Implementation.Validators.Comments;
using Implementation.Validators.Messages;
using Implementation.Validators.Reservations;
using Implementation.Validators.Rooms;
using Implementation.Validators.Services;
using Implementation.Validators.Types;
using Implementation.Validators.Users;
using Microsoft.AspNetCore.Hosting;
using System.IdentityModel.Tokens.Jwt;

namespace API.Core
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<IUseCaseLogger, EfUseCaseLogger>();
            services.AddTransient<UseCaseHandler>();
            services.AddTransient<RegisterUserDtoValidator>();
            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();
            services.AddTransient<UpdateUserDtoValidator>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<UpdateUserImageValidator>();
            services.AddTransient<IUpdateUserImageCommand, EfUpdateUserImageCommand>();
            services.AddTransient<ICreateBedCommand, EfCreateBedCommand>();
            services.AddTransient<CreateBedDtoValidator>();
            services.AddTransient<IUpdateBedCommand, EfUpdateBedCommand>();
            services.AddTransient<UpdateBedDtoValidator>();
            services.AddTransient<IDeleteBedCommand, EfDeleteBedCommand>();
            //services.AddAutoMapper(typeof(QueryableExtensions).Assembly);
            
            services.AddTransient<ICreateTypeCommand, EfCreateTypeCommand>();
            services.AddTransient<CreateTypeDtoValidator>();
            services.AddTransient<IUpdateTypeCommand, EfUpdateTypeCommand>();
            services.AddTransient<UpdateTypeDtoValidator>();
            services.AddTransient<IDeleteTypeCommand, EfDeleteTypeCommand>();
            services.AddTransient<ICreateServiceCommand, EfCreateServiceCommand>();
            services.AddTransient<CreateServiceDtoValiator>();
            services.AddTransient<IUpdateServiceCommand, EfUpdateServiceCommand>();
            services.AddTransient<UpdateServiceDtoValidator>();
            services.AddTransient<IDeleteServiceCommand, EfDeleteServiceCommand>();

            services.AddAutoMapper(typeof(EfGetBedsQuery).Assembly);
            services.AddTransient<IGetBedsQuery, EfGetBedsQuery>();
            services.AddAutoMapper(typeof(EfGetTypesQuery).Assembly);
            services.AddTransient<IGetTypesQuery, EfGetTypesQuery>();
            services.AddAutoMapper(typeof(EfGetServicesQuery).Assembly);
            services.AddTransient<IGetServicesQuery, EfGetServicesQuery>();

            services.AddTransient<ICreateRoomCommand, EfCreateRoomCommand>();
            services.AddTransient<CreateRoomDtoValidator>();

            services.AddTransient<IEmailSender, SMTPEmailSender>();

            services.AddTransient<IUpdateRoomCommand, EfUpdateRoomCommand>();
            services.AddTransient<UpdateRoomDtoValidator>();

            services.AddTransient<IDeleteRoomCommand, EfDeleteRoomCommand>();

            services.AddTransient<IGetRoomsQuery, EfGetRoomsQuery>();

            services.AddTransient<IGetRoomQuery, EfGetRoomQuery>();
            services.AddAutoMapper(typeof(EfGetRoomQuery).Assembly);

            services.AddAutoMapper(typeof(EfGetUsersQuery).Assembly);
            services.AddAutoMapper(typeof(EfGetBedQuery).Assembly);
            services.AddTransient<IGetBedQuery, EfGetBedQuery>();

            //services.AddTransient(typeof(ICommand<>), typeof(EfGenericDeleteCommand<>));

            services.AddTransient<ICommand<int>, EfDeleteRoomCommand>();
            services.AddTransient<ICommand<int>, EfDeleteServiceCommand>();
            services.AddTransient<ICommand<int>, EfDeleteTypeCommand>();
            services.AddTransient<ICommand<int>, EfDeleteUserCommand>();
            services.AddTransient<ICommand<int>, EfDeleteBedCommand>();
            services.AddTransient<ICommand<int>, EfDeleteCommentCommand>();
            services.AddTransient<ICommand<int>, EfDeleteReservationCommand>();


            services.AddAutoMapper(typeof(EfGetUserQuery).Assembly);
            services.AddTransient<IGetUserQuery, EfGetUserQuery>();

            services.AddAutoMapper(typeof(EfGetServiceQuery).Assembly);
            services.AddTransient<IGetServiceQuery, EfGetServiceQuery>();

            services.AddAutoMapper(typeof(EfGetTypeQuery).Assembly);
            services.AddTransient<IGetTypeQuery, EfGetTypeQuery>();

            services.AddTransient<CreateCommentDtoValidator>();
            services.AddTransient<ICreateCommentCommand, EfCreateCommentCommand>();
            services.AddTransient<UpdateCommentDtoValidator>();
            services.AddTransient<IUpdateCommentCommand, EfUpdateCommentCommand>();
            services.AddTransient<IDeleteCommentCommand, EfDeleteCommentCommand>();

            services.AddAutoMapper(typeof(EfCreateCommentCommand).Assembly);
            services.AddAutoMapper(typeof(EfUpdateCommentCommand).Assembly);

            services.AddTransient<IGetCommentQuery, EfGetCommentQuery>();
            services.AddTransient<IGetCommentsQuery, EfGetCommentsQuery>();
            services.AddAutoMapper(typeof(EfCreateMessageCommand));
            services.AddTransient<ICreateMessageCommand, EfCreateMessageCommand>();
            services.AddTransient<CreateMessageDtoValidator>();
            services.AddTransient<IDeleteMessageCommand, EfDeleteMessageCommand>();

            services.AddTransient<IGetMessagesQuery, EfGetMessagesQuery>();

            services.AddTransient<ICreateReservationCommand, EfCreateReservationCommand>();
            services.AddTransient<CreateReservationDtoValidator>();
            services.AddTransient<CheckReservationDtoValidator>();
            services.AddTransient<IUpdateReservationCommand, EfUpdateReservationCommand>();
            services.AddTransient<UpdateReservationDtoValidator>();
            services.AddTransient<IDeleteReservationCommand, EfDeleteReservationCommand>();

            services.AddAutoMapper(typeof(EfGetReservationsQuery).Assembly);
            services.AddTransient<IGetReservationsQuery, EfGetReservationsQuery>();

            services.AddAutoMapper(typeof(EfGetAuditLogsQuery).Assembly);
            services.AddTransient<IGetAuditLogsQuery, EfGetAuditLogsQuery>();

            services.AddTransient<UpdateUserAccessDtoValidator>();
            services.AddTransient<IUpdateUseAccessCommand, EfUpdateUserAcessCommand>();



            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<TypeProfile>();
            //});


        }

        public static Guid? GetTokenId(this HttpRequest request)
        {
            if (request == null || !request.Headers.ContainsKey("Authorization"))
            {
                return null;
            }

            string authHeader = request.Headers["Authorization"].ToString();

            if (authHeader.Split("Bearer ").Length != 2)
            {
                return null;
            }

            string token = authHeader.Split("Bearer ")[1];

            var handler = new JwtSecurityTokenHandler();

            var tokenObj = handler.ReadJwtToken(token);

            var claims = tokenObj.Claims;

            var claim = claims.First(x => x.Type == "jti").Value;

            var tokenGuid = Guid.Parse(claim);

            return tokenGuid;
        }
    }
}
