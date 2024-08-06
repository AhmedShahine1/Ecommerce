using AutoMapper;
using Ecommerce.Core.DTO.AuthViewModel.CityModel;
using Ecommerce.Core.DTO.AuthViewModel.FilesModel;
using Ecommerce.Core.DTO.AuthViewModel.RegisterModel;
using Ecommerce.Core.DTO.AuthViewModel.RoleModel;
using Ecommerce.Core.Entity.ApplicationData;
using Ecommerce.Core.Entity.Files;
using Ecommerce.Core.Entity.Others;
using Elfie.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BusinessLayer.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RoleDTO, ApplicationRole>()
                .ForMember(a => a.Name, s => s.MapFrom(b => b.RoleName))
                .ForMember(a => a.ArName, s => s.MapFrom(b => b.RoleAr))
                .ForMember(a => a.Description, s => s.MapFrom(b => b.RoleDescription));
            CreateMap<ApplicationRole, RoleDTO>()
                .ForMember(a => a.RoleName, s => s.MapFrom(b => b.Name))
                .ForMember(a => a.RoleAr, s => s.MapFrom(b => b.ArName))
                .ForMember(a => a.RoleDescription, s => s.MapFrom(b => b.Description));
            //--------------------------------------------------------------------------------------------------------
            CreateMap<Paths, PathsModel>()
                .ForMember(a => a.Name, s => s.MapFrom(b => b.Name))
                .ForMember(a => a.Description, s => s.MapFrom(b => b.Description));
            CreateMap<PathsModel, Paths>()
                .ForMember(a => a.Name, s => s.MapFrom(b => b.Name))
                .ForMember(a => a.Description, s => s.MapFrom(b => b.Description));
            //--------------------------------------------------------------------------------------------------------
            CreateMap<City, CityModel>()
                .ForMember(a => a.NameEn, s => s.MapFrom(b => b.NameEn))
                .ForMember(a => a.NameAr, s => s.MapFrom(b => b.NameAr))
                .ForMember(a => a.CountryAr, s => s.MapFrom(b => b.CountryAr))
                .ForMember(a => a.CountryEn, s => s.MapFrom(b => b.CountryEn));
            CreateMap<CityModel, City>()
                .ForMember(a => a.NameEn, s => s.MapFrom(b => b.NameEn))
                .ForMember(a => a.NameAr, s => s.MapFrom(b => b.NameAr))
                .ForMember(a => a.CountryAr, s => s.MapFrom(b => b.CountryAr))
                .ForMember(a => a.CountryEn, s => s.MapFrom(b => b.CountryEn));
            //--------------------------------------------------------------------------------------------------------
            CreateMap<ApplicationUser, RegisterAdmin>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId));

            CreateMap<RegisterAdmin, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId))
                .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => true));
        }
    }
}
