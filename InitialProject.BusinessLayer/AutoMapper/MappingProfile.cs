using AutoMapper;
using Ecommerce.Core.DTO.AuthViewModel.FilesModel;
using Ecommerce.Core.DTO.AuthViewModel.RoleModel;
using Ecommerce.Core.Entity.ApplicationData;
using Ecommerce.Core.Entity.Files;
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
            CreateMap<Paths, PathsModel>()
                .ForMember(a => a.Name, s => s.MapFrom(b => b.Name))
                .ForMember(a => a.Description, s => s.MapFrom(b => b.Description));
            CreateMap<PathsModel, Paths>()
                .ForMember(a => a.Name, s => s.MapFrom(b => b.Name))
                .ForMember(a => a.Description, s => s.MapFrom(b => b.Description));
        }
    }
}
