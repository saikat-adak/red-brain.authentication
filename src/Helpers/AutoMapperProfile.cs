using AutoMapper;
using RedBrain.Authentication.Entities;
using RedBrain.Authentication.Models.Users;

namespace RedBrain.Authentication.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();
        }
    }
}