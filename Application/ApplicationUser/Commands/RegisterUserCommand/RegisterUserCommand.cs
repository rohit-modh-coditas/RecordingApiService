using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ApplicationUser.Commands.RegisterUserCommand
{
   public class RegisterUserCommand:IRequest, IMapFrom<AspNetUser>
    {
        public string Email { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<AspNetUser, RegisterUserCommand>();
        }
    }

    public class RegisterUserHandler: IRequestHandler<RegisterUserCommand, Unit>
    {
        private readonly IStoreDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RegisterUserHandler(IMediator mediator, IStoreDbContext context, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            //check if username already exists.
            var IsUserExist = _context.AspNetUsers.Find(request.Email); // .Where(x => x.Email == request.Email).FirstOrDefault();
            if (IsUserExist!=null)
            {
                throw new UserAlreadyExistException(request.Email);
            }

            var user = new AspNetUser
            {
                Id = "4",
                Email = request.Email,
                PasswordHash = request.Password
            };

            _context.AspNetUsers.Add(user);

            bool res =  _context.SaveChangesAsync(cancellationToken).IsCompleted;
            if (res)
            {
                await _mediator.Publish(new CustomerCreated { UserId = user.Email }, cancellationToken);
            }

           

            return Unit.Value;


            //register new user.
            //throw new NotImplementedException();
        }
    }
}
